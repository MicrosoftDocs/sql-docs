---
title: "Download SQL Server Management Studio (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "12/14/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "sql-tools"
ms.service: ""
ms.component: "ssms"
ms.reviewer: ""
ms.suite: "sql"
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
ms.workload: "Active"
---
# Download SQL Server Management Studio (SSMS)
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../includes/appliesto-ss-asdb-asdw-xxx-md.md)]
SSMS is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. SSMS provides tools to configure, monitor, and administer instances of SQL. Use SSMS to deploy, monitor, and upgrade the data-tier components used by your applications, as well as build queries and scripts.

Use SQL Server Management Studio (SSMS) to query, design, and manage your databases and data warehouses, wherever they are - on your local computer, or in the cloud.

**SSMS is free!**

SSMS 17.x is the latest generation of *SQL Server Management Studio* and provides support for SQL Server 2017.

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.4](https://go.microsoft.com/fwlink/?linkid=864329)**

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.4 Upgrade Package (upgrades 17.x to 17.4)](https://go.microsoft.com/fwlink/?linkid=864331)**

The SSMS 17.x installation does not upgrade or replace SSMS versions 16.x or earlier. SSMS 17.x installs side by side with previous versions so both versions are available for use.
If a computer contains side by side installations of SSMS, verify you start the correct version for your specific needs. The latest version is labeled *Microsoft SQL Server Management Studio 17*, and has a new icon: 
 
   ![SSMS 17.x](media/download-sql-server-management-studio-ssms/version-icons.png)


## Available Languages

> [!NOTE]
> Non-English localized releases of SSMS require the [KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.


This release of SSMS can be installed in the following languages:

SQL Server Management Studio 17.4:<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=864329&clcid=0x40a)

SQL Server Management Studio 17.4 Upgrade Package (upgrades 17.x to 17.4):<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=864331&clcid=0x40a)

> [!NOTE]
> The SQL Server PowerShell module is now a separate install through the PowerShell Gallery. For more information, see [Download SQL Server PowerShell Module](download-sql-server-ps-module.md).
## SQL Server Management Studio

**Version Information**

The release number: 17.4

The build number for this release: 14.0.17213.0

The release date: December 7, 2017

## New in this Release

SSMS 17.4 is the latest version of SQL Server Management Studio. The 17.x generation of SSMS provides support for almost all feature areas on SQL Server 2008 through SQL Server 2017. Version 17.x also supports SQL Analysis Service PaaS.

Version 17.4 includes:

Vulnerability Assessment:
- Added a new SQL Vulnerability Assessment service to scan your databases for potential vulnerabilities and deviations from best practices, such as misconfigurations, excessive permissions, and exposed sensitive data. 
- Results of the assessment include actionable steps to resolve each issue and customized remediation scripts where applicable. The assessment report can be customized for each environment and tailored to specific requirements. Learn more at [SQL Vulnerability Assessment](https://docs.microsoft.com/sql/relational-databases/security/sql-vulnerability-assessment).

SMO:
- Fixed issue where *HasMemoryOptimizedObjects* was throwing exception on Azure.
- Added support for new CATALOG_COLLATION feature.

Always On Dashboard:
- Improvements for latency analysis in Availability Groups.
- Added two new reports: *AlwaysOn\_Latency\_Primary* and *AlwaysOn\_Latency\_Secondary*.

Showplan:
- Updated links to point to correct documentation.
- Allow single plan analysis directly from actual plan produced.
- New set of icons.
- Added support for recognize "Apply logical operators" like GbApply, InnerApply.
		
XE Profiler:
- Renamed to XEvent Profiler.
- Stop/Start menu commands now stop/start the session by default.
- Enabled keyboard shortcuts (for example, CTRL-F to search).
- Added database\_name and client\_hostname actions to appropriate events in XEvent Profiler sessions. For the change to take effect, you may need to delete existing QuickSessionStandard or QuickSessionTSQL session instances on the servers - [Connect 3142981](https://connect.microsoft.com/SQLServer/feedback/details/3142981)

Command line:
- Added a new command line option ("-G") that can be used to automatically have SSMS connect to a server/database using Active Directory Authentication (either 'Integrated' or 'Password'). For details, see [Ssms utility](ssms-utility.md).

Import Flat File Wizard:
- Added a way to pick a schema name other than the default ("dbo") when creating the table.

Query Store:
- Restored the "Regressed Queries" report when expanding the Query Store available reports list.

**Integration Services (IS)**
- Added package validation function in Deployment Wizard, which helps the user figure out components inside SSIS packages that are not supported in Azure-SSIS IR.


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


## Release Notes

The following are issues and limitations with this 17.4 release:

There are no known issues at this time.

## Previous releases

[Previous SQL Server Management Studio Releases](../ssms/sql-server-management-studio-changelog-ssms.md#previous-ssms-releases)

## Feedback

![needhelp_person_icon](../ssms/media/needhelp_person_icon.png) [SQL Client Tools Forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=sqltools) |  [Log an issue or suggestion at Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback)




## See Also

- [Tutorial: SQL Server Management Studio](tutorials/tutorial-sql-server-management-studio.md)
- [SQL Server Management Studio documentation](sql-server-management-studio-ssms.md)
- [Additional updates and service packs](https://technet.microsoft.com/sqlserver/ff803383.aspx)
- [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)
