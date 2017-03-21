---
title: "DBCC SQLPERF (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 43
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# DBCC SQLPERF (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Provides transaction log space usage statistics for all databases. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] it can also be used to reset wait and latch statistics.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)), [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] ([Preview in some regions](http://azure.microsoft.com/documentation/articles/sql-database-preview-whats-new/?WT.mc_id=TSQL_GetItTag)).|  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DBCC SQLPERF   
(  
     [ LOGSPACE ]  
     |  
          [ "sys.dm_os_latch_stats" , CLEAR ]  
     |  
     [ "sys.dm_os_wait_stats" , CLEAR ]  
)   
     [WITH NO_INFOMSGS ]  
```  
  
## Arguments  
 LOGSPACE  
 Returns the current size of the transaction log and the percentage of log space used for each database. You can use this information to monitor the amount of space used in a transaction log.  
  
 **"** **sys.dm_os_latch_stats" ,** CLEAR  
 Resets the latch statistics. For more information, see [sys.dm_os_latch_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-latch-stats-transact-sql.md). This option is not available in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
 **"sys.dm_os_wait_stats" ,** CLEAR  
 Resets the wait statistics. For more information, see [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md). This option is not available in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
 WITH NO_INFOMSGS  
 Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Result Sets  
 The following table describes the columns in the result set.  
  
|Column name|Definition|  
|-----------------|----------------|  
|**Database Name**|Name of the database for the log statistics displayed.|  
|**Log Size (MB)**|Current size allocated to the log. This value is always smaller than the amount originally allocated for log space because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] reserves a small amount of disk space for internal header information.|  
|**Log Space Used (%)**|Percentage of the log file currently in use to store transaction log information.|  
|**Status**|Status of the log file. Always 0.|  
  
## Remarks  
 The transaction log records each transaction made in a database. For more information see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md).  
  
## Permissions  
 On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to run DBCC SQLPERF(LOGSPACE) requires VIEW SERVER STATE permission on the server. To reset wait and latch statistics requires ALTER SERVER STATE permission on the server.  
  
 On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Premium Tiers requires the VIEW DATABASE STATE permission in the database. On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Standard and Basic Tiers requires the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] admin account. Reset wait and latch statistics are not supported.  
  
## Examples  
  
### A. Displaying log space information for all databases  
 The following example displays `LOGSPACE` information for all databases contained in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```tsql  
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
  
```tsql  
DBCC SQLPERF("sys.dm_os_wait_stats",CLEAR);  
```  
  
## See Also  
 [DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)   
 [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)  
  
  