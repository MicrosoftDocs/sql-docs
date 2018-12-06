---
title: "suspect_pages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "suspect_page_table"
  - "suspect_page_table_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "suspect_pages system table"
  - "suspect pages [SQL Server]"
ms.assetid: 119c8d62-eea8-44fb-bf72-de469c838c50
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# suspect_pages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row per page that failed with a minor 823 error or an 824 error. Pages are listed in this table because they are suspected of being bad, but they might actually be fine. When a suspect page is repaired, its status is updated in the **event_type** column.  
  
 The following table, which has a limit of 1,000 rows, is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|ID of the database to which this page applies.|  
|**file_id**|**int**|ID of the file in the database.|  
|**page_id**|**bigint**|ID of the suspect page. Every page has a page ID that is a 32-bit value identifying the location of the page in the database. The **page_id** is the offset into the data file of the 8 KB page. Each page ID is unique in a file.|  
|**event_type**|**int**|The type of error; one of:<br /><br /> 1 = An 823 error that causes a suspect page (such as a disk error) or an 824 error other than a bad checksum or a torn page (such as a bad page ID).<br /><br /> 2 = Bad checksum.<br /><br /> 3 = Torn page.<br /><br /> 4 = Restored (page was restored after it was marked bad).<br /><br /> 5 = Repaired (DBCC repaired the page).<br /><br /> 7 = Deallocated by DBCC.|  
|**error_count**|**int**|Number of times the error has occurred.|  
|**last_update_date**|**datetime**|Date-and-time stamp of the last update.|  
  
## Permissions  
 Anyone with access to **msdb** can read the data in the **suspect_pages** table. Anyone with UPDATE permission on the suspect_pages table can update its records. Members the **db_owner** fixed database role on **msdb** or the **sysadmin** fixed server role can insert, update, and delete records.  
  
## See Also  
 [Restore Pages &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-pages-sql-server.md)   
 [Database Suspect Data Page Event Class](../../relational-databases/event-classes/database-suspect-data-page-event-class.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)   
 [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)  
  
  
