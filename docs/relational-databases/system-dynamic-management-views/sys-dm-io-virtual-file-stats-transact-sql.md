---
title: "sys.dm_io_virtual_file_stats (Transact-SQL)"
description: sys.dm_io_virtual_file_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/21/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_io_virtual_file_stats"
  - "sys.dm_io_virtual_file_stats_TSQL"
  - "sys.dm_io_virtual_file_stats"
  - "dm_io_virtual_file_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_io_virtual_file_stats dynamic management function"
dev_langs:
  - "TSQL"
ms.assetid: fa3e321f-6fe5-45ff-b397-02a0dd3d6b7d
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_io_virtual_file_stats (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns I/O statistics for data and log files. This dynamic management function replaces the [fn_virtualfilestats](../../relational-databases/system-functions/sys-fn-virtualfilestats-transact-sql.md) function.  
  
> [!NOTE]  
> To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], use the name **sys.dm_pdw_nodes_io_virtual_file_stats**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database

sys.dm_io_virtual_file_stats (   
    { database_id | NULL },  
    { file_id | NULL }  
)  
```  

```  
-- Syntax for Azure Synapse Analytics

sys.dm_pdw_nodes_io_virtual_file_stats
```
  
## Arguments  


 *database_id* | NULL

 **Applies to:** [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later, Azure SQL Database

 ID of the database. *database_id* is int, with no default. Valid inputs are the ID number of a database or NULL. When NULL is specified, all databases in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are returned.  
  
 The built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) can be specified.  
  
*file_id* | NULL

**Applies to:** [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later, Azure SQL Database
 
ID of the file. *file_id* is int, with no default. Valid inputs are the ID number of a file or NULL. When NULL is specified, all files on the database are returned.  
  
 The built-in function [FILE_IDEX](../../t-sql/functions/file-idex-transact-sql.md) can be specified, and refers to a file in the current database.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_name**|**sysname**|**Does not apply to:**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> Database name.</br></br>For Azure Synapse Analytics, this is the name of the database stored on the node which is identified by pdw_node_id. Each node has one tempdb database that has 13 files. Each node also has one database per distribution, and each distribution database has 5 files. For example, if each node contains 4 distributions, the results show 20 distribution database files per pdw_node_id. 
|**database_id**|**smallint**|ID of database.|  
|**file_id**|**smallint**|ID of file.|  
|**sample_ms**|**bigint**|Number of milliseconds since the computer was started. This column can be used to compare different outputs from this function.</br></br>The data type is **int** for [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. In these versions, the value will reset to 0 after approximately 25 days of continuous database engine uptime.|  
|**num_of_reads**|**bigint**|Number of reads issued on the file.|  
|**num_of_bytes_read**|**bigint**|Total number of bytes read on this file.|  
|**io_stall_read_ms**|**bigint**|Total time, in milliseconds, that the users waited for reads issued on the file.|  
|**num_of_writes**|**bigint**|Number of writes made on this file.|  
|**num_of_bytes_written**|**bigint**|Total number of bytes written to the file.|  
|**io_stall_write_ms**|**bigint**|Total time, in milliseconds, that users waited for writes to be completed on the file.|  
|**io_stall**|**bigint**|Total time, in milliseconds, that users waited for I/O to be completed on the file.|  
|**size_on_disk_bytes**|**bigint**|Number of bytes used on the disk for this file. For sparse files, this number is the actual number of bytes on the disk that are used for database snapshots.|  
|**file_handle**|**varbinary**|Windows file handle for this file.|  
|**io_stall_queued_read_ms**|**bigint**|**Does not apply to:**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)].<br /><br /> Total IO latency introduced by IO resource governance for reads. Is not nullable. For more information, see [sys.dm_resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md).|  
|**io_stall_queued_write_ms**|**bigint**|**Does not apply to:**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)].<br /><br />  Total IO latency introduced by IO resource governance for writes. Is not nullable.|
|**pdw_node_id**|**int**|**Applies to:** [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]</br></br>Identifier of the node for the distribution.
 
## Remarks
The counters are initialized to empty whenever the SQL Server (MSSQLSERVER) service is started.
  
## Permissions  
 Requires VIEW SERVER STATE permission. For more information, see [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md).  
  
## Examples  

### A. Return statistics for a log file

**Applies to:** [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later, Azure SQL Database

 The following example returns statistics for the log file in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
SELECT * FROM sys.dm_io_virtual_file_stats(DB_ID(N'AdventureWorks2012'), 2);  
GO  
```  
  
### B. Return statistics for file in tempdb

**Applies to:** Azure Synapse Analytics

```sql
SELECT * FROM sys.dm_pdw_nodes_io_virtual_file_stats 
WHERE database_name = 'tempdb' AND file_id = 2;

```

## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [I O Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/i-o-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
  
  

