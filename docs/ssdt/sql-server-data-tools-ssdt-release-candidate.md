---
title: "SQL Server Data Tools (SSDT) - Release Candidate | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/30/2017"
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
---
# SQL Server Data Tools (SSDT) - Release Candidate
Welcome to the current release candidate (17.0 RC2) of SQL Server Data Tools (SSDT)!  This release candidate of SQL Server Data Tools (SSDT) includes support for [SQL Server vNext](https://msdn.microsoft.com/library/mt788653.aspx). 


## Download

SSDT release candidate 17.0 RC2 works side-by-side with our [generally available release &#40;16.x&#41;](../ssdt/download-sql-server-data-tools-ssdt.md), but is not recommended for production use. 
  
![download](../ssdt/media/download.png)[ Download SQL Server Data Tools 17.0 RC2](https://go.microsoft.com/fwlink/?LinkID=835150).  

![download](../ssdt/media/download.png)[ Download DacFx and SqlPackage.exe 17.0 RC2](https://go.microsoft.com/fwlink/?LinkID=835693).  


  
## Release notes

**SSDT:**
- The SSIS OData Source and OData Connection Manager now support connecting to the OData feeds of Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online.
- Amending a clustered index on a view will no longer block deployment.
- Schema comparison strings relating to column encryption will use the proper name rather than the instance name.   


**BI improvements:**

**AS:**
- 17.0 RC 2: SSAS Tabular Projects now introduce a preview of the 1400 compatibility mode, including a new data import and connectivity stack. 1400 mode is available in both Integrated workspace and when working against the SQL Server vNext CTP 1.1 Analysis Services Engine.
- 17.0 RC 2: SSAS Tabular Projects now support an additional set of Data Sources when in Direct Query mode.
- 17.0 RC 2: SSAS Tabular Projects targeting 1400 now expose HideMemberIf and Detail Row Expression, bringing ragged hierarchy and drill-through support to tabular models.
- 17.0 RC 2: Pasted Table experience has been enhanced in SSAS Tabular Projects and allows for larger table imports.
- 17.0 RC 1: SSAS Tabular Projects now allow for "Rely on Referential Integrity". This option can be enabled to tell the AS Engine that the underlying data source does have referential integrity (this modifies the type of query that AS can generate and therefore improves performance).

**RS:**
- 17.0 RC 2: Embeddable RVC Control is now available supporting SSRS 2016

## Bug fixes

### RC2

**SSAS**

- SSDT for SSAS Tabular: A variety of enhancements and performance fixes for DAX parsing and the formula bar. 8831395
- SSDT for SSAS Tabular: Tabular Model Explorer will no longer be visible if no SSAS Tabular projects are open. 794265 
- SSDT for SSAS Multi-dimensional: Fixed an issue where the processing dialog was unusable on High-DPI machines. 8917467 
- SSDT for SSAS Tabular: Fixed an issue where SSDT faults when opening any BI project when SSMS is already open. 8311959 [Link](http://connect.microsoft.com/SQLServer/feedback/details/3100900/ssdt-faults-when-opening-any-bi-project-when-ssms-is-already-open)
- SSDT for SSAS Tabular: Fixed an issue where hierarchies were not being properly saved to the bim file in an 1103 model. 8512622 [Link](http://connect.microsoft.com/SQLServer/feedback/details/3105222/vs-2015-ssdt)
- SSDT for SSAS Tabular: Fixed an issue where Integrated Workspace mode was allowed on 32-bit machines even though it is not supported. 8493802 
- SSDT for SSAS Tabular: Fixed an issue where clicking on anything while in semi-select mode (typing a DAX expression but clicking a measure, for example) could cause crashes. 8746619 
- SSDT for SSAS Tabular: Fixed an issue where Deployment Wizard would reset the model's .Name property back to "Model". 8715823 [Link](http://connect.microsoft.com/SQLServer/feedback/details/3107018/ssas-deployment-wizard-resets-modelname-to-model)
- SSDT for SSAS Tabular: Fixed an issue where selecting a heirarchy in TME should display properties even if Diagram View is not selected. 8506201 

**SSRS**

- SSDT for SSRS: Fixed an issue where Save should save the version of RDL, not the latest version. 8846304 
- SSDT for SSRS: Fixed an issue where SSDT RS is backing up files when backup is turned off and several other issues. 8860617 

### RC1

**SSAS**

- SSDT for SSAS Tabular: Fixed an issue where pasting into the DAX Formula bar would paste images or other content instead of text when pasting from certain applications. 8688752 
- SSDT for SSAS Tabular: Fixed an issue where some old models in the 1103 couldn't be opened due to presence of measures with a specific definition. 8142135 
- SSMS for SSAS Tabular: Fixed an issue where XEvent Sessions could not be deleted. 8563313 

**SSRS**

- SSDT for SSRS: Fixed an issue in Report Builder where an error would be shown when clicking "Split Cells". 8341630 [Link](http://connect.microsoft.com/SQLServer/feedback/details/3101818/ssdt-2015-ssrs-designer-error-by-matrix-cell-split)
- SSDT for SSRS: Fixed an issue where caching could cause incorrect data in a report. 8350942 [Link](http://connect.microsoft.com/SQLServer/feedback/details/3102158/ssdtbi-14-0-60812-report-preview-data-is-frequently-wrong-due-to-bad-caching)

## See Also  
[SQL Server Data Tools in Visual Studio](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)  
[SSDT Team Blog](http://blogs.msdn.com/b/ssdt/)  
[SSDT Connect Feedback](https://connect.microsoft.com/SQLServer/Feedback)  
[SSDT Documentation](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)  
[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)  
