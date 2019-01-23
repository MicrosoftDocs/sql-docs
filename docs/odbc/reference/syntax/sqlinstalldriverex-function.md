---
title: "SQLInstallDriverEx Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLInstallDriverEx"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLInstallDriverEx"
helpviewer_keywords: 
  - "SQLInstallDriverEx function [ODBC]"
ms.assetid: 1dd74544-f4e9-46e1-9b5f-c11d84fdab4c
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLInstallDriverEx Function
**Conformance**  
 Version Introduced: ODBC 3.0  
  
 **Summary**  
 **SQLInstallDriverEx** adds information about the driver to the Odbcinst.ini entry in the system information and increments the driver's *UsageCount* by 1. However, if a version of the driver already exists but the *UsageCount* value for the driver does not exist, the new *UsageCount* value is set to 2.  
  
 This function does not actually copy any files. It is the responsibility of the calling program to copy the driver's files to the target directory properly.  
  
 The functionality of **SQLInstallDriverEx** can also be accessed with [ODBCCONF.EXE](../../../odbc/odbcconf-exe.md).  
  
## Syntax  
  
```  
  
BOOL SQLInstallDriverEx(  
     LPCSTR    lpszDriver,  
     LPCSTR    lpszPathIn,  
     LPSTR     lpszPathOut,  
     WORD      cbPathOutMax,  
     WORD *    pcbPathOut,  
     WORD      fRequest,  
     LPDWORD   lpdwUsageCount);  
```  
  
## Arguments  
 *lpszDriver*  
 [Input] The driver description (usually the name of the associated DBMS) presented to users instead of the physical driver name. The *lpszDriver* argument must contain a doubly null-terminated list of keyword-value pairs describing the driver. For more information about keyword-value pairs, see [Driver Specification Subkeys](../../../odbc/reference/install/driver-specification-subkeys.md). For more information about the doubly null-terminated string, see [ConfigDSN Function](../../../odbc/reference/syntax/configdsn-function.md).  
  
 *lpszPathIn*  
 [Input] Full path of the target directory of the installation, or a null pointer. If *lpszPathIn* is a null pointer, the drivers will be installed in the system directory.  
  
 *lpszPathOut*  
 [Output] Path of the target directory where the driver should be installed. If the driver has not previously been installed, *lpszPathOut* should be the same as *lpszPathIn*. If the driver was previously installed, *lpszPathOut* is the path of the previous installation.  
  
 *cbPathOutMax*  
 [Input] Length of *lpszPathOut*.  
  
 *pcbPathOut*  
 [Output] Total number of bytes (excluding the null-termination character) available to return in *lpszPathOut*. If the number of bytes available to return is greater than or equal to *cbPathOutMax*, the output path in *lpszPathOut* is truncated to *cbPathOutMax* minus the null-termination character. The *pcbPathOut* argument can be a null pointer.  
  
 *fRequest*  
 [Input] Type of request. The *fRequest* argument must contain one of the following values:  
  
 ODBC_INSTALL_INQUIRY: Inquire about where a driver can be installed.  
  
 ODBC_INSTALL_COMPLETE: Complete the installation request.  
  
 *lpdwUsageCount*  
 [Output] The usage count of the driver after this function has been called.  
  
 Applications should not set the usage count. ODBC will maintain this count.  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **SQLInstallDriverEx** returns FALSE, an associated *\*pfErrorCode* value can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_GENERAL_ERR|General installer error|An error occurred for which there was no specific installer error.|  
|ODBC_ERROR_INVALID_BUFF_LEN|Invalid buffer length|The *lpszPathOut* argument was not large enough to contain the output path. The buffer contains the truncated path.<br /><br /> The *cbPathOutMax* argument was 0, and *fRequest* was ODBC_INSTALL_COMPLETE.|  
|ODBC_ERROR_INVALID_REQUEST_TYPE|Invalid type of request|The *fRequest* argument was not one of the following:<br /><br /> ODBC_INSTALL_INQUIRY ODBC_INSTALL_COMPLETE|  
|ODBC_ERROR_INVALID_KEYWORD_VALUE|Invalid keyword-value pairs|The *lpszDriver* argument contained a syntax error.|  
|ODBC_ERROR_INVALID_PATH|Invalid install path|The *lpszPathIn* argument contained an invalid path.|  
|ODBC_ERROR_LOAD_LIBRARY_FAILED|Could not load the driver or translator  setup library|The driver setup library could not be loaded.|  
|ODBC_ERROR_INVALID_PARAM_SEQUENCE|Invalid parameter sequence|The *lpszDriver* argument did not contain a list of keyword-value pairs.|  
|ODBC_ERROR_USAGE_UPDATE_FAILED|Could not increment or decrement the component usage count|The installer failed to increment the driver's usage count.|  
  
