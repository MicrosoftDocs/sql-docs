---
title: "DBCC SQLPERF (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/07/2018"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SQLPERF"
  - "DBCC_SQLPERF_TSQL"
  - "SQLPERF_TSQL"
  - "DBCC SQLPERF"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "statistical information [SQL Server], transaction logs"
  - "transaction logs [SQL Server], space usage"
  - "space [SQL Server], transaction logs"
  - "DBCC SQLPERF statement"
ms.assetid: ec9225ce-e20f-4b03-8b3a-7bcad8a649df
author: uc-msft
ms.author: umajay
manager: craigg
---
# DBCC SQLPERF (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

Provides transaction log space usage statistics for all databases. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] it can also be used to reset wait and latch statistics.
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]), [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] ([Preview in some regions](https://azure.microsoft.com/documentation/articles/sql-database-preview-whats-new/?WT.mc_id=TSQL_GetItTag))
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```
DBCC SQLPERF   
(  
     [ LOGSPACE ]  
     | [ "sys.dm_os_latch_stats" , CLEAR ]  
     | [ "sys.dm_os_wait_stats" , CLEAR ]  
)   
     [WITH NO_INFOMSGS ]  
```  
  
## Arguments  
LOGSPACE  
Returns the current size of the transaction log and the percentage of log space used for each database. Use this information to monitor the amount of space used in a transaction log.

> [!IMPORTANT]
> For more information about space usage information for the transaction log starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], refer to the [Remarks](#Remarks) section in this topic.
  
**"sys.dm_os_latch_stats"**, CLEAR  
Resets the latch statistics. For more information, see [sys.dm_os_latch_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-latch-stats-transact-sql.md). This option is not available in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
**"sys.dm_os_wait_stats"**, CLEAR  
Resets the wait statistics. For more information, see [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md). This option is not available in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
WITH NO_INFOMSGS  
Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Result Sets  
 The following table describes the columns in the result set.  
  
|Column name|Definition|  
|---|---|
|**Database Name**|Name of the database for the log statistics displayed.|  
|**Log Size (MB)**|Current size allocated to the log. This value is always smaller than the amount originally allocated for log space because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] reserves a small amount of disk space for internal header information.|  
|**Log Space Used (%)**|Percentage of the log file currently in use to store transaction log information.|  
|**Status**|Status of the log file. Always 0.|  
  
## <a name="Remarks"></a> Remarks  
Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], use the [sys.dm_db_log_space_usage](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md) DMV instead of `DBCC SQLPERF(LOGSPACE)`, to return space usage information for the transaction log per database.    
 
The transaction log records each transaction made in a database. For more information see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md) and [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md).
  
## Permissions  
On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to run `DBCC SQLPERF(LOGSPACE)` requires `VIEW SERVER STATE` permission on the server. To reset wait and latch statistics requires `ALTER SERVER STATE` permission on the server.
  
On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Premium and Business Critical tiers requires the `VIEW DATABASE STATE` permission in the database. On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Standard, Basic, and General Purpose tiers requires the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] admin account. Reset wait and latch statistics are not supported.
  
## Examples  
  
### A. Displaying log space information for all databases  
The following example displays `LOGSPACE` information for all databases contained in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
```sql  
DBCC SQLPERF(LOGSPACE);  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
Database Name Log Size (MB) Log Space Used (%) Status        
------------- ------------- ------------------ -----------   
master         3.99219      14.3469            0   
tempdb         1.99219      1.64216            0   
model          1.0          12.7953            0   
msdb           3.99219      17.0132            0   
AdventureWorks 19.554688    17.748701          0  
```  
  
### B. Resetting wait statistics  
The following example resets the wait statistics for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
```sql  
DBCC SQLPERF("sys.dm_os_wait_stats",CLEAR);  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)   
[sys.dm_os_latch_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-latch-stats-transact-sql.md)    
[sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md)     
[sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)    
[sys.dm_db_log_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md)    
[sys.dm_db_log_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-space-usage-transact-sql.md)     
[sys.dm_db_log_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-stats-transact-sql.md)     

