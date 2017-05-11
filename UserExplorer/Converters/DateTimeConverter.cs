using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UserExplorer.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        private string[] Months = new[] { "Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря" };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Проверяем, дата ли передаваемое значение
            if (!(value is DateTime)) return value;
            //Если да, то
            var dt = (DateTime)value;
            //Преобразуем в строку
            return dt.ToString($"dd-{Months[dt.Month-1]}-yyyy HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Проверяем, строка ли передаваемое значение
            if (!(value is string)) return value;
            //Если да, то
            var str = value as string;
            //Перебераем массив названий месяцев и заменяем на числовое значение
            if (Months.ToList().Any(x => x.Equals(str, StringComparison.CurrentCultureIgnoreCase)))
                Months.ToList().ForEach(x =>
                {
                    //Ищем заглавные буквы и заменяем
                    str = str.Replace(x.ToUpper(), (Months.ToList().IndexOf(x) + 1).ToString());
                    //Ищем строчные буквы и заменяем
                    str = str.Replace(x.ToLower(), (Months.ToList().IndexOf(x) + 1).ToString());
                    //Ищем точное соответствие и заменяем
                    str = str.Replace(x, (Months.ToList().IndexOf(x) + 1).ToString());
                });
            //Преобразуем строку в дату
            DateTime out_dt;
            if (DateTime.TryParse(str, out out_dt)) return out_dt;
            //Если не получилось, возвращаем нулевое значение
            return new DateTime?();
        }
    }
}
