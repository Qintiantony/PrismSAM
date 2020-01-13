using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismSAM.Core;
using PrismSAM.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using static PrismSAM.Core.Declaration;

namespace PrismSAM.Modules.SysInfo.ViewModels
{
    public class InformationViewModel : BindableBase
    {
        #region Properties
        private string _swpinfo_TracePoints;
        public string swpinfo_TracePoints { get{ return _swpinfo_TracePoints; } set { SetProperty(ref _swpinfo_TracePoints, value); } }

        private string _swpinfo_DetPoints;
        public string swpinfo_DetPoints { get { return _swpinfo_DetPoints; } set { SetProperty(ref _swpinfo_DetPoints, value); } }

        private string _swpinfo_DecimateRate;
        public string swpinfo_DecimateRate { get { return _swpinfo_DecimateRate; } set { SetProperty(ref _swpinfo_DecimateRate, value); } }

        private string _swpinfo_DspPlatform;
        public string swpinfo_DspPlatform { get { return _swpinfo_DspPlatform; } set { SetProperty(ref _swpinfo_DspPlatform, value); } }

        private bool _BS_isEnabled;
        public bool BS_isEnabled
        {
            get { return _BS_isEnabled; }
            set { SetProperty(ref _BS_isEnabled, value); }
        }

        private DispatcherTimer infoUpdateTimer;
        private TimeSpan updateTimeSpan;

        private IEventAggregator _ea;
        #endregion

        #region Constructor
        public InformationViewModel(IEventAggregator ea)
        {
            _ea = ea;
            BS_isEnabled = false;
            SweepMode.Fake_SWP_configuration();
            updateTimeSpan = TimeSpan.FromMilliseconds(500);
            infoUpdateTimer = new DispatcherTimer { Interval = updateTimeSpan };
            GetSWPInfo();
            infoUpdateTimer.Tick += UpdateInfo;
            infoUpdateTimer.Start();
        }

        private void UpdateInfo(object sender, EventArgs e)
        {
            GetSWPInfo();
            GetCTLInfo();
        }
        #endregion

        #region Methods
        public void GetSWPInfo()
        {
            SweepMode.Get_SWP_Info();
            swpinfo_DecimateRate = SweepMode.swpParamInfo.DecimateRate.ToString();
            swpinfo_DetPoints = SweepMode.swpParamInfo.DetPoints.ToString();
            swpinfo_TracePoints = SweepMode.swpParamInfo.TracePoints.ToString();
            swpinfo_DspPlatform = SweepMode.swpParamInfo.DSPPlatform == DSPPlatform_Typedef.FPGA ? "FPGA" : "CPU";
        }
        public void GetCTLInfo()
        {
            if (BS_isEnabled != CTL_Connection.BS_Catch_enabled)
            {
                BS_isEnabled = CTL_Connection.BS_Catch_enabled;
                if (!BS_isEnabled)
                {
                    _ea.GetEvent<CTL_Events>().Publish(BS_isEnabled);
                }
            }
        }
        #endregion
    }
}
