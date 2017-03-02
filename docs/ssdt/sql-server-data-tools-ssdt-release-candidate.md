---
title: "SQL Server Data Tools (SSDT) - Release Candidate | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "03/01/2017"
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
Welcome to the current release candidate (17.0 RC2) of SQL Server Data Tools (SSDT)!  This release candidate of SQL Server Data Tools (SSDT) includes support for [SQL Server vNext](https://msdn.microsoft.com/library/mt788653.aspx). 


## Download
  
![download](../ssdt/media/download.png)[ Download SQL Server Data Tools 17.0 RC2](https://go.microsoft.com/fwlink/?linkid=837939). 


## Enhancements

**Database projects:**
- Amending a clustered index on a view will no longer block deployment.
- Schema comparison strings relating to column encryption will use the proper name rather than the instance name.   

**IS projects:**
- The SSIS OData Source and OData Connection Manager now support connecting to the OData feeds of Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online.
- SSIS project now supports target server version of "SQL Server vNext" 


**BI improvements:**

**AS projects:**
- SSAS Tabular Projects now introduce a preview of the 1400 compatibility mode, including a new data import and connectivity stack. 1400 mode is available in both Integrated workspace and when working against the SQL Server vNext CTP 1.1 Analysis Services Engine.
- SSAS Tabular Projects now support an additional set of Data Sources when in Direct Query mode.
- SSAS Tabular Projects targeting 1400 now expose HideMemberIf and Detail Row Expression, bringing ragged hierarchy and drill-through support to tabular models.
- Pasted Table experience has been enhanced in SSAS Tabular Projects and allows for larger table imports.
- SSAS Tabular Projects now allow for "Rely on Referential Integrity". This option can be enabled to tell the AS Engine that the underlying data source does have referential integrity (this modifies the type of query that AS can generate and therefore improves performance).

**RS projects:**
- Embeddable RVC Control is now available supporting SSRS 2016

## Bug fixes

**AS projects:**

- Tabular: A variety of enhancements and performance fixes for DAX parsing and the formula bar.
- Tabular: Tabular Model Explorer will no longer be visible if no SSAS Tabular projects are open.
- Multi-dimensional: Fixed an issue where the processing dialog was unusable on High-DPI machines.
- Tabular: Fixed an issue where SSDT faults when opening any BI project when SSMS is already open.[Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3100900/ssdt-faults-when-opening-any-bi-project-when-ssms-is-already-open)
- Tabular: Fixed an issue where hierarchies were not being properly saved to the bim file in an 1103 model.[Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3105222/vs-2015-ssdt)
- Tabular: Fixed an issue where Integrated Workspace mode was allowed on 32-bit machines even though it is not supported.
- Tabular: Fixed an issue where clicking on anything while in semi-select mode (typing a DAX expression but clicking a measure, for example) could cause crashes.
- Tabular: Fixed an issue where Deployment Wizard would reset the model's .Name property back to "Model". [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3107018/ssas-deployment-wizard-resets-modelname-to-model)
- Tabular: Fixed an issue where selecting a heirarchy in TME should display properties even if Diagram View is not selected.
- Tabular: Fixed an issue where pasting into the DAX Formula bar would paste images or other content instead of text when pasting from certain applications.
- Tabular: Fixed an issue where some old models in the 1103 couldn't be opened due to presence of measures with a specific definition.
- Tabular: Fixed an issue where XEvent Sessions could not be deleted.

**RS projects:**

- Fixed an issue where Save should save the version of RDL, not the latest version.
- Fixed an issue where SSDT RS is backing up files when backup is turned off and several other issues.
- Fixed an issue in Report Builder where an error would be shown when clicking "Split Cells". [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3101818/ssdt-2015-ssrs-designer-error-by-matrix-cell-split)
- Fixed an issue where caching could cause incorrect data in a report. [Connect Item](http://connect.microsoft.com/SQLServer/feedback/details/3102158/ssdtbi-14-0-60812-report-preview-data-is-frequently-wrong-due-to-bad-caching)

**IS projects:**
- Fixed an issue that run64bitruntime setting doesn't stick.
- Fixed an issue that DataViewer doesn't save displayed columns.
- Fixed an issue that Package Parts hides annotations. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3106624/package-parts-hide-annotations)
- Fixed an issue that Pacakage Parts discards Data Flow layouts and annotations. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3109241/package-parts-discard-data-flow-layouts-and-annotations)
- Fixed an issue that SSDT crashes when importing project from sql server.

## Known Issues
- In IS projects, CDC Control Task, CDC Source and CDC Splitter are not available when Target Server Version is set to "SQL Server vNext". Switch to other target server version (for example, SQL Server 2016), then design and execute packages containing these components.
- In AS projects, when you edit the metadata for a structured data source in Tabular 1400 models, you need to update the data source credentials afterwards. If you change the data source name, you must also update all the table queries to refer to the data source with the new name and then “ProcessAll” will work. 
- In As projects, the period character is not allowed in Data Source names in 1400 mode. In some cases, such as when using a fully qualified domain name (FQDN) for your SQL Server when creating a 1400 mode data source, the auto-generated name will include periods. You must modify the data source name manually in the .bim file before importing tables from the data source. If you use the "Import From Data Source" option instead of the "Add / New Data Source" options, data previews may not work in the query editor window, and you will have to update both the Data Source and the Table Partition Queries with updated names. 


## See Also  
[SQL Server Data Tools in Visual Studio](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)  
[SSDT Team Blog](http://blogs.msdn.com/b/ssdt/)  
[SSDT Connect Feedback](https://connect.microsoft.com/SQLServer/Feedback)  
[SSDT Documentation](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)  
[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)  
