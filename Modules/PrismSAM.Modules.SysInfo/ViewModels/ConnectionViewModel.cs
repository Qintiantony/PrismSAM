using Prism.Commands;
using Prism.Mvvm;
using PrismSAM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PrismSAM.Modules.SysInfo.ViewModels
{
    public class ConnectionViewModel : BindableBase
    {
        #region Properties
        private string _connectionStatus = "Device Not Connected";

        public DelegateCommand ConnectCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DeviceConnection Device { get; set; }

        public string connectionStatus
        {
            get { return _connectionStatus; }
            set { SetProperty(ref _connectionStatus, value); }
        }

        private string _hw_info;

        public string hw_info
        {
            get { return _hw_info; }
            set { SetProperty(ref _hw_info, value); }
        }

        private string _sw_info;

        public string sw_info
        {
            get { return _sw_info; }
            set { SetProperty(ref _sw_info, value); }
        }

        private string _func_list;

        public string func_list
        {
            get { return _func_list; }
            set { SetProperty(ref _func_list, value); }
        }

        private string _dev_UID;

        public string dev_UID
        {
            get { return _dev_UID; }
            set { SetProperty(ref _dev_UID, value); }
        }



        #endregion

        #region Constructor
        public ConnectionViewModel()
        {
            ConnectCommand = new DelegateCommand(Connect);
            CloseCommand = new DelegateCommand(Close);
            Device = new DeviceConnection();
        }
        #endregion

        #region Command Methods
        public void Connect()
        {

            //connectionStatus = "Device has been connected..." ;

            connectionStatus = Device.ConnectDevice();
            //hw_info = Device.deviceTemp.ToString();
            hw_info = DeviceConnection.HW_Info.ToString();
            sw_info = DeviceConnection.SW_Info.ToString();
            func_list = DeviceConnection.funcList.ToString();
            dev_UID = DeviceConnection.devUID.ToString();

            return;
        }

        public void Close()
        {
            connectionStatus = Device.CloseDevice();
            return;
        }

        #endregion

    }
}
