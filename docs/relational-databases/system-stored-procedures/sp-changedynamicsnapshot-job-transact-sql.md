---
description: "sp_changedynamicsnapshot_job (Transact-SQL)"
title: "sp_changedynamicsnapshot_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_changedynamicsnapshot_job"
  - "sp_changedynamicsnapshot_job_TSQL"
helpviewer_keywords: 
  - "sp_changedynamicsnapshot_job"
ms.assetid: ea0dacd2-a5fd-42f4-88dd-7d289b0ae017
author: markingmyname
ms.author: maghan
---
# sp_changedynamicsnapshot_job (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Modifies the agent job that generates the snapshot for a subscription to a publication with a parameterized row filter. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changedynamicsnapshot_job [ @publication = ] 'publication'  
    [ , [ @dynamic_snapshot_jobname = ] 'dynamic_snapshot_jobname' ]  
    [ , [ @dynamic_snapshot_jobid = ] 'dynamic_snapshot_jobid' ]  
    [ , [ @frequency_type = ] frequency_type ]   
    [ , [ @frequency_interval = ] frequency_interval ]   
    [ , [ @frequency_subday = ] frequency_subday ]   
    [ , [ @frequency_subday_interval = ] frequency_subday_interval ]   
    [ , [ @frequency_relative_interval = ] frequency_relative_interval ]   
    [ , [ @frequency_recurrence_factor = ] frequency_recurrence_factor ]   
    [ , [ @active_start_date = ] active_start_date ]   
    [ , [ @active_end_date = ] active_end_date ]   
    [ , [ @active_start_time_of_day = ] active_start_time_of_day ]   
    [ , [ @active_end_time_of_day = ] active_end_time_of_day ]   
    [ , [ @job_login = ] 'job_login' ]   
    [ , [ @job_password = ] 'job_password' ]   
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @dynamic_snapshot_jobname = ] 'dynamic_snapshot_jobname'`
 Is the name of the snapshot job being changed. *dynamic_snapshot_jobname*is **sysname**, with default value of N'%'. If *dynamic_snapshot_jobid* is specified, you must use the default value for *dynamic_snapshot_jobname*.  
  
`[ @dynamic_snapshot_jobid = ] 'dynamic_snapshot_jobid'`
 Is the ID of the snapshot job being changed. *dynamic_snapshot_jobid* is **uniqueidentifier**, with default value of NULL. If *dynamic_snapshot_jobname*is specified, you must use the default value for *dynamic_snapshot_jobid*.  
  
`[ @frequency_type = ] frequency_type`
 Is the frequency with which to schedule the agent. *frequency_type* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|One time|  
|**2**|On demand|  
|**4**|Daily|  
|**8**|Weekly|  
|**16**|Monthly|  
|**32**|Monthly relative|  
|**64**|Autostart|  
|**128**|Recurring|  
|NULL (default)||  
  
`[ @frequency_interval = ] frequency_interval`
 The days that the agent runs. *frequency_interval* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Sunday|  
|**2**|Monday|  
|**3**|Tuesday|  
|**4**|Wednesday|  
|**5**|Thursday|  
|**6**|Friday|  
|**7**|Saturday|  
|**8**|Day|  
|**9**|Weekdays|  
|**10**|Weekend days|  
|NULL (default)||  
  
`[ @frequency_subday = ] frequency_subday`
 Is how often to reschedule during the defined period. *frequency_subday* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4**|Minute|  
|**8**|Hour|  
|NULL (default)||  
  
`[ @frequency_subday_interval = ] frequency_subday_interval`
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with a default of NULL.  
  
`[ @frequency_relative_interval = ] frequency_relative_interval`
 Is the date that the Merge Agent runs. This parameter is used when *frequency_type* is set to **32** (monthly relative). *frequency_relative_interval* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|First|  
|**2**|Second|  
|**4**|Third|  
|**8**|Fourth|  
|**16**|Last|  
|NULL (default)||  
  
`[ @frequency_recurrence_factor = ] frequency_recurrence_factor`
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of NULL.  
  
`[ @active_start_date = ] active_start_date`
 Is the date when the Merge Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of NULL.  
  
`[ @active_end_date = ] active_end_date`
 Is the date when the Merge Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of NULL.  
  
`[ @active_start_time_of_day = ] active_start_time_of_day`
 Is the time of day when the Merge Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_end_time_of_day = ] active_end_time_of_day`
 Is the time of day when the Merge Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of NULL.  
  
`[ @job_login = ] 'job_login'`
 Is the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Account under which the Snapshot Agent runs when generating the snapshot for a subscription using a parameterized row filter. *job_login* is **nvarchar(257)**, with a default value of NULL.  
  
`[ @job_password = ] 'job_password'`
 Is the password for the Windows Account under which the Snapshot Agent runs when generating the snapshot for a subscription using a parameterized row filter. *job_password* is **nvarchar(257)**, with a default value of NULL.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changedynamicsnapshot_job** is used in merge replication for publications with parameterized row filters.  
  
 After changing an agent login or password, you must stop and restart the agent before the change takes effect.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_changedynamicsnapshot_job**.  
  
## See Also  
 [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [Snapshots for Merge Publications with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md)  
  
  
