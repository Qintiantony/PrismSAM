using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PrismSAM.Core.Declaration;

namespace PrismSAM.Core
{
    public static class IQRecorderMode
    {
        #region Properties
        [DllImport("saAPI.dll", EntryPoint = "IQS_Configuration", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IQS_Configuration(ref IntPtr pSA, ref IQS_TypeDef IQS_Profile);

        [DllImport("saAPI.dll", EntryPoint = "IQS_GetIQStream", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IQS_GetIQStream(ref IntPtr pSA, byte[] bulkData, uint bulkSize);

        [DllImport("saAPI.dll", EntryPoint = "IQS_GetPwrOffset", CallingConvention = CallingConvention.Cdecl)]
        public static extern double IQS_GetPwrOffset(ref IntPtr pSA, double CustomOffset);

        [DllImport("saAPI.dll", EntryPoint = "IQS_SoftTRGStart", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IQS_SoftTRGStart(ref IntPtr pSA);

        [DllImport("saAPI.dll", EntryPoint = "IQS_SoftTRGStop", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IQS_SoftTRGStop(ref IntPtr pSA);

        public static IQS_TypeDef IQS_Config = new IQS_TypeDef();
        public static IQS_TypeDef IQS_Default = new IQS_TypeDef()
        {
            CenterFreq_Hz = 300e6,
            RefLevel_dBm = 0,
            DecimateFactor = 256,
            FramePts = 1024,
            Timeout = 100,
            RFPath = RFRxPathTypedef.RxPath_Ext,
            TRGMode = TRGMode_TypeDef.Triggered,
            TRGSrc = BBTRG_TypeDef.BBTRG_SOFT,
        };
        #endregion

        #region Methods
        public static int ConfigureIQRecorder()
        {
            int op_status;
            if (DeviceConnection.deviceStatus == 1)
            {
                op_status = IQS_Configuration(ref DeviceConnection.pSA, ref IQS_Config);
            }
            else
            {
                op_status = 99;
            }
            return op_status;
        }
        #endregion
    }
}
