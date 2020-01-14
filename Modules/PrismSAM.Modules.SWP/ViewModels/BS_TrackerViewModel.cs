using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismSAM.Core;
using PrismSAM.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismSAM.Modules.SWP.ViewModels
{
    public class BS_TrackerViewModel : BindableBase
    {
        #region Properties
        private BS_Tracker _dataModel;
        public BS_Tracker dataModel
        {
            get { return _dataModel; }
            set { SetProperty(ref _dataModel, value); }
        }
        #endregion
        public BS_TrackerViewModel(IEventAggregator ea)
        {
            dataModel = new BS_Tracker();
            ea.GetEvent<BS_CatchedEvent>().Subscribe(BS_CatchedReceived);
        }

        private void BS_CatchedReceived(BS_TrackPoints obj)
        {
            dataModel.Add(obj);
        }
    }
}
