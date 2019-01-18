---
title: "XML Input File Reference (Database Engine Tuning Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Database Engine Tuning Advisor [SQL Server], XML input files"
  - "input file reference [Database Engine Tuning Advisor]"
  - "XML input files [Database Engine Tuning Advisor]"
ms.assetid: 05e5e5f0-d6df-4336-b18e-e9bc2835a766
author: stevestein
ms.author: sstein
manager: craigg
---
# XML Input File Reference (Database Engine Tuning Advisor)
  [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor can use an XML input file to tune a database. This XML file designates which databases, tables, workload files or tables, and tuning options to use for the tuning session. You can also use this file to specify a user-specified configuration to perform "what-if" analysis.  
  
 A [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor XML input file contains a hierarchy of XML elements, each containing text or other elements that specify the tuning session settings. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor XML input file must conform to the standards for well-formed XML, so all element names are case sensitive. Elements are specified using Pascal case, which means that the first character is uppercase and the first letter of any subsequent concatenated word is uppercase.  
  
 All element values must conform to XML naming conventions. For more information about these conventions, see [XML Textual Content](https://go.microsoft.com/fwlink/?LinkId=7614) in the MSDN Library.  
  
 Note that this reference is not comprehensive. For information about all the elements you can use to define XML input, refer to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor XML schema, DTASchema.xsd.  
  
## XML Declaration  
  
-   [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)  
  
## DTAXML Root Element  
  
-   [DTAXML Element &#40;DTA&#41;](dtaxml-element-dta.md)  
  
## DTAInput Elements  
  
-   [DTAInput Element &#40;DTA&#41;](dtainput-element-dta.md)  
  
-   [Server Element &#40;DTA&#41;](server-element-dta.md)  
  
-   [Workload Element &#40;DTA&#41;](workload-element-dta.md)  
  
-   [TuningOptions Element &#40;DTA&#41;](tuningoptions-element-dta.md)  
  
-   [Configuration Element &#40;DTA&#41;](configuration-element-dta.md)  
  
## Server Elements  
  
-   [Name Element for Server &#40;DTA&#41;](name-element-for-server-dta.md)  
  
-   [Database Element for Server &#40;DTA&#41;](database-element-for-server-dta.md)  
  
## Workload Elements  
  
-   [File Element &#40;DTA&#41;](file-element-dta.md)  
  
-   [Database Element for Workload &#40;DTA&#41;](database-element-for-workload-dta.md)  
  
-   [EventString Element &#40;DTA&#41;](eventstring-element-dta.md)  
  
## Tuning Options Elements  
  
-   [TuningTimeInMin Element &#40;DTA&#41;](tuningtimeinmin-element-dta.md)  
  
-   [StorageBoundInMB Element &#40;DTA&#41;](storageboundinmb-element-dta.md)  
  
-   [TestServer Element &#40;DTA&#41;](testserver-element-dta.md)  
  
-   [FeatureSet Element &#40;DTA&#41;](featureset-element-dta.md)  
  
-   [Partitioning Element &#40;DTA&#41;](partitioning-element-dta.md)  
  
-   [DropOnlyMode Element &#40;DTA&#41;](droponlymode-element-dta.md)  
  
-   [KeepExisting Element &#40;DTA&#41;](keepexisting-element-dta.md)  
  
-   [OnlineIndexOperation Element &#40;DTA&#41;](onlineindexoperation-element-dta.md)  
  
-   [DatabaseToConnect Element &#40;DTA&#41;](databasetoconnect-element-dta.md)  
  
## Configuration Elements  
  
-   [Server Element for Configuration &#40;DTA&#41;](server-element-for-configuration-dta.md)  
  
-   [Database Element for Configuration &#40;DTA&#41;](database-element-for-configuration-dta.md)  
  
-   [Recommendation Element &#40;DTA&#41;](recommendation-element-dta.md)  
  
-   [Create Element &#40;DTA&#41;](create-element-dta.md)  
  
-   [Index Element &#40;DTA&#41;](index-element-dta.md)  
  
-   [Name Element for Index &#40;DTA&#41;](name-element-for-index-dta.md)  
  
-   [Column Element for Index &#40;DTA&#41;](column-element-for-index-dta.md)  
  
-   [Name Element for Column &#40;DTA&#41;](name-element-for-column-dta.md)  
  
-   [Filegroup Element for Index &#40;DTA&#41;](filegroup-element-for-index-dta.md)  
  
## Database Elements  
  
-   [Name Element for Database &#40;DTA&#41;](name-element-for-database-dta.md)  
  
-   [Schema Element for Database &#40;DTA&#41;](schema-element-for-database-dta.md)  
  
-   [Name Element for Schema &#40;DTA&#41;](name-element-for-schema-dta.md)  
  
-   [Table Element for Schema &#40;DTA&#41;](table-element-for-schema-dta.md)  
  
-   [Name Element for Table &#40;DTA&#41;](name-element-for-table-dta.md)  
  
## See Also  
 [Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md)  
  
  
