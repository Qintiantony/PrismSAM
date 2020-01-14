using Infragistics.Controls.Charts;
using Infragistics.Windows.DockManager;
using Microsoft.Win32;
using PrismSAM.Core;
using PrismSAM.Modules.SWP.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            this.Loaded += SpectrumLoaded;
        }

        private void SpectrumLoaded(object sender, RoutedEventArgs e)
        {
            SweepMode.Configure_SWP_Standard();
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
            double centerValue = (this.xAxis.ActualVisibleMaximumValue + this.xAxis.ActualVisibleMinimumValue)/2;
            double spanValue = this.xAxis.ActualVisibleMaximumValue - this.xAxis.ActualVisibleMinimumValue;
            this.Chart1.Subtitle = "Center freq: "+ centerValue.ToString("F2") + " MHz; Span: " + spanValue.ToString("F2") + " MHz;";
        }

        private void ApplyYBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.yAxis.MaximumValue = (double)this.ReferenceTopTextbox.Value;
            this.yAxis.MinimumValue = (double)this.ReferenceBottomTextbox.Value;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int DPI = 300;
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)(this.DataChartGrid.ActualWidth*DPI/96), 
                (int)(this.DataChartGrid.ActualHeight*DPI/96), DPI, DPI, PixelFormats.Pbgra32);
            bitmap.Render(this.Chart1);
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = ".png";
            saveDialog.Filter = "PNG|*.png";
            saveDialog.InitialDirectory = @"E:\CloudStation\CloudStation\Python Scripts\SAMTEMP";
            saveDialog.RestoreDirectory = true;
            if (saveDialog.ShowDialog() == true)
            {
                using (Stream stream = saveDialog.OpenFile())
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(stream);
                }
            }
        }
    }
}
