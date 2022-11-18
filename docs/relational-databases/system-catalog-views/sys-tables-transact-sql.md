---
title: sys.tables (Transact-SQL)
description: sys.tables (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "sys.tables_TSQL"
  - "sys.tables"
helpviewer_keywords:
  - "sys.tables catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 8c42eba1-c19f-4045-ac82-b97a5e994090
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.tables (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a row for each user table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|\<inherited columns>||For a list of columns that this view inherits, see [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).|  
|lob_data_space_id|**int**|A nonzero value is the ID of the data space (filegroup or partition scheme) that holds the large object binary (LOB) data for this table. Examples of LOB data types include **varbinary(max)**, **varchar(max)**, **geography**, or **xml**.<br /><br /> 0 = The table doesn't have LOB data.|  
|filestream_data_space_id|**int**|Is the data space ID for a FILESTREAM filegroup or a partition scheme that consists of FILESTREAM filegroups.<br /><br /> To report the name of a FILESTREAM filegroup, execute the query `SELECT FILEGROUP_NAME (filestream_data_space_id) FROM sys.tables`.<br /><br /> sys.tables can be joined to the following views on filestream_data_space_id = data_space_id.<br /><br /> - sys.filegroups<br /><br /> - sys.partition_schemes<br /><br /> - sys.indexes<br /><br /> - sys.allocation_units<br /><br /> - sys.fulltext_catalogs<br /><br /> - sys.data_spaces<br /><br /> - sys.destination_data_spaces<br /><br /> - sys.master_files<br /><br /> - sys.database_files<br /><br /> - `backupfilegroup` (join on filegroup_id)|  
|max_column_id_used|**int**|Maximum column ID ever used by this table.|  
|lock_on_bulk_load|**bit**|Table is locked on bulk load. For more information, see [sp_tableoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md).|  
|uses_ansi_nulls|**bit**|Table was created with the SET ANSI_NULLS database option ON.|  
|is_replicated|**bit**|1 = Table is published using snapshot replication or transactional replication.|  
|has_replication_filter|**bit**|1 = Table has a replication filter.|  
|is_merge_published|**bit**|1 = Table is published using merge replication.|  
|is_sync_tran_subscribed|**bit**|1 = Table is subscribed using an immediate updating subscription.|  
|has_unchecked_assembly_data|**bit**|1 = Table contains persisted data that depends on an assembly whose definition changed during the last ALTER ASSEMBLY. Will be reset to 0 after the next successful DBCC CHECKDB or DBCC CHECKTABLE.|  
|text_in_row_limit|**int**|The maximum bytes allowed for text in row.<br /><br /> 0 = Text in row option isn't set. For more information, see [sp_tableoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md).|  
|large_value_types_out_of_row|**bit**|1 = Large value types are stored out-of-row. For more information, see [sp_tableoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md).|  
|is_tracked_by_cdc|**bit**|1 = Table is enabled for change data capture. For more information, see [sys.sp_cdc_enable_table &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md).|  
|lock_escalation|**tinyint**|The value of the LOCK_ESCALATION option for the table:<br /><br /> 0 = TABLE<br /><br /> 1 = DISABLE<br /><br /> 2 = AUTO|  
|lock_escalation_desc|**nvarchar(60)**|A text description of the lock_escalation option for the table. Possible values are: TABLE, AUTO, and DISABLE.|  
|is_filetable|**bit**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)].<br /><br /> 1 = Table is a FileTable.<br /><br /> For more information about FileTables, see [FileTables &#40;SQL Server&#41;](../../relational-databases/blob/filetables-sql-server.md).|  
|durability|**tinyint**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)].<br /><br /> The following are possible values:<br /><br /> 0 = SCHEMA_AND_DATA<br /><br /> 1 = SCHEMA_ONLY<br /><br /> The value of 0 is the default value.|  
|durability_desc|**nvarchar(60)**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)].<br /><br /> The following are the possible values:<br /><br /> SCHEMA_ONLY<br /><br /> SCHEMA_AND_DATA<br /><br /> The value of SCHEMA_AND_DATA indicates that the table is a durable, in-memory table. SCHEMA_AND_DATA is the default value for memory optimized tables. The value of SCHEMA_ONLY indicates that the table data won't be persisted upon restart of the database with memory optimized objects.|  
|is_memory_optimized|**bit**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)].<br /><br /> The following are the possible values:<br /><br /> 0 = not memory optimized.<br /><br /> 1 = is memory optimized.<br /><br /> A value of 0 is the default value.<br /><br /> Memory optimized tables are in-memory user tables, the schema of which is persisted on disk similar to other user tables. Memory optimized tables can be accessed from natively compiled stored procedures.|  
|temporal_type|**tinyint**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)].<br /><br /> The numeric value representing the type of table:<br /><br /> 0 = NON_TEMPORAL_TABLE<br /><br /> 1 = HISTORY_TABLE (associated with a temporal table)<br /><br /> 2 = SYSTEM_VERSIONED_TEMPORAL_TABLE|  
|temporal_type_desc|**nvarchar(60)**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)].<br /><br /> The text description of the type of table:<br /><br /> NON_TEMPORAL_TABLE<br /><br /> HISTORY_TABLE<br /><br /> SYSTEM_VERSIONED_TEMPORAL_TABLE|  
|history_table_id|**int**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)].<br /><br /> When temporal_type = 2 or ledger_type = 2 returns object_id of the table that maintains historical data for a temporal table, otherwise returns NULL.|  
|is_remote_data_archive_enabled|**bit**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]<br /><br /> Indicates whether the table is Stretch-enabled.<br /><br /> 0 = The table isn't Stretch-enabled.<br /><br /> 1 = The table is Stretch-enabled.<br /><br /> For more info, see [Stretch Database](../../sql-server/stretch-database/stretch-database.md).|  
|is_external|**bit**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[sssdwfull](../../includes/sssdwfull-md.md)].<br /><br /> Indicates table is an external table.<br /><br /> 0 = The table isn't an external table.<br /><br /> 1 = The table is an external table.| 
|history_retention_period|**int**|**Applies to**: [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>The numeric value representing duration of the temporal history retention period in units specified with history_retention_period_unit. |  
|history_retention_period_unit|**int**|**Applies to**: [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>The numeric value representing type of temporal history retention period unit. <br /><br />-1: INFINITE <br /><br />3: DAY <br /><br />4: WEEK <br /><br />5: MONTH <br /><br />6: YEAR |  
|history_retention_period_unit_desc|**nvarchar(10)**|**Applies to**: [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>The text description of type of temporal history retention period unit. <br /><br />INFINITE <br /><br />DAY <br /><br />WEEK <br /><br />MONTH <br /><br />YEAR |  
|is_node|**bit**|**Applies to**: [!INCLUDE[sssql17-md.md](../../includes/sssql17-md.md)] and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>1 = This is a graph Node table. <br /><br />0 = This isn't a graph Node table. |  
|is_edge|**bit**|**Applies to**: [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>1 = This is a graph Edge table. <br /><br />0 = This isn't a graph Edge table. |  
|ledger_type|**tinyint**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>The numeric value indicates if the table is a ledger table.<br/><br/>0 = NON_LEDGER_TABLE<br/>1 = HISTORY_TABLE (associated with an updatable ledger table)<br/>2 = UPDATABLE_LEDGER_TABLE<br/>3 = APPEND_ONLY_LEDGER_TABLE<br /><br />For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).|
|ledger_type_desc|**nvarchar(60)**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>The text description of a value in the ledger_type column:<br/><br/>NON_LEDGER_TABLE<br/>HISTORY_TABLE<br/>UPDATABLE_LEDGER_TABLE<br/>APPEND_ONLY_LEDGER_TABLE|  
|ledger_view_id|**int**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>When ledger_type IN (2,3) returns object_id of the ledger view, otherwise returns NULL.|
|is_dropped_ledger_table|**bit**|**Applies to**: Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/>Indicates a ledger table that has been dropped.|

## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 The following example returns all of the user tables that don't have a primary key.  
  
```  
SELECT SCHEMA_NAME(schema_id) AS schema_name  
    ,name AS table_name   
FROM sys.tables   
WHERE OBJECTPROPERTY(object_id,'TableHasPrimaryKey') = 0  
ORDER BY schema_name, table_name;  
GO  
  
```  
  
The following example shows how related temporal data can be exposed.  
   
**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)].
  
```  
SELECT T1.object_id, T1.name as TemporalTableName, SCHEMA_NAME(T1.schema_id) AS TemporalTableSchema,  
T2.name as HistoryTableName, SCHEMA_NAME(T2.schema_id) AS HistoryTableSchema,  
T1.temporal_type_desc  
FROM sys.tables T1  
LEFT JOIN sys.tables T2   
ON T1.history_table_id = T2.object_id  
ORDER BY T1.temporal_type desc  
```  

The following example shows how information on temporal history retention can be exposed.  

**Applies to**: [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)].  
  
```  
SELECT DB.is_temporal_history_retention_enabled, SCHEMA_NAME(T1.schema_id) AS TemporalTableSchema, 
T1.name as TemporalTableName, SCHEMA_NAME(T2.schema_id) AS HistoryTableSchema, T2.name as HistoryTableName,
T1.history_retention_period, T1.history_retention_period_unit_desc
FROM sys.tables T1  
OUTER APPLY (select is_temporal_history_retention_enabled from sys.databases where name = DB_NAME()) DB
LEFT JOIN sys.tables T2   
ON T1.history_table_id = T2.object_id WHERE T1.temporal_type = 2 
```  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)   
 [DBCC CHECKTABLE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)   
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md)  
  
