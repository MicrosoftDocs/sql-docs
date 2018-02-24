---
title: "Download SQL Server Management Studio (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "02/21/2018"
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

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.5](https://go.microsoft.com/fwlink/?linkid=867670)**

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.5 Upgrade Package (upgrades 17.x to 17.5)](https://go.microsoft.com/fwlink/?linkid=867672)**

The SSMS 17.x installation does not upgrade or replace SSMS versions 16.x or earlier. SSMS 17.x installs side by side with previous versions so both versions are available for use.
If a computer contains side by side installations of SSMS, verify you start the correct version for your specific needs. The latest version is labeled *Microsoft SQL Server Management Studio 17*, and has a new icon: 
 
   ![SSMS 17.x](media/download-sql-server-management-studio-ssms/version-icons.png)


## Available Languages

> [!NOTE]
> Non-English localized releases of SSMS require the [KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.


This release of SSMS can be installed in the following languages:

SQL Server Management Studio 17.5:<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=867670&clcid=0x40a)

SQL Server Management Studio 17.5 Upgrade Package (upgrades 17.x to 17.5):<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=867672&clcid=0x40a)

> [!NOTE]
> The SQL Server PowerShell module is now a separate install through the PowerShell Gallery. For more information, see [Download SQL Server PowerShell Module](download-sql-server-ps-module.md).
## SQL Server Management Studio

**Version Information**

Release number: 17.5

Build number for this release: 14.0.17224.0

Release date: February 15, 2018

## New in this Release

SSMS 17.5 is the latest version of SQL Server Management Studio. The 17.x generation of SSMS provides support for almost all feature areas on SQL Server 2008 through SQL Server 2017. Version 17.x also supports SQL Analysis Service PaaS.

Version 17.5 includes:

Data Discovery & Classification:

- Added a new SQL Data Discovery & Classification feature for discovering, classifying, labeling & reporting sensitive data in your databases. 
- Auto-discovering and classifying your most sensitive data (business, financial, healthcare, PII, etc.) can play a pivotal role in your organizational information protection stature.
- Learn more at [SQL Data Discovery & Classification](../relational-databases/security/sql-data-discovery-and-classification.md).

Query Editor:

- Added support for SkipRows option to the Delimited Text External File Format for Azure SQL DW. This capability allows users to skip a specified number of rows when loading delimited text files into SQL DW. Also added the corresponding intellisense/SMO support for the FIRST_ROW keyword. 

Showplan:

- Enabled display of estimated plan button for SQL Data Warehouse
- Added new showplan attribute *EstimateRowsWithoutRowGoal*; and added new showplan attributes to *QueryTimeStats*: *UdfCpuTime* and *UdfElapsedTime*. For more information, see [Optimizer row goal information in query execution plan added in SQL Server 2017 CU3](http://support.microsoft.com/help/4051361).



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
  * Make sure you are running an up-to-date version of the Visual C++ 2013 Redistributable Package. Version 12.00.40649.5 (or greater) is required. Only the x64 version is needed.
  * Verify the version of .NET Framework on the computer is 4.6.1 (or greater).
  * Close any other instances of Visual Studio that are open on the computer.
  * Make sure all the latest OS updates are installed on the computer.
  * The noted actions are typically required only once. There are few cases where a reboot is required during additional upgrades to the same major version of SSMS. For minor upgrades, all the prerequirements for SSMS are already be installed on the computer.


## Release Notes

The following are issues and limitations with this 17.5 release:

Data classification:
- Removing a classification and then manually adding a new classification for the same column results in the old information type and sensitivity label being assigned to the column in the main view.<br>
*Workaround*: Assign the new information type and sensitivity label after the classification was added back to the main view and before saving.  


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
