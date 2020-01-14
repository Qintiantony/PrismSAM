using Infragistics.Windows.Ribbon;
using PrismSAM.Core;
using PrismSAM.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;

namespace PrismSAM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : XamRibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += WindowClosed;
        }

        private void WindowClosed(object sender, CancelEventArgs e)
        {
            if(DeviceConnection.deviceStatus == 1)
            {
                DeviceConnection.SA_CloseDevice(ref DeviceConnection.pSA);
            }
        }

        private void TabGroupPane_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var viewmodel = (MainWindowViewModel)DataContext;
            var activeModuleIndex = this.ModuleTabGroupPane.SelectedIndex;
            switch (activeModuleIndex)
            {
                case 1:
                    viewmodel.ModuleSwitchCommand.Execute();
                    break;
            }
        }
    }
}
