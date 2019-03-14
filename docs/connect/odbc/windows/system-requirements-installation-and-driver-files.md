---
title: "System Requirements, Installation, and Driver Files | Microsoft Docs"
ms.custom: ""
ms.date: "02/14/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: d90fa182-1dab-4d6f-bd85-a04dd1479986
author: MightyPen
ms.author: genemi
manager: craigg
---
# System Requirements, Installation, and Driver Files
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports connections to SQL Server 2014, SQL Server 2012, [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssKatmai](../../../includes/sskatmai_md.md)], and [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
The ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows can be installed on a computer that also has one or more versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.  
  
The ODBC Driver 13 and 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], in addition to the above, supports SQL Server 2016. 

The ODBC Driver 17 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports all the above ones and also SQL Server 2017.
  
The driver name that you specify in a connection string is `ODBC Driver 11 for SQL Server` or `ODBC Driver 13 for SQL Server` (for both 13 and 13.1) or `ODBC Driver 17 for SQL Server`.
  
## Supported Operating Systems

You can run applications with the driver on the following Windows operating systems:  

-   Windows Server 2008 R2 
-   Windows Server 2012
-   Windows Server 2012 R2    
-   Windows Vista SP2  *(ODBC Driver 11 Only)*  
-   Windows 7  
-   Windows 8
-   Windows 8.1
-   Windows 10
  
## Installing Microsoft ODBC Driver for SQL Server

The driver is installed when you run `msodbcsql.msi` from one of the following links:

- [Download the Microsoft ODBC Driver 17 for SQL Server on Windows](https://www.microsoft.com/download/details.aspx?id=56567)
- [Download the Microsoft ODBC Driver 13.1 for SQL Server on Windows](https://www.microsoft.com/download/details.aspx?id=53339)
- [Download the Microsoft ODBC Driver 13 for SQL Server on Windows](https://www.microsoft.com/download/details.aspx?id=50420)
- [Download the Microsoft ODBC Driver 11 for SQL Server on Windows](https://www.microsoft.com/download/details.aspx?id=36434). 

> [!NOTE]
> For those who have Driver 17.1.0.1 or below installed, it is recommended that it be uninstalled manually prior to installing the newer version of the Driver

It can be installed side-by-side with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.  

When you invoke `msodbcsql.msi`, only the client components are installed by default. The client components are files that support running an application that was developed using the driver. To install the SDK components, specify `ADDLOCAL=ALL` on the command line. For example:  
  
```  
msiexec /i msodbcsql.msi ADDLOCAL=ALL  
```  
  
 Specify `IACCEPTMSODBCSQLLICENSETERMS=YES` to accept the terms of the end-user license if you use the `/passive`, `/qn`, `/qb`, or `/qr` option to install. This option must be specified in all uppercase letters. For example:  
  
```  
msiexec /quiet /passive /qn /i msodbcsql.msi IACCEPTMSODBCSQLLICENSETERMS=YES ADDLOCAL=ALL  
```  
  
 To do a silent uninstall:  
  
```  
msiexec /quiet /passive /qn /uninstall msodbcsql.msi  
```  
  
When an application uses the driver, the application should indicate that it depends on the driver through the install option `APPGUID`. Doing so enables the driver installer to report dependent applications before uninstalling. To specify a dependency on the driver, set the `APPGUID` command-line parameter to your product code when silently installing the driver. (A product code must be created when using Microsoft Installer to bundle your application setup program.) For example:  
  
```  
msiexec /i msodbcsql.msi APPGUID={ <Your dependent application's APPGUID> }  
```  

## Command-Line Tools: sqlcmd.exe and bcp.exe

The `bcp.exe` and `sqlcmd.exe` tools for use with the driver can be downloaded at [Microsoft Command Line Utilities 11 for SQL Server](https://www.microsoft.com/download/details.aspx?id=36433), [Microsoft Command Line Utilities 13 for SQL Server](https://www.microsoft.com/download/details.aspx?id=52680), or [Microsoft Command Line Utilities 13.1 for SQL Server](https://www.microsoft.com/download/details.aspx?id=53591). The driver is a prerequisite to install `sqlcmd.exe` and `bcp.exe`.
  
`bcp.exe` and `sqlcmd.exe` are installed in the `110\Tools` subfolder of `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC` for version 11, and `130\Tools` for 13 and 13.1.

An application that uses BCP functions must specify the driver from the same version which shipped with the header file and library used to compile the application.  

For example, when you compile an ODBC application with `msodbcsql11.lib` and `msodbcsql.h`, use "DRIVER={ODBC Driver 11 for SQL Server}" in the connection string.

## Components of the Microsoft ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows 
 The ODBC driver on Windows contains the following components:
 
|Component|Description|  
|---------------|-----------------|  
|msodbcsql17.dll or <br> msodbcsql13.dll or <br> msodbcsql11.dll|The dynamic-link library (DLL) file that contains all of the driver's functionality. This file is installed in %SYSTEMROOT%\System32.|  
|msodbcdiag17.dll or <br> msodbcdiag13.dll or <br> msodbcdiag11.dll|The dynamic-link library (DLL) file that contains the driver's diagnostics (tracing) interface. This file is installed in %SYSTEMROOT%\System32.|
|msodbcsqlr17.rll or <br> msodbcsqlr13.rll or <br> msodbcsqlr11.rll|The accompanying resource file for the driver library. This file is installed in %SYSTEMROOT%\System32\1033.| 
|s13ch_msodbcsql.chm or <br> s11ch_msodbcsql.chm |The Data Source Wizard help file that documents how to create a data source for the driver. This file is installed in %SYSTEMROOT%\System32\1033 <br /> <br /> **NOTE:** There is no chm file for ODBC Driver 17. |  
|msodbcsql.h|The header file that contains all of the new definitions needed to use the driver.<br /><br /> **Note:**  You cannot reference msodbcsql.h and odbcss.h in the same program.<br /><br /> msodbcsql.h for ODBC Driver 17 or 13 is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\130\SDK. <br /> msodbcsql.h for ODBC Driver 11 is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\SDK.| 
|msodbcsql17.lib or <br> msodbcsql13.lib or <br> msodbcsql11.lib|The library file needed to call the **bcp** utility functions that are part of the driver.<br /><br /> **Note:**  If you do reference this library file in your program, make sure that it is in your system path and in the system path of those that use the application.<br /><br /> msodbcsql17.lib or msodbcsql13.lib is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\130\SDK.<br /> msodbcsql11.lib is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\SDK.|

  
## See Also  
 [Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)  
  
  
