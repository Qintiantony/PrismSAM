using Infragistics.Windows.DockManager;
using System;
using System.Windows;
using System.Windows.Controls;
using PrismSAM.Core;

namespace PrismSAM.Modules.IQRecorder.Views
{
    /// <summary>
    /// Interaction logic for RecorderView
    /// </summary>
    public partial class RecorderView : UserControl
    {
        public RecorderView()
        {
            InitializeComponent();
            this.Loaded += RecorderLoaded;
        }

        private void RecorderLoaded(object sender, RoutedEventArgs e)
        {
            IQRecorderMode.ConfigureIQRecorder();
        }
    }
}
