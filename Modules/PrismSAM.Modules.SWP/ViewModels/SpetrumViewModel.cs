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
        private DummyDataModel _dummyData;
        public DummyDataModel dummyData { 
            get { return _dummyData; } 
            set { SetProperty(ref _dummyData, value); } 
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
        #endregion

        public SpetrumViewModel()
        {
            dummyData = new DummyDataModel();
            ApplyCommand = new DelegateCommand(ApplyConfiguration);
            UpdateFreqStartCommand = new DelegateCommand(UpdateFreqStart);
            freqStart = SweepMode.swpDefaultConfig.StartFreq_Hz/1e6;
            freqStop = SweepMode.swpDefaultConfig.StopFreq_Hz/1e6;
            freqStartTextbox = freqStart;
        }

        

        #region Commands
        public DelegateCommand ApplyCommand { get; private set; }
        public DelegateCommand UpdateFreqStartCommand { get; private set; }
        #endregion

        #region Command Methods
        private void ApplyConfiguration()
        {
            //var newConfig = new SWPParam_Typedef() { 
            //    DecimateRate = 1024, 
            //    DetPoints = 12345,
            //    DSPPlatform = DSPPlatform_Typedef.CPU,
            //    TracePoints = 3987
            //};
            //SweepMode.swpParamInfo = newConfig;
            int opcode = SweepMode.Initialize_SWP_Standard();
            //SweepMode.Get_SWP_Info();
            //dummyData.GenerateData(SweepMode.swpParamInfo.TracePoints);
            freqStart = SweepMode.swpConfig.StartFreq_Hz/1e6;
            freqStop = SweepMode.swpConfig.StopFreq_Hz/1e6;
            //dummyData.updateTimer.Start();
            //dummyData.isSWP_Configured = true;
            operationStatus = opcode.ToString();
        }

        private void UpdateFreqStart()
        {
            freqStart = freqStartTextbox;
        }
        #endregion
        #region Event Handlers
        
        #endregion



    }
}
