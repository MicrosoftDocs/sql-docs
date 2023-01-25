---
title: "sys.indexes (Transact-SQL)"
description: "sys.indexes contains a row per index or heap of a tabular object, such as a table, view, or table-valued function."
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.indexes"
  - "indexes"
  - "sys.indexes_TSQL"
  - "indexes_TSQL"
helpviewer_keywords:
  - "sys.indexes catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.indexes (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Contains a row per index or heap of a tabular object, such as a table, view, or table-valued function.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object to which this index belongs.|  
|**name**|**sysname**|Name of the index. `name` is unique only within the object.<br /><br /> NULL = Heap|  
|**index_id**|**int**|ID of the index. `index_id` is unique only within the object.<br /><br /> 0 = Heap<br /><br /> 1 = Clustered index<br /><br /> > 1 = Nonclustered index|  
|**type**|**tinyint**|Type of index:<br /><br /> 0 = Heap<br /><br /> 1 = Clustered rowstore (B-tree)<br /><br /> 2 = Nonclustered rowstore (B-tree)<br /><br /> 3 = XML<br /><br /> 4 = Spatial<br /><br /> 5 = Clustered columnstore index. **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> 6 = Nonclustered columnstore index. **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> 7 = Nonclustered hash index. **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.|  
|**type_desc**|**nvarchar(60)**|Description of index type:<br /><br /> HEAP<br /><br /> CLUSTERED<br /><br /> NONCLUSTERED<br /><br /> XML<br /><br /> SPATIAL<br /><br /> CLUSTERED COLUMNSTORE - **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> NONCLUSTERED COLUMNSTORE - **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> NONCLUSTERED HASH : NONCLUSTERED HASH indexes are supported only on memory-optimized tables. The `sys.hash_indexes` view shows the current hash indexes and the hash properties. For more information, see [sys.hash_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-hash-indexes-transact-sql.md). **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.|  
|**is_unique**|**bit**|1 = Index is unique.<br /><br /> 0 = Index is not unique.<br /><br /> Always 0 for clustered columnstore indexes.|  
|**data_space_id**|**int**|ID of the data space for this index. Data space is either a filegroup or partition scheme.<br /><br /> 0 = `object_id` is a table-valued function or in-memory index.|  
|**ignore_dup_key**|**bit**|1 = IGNORE_DUP_KEY is ON.<br /><br /> 0 = IGNORE_DUP_KEY is OFF.|  
|**is_primary_key**|**bit**|1 = Index is part of a PRIMARY KEY constraint.<br /><br /> Always 0 for clustered columnstore indexes.|  
|**is_unique_constraint**|**bit**|1 = Index is part of a UNIQUE constraint.<br /><br /> Always 0 for clustered columnstore indexes.|  
|**fill_factor**|**tinyint**|> 0 = FILLFACTOR percentage used when the index was created or rebuilt.<br /><br /> 0 = Default value<br /><br /> Always 0 for clustered columnstore indexes.|  
|**is_padded**|**bit**|1 = PADINDEX is ON.<br /><br /> 0 = PADINDEX is OFF.<br /><br /> Always 0 for clustered columnstore indexes.|  
|**is_disabled**|**bit**|1 = Index is disabled.<br /><br /> 0 = Index is not disabled.|  
|**is_hypothetical**|**bit**|1 = Index is hypothetical and cannot be used directly as a data access path. Hypothetical indexes hold column-level statistics.<br /><br /> 0 = Index is not hypothetical.|  
|**allow_row_locks**|**bit**|1 = Index allows row locks.<br /><br /> 0 = Index does not allow row locks.<br /><br /> Always 0 for clustered columnstore indexes.|  
|**allow_page_locks**|**bit**|1 = Index allows page locks.<br /><br /> 0 = Index does not allow page locks.<br /><br /> Always 0 for clustered columnstore indexes.|  
|**has_filter**|**bit**|1 = Index has a filter and only contains rows that satisfy the filter definition.<br /><br /> 0 = Index does not have a filter.|  
|**filter_definition**|**nvarchar(max)**|Expression for the subset of rows included in the filtered index.<br /><br /> NULL for heap, non-filtered index, or insufficient permissions on the table.|  
|**compression_delay**|**int**|> 0 = Columnstore index compression delay specified in minutes.<br /><br /> NULL = Columnstore index rowgroup compression delay is managed automatically.|
|**suppress_dup_key_messages**|**bit**|1 = Index is configured to suppress duplicate key messages during an index rebuild operation.<br /><br /> **0** = Index is not configured to suppress duplicate key messages during an index rebuild operation.<br /><br />**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]|
|**auto_created**|**bit**|1 = Index was created by the automatic tuning.<br /><br />0 = Index was created by the user.<br /><br />**Applies to:** [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]|
|**optimize_for_sequential_key**|**bit**|1 = Index has last-page insert optimization enabled.<br><br>0 = Default value. Index has last-page insert optimization disabled.<br /><br />**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]|

## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 The following example returns all indexes for the table `Production.Product` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
SELECT i.name AS index_name  
    ,i.type_desc  
    ,is_unique  
    ,ds.type_desc AS filegroup_or_partition_scheme  
    ,ds.name AS filegroup_or_partition_scheme_name  
    ,ignore_dup_key  
    ,is_primary_key  
    ,is_unique_constraint  
    ,fill_factor  
    ,is_padded  
    ,is_disabled  
    ,allow_row_locks  
    ,allow_page_locks  
FROM sys.indexes AS i  
INNER JOIN sys.data_spaces AS ds ON i.data_space_id = ds.data_space_id  
WHERE is_hypothetical = 0 AND i.index_id <> 0   
AND i.object_id = OBJECT_ID('Production.Product');  
GO  
```  
  
## Next steps

 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)   
 [sys.xml_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-xml-indexes-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.key_constraints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-key-constraints-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)   
 [sys.partition_schemes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-schemes-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)   
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md)  
  
