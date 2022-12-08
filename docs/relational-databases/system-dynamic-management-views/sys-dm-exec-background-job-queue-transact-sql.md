---
title: "sys.dm_exec_background_job_queue (Transact-SQL)"
description: sys.dm_exec_background_job_queue (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/09/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_exec_background_job_queue"
  - "sys.dm_exec_background_job_queue_TSQL"
  - "dm_exec_background_job_queue_TSQL"
  - "sys.dm_exec_background_job_queue"
helpviewer_keywords:
  - "sys.dm_exec_background_job_queue dynamic management function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_background_job_queue (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a row for each query processor job that is scheduled for asynchronous (background) execution.  
  
> [!NOTE]
> To call this from **[!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)]** or **[!INCLUDE[ssPDW](../../includes/sspdw-md.md)]**, use the name `sys.dm_pdw_nodes_exec_background_job_queue`. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**time_queued**|**datetime**|Time when the job was added to the queue.|  
|**job_id**|**int**|Job identifier.|  
|**database_id**|**int**|Database on which the job is to execute.|  
|**object_id1**|**int**|Value depends on the job type. For more information, see the Remarks section.|  
|**object_id2**|**int**|Value depends on the job type. For more information, see the Remarks section.|  
|**object_id3**|**int**|Value depends on the job type. For more information, see the Remarks section.|  
|**object_id4**|**int**|Value depends on the job type. For more information, see the Remarks section.|  
|**error_code**|**int**|Error code if the job reinserted due to failure. NULL if suspended, not picked up, or completed.|  
|**request_type**|**smallint**|Type of the job request.|  
|**retry_count**|**smallint**|Number of times the job was picked from the queue and reinserted because of lack of resources or other reasons.|  
|**in_progress**|**smallint**|Indicates whether the job has started execution.<br /><br /> 1 = Started<br /><br /> 0 = Still waiting|  
|**session_id**|**smallint**|Session identifier.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## Remarks  
 This view returns information only for asynchronous update statistics jobs. For more information about asynchronous update statistics, see [Statistics](../../relational-databases/statistics/statistics.md).  
  
 The values of **object_id1** through **object_id4** depend on the type of the job request. The following table summarizes the meaning of these columns for the different job types.  
  
|Request type|object_id1|object_id2|object_id3|object_id4|  
|------------------|-----------------|-----------------|-----------------|-----------------|  
|Asynchronous update statistics|Table or view ID|Statistics ID|Not used|Not used|  
  
## Examples  
 The following example returns the number of active asynchronous jobs in the background queue for each database in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```tsql  
SELECT DB_NAME(database_id) AS [Database], COUNT(*) AS [Active Async Jobs]  
FROM sys.dm_exec_background_job_queue  
WHERE in_progress = 1  
GROUP BY database_id;  
GO  
```  
  
## See also  
 - [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 - [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 - [Statistics](../../relational-databases/statistics/statistics.md)   
 - [KILL STATS JOB &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-stats-job-transact-sql.md)  
  
