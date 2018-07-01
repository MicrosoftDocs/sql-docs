---
title: "Download SQL Server Management Studio (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/26/2018"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.component: "ssms"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: ssms
ms.tgt_pltfrm: ""
ms.topic: conceptual
keywords: 
  - "install ssms, download ssms, latest ssms"
  - "SQL Server Management Studio"
  - "ssms.exe"
  - "sql man studio"
  - "sql management studio"
  - "sql management studio install"
  - "download sql management studio"
  - "ms sql management studio"
  - "install sql management studio"
  - "ssms download"
  - "sql server ssms"
  - "ssms express"
ms.assetid: adafeeef-4255-4924-8042-02f503d599ca
caps.latest.revision: 145
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Download SQL Server Management Studio (SSMS)
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../includes/appliesto-ss-asdb-asdw-xxx-md.md)]
SSMS is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. SSMS provides tools to configure, monitor, and administer instances of SQL. Use SSMS to deploy, monitor, and upgrade the data-tier components used by your applications, as well as build queries and scripts.

Use SQL Server Management Studio (SSMS) to query, design, and manage your databases and data warehouses, wherever they are - on your local computer, or in the cloud.

**SSMS is free!**

SSMS 17.x is the latest generation of *SQL Server Management Studio* and provides support for SQL Server 2017.

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.8.1](https://go.microsoft.com/fwlink/?linkid=875802)**

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.8.1 Upgrade Package (upgrades 17.x to 17.8.1)](https://go.microsoft.com/fwlink/?linkid=875804)**


**Version Information**

Release number: 17.8.1<br>
Build number: 14.0.17277.0<br>
Release date: June 26, 2018

The SSMS 17.x installation does not upgrade or replace SSMS versions 16.x or earlier. SSMS 17.x installs side by side with previous versions so both versions are available for use.
If a computer contains side by side installations of SSMS, verify you start the correct version for your specific needs. The latest version is labeled *Microsoft SQL Server Management Studio 17*, and has a new icon: 
 
   ![SSMS 17.x](media/download-sql-server-management-studio-ssms/version-icons.png)


## Available Languages

> [!NOTE]
> Non-English localized releases of SSMS require the [KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.


This release of SSMS can be installed in the following languages:

SQL Server Management Studio 17.8.1:<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=875802&clcid=0x40a)

SQL Server Management Studio 17.8.1 Upgrade Package (upgrades 17.x to 17.8.1):<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=875804&clcid=0x40a)

> [!NOTE]
> The SQL Server PowerShell module is now a separate install through the PowerShell Gallery. For more information, see [Download SQL Server PowerShell Module](download-sql-server-ps-module.md).

## SQL Server Management Studio


## New in this Release

SSMS 17.8.1 is the latest version of SQL Server Management Studio. The 17.x generation of SSMS provides support for almost all feature areas on SQL Server 2008 through SQL Server 2017. Version 17.x also supports SQL Analysis Service PaaS.

Version 17.8.1 includes:

**General SSMS**

Database Properties:

- This improvement exposes the "AUTOGROW_ALL_FILES" configuration option for Filegroups. This new config option is added under the Database Properties > Filegroups window in the form of a new column (Autogrow All Files) of checkboxes for each available Filegroup (except for Filestream and Memory Optimized Filegroups). The user can enable/disable AUTOGROW_ALL_FILES for a particular Filegroup by toggling the corresponding Autogrow_All_Files checkbox. Correspondingly, the AUTOGROW_ALL_FILES option is properly scripted when scripting the database for CREATE / generating scripts for the database (SQL2016 and above).
	
SQL Editor:

- Improved experience with Intellisense in Azure SQL Database when the user doesn't have master access.

Scripting:

- General performance improvements, especially over high-latency connections.
	
**Analysis Servics (AS)**

- Analysis Services client libraries and data providers updated to the latest version, which added support for the new Azure Government AAD authority (login.microsoftonline.us).


## Supported SQL offerings

