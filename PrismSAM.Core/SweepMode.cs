﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PrismSAM.Core.Declaration;

namespace PrismSAM.Core
{
    public static class SweepMode
    {
        #region Properties


        [DllImport("saAPI.dll", EntryPoint = "SWP_Configuration", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SWP_Configuration(ref IntPtr pSA, ref SWP_TypeDef SWP_Profile);
        [DllImport("saAPI.dll", EntryPoint = "SWP_QueryParam", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SWP_QueryParam(ref IntPtr pSA, ref SWPParam_Typedef SWP_Profile);
        [DllImport("saAPI.dll", EntryPoint = "SWP_GetPartialSweep", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SWP_GetPartialSweep(ref IntPtr pSA, double[] Frequency, double[] Amplitude, ref int PackageIndex);

        public static SWPParam_Typedef swpParamInfo = new SWPParam_Typedef();
        public static SWP_TypeDef swpConfig = new SWP_TypeDef();
        public static SWP_TypeDef swpDefaultConfig = new SWP_TypeDef() {
            // Initial SWP configuration
            Detector = DetMode_TypeDef.PosPeak,
            PerformanceRate = 0,
            RBW_Hz = 70E3, //Should be set from 10Hz to 50MHz
            RefLevel_dBm = 0, //Should be set from -40 to +26 dBm
            RFPath = RFRxPathTypedef.RxPath_Ext,
            StartFreq_Hz = 50E6,
            StopFreq_Hz = 600E6,
            TracePoints = 2000, // Should be set from 600 to 10000, auto ajusted by SAM
            Window = Window_TypeDef.FlatTop,
            TrigSrc = TrigINSrc_TypeDef.TRIGIN_FREERUN,
            TrigOutMode = TrigOUTMode_TypeDef.TRIGOUT_NULL,
            SpurRejLevel = SpurRej_TypeDef.Enhanced,
            CPUUtilization = CPUUtil_TypeDef.Low
        };
        public static double[] freqs = new double[30];
        public static double[] amps = new double[30];
        public static int packIndex;
        public static int packIndexMax;
        public static bool sweepIsPaused;
        #endregion

        #region Methods
        //TO BE Deleted: Fake method for swp configuration
        public static int Fake_SWP_configuration()
        {
            swpParamInfo.DecimateRate = 10;
            swpParamInfo.DetPoints = 100;
            swpParamInfo.DSPPlatform = DSPPlatform_Typedef.FPGA;
            swpParamInfo.TracePoints = 512;
            return 1;
        }

        public static int Initialize_SWP_Standard()
        {
            // Apply SWP configuration
            int op_status;
            if (DeviceConnection.deviceStatus == 1)
            {
                swpConfig = swpDefaultConfig;
                op_status = SWP_Configuration(ref DeviceConnection.pSA, ref swpConfig);
                Get_SWP_Info();
            }
            else
            {
                op_status = 99;
            }
            return op_status;
        }

        public static int Configure_SWP_Standard()
        {
            int op_status;
            if (DeviceConnection.deviceStatus == 1)
            {
                op_status = SWP_Configuration(ref DeviceConnection.pSA, ref swpConfig);
                Get_SWP_Info();
            }
            else
            {
                op_status = 99;
            }
            return op_status;
        }

        public static int Get_SWP_Info()
        {
            int op_status;
            if (DeviceConnection.deviceStatus == 1)
            {
                op_status = SWP_QueryParam(ref DeviceConnection.pSA, ref swpParamInfo);
                freqs = new double[swpParamInfo.DetPoints];
                amps = new double[swpParamInfo.DetPoints];
                packIndexMax = (int)(swpParamInfo.TracePoints/swpParamInfo.DetPoints);
            }
            else op_status = 99;
            return op_status;
        }

        public static int Get_SWP_Data()
        {
            //freqs = new double[swpParamInfo.DetPoints];
            //amps = new double[swpParamInfo.DetPoints];
            //packIndex = 0;
            int op_status;
            if (DeviceConnection.deviceStatus == 1)
            {
                op_status = SWP_GetPartialSweep(ref DeviceConnection.pSA, freqs, amps, ref packIndex);
            }
            else op_status = 99;
            return op_status;
        }
        #endregion
    }
}
