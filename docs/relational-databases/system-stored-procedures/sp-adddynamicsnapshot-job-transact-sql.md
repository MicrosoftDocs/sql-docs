---
title: "sp_adddynamicsnapshot_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_adddynamicsnapshot_job"
  - "sp_adddynamicsnapshot_job_TSQL"
helpviewer_keywords: 
  - "sp_adddynamicsnapshot_job"
ms.assetid: ef50ccf6-e360-4e4b-91b9-6706b8fabefa
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# sp_adddynamicsnapshot_job (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates an agent job that generates a filtered data snapshot for a publication with parameterized row filters. This stored procedure is executed at the Publisher on the publication database. This stored procedure is used by an administrator to manually create filtered data snapshot jobs for Subscribers.  
  
> [!NOTE]  
>  In order for a filtered data snapshot job to be created, a standard snapshot job for the publication must already exist.  
  
 For more information, see [Snapshots for Merge Publications with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_adddynamicsnapshot_job [ @publication = ] 'publication'   
    [ , [ @suser_sname = ] 'suser_sname' ]   
    [ , [ @host_name = ] 'host_name' ]   
    [ , [ @dynamic_snapshot_jobname = ] 'dynamic_snapshot_jobname' OUTPUT ]   
    [ , [ @dynamic_snapshot_jobid = ] 'dynamic_snapshot_jobid' OUTPUT ]   
    [ , [ @frequency_type= ] frequency_type ]  
    [ , [ @frequency_interval= ] frequency_interval ]  
    [ , [ @frequency_subday= ] frequency_subday ]  
    [ , [ @frequency_subday_interval= ] frequency_subday_interval ]  
    [ , [ @frequency_relative_interval= ] frequency_relative_interval ]  
    [ , [ @frequency_recurrence_factor= ] frequency_recurrence_factor ]  
    [ , [ @active_start_date= ] active_start_date ]  
    [ , [ @active_end_date= ] active_end_date ]  
    [ , [ @active_start_time_of_day= ] active_start_time_of_day ]  
    [ , [ @active_end_time_of_day= ] active_end_time_of_day ]  
```  
  
## Arguments  
 [ **@publication=**] **'***publication***'**  
 Is the name of the publication to which the filtered data snapshot job is being added. *publication* is **sysname**, with no default.  
  
 [ **@suser_sname**= ] **'***suser_sname***'**  
 Is the value used when creating a filtered data snapshot for a subscription that is filtered by the value of the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function at the Subscriber. *suser_sname* is **sysname**, with no default. *suser_sname* should be NULL if this function is not used to dynamically filter the publication.  
  
 [ **@host_name**= ] **'***host_name***'**  
 Is the value used when creating a filtered data snapshot for a subscription that is filtered by the value of the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function at the Subscriber. *host_name* is **sysname**, with no default. *host_name* should be NULL if this function is not used to dynamically filter the publication.  
  
 [ **@dynamic_snapshot_jobname**= ] **'***dynamic_snapshot_jobname***'**  
 Is the name of the filtered data snapshot job created. *dynamic_snapshot_jobname* is **sysname**, with default of NULL, and is an optional OUTPUT parameter. If specified, *dynamic_snapshot_jobname* must resolve to a unique job at the Distributor. If unspecified, a job name will be automatically generated and returned in the result set, where the name is created as follows:  
  
```  
'dyn_' + <name of the standard snapshot job> + <GUID>  
```  
  
> [!NOTE]  
>  When generating the name of the dynamic snapshot job, you may truncate the name of the standard snapshot job.  
  
 [ **@dynamic_snapshot_jobid**= ] **'***dynamic_snapshot_jobid***'**  
 Is an identifier for the filtered data snapshot job created. *dynamic_snapshot_jobid* is **uniqueidentifier**, with default of NULL, and is an optional OUTPUT parameter.  
  
 [ **@frequency_type=**] *frequency_type*  
 Is the frequency with which to schedule the filtered data snapshot job. *frequency_type* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|One time|  
|**2**|On demand|  
|**4** (default)|Daily|  
|**8**|Weekly|  
|**16**|Monthly|  
|**32**|Monthly relative|  
|**64**|Autostart|  
|**128**|Recurring|  
  
 [ **@frequency_interval =** ] *frequency_interval*  
 Is the period (measured in days) when the filtered data snapshot job is executed. *frequency_interval* is **int**, with a default value of 1, and depends on the value of *frequency_type*.  
  
|Value of *frequency_type*|Effect on *frequency_interval*|  
|--------------------------------|-------------------------------------|  
|**1**|*frequency_interval* is unused.|  
|**4** (default)|Every *frequency_interval* days, with a default of daily.|  
|**8**|*frequency_interval* is one or more of the following (combined with a [&#124; &#40;Bitwise OR&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-or-transact-sql.md) logical operator):<br /><br /> **1** = Sunday &#124; **2** = Monday &#124; **4** = Tuesday &#124; **8** = Wednesday &#124; **16** = Thursday &#124; **32** = Friday &#124; **64** = Saturday|  
|**16**|On the *frequency_interval* day of the month.|  
|**32**|*frequency_interval* is one of the following:<br /><br /> **1** = Sunday &#124; **2** = Monday &#124; **3** = Tuesday &#124; **4** = Wednesday &#124; **5** = Thursday &#124; **6** = Friday &#124; **7** = Saturday &#124; **8** = Day &#124; **9** = Weekday &#124; **10** = Weekend day|  
|**64**|*frequency_interval* is unused.|  
|**128**|*frequency_interval* is unused.|  
  
 [ **@frequency_subday=**] *frequency_subday*  
 Specifies the units for *frequency_subday_interval*. *frequency_subday* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4** (default)|Minute|  
|**8**|Hour|  
  
 [ **@frequency_subday_interval=**] *frequency_subday_interval*  
 Is the number of *frequency_subday* periods that occur between each execution of the job. *frequency_subday_interval* is **int**, with a default of 5.  
  
 [ **@frequency_relative_interval=**] *frequency_relative_interval*  
 Is the occurrence of the filtered data snapshot job in each month. This parameter is used when *frequency_type* is set to **32** (monthly relative). *frequency_relative_interval* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1** (default)|First|  
|**2**|Second|  
|**4**|Third|  
|**8**|Fourth|  
|**16**|Last|  
  
 [ **@frequency_recurrence_factor=**] *frequency_recurrence_factor*  
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of 0.  
  
 [ **@active_start_date=**] *active_start_date*  
 Is the date when the filtered data snapshot job is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of NULL.  
  
 [ **@active_end_date=**] *active_end_date*  
 Is the date when the filtered data snapshot job stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of NULL.  
  
 [ **@active_start_time_of_day=**] *active_start_time_of_day*  
 Is the time of day when the filtered data snapshot job is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of NULL.  
  
 [ **@active_end_time_of_day=**] *active_end_time_of_day*  
 Is the time of day when the filtered data snapshot job stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of NULL.  
  
## Result Set  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|Identifies the filtered data snapshot job in the [MSdynamicsnapshotjobs](../../relational-databases/system-tables/msdynamicsnapshotjobs-transact-sql.md) system table.|  
|**dynamic_snapshot_jobname**|**sysname**|Name of the filtered data snapshot job.|  
|**dynamic_snapshot_jobid**|**uniqueidentifier**|Uniquely identifies the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job at the Distributor.|  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_adddynamicsnapshot_job** is used in merge replication for publications that use a parameterized filter.  
  
## Example  
 [!code-sql[HowTo#sp_MergeDynamicPubPlusPartition](../../relational-databases/replication/codesnippet/tsql/sp-adddynamicsnapshot-jo_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_adddynamicsnapshot_job**.  
  
## See Also  
 [Create a Snapshot for a Merge Publication with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md)   
 [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md)   
 [sp_dropdynamicsnapshot_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdynamicsnapshot-job-transact-sql.md)   
 [sp_helpdynamicsnapshot_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdynamicsnapshot-job-transact-sql.md)  
  
  
