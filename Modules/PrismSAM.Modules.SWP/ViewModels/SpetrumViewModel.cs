using Infragistics.Controls.Charts;
using Prism.Commands;
using Prism.Mvvm;
using PrismSAM.Core;
using PrismSAM.Modules.SWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using static PrismSAM.Core.Declaration;

namespace PrismSAM.Modules.SWP.ViewModels
{
    public class SpetrumViewModel : BindableBase
    {
        #region Properties
        //private DummyDataModel _dummyData;
        //public DummyDataModel dummyData { 
        //    get { return _dummyData; } 
        //    set { SetProperty(ref _dummyData, value); } 
        //}
        private GetSweepDataModel _dataModel;
        public GetSweepDataModel dataModel
        {
            get { return _dataModel; }
            set { SetProperty(ref _dataModel, value); }
        }
        public string chartTitle { get; set; } = "Power Spetrum Density";

        private string _operationStatus;
        public string operationStatus
        {
            get { return _operationStatus; }
            set { SetProperty(ref _operationStatus, value); }
        }

        private double _freqStart;
        public double freqStart
        {
            get { return _freqStart; }
            set { SetProperty(ref _freqStart, value); }
        }

        private double _freqStop;
        public double freqStop
        {
            get { return _freqStop; }
            set { SetProperty(ref _freqStop, value); }
        }

        private double _freqStartTextbox;
        public double freqStartTextbox
        {
            get { return _freqStartTextbox; }
            set { SetProperty(ref _freqStartTextbox, value); }
        }

        private double _freqStopTextbox;
        public double freqStopTextbox
        {
            get { return _freqStopTextbox; }
            set { SetProperty(ref _freqStopTextbox, value); }
        }

        private double _referenceTop = 0;
        public double referenceTop
        {
            get { return _referenceTop; }
            set { SetProperty(ref _referenceTop, value); }
        }

        private double _referenceBottom=-100;
        public double referenceBottom
        {
            get { return _referenceBottom; }
            set { SetProperty(ref _referenceBottom, value); }
        }

        private double _RBWTextbox;
        public double RBWTextbox
        {
            get { return _RBWTextbox; }
            set { SetProperty(ref _RBWTextbox, value); }
        }

        public bool isPausedBtn
        {
            get { return GetSweepDataModel.isPaused; }
            set { SetProperty(ref GetSweepDataModel.isPaused, value); }
        }

        private string _charSubtitle;
        public string chartSubtitle
        {
            get { return _charSubtitle; }
            set { SetProperty(ref _charSubtitle, value); }
        }
        #endregion

        public SpetrumViewModel()
        {
            // Dummy data model for test only
            //dataModel = new DummyDataModel();
            dataModel = new GetSweepDataModel();
            ApplyCommand = new DelegateCommand(ApplyConfiguration);
            freqStart = SweepMode.swpDefaultConfig.StartFreq_Hz/1e6;
            freqStop = SweepMode.swpDefaultConfig.StopFreq_Hz/1e6;
            freqStartTextbox = freqStart;
            freqStopTextbox = freqStop;
            RBWTextbox = SweepMode.swpDefaultConfig.RBW_Hz/1e3;
        }

        

        #region Commands
        public DelegateCommand ApplyCommand { get; private set; }
        #endregion

        #region Command Methods
        private void ApplyConfiguration()
        {
            SweepMode.swpConfig.StartFreq_Hz = freqStartTextbox*1e6;
            SweepMode.swpConfig.StopFreq_Hz = freqStopTextbox*1e6;
            SweepMode.swpConfig.RBW_Hz = RBWTextbox * 1e3;
            SweepMode.Configure_SWP_Standard();
        }
        #endregion
        #region Event Handlers
        
        #endregion



    }
}
