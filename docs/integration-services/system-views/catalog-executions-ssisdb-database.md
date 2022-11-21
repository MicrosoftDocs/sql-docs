---
description: "catalog.executions (SSISDB Database)"
title: "catalog.executions (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "04/13/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
helpviewer_keywords: 
  - "executions view [Integration Services]"
  - "catalog.executions view [Integration Services]"
ms.assetid: 879f13b0-331d-4dee-a079-edfaca11ae5b
author: chugugrace
ms.author: chugu
---
# catalog.executions (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

  Displays the instances of package execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog. Packages that are executed with the Execute Package task run in the same instance of execution as the parent package.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|execution_id|**bigint**|The unique identifier (ID) for the instance of execution.|  
|folder_name|**sysname(nvarchar(128))**|The name of the folder that contains the project.|  
|project_name|**sysname(nvarchar(128))**|The name of the project.|  
|package_name|**nvarchar(260)**|The name of the first package that was started during execution.|  
|reference_id|**bigint**|The environment that is referenced by the instance of execution.|  
|reference_type|**char(1)**|Indicates whether the environment can be located in the same folder as the project (relative reference) or in a different folder (absolute reference). When the value is `R`, the environment is located by using a relative reference. When the value is `A`, the environment is located by using an absolute reference.|  
|environment_folder_name|**nvarchar(128)**|The name of the folder that contains the environment.|  
|environment_name|**nvarchar(128)**|The name of the environment that was referenced during execution.|  
|project_lsn|**bigint**|The version of the project that corresponds with the instance of execution. This number is not guaranteed to be sequential.|  
|executed_as_sid|**varbinary(85)**|The SID of the user who started the instance of execution.|  
|executed_as_name|**nvarchar(128)**|The name of the database principal that was used to start the instance of execution.|  
|use32bitruntime|**bit**|Indicates if the 32-bit runtime is used to run the package on a 64-bit operating system. When the value is `1`, the execution is performed with the 32-bit runtime. When the value is `0`, the execution is performed with the 64-bit runtime.|  
|operation_type|**smallint**|The type of operation. The operation_type of 200 means create/start execution.|  
|created_time|**datetimeoffset**|The time when the instance was initiated.  Keep in mind that there is a lag time between initiation and actual start time.| 
|object_type|**smallint**|The type of object. The object may be a project (`20`) or a package (`30`).|  
|object_id|**bigint**|The ID of the object affected by the operation.|  
|status|**int**|The status of the operation. The possible values are created (`1`), running (`2`), canceled (`3`), failed (`4`), pending (`5`), ended unexpectedly (`6`), succeeded (`7`), stopping (`8`), and completed (`9`).|  
|start_time|**datetimeoffset**|The time when the instance of execution was started.|  
|end_time|**datetimeoffsset**|The time when the instance of execution ended.|  
|caller_sid|**varbinary(85)**|The security ID (SID) of the user if Windows Authentication was used to log on.|  
|caller_name|**nvarchar(128)**|The name of the account that performed the operation.|  
|process_id|**int**|The process ID of the external process, if applicable.|  
|stopped_by_sid|**varbinary(85)**|The security ID (SID) of the user who stopped the instance of execution.|  
|stopped_by_name|**nvarchar(128)**|The name of the user who stopped the instance of execution.|  
|dump_id|**uniqueidentifier**|The ID of an execution dump.|
|server_name|**nvarchar(128)**|The Windows server and instance information for a specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|machine_name|**nvarchar(128)**|The computer name on which the server instance is running.|
|worker_agent_id|**uniqueidentifier**|The worker agent ID of Scale Out Worker.|
|total_physical_memory_kb|**bigint**|The total physical memory (in kilobytes) on the server when the execution is started.|  
|available_physical_memory_kb|**bigint**|The available physical memory (in kilobytes) on the server when the execution is started.|  
|total_page_file_kb|**bigint**|The total page memory (in kilobytes) on the server when the execution is started.|  
|available_page_file_kb|**bigint**|The available page memory (in kilobytes) on the server when the execution is started.|  
|cpu_count|**int**|The number of logical CPUs on the server when the execution is started.|   
|executed_count|**int**|The number of times the operation was executed.|   
  
  
## Remarks  
 This view displays a row for each instance of execution in the catalog.  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the instance of execution  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
> [!NOTE]  
>  Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
