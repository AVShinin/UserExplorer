using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserExplorer
{
    internal class FrameHelper
    {
        //Хранилище фреймов
        private static Dictionary<Type, object> Frames = new Dictionary<Type, object>();
        //Делегат для добавления фрейма
        public delegate void SetFrameDelegate(Type type);
        //Реализация делегата
        public static SetFrameDelegate SetFrame;

        //Функция возвращает существующий фрейм или создает новый
        public static object GetFrame(Type key)
        {
            //Если фрейм найден в кранилище, то возвращаем его
            if (Frames.ContainsKey(key)) return Frames[key];
            //Иначе создаем новый фрейм по переданному типу
            var item = Activator.CreateInstance(key);
            //Добавляем в хранилище
            Frames.Add(key, item);
            //Возвращаем его
            return item;
        }
    }
}
