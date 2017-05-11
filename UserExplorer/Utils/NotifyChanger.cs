using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public class NotifyChanger : INotifyPropertyChanged
{
    //Хранилище переменных
    private Dictionary<string, object> dictionary = new Dictionary<string, object>();
    //Событие обновления параметра
    public event PropertyChangedEventHandler PropertyChanged;
    /// <summary>
    /// Устанавливает значение переменной
    /// </summary>
    /// <param name="value">Значение</param>
    /// <param name="propertyName">Имя переменной</param>
    public void Set(object value, [CallerMemberName]String propertyName = null)
    {
        //Если имя пустое, выходим
        if (string.IsNullOrEmpty(propertyName)) return;
        //Если в хранилище найден параметр, возвращаем
        if (dictionary.ContainsKey(propertyName)) dictionary[propertyName] = value;
        //В пративном случае добавляем в хранилище
        else dictionary.Add(propertyName, value);
        //Вызываем событие обновления переменной
        Upd(propertyName);
    }
    /// <summary>
    /// Возвращает значение переменной
    /// </summary>
    /// <typeparam name="T">Тип переменной</typeparam>
    /// <param name="ctorOn">Если переменная просит вызывать конструктор</param>
    /// <param name="propertyName">Имя переменной</param>
    /// <returns>Значение переменной</returns>
    public T Get<T>(bool ctorOn = false, [CallerMemberName]string propertyName = null)
    {
        //Если имя пустое, выходим
        if (string.IsNullOrEmpty(propertyName)) return default(T);
        //Если в хранилище не найден параметр, то
        if (!dictionary.ContainsKey(propertyName))
        {
            //Создаем переменную по умолчанию
            T obj = default(T);
            //Если необходимо вызвать конструктор
            if (ctorOn)
                //Вызываем
                obj = (T)Activator.CreateInstance(typeof(T));
            //Устанавливаем значение
            Set(obj, propertyName);
        }
        //Возвращаем переменную
        return (T)dictionary[propertyName];
    }
    /// <summary>
    /// Вызывает событие обновления переменной
    /// </summary>
    /// <param name="propertyName"></param>
    public void Upd([CallerMemberName]String propertyName = null)
    {
        //Если имя пустое, выходим
        if (string.IsNullOrEmpty(propertyName)) return;
        //Вызываем событие обновления переменной
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}