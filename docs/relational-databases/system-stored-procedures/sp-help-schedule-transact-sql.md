---
description: "sp_help_schedule (Transact-SQL)"
title: "sp_help_schedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_help_schedule"
  - "sp_help_schedule_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_schedule"
ms.assetid: b2fc4ce1-0a8e-44d2-b206-7dc7b258d8c9
author: markingmyname
ms.author: maghan
---
# sp_help_schedule (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Lists information about schedules.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_schedule   
     [ @schedule_id = ] id ,  
     [ @schedule_name = ] 'schedule_name'   
     [ , [ @attached_schedules_only = ] attached_schedules_only ]  
     [ , [ @include_description = ] include_description ]  
```  
  
## Arguments  
`[ @schedule_id = ] id`
 The identifier of the schedule to list. *schedule_name* is **int**, with no default. Either *schedule_id* or *schedule_name* may be specified.  
  
`[ @schedule_name = ] 'schedule_name'`
 The name of the schedule to list. *schedule_name* is **sysname**, with no default. Either *schedule_id* or *schedule_name* may be specified.  
  
`[ @attached_schedules_only = ] attached_schedules_only ]`
 Specifies whether to show only schedules that a job is attached to. *attached_schedules_only* is **bit**, with a default of **0**. When *attached_schedules_only* is **0**, all schedules are shown. When *attached_schedules_only* is **1**, the result set contains only schedules that are attached to a job.  
  
`[ @include_description = ] include_description`
 Specifies whether to include descriptions in the result set. *include_description* is **bit**, with a default of **0**. When *include_description* is **0**, the *schedule_description* column of the result set contains a placeholder. When *include_description* is **1**, the description of the schedule is included in the result set.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 This procedure returns the following result set:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**schedule_id**|**int**|Schedule identifier number.|  
|**schedule_uid**|**uniqueidentifier**|Identifier for the schedule.|  
|**schedule_name**|**sysname**|Name of the schedule.|  
|**enabled**|**int**|Whether the schedule enabled (**1**) or not enabled (**0**).|  
|**freq_type**|**int**|Value indicating when the job is to be executed.<br /><br /> **1** = Once<br /><br /> **4** = Daily<br /><br /> **8** = Weekly<br /><br /> **16** = Monthly<br /><br /> **32** = Monthly, relative to the **freq_interval**<br /><br /> **64** = Run when SQLServerAgent service starts.|  
|**freq_interval**|**int**|Days when the job is executed. The value depends on the value of **freq_type**. For more information, see [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md).|  
|**freq_subday_type**|**int**|Units for **freq_subday_interval**. For more information, see [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md).|  
|**freq_subday_interval**|**int**|Number of **freq_subday_type** periods to occur between each execution of the job. For more information, see [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md).|  
|**freq_relative_interval**|**int**|Scheduled job's occurrence of the **freq_interval** in each month. For more information, see [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md).|  
|**freq_recurrence_factor**|**int**|Number of months between the scheduled execution of the job.|  
|**active_start_date**|**int**|Date the schedule is activated.|  
|**active_end_date**|**int**|End date of the schedule.|  
|**active_start_time**|**int**|Time of the day the schedule starts.|  
|**active_end_time**|**int**|Time of the day schedule ends.|  
|**date_created**|**datetime**|Date the schedule is created.|  
|**schedule_description**|**nvarchar(4000)**|An English description of the schedule (if requested).|  
|**job_count**|**int**|Returns how many jobs reference this schedule.|  
  
## Remarks  
 When no parameters are provided, **sp_help_schedule** lists information for all schedules in the instance.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Members of **SQLAgentUserRole** can only view the schedules that they own.  
  
## Examples  
  
### A. Listing information for all schedules in the instance  
 The following example lists information for all schedules in the instance.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_schedule ;  
GO  
```  
  
### B. Listing information for a specific schedule  
 The following example lists information for the schedule named `NightlyJobs`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_schedule  
    @schedule_name = N'NightlyJobs' ;  
GO  
```  
  
## See Also  
 [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)   
 [sp_attach_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-attach-schedule-transact-sql.md)   
 [sp_delete_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-schedule-transact-sql.md)   
 [sp_detach_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-detach-schedule-transact-sql.md)  
  
  
