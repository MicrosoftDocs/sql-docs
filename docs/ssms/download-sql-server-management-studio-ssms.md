---
title: "Download SQL Server Management Studio (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "05/09/2018"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.service: ""
ms.component: "ssms"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: ssms
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
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../includes/appliesto-ss-asdb-asdw-xxx-md.md)]
SSMS is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. SSMS provides tools to configure, monitor, and administer instances of SQL. Use SSMS to deploy, monitor, and upgrade the data-tier components used by your applications, as well as build queries and scripts.

Use SQL Server Management Studio (SSMS) to query, design, and manage your databases and data warehouses, wherever they are - on your local computer, or in the cloud.

**SSMS is free!**

SSMS 17.x is the latest generation of *SQL Server Management Studio* and provides support for SQL Server 2017.

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.7](https://go.microsoft.com/fwlink/?linkid=870039)**

**[![download](../ssdt/media/download.png) Download SQL Server Management Studio 17.7 Upgrade Package (upgrades 17.x to 17.7)](https://go.microsoft.com/fwlink/?linkid=870041)**


**Version Information**

Release number: 17.7<br>
Build number: 14.0.17254.0<br>
Release date: May 09, 2018

The SSMS 17.x installation does not upgrade or replace SSMS versions 16.x or earlier. SSMS 17.x installs side by side with previous versions so both versions are available for use.
If a computer contains side by side installations of SSMS, verify you start the correct version for your specific needs. The latest version is labeled *Microsoft SQL Server Management Studio 17*, and has a new icon: 
 
   ![SSMS 17.x](media/download-sql-server-management-studio-ssms/version-icons.png)


## Available Languages

> [!NOTE]
> Non-English localized releases of SSMS require the [KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.


This release of SSMS can be installed in the following languages:

SQL Server Management Studio 17.7:<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=870039&clcid=0x40a)

SQL Server Management Studio 17.7 Upgrade Package (upgrades 17.x to 17.7):<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=870041&clcid=0x40a)

> [!NOTE]
> The SQL Server PowerShell module is now a separate install through the PowerShell Gallery. For more information, see [Download SQL Server PowerShell Module](download-sql-server-ps-module.md).
## SQL Server Management Studio


## New in this Release

SSMS 17.7 is the latest version of SQL Server Management Studio. The 17.x generation of SSMS provides support for almost all feature areas on SQL Server 2008 through SQL Server 2017. Version 17.x also supports SQL Analysis Service PaaS.

Version 17.7 includes:

**General SSMS**

Replication Monitor:   
- Replication monitor now supports registering a listener for scenarios where publisher database and/or distributor database is part of Availability Group. You can now monitor replication environments where publisher database and/or distribution database is part of Always On. 
 
Azure SQL Data Warehouse: 
- Add Rejected Row Location support for External Tables in Azure SQL Data Warehouse. 

**Integration Services (IS)**

- Added a scheduling feature for SSIS packages deployed to Azure SQL Database. Unlike SQL Server on premises and SQL Database Managed Instance (Preview), which have SQL Server Agent as a first-class job scheduler, SQL Database does not have a built-in scheduler. This new SSMS feature provides a familiar user interface that's similar to SQL Server Agent for scheduling packages deployed to SQL Database. If you're using SQL Database to host the SSIS catalog database, SSISDB, you can use this SSMS feature to generate the Data Factory pipelines, activities, and triggers required to schedule SSIS packages. You can then edit and extend these objects in Data Factory. For more info, see [Schedule SSIS package execution on Azure SQL Database with SSMS](../integration-services/lift-shift/ssis-azure-schedule-packages-ssms). To learn more about Azure Data Factory pipelines, activities, and triggers, see [Pipelines and activities in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/concepts-pipelines-activities) and [Pipeline execution and triggers in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/concepts-pipeline-execution-triggers).
- Support for SSIS package scheduling in SQL Agent on SQL Managed instance. It is now possible to create SQL Agent jobs to execute SSIS packages on the managed instance. 


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
  * The noted actions are typically required only once. There are few cases where a reboot is required during additional upgrades to the same major version of SSMS. For minor upgrades, all the prerequirements for SSMS are already be installed on the computer.


## Release Notes

The following are issues and limitations with this 17.7 release:

Some dialogs display an invalid edition error when working with new *General Purpose* or *Business Critical* Azure SQL Database editions.


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