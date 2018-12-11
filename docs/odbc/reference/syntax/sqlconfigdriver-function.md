---
title: "SQLConfigDriver Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLConfigDriver"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLConfigDriver"
helpviewer_keywords: 
  - "SQLConfigDriver function [ODBC]"
ms.assetid: 4f681961-ac9f-4d88-b065-5258ba112642
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLConfigDriver Function
**Conformance**  
 Version Introduced: ODBC 2.5  
  
 **Summary**  
 **SQLConfigDriver** loads the appropriate driver setup DLL and calls the **ConfigDriver** function.  
  
 The functionality of **SQLConfigDriver** can also be accessed with [ODBCCONF.EXE](../../../odbc/odbcconf-exe.md).  
  
## Syntax  
  
```  
  
BOOL SQLConfigDriver(  
     HWND     hwndParent,  
     WORD     fRequest,  
     LPCSTR   lpszDriver,  
     LPCSTR   lpszArgs,  
     LPSTR    lpszMsg,  
     WORD     cbMsgMax,  
     WORD *   pcbMsgOut);  
```  
  
## Arguments  
 *hwndParent*  
 [Input] Parent window handle. The function will not display any dialog boxes if the handle is null.  
  
 *fRequest*  
 [Input] Type of request. *fRequest* must contain one of the following values:  
  
 ODBC_CONFIG_DRIVER: Changes the connection pooling timeout used by the driver.  
  
 ODBC_INSTALL_DRIVER: Installs a new driver.  
  
 ODBC_REMOVE_DRIVER: Removes an existing driver.  
  
 This option can also be driver-specific, in which case the *fRequest* for the first option must start from ODBC_CONFIG_DRIVER_MAX+1. The *fRequest* for any additional option must also start from a value greater than ODBC_CONFIG_DRIVER_MAX+1.  
  
 *lpszDriver*  
 [Input] The name of the driver as registered in the system information.  
  
 *lpszArgs*  
 [Input] A null-terminated string that contains arguments for a driver-specific *fRequest*.  
  
 *lpszMsg*  
 [Output] A null-terminated string that contains an output message from the driver setup.  
  
 *cbMsgMax*  
 [Input] Length of *lpszMsg.*  
  
 *pcbMsgOut*  
 [Output] Total number of bytes available to return in *lpszMsg*. If the number of bytes available to return is greater than or equal to *cbMsgMax*, the output message in *lpszMsg* is truncated to *cbMsgMax* minus the null-termination character. The *pcbMsgOut* argument can be a null pointer.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLConfigDriver** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_BUFF_LEN|Invalid buffer length|The *lpszMsg* argument was invalid.|  
|ODBC_ERROR_INVALID_HWND|Invalid window handle|The *hwndParent* argument was invalid.|  
|ODBC_ERROR_INVALID_REQUEST_TYPE|Invalid type of request|The *fRequest* argument was not one of the following:<br /><br /> ODBC_INSTALL_DRIVER ODBC_REMOVE_DRIVER<br /><br /> The *fRequest* argument was a driver-specific option that was less than or equal to ODBC_CONFIG_DRIVER_MAX.|  
|ODBC_ERROR_INVALID_NAME|Invalid driver or translator name|The *lpszDriver* argument was invalid. It could not be found in the registry.|  
|ODBC_ERROR_INVALID_KEYWORD_VALUE|Invalid keyword-value pairs|The *lpszArgs* argument contained a syntax error.|  
|ODBC_ERROR_REQUEST_FAILED|*Request* failed|The installer could not perform the operation requested by the *fRequest* argument. The call to **ConfigDriver** failed.|  
|ODBC_ERROR_LOAD_LIBRARY_FAILED|Could not load the driver or translator setup library|The driver setup library could not be loaded.|  
|ODBC_ERROR_OUT_OF_MEM|Out of memory|The installer could not perform the function because of a lack of memory.|  
  
## Comments  
 **SQLConfigDriver** allows an application to call a driver's **ConfigDriver** routine without having to know the name and load the driver-specific setup DLL. A Setup program calls this function after the driver setup DLL has been installed. The calling program should be aware that this function might not be available for all drivers. In such a case, the calling program should continue without error.  
  
## Driver-Specific Options  
 An application can request driver-specific features exposed by the driver by using the *fRequest* argument. The *fRequest* for the first option will be ODBC_CONFIG_DRIVER_MAX+1, and additional options will be incremented by 1 from that value. Any arguments required by the driver for that function should be provided in a null-terminated string passed in the *lpszArgs* argument. Drivers providing such functionality should maintain a table of driver-specific options. The options should be fully documented in driver documentation. Application writers who use driver-specific options should be aware that this use will make the application less interoperable.  
  
## Setting Connection Pooling Timeout  
 Connection pooling timeout properties can be set when you set the configuration of the driver. **SQLConfigDriver** is called with an *fRequest* of ODBC_CONFIG_DRIVER and *lpszArgs* set to **CPTimeout**. **CPTimeout** determines the period of time that a connection can remain in the connection pool without being used. When the timeout expires, the connection is closed and removed from the pool. The default timeout is 60 seconds.  
  
 When **SQLConfigDriver** is called with *fRequest* set to ODBC_INSTALL_DRIVER or ODBC_REMOVE_DRIVER, the Driver Manager loads the appropriate driver setup DLL and calls the **ConfigDriver** function. When **SQLConfigDriver** is called with an *fRequest* of ODBC_CONFIG_DRIVER, all processing is performed in the ODBC installer, so that the driver setup DLL does not have to be loaded.  
  
## Messages  
 A driver setup routine can send a text message to an application as null-terminated strings in the *lpszMsg* buffer. The message will be truncated to *cbMsgMax* minus the null-termination character by the **ConfigDriver** function if it is greater than or equal to *cbMsgMax* characters.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Adding, modifying, or removing a driver|[ConfigDriver](../../../odbc/reference/syntax/configdriver-function.md)(in the setup DLL)|  
|Removing the default data source|[SQLRemoveDefaultDataSource](../../../odbc/reference/syntax/sqlremovedefaultdatasource-function.md)|
