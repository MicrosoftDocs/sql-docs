---
title: "Driver Specification Subkeys | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "subkeys [ODBC], driver specification subkeys"
  - "driver specification subkeys [ODBC]"
  - "registry entries for components [ODBC], driver specification subkeys"
  - "drivers subkey [ODBC]"
ms.assetid: b4d802ef-b199-4e64-b7a5-6f2b3e5e2c80
author: MightyPen
ms.author: genemi
manager: craigg
---
# Driver Specification Subkeys
Each driver listed in the ODBC Drivers subkey has a subkey of its own. This subkey has the same name as the corresponding value under the ODBC Drivers subkey. The values under this subkey list the full paths of the driver and driver setup DLLs, the values of the driver keywords returned by **SQLDrivers**, and the usage count. The formats of the values are as shown in the following table.  
  
|Name|Data type|Data|  
|----------|---------------|----------|  
|APILevel|REG_SZ|**0** &#124; **1** &#124; **2**|  
|ConnectFunctions|REG_SZ|{**Y**&#124;**N**}{**Y**&#124;**N**}{**Y**&#124;**N**}|  
|CreateDSN|REG_SZ|*driver-description*|  
|Driver|REG_SZ|*driver-DLL-path*|  
|DriverODBCVer|REG_SZ|*nn.nn*|  
|FileExtns|REG_SZ|**\*.** *file-extension1*[**,\*.** *file-extension2*]...|  
|FileUsage|REG_SZ|**0** &#124; **1** &#124; **2**|  
|Setup|REG_SZ|*setup-DLL-path*|  
|SQLLevel|REG_SZ|**0** &#124; **1** &#124; **2**|  
|UsageCount|REG_DWORD|*count*|  
  
 The use of each keyword is shown in the following table.  
  
|Keyword|Usage|  
|-------------|-----------|  
|**APILevel**|A number indicating the ODBC interface conformance level supported by the driver:<br /><br /> 0 = None<br /><br /> 1 = Level 1 supported<br /><br /> 2 = Level 2 supported<br /><br /> This must be the same as the value returned for the SQL_ODBC_INTERFACE_CONFORMANCE option in **SQLGetInfo**.|  
|**CreateDSN**|The name of one or more data sources to be created when the driver is installed. The system information must include one data source specification section for each data source listed with the **CreateDSN** keyword. These sections should not include the **Driver** keyword, because this is specified in the driver specification section, but must include enough information for the **ConfigDSN** function in the driver setup DLL to create a data source specification without displaying any dialog boxes. For the format of a data source specification section, see [Data Source Specification Subkeys](../../../odbc/reference/install/data-source-specification-subkeys.md).|  
|**ConnectFunctions**|A three-character string indicating whether the driver supports **SQLConnect**, **SQLDriverConnect**, and **SQLBrowseConnect**. If the driver supports **SQLConnect**, the first character is "Y"; otherwise, it is "N". If the driver supports **SQLDriverConnect**, the second character is "Y"; otherwise, it is "N". If the driver supports **SQLBrowseConnect**, the third character is "Y"; otherwise, it is "N". For example, if a driver supports **SQLConnect** and **SQLDriverConnect** but not **SQLBrowseConnect**, the three-character string is "YYN".|  
|**DriverODBCVer**|A character string with the version of ODBC that the driver supports. The version is of the form *nn.nn*, where the first two digits are the major version and the next two digits are the minor version. For the version of ODBC described in this manual, the driver must return "03.00".<br /><br /> This must be the same as the value returned for the SQL_DRIVER_ODBC_VER option in **SQLGetInfo**.|  
|**FileExtns**|For file-based drivers, a comma-separated list of extensions of the files the driver can use. For example, a dBASE driver might specify \*.dbf and a formatted text file driver might specify \*.txt,\*.csv. For an example of how an application might use this information, see the **FileUsage** keyword.|  
|**FileUsage**|A number indicating how a file-based driver directly treats files in a data source.<br /><br /> 0 = The driver is not a file-based driver. For example, an ORACLE driver is a DBMS-based driver.<br /><br /> 1 = A file-based driver treats files in a data source as tables. For example, an Xbase driver treats each Xbase file as a table.<br /><br /> 2 = A file-based driver treats files in a data source as a catalog. For example, a Microsoft® Access driver treats each Microsoft Access file as a complete database.<br /><br /> An application might use this to determine how users will select data. For example, Xbase and Paradox users often think of data as stored in files, while ORACLE and Microsoft Access users generally think of data as stored in tables.<br /><br /> When a user selects **Open Data File** from the **File** menu, an application could display the **Windows File Open** common dialog box. The list of file types would use the file extensions specified with the **FileExtns** keyword for drivers that specify a **FileUsage** value of 1 and "Y" as the second character of the value of the **ConnectFunctions** keyword. After the user selects a file, the application would call **SQLDriverConnect** with the **DRIVER** keyword and then execute a **SELECT \* FROM *table-name*** statement.<br /><br /> When the user selects **Import Data** from the **File** menu, an application could display a list of descriptions for drivers that specify a **FileUsage** value of 0 or 2, and "Y" as the second character of the value of the **ConnectFunctions** keyword. After the user selects a driver, the application would call **SQLDriverConnect** with the **DRIVER** keyword and then display a custom **Select Table** dialog box.|  
|**SQLLevel**|A number indicating the SQL-92 grammar supported by the driver:<br /><br /> 0 = SQL-92 Entry<br /><br /> 1 = FIPS127-2 Transitional<br /><br /> 2 = SQL-92 Intermediate<br /><br /> 3 = SQL-92 Full<br /><br /> This must be the same as the value returned for the SQL_SQL_CONFORMANCE option in **SQLGetInfo**.|  
  
 For information about usage counts, see [Usage Counting](../../../odbc/reference/install/usage-counting.md) earlier in this section.  
  
 Applications should not set the usage count. ODBC will maintain this count.  
  
 For example, suppose a driver for formatted text files has a driver DLL named Text.dll, a separate driver setup DLL named Txtsetup.dll, and has been installed three times. If the driver supports the Level 1 API conformance level, supports the Minimum SQL grammar conformance level, treats files as tables, and can use files with the .txt and .csv extensions, the values under the Text subkey might be as follows:  
  
```  
APILevel : REG_SZ : 1  
ConnectFunctions : REG_SZ : YYN  
Driver : REG_SZ : C:\WINDOWS\SYSTEM32\TEXT.DLL  
DriverODBCVer : REG_SZ : 03.00.00  
FileExtns : REG_SZ : *.txt,*.csv  
FileUsage : REG_SZ : 1  
Setup : REG_SZ : C:\WINDOWS\SYSTEM32\TXTSETUP.DLL  
SQLLevel : REG_SZ : 0  
UsageCount : REG_DWORD : 0x3  
```
