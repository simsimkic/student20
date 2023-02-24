using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjekatBolnica.DoctorView.DoctorCommands
{
    public static class CustomCommands
    {
        public static readonly ICommand tabCommand = new displayTabCommand();
    }

    public class displayTabCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Button tabButton = (Button)parameter;
            tabButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }


}
