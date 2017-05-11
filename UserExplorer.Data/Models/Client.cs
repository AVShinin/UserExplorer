using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserExplorer.Data.Models 
{
    public class Client : INotifyPropertyChanged
    {
        #region Приватные переменные
        private int __id;
        private string __family;
        private string __name;
        private DateTime __birthdate;
        #endregion

        //Событие обновления переменных
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int ID { get { return __id; } set { if (__id != value) { __id = value; RaizeProperty(nameof(ID)); } } } //Если значение не равно хранимой переменной, обновляем переменную и вызываем событие обновления переменной
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        [Required]
        public string Family { get { return __family; } set { if (__family != value) { __family = value; RaizeProperty(nameof(Family)); } } } //Если значение не равно хранимой переменной, обновляем переменную и вызываем событие обновления переменной
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get { return __name; } set { if (__name != value) { __name = value; RaizeProperty(nameof(Name)); } } } //Если значение не равно хранимой переменной, обновляем переменную и вызываем событие обновления переменной
        /// <summary>
        /// Дата рождения клиента
        /// </summary>
        [Required]
        public DateTime BirthDate { get { return __birthdate; } set { if (__birthdate != value) { __birthdate = value; RaizeProperty(nameof(BirthDate)); } } } //Если значение не равно хранимой переменной, обновляем переменную и вызываем событие обновления переменной
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="family">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="birthDate">Дата рождения</param>
        public Client(string family, string name, DateTime birthDate)
        {
            this.Family = family;
            this.Name = name;
            this.BirthDate = birthDate;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public Client() : this(string.Empty, string.Empty, new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day)) { }
        /// <summary>
        /// Вызывает событие обновления переменной
        /// </summary>
        /// <param name="propertyName">Имя переменной</param>
        private void RaizeProperty(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
