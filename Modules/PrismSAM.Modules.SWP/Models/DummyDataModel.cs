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
    public class DummyDataModel : ObservableCollection<DummyDataPoint>
    {
        #region Properties
        public static Random Rand = new Random();
        public DispatcherTimer updateTimer;
        public bool isSWP_Configured;
        private TimeSpan _updateInterval;

        public TimeSpan updateInterval
        {
            get { return _updateInterval; }
            set { _updateInterval = value; }
        }

        #endregion
        #region Constructor
        public DummyDataModel()
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
            this.FetchData();
        }

        public void GenerateData(int tracepoints)
        {
            double value = 50;
            double interval = (SweepMode.swpConfig.StopFreq_Hz-SweepMode.swpConfig.StartFreq_Hz)/tracepoints;
            for (int i = 0; i < tracepoints; i++)
            {
                double change = Rand.NextDouble();
                if (change > 0.5)
                {
                    value += (int)(change * 20);
                }
                else
                {
                    value -= (int)(change * 20);
                }
                value = value % 100 - 50;
                this.Add(new DummyDataPoint
                {
                    X = ((double)SweepMode.swpConfig.StartFreq_Hz+i*interval)/1e6,
                    Y = (double) value
                });
            }
        }
        public void FetchData()
        {
            this.Clear();
            GenerateData(SweepMode.swpParamInfo.TracePoints);
        }
 
        #endregion
    }

    public class DummyDataPoint
    {
        #region Properties
        public double X { get; set; }
        public double Y { get; set; }

        #endregion
    }
}
