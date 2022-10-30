using System.Windows;
using TestWPF.Commands.Base;


namespace TestWPF.Commands
{
    class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object parameter) => true;
        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
