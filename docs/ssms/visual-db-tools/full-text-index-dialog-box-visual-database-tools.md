---
title: "Full-Text Index Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.fulltextindex"
ms.assetid: ef45b585-2567-4abe-b421-9fd0994e0146
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Full-Text Index Dialog Box (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
This dialog box allows you to create a full-text index, for full-text searches on text-based columns in your database tables. A full-text index relies on a regular index, so you must create that first. The regular index must be created on a single, non-null column; it is best to choose a column with small values rather than a column with large ones.  
  
> [!NOTE]
> To create a full-text index, you must first create a full-text catalog for the database using an outside tool, such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or Enterprise Manager.  
> 
> [!NOTE]
> Full-text index functionality is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2012](https://msdn.microsoft.com/5da61ff5-12b9-48e6-b3c8-0dacca1751c4).  
  
## Options  
**Selected Full-Text Index**  
Lists existing full-text indexes. Select an index to show its properties in the grid to the right. If the list is empty, no full-text relationships have been defined for the table.  
  
**Add**  
Create a new full-text index.  
  
**Delete**  
Delete the full-text index selected in the **Selected Full-Text Index** list.  
  
**General Category**  
When expanded, shows **Columns** and **Full-text Catalog Name**.  
  
**Columns**  
Displays a comma-separated list of the names of full-text-searchable columns. To see the complete list, click the ellipsis button (**...**) to the left of the property field.  
  
**Full-Text Catalog Name**  
Displays the name of the full-text catalog on which this full-text index is stored. To store the index on a different catalog, click the catalog name and choose another from the drop-down list.  
  
> [!NOTE]  
> The catalog must be created first in an outside tool, such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or Enterprise Manager.  
  
**Identity Category**  
When expanded, shows the name field for this index.  
  
**Name**  
Displays the system-specified name for this full-text index.  
  
**Table Designer Category**  
When expanded, shows properties that dictate how the index performs.  
  
**Active**  
Indicates whether you can currently perform a full-text search using this full-text index.  
  
**Change Tracking Setting**  
Describes the status of change tracking for this index: Manual, Auto, or Off.  
  
**Crawl Completed**  
Shows whether the most recent crawl has been completed. If this property value is No, a crawl is currently in progress.  
  
**End Date And Time Of Current Or Last Crawl**  
Displays the date and time that the most recent crawl ended.  
  
**Errors In Current Or Last Crawl**  
Displays the number of errors in current or most recent crawl.  
  
**Index Version**  
Displays the schema version of table at time the crawl started.  
  
**Rows In Current Or Last Crawl**  
Displays the number of rows updated in the current or most recent crawl.  
  
**Start Date And Time Of Current Or Last Crawl**  
Displays the date and time that the current or most recent crawl started.  
  
**Time Stamp For Next Crawl**  
Displays the date and time that the next crawl will start.  
  
**Type Of Current Or Last Crawl**  
Displays the type of the current or most recent crawl: Full, Incremental, Update, or Auto Propagation.  
  
**Unique Index Name**  
Displays a list of all of the names of columns in this database that have unique single-column indexes. These columns can be used to create a full-text index.  
  
## See Also  
[Use the Full-Text Indexing Wizard](https://msdn.microsoft.com/3e9d9605-6525-4781-9168-fdaa06db3459)  
[CREATE FULLTEXT INDEX (Transact-SQL)](https://msdn.microsoft.com/8b80390f-5f8b-4e66-9bcc-cabd653c19fd)  
  
