using CsvHelper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismSAM.Core;
using PrismSAM.Core.Events;
using System;
using System.Collections.Generic;
using System.IO;
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

        #region Constructor
        public BS_TrackerViewModel(IEventAggregator ea)
        {
            dataModel = new BS_Tracker();
            ea.GetEvent<BS_CatchedEvent>().Subscribe(BS_CatchedReceived);
            Save_CSV_Command = new DelegateCommand(Save_CSV);
            ClearTrackCommand = new DelegateCommand(ClearTrack);
        }
        #endregion

        private void BS_CatchedReceived(BS_TrackPoints obj)
        {
            dataModel.Add(obj);
        }

        #region Commands
        public DelegateCommand Save_CSV_Command { get; private set; }
        public DelegateCommand ClearTrackCommand { get; private set; }

        #endregion
        #region Command Methods


        private void Save_CSV()
        {
            string CSV_filename = "Track-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm") + ".csv";
            using (var writer = new StreamWriter(@"E:\CloudStation\CloudStation\Python Scripts\SAMTEMP\CSV\" + CSV_filename))
            {
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(dataModel);
                }
            }

        }

        private void ClearTrack()
        {
            dataModel.Clear();
        }
        #endregion
    }
}
