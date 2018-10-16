---
title: "ConfigDriver Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "ConfigDriver"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "ConfigDriver"
helpviewer_keywords: 
  - "ConfigDriver [ODBC]"
ms.assetid: 9473f48f-bcae-4784-89c1-7839bad4ed13
author: MightyPen
ms.author: genemi
manager: craigg
---
# ConfigDriver Function
**Conformance**  
 Version Introduced: ODBC 2.5  
  
 **Summary**  
 **ConfigDriver** allows a setup program to perform install and uninstall functions without requiring the program to call **ConfigDSN**. This function will perform driver-specific functions such as creating driver-specific system information and performing DSN conversions during installation, as well as cleaning up system information modifications during uninstall. This function is exposed by the driver setup DLL or a separate setup DLL.  
  
## Syntax  
  
```  
  
BOOL ConfigDriver(  
      HWND    hwndParent,  
      WORD    fRequest,  
      LPCSTR  lpszDriver,  
      LPCSTR  lpszArgs,  
      LPSTR   lpszMsg,  
      WORD    cbMsgMax,  
      WORD *  pcbMsgOut);  
```  
  
## Arguments  
 *hwndParent*  
 [Input] Parent window handle. The function will not display any dialog boxes if the handle is null.  
  
 *fRequest*  
 [Input] Type of request. The *fRequest* argument must contain one of the following values:  
  
 ODBC_INSTALL_DRIVER: Install a new driver.  
  
 ODBC_REMOVE_DRIVER: Remove a driver.  
  
 This option can also be driver-specific, in which case the *fRequest* argument for the first option must start from ODBC_CONFIG_DRIVER_MAX+1. The *fRequest* argument for any additional option must also start from a value greater than ODBC_CONFIG_DRIVER_MAX+1.  
  
 *lpszDriver*  
 [Input] The name of the driver as registered in the Odbcinst.ini key of the system information.  
  
 *lpszArgs*  
 [Input] A null-terminated string containing arguments for a driver-specific *fRequest*.  
  
 *lpszMsg*  
 [Output] A null-terminated string containing an output message from the driver setup.  
  
 *cbMsgMax*  
 [Input] Length of *lpszMsg*.  
  
 *pcbMsgOut*  
 [Output] Total number of bytes available to return in *lpszMsg*.  
  
 If the number of bytes available to return is greater than or equal to *cbMsgMax*, the output message in *lpszMsg* is truncated to *cbMsgMax* minus the null-termination character. The *pcbMsgOut* argument can be a null pointer.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **ConfigDriver** returns FALSE, an associated *\*pfErrorCode* value is posted to the installer error buffer by a call to **SQLPostInstallerError** and can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_INVALID_HWND|Invalid window handle|The *hwndParent* argument was invalid.|  
|ODBC_ERROR_INVALID_REQUEST_TYPE|Invalid type of request|The *fRequest* argument was not one of the following:<br /><br /> ODBC_INSTALL_DRIVER ODBC_REMOVE_DRIVER<br /><br /> The driver-specific option was less than or equal to ODBC_CONFIG_DRIVER_MAX.|  
|ODBC_ERROR_INVALID_NAME|Invalid driver or translator name|The *lpszDriver* argument was invalid. It could not be found in the registry.|  
|ODBC_ERROR_REQUEST_FAILED|*Request* failed|Could not perform the operation requested by the *fRequest* argument.|  
|ODBC_ERROR_DRIVER_SPECIFIC|Driver- or translator-specific error|A driver-specific error for which there is no defined ODBC installer error. The *SzError* argument in a call to the **SQLPostInstallerError** function should contain the driver-specific error message.|  
  
## Comments  
  
### Driver-Specific Options  
 An application can request driver-specific features exposed by the driver by using the *fRequest* argument. The *fRequest* for the first option will be ODBC_CONFIG_DRIVER_MAX plus 1, and additional options will be incremented by 1 from that value. Any arguments required by the driver for that function should be provided in a null-terminated string passed in the *lpszArgs* argument. Drivers providing such functionality should maintain a table of driver-specific options. The options should be fully documented in driver documentation. Application writers who use driver-specific options should be aware that this will make the application less interoperable.  
  
### Messages  
 A driver setup routine can send a text message to an application as a null-terminated string in the *lpszMsg* buffer. The message will be truncated to *cbMsgMax* minus the null-termination character by the **ConfigDriver** function if it is greater than or equal to *cbMsgMax* characters.
