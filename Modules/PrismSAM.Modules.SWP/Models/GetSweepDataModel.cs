using Prism.Events;
using PrismSAM.Core;
using PrismSAM.Core.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PrismSAM.Modules.SWP.Models
{
    public class GetSweepDataModel : ObservableCollection<SweepDataPoint>
    {
        #region Properties
        public static Random Rand = new Random();
        public DispatcherTimer updateTimer;
        public bool isSWP_Configured;
        private TimeSpan _updateInterval;
        public static bool isPaused = false;
        private double amp_buffer;
        private double freq_buffer;
        private IEventAggregator _ea;

        public TimeSpan updateInterval
        {
            get { return _updateInterval; }
            set { _updateInterval = value; }
        }
        #endregion
        #region Constructor
        public GetSweepDataModel(IEventAggregator ea)
        {
            _ea = ea;
            // Initialize update timer
            updateInterval = TimeSpan.FromMilliseconds(0.1);
            this.updateTimer = new DispatcherTimer { Interval = this.updateInterval };
            GenerateData(1000);
            this.updateTimer.Tick += UpdateData;
            this.updateTimer.Start();
        }
        #endregion

        #region Methods
        private void UpdateData(object sender, EventArgs e)
        {
            if ( (DeviceConnection.deviceStatus == 1))
            {
                this.FetchData();
                
            }

        }

        public void GenerateData(int tracepoints)
        {
            this.Clear();
            for (int i = 0; i < tracepoints; i++)
            {
                this.Add(new SweepDataPoint
                {
                    X = 7000,
                    Y = 0
                });
            }
        }

        public void FetchData()
        {
            this.Clear();
            //GenerateData(SweepMode.swpParamInfo.TracePoints);
            amp_buffer = -999; 
            int det_points = SweepMode.swpParamInfo.DetPoints;
            do
            {
                SweepMode.Get_SWP_Data();
                //SweepMode.Get_SWP_Data();
                var amp_maximum = SweepMode.amps.Max();
                if (amp_maximum > amp_buffer)
                {
                    amp_buffer = amp_maximum;
                    freq_buffer = SweepMode.freqs[Array.IndexOf(SweepMode.amps, amp_maximum)];
                }
                //int det_points = SweepMode.swpParamInfo.DetPoints;
                for (int i = 0; i < det_points; i++)
                {
                    this.Add(new SweepDataPoint
                    {
                        X = SweepMode.freqs[i] / 1e6, //Convert freq in Hz to MHz
                        Y = SweepMode.amps[i]
                    });
                    //this.RemoveItem((int)pack_index * det_points + i);
                    //this.InsertItem((int)pack_index * det_points + i, new SweepDataPoint
                    //{
                    //    X = SweepMode.freqs[i] / 1e6, //Convert freq in Hz to MHz
                    //    Y = SweepMode.amps[i]
                    //});
                }
            }
            while (SweepMode.packIndex < (SweepMode.packIndexMax-1));
            //amp_buffer = this.Max(m => m.Y);
            this.CatchBS();
        }

        //TODO: Restore CatechBS after dubugging event aggregator
        public void CatchBS()
        {
            if (amp_buffer < CTL_Connection.BS_Threshold)
            {
                CTL_Connection.BS_isCatched = false;
            }
            else
            {
                if (CTL_Connection.BS_Catch_enabled && CTL_Connection.socketConnectionStatus && !CTL_Connection.BS_isCatched)
                {
                    //CTL_Connection.SendCommand(CTL_Commands.ctl_scan_pause);
                    CTL_Connection.BS_isCatched = true;
                    //CTL_Connection.BS_Catch_enabled = false;
                    var lambdaString = CTL_Connection.SendCommand(CTL_Commands.ctl_lambda);
                    //_ea.GetEvent<CTL_Events>().Publish(false);
                    _ea.GetEvent<BS_CatchedEvent>().Publish(new BS_TrackPoints
                    {
                        freq = freq_buffer/1e6,
                        powr = amp_buffer,
                        lamd = Convert.ToDouble(lambdaString)
                    });
                }
            }
        }

        //public void CatchBS()
        //{
        //    if (CTL_Connection.BS_Catch_enabled && (Rand.Next()%100==5))
        //    {
        //        CTL_Connection.BS_Catch_enabled = false;
        //    }
        //}
        #endregion
    }

    public class SweepDataPoint
    {
        #region Properties
        public double X { get; set; }
        public double Y { get; set; }
        #endregion
    }
}
