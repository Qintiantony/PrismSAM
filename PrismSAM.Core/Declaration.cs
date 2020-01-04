using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PrismSAM.Core
{
    public class Declaration
    {
        #region Declare C enum in C# 
        // Basic configuration
        public enum REFCLKSource_TypeDef
        {
            InternalREFCLK = 0x00,		//Select internal 10MHz as system reference clock; Default internal
            ExternalREFCLK = 0x01		//Select external 10MHz as system reference clock
        }
        public enum TrigINSrc_TypeDef
        {
            TRIGIN_FREERUN = 0x00,	/*Use Auto Trig and Free Run*/
            TRIGIN_EXT_PRE = 0x01,	/*Use External Trig,wait for the Rising Edge at Each Points*/
            TRIGIN_EXT_PFE = 0x02,	/*Use External Trig,wait for the falling Edge at Each Points*/
            TRIGIN_EXT_SRE = 0x03,	/*Use External Trig,wait for the Rising Edge at Each Sweep*/
            TRIGIN_EXT_SFE = 0x04	/*Use External Trig,wait for the falling Edge at Each Sweep*/
        }

        public enum TrigOUTMode_TypeDef
        {
            TRIGOUT_NULL = 0x00,	/*No Trigger Out*/
            TRIGOUT_PRE = 0x01,		/*Trigger out with Sending Rising Edge at Each Points*/
            TRIGOUT_PFE = 0x02,		/*Trigger out with Sending Rising falling Edge at Each Points*/
            TRIGOUT_SRE = 0x03,		/*Trigger out with Sending Rising Rising Edge at Each Sweep*/
            TRIGOUT_SFE = 0x04		/*Trigger out with Sending Rising falling Edge at Each Sweep*/
        }

        public enum RFRxPathTypedef
        {
            RxPath_Ext = 0x00,		/*External RF Rx Path*/
            RxPath_Int = 0x01		/*Internal RF Rx Path*/
        }

        public enum DetMode_TypeDef
        {
            NegPeak = 0x00,		/*Negtive Peak Detection Mode*/
            Sample = 0x01,		/*Sample Detection Mode*/
            Rosenfell = 0x02,	/*Rosenfell Detection Mode*/
            PosPeak = 0x03,		/*Postive Peak Detection Mode*/
            RMS = 0x04			/*RMS Detection Mode*/
        }

        public enum Window_TypeDef
        {
            FlatTop = 0x00,		/*Flaptop Window for FFT Process*/
            Nuttall = 0x01		/*Nuttal Window for FFT Process*/
        }

        public enum CPUUtil_TypeDef
        {
            Low = 0x00,		/*CPU Utilization Level -Low*/
            Medium = 0x01,	/*CPU Utilization Level -Medium*/
            High = 0x02		/*CPU Utilization Level -High*/
        }

        public enum SpurRej_TypeDef
        {
            Bypass = 0x00,		/*Bypass:No Spurrejection*/
            Standard = 0x01,	/*Standard:Medium Spurrejection*/
            Enhanced = 0x02		/*Enhanced:Highest Spurrejection*/
        }

        public enum DCCancel_Typedef
        {
            DCCancel_Off = 0x00,	/*Turn off the DCCancellation Function*/
            DCCancel_On = 0x01		/*Turn on the DCCancellation Function*/
        }

        public enum DSPPlatform_Typedef
        {
            CPU = 0x00,	/*DSP on FPGA*/
            FPGA = 0x01		/*DSP on CPU*/
        }

        public enum TxOprtMode_Typedef
        {
            OPRT_PWROFF = 0x00,		/*Tx Power Off*/
            OPRT_SINGPTS = 0x01,	/*Tx Operate in Single Frequency Points*/
            OPRT_CONTSW = 0x02,		/*Tx Continous Sweep*/
            OPRT_SINGSW = 0x03		/*Tx Single Sweep*/
        }

        public enum RFTxPathTypedef
        {
            TxPath_Ext = 0x00,	/*External RF Tx Path*/
            TxPath_Int = 0x01	/*Internal RF Tx Path*/
        }

        // IQS related configuration
        public enum BBTRG_TypeDef
        {
            BBTRG_EXT = 0x00,/*Ext trigger*/
            BBTRG_TIMER = 0x01,
            BBTRG_AFE = 0x02,
            BBTRG_LEVEL = 0x03,
            BBTRG_INSTR = 0x04,
            BBTRG_SOFT = 0x05 /*Soft trigger*/
        }
        public enum TRGMode_TypeDef
        {
            Fixed = 0x00,/*Collect a piece of data in IQS mode*/
            Triggered = 0x01/*Collect data from start trigger to stop trigger in IQS mode*/
        }
        #endregion

        #region Declare C struct in C#
        [StructLayout(LayoutKind.Sequential)]
        public struct ErrTypedef
        {
            public int ErrorStatus;	/*Error Status for API Called */
            public string ErrorDetail;	/*Error Detailed Info for API Called*/
            public int ErrorID;		/*Error ID to Indicate the API ID*/
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SWPParam_Typedef
        {
            //System.Double StartFreq_Hz;		/*SA Start Frequency in SWP Mode*/
            //System.Double StopFreq_Hz;			/*SA Stop Frequency in SWP Mode*/
            //System.Double StepFreq_Hz;			/*SA Step Frequency in SWP Mode*/
            //unsigned int SweepPoints;  /*SA Frequency SweepPoints in SWP Mode*/
            public int TracePoints;  /*Trace Points in SWP Mode*/
            public int DetPoints;	/*Detection Points in SWP Mode*/
            public int DecimateRate; /*Decimate Rate in SWP Mode*/
            public DSPPlatform_Typedef DSPPlatform; /*DSP Platform*/
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SWP_TypeDef
        {
            public double StartFreq_Hz;	/*Start Frquency in the SWP Mode*/
            public double StopFreq_Hz;		/*Stop Frquency in the SWP Mode*/
            public double RefLevel_dBm;	/*RefLevel in the SWP Mode*/
            public double RBW_Hz;			/*Resolution Bandwidth in the SWP Mode*/
            public int TracePoints;/*Trace Points in the SWP Mode*/
            public int PerformanceRate;/*PerformanceRate in the SWP Mode*/
            public Window_TypeDef Window;	/*Window Type for FFT in the SWP Mode*/
            public DetMode_TypeDef Detector;/*Detection Mode in the SWP Mode*/
            public RFRxPathTypedef RFPath;/*RFRx Path in the SWP Mode*/
            public TrigINSrc_TypeDef TrigSrc;/*Triger Source in the SWP Mode*/
            public TrigOUTMode_TypeDef TrigOutMode;/*Triger OutMode in the SWP Mode*/
            public CPUUtil_TypeDef CPUUtilization;/*CPUUtilization in the SWP Mode*/
            public SpurRej_TypeDef SpurRejLevel;/*SpurRejLevel in the SWP Mode*/
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IQS_TypeDef
        {
            public System.Double CenterFreq_Hz;/*CenterFreq_Hz in the IQS Mode*/
            public System.Double RefLevel_dBm;/*RefLevel in the IQS Mode*/
            public System.Double DecimateFactor;/*DecimateFactor in the IQS Mode, range 1--4096*/
            public System.UInt32 FramePts;/*FramePts in the IQS Mode, must be the power of 2 times(2^ )*/
            public System.Int32 Timeout;/*Timeout in the IQS Mode, unit(ms)*/
            //int RamSize;
            //int TimerCyc;
            //Window_TypeDef Window;
            public RFRxPathTypedef RFPath;/*RFPath in the IQS Mode*/
            public BBTRG_TypeDef TRGSrc;/*TRGSrc in the IQS Mode, Currently available: BBTRG_SOFT*/
            public TRGMode_TypeDef TRGMode;/*TRGMode in the IQS Mode, fixed or triggered*/
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MTR_TypeDef
        {
            public System.Double RefLevel_dBm;/*RefLevel in the Spectrum Monitor Mode*/
            public System.Double RBW_Hz;/*Resolution Bandwidth in the Spectrum Monitor Mode*/
            public int PerformanceRate;/*PerformanceRate in the Spectrum Monitor Modee*/
            public int DetPoints;/*Detection Points in Spectrum Monitor Mode*/
            public Window_TypeDef Window;/*Window Type for FFT in the Spectrum Monitor Mode*/
            public DetMode_TypeDef Detector;/*Detection Mode in the Spectrum Monitor Mode*/
            public RFRxPathTypedef RFPath;/*RFRx Path in the Spectrum Monitor Mode*/
            public TrigINSrc_TypeDef TrigSrc;/*Triger Source in the Spectrum Monitor Mode*/
            public TrigOUTMode_TypeDef TrigOutMode;/*Triger OutMode in the Spectrum Monitor Mode*/
            public DCCancel_Typedef DCCancelState;/*DCCancelState in the Spectrum Monitor Mode*/
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PBP_TypeDef
        {
            public System.Double StartFreq_Hz;/*Start Frquency in Point by Point Sweep Mode*/
            public System.Double StopFreq_Hz;/*Stop Frquency in Point by Point Sweep Mode*/
            public System.Double SweepPoints;/*SweepPoints in Point by Point Sweep Modee*/
            public System.Double RefLevel_dBm;/*RefLevel in Point by Point Sweep Mode*/
            public System.Double SamplePoints;/*SamplePoints inPoint by Point Sweep Mode*/
            public int PerformanceRate;/*PerformanceRate in Point by Point Sweep Mode*/
            public RFRxPathTypedef RFPath;/*RFRx Path in Point by Point Sweep Mode*/
            public TrigINSrc_TypeDef TrigSrc;/*Triger Source in Point by Point Sweep Mode*/
            public TrigOUTMode_TypeDef TrigOutMode;/*Triger OutMode in Point by Point Sweep Mode*/
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AUXS_TypeDef
        {
            public System.Double FixedFreq_Hz; /*Fixed Single tone Frequency*/
            public System.Double StartFreq_Hz; /*Start Frequency in Continous/Single Sweep Mode*/
            public System.Double StopFreq_Hz;/*Stop Frequency in Continous/Single Sweep Mode*/
            public System.Double StepFreq_Hz;/*Step Frequency in Continous/Single Sweep Mode*/
            public System.Double OutLevel_dBm;/*Output Power*/
            public System.Double DwellTime_s;/*DwellTime at Each Point in Continous/Single Sweep Mode*/
            public TxOprtMode_Typedef TxOperationMode;/*Tx Operation Mode*/
            public RFTxPathTypedef RFPath; /*Tx Output Path*/
        }
        #endregion



    }
}
