using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserExplorer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Обработчик кнопки Изменить/Удалить
        private void on_editItem(object sender, RoutedEventArgs e)
        {
            //Преобразуем отправителя в кнопку
            var btn = sender as Button;
            //Получаем передаваемый идентификатор
            var id = (int)btn.CommandParameter;
            //Задаем фрейм
            FrameHelper.SetFrame(typeof(Controls.EditClientUC));
            //Передаем в модель фрейма идентификатор
            ViewDataModel.EditClientUCModel.SetID?.Invoke(id);
        }
    }
}
