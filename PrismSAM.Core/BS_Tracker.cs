using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismSAM.Core
{
    public class BS_Tracker : ObservableCollection<BS_TrackPoints>
    {
        
    }

    public class BS_TrackPoints
    {
        public double freq { get; set; }
        public double powr { get; set; }
        public double lamd { get; set; }
    }
}
