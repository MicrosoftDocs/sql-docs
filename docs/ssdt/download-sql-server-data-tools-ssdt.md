---
title: "Download SQL Server Data Tools (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "06/01/2018"
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

For most users, SQL Server Data Tools (SSDT) is installed as part of Visual Studio. 

## Install SSDT with Visual Studio 2017

To install SSDT during [Visual Studio installation](https://docs.microsoft.com/visualstudio/install/install-visual-studio), select the **Data storage and processing** workload, and then select **SQL Server Data Tools**. If Visual Studio is already installed, you can [edit the list of workloads](https://docs.microsoft.com/visualstudio/install/modify-visual-studio) to include SSDT:
![Data storage and processing workload](../ssdt/media/download-sql-server-data-tools-ssdt/data-workload.png)



## Install Analysis Services, Integration Services, and Reporting Services tools
To install AS, IS, and RS project support, run the [SSDT services installer](#ssdt-for-visual-studio-2017). 

The installer lists available Visual Studio instances to add the SSDT services to. If Visual Studio is not installed, selecting **Install a new SQL Server Data Tools ... instance** installs SSDT with a minimal version of Visual Studio, but for the best experience we recommend using SSDT with [the latest version of Visual Studio](https://www.visualstudio.com/downloads). 

![select AS, IS, RS](../ssdt/media/download-sql-server-data-tools-ssdt/select-services.png)



## SSDT for Visual Studio 2017
Install Analysis Services (AS), Integration Services (IS), and Reporting Services (RS) tools.

[![download](../ssdt/media/download.png) Download SSDT for Visual Studio 2017 (15.7.0) ](https://go.microsoft.com/fwlink/?linkid=874716) 

> [!IMPORTANT]
> Before installing SSDT for Visual Studio 2017 (15.7.0), uninstall "Microsoft Analysis Services Projects" and "Microsoft Reporting Services Projects" extensions if they are already installed, and close all VS instances. 



**Version Information**  
  
Release number: 15.7.0  
Build number: 14.0.16165.0  
Release date: June 1, 2018  

For a complete list of changes, see the [changelog](changelog-for-sql-server-data-tools-ssdt.md).

SSDT for Visual Studio 2017 has the same [system requirements](https://docs.microsoft.com/visualstudio/productinfo/vs2017-system-requirements-vs) as Visual Studio.  

### Available Languages - SSDT for VS 2017

This release of **SSDT for VS 2017** can be installed in the following languages:  

[Chinese (People's Republic of China)]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x804) | 
[Chinese (Taiwan)]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x404) | 
[English (United States)]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x409) | 
[French]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x40c)  
[German]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x407) | 
[Italian]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x410) | 
[Japanese]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x411) | 
[Korean]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x412) | 
[Portuguese (Brazil)]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x416) | 
[Russian]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x419) | 
[Spanish]( https://go.microsoft.com/fwlink/?linkid=874716&clcid=0x40a)  



## SSDT for Visual Studio 2015

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

- [Project-Oriented Offline Database Development](https://msdn.microsoft.com/library/hh272702(v=vs.103).aspx)  
- [SSIS Tutorial: Create a Simple ETL Package](../integration-services/ssis-how-to-create-an-etl-package.md)  
- [Analysis Services tutorials](../analysis-services/analysis-services-tutorials-ssas.md)  
- [Create a Basic Table Report (SSRS Tutorial)](../reporting-services/create-a-basic-table-report-ssrs-tutorial.md)  

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]


## See Also  
[SQL Server Data Tools in Visual Studio](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)  
[SSDT Team Blog](http://blogs.msdn.com/b/ssdt/)  
[SSDT Documentation](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)  
[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)  
