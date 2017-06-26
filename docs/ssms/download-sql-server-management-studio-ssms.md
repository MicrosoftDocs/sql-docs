---
title: "Download SQL Server Management Studio (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "05/30/2017"
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
SQL Server Management Studio (SSMS) is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. SSMS provides tools to configure, monitor, and administer instances of SQL from wherever you deploy it. With SSMS you can deploy, monitor, and upgrade the data-tier components used by your applications, as well as build queries and scripts. 

This release features improved compatibility with previous versions of SQL Server, a stand-alone web installer, and toast notifications within SSMS when new releases become available.  

SSMS is free! Download it below!
    
![download](../ssdt/media/download.png) **[Download SQL Server Management Studio 17.1](https://go.microsoft.com/fwlink/?linkid=849819)**  SMS 17.X is the latest generation of SQL Server Management Studio and provides support for SQL Server 2017. 

![download](../ssdt/media/download.png) **[Download SQL Server Management Studio 17.1 Upgrade Package (upgrades 17.0 to 17.1)](https://go.microsoft.com/fwlink/?linkid=849821)**

> [!NOTE]
> The SQL Server PowerShell module is now a separate install through the PowerShell Gallery.  Please see the [download instructions](download-sql-server-ps-module.md) for more information.

## SQL Server Management Studio   
**Version Information**  
  
The release number: 17.1  
The build number for this release: 14.0.17119.0

## New in this Release  

SSMS 17.1 is first update to the 17.X generation of SQL Server Management Studio.  The 17.X generation provides support for almost all feature areas on SQL Server 2008 through SQL Server 2017.  Version 17.X is also the generation of SSMS that supports SQL Analysis Service PaaS.

Version 17.1 includes:

* Fixes for several user reported issues 
* A new Integration Services scale-out management tool

For the full list of changes, see   
                [SQL Server Management Studio - Changelog (SSMS)](../ssms/sql-server-management-studio-changelog-ssms.md)  
   
For information about user data collection, see   
                [SQL Server Privacy Statement](http://www.microsoft.com/privacystatement/en-us/SQLServer/Default.aspx) 
  
## Supported SQL offerings
  
* This version of SSMS works with all [supported versions of SQL Server (SQL Server 2008 - SQL Server 2017)](https://support.microsoft.com/en-us/lifecycle?C2=1044) and provides the greatest level of support for working with the latest cloud features in Azure SQL Database, and Azure SQL Data Warehouse.  
* There is no explicit block for SQL Server 2000 or SQL Server 2005, but some features may not work properly.  
* Additionally, SSMS 17.X can be installed side-by-side with SSMS 16.X or SQL Server 2014 SSMS and earlier. 
  
## Supported Operating systems
  
This release of SSMS supports the following platforms when used with the latest available service pack:   
- Windows 10
- Windows 8.1
- Windows 8
- Windows 7 (SP1)
- Windows Server 2016
- Windows Server 2012 (64-bit) 
- Windows Server 2012 R2 (64-bit) 
- Windows Server 2008 R2 (64-bit)  

>[!NOTE]
>SSMS 17.X is based on the Visual Studio 2015 Isolated shell, which was released before Windows Server 2016. Microsoft takes app compatibility very seriously and ensures that already-shipped applications continue to run on the latest Windows releases. Because of this, we do not anticipate that SSMS with all latest updates applied) will encounter issues when running on Windows Server 2016. Customers are advised to contact support, should they encounter any issues with SSMS on Windows Server 2016. Support will then work with customers to determine if the issue is with SSMS or Visual Studio or with Windows compatibility, and route the issue appropriately.

## SSMS installation tips and issues
**Minimize Installation Reboots**

- You can reduce the chances of SSMS setup requiring a reboot at the end by making by taking the following actions:
  - Make sure you are running an up-to-date version of the Visual C++ 2013 Redistributable Package. You’ll need version 12.00.40649.5 (or greater). Only the x64 version is needed.
  - Make sure the version of .NET Framework on the machine is 4.6.1 (or greater)
  - Close any other version Visual Studio that may be running on the machine
  - Make sure all the latest OS updates are installed on the machine
  - The noted actions should only be required once. The likelihood of reboots needed while doing addtiional upgrades to newer versions of SSMS (same major version) is very limited, since all the prerequirements for SSMS are already on the machine.

- To see the list of known issues and work arounds, see [SQL Server Management Studio -  Release Notes](../ssms/sql-server-management-studio-release-notes.md)

## Available Languages  
> Non-English localized releases of SSMS require the [KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2. 
  
This release of SSMS can be installed in the following languages:

SQL Server Management Studio 17.1:<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=849819&clcid=0x40a)

SQL Server Management Studio 17.1 Upgrade Package (upgrades 17.0 to 17.1):<br>
[Chinese (People's Republic of China)](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x804) | [Chinese (Taiwan)](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=849821&clcid=0x40a)

## Previous releases  
[Previous SQL Server Management Studio Releases](../ssms/previous-sql-server-management-studio-releases.md)  
  
## Feedback  
  
![needhelp_person_icon](../ssms/media/needhelp_person_icon.png) [SQL Client Tools Forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=sqltools) |  [Log an issue or suggestion at Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback)  
  
## See Also  
[Tutorial: SQL Server Management Studio](http://msdn.microsoft.com/en-us/d2bade70-07cf-4d94-b5d2-88aecb538ed1)  
[SQL Server Management Studio documentation](https://msdn.microsoft.com/library/hh213248(v=sql.130).aspx)  
[Microsoft SQL Server](https://msdn.microsoft.com/library/bb545450.aspx)  
[Additional updates and service packs](https://technet.microsoft.com/sqlserver/ff803383.aspx)  
[Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)  


