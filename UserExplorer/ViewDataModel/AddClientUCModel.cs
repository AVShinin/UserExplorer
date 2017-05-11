using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserExplorer.ViewDataModel
{
    public class AddClientUCModel : NotifyChanger
    {
        #region Public Property
        //Фамилия
        public string Family { get { return Get<string>(); } set { Set(value); } }
        //Имя
        public string Name { get { return Get<string>(); } set { Set(value); } }
        //Дата рождения
        public DateTime? BirthDate { get { return Get<DateTime?>(true); } set { Set(value); } }
        //Сообщение пользователю
        public string Message { get { return Get<string>(); }set { Set(value); } }
        #endregion

        #region Commands
        //Команда кнопки "Готово"
        public Command Complete_Command { get; private set; }
        #endregion

        #region Contructor
        public AddClientUCModel()
        {
            //Регистрируем команду и создаем обработчик
            Complete_Command = Command.RegisterCommand(On_Complete_Command);
        }
        #endregion

        #region Command Methods
        //Обработчик комманды
        private async void On_Complete_Command(object obj)
        {
            Message = "Ожидайте...";
            try
            {
                //Проверяем заполнение обязательных полей
                if (!BirthDate.HasValue || string.IsNullOrWhiteSpace(Family)) throw new Exception("Не заполнены обязательные поля");
                //Все хорошо, добавляем в базу данных
                await Data.ClientsRepository.Repo.AddClient(new Data.Models.Client(Family, Name, BirthDate.Value));
                //Сбрасываем переменные
                Clear();
            }
            catch(Exception e)
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
            Family = string.Empty;
            Name = string.Empty;
            BirthDate = new DateTime?();
            Message = string.Empty;
        }
    }
}
