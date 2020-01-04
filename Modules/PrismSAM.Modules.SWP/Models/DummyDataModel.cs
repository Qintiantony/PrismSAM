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

        private void UpdateData(object sender, EventArgs e)
        {
            if (DeviceConnection.pSA != IntPtr.Zero)
            {
                this.FetchData();
            }
            
        }

        public void GenerateData(int tracepoints)
        {
            for (int i = 0; i < tracepoints; i++)
            {
                //double change = Rand.NextDouble();
                //if (change > 0.5)
                //{
                //    value += (int)(change * 20);
                //}
                //else
                //{
                //    value -= (int)(change * 20);
                //}
                //value %= 100;
                this.Add(new DummyDataPoint
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
                    this.InsertItem((int)pack_index * det_points + i, new DummyDataPoint
                    {
                        X = SweepMode.freqs[i],
                        Y = SweepMode.amps[i]
                    });
                    //this.Add(new DummyDataPoint
                    //{
                    //    X = SweepMode.freqs[i],
                    //    Y = SweepMode.amps[i]
                    //});

                }
            }
            while (SweepMode.packIndex != 0);
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
