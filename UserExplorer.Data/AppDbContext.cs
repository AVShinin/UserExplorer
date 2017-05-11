using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserExplorer.Data.Models;

namespace UserExplorer.Data
{
    public class AppDbContext : DbContext
    {
        //Вызываем конструктор базового класса и передаем ему имя проки подключения
        public AppDbContext() : base("name=ClientsContext") { }
        //Коллекция клиентов
        public virtual DbSet<Client> Clients { get; set; }
    }
}
