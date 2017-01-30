---
title: "Download SQL Server Data Tools (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "01/30/2017"
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

**[SQL Server Data Tools](https://msdn.microsoft.com/mt186501)** is a modern development tool that you can download for free to build SQL Server relational databases, Azure SQL databases, Integration Services packages, Analysis Services data models, and Reporting Services reports. With SSDT, you can design and deploy any SQL Server content type with the same ease as you would develop an application in Visual Studio. This release supports SQL Server 2016 through SQL Server 2005, and provides the design environment for adding features that are new in SQL Server 2016.  
    
    
| ![download](../ssdt/media/download.png) Download SQL Server Data Tools for Visual Studio 2015  | Download Data-Tier Application Framework (DacFx) ||
|:---|:---|:---|
|**[Download SQL Server Data Tools (16.5)](https://msdn.microsoft.com/mt186501)**|[**Download DacFx and SqlPackage.exe (16.5)**](https://www.microsoft.com/download/details.aspx?id=54106) |Current GA release for production use.|
|[**Download SQL Server Data Tools - Release Candidate**](../ssdt/sql-server-data-tools-ssdt-release-candidate.md)| [**Download DacFx and SqlPackage.exe - Release Candidate**](../ssdt/sql-server-data-tools-ssdt-release-candidate.md) |Includes support for SQL Server vNext CTP1, but not recommended for production use.|



## SSDT works best with Visual Studio 2015

- #### [**Download Visual Studio Community 2015**](https://www.visualstudio.com/products/visual-studio-community-vs.aspx)



If you don't have Visual Studio installed on your machine, installing SSDT will also install a minimal “Integrated Shell” version of Visual Studio 2015. This version of Visual Studio is free to install and use on as many machines as you wish. It gives you all the SQL Server project types, plus SQL Server Object Explorer and other SQL tools experiences.

If you do have [Visual Studio 2015 Community Edition (or above)](https://www.visualstudio.com/products/visual-studio-community-vs.aspx) installed on your machine, installing SSDT will add the full set of SQL Server tools into your existing Visual Studio installation. Visual Studio includes many features you might want to use, such as Source Code Control integration and non-SQL language support. We recommend using Visual Studio 2015 Community or above to get the best experience when developing T-SQL.

- [Compare Visual Studio 2015 features by Edition](https://www.visualstudio.com/products/compare-visual-studio-2015-products-vs)

## Supported SQL versions
  
|Project Templates|SQL Platforms Supported|  
|-------------------|--------------------|  
Relational databases|  SQL Server 2005* - SQL Server 2016 <br /><br />Azure SQL Database<br /><br />Azure SQL Data Warehouse (supports queries only; database projects are not yet supported)<br /><br />  * SQL Server 2005 support is deprecated,<br /><br /> please move to an officially supported SQL version|
  |Analysis Services models<br /><br />Reporting Services reports | SQL Server 2008 – SQL Server 2016|
  |Integration Services packages| SQL Server 2012 – SQL Server 2016    |
  
## Next steps  
After installing SSDT, work through these tutorials to learn how to create databases, packages, data models, and reports using SSDT:  
  
-   [Project-Oriented Offline Database Development](https://msdn.microsoft.com/library/hh272702(v=vs.103).aspx)  
  
-   [SSIS Tutorial: Create a Simple ETL Package](https://msdn.microsoft.com/library/ms169917.aspx)  
  
-   [Analysis Services tutorials](https://msdn.microsoft.com/library/hh231701.aspx)  
  
-   [Create a Basic Table Report (SSRS Tutorial)](https://msdn.microsoft.com/library/ms167305.aspx)  
  
## See Also  
[SQL Server Data Tools in Visual Studio](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)  
[SSDT Team Blog](http://blogs.msdn.com/b/ssdt/)  
[SSDT Connect Feedback](https://connect.microsoft.com/SQLServer/Feedback)  
[SSDT Documentation](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)  
[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)  
