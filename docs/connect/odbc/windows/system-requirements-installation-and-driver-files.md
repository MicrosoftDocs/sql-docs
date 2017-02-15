---
title: "System Requirements, Installation, and Driver Files | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d90fa182-1dab-4d6f-bd85-a04dd1479986
caps.latest.revision: 26
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# System Requirements, Installation, and Driver Files
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

  The ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] supports connections to SQL Server 2014, SQL Server 2012 R2, [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro_md.md)], [!INCLUDE[ssKatmai](../../../includes/sskatmai_md.md)], and [!INCLUDE[ssVersion2005](../../../includes/ssversion2005_md.md)].  
  
 The ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Windows can be installed on a computer that also has one or more versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] Native Client.  
  
 The ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] in addition to the above, supports SQL Server 2016.  
  
 The driver name that you specify in a connection string is **ODBC Driver 11 for SQL Server** (or **ODBC Driver 13 for SQL Server** if using ODBC Driver 13.1).  
  
## Supported Operating Systems  
 You can run applications with the ODBC driver on Windows on the following Windows operating systems:  

-   Windows Server 2008 R2 *(ODBC Driver 11 Only)*
-   Windows Server 2012
-   Windows Server 2012 R2    
-   Vista SP2  *(ODBC Driver 11 Only)*  
-   Windows 7 *(ODBC Driver 11 Only)*  
-   Windows 8
-   Windows 8.1
-   Windows 10
  
## Installing Microsoft ODBC Driver for SQL Server  
 The ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Windows, or the ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Windows, is installed when you run msodbcsql.msi from [Download the Microsoft ODBC Driver 13.1 for SQL Server on Windows](https://www.microsoft.com/en-us/download/details.aspx?id=50420) or [Download the Microsoft ODBC Driver 11 for SQL Server on Windows](http://www.microsoft.com/download/details.aspx?id=36434) respectively. The ODBC driver can be installed side-by-side with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] Native Client.  
  
 When you invoke msodbcsql.msi, only the client components are installed by default. The client components are files that support running an application that was developed using the ODBC driver. To install the SDK components, specify `ADDLOCAL=All` on the command line. For example:  
  
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
  
 When an application uses the ODBC driver, the application should indicate that it depends on the driver through the install option APPGUID. Doing so enables the ODBC driver installer to report dependent applications, before uninstalling. To specify a dependency on the ODBC driver, set the APPGUID command-line parameter to your product code when silently installing the ODBC driver. (A product code must be created when using Microsoft Installer to bundle your application setup program.) For example:  
  
```  
msiexec /i msodbcsql.msi APPGUID={ <Your dependent application's APPGUID> }  
```  
 
## Command-Line Tools: Sqlcmd.exe and Bcp.exe  
 The bcp.exe and sqlcmd.exe tools for use with the Microsoft ODBC Driver 11 for SQL Server on Windows can be downloaded at [Download bcp and sqlcmd for the Microsoft ODBC Driver 11 for SQL Server on Windows](http://www.microsoft.com/download/details.aspx?id=36433). The ODBC driver is a prerequisite to install Sqlcmd.exe and Bcp.exe.  
  
 Bcp.exe and sqlcmd.exe are installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\Tools.  
  
 An application that uses BCP functions must specify the driver from the same version that shipped with the header file and library used to compile the application.  
  
 For example, when you compile an ODBC driver application with msodbcsql11.lib and msodbcsql.h, use "DRIVER={ODBC Driver 11 for SQL Server}" in the connection string.  
  
## Components of the Microsoft ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Windows  
 The ODBC driver on Windows contains the following components:  
  
|Component|Description|  
|---------------|-----------------|  
|msodbcsql13.dll|The dynamic-link library (DLL) file that contains all of the driver's functionality. msodbcsql13.dll is installed in %SYSTEMROOT%\System32.|  
|msodbcsqlr13.rll|The accompanying resource file for the driver library. msodbcsqlr13.rll is installed in %SYSTEMROOT%\System32\1033.|  
|s13ch_msodbcsql.chm|The Data Source Wizard help file that documents how to create a data source from the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)]. s13ch_msodbcsql.chm is installed in %SYSTEMROOT%\System32\1033|  
|msodbcsql.h|The header file that contains all of the new definitions needed to use the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Windows.<br /><br /> **Note:**  You cannot reference msodbcsql.h and odbcss.h in the same program.<br /><br /> msodbcsql.h is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\SDK.|  
|msodbcsql13.lib|The library file needed to call the **bcp** utility functions that are part of the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Windows.<br /><br /> **Note:**  If you do reference msodbcsql13.lib in your program, make sure that msodbcsql13.dll is in your system path and in the system path of those that use the application.<br /><br /> msodbcsql13.lib is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\130\SDK.|  
  
## Components of the Microsoft ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Windows  
 The ODBC driver on Windows contains the following components:  
  
|Component|Description|  
|---------------|-----------------|  
|msodbcsql11.dll|The dynamic-link library (DLL) file that contains all of the driver's functionality. msodbcsql11.dll is installed in %SYSTEMROOT%\System32.|  
|msodbcsqlr11.rll|The accompanying resource file for the driver library. msodbcsqlr11.rll is installed in %SYSTEMROOT%\System32\1033.|  
|s11ch_msodbcsql.chm|The Data Source Wizard help file that documents how to create a data source from the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)]. s11ch_msodbcsql.chm is installed in %SYSTEMROOT%\System32\1033|  
|msodbcsql.h|The header file that contains all of the new definitions needed to use the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Windows.<br /><br /> **Note:**  You cannot reference msodbcsql.h and odbcss.h in the same program.<br /><br /> msodbcsql.h is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\SDK.|  
|msodbcsql11.lib|The library file needed to call the **bcp** utility functions that are part of the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Windows.<br /><br /> **Note:**  If you do reference msodbcsql11.lib in your program, make sure that msodbcsql11.dll is in your system path and in the system path of those that use the application.<br /><br /> msodbcsql11.lib is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\SDK.|  
  
## See Also  
 [Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)  
  
  