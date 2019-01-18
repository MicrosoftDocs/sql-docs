---
title: "sys.dm_db_log_info (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/24/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
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
author: "savjani"
ms.author: "pariks"
manager: "ajayj"
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_log_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-2016sp2-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md)]

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
|vlf_encryptor_thumbprint|**varbinary(20)**| **Applies to:** [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] <br><br> Shows the thumbprint of the encryptor of the VLF if the VLF is encrypted using [Transparent Data Encryption](../../relational-databases/security/encryption/transparent-data-encryption.md), otherwise NULL. |

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

### B. Determing the position of the last `VLF` in transaction log before shrinking the log file

The following query can be used to determine the position of the last active VLF before running shrinkfile on transaction log to determine if transaction log can shrink.

```sql
USE AdventureWorks2016
GO

;WITH cte_vlf AS (
SELECT ROW_NUMBER() OVER(ORDER BY vlf_begin_offset) AS vlfid, DB_NAME(database_id) AS [Database Name], vlf_sequence_number, vlf_active, vlf_begin_offset, vlf_size_mb
	FROM sys.dm_db_log_info(DEFAULT)),
cte_vlf_cnt AS (SELECT [Database Name], COUNT(vlf_sequence_number) AS vlf_count,
	(SELECT COUNT(vlf_sequence_number) FROM cte_vlf WHERE vlf_active = 0) AS vlf_count_inactive,
	(SELECT COUNT(vlf_sequence_number) FROM cte_vlf WHERE vlf_active = 1) AS vlf_count_active,
	(SELECT MIN(vlfid) FROM cte_vlf WHERE vlf_active = 1) AS ordinal_min_vlf_active,
	(SELECT MIN(vlf_sequence_number) FROM cte_vlf WHERE vlf_active = 1) AS min_vlf_active,
	(SELECT MAX(vlfid) FROM cte_vlf WHERE vlf_active = 1) AS ordinal_max_vlf_active,
	(SELECT MAX(vlf_sequence_number) FROM cte_vlf WHERE vlf_active = 1) AS max_vlf_active
	FROM cte_vlf
	GROUP BY [Database Name])
SELECT [Database Name], vlf_count, min_vlf_active, ordinal_min_vlf_active, max_vlf_active, ordinal_max_vlf_active,
((ordinal_min_vlf_active-1)*100.00/vlf_count) AS free_log_pct_before_active_log,
((ordinal_max_vlf_active-(ordinal_min_vlf_active-1))*100.00/vlf_count) AS active_log_pct,
((vlf_count-ordinal_max_vlf_active)*100.00/vlf_count) AS free_log_pct_after_active_log
FROM cte_vlf_cnt
GO
```

## See Also  
[Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
[Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)   
[sys.dm_db_log_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md)   
[sys.dm_db_log_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-stats-transact-sql.md)

