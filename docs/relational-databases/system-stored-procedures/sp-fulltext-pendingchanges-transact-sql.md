---
description: "sp_fulltext_pendingchanges (Transact-SQL)"
title: "sp_fulltext_pendingchanges (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_fulltext_pendingchanges_TSQL"
  - "sp_fulltext_pendingchanges"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_fulltext_pendingchanges"
ms.assetid: fee042fe-4781-4a33-a01b-d98fb5629f1b
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_fulltext_pendingchanges (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns unprocessed changes, such as pending inserts, updates, and deletes, for a specified table that is using change tracking.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_fulltext_pendingchanges table_id  
```  
  
## Arguments  
 *table_id*  
 ID of the table. If the table is not full-text indexed, or change tracking is not enabled on the table, an error is returned.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Key**|*|Is the full-text key value from the specified table.|  
|**DocId**|**bigint**|Is an internal document identifier (DocId) column that corresponds to the key value.|  
|**Status**|**int**|0 = Row will be removed from the full-text index.<br /><br /> 1 = Row will be full-text indexed.<br /><br /> 2 = Row is up-to-date.<br /><br /> -1 = Row is in a transitional (batched, but not committed) state or an error state.|  
|**DocState**|**tinyint**|Is a raw dump of the internal document identifier (DocId) map status column.|  
  
 <sup>* The data type for Key is same as the data type of the full-text key column in the base table.</sup>  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Remarks  
 If there are no changes to process, an empty rowset is returned.  
  
 Full-Text Search queries do not return rows with a **Status** value of 0. This is because the row has been deleted from base table and is waiting to be deleted from the full-text index.  
  
 To find out how many changes are pending for a particular table, use the **TableFullTextPendingChanges** property of the OBJECTPROPERTYEX function.  
  
## See Also  
 [Full-Text Search and Semantic Search Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/full-text-search-and-semantic-search-stored-procedures-transact-sql.md)   
 [OBJECTPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/objectpropertyex-transact-sql.md)  
  
  
