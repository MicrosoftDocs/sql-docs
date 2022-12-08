---
title: System Requirements, Installation, and Driver Files
description: This article describes the system requirements for the Microsoft ODBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# System requirements, installation, and driver files

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article discusses the ODBC drivers that connect to SQL Server.

## SQL version compatibility

Compatibility indicates that a driver was tested for compatibility against existing releases of SQL at the time of the driver's release. SQL Server releases generally try to maintain backwards compatibility with existing client drivers. But new features in SQL Server releases may not be available with older client drivers.

|Database version&nbsp;&#8594;<br />&#8595; Driver Version|Azure SQL Database|Azure Synapse Analytics|Azure SQL Managed Instance|SQL Server 2019|SQL Server 2017|SQL Server 2016|SQL Server 2014|SQL Server 2012|SQL Server 2008 R2|SQL Server 2008|SQL Server 2005|
|----|---|---|---|---|---|---|---|---|---|---|---|
|18.1|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |   |
|18.0|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|17.10|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|17.9|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|17.8|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|17.7|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|17.6|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|17.5|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|17.4|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|17.3|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |
|17.2|Yes|Yes|Yes|   |Yes|Yes|Yes|Yes|Yes|Yes|   |
|17.1|Yes|Yes|Yes|   |Yes|Yes|Yes|Yes|Yes|Yes|   |
|17.0|Yes|Yes|Yes|   |Yes|Yes|Yes|Yes|Yes|Yes|   |
|13.1|   |   |   |   |Yes|Yes|Yes|Yes|Yes|Yes|   |
|13  |   |   |   |   |   |Yes|Yes|Yes|Yes|Yes|   |
|11  |   |   |   |   |   |   |Yes|Yes|Yes|Yes|Yes|

### Connection string details

The driver name that you specify in a connection string is one of the following:

- `ODBC Driver 11 for SQL Server`
- `ODBC Driver 13 for SQL Server` (for both 13 and 13.1)
- `ODBC Driver 17 for SQL Server`
- `ODBC Driver 18 for SQL Server`

## Supported operating systems

The following matrix indicates driver version support for Windows operating system versions:

|Operating system&nbsp;&#8594;<br />&#8595; Driver version|Windows Server 2022|Windows Server 2019|Windows Server 2016|Windows Server 2012 R2|Windows Server 2012|Windows Server 2008 R2|Windows 11|Windows 10|Windows 8.1|Windows 7|Windows Vista SP2|
|----|---|---|---|---|---|---|---|---|---|---|---|
|18.1|Yes|Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |   |
|18.0|Yes|Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |   |
|17.10|Yes|Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |   |
|17.9|Yes|Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |   |
|17.8|   |Yes|Yes|Yes|Yes|   |   |Yes|Yes|   |   |
|17.7|   |Yes|Yes|Yes|Yes|   |   |Yes|Yes|   |   |
|17.6|   |Yes|Yes|Yes|Yes|   |   |Yes|Yes|   |   |
|17.5|   |Yes|Yes|Yes|Yes|   |   |Yes|Yes|   |   |
|17.4|   |Yes|Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |
|17.3|   |Yes|Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |
|17.2|   |   |Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |
|17.1|   |   |Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |
|17.0|   |   |Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |
|13.1|   |   |Yes|Yes|Yes|Yes|   |Yes|Yes|Yes|   |
|13  |   |   |   |   |Yes|Yes|   |   |Yes|Yes|   |
|11  |   |   |   |   |Yes|Yes|   |   |   |Yes|Yes|

## Installing Microsoft ODBC Driver for SQL Server

