---
title: "catalog.execution_component_phases | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 07a9a163-4787-40f7-b371-ac5c6cb4b095
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.execution_component_phases
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the time spent by a data flow component in each execution phase.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|phase_stats_id|**bigint**|Unique identifier (ID) of the phase.|  
|execution_id|**bigint**|Unique ID for the instance of execution.|  
|package_name|**nvarchar(260)**|The name of the first package that was started during execution.|  
|task_name|**nvarchar(4000)**|The name of the data flow task.|  
|subcomponent_name|**nvarchar(4000)**|The name of the data flow component.|  
|phase|**nvarchar(128)**|The name of the execution phase.|  
|start_time|**datetimeoffset(7)**|The time when the phase started.|  
|end_time|**datetimeoffset(7)**|The time when the phase ended.|  
|execution_path|**nvarchar(max)**|The execution path of the data flow task.|  
  
## Remarks  
 This view displays a row for each execution phase of a data flow component, such as Validate, Pre-Execute, Post-Execute, PrimeOutput, and ProcessInput. Each row displays the start and end time for a specific execution phase.  
  
## Example  
 The following example uses the catalog.execution_component_phases view to find the total amount of time that a specific package has spent executing in all phases (**active_time**), and the total elapsed time for the package (**total_time**).  
  
> [!WARNING]  
>  The catalog.execution_component_phases view provides this information when the logging level of the package execution is set to Performance or Verbose. For more information, see [Enable Logging for Package Execution on the SSIS Server](../../integration-services/performance/integration-services-ssis-logging.md#server_logging).  
  
```sql
use SSISDB  
select package_name, task_name, subcomponent_name, execution_path,  
    SUM(DATEDIFF(ms,start_time,end_time)) as active_time,  
    DATEDIFF(ms,min(start_time), max(end_time)) as total_time  
from catalog.execution_component_phases  
where execution_id = 1841  
group by package_name, task_name, subcomponent_name, execution_path  
order by package_name, task_name, subcomponent_name, execution_path  
```  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the instance of execution  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
> [!NOTE]  
>  When you have permission to perform an operation on the server, you also have permission to view information about the operation. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
