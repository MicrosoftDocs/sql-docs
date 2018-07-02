---
title: "Download SQL Server Data Tools (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "07/02/2018"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.component: "ssdt"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: ssdt
ms.tgt_pltfrm: ""
ms.topic: conceptual
keywords: 
  - "install ssdt, download ssdt, latest ssdt"
ms.assetid: b0fc4987-d260-4d0a-9dd1-98099835b361
caps.latest.revision: 113
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Download and install SQL Server Data Tools (SSDT) for Visual Studio
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md.md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
**SQL Server Data Tools** is a modern development tool for building SQL Server relational databases, Azure SQL databases, Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports. With SSDT, you can design and deploy any SQL Server content type with the same ease as you would develop an application in Visual Studio.

*For most users, SQL Server Data Tools (SSDT) is installed during Visual Studio installation. Installing SSDT using the Visual Studio installer adds the base SSDT functionality, so you still need to run the [SSDT standalone installer](#ssdt-for-vs-2017-standalone-installer) to get AS, IS, and RS tools.*

## Install SSDT with Visual Studio 2017

To install SSDT during [Visual Studio installation](https://docs.microsoft.com/visualstudio/install/install-visual-studio), select the **Data storage and processing** workload, and then select **SQL Server Data Tools**. If Visual Studio is already installed, you can [edit the list of workloads](https://docs.microsoft.com/visualstudio/install/modify-visual-studio) to include SSDT:
![Data storage and processing workload](../ssdt/media/download-sql-server-data-tools-ssdt/data-workload.png)



## Install Analysis Services, Integration Services, and Reporting Services tools
To install AS, IS, and RS project support, run the [SSDT standalone installer](#ssdt-for-vs-2017-standalone-installer). 

The installer lists available Visual Studio instances to add the SSDT tools to. If Visual Studio is not installed, selecting **Install a new SQL Server Data Tools instance** installs SSDT with a minimal version of Visual Studio, but for the best experience we recommend using SSDT with [the latest version of Visual Studio](https://www.visualstudio.com/downloads). 

![select AS, IS, RS](../ssdt/media/download-sql-server-data-tools-ssdt/select-services.png)



## SSDT for VS 2017 (standalone installer)

[![download](../ssdt/media/download.png) Download SSDT for Visual Studio 2017 (15.7.1) ](https://go.microsoft.com/fwlink/?linkid=875613) 

> [!IMPORTANT]
> - Before installing SSDT for Visual Studio 2017 (15.7.1), uninstall *Analysis Services Projects* and *Reporting Services Projects* extensions if they are already installed, and close all VS instances.
> - When installing SSDT on Windows 10 and choosing **Install new SQL Server Data Tools for Visual Studio 2017 instance**, please clear any checkbox and install the new instance first. After the new instance is installed, please reboot the computer and open the SSDT installer again to continue the installation.  



**Version Information**  
  
Release number: 15.7.1  
Build number: 14.0.16167.0  
Release date: July 02, 2018  

For a complete list of changes, see the [changelog](changelog-for-sql-server-data-tools-ssdt.md).

SSDT for Visual Studio 2017 has the same [system requirements](https://docs.microsoft.com/visualstudio/productinfo/vs2017-system-requirements-vs) as Visual Studio.  

### Available Languages - SSDT for VS 2017

This release of **SSDT for VS 2017** can be installed in the following languages:  

[Chinese (People's Republic of China)]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x804) | 
[Chinese (Taiwan)]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x404) | 
[English (United States)]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x409) | 
[French]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x40c)  
[German]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x407) | 
[Italian]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x410) | 
[Japanese]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x411) | 
[Korean]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x412) | 
[Portuguese (Brazil)]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x416) | 
[Russian]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x419) | 
[Spanish]( https://go.microsoft.com/fwlink/?linkid=875613&clcid=0x40a)  



## SSDT for VS 2015 (standalone installer)

[![download](../ssdt/media/download.png) Download SSDT for Visual Studio 2015 (17.4)](https://go.microsoft.com/fwlink/?linkid=863440)

**Version Information**  
  
The release number: 17.4

The build number for this release: 14.0.61712.050
  
For a complete list of changes, see the [changelog](changelog-for-sql-server-data-tools-ssdt.md).

### Available Languages - SSDT for VS 2015
  
This release of **SSDT for VS 2015** can be installed in the following languages:  

[Chinese (People's Republic of China)]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x804) | 
[Chinese (Taiwan)]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x404) | 
[English (United States)]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x409) | 
[French]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x40c)  
[German]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x407) | 
[Italian]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x410) | 
[Japanese]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x411) | 
[Korean]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x412) | 
[Portuguese (Brazil)]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x416) | 
[Russian]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x419) | 
[Spanish]( https://go.microsoft.com/fwlink/?linkid=863440&clcid=0x40a)  

### ISO Images - SSDT for VS 2015

An ISO image of SSDT can be used as an alternative way to install SSDT or to set up an Administrative Installation point. The ISO is a self-contained file that contains all of the components needed by SSDT and it can be downloaded using a restartable download manager, useful for situations with limited or less reliable network bandwidth. Once downloaded, the ISO can be mounted as a drive or burned to a DVD.

> [!NOTE]
> The SSDT for VS 2015 17.4 ISO images are now available.

[Chinese (People's Republic of China)]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x804) |
[Chinese (Taiwan)]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x404) |
[English (United States)]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x409) |
[French]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x40c)  
[German]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x407) |
[Italian]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x410) |
[Japanese]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x411) |
[Korean]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x412) |
[Portuguese (Brazil)]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x416) |
[Russian]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x419) |
[Spanish]( https://go.microsoft.com/fwlink/?linkid=863443&clcid=0x40a)



## Supported SQL versions
  
|Project Templates|SQL Platforms Supported|  
|-------------------|--------------------|  
Relational databases|  SQL Server 2005* - SQL Server 2017<br> (use SSDT 17.x or SSDT for Visual Studio 2017 to connect to [SQL Server on Linux](../linux/sql-server-linux-overview.md))<br /><br />Azure SQL Database<br /><br />Azure SQL Data Warehouse (supports queries only; database projects are not yet supported)<br /><br />  * SQL Server 2005 support is deprecated,<br /><br /> please move to an officially supported SQL version|
  |Analysis Services models<br /><br />Reporting Services reports | SQL Server 2008 – SQL Server 2017|
  |Integration Services packages| SQL Server 2012 – SQL Server 2017    |
  
## DacFx
SSDT for Visual Studio 2015, and SSDT for Visual Studio 2017 both use DacFx 17.4.1: [Download Data-Tier Application Framework (DacFx) 17.4.1](https://www.microsoft.com/download/details.aspx?id=56508).


## Next steps  
After installing SSDT, work through these tutorials to learn how to create databases, packages, data models, and reports using SSDT:  

- [Project-Oriented Offline Database Development](project-oriented-offline-database-development.md)  
- [SSIS Tutorial: Create a Simple ETL Package](../integration-services/ssis-how-to-create-an-etl-package.md)  
- [Analysis Services tutorials](../analysis-services/analysis-services-tutorials-ssas.md)  
- [Create a Basic Table Report (SSRS Tutorial)](../reporting-services/create-a-basic-table-report-ssrs-tutorial.md)  

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]


## See Also  
[SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)  
[SSDT Team Blog](http://blogs.msdn.com/b/ssdt/)  
[DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)  
[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)  