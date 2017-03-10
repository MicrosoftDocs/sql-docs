---
title: "SQL Server Data Tools (SSDT) - Release Candidate | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "03/09/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssdt"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 13e47d2d-a441-4962-87ec-1c08973bb9e8
caps.latest.revision: 2
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# SQL Server Data Tools (SSDT) - Release Candidate

Welcome to the current release candidate (17.0 RC3) of SQL Server Data Tools (SSDT)!  This release candidate of SQL Server Data Tools (SSDT) includes support for [SQL Server vNext](https://msdn.microsoft.com/library/mt788653.aspx). 


## Download

SSDT release candidate 17.0 RC3 is not recommended for production use. For production use, it is recommended to continue to use the [generally available release &#40;16.x&#41;](../ssdt/download-sql-server-data-tools-ssdt.md)
  
![download](../ssdt/media/download.png)[ Download SQL Server Data Tools 17.0 RC3](https://go.microsoft.com/fwlink/?linkid=842523).  

## Enhancements

**Database projects:**
- Added a new command line option to SqlPackage: ModelFilePath.  This provides an option for advanced users to specify an external model.xml file for import, publishing and scripting operations.   

**IS projects:**
- Support for CDC Control Task, CDC Splitter and CDC Source when targeting SQL Server vNext. 

**BI improvements:**

**AS projects:**
- Performance improvements in AS integrated workspace when committing edits to tabular models (calculated columns, measures, importing tables, etc.)
- General improvements and fixes for preview 1400 compat-level models, PowerQuery integration 

**RS projects:**
- Embeddable RVC Control is now available supporting SSRS 2016

## Bug fixes
- Fixed the template priority for BI Projects so they don’t show up at the top of the New Projects categories in VS
- Fixed a VS crash that may occur in rare circumstances when SSIS, SSAS or SSRS solution opened

**AS projects:**

- Fixed an issue with attempting to build AS “smproj” files with devenv.com would fail
- Fixed an issue that was finalizing text changes too frequently when using the Korean IME in AS tabular model sheet tab titles
- Fixed an issue where the intellisense for DAX Related() function was not working correctly to show columns from other tables
- Improved AS Tabular project import from database dialog by sorting the list of AS databases
- Fixed an issue when creating calculated tables in AS tabular model where Tables weren’t listed as suggested objects in the expression
- Fixed an issue in preview 1400 AS models trying to open using Integrated Workspace server after viewing code
- Fixed an issue that was preventing some data sources (with no support for initial catalog) from working correctly in certain circumstances 
- Deployment Wizard should apply changes to calculated table partitions even when the option to keep partitions is enabled
- Fixed an issue where Advanced Properties dialog to existing AS Connection didn’t show full list until reselected 

**RS projects:**

- Fixed an issue when designing reports in SSDT the tree view of parameters, data sources and datasets would collapse when most changes made 

**IS projects:**
- Fix an issue that "Tabular Model Explorer" menu shows when creating an Integration Service Project

**Database projects:**
- SSDT DACPAC deploy add setting back in for IgnoreColumnOrder [Connect item](https://connect.microsoft.com/SQLServer/feedback/details/1221587/ssdt-dacpac-deploy-add-setting-back-in-for-ignorecolumnorder)
- SSDT failing to compile if STRING_SPLIT is used [Connect item](http://connect.microsoft.com/SQLServer/feedback/details/2906200/ssdt-failing-to-compile-if-string-split-is-used)
- Fix issue where DeploymentContributors have access to the public model but the backing schema has not been initialized [Github issue](https://github.com/Microsoft/DACExtensions/issues/8)
- DacFx temporal fix for FILEGROUP placement
- Fix for "Unresolved Reference" error for external synonyms. 
- Always Encrypted: Online encryption does not disable change tracking on cancellation and does not work properly if change tracking has not been cleaned prior to start encryption

## See Also  
[SQL Server Data Tools in Visual Studio](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)  
[SSDT Team Blog](http://blogs.msdn.com/b/ssdt/)  
[SSDT Connect Feedback](https://connect.microsoft.com/SQLServer/Feedback)  
[SSDT Documentation](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)  
[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)  