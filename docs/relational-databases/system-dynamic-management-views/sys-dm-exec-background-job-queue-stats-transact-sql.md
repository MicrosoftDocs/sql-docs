---
title: "sys.dm_exec_background_job_queue_stats (Transact-SQL)"
description: sys.dm_exec_background_job_queue_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_exec_background_job_queue_stats"
  - "sys.dm_exec_background_job_queue_stats"
  - "dm_exec_background_job_queue_stats_TSQL"
  - "sys.dm_exec_background_job_queue_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_exec_background_job_queue_stats dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 27f62ab5-46c4-417e-814d-8d6437034d1c
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_background_job_queue_stats (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a row that provides aggregate statistics for each query processor job submitted for asynchronous (background) execution.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_exec_background_job_queue_stats**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**queue_max_len**|**int**|Maximum length of the queue.|  
|**enqueued_count**|**int**|Number of requests successfully posted to the queue.|  
|**started_count**|**int**|Number of requests that started execution.|  
|**ended_count**|**int**|Number of requests serviced to either success or failure.|  
|**failed_lock_count**|**int**|Number of requests that failed due to lock contention or deadlock.|  
|**failed_other_count**|**int**|Number of requests that failed due to other reasons.|  
|**failed_giveup_count**|**int**|Number of requests that failed because retry limit has been reached.|  
|**enqueue_failed_full_count**|**int**|Number of failed enqueue attempts because the queue is full.|  
|**enqueue_failed_duplicate_count**|**int**|Number of duplicate enqueue attempts.|  
|**elapsed_avg_ms**|**int**|Average elapsed time of request in milliseconds.|  
|**elapsed_max_ms**|**int**|Elapsed time of the longest request in milliseconds.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Remarks  
 This view returns information only for asynchronous update statistics jobs. For more information about asynchronous update statistics, see [Statistics](../../relational-databases/statistics/statistics.md).  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Examples  
  
### A. Determining the percentage of failed background jobs  
 The following example returns the percentage of failed background jobs for all executed queries.  
  
```  
SELECT   
        CASE ended_count WHEN 0   
                THEN 'No jobs ended'   
                ELSE CAST((failed_lock_count + failed_giveup_count + failed_other_count) / CAST(ended_count AS float) * 100 AS varchar(20))   
        END AS [Percent Failed]  
FROM sys.dm_exec_background_job_queue_stats;  
GO  
```  
  
### B. Determining the percentage of failed enqueue attempts  
 The following example returns the percentage of failed enqueue attempts for all executed queries.  
  
```  
SELECT   
        CASE enqueued_count WHEN 0   
                THEN 'No jobs posted'   
                ELSE CAST((enqueue_failed_full_count + enqueue_failed_duplicate_count) / CAST(enqueued_count + enqueue_failed_full_count + enqueue_failed_duplicate_count AS float) * 100 AS varchar(20))   
        END AS [Percent Enqueue Failed]  
FROM sys.dm_exec_background_job_queue_stats;  
GO  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)  
  
