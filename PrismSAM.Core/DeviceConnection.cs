using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PrismSAM.Core.Declaration;

namespace PrismSAM.Core
{
    public class DeviceConnection
    {
        #region Properties
        public static int deviceStatus { get; set; }
        public static int deviceNum { get; set; }

        public static IntPtr pSA = IntPtr.Zero;
        public static StringBuilder HW_Info { get; set; }
        public static StringBuilder SW_Info { get; set; }
        public static StringBuilder funcList { get; set; }
        public static StringBuilder devUID { get; set; }

        public double deviceTemp = 0;

        [DllImport("saAPI.dll", EntryPoint = "SA_OpenDevice", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SA_OpenDevice(ref IntPtr pSA, int DeviceNum);

        [DllImport("saAPI.dll", EntryPoint = "SA_CloseDevice", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SA_CloseDevice(ref IntPtr pSA);

        [DllImport("saAPI.dll", EntryPoint = "SA_QueryTemp", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SA_QueryTemp(ref IntPtr pSA, ref double SA_Temp);

        [DllImport("saAPI.dll", EntryPoint = "SA_QueryDevInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SA_QueryDevInfo(ref IntPtr pSA, [Out] StringBuilder HWInfo, 
            [Out] StringBuilder SWInfo, [Out] StringBuilder FuncList, [Out] StringBuilder DevUID);
        #endregion

        #region Default Constructor
        static DeviceConnection()
        {
            deviceNum = 0;
            deviceStatus = 0;
            HW_Info = new StringBuilder(16);
            SW_Info = new StringBuilder(16);
            funcList = new StringBuilder(16);
            devUID = new StringBuilder(16);
        }
        #endregion

        #region Methods
        public string ConnectDevice()
        {
            if (deviceStatus == 0)
            {
                var opStatus = SA_OpenDevice(ref pSA, deviceNum);
                if (opStatus == 0)
                {
                    deviceStatus = 1;
                    opStatus = SA_QueryDevInfo(ref pSA, HW_Info, SW_Info, funcList, devUID);
                    if (opStatus != 0) return "Fail to load device infomation";
                    opStatus = SA_QueryTemp(ref pSA, ref deviceTemp);
                    SweepMode.Initialize_SWP_Standard(); // Initialize SWP mode for visualization
                    return "Device openned successfully";
                }
                else return "Connection failed with error...";
            }
            else
            {
                return "Device is already openned!";
            }
        }
        public string CloseDevice()
        {
            if (deviceStatus == 1)
            {
                int opStatus = SA_CloseDevice(ref pSA);
                if (opStatus == 0)
                {
                    deviceStatus = 0;
                    pSA = IntPtr.Zero;
                    return "Device closed successfully!";
                }
                else return "Close operation failed with error...";
            }
            else return "Device has already been closed!";
        }
        #endregion




    }
}
