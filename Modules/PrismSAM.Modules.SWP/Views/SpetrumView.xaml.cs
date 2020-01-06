﻿using Infragistics.Controls.Charts;
using Infragistics.Windows.DockManager;
using System.Windows.Controls;

namespace PrismSAM.Modules.SWP.Views
{
    /// <summary>
    /// Interaction logic for SpetrumView
    /// </summary>
    public partial class SpetrumView : UserControl
    {
        public SpetrumView()
        {
            InitializeComponent();
        }

        private void CreateChart()
        {
            var Yaxis = new NumericYAxis();
        }

        private void xAxis_RangeChanged(object sender, AxisRangeChangedEventArgs e)
        {
            
        }

        private void Chart1_WindowRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            this.FreqStartTexbox.Value = this.xAxis.ActualVisibleMinimumValue;
            this.FreqStopTexbox.Value = this.xAxis.ActualVisibleMaximumValue;
        }

        private void ApplyYBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.yAxis.MaximumValue = (double)this.ReferenceTopTextbox.Value;
            this.yAxis.MinimumValue = (double)this.ReferenceBottomTextbox.Value;
        }
    }
}
