using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserExplorer.Data.Models;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace UserExplorer.Data
{
    public class ClientsRepository
    {
        //Статический экземпляр класса
        public static ClientsRepository Repo { get; set; } = new ClientsRepository();

        //Контекст базы данных
        public AppDbContext dbContext;

        //Конструктор
        public ClientsRepository()
        {
            //Новый экземпляр класса
            dbContext = new AppDbContext();
            //оптимизируем запросы отключение автопроверок
            dbContext.Configuration.AutoDetectChangesEnabled = false;
        }

        #region Public Methods
        /// <summary>
        /// Добавляет нового клиента
        /// </summary>
        /// <param name="client">Клиент</param>
        public async Task<Client> AddClient(Client client)
        {
            //Загружаем данные
            dbContext.Clients.Load();
            //Проверяем наличие клиента
            if (client.ID != 0 && dbContext.Clients.Any(x => x.ID.Equals(client.ID))) throw new Exception("Клиент с таким идентификатором уже существует в базе данных!");
            //Иначе добавляем в коллекцию
            client = dbContext.Clients.Add(client);
            //Сохраняемся
            await Update();
            //Возвращаем эеземпляр клиента
            return client;
        }
        /// <summary>
        /// Возвращает всех клиентов в базе данных
        /// </summary>
        /// <returns>Перечисление клиентов</returns>
        public ObservableCollection<Client> GetAllClients()
        {
            //Загружаем данные
            dbContext.Clients.Load();
            //Получаем всех клиентов из локальной коллекции и возвращаем их
            var all = dbContext.Clients.Local;
            return all;
        }
        /// <summary>
        /// Возвращет клиента найденного по идентификатору
        /// </summary>
        /// <param name="clientID">Идентификатор</param>
        /// <returns>Экземпляр клиента</returns>
        public async Task<Client> GetClient(int clientID)
        {
            //Загружаем данные
            await dbContext.Clients.LoadAsync();
            //Ищем первого подходящего клиента и возвращаем его
            var client = dbContext.Clients.First(x => x.ID.Equals(clientID));
            return client;
        }
        /// <summary>
        /// Удаляет клиента найденного по идентификатору
        /// </summary>
        /// <param name="clientID">Идентификатор</param>
        public async Task RemoveClient(int clientID)
        {
            //Загружаем данные
            dbContext.Clients.Load();
            //проверяем, есть ли клиент в бд
            if (!dbContext.Clients.Any(x => x.ID.Equals(clientID))) throw new Exception("Клиент не найден");
            //Если есть, то возвращаем его
            var client = dbContext.Clients.First(x => x.ID.Equals(clientID));
            //Удаляем
            dbContext.Clients.Remove(client);
            //Сохраняем изменения
            await Update();
        }
        /// <summary>
        /// Сохраняет изменения в базу
        /// </summary>
        public async Task Update()
        {
            //Ищем изменения
            dbContext.ChangeTracker.DetectChanges();
            //Сохраняем в базе данных асинхронно
            await dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
