---
title: "Download SQL Server Data Tools (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "09/21/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssdt"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: 
  - "install ssdt, download ssdt, latest ssdt"
ms.assetid: b0fc4987-d260-4d0a-9dd1-98099835b361
caps.latest.revision: 113
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Download SQL Server Data Tools (SSDT)

**[SQL Server Data Tools](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)** is a modern development tool that you can download for free to build SQL Server relational databases, Azure SQL databases, Integration Services packages, Analysis Services data models, and Reporting Services reports. With SSDT, you can design and deploy any SQL Server content type with the same ease as you would develop an application in Visual Studio. 

The preview version of SSDT for Visual Studio 2017 (15.3.0 preview) is now available. This release introduces a standalone web installation experience for SQL Server Database, Analysis Services, Reporting Services, and Integration Services projects in Visual Studio 2017 15.3 or later.

| SSDT for Visual Studio 2017 (preview) | SSDT for Visual Studio 2015 | 
|:--|:--|
|[![download](../ssdt/media/download.png) Download SSDT for Visual Studio 2017 (15.3.0 preview) ](https://go.microsoft.com/fwlink/?linkid=853836) | [![download](../ssdt/media/download.png) Download SSDT for Visual Studio 2015 (17.3)](https://go.microsoft.com/fwlink/?linkid=852922)|
|||

> [!IMPORTANT]
> Before installing SSDT for Visual Studio 2017 (preview), close all VS instances, and uninstall SSAS and SSRS if they are already installed on VS 2017.
> 


SSDT for Visual Studio 2015, and SSDT for Visual Studio 2017 both use DacFx 17.3: [Download Data-Tier Application Framework (DacFx) 17.3](https://www.microsoft.com/download/details.aspx?id=55713)



## SSDT for Visual Studio 2017
**Version Information**  
  
The release number: 15.3.0 preview???  
The build number for this release: ???14.0.16121.0

For a complete list of changes, see the [changelog](changelog-for-sql-server-data-tools-ssdt.md).

SSDT for Visual Studio 2017 has the same system requirements as installing VS, supported operating systems are Windows 7 SP1, Windows 8.1 or Windows Server 2012 R2, Windows 10 or Windows Server 2016.  

### Available Languages - SSDT for VS 2017
  
 This preview release of SSDT is currently available in English only.



## SSDT for Visual Studio 2015
**Version Information**  
  
The release number: 17.3  
The build number for this release: NEEDED
  
For a complete list of changes, see the [changelog](changelog-for-sql-server-data-tools-ssdt.md).

### Available Languages - SSDT for VS 2015
  
 This release of SSDT can be installed in the following languages:  
[Chinese (People's Republic of China)]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x804) | 
[Chinese (Taiwan)]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x404) | 
[English (United States)]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x409) | 
[French]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x40c)  
[German]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x407) | 
[Italian]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x410) | 
[Japanese]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x411) | 
[Korean]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x412) | 
[Portuguese (Brazil)]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x416) | 
[Russian]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x419) | 
[Spanish]( https://go.microsoft.com/fwlink/?linkid=852922&clcid=0x40a)  

### ISO Images - SSDT for VS 2015

An ISO image of SSDT can be used as an alternative way to install SSDT or to set up an Administrative Installation point. The ISO is a self-contained file that contains all of the components needed by SSDT and it can be downloaded using a restartable download manager, useful for situations with limited or less reliable network bandwidth. Once downloaded, the ISO can be mounted as a drive or burned to a DVD.

[Chinese (People's Republic of China)]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x804) |
[Chinese (Taiwan)]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x404) |
[English (United States)]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x409) |
[French]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x40c)  
[German]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x407) |
[Italian]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x410) |
[Japanese]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x411) |
[Korean]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x412) |
[Portuguese (Brazil)]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x416) |
[Russian]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x419) |
[Spanish]( https://go.microsoft.com/fwlink/?linkid=852942&clcid=0x40a)


## Download Visual Studio

* [**Download Visual Studio**](https://www.visualstudio.com/downloads)

## Installing SSDT without Visual Studio pre-installed

If you don't have Visual Studio installed on your machine, installing SSDT for Visual Studio will install a minimal version of Visual Studio. This version of Visual Studio is free to install and use on as many machines as you wish. It gives you all the SQL Server project types, plus *SQL Server Object Explorer* and other SQL tools experiences.

If you already have Visual Studio 2015 (or above) installed, installing SSDT will add the full set of SQL Server tools into your existing Visual Studio installation. Visual Studio includes many features you might want to use, such as Source Code Control integration and non-SQL language support. We recommend using Visual Studio 2015 or above to get the best experience when developing T-SQL.


## Supported SQL versions
  
|Project Templates|SQL Platforms Supported|  
|-------------------|--------------------|  
Relational databases|  SQL Server 2005* - SQL Server 2017 <br /><br />Azure SQL Database<br /><br />Azure SQL Data Warehouse (supports queries only; database projects are not yet supported)<br /><br />  * SQL Server 2005 support is deprecated,<br /><br /> please move to an officially supported SQL version|
  |Analysis Services models<br /><br />Reporting Services reports | SQL Server 2008 – SQL Server 2017|
  |Integration Services packages| SQL Server 2012 – SQL Server 2017    |
  
## Next steps  
After installing SSDT, work through these tutorials to learn how to create databases, packages, data models, and reports using SSDT:  
  
-   [Project-Oriented Offline Database Development](https://msdn.microsoft.com/library/hh272702(v=vs.103).aspx)  
  
-   [SSIS Tutorial: Create a Simple ETL Package](/sql-docs/docs/integration-services/ssis-how-to-create-an-etl-package)  
  
-   [Analysis Services tutorials](/sql-docs/docs/analysis-services/analysis-services-tutorials-ssas)  
  
-   [Create a Basic Table Report (SSRS Tutorial)](/sql-docs/docs/reporting-services/create-a-basic-table-report-ssrs-tutorial)  
  



## See Also  
[SQL Server Data Tools in Visual Studio](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)  
[SSDT Team Blog](http://blogs.msdn.com/b/ssdt/)  
[SSDT Connect Feedback](https://connect.microsoft.com/SQLServer/Feedback)  
[SSDT Documentation](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)  
[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)  
