using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserExplorer.ViewDataModel
{
    public class MainWindowModel : NotifyChanger
    {
        //Текущий фрейм
        public object thisFrame { get { return Get<object>(); }set { Set(value); } }
        //Отображаемая коллекция клиентов
        public ObservableCollection<Data.Models.Client> Items { get { return Data.ClientsRepository.Repo.GetAllClients(); } }

        #region Commands
        // Команда добавления нового клиента
        public Command Add_Command { get; private set; }

        #endregion

        #region Constructor
        public MainWindowModel()
        {
            Add_Command = Command.RegisterCommand(On_Add_Command); //Регистрация обработчика
            
            FrameHelper.SetFrame = (x)=>{ thisFrame = FrameHelper.GetFrame(x); }; //Функция обновляет текущий фрейм
            FrameHelper.SetFrame(typeof(Controls.AddClientUC)); //Установим фрейм по умолчанию
        }
        #endregion

        #region Command methods
        //Обработчик команды добавления нового клиента
        private void On_Add_Command(object obj)
        {
            //Выводим новый фрейм = добавление клиента
            FrameHelper.SetFrame(typeof(Controls.AddClientUC));
        }
        #endregion
    }
}