* This version of SSMS works with all [supported versions of SQL Server 2008 - SQL Server 2017](https://support.microsoft.com/lifecycle?C2=1044) and provides the greatest level of support for working with the latest cloud features in Azure SQL Database and Azure SQL Data Warehouse.
* Use SSMS 17.x to connect to [SQL Server on Linux](../linux/sql-server-linux-overview.md).
* Additionally, SSMS 17.x can be installed side by side with SSMS 16.x or SQL Server 2014 SSMS and earlier.
* SQL Server Integration Services (SSIS) - SSMS version 17.x does not support connecting to the legacy SQL Server Integration Services service. To connect to an earlier version of the legacy Integration Services, use the version of SSMS aligned with the version of SQL Server. For example, use SSMS 16.x to connect to the legacy SQL Server 2016 Integration Services service. SSMS 17.x and SSMS 16.x can be installed side-by-side on the same computer. Since the release of SQL Server 2012, the SSIS Catalog database, SSISDB, is the recommended way to store, manage, run, and monitor Integration Services packages. For details, see [SSIS Catalog](../integration-services/catalog/ssis-catalog.md).

## Supported Operating systems
  
This release of SSMS supports the following 64-bit platforms when used with the latest available service pack:
- Windows 10 (64-bit)
- Windows 8.1 (64-bit)
- Windows 8 (64-bit)
- Windows 7 (SP1) (64-bit)
- Windows Server 2016 *
- Windows Server 2012 R2 (64-bit)
- Windows Server 2012 (64-bit)
- Windows Server 2008 R2 (64-bit)

\* SSMS 17.X is based on the Visual Studio 2015 Isolated shell, which was released before Windows Server 2016. Microsoft takes app compatibility seriously and ensures that already-shipped applications continue to run on the latest Windows releases. To minimize issues running SSMS on Windows Server 2016, ensure SSMS has all of the latest updates applied. If you experience any issues with SSMS on Windows Server 2016, contact support. The support team determines if the issue is with SSMS, Visual Studio, or with Windows compatibility. The support team then routes the issue to the appropriate team for further investigation.

## SSMS installation tips and issues

### Minimize Installation Reboots

* Take the following actions to reduce the chances of SSMS setup requiring a reboot at the end of installation:
  * Make sure you are running an up-to-date version of the Visual C++ 2013 Redistributable Package. Version 12.0.40649.5 (or greater) is required. Only the x64 version is needed.
  * Verify the version of .NET Framework on the computer is 4.6.1 (or greater).
  * Close any other instances of Visual Studio that are open on the computer.
  * Make sure all the latest OS updates are installed on the computer.
  * The noted actions are typically required only once. There are few cases where a reboot is required during additional upgrades to the same major version of SSMS. For minor upgrades, all the prerequirements for SSMS are already installed on the computer.


## Release Notes

The following are issues and limitations with this 17.8 release:

- Clicking the *Script* button after modifying any filegroup property in the *Properties* window, generates two scripts – one script with a *USE <database>* statement, and a second script with a *USE master* statement.  The script with *USE master* is generated in error and should be discarded. Run the script that contains the *USE <database>* statement.
- Some dialogs display an invalid edition error when working with new *General Purpose* or *Business Critical* Azure SQL Database editions.
- Some latency in XEvents viewer may be observed. This is a [known issue in the .Net Framework](https://github.com/Microsoft/dotnet/blob/master/releases/net472/dotnet472-changes.md#sql). Please, consider upgrading to NetFx 4.7.2.


## Uninstall and reinstall SSMS

If your SSMS installation is having problems, and a standard uninstall and reinstall doesn't resolve them, you can first try [repairing](https://support.microsoft.com/help/4028054/windows-10-repair-or-remove-programs) the Visual Studio 2015 IsoShell. If repairing the Visual Studio 2015 IsoShell doesn't resolve the problem, the following steps have been found to fix many random issues:

1.	Uninstall SSMS the same way you uninstall any application (using *Apps & features*, *Programs and features*, etc. depending on your version of Windows).

2.	Uninstall Visual Studio 2015 IsoShell **from an elevated cmd prompt**:
   
    ```PUSHD "C:\ProgramData\Package Cache\FE948F0DAB52EB8CB5A740A77D8934B9E1A8E301\redist"```

    ```vs_isoshell.exe /Uninstall /Force /PromptRestart```

3.	Uninstall Microsoft Visual C++ 2015 Redistributable the same way you uninstall any application. Uninstall both x86 and x64 if they're on your computer.

4.	Reinstall Visual Studio 2015 IsoShell **from an elevated cmd prompt**:  

    ```PUSHD "C:\ProgramData\Package Cache\FE948F0DAB52EB8CB5A740A77D8934B9E1A8E301\redist"```  
 
    ```vs_isoshell.exe /PromptRestart```

5.	Reinstall SSMS.

6.	Upgrade to the [latest version of the Visual C++ 2015 Redistributable](https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads) if you're not currently up to date.




## Previous releases

[Previous SQL Server Management Studio Releases](../ssms/sql-server-management-studio-changelog-ssms.md#previous-ssms-releases)

## Feedback

![needhelp_person_icon](../ssms/media/needhelp_person_icon.png) [SQL Client Tools Forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=sqltools)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

## See Also

- [Tutorial: SQL Server Management Studio](tutorials/tutorial-sql-server-management-studio.md)
- [SQL Server Management Studio documentation](sql-server-management-studio-ssms.md)
- [Additional updates and service packs](https://technet.microsoft.com/sqlserver/ff803383.aspx)
- [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
