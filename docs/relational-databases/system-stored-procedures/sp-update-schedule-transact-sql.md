---
description: "sp_update_schedule (Transact-SQL)"
title: "sp_update_schedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_update_schedule"
  - "sp_update_schedule_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_update_schedule"
ms.assetid: 97b3119b-e43e-447a-bbfb-0b5499e2fefe
author: markingmyname
ms.author: maghan
---
# sp_update_schedule (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes the settings for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent schedule.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_update_schedule   
    {   [ @schedule_id = ] schedule_id   
      | [ @name = ] 'schedule_name' }  
    [ , [ @new_name = ] new_name ]  
    [ , [ @enabled = ] enabled ]  
    [ , [ @freq_type = ] freq_type ]  
    [ , [ @freq_interval = ] freq_interval ]   
    [ , [ @freq_subday_type = ] freq_subday_type ]   
    [ , [ @freq_subday_interval = ] freq_subday_interval ]   
    [ , [ @freq_relative_interval = ] freq_relative_interval ]   
    [ , [ @freq_recurrence_factor = ] freq_recurrence_factor ]   
    [ , [ @active_start_date = ] active_start_date ]   
    [ , [ @active_end_date = ] active_end_date ]   
    [ , [ @active_start_time = ] active_start_time ]   
    [ , [ @active_end_time = ] active_end_time ]   
    [ , [ @owner_login_name = ] 'owner_login_name' ]  
    [ , [ @automatic_post =] automatic_post ]  
