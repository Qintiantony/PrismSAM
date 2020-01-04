using Prism.Mvvm;

namespace PrismSAM.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism SAM Utility";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
