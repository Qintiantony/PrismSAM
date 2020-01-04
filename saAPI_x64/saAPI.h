/*
## HAROGIC SA Library header file (saAPI.h)
## =======================================================
##
##  Copyright HAROGIC Technologies, 2015-2018,
##  All Rights Reserved
##  Release Time: 14-Apr-20180414
##  Version: V3.57
##  This Library Contains all User Level Fucntions for the SA
## =======================================================
*/

/*To Prevent multiCompile*/
#pragma once
#ifndef SA_API_H
#define SA_API_H

#include "stdafx.h"
#include "string.h"
#include<stdio.h>
#include<stdlib.h>
#include<windows.h>

#ifdef __cplusplus
extern "C"{
#endif

#ifdef SA_API
#define SA_API __declspec(dllexport)
#else
#define SA_API __declspec(dllimport)
#endif

	/*API Version Information*/
	typedef struct
	{
		long ErrorStatus;	/*Error Status for API Called */
		char *ErrorDetail;	/*Error Detailed Info for API Called*/
		long ErrorID;		/*Error ID to Indicate the API ID*/
	}ErrTypedef;

	typedef enum
	{
		InternalREFCLK = 0x00,		//Select internal 10MHz as system reference clock; Default internal
		ExternalREFCLK = 0x01		//Select external 10MHz as system reference clock

	}REFCLKSource_TypeDef;

	/*Triger Source for SWP/MTR Mode*/
	typedef enum
	{
		TRIGIN_FREERUN = 0x00,	/*Use Auto Trig and Free Run*/
		TRIGIN_EXT_PRE = 0x01,	/*Use External Trig,wait for the Rising Edge at Each Points*/
		TRIGIN_EXT_PFE = 0x02,	/*Use External Trig,wait for the falling Edge at Each Points*/
		TRIGIN_EXT_SRE = 0x03,	/*Use External Trig,wait for the Rising Edge at Each Sweep*/
		TRIGIN_EXT_SFE = 0x04	/*Use External Trig,wait for the falling Edge at Each Sweep*/
	}TrigINSrc_TypeDef;

	/*
		Triger Out Mode for SWP/MTR/AUXS;
		Only Operate in One Mode at the same time
	*/
	typedef enum
	{
		TRIGOUT_NULL = 0x00,	/*No Trigger Out*/
		TRIGOUT_PRE = 0x01,		/*Trigger out with Sending Rising Edge at Each Points*/
		TRIGOUT_PFE = 0x02,		/*Trigger out with Sending Rising falling Edge at Each Points*/
		TRIGOUT_SRE = 0x03,		/*Trigger out with Sending Rising Rising Edge at Each Sweep*/
		TRIGOUT_SFE = 0x04		/*Trigger out with Sending Rising falling Edge at Each Sweep*/
	}TrigOUTMode_TypeDef;
	
	/*RFRx Path*/
	typedef enum
	{
		RxPath_Ext = 0x00,		/*External RF Rx Path*/
		RxPath_Int = 0x01		/*Internal RF Rx Path*/
	}RFRxPathTypedef;
	
	/* Vedio Detection Mode for SWP/MTR Mode*/
	typedef enum
	{
		NegPeak = 0x00,		/*Negtive Peak Detection Mode*/
		Sample = 0x01,		/*Sample Detection Mode*/
		Rosenfell = 0x02,	/*Rosenfell Detection Mode*/
		PosPeak = 0x03,		/*Postive Peak Detection Mode*/
		RMS = 0x04			/*RMS Detection Mode*/
	}DetMode_TypeDef;

	/*Window Type for the FFT Process*/
	typedef enum
	{
		FlatTop = 0x00,		/*Flattop Window for FFT Process*/
		Nuttall = 0x01		/*Nuttall Window for FFT Process*/
	}Window_TypeDef;

	/*
	CPU Utilization
	Adjust the CPU Ulitization to change the Sweep Speed
	*/
	typedef enum
	{
		Low = 0x00,		/*CPU Utilization Level -Low*/
		Medium = 0x01,	/*CPU Utilization Level -Medium*/
		High = 0x02		/*CPU Utilization Level -High*/
	}CPUUtil_TypeDef;

	/*SpurRejection for SWP Mode*/
	typedef enum
	{
		Bypass = 0x00,		/*Bypass:No Spurrejection*/
		Standard = 0x01,	/*Standard:Medium Spurrejection*/
		Enhanced = 0x02		/*Enhanced:Highest Spurrejection*/
	}SpurRej_TypeDef;

	/*DC Cancellation for the MTR Mode*/
	typedef enum
	{
		DCCancel_Off = 0x00,	/*Turn off the DCCancellation Function*/
		DCCancel_On = 0x01		/*Turn on the DCCancellation Function*/
	}DCCancel_Typedef;

	/*DSP Platform*/
	typedef enum
	{
		CPU = 0x00,	/*DSP on FPGA*/
		FPGA = 0x01		/*DSP on CPU*/
	}DSPPlatform_Typedef;

	typedef enum
	{
		BBTRG_EXT = 0x00,
		BBTRG_TIMER = 0x01,
		BBTRG_AFE = 0x02,
		BBTRG_LEVEL = 0x03,
		BBTRG_INSTR = 0x04,
		BBTRG_SOFT = 0x05
	} BBTRG_TypeDef;

	typedef enum
	{
		FixedPoints = 0x00,
		Continuous = 0x01
	} TRGMode_TypeDef;

	/*
	SWP Config Infomation
	This Function is declared to be read by API
	*/
	typedef struct
	{
		//double StartFreq_Hz;		/*SA Start Frequency in SWP Mode*/
		//double StopFreq_Hz;			/*SA Stop Frequency in SWP Mode*/
		//double StepFreq_Hz;			/*SA Step Frequency in SWP Mode*/
		//unsigned long SweepPoints;  /*SA Frequency SweepPoints in SWP Mode*/
		unsigned long TracePoints;  /*Trace Points in SWP Mode*/
		unsigned long DetPoints;	/*Detection Points in SWP Mode*/
		unsigned long DecimateRate; /*Decimate Rate in SWP Mode*/
		DSPPlatform_Typedef DSPPlatform; /*DSP Platform*/
	}SWPParam_Typedef;

	/*Parameter for the SweepMode*/
	typedef struct
	{
		double StartFreq_Hz;	/*Start Frquency in the SWP Mode*/
		double StopFreq_Hz;		/*Stop Frquency in the SWP Mode*/
		double RefLevel_dBm;	/*RefLevel in the SWP Mode*/
		double RBW_Hz;			/*Resolution Bandwidth in the SWP Mode*/
		unsigned long TracePoints;/*Trace Points in the SWP Mode*/
		unsigned long  PerformanceRate;/*PerformanceRate in the SWP Mode*/
		Window_TypeDef Window;	/*Window Type for FFT in the SWP Mode*/
		DetMode_TypeDef Detector;/*Detection Mode in the SWP Mode*/
		RFRxPathTypedef   RFPath;/*RFRx Path in the SWP Mode*/
		TrigINSrc_TypeDef TrigSrc;/*Triger Source in the SWP Mode*/
		TrigOUTMode_TypeDef TrigOutMode;/*Triger OutMode in the SWP Mode*/
		CPUUtil_TypeDef CPUUtilization;/*CPUUtilization in the SWP Mode*/
		SpurRej_TypeDef SpurRejLevel;/*SpurRejLevel in the SWP Mode*/
	}SWP_TypeDef;

	/*Parameter for IQ Stream Mode*/
	typedef struct
	{
		double CenterFreq_Hz;	/*Center Frquency in the IQ StreamMode*/
		double RefLevel_dBm;	/*RefLevel in the IQ StreamMode*/
		double DecimateFactor;	/*Decimate Rata in the IQ StreamMode*/
		//Window_TypeDef Window;	/*Window Type for FFT in the IQ StreamMode*/
		unsigned int FramePts;
		int Timeout;
		RFRxPathTypedef   RFPath;/*RFRx Path in the IQ StreamMode*/
		BBTRG_TypeDef TRGSrc;
		TRGMode_TypeDef TRGMode;
		//TXTRGSRC_TypeDef TrigSrc;/*Triger Source in the SWP Mode*/
	}IQS_TypeDef;

	/*Parameters for the Spectrum Monitor Mode*/
	typedef struct
	{
		double RefLevel_dBm;/*RefLevel in the Spectrum Monitor Mode*/
		double RBW_Hz;/*Resolution Bandwidth in the Spectrum Monitor Mode*/
		unsigned long  PerformanceRate;/*PerformanceRate in the Spectrum Monitor Modee*/
		long  DetPoints;/*Detection Points in Spectrum Monitor Mode*/
		Window_TypeDef Window;/*Window Type for FFT in the Spectrum Monitor Mode*/
		DetMode_TypeDef Detector;/*Detection Mode in the Spectrum Monitor Mode*/
		RFRxPathTypedef   RFPath;/*RFRx Path in the Spectrum Monitor Mode*/
		TrigINSrc_TypeDef TrigSrc;/*Triger Source in the Spectrum Monitor Mode*/
		TrigOUTMode_TypeDef TrigOutMode;/*Triger OutMode in the Spectrum Monitor Mode*/
		DCCancel_Typedef DCCancelState;/*DCCancelState in the Spectrum Monitor Mode*/
	}MTR_TypeDef;

	/*Parameters for PointByPoint Mode*/
	typedef struct
	{
		double StartFreq_Hz;/*Start Frquency in Point by Point Sweep Mode*/
		double StopFreq_Hz;/*Stop Frquency in Point by Point Sweep Mode*/
		double SweepPoints;/*SweepPoints in Point by Point Sweep Modee*/
		double RefLevel_dBm;/*RefLevel in Point by Point Sweep Mode*/
		double SamplePoints;/*SamplePoints inPoint by Point Sweep Mode*/
		unsigned long  PerformanceRate;/*PerformanceRate in Point by Point Sweep Mode*/
		RFRxPathTypedef   RFPath;/*RFRx Path in Point by Point Sweep Mode*/
		TrigINSrc_TypeDef TrigSrc;/*Triger Source in Point by Point Sweep Mode*/
		TrigOUTMode_TypeDef TrigOutMode;/*Triger OutMode in Point by Point Sweep Mode*/
	}PBP_TypeDef;

	/*Tx Operation Mode*/
	typedef enum
	{
		OPRT_PWROFF = 0x00,		/*Tx Power Off*/
		OPRT_SINGPTS = 0x01,	/*Tx Operate in Single Frequency Points*/
		OPRT_CONTSW = 0x02,		/*Tx Continous Sweep*/
		OPRT_SINGSW = 0x03		/*Tx Single Sweep*/
	}TxOprtMode_Typedef;

	/*Tx Path*/
	typedef enum
	{
		TxPath_Ext = 0x00,	/*External RF Tx Path*/
		TxPath_Int = 0x01	/*Internal RF Tx Path*/
	}RFTxPathTypedef;

	/*Parameters for the Auxiliary Source */
	typedef struct
	{
		double FixedFreq_Hz; /*Fixed Single tone Frequency*/
		double StartFreq_Hz; /*Start Frequency in Continous/Single Sweep Mode*/
		double StopFreq_Hz;/*Stop Frequency in Continous/Single Sweep Mode*/
		double StepFreq_Hz;/*Step Frequency in Continous/Single Sweep Mode*/
		double OutLevel_dBm;/*Output Power*/
		double DwellTime_s;/*DwellTime at Each Point in Continous/Single Sweep Mode*/
		TxOprtMode_Typedef TxOperationMode;/*Tx Operation Mode*/
		RFTxPathTypedef   RFPath; /*Tx Output Path*/
	}AUXS_TypeDef;


	SA_API int TestAdd(int A, int B);
	/*Exported for the USB UserAPI*/
	SA_API int USB_OpenDevice(void **USBDevice, int DeviceNum);
	SA_API int USB_ResetDevice(void **USBDevice);
	SA_API int USB_CloseDevice(void **USBDevice);
	SA_API int USB_CtrlWrite(void **USBDevice, unsigned char CtrlData[], long Datalen);
	SA_API int USB_CtrlRead(void **USBDevice, unsigned char CtrlData[], long Datalen);
	SA_API int USB_ControlIN(void **USBDevice, unsigned char ReqCode, unsigned short wValue, unsigned short wIndex, unsigned char USBVersion[], long Datalen);
	SA_API int USB_BulkIN(void **USBDevice, unsigned char BulkData[], long BulkSize);
	SA_API int USB_SetBulkInTimeout(void **USBDevice, int Timeout);
	SA_API int USB_SetBurstLen(void **USBDevice, unsigned short Burstlen);
	SA_API int USB_QueryVersion(void **USBDevice, unsigned char USBVersion[], long Datalen);

	/*Exported for the System UserAPI*/
	SA_API int SA_OpenDevice(void **pSA, int DeviceNum);
	SA_API int SA_CloseDevice(void **pSA);
	SA_API int SA_ResetDevice(void **pSA);
	SA_API int SA_Initiate(void **pSA);
	SA_API int SA_GetFrameData(void **pSA, unsigned char BulkData[], long BulkSize);
	SA_API int SA_QueryTemp(void **pSA, double *SA_Temp);
	SA_API int SA_QueryStartupState(void **pSA, char *StartupState);
	SA_API int SA_QueryDevInfo(void **pSA, char HWInfo[], char SWInfo[], char FuncList[], char DevUID[]);
	SA_API int SA_CalPwrtoTemp(void **pSA,double *Temp);
	
	SA_API int SA_QueryRefClkSrc(void **pSA, REFCLKSource_TypeDef *CurrRefClk);
	SA_API int SA_SetRefClkSrc(void **pSA, REFCLKSource_TypeDef *RefClkSrc);
	/*Exported for the Operation UserAPI*/
	/*SWP Mode API*/
	SA_API int SWP_Configuration(void **pSA, SWP_TypeDef *SWP_Profile);
	SA_API int SWP_QueryParam(void **pSA, SWPParam_Typedef *SWPParam);
	SA_API int SWP_GetPartialSweep(void **pSA, double Frequency[], double Amplitude[], long *PackageIndex);
	SA_API int SWP_GetSpectrumTrace(void **pSA, double PartialFreq[], double PartialAmpl[], double PanoFreq[], double PanoAmpl[], long *PackageIndex);
	
	/*IQ Stream Mode API*/
	SA_API int IQS_Configuration(void **pSA, IQS_TypeDef *IQS_Profile);
	SA_API int IQS_GetIQStream(void **pSA, unsigned char BulkData[], long BulkSize);
	SA_API int IQS_SoftTRGStart(void **pSA);
	SA_API int IQS_SoftTRGStop(void **pSA);
	SA_API double IQS_GetPwrOffset(void **pSA, double CustomOffset);

	/*Spectrum Monitor API*/
	SA_API int MTR_Configuration(void **pSA, MTR_TypeDef *MTR_Profile, double FreqTab_Hz[], long FmapSize);
	SA_API int MTR_GetSpectrum(void **pSA, double Frequency[], double Amplitude[], long *PackageIndex);
	//SA_API int MTR_GetTDWaveform(void **pSA, double Frequency[], double Amplitude[], long *PackageIndex);

	/*AUXS API*/
	SA_API int AUXS_Configuration(void **pSA, AUXS_TypeDef *AUXS_Profile);
	/*PBP API*/
	SA_API int PBP_Configuration(void **pSA, PBP_TypeDef *PBP_Profile);
	/*AUXS Calibration API*/
	SA_API int ACAL_Configuration(void **pSA, PBP_TypeDef *PBP_Profile);


#ifdef __cplusplus
}
#endif

#endif


