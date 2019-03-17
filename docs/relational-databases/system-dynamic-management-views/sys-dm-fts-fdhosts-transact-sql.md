---
title: "sys.dm_fts_fdhosts (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_fts_fdhosts"
  - "dm_fts_fdhosts_TSQL"
  - "sys.dm_fts_fdhosts"
  - "sys.dm_fts_fdhosts_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_fts_fdhosts dynamic management view"
  - "troubleshooting [SQL Server], full-text search"
ms.assetid: d42a6334-4362-4361-83da-f8324fe55ec7 
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_fts_fdhosts (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns information on the current activity of the filter daemon host or hosts on the server instance.  
  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**fdhost_id**|**int**|ID of the filter daemon host.|  
|**fdhost_name**|**nvarchar(120)**|Name of filter daemon host.|  
|**fdhost_process_id**|**int**|Windows process ID of the filter daemon host.|  
|**fdhost_type**|**nvarchar(120)**|Type of document being processed by the filter daemon host, one of:<br /><br /> Single thread<br /><br /> Multi-thread<br /><br /> Huge document|  
|**max_thread**|**int**|Maximum number of threads in the filter daemon host.|  
|**batch_count**|**int**|Number of batches that are being processed in the filter daemon host.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## Examples  
 The following example returns the name of the filter daemon host and the maximum number of threads in it. It also monitors how many batches are currently being processed in the filter daemon. This information can be used to diagnose performance.  
  
```  
SELECT fdhost_name, batch_count, max_thread FROM sys.dm_fts_fdhosts;  
GO  
```  
  
## See Also  
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)  
  
  
