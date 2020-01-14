using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismSAM.Core.Events;

namespace PrismSAM.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Propeties
        private string _title = "Prism SAM Utility";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand ModuleSwitchCommand;

        private IEventAggregator _ea;
        #endregion

        public MainWindowViewModel(IEventAggregator ea)
        {
            _ea = ea; //Instantiate event aggregator
            ModuleSwitchCommand = new DelegateCommand(SweepModeInactivate);
        }

        #region Commands
        private void SweepModeInactivate()
        {
            _ea.GetEvent<SweepPauseEvent>().Publish(true);
        }
        #endregion
    }
}
