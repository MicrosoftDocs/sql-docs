---
title: "sys.dm_db_log_info (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/16/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "dmv's"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sys.dm_db_log_info"
  - "sys.dm_db_log_info_TSQL"
  - "dm_db_log_info"
  - "dm_db_log_info_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_log_info dynamic management view"
ms.assetid: f6b40060-c17d-472f-b0a3-3b350275d487
caps.latest.revision: 4
author: "savjani"
ms.author: "pariks"
manager: "ajayj"
ms.workload: "Inactive"
---
# sys.dm_db_log_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

Returns [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) information of the transaction log. Note all transaction log files are combined in the table output. Each row in the output represents a VLF in the transaction log and provides information relevant to that VLF in the log.

## Syntax  
  
```  
sys.dm_db_log_info ( database_id )  
```  
## Arguments  
 *database_id* | NULL | DEFAULT  
 Is the ID of the database. *database_id* is **int**. Valid inputs are the ID number of a database, NULL, or DEFAULT. The default is NULL. NULL and DEFAULT are equivalent values in the context of current database.
 
 Specify NULL to return VLF information of the current database.

 The built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) can be specified. When using `DB_ID` without specifying a database name, the compatibility level of the current database must be 90 or greater.  

## Table Returned  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**int**|Database ID.|
|file_id|**smallint**|File id of the transaction log.|  
|vlf_begin_offset|**bigint** |Offset location of the [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) from the beginning of the transaction log file.|
|vlf_size_mb |**float** |[virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) size in MB, rounded to 2 decimal places.|     
|vlf_sequence_number|**bigint** |[virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) sequence number in the created order. Used to uniquely identify VLFs in log file.|
|vlf_active|**bit** |Indicates whether [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) is in use or not. <br />0 - VLF is not in use.<br />1 - VLF is active.|
|vlf_status|**int** |Status of the [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch). Possible values include <br />0 - VLF is inactive <br />1 - VLF is initialized but unused <br /> 2 - VLF is active.|
|vlf_parity|**tinyint** |Parity of [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch).Used internally to determine the end of log within a VLF.|
|vlf_first_lsn|**nvarchar(48)** |[Log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch) of the first log record in the [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch).|
|vlf_create_lsn|**nvarchar(48)** |[Log sequence number (LSN)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#Logical_Arch) of the log record that created the [virtual log file (VLF)](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#physical_arch).|

## Remarks
 The `sys.dm_db_log_info` dynamic management function replaces the `DBCC LOGINFO` statement. 
 
## Permissions  
 Requires the `VIEW DATABASE STATE` permission in the database.  
  
## Examples  
  
### A. Determing databases in a SQL Server instance with high number of VLFs
The following query determines the databases with more than 100 VLFs in the log files, which can affect the database startup, restore, and recovery time.

```sql
SELECT [name], COUNT(l.database_id) AS 'vlf_count' 
FROM sys.databases s
CROSS APPLY sys.dm_db_log_info(s.database_id) l
GROUP BY [name]
HAVING COUNT(l.database_id) > 100
```

### B. Determing the status of last `VLF` in transaction log before shrinking the log file

The following query can be used to determine the status of last VLF before running shrinkfile on transaction log to determine if transaction log can shrink.

```sql
USE AdventureWorks2016
GO

SELECT TOP 1 DB_NAME(database_id) AS "Database Name", file_id, vlf_size_mb, vlf_sequence_number, vlf_active, vlf_status
FROM sys.dm_db_log_info(DEFAULT)
ORDER BY vlf_sequence_number DESC
```


## See Also  
[Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
[Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)   
[sys.dm_db_log_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md)   
[sys.dm_db_log_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-stats-transact-sql.md)