```  
  
## Arguments  
`[ @schedule_id = ] schedule_id`
 The identifier of the schedule to modify. *schedule_id* is **int**, with no default. Either *schedule_id* or *schedule_name* must be specified.  
  
`[ @name = ] 'schedule_name'`
 The name of the schedule to modify. *schedule_name* is **sysname**, with no default. Either *schedule_id* or *schedule_name* must be specified.  
  
`[ @new_name = ] new_name`
 The new name for the schedule. *new_name* is **sysname**, with a default of NULL. When *new_name* is NULL, the name of the schedule is unchanged.  
  
`[ @enabled = ] enabled`
 Indicates the current status of the schedule. *enabled* is **tinyint**, with a default of **1** (enabled). If **0**, the schedule is not enabled. When the schedule is not enabled, no jobs will run on this schedule.  
  
`[ @freq_type = ] freq_type`
 A value indicating when a job is to be executed. *freq_type* is **int**, with a default of **0**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**4**|Daily|  
|**8**|Weekly|  
|**16**|Monthly|  
|**32**|Monthly, relative to *freq interval*|  
|**64**|Run when SQLServerAgent service starts|  
|**128**|Run when the computer is idle|  
  
`[ @freq_interval = ] freq_interval`
 The days that a job is executed. *freq_interval* is **int**, with a default of **0**, and depends on the value of *freq_type*.  
  
|Value of *freq_type*|Effect on *freq_interval*|  
|---------------------------|--------------------------------|  
|**1** (once)|*freq_interval* is unused.|  
|**4** (daily)|Every *freq_interval* days.|  
|**8** (weekly)|*freq_interval* is one or more of the following (combined with an **OR** logical operator):<br /><br /> **1** = Sunday<br /><br /> **2** = Monday<br /><br /> **4** = Tuesday<br /><br /> **8** = Wednesday<br /><br /> **16** = Thursday<br /><br /> **32** = Friday<br /><br /> **64** = Saturday|  
|**16** (monthly)|On the *freq_interval* day of the month.|  
|**32** (monthly relative)|*freq_interval* is one of the following:<br /><br /> **1** = Sunday<br /><br /> **2** = Monday<br /><br /> **3** = Tuesday<br /><br /> **4** = Wednesday<br /><br /> **5** = Thursday<br /><br /> **6** = Friday<br /><br /> **7** = Saturday<br /><br /> **8** = Day<br /><br /> **9** = Weekday<br /><br /> **10** = Weekend day|  
|**64** (when SQLServerAgent service starts)|*freq_interval* is unused.|  
|**128**|*freq_interval* is unused.|  
  
`[ @freq_subday_type = ] freq_subday_type`
 Specifies the units for *freq_subday_interval**.* *freq_subday_type*is **int**, with a default of **0**, and can be one of these values.  
  
|Value|Description (unit)|  
|-----------|--------------------------|  
|**0x1**|At the specified time|  
|**0x2**|Seconds|  
|**0x4**|Minutes|  
|**0x8**|Hours|  
  
`[ @freq_subday_interval = ] freq_subday_interval`
 The number of *freq_subday_type* periods to occur between each execution of a job. *freq_subday_interval*is **int**, with a default of **0**.  
  
`[ @freq_relative_interval = ] freq_relative_interval`
 A job's occurrence of *freq_interval* in each month, if *freq_interval* is **32** (monthly relative). *freq_relative_interval*is **int**, with a default of **0**, and can be one of these values.  
  
|Value|Description (unit)|  
|-----------|--------------------------|  
|**1**|First|  
|**2**|Second|  
|**4**|Third|  
|**8**|Fourth|  
|**16**|Last|  
  
`[ @freq_recurrence_factor = ] freq_recurrence_factor`
 The number of weeks or months between the scheduled execution of a job. *freq_recurrence_factor* is used only if *freq_type* is **8**, **16**, or **32**. *freq_recurrence_factor*is **int**, with a default of **0**.  
  
`[ @active_start_date = ] active_start_date`
 The date on which execution of a job can begin. *active_start_date*is **int**, with a default of NULL, which indicates today's date. The date is formatted as YYYYMMDD. If *active_start_date* is not NULL, the date must be greater than or equal to 19900101.  
  
 After the schedule is created, review the start date and confirm that it is the correct date. For more information, see the section "Scheduling Start Date" in [Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md).  
  
`[ @active_end_date = ] active_end_date`
 The date on which execution of a job can stop. *active_end_date*is **int**, with a default of **99991231**, which indicates December 31, 9999. Formatted as YYYYMMDD.  
  
`[ @active_start_time = ] active_start_time`
 The time on any day between *active_start_date* and *active_end_date* to begin execution of a job. *active_start_time*is **int**, with a default of 000000, which indicates 12:00:00 A.M. on a 24-hour clock, and must be entered using the form HHMMSS.  
  
`[ @active_end_time = ] active_end_time`
 The time on any day between *active_start_date* and *active_end_date* to end execution of a job. *active_end_time*is **int**, with a default of **235959**, which indicates 11:59:59 P.M. on a 24-hour clock, and must be entered using the form HHMMSS.  
  
`[ @owner_login_name = ] 'owner_login_name']`
 The name of the server principal that owns the schedule. *owner_login_name* is **sysname**, with a default of NULL, which indicates that the schedule is owned by the creator.  
  
`[ @automatic_post = ] automatic_post`
 Reserved.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 All jobs that use the schedule immediately use the new settings. However, changing a schedule does not stop jobs that are currently running.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
 For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
 Only members of **sysadmin** can modify a schedule owned by another user.  
  
## Examples  
 The following example changes the enabled status of the `NightlyJobs` schedule to `0` and sets the owner to `terrid`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_update_schedule  
    @name = 'NightlyJobs',  
    @enabled = 0,  
    @owner_login_name = 'terrid' ;  
GO  
```  
  
## See Also  
 [Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md)   
 [Schedule a Job](../../ssms/agent/schedule-a-job.md)   
 [Create a Schedule](../../ssms/agent/create-a-schedule.md)   
 [SQL Server Agent Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)   
 [sp_add_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)   
 [sp_add_jobschedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-jobschedule-transact-sql.md)   
 [sp_delete_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-schedule-transact-sql.md)   
 [sp_help_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-schedule-transact-sql.md)   
 [sp_attach_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-attach-schedule-transact-sql.md)  
  
  
