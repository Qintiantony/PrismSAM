using Infragistics.Controls.Charts;
using Prism.Commands;
using Prism.Mvvm;
using PrismSAM.Core;
using PrismSAM.Modules.SWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private double _freqStart = SweepMode.swpConfig.StartFreq_Hz;
        public double freqStart
        {
            get { return _freqStart; }
            set { SetProperty(ref _freqStart, value); }
        }

        private double _freqStop = SweepMode.swpConfig.StopFreq_Hz;
        public double freqStop
        {
            get { return _freqStop; }
            set { SetProperty(ref _freqStop, value); }
        }
        #endregion

        public SpetrumViewModel()
        {
            dummyData = new DummyDataModel();
            ApplyCommand = new DelegateCommand(ApplyConfiguration);
        }

        

        #region Commands
        public DelegateCommand ApplyCommand { get; private set; }
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
            freqStart = SweepMode.swpConfig.StartFreq_Hz;
            freqStop = SweepMode.swpConfig.StopFreq_Hz;
            //dummyData.updateTimer.Start();
            //dummyData.isSWP_Configured = true;
            operationStatus = opcode.ToString();

        }
        #endregion



    }
}
