---
title: "Download SQL Server Management Studio (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "09/28/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
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
manager: "craigg"
---
# Download SQL Server Management Studio (SSMS)

SSMS is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. SSMS provides tools to configure, monitor, and administer instances of SQL. Use SSMS to deploy, monitor, and upgrade the data-tier components used by your applications, as well as build queries and scripts.

Use SQL Server Management Studio (SSMS) to query, design, and manage your databases and data warehouses, wherever they are - on your local computer, or in the cloud.

**SSMS is free!**

SSMS 17.x is the latest generation of *SQL Server Management Studio* and provides support for SQL Server 2017.

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.3](https://go.microsoft.com/fwlink/?linkid=858904)**

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.3 Upgrade Package (upgrades 17.x to 17.3)](https://go.microsoft.com/fwlink/?linkid=858906)**

The SSMS 17.x installation does not upgrade or replace SSMS versions 16.x or earlier. SSMS 17.x installs side by side with previous versions so both versions are available for use.
If a computer contains side by side installations of SSMS, verify you start the correct version for your specific needs. The latest version is labeled *Microsoft SQL Server Management Studio 17*, and has a new icon: 
 
   ![SSMS 17.x](media/download-sql-server-management-studio-ssms/version-icons.png)


> [!NOTE]
> The SQL Server PowerShell module is now a separate install through the PowerShell Gallery.  For more information, see [download instructions](download-sql-server-ps-module.md).

## SQL Server Management Studio

**Version Information**

The release number: 17.3
The build number for this release: NEEDED

## New in this Release

SSMS 17.3 is the latest version of SQL Server Management Studio. The 17.x generation of SSMS provides support for almost all feature areas on SQL Server 2008 through SQL Server 2017. Version 17.x also supports SQL Analysis Service PaaS.

Version 17.3 includes:

- New "Import Flat File" wizard added to streamline the import experience of CSV files with an intelligent framework, requiring minimal user intervention or specialized domain knowledge.
- Added "XEvent Profiler" node to Object Explorer.
- Updated waits filtering and categorization in Performance Dashboard historical waits report.
- Added the syntax check of the "Predict" function.
- Added the syntax check of the External Library Management queries.
- Added SMO support for External Library Management.
- Added "Start PowerShell" support to "Registered Servers" window (requires a new SQL PowerShell module).
- Always On: added [read-only routing support](../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md) for availability groups.
- Added an option to send tracing details to the Output Window for "Active Directory - Universal with MFA support" logins (off by default; needs to be turned on in user settings under "Tools > Options > Azure Services > Azure Cloud > ADAL Output Window Trace Level"). 
- Query Store: 
  - Query Store UI will be accessible even when QDS is OFF as long as QDS have recorded any data.
  - Query Store UI now exposes waits categorization in all the existing reports. This will let customers unlock the scenarios of Top Waiting Queries and many more.
- Made inclusion of the scripting parameters headers optional (off by default;  can be enabled in user settings under "Tools > Options > SQL Server Object Explorer > Scripting > Include scripting parameters header") - [Connect item 3139199](https://connect.microsoft.com/SQLServer/feedback/details/3139199).
- Removed "RC" branding.

For the full list of changes, see [SQL Server Management Studio - Changelog (SSMS)](../ssms/sql-server-management-studio-changelog-ssms.md).

For information about user data collection, see [SQL Server Privacy Statement](http://www.microsoft.com/privacystatement/en-us/SQLServer/Default.aspx).

## Supported SQL offerings

* This version of SSMS works with all [supported versions of SQL Server 2008 - SQL Server 2017](https://support.microsoft.com/lifecycle?C2=1044) and provides the greatest level of support for working with the latest cloud features in Azure SQL Database and Azure SQL Data Warehouse.
* There is no explicit block for SQL Server 2000 or SQL Server 2005, but some features may not work properly.
* Additionally, SSMS 17.x can be installed side by side with SSMS 16.x or SQL Server 2014 SSMS and earlier.

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
  * Make sure you are running an up-to-date version of the Visual C++ 2013 Redistributable Package. Version 12.00.40649.5 (or greater) is required. Only the x64 version is needed.
  * Verify the version of .NET Framework on the computer is 4.6.1 (or greater).
  * Close any other instances of Visual Studio that are open on the computer.
  * Make sure all the latest OS updates are installed on the computer.
  * The noted actions are typically required only once. There are few cases where a reboot is required during additional upgrades to the same major version of SSMS. For minor upgrades, all the prerequirements for SSMS are already be installed on the computer.

* To see the list of known issues and work-arounds, see [SQL Server Management Studio -  Release Notes](../ssms/sql-server-management-studio-release-notes.md)

## Available Languages

> [!NOTE]
> Non-English localized releases of SSMS require the [KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.

This release of SSMS can be installed in the following languages:

SQL Server Management Studio 17.3:<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=858904&clcid=0x40a)

SQL Server Management Studio 17.3 Upgrade Package (upgrades 17.x to 17.3):<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=858906&clcid=0x40a)

## Release Notes

The following are issues and limitations with this 17.3 release:

**General SSMS**

- The following SSMS functionality is not supported for Azure AD auth using UA with MFA:
   - Database Engine Tuning Advisor is not supported for Azure AD auth; there is a known issue where the error message presented to the user is a bit cryptic "Could not load file or assembly 'Microsoft.IdentityModel.Clients.ActiveDirectory,…" instead of the expected "Database Engine Tuning Advisor does not support Microsoft Azure SQL Database. (DTAClient)".
- Trying to analyze a query in DTA results in an error: "Object must implement IConvertible. (mscorlib)".


**Integration Services (IS)**

- The [execution_path] in [catalog].[event_messagea] is not correct for package executions in Scale Out. The [execution_path] starts with “\Package” instead of the object name of the package executable. When viewing the overview report of package executions in SSMS, the link of “Execution Path” in Execution Overview cannot work. The workaround is to click “View Messages” on overview report to check all event messages.



## Previous releases

[Previous SQL Server Management Studio Releases](../ssms/sql-server-management-studio-changelog-ssms.md#previous-ssms-releases)

## Feedback

![needhelp_person_icon](../ssms/media/needhelp_person_icon.png) [SQL Client Tools Forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=sqltools) |  [Log an issue or suggestion at Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback)

## See Also

- [Tutorial: SQL Server Management Studio](tutorials/tutorial-sql-server-management-studio.md)
- [SQL Server Management Studio documentation](sql-server-management-studio-ssms.md)
- [Additional updates and service packs](https://technet.microsoft.com/sqlserver/ff803383.aspx)
- [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)
