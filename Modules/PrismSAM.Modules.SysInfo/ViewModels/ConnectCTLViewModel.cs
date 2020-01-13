using Prism.Commands;
using Prism.Mvvm;
using PrismSAM.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PrismSAM.Modules.SysInfo.ViewModels
{
    public class ConnectCTLViewModel : BindableBase
    {
        #region Properties
        private string _feedback_message;
        public string feedback_message
        {
            get { return _feedback_message; }
            set { SetProperty(ref _feedback_message, value); }
        }

        private bool _connectionStatus;
        public bool connectionStatus
        {
            get { return _connectionStatus; }
            set { SetProperty(ref _connectionStatus, value); }
        }

        public string server_ip
        {
            get { return CTL_Connection.server_addr; }
            set { SetProperty(ref CTL_Connection.server_addr, value); }
        }

        public string server_port
        {
            get { return CTL_Connection.server_port.ToString(); }
            set 
            {
                if (value != "")
                {
                    SetProperty(ref CTL_Connection.server_port, Convert.ToInt32(value));
                }
            }
        }

        public string CTL_ip
        {
            get { return CTL_Connection.CTL_addr; }
            set { SetProperty(ref CTL_Connection.CTL_addr, value); }
        }

        private string _errmsg;
        public string errmsg
        {
            get { return _errmsg; }
            set { SetProperty(ref _errmsg, value); }
        }

        public DelegateCommand ConnectCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand SendCommand { get; private set; }
        #endregion

        #region Constructor
        public ConnectCTLViewModel()
        {
            ConnectCommand = new DelegateCommand(Connect);
            CloseCommand = new DelegateCommand(Close);
            SendCommand = new DelegateCommand(Send);
            connectionStatus = CTL_Connection.socketConnectionStatus;
        }
        #endregion

        #region Commands
        private void Connect()
        {
            CTL_Connection.LaunchServer(CTL_ip);
            CTL_Connection.Connect();
            if (CTL_Connection.socketConnectionStatus)
            {
                feedback_message = CTL_Connection.SendCommand(CTL_Commands.ctl_state);
            }
            errmsg = CTL_Connection.errmsg;
            connectionStatus = CTL_Connection.socketConnectionStatus;
        }

        private void Close()
        {
            feedback_message = CTL_Connection.SendCommand(CTL_Commands.close_server);
            CTL_Connection.Disconnect();
            connectionStatus = CTL_Connection.socketConnectionStatus;
        }

        private void Send()
        {
            feedback_message = CTL_Connection.SendCommand("AMD YES!");
        }
        #endregion
    }
}
