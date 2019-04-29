---
title: "sys.dm_db_missing_index_details (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_missing_index_details"
  - "dm_db_missing_index_details"
  - "sys.dm_db_missing_index_details_TSQL"
  - "dm_db_missing_index_details_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "missing indexes feature [SQL Server], sys.dm_db_missing_index_details dynamic management view"
  - "sys.dm_db_missing_index_details dynamic management view"
ms.assetid: ced484ae-7c17-4613-a3f9-6d8aba65a110
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_missing_index_details (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns detailed information about missing indexes, excluding spatial indexes.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out.  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**index_handle**|**int**|Identifies a particular missing index. The identifier is unique across the server. **index_handle** is the key of this table.|  
|**database_id**|**smallint**|Identifies the database where the table with the missing index resides.|  
|**object_id**|**int**|Identifies the table where the index is missing.|  
|**equality_columns**|**nvarchar(4000)**|Comma-separated list of columns that contribute to equality predicates of the form:<br /><br /> *table.column* =*constant_value*|  
|**inequality_columns**|**nvarchar(4000)**|Comma-separated list of columns that contribute to inequality predicates, for example, predicates of the form:<br /><br /> *table.column* > *constant_value*<br /><br /> Any comparison operator other than "=" expresses inequality.|  
|**included_columns**|**nvarchar(4000)**|Comma-separated list of columns needed as covering columns for the query. For more information about covering or included columns, see [Create Indexes with Included Columns](../../relational-databases/indexes/create-indexes-with-included-columns.md).<br /><br /> For memory-optimized indexes (both hash and memory-optimized nonclustered), ignore **included_columns**. All columns of the table are included in every memory-optimized index.|  
|**statement**|**nvarchar(4000)**|Name of the table where the index is missing.|  
  
## Remarks  
 Information returned by **sys.dm_db_missing_index_details** is updated when a query is optimized by the query optimizer, and is not persisted. Missing index information is kept only until [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted. Database administrators should periodically make backup copies of the missing index information if they want to keep it after server recycling.  
  
 To determine which missing index groups a particular missing index is part of, you can query the **sys.dm_db_missing_index_groups** dynamic management view by equijoining it with **sys.dm_db_missing_index_details** based on the **index_handle** column.  

  >[!NOTE]
  >The result set for this DMV is limited to 600 rows. Each row contains one missing index. If you have more than 600 missing indexes, you should address the existing missing indexes so you can then view the newer ones. 
  
## Using Missing Index Information in CREATE INDEX Statements  
 To convert the information returned by **sys.dm_db_missing_index_details** into a CREATE INDEX statement for both memory-optimized and disk-based indexes, equality columns should be put before the inequality columns, and together they should make the key of the index. Included columns should be added to the CREATE INDEX statement using the INCLUDE clause. To determine an effective order for the equality columns, order them based on their selectivity: list the most selective columns first (leftmost in the column list).  
  
 For more information about memory-optimized indexes, see [Indexes for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/indexes-for-memory-optimized-tables.md).  
  
## Transaction Consistency  
 If a transaction creates or drops a table, the rows containing missing index information about the dropped objects are removed from this dynamic management object, preserving transaction consistency.  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## See Also  
 [sys.dm_db_missing_index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-columns-transact-sql.md)   
 [sys.dm_db_missing_index_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-groups-transact-sql.md)   
 [sys.dm_db_missing_index_group_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-transact-sql.md)  
  
  
