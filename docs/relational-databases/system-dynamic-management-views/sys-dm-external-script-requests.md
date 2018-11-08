---
title: "sys.dm_external_script_requests | Microsoft Docs"
ms.custom: ""
ms.date: "10/28/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
  
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_external_script_requests"
  - "sys.dm_external_script_requests_TSQL"
  - "dm_external_script_requests"
  - "dm_external_script_requests_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_external_script_requests dynamic management view"
ms.assetid: e7e7c50f-b8b2-403c-b8c8-1955da5636c3
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_external_script_requests
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

Returns a row for each active worker account that is running an external script.
  
> [!NOTE] 
>  
> This dynamic management view (DMV) is available only if you have installed and enabled the feature that supports external script execution. For more information, see [R Services in SQL Server 2016](../../advanced-analytics/r/sql-server-r-services.md) and  [Machine Learning Services (R, Python) in SQL Server 2017](../../advanced-analytics/what-is-sql-server-machine-learning.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|external_script_request_id|**unique identifier**|ID of the process that sent the external script request. This corresponds to the process ID as received by [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]|  
|language|**nvarchar**|Keyword that represents a supported script language. |  
|degree_of_parallelism|**int**|Number indicating the number of parallel processes that were created. This value might be different from the number of parallel processes that were requested.|  
|external_user_name|**nvarchar**|The Windows worker account under which the script was executed.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on server.  
  
> [!NOTE]
>   
>  Users who run external scripts must have the additional permission EXECUTE ANY EXTERNAL SCRIPT, however, this DMV can be used by administrators without this permission. 
  
## Remarks  

This view can be filtered using the script language identifier.

The view also returns the worker account under which the script is being run. For information about worker accounts used by the external scripts, see the Identities used in processing (SQLRUserGroup) section in [Security overview for the extensibility framework in SQL Server Machine Learning Services](../../advanced-analytics/concepts/security.md#sqlrusergroup).

The GUID that is returned in the **external_script_request_id** field also represents the file name of the secured directory where temporary files are stored. Each worker account such as MSSQLSERVER01 represents a single SQL login or Windows user, and might be used to run multiple script requests. By default, these temporary files are cleaned up after completion of the requested script.
 
This DMV monitors only active processes and cannot report on scripts that have already completed. If you need to track the duration of scripts, we recommend that you add timing information into your script and capture that as part of script execution.

## Examples  
  
### Viewing the currently active R scripts for a particular process 
 The following example displays the number of external script executions being run on the current instance.  
  
```  
SELECT external_script_request_id 
  , [language]
  , degree_of_parallelism
  , external_user_name
FROM sys.dm_external_script_requests; 
```  

Results  


external_script_request_id  |language  |degree_of_parallelism  |external_user_name  
---------|---------|---------|---------
183EE6FC-7399-4318-AA2E-7A6C68E435A8     |     R    |      1   |  MSSQLSERVER01       


  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)  
[sys.dm_external_script_execution_stats](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-execution-stats.md)
[sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)  
  

