using Infragistics.Controls.Charts;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismSAM.Core;
using PrismSAM.Core.Events;
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

        private bool _BS_isEnabled;
        public bool BS_isEnabled
        {
            get { return _BS_isEnabled; }
            //set { SetProperty(ref CTL_Connection.BS_Catch_enabled, value); }
            set { SetProperty(ref _BS_isEnabled, value); }
        }

        public bool BS_btn_isEnabled
        {
            get { return CTL_Connection.socketConnectionStatus; }
            set { SetProperty(ref CTL_Connection.socketConnectionStatus, value); }
        }
        #endregion

        public SpetrumViewModel(IEventAggregator ea)
        {
            ea.GetEvent<CTL_Events>().Subscribe(BS_isEnabled_received);
            // Dummy data model for test only
            //dataModel = new DummyDataModel();
            dataModel = new GetSweepDataModel();
            ApplyCommand = new DelegateCommand(ApplyConfiguration);
            Enable_BS_Command = new DelegateCommand(Enable_BS);
            freqStart = SweepMode.swpDefaultConfig.StartFreq_Hz/1e6;
            freqStop = SweepMode.swpDefaultConfig.StopFreq_Hz/1e6;
            freqStartTextbox = freqStart;
            freqStopTextbox = freqStop;
            RBWTextbox = SweepMode.swpDefaultConfig.RBW_Hz/1e3;
        }

        private void BS_isEnabled_received(bool parameter)
        {
            BS_isEnabled = parameter;
        }




        #region Commands
        public DelegateCommand ApplyCommand { get; private set; }
        public DelegateCommand Enable_BS_Command { get; private set; }
        #endregion

        #region Command Methods
        private void ApplyConfiguration()
        {
            SweepMode.swpConfig.StartFreq_Hz = freqStartTextbox*1e6;
            SweepMode.swpConfig.StopFreq_Hz = freqStopTextbox*1e6;
            SweepMode.swpConfig.RBW_Hz = RBWTextbox * 1e3;
            SweepMode.swpConfig.TracePoints = 2000;
            SweepMode.Configure_SWP_Standard();
            dataModel.GenerateData(SweepMode.swpConfig.TracePoints);
        }

        private void Enable_BS()
        {
            CTL_Connection.BS_Catch_enabled = true;
            BS_isEnabled = true;
        }
        #endregion
        #region Event Handlers
        
        #endregion



    }
}
