using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


public class Command : ICommand
{
    //Выполняемый метод
    public Action<object> RunMethod { get; set; }

    public event EventHandler CanExecuteChanged;
    /// <summary>
    /// Возвращет истину, если пользовательский метод установлен
    /// </summary>
    /// <param name="parameter">параметр</param>
    /// <returns>возвращет true если метод установлен</returns>
    public bool CanExecute(object parameter)
    {
        if (RunMethod != null) return true;
        else return false;
    }

    /// <summary>
    /// Функция выполняет метод с параметрами
    /// </summary>
    /// <param name="parameter">Передаваемые параметры</param>
    public void Execute(object parameter)
    {
        RunMethod(parameter);
    }
    /// <summary>
    /// Регистрирует обработчик и создает команду
    /// </summary>
    /// <param name="runMethod">Обработчик</param>
    /// <returns>Команда</returns>
    public Command Register(Action<object> runMethod)
    {
        this.RunMethod = runMethod;
        CanExecuteChanged?.Invoke(this, new EventArgs());
        return this;
    }
    /// <summary>
    /// Регистрирует обработчик и создает команду
    /// </summary>
    /// <param name="runMethod">Обработчик</param>
    /// <returns>Команда</returns>
    public static Command RegisterCommand(Action<object> runMethod)
    {
        var command = new Command();
        command.RunMethod = runMethod;
        command.CanExecuteChanged?.Invoke(command, new EventArgs());
        return command;
    }
}