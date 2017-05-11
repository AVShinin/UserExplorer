using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UserExplorer.ViewDataModel
{
    public class EditClientUCModel : NotifyChanger
    {
        //Делегат для передачи Идентификатора в модель
        public delegate void SetEditIDDelegate(int id);
        //Статическая реализация делегата
        public static SetEditIDDelegate SetID;

        #region Public Property
        //Идентификатор
        public int ID { get { return Get<int>(); } set { Set(value); } }
        //Фамилия
        public string Family { get { return Get<string>(); } set { Set(value); } }
        //Имя
        public string Name { get { return Get<string>(); } set { Set(value); } }
        //Дата рождения
        public DateTime? BirthDate { get { return Get<DateTime?>(true); } set { Set(value); } }
        //Сообщение пользователю
        public string Message { get { return Get<string>(); } set { Set(value); } }
        #endregion

        #region Commands
        //Команда кнопки "Готово"
        public Command Complete_Command { get; private set; }
        //Команда кнопки "Удалить"
        public Command Remove_Command { get; private set; }
        #endregion

        #region Contructor
        public EditClientUCModel()
        {
            //Устанавливаем метод для делегата
            SetID = On_SetID;

            //Регистрируем команду и создаем обработчик
            Complete_Command = Command.RegisterCommand(On_Complete_Command);
            //Регистрируем команду и создаем обработчик
            Remove_Command = Command.RegisterCommand(On_Remove_Command);
        }
        #endregion

        #region Command Methods
        //Обработчик комманды Готово
        private async void On_Complete_Command(object obj)
        {
            Message = "Ожидайте...";
            try
            {
                //Проверяем заполнение обязательных полей
                if (!BirthDate.HasValue || string.IsNullOrWhiteSpace(Family)) throw new Exception("Не заполнены обязательные поля");
                //Все хорошо, находим и обновляем клиента
                var client = await Data.ClientsRepository.Repo.GetClient(ID);
                client.Family = Family;
                client.Name = Name;
                client.BirthDate = BirthDate.Value;
                //Записываем изменения в базу данных
                await Data.ClientsRepository.Repo.Update();
                //Сбрасываем сообщение
                Message = "Сохранено";
            }
            catch (Exception e)
            {
                //Если есть ошибки, показываем их пользователю
                Message = e.Message;
            }
        }
        //Обработчик комманды Удалить
        private async void On_Remove_Command(object obj)
        {
            //Спрашиваем пользователя, действительно ли он хочет удалить? В случае отказа выходим
            if (MessageBox.Show("Внимание!\nВы действительно хотите удалить клиента?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            Message = "Ожидайте...";
            try
            {
                //Удаляем клиента
                await Data.ClientsRepository.Repo.RemoveClient(ID);
                //Если нет ошибок, меняем фрейм
                FrameHelper.SetFrame(typeof(Controls.AddClientUC));
            }
            catch (Exception e)
            {
                //Если есть ошибки, показываем их пользователю
                Message = e.Message;
            }
        }
        #endregion

        /// <summary>
        /// Сбрасывает переменные
        /// </summary>
        private void Clear()
        {
            ID = 0;
            Family = string.Empty;
            Name = string.Empty;
            BirthDate = new DateTime?();
            Message = string.Empty;
        }

        
        /// <summary>
        /// Обработчик метода уставливающего Идентификатор для изменения клиента
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private async void On_SetID(int id)
        {
            //Чистим фрейм перед установкой переменных
            Clear();
            //Задаем Идентификатор и сообщение об ожидании
            ID = id;
            Message = "Ожидайте...";
            try
            {
                //Получаем клиента по Идентификатору и заполняем локальные переменные
                var client = await Data.ClientsRepository.Repo.GetClient(ID);
                Family = client.Family;
                Name = client.Name;
                BirthDate = client.BirthDate;
                //Сбрасываем сообщение
                Message = string.Empty;
            }
            catch (Exception e)
            {
                //Если есть ошибки, показываем их пользователю
                Message = e.Message;
            }
        }
    }
}