The driver is installed when you run `msodbcsql.msi` from one of the [Downloads for Windows](../download-odbc-driver-for-sql-server.md#download-for-windows).

> [!NOTE]
> For those who have Driver 17.1.0.1 or below installed, it is recommended that it be uninstalled manually prior to installing the newer version of the Driver.

### Side-by-side with Native Client

The driver can be installed side-by-side with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. Major versions of the driver (11, 13, 17, 18) can all be installed side-by-side with each other, as well.

When you invoke `msodbcsql.msi`, only the client components are installed by default. The client components are files that support running an application that was developed using the driver. To install the SDK components, specify `ADDLOCAL=ALL` on the command line. Here is an example.
  
```console
msiexec /i msodbcsql.msi ADDLOCAL=ALL  
```  

### End-user license

Specify `IACCEPTMSODBCSQLLICENSETERMS=YES` to accept the terms of the end-user license if you use the `/passive`, `/qn`, `/qb`, or `/qr` option to install. This option must be specified in all uppercase letters. Here is an example.
  
```console
msiexec /quiet /passive /qn /i msodbcsql.msi IACCEPTMSODBCSQLLICENSETERMS=YES ADDLOCAL=ALL  
```  

### Silent uninstall

The following example shows how to perform a silent uninstall.
  
```console
msiexec /quiet /passive /qn /uninstall msodbcsql.msi  
```  

### Indicate dependency

When an application uses the driver, the application should indicate that it depends on the driver through the install option `APPGUID`. this indication enables the driver installer to report dependent applications before uninstalling. To specify a dependency on the driver, set the `APPGUID` command-line parameter to your product code when silently installing the driver. A product code must be created when using Microsoft Installer to bundle your application setup program. Here is an example.
  
```console
msiexec /i msodbcsql.msi APPGUID={ <Your dependent application's APPGUID> }  
```  

## Command-line tools: sqlcmd.exe and bcp.exe

The `bcp.exe` and `sqlcmd.exe` tools for use with the driver can be downloaded at [Microsoft Command Line Utilities 11 for SQL Server](https://www.microsoft.com/download/details.aspx?id=36433), [Microsoft Command Line Utilities 13 for SQL Server](https://www.microsoft.com/download/details.aspx?id=52680), or [Microsoft Command Line Utilities 13.1 for SQL Server](https://www.microsoft.com/download/details.aspx?id=53591). The driver is a prerequisite to install `sqlcmd.exe` and `bcp.exe`.
  
`bcp.exe` and `sqlcmd.exe` are installed in the `110\Tools` subfolder of `%PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC` for version 11, and `130\Tools` for 13 and 13.1.

An application that uses BCP functions must specify the driver from the same version which shipped with the header file and library used to compile the application.  

For example, when you compile an ODBC application with `msodbcsql11.lib` and `msodbcsql.h`, use "DRIVER={ODBC Driver 11 for SQL Server}" in the connection string.

## Components of the Microsoft ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows

The ODBC driver on Windows contains the following components:

| Component | Description |
| :-------- | :---------- |
|msodbcsql18.dll or <br/> msodbcsql17.dll or <br/> msodbcsql13.dll or <br/> msodbcsql11.dll|The dynamic-link library (DLL) file that contains all of the driver's functionality. This file is installed in %SYSTEMROOT%\System32.|  
|msodbcdiag18.dll or <br/> msodbcdiag17.dll or <br/> msodbcdiag13.dll or <br/> msodbcdiag11.dll|The dynamic-link library (DLL) file that contains the driver's diagnostics (tracing) interface. This file is installed in %SYSTEMROOT%\System32.|
|msodbcsqlr18.rll or <br/> msodbcsqlr17.rll or <br/> msodbcsqlr13.rll or <br/> msodbcsqlr11.rll|The accompanying resource file for the driver library. This file is installed in %SYSTEMROOT%\System32\1033.|
|s13ch_msodbcsql.chm or <br/> s11ch_msodbcsql.chm |The Data Source Wizard help file that documents how to create a data source for the driver. This file is installed in %SYSTEMROOT%\System32\1033 <br /> <br /> **NOTE:** There is no chm file for ODBC Driver 17 and above. |  
|msodbcsql.h|The header file that contains all of the new definitions needed to use the driver.<br /><br /> **Note:**  You cannot reference msodbcsql.h and odbcss.h in the same program.<br /><br /> msodbcsql.h for ODBC Driver 18 is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\180\SDK. <br /> msodbcsql.h for ODBC Driver 17 is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\170\SDK. <br /> msodbcsql.h for ODBC Driver 13 is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\130\SDK. <br /> msodbcsql.h for ODBC Driver 11 is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\SDK.|
|msodbcsql18.lib or <br/> msodbcsql17.lib or <br/> msodbcsql13.lib or <br/> msodbcsql11.lib|The library file needed to call the **bcp** utility functions that are part of the driver.<br /><br /> **Note:**  If you do reference this library file in your program, make sure that it is in your system path and in the system path of those that use the application.<br /><br /> msodbcsql18.lib is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\180\SDK.<br /> msodbcsql17.lib is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\170\SDK.<br /> msodbcsql13.lib is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\130\SDK.<br /> msodbcsql11.lib is installed in %PROGRAMFILES%\Microsoft SQL Server\Client SDK\ODBC\110\SDK.|

## See also

[Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)  
