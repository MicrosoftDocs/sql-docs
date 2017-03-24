---
title: "sys.dm_db_missing_index_groups (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_missing_index_groups"
  - "sys.dm_db_missing_index_groups_TSQL"
  - "dm_db_missing_index_groups"
  - "dm_db_missing_index_groups_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_missing_index_groups dynamic management view"
  - "missing indexes feature [SQL Server], sys.dm_db_missing_index_groups dynamic management view"
ms.assetid: 9cc00acd-d83d-49f8-be72-5b2aebed246b
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.dm_db_missing_index_groups (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns information about what missing indexes are contained in a specific missing index group, excluding spatial indexes.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn’t belong to the connected tenant is filtered out.  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**index_group_handle**|**int**|Identifies a missing index group.|  
|**index_handle**|**int**|Identifies a missing index that belongs to the group specified by **index_group_handle**.<br /><br /> An index group contains only one index.|  
  
## Remarks  
 Information returned by **sys.dm_db_missing_index_groups** is updated when a query is optimized by the query optimizer, and is not persisted. Missing index information is kept only until [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted. Database administrators should periodically make backup copies of the missing index information if they want to keep it after server recycling.  
  
 Neither column of the output result set is a key, but together they form an index key.  
  
## Permissions  
 To query this dynamic management view, users must be granted the VIEW SERVER STATE permission or any permission that implies the VIEW SERVER STATE permission.  
  
## See Also  
 [sys.dm_db_missing_index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-columns-transact-sql.md)   
 [sys.dm_db_missing_index_details &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md)   
 [sys.dm_db_missing_index_group_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-transact-sql.md)  
  
  
