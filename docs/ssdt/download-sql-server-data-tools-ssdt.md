---
title: "Download SQL Server Data Tools (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "03/10/2017"
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
manager: "jhubbard"
---
# Download SQL Server Data Tools (SSDT)

**[SQL Server Data Tools](https://msdn.microsoft.com/mt186501)** is a modern development tool that you can download for free to build SQL Server relational databases, Azure SQL databases, Integration Services packages, Analysis Services data models, and Reporting Services reports. With SSDT, you can design and deploy any SQL Server content type with the same ease as you would develop an application in Visual Studio. This release supports SQL Server 2017 through SQL Server 2005, and provides the design environment for adding features that are new in SQL Server 2016.  
    
    
![download](../ssdt/media/download.png) [Download SQL Server Data Tools 17.1 for Visual Studio 2015](https://go.microsoft.com/fwlink/?linkid=849393)

![download](../ssdt/media/download.png) [Download Data-Tier Application Framework (DacFx) 17.1](https://www.microsoft.com/download/details.aspx?id=55255)

## SQL Server Data Tools   
**Version Information**  
  
The release number: 17.1  
The build number for this release: 14.0.61705.170
  
 **What's New**
 - Offline support for AS non-model-related IntelliSense (e.g. highlighting, statement completion, and parameter information)
 - Addition to Tabular Model Explorer to display M expressions
 - Azure Active Directory People Picker for configuring Role Members in Tabular models
 - Support for encoding hints in UI when defining 1400 models
 - Several AS project bug fixes
 - Several DacFx bug fixes

 **Known Issues**
 - When creating a new data source in a 1400-compatibility level AS model, if you select a file-based data source and cancel press cancel before creating the data source, the tabular editor (Model.bim) becomes read-only. You can workaround this issue by simply closing the tabular editor and reopening it from Solution Explorer.

Complete list of changes available in the [changelog](changelog-for-sql-server-data-tools-ssdt.md)

 > To use SQL Server Data Tools in Visual Studio 2017 see [this](#use-ssdt-in-visual-studio-2017) section below

  **Available Languages**  
  
 This release of SSDT can be installed in the following languages:  
[Chinese (People's Republic of China)]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x804) | 
[Chinese (Taiwan)]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x404) | 
[English (United States)]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x409) | 
[French]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x40c)  
[German]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x407) | 
[Italian]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x410) | 
[Japanese]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x411) | 
[Korean]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x412) | 
[Portuguese (Brazil)]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x416) | 
[Russian]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x419) | 
[Spanish]( https://go.microsoft.com/fwlink/?linkid=849393&clcid=0x40a)  

**ISO Images**

An ISO image of SSDT can be used as an alternative way to install SSDT or to set up an Administrative Installation point. The ISO is a self-contained file that contains all of the components needed by SSDT and it can be downloaded using a restartable download manager, useful for situations with limited or less reliable network bandwidth. Once downloaded, the ISO can be mounted as a drive or burned to a DVD.

[Chinese (People's Republic of China)]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x804) |
[Chinese (Taiwan)]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x404) |
[English (United States)]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x409) |
[French]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x40c)  
[German]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x407) |
[Italian]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x410) |
[Japanese]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x411) |
[Korean]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x412) |
[Portuguese (Brazil)]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x416) |
[Russian]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x419) |
[Spanish]( https://go.microsoft.com/fwlink/?linkid=849399&clcid=0x40a)

## Download Visual Studio

* [**Download Visual Studio Community 2015**](https://www.visualstudio.com/products/visual-studio-community-vs.aspx)

## Installing SSDT without Visual Studio pre-installed

If you don't have Visual Studio installed on your machine, installing SSDT for Visual Studio 2015 will also install a minimal “Integrated Shell” version of Visual Studio 2015. This version of Visual Studio is free to install and use on as many machines as you wish. It gives you all the SQL Server project types, plus SQL Server Object Explorer and other SQL tools experiences.

If you do have [Visual Studio 2015 Community Edition (or above)](https://www.visualstudio.com/products/visual-studio-community-vs.aspx) installed on your machine, installing SSDT will add the full set of SQL Server tools into your existing Visual Studio installation. Visual Studio includes many features you might want to use, such as Source Code Control integration and non-SQL language support. We recommend using Visual Studio 2015 Community or above to get the best experience when developing T-SQL.

## Supported SQL versions
  
|Project Templates|SQL Platforms Supported|  
|-------------------|--------------------|  
Relational databases|  SQL Server 2005* - SQL Server 2017 <br /><br />Azure SQL Database<br /><br />Azure SQL Data Warehouse (supports queries only; database projects are not yet supported)<br /><br />  * SQL Server 2005 support is deprecated,<br /><br /> please move to an officially supported SQL version|
  |Analysis Services models<br /><br />Reporting Services reports | SQL Server 2008 – SQL Server 2017|
  |Integration Services packages| SQL Server 2012 – SQL Server 2017    |
  
## Next steps  
After installing SSDT, work through these tutorials to learn how to create databases, packages, data models, and reports using SSDT:  
  
-   [Project-Oriented Offline Database Development](https://msdn.microsoft.com/library/hh272702(v=vs.103).aspx)  
  
-   [SSIS Tutorial: Create a Simple ETL Package](https://msdn.microsoft.com/library/ms169917.aspx)  
  
-   [Analysis Services tutorials](https://msdn.microsoft.com/library/hh231701.aspx)  
  
-   [Create a Basic Table Report (SSRS Tutorial)](https://msdn.microsoft.com/library/ms167305.aspx)  
  
## Use SSDT in Visual Studio 2017 

* [**Download Visual Studio 2017**](https://www.visualstudio.com/) ([compare Visual Studio 2017 features by edition](https://www.visualstudio.com/vs/compare/))

To use relational database projects, we recommend checking the **Data Storage and Processing** workload during installation. SSDT database project support is also included in a number of other workloads including *Azure*, *ASP.Net and Web Development*, and *.Net Core Cross Platform Development*.

> [!NOTE]
> SSDT database projects in Visual Studio 2017 currently support up to SQL Server 2016.  Support for SQL Server 2017 will be coming soon in a Visual Studio 2017 update.

If you are using SSDT with Visual Studio 2017, install the AS and RS components:
* [Analysis Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects)
* [Reporting Services](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio)


## See Also  
[SQL Server Data Tools in Visual Studio](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)  
[SSDT Team Blog](http://blogs.msdn.com/b/ssdt/)  
[SSDT Connect Feedback](https://connect.microsoft.com/SQLServer/Feedback)  
[SSDT Documentation](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)  
[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)  
