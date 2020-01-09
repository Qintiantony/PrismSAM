using PrismSAM.Core;
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

        public TimeSpan updateInterval
        {
            get { return _updateInterval; }
            set { _updateInterval = value; }
        }
        #endregion
        #region Constructor
        public GetSweepDataModel()
        {
            // Initialize update timer
            updateInterval = TimeSpan.FromMilliseconds(10);
            this.updateTimer = new DispatcherTimer { Interval = this.updateInterval };
            //GenerateData(200);
            this.updateTimer.Tick += UpdateData;
            this.updateTimer.Start();
        }
        #endregion

        #region Methods
        private void UpdateData(object sender, EventArgs e)
        {
            if ( (DeviceConnection.deviceStatus == 1) && (! isPaused) )
            {
                this.FetchData();
            }

        }

        public void GenerateData(int tracepoints)
        {
            for (int i = 0; i < tracepoints; i++)
            {
                this.Add(new SweepDataPoint
                {
                    X = (double)i,
                    Y = 0
                });
            }
        }

        public void FetchData()
        {
            this.Clear();
            GenerateData(SweepMode.swpParamInfo.TracePoints);
            do
            {
                SweepMode.Get_SWP_Data();
                int pack_index = SweepMode.packIndex;
                int det_points = SweepMode.swpParamInfo.DetPoints;
                for (int i = 0; i < SweepMode.swpParamInfo.DetPoints; i++)
                {
                    this.RemoveItem((int)pack_index * det_points + i);
                    this.InsertItem((int)pack_index * det_points + i, new SweepDataPoint
                    {
                        X = SweepMode.freqs[i] / 1e6, //Convert freq in Hz to MHz
                        Y = SweepMode.amps[i]
                    });
                }
            }
            while (SweepMode.packIndex != 0);
        }
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
