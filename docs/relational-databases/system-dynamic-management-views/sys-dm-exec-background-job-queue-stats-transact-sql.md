---
title: "sys.dm_exec_background_job_queue_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "dm_exec_background_job_queue_stats"
  - "sys.dm_exec_background_job_queue_stats"
  - "dm_exec_background_job_queue_stats_TSQL"
  - "sys.dm_exec_background_job_queue_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_background_job_queue_stats dynamic management view"
ms.assetid: 27f62ab5-46c4-417e-814d-8d6437034d1c
caps.latest.revision: 27
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sys.dm_exec_background_job_queue_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a row that provides aggregate statistics for each query processor job submitted for asynchronous (background) execution.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_exec_background_job_queue_stats**.  
  
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
 On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires VIEW SERVER STATE permission on the server.  
  
 On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Premium Tiers requires the VIEW DATABASE STATE permission in the database. On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Standard and Basic Tiers requires the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] admin account.  
  
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
                ELSE CAST((enqueue_failed_full_count + enqueue_failed_duplicate_count) / CAST(enqueued_count AS float) * 100 AS varchar(20))   
        END AS [Percent Enqueue Failed]  
FROM sys.dm_exec_background_job_queue_stats;  
GO  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  