## Comments  
 The *lpszDriver* argument is a list of attributes in the form of keyword-value pairs. Each pair is terminated with a null byte, and the entire list is terminated with a null byte. (That is, two null bytes mark the end of the list.) The format of this list is as follows:  
  
 _driver-desc_ **\\**0Driver**=**_driver-DLL-filename_**\\**0[Setup**=**_setup-DLL-filename_<b>\\</b>0]  
  
 [_driver-attr-keyword1_**=**_value1_<b>\\</b>0][_driver-attr-keyword2_**=**_value2_<b>\\</b>0]...<b>\\</b>0  
  
 where \0 is a null byte and *driver-attr-keywordn* is any driver attribute keyword. The keywords must appear in the specified order. For example, suppose a driver for formatted text files has separate driver and setup DLLs and can use files with the .txt and .csv extensions. The *lpszDriver* argument for this driver might be as follows:  
  
```  
Text\0Driver=TEXT.DLL\0Setup=TXTSETUP.DLL\0FileUsage=1\0  
FileExtns=*.txt,*.csv\0\0  
```  
  
 Suppose that a driver for SQL Server does not have a separate setup DLL and does not have any driver attribute keywords. The *lpszDriver* argument for this driver might be as follows:  
  
```  
SQL Server\0Driver=SQLSRVR.DLL\0\0  
```  
  
 After **SQLInstallDriverEx** retrieves information about the driver from the *lpszDriver* argument, it adds the driver description to the [ODBC Drivers] section of the Odbcinst.ini entry in the system information. It then creates a section titled with the driver's description and adds the full paths of the driver DLL and the setup DLL. Finally, it returns the path of the target directory of the installation but does not copy the driver files to it. The calling program must actually copy the driver files to the target directory.  
  
 **SQLInstallDriverEx** increments the component usage count for the installed driver by 1. If a version of the driver already exists but the component usage count for the driver does not exist, the new component usage count value is set to 2.  
  
 The application setup program is responsible for physically copying the driver file and maintaining the file usage count. If the driver file has not previously been installed, the application setup program must copy the file in the *lpszPathIn* path and create the file usage count. If the file has previously been installed, the setup program merely increments the file usage count and returns the path of the prior installation in the *lpszPathOut* argument.  
  
> [!NOTE]  
>  For more information about component usage counts and file usage counts, see [Usage Counting](../../../odbc/reference/install/usage-counting.md).  
  
 If an older version of the driver file was previously installed by the application, the driver should be uninstalled and then reinstalled, so that the driver component usage count is valid. **SQLConfigDriver** (with an *fRequest* of ODBC_REMOVE_DRIVER) should first be called, and then **SQLRemoveDriver** should be called to decrement the component usage count. **SQLInstallDriverEx** should then be called to reinstall the driver, incrementing the component usage count. The application setup program must replace the old file with the new file. The file usage count will remain the same, and any other application that used the older version file will now use the newer version.  
  
> [!NOTE]  
>  If the driver was previously installed and **SQLInstallDriverEx** is called to install the driver in a different directory, the function will return TRUE, but *lpszPathOut* will include the directory where the driver was already installed. It will not include the directory entered in the *lpszDriver* argument.  
  
 The length of the path in *lpszPathOut* in **SQLInstallDriverEx** allows for a two-phase install process, so an application can determine what *cbPathOutMax* should be by calling **SQLInstallDriverEx** with an *fRequest* of ODBC_INSTALL_INQUIRY mode. This will return the total number of bytes available in the *pcbPathOut* buffer. **SQLInstallDriverEx** can then be called with an *fRequest* of ODBC_INSTALL_COMPLETE and the *cbPathOutMax* argument set to the value in the *pcbPathOut* buffer, plus the null-termination character.  
  
 If you choose not to use the two-phase model for **SQLInstallDriverEx**, you must set *cbPathOutMax*, which defines the size of the storage for the path of the target directory, to the value _MAX_PATH, as defined in Stdlib.h, to prevent truncation.  
  
 When *fRequest* is ODBC_INSTALL_COMPLETE, **SQLInstallDriverEx** does not allow *lpszPathOut* to be NULL (or *cbPathOutMax* to be 0). If *fRequest* is ODBC_INSTALL_COMPLETE, FALSE is returned when the number of bytes available to return is greater than or equal to *cbPathOutMax*, with the result that truncation occurs.  
  
 After **SQLInstallDriverEx** has been called and the application setup program has copied the driver file (if necessary), the driver setup DLL must call **SQLConfigDriver** to set the configuration for the driver.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Installing the Driver Manager|[SQLInstallDriverManager](../../../odbc/reference/syntax/sqlinstalldrivermanager-function.md)|
