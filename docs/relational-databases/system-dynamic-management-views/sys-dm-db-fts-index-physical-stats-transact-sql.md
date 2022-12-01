---
title: "sys.dm_db_fts_index_physical_stats (Transact-SQL)"
description: sys.dm_db_fts_index_physical_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/20/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_fts_index_physical_stats_TSQL"
  - "dm_db_fts_index_physical_stats"
  - "dm_db_fts_index_physical_stats_TSQL"
  - "sys.dm_db_fts_index_physical_stats"
helpviewer_keywords:
  - "sys.dm_db_fts_index_physical_stats dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 997c3278-3630-47f6-ada3-190b6c16ce0e
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_fts_index_physical_stats (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a row for each full-text or semantic index in each table that has an associated full-text or semantic index.  
  
|**Column name**|**Type**|**Description**|  
|-|-|-|  
|**object_id**|int|Object ID of the table that contains the index.|  
|**fulltext_index_page_count**|**bigint**|Logical size of the extraction in number of index pages.|  
|**keyphrase_index_page_count**|**bigint**|Logical size of the extraction in number of index pages.|  
|**similarity_index_page_count**|**bigint**|Logical size of the extraction in number of index pages.|  
  
## General Remarks  
 For more information, see [Manage and Monitor Semantic Search](../../relational-databases/search/manage-and-monitor-semantic-search.md).  
  
## Metadata  
 For information about the status of semantic indexing, query the following dynamic management views:  
  
-   [sys.dm_fts_index_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql.md)  
  
-   [sys.dm_fts_semantic_similarity_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-semantic-similarity-population-transact-sql.md)  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Examples  
 The following example shows how to query for the logical size of each full-text or semantic index in every table that has an associated full-text or semantic index:  
  
```  
SELECT * FROM sys.dm_db_fts_index_physical_stats;  
GO  
```  
  
## See Also  
 [Manage and Monitor Semantic Search](../../relational-databases/search/manage-and-monitor-semantic-search.md)  
  
