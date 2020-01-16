using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PrismSAM.Core.Declaration;

namespace PrismSAM.Core
{
    public class AUXSMode
    {
        [DllImport("saAPI.dll", EntryPoint = "AUXS_Configuration", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AUXS_Configuration(ref IntPtr pSA, ref AUXS_TypeDef AUXS_Profile);

        public static AUXS_TypeDef AUXS_Config = new AUXS_TypeDef()
        {
            FixedFreq_Hz = 300e6,
            StartFreq_Hz = 200e6,
            StopFreq_Hz = 400e6,
            StepFreq_Hz = 10e6,
            OutLevel_dBm = 0,
            DwellTime_s = 50,
            RFPath = RFTxPathTypedef.TxPath_Ext,
            TxOperationMode = TxOprtMode_Typedef.OPRT_SINGPTS
        };
       
        public static int AUXS_Apply()
        {
            int op_status;
            if (DeviceConnection.deviceStatus == 1)
            {
                op_status = AUXS_Configuration(ref DeviceConnection.pSA, ref AUXS_Config); ;
            }
            else
            {
                op_status = 99;
            }
            return op_status;
        }

        public static int AUXS_Off()
        {
            int op_status;
            if (DeviceConnection.deviceStatus == 1)
            {
                AUXS_Config.TxOperationMode = TxOprtMode_Typedef.OPRT_PWROFF;
                op_status = AUXS_Configuration(ref DeviceConnection.pSA, ref AUXS_Config); ;
            }
            else
            {
                op_status = 99;
            }
            return op_status;
        }
    }
}
