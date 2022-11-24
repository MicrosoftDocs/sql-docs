---
title: "sys.index_resumable_operations (Transact-SQL)"
description: sys.index_resumable_operations (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/12/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.index_resumable_operations_TSQL"
  - "sys.indexes_TSQL"
helpviewer_keywords:
  - "sys.indexes"
  - "sys.index_resumable_operations"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.index_resumable_operations (Transact-SQL)

[!INCLUDE[sqlserver2017-asdb](../../includes/applies-to-version/sqlserver2017-asdb.md)]
**sys.index_resumable_operations** is a system view that monitors and checks the current execution status for resumable Index rebuild or creation.  
**Applies to**: SQL Server (2017 and newer), and Azure SQL Database
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object to which this index belongs (not nullable).|  
|**index_id**|**int**|ID of the index (not nullable). **index_id** is unique only within the object.|
|**name**|**sysname**|Name of the index. **name** is unique only within the object.|  
|**sql_text**|**nvarchar(max)**|DDL T-SQL statement text|
|**last_max_dop**|**smallint**|Last MAX_DOP used (default = 0)|
|**partition_number**|**int**|Partition number within the owning index or heap. For non-partitioned tables and indexes or in case all partitions are being rebuild the value of this column is NULL.|
|**state**|**tinyint**|Operational state for resumable index:<br /><br />0=Running<br /><br />1=Pause|
|**state_desc**|**nvarchar(60)**|Description of the operational state for resumable index (running or Paused)|  
|**start_time**|**datetime**|Index operation start time (not nullable)|
|**last_pause_time**|**datatime**| Index operation last pause time (nullable). NULL if operation is running and never paused.|
|**total_execution_time**|**int**|Total execution time from start time in minutes (not nullable)|
|**percent_complete**|**real**|Index operation progress completion in % ( not nullable).|
|**page_count**|**bigint**|Total number of index pages allocated by the index build operation for the new and mapping indexes ( not nullable ).

## Permissions

[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  

## Example

 List all resumable index creation or rebuild operations that are in the PAUSE state.

```sql
SELECT * FROM  sys.index_resumable_operations WHERE STATE = 1;  
```

## See Also

- [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md)
- [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md)
- [Catalog views](catalog-views-transact-sql.md)
- [Object catalog views](object-catalog-views-transact-sql.md)
- [sys.indexes](sys-xml-indexes-transact-sql.md)
- [sys.index_columns](sys-index-columns-transact-sql.md)
- [sys.xml_indexes](sys-xml-indexes-transact-sql.md)
- [sys.objects](sys-index-columns-transact-sql.md)
- [sys.key_constraints](sys-key-constraints-transact-sql.md)
- [sys.filegroups](sys-filegroups-transact-sql.md)
- [sys.partition_schemes](sys-partition-schemes-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
