using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrismSAM.Modules.SWP.Views
{
    /// <summary>
    /// Interaction logic for BS_TrackerView
    /// </summary>
    public partial class BS_TrackerView : UserControl
    {
        public BS_TrackerView()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int DPI = 300;
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)(this.DataChartGrid.ActualWidth * DPI / 96),
                (int)(this.DataChartGrid.ActualHeight * DPI / 96), DPI, DPI, PixelFormats.Pbgra32);
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
