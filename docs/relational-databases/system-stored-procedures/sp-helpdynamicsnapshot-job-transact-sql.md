---
title: "sp_helpdynamicsnapshot_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpdynamicsnapshot_TSQL"
  - "sp_helpdynamicsnapshot_job_TSQL"
  - "job_TSQL"
  - "helpdynamicsnapshot"
  - "job"
  - "sp_helpdynamicsnapshot"
  - "sp_helpdynamicsnapshot_job"
  - "helpdynamicsnapshot_TSQL"
helpviewer_keywords: 
  - "sp_helpdynamicsnapshot_job"
ms.assetid: d6dfdf26-f874-495f-a8a6-8780699646d7
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpdynamicsnapshot_job (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information on agent jobs that generate filtered data snapshots. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpdynamicsnapshot_job [ [ @publication = ] 'publication' ]   
    [ , [ @dynamic_snapshot_jobname = ] 'dynamic_snapshot_jobname' ]  
    [ , [ @dynamic_snapshot_jobid = ] 'dynamic_snapshot_jobid' ]  
```  
  
## Arguments  
 [ **@publication =** ] **'***publication***'**  
 Is the name of the publication. *publication* is **sysname**, with a default of **%**, which returns information on all filtered data snapshot jobs that match the specified *dynamic_snapshot_jobid*and *dynamic_snapshot_jobname*for all publications.  
  
 [ **@dynamic_snapshot_jobname =** ] **'***dynamic_snapshot_jobname***'**  
 Is the name of a filtered data snapshot job. *dynamic_snapshot_jobname*is **sysname**, with default of **%**', which returns all dynamic jobs for a publication with the specified *dynamic_snapshot_jobid*. If a job name was not explicitly specified when the job was created, the job name is in the following format:  
  
```  
'dyn_' + <name of the standard snapshot job> + <GUID>  
```  
  
 [ **@dynamic_snapshot_jobid =** ] **'***dynamic_snapshot_jobid***'**  
 Is an identifier for a filtered data snapshot job. *dynamic_snapshot_jobid*is **uniqueidentifier**, with default of NULL, which returns all snapshot jobs that match the specified *dynamic_snapshot_jobname*.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|Identifies the filtered data snapshot job.|  
|**job_name**|**sysname**|Name of the filtered data snapshot job.|  
|**job_id**|**uniqueidentifier**|Identifies the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job at the Distributor.|  
|**dynamic_filter_login**|**sysname**|Value used for evaluating the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function in a parameterized row filter defined for the publication.|  
|**dynamic_filter_hostname**|**sysname**|Value used for evaluating the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function in a parameterized row filter defined for the publication.|  
|**dynamic_snapshot_location**|**nvarchar(255)**|Path to the folder where the snapshot files are read from if a parameterized row filter is used.|  
|**frequency_type**|**int**|Is the frequency with which the agent is scheduled to run, which can be one of these values.<br /><br /> **1** = One time<br /><br /> **2** = On demand<br /><br /> **4** = Daily<br /><br /> **8** = Weekly<br /><br /> **16** = Monthly<br /><br /> **32** = Monthly relative<br /><br /> **64** = Autostart<br /><br /> **128** = Recurring|  
|**frequency_interval**|**int**|The days that the agent runs, which can be one of these values.<br /><br /> **1** = Sunday<br /><br /> **2** = Monday<br /><br /> **3** = Tuesday<br /><br /> **4** = Wednesday<br /><br /> **5** = Thursday<br /><br /> **6** = Friday<br /><br /> **7** = Saturday<br /><br /> **8** = Day<br /><br /> **9** = Weekdays<br /><br /> **10** = Weekend days|  
|**frequency_subday_type**|**int**|Is the type that defines how often the agent runs when *frequency_type* is **4** (daily), and can be one of these values.<br /><br /> **1** = At the specified time<br /><br /> **2** = Seconds<br /><br /> **4** = Minutes<br /><br /> **8** = Hours|  
|**frequency_subday_interval**|**int**|Number of intervals of *frequency_subday_type* that occur between scheduled execution of the agent.|  
|**frequency_relative_interval**|**int**|Is the week that the agent runs in a given month when *frequency_type* is **32** (monthly relative), and can be one of these values.<br /><br /> **1** = First<br /><br /> **2** = Second<br /><br /> **4** = Third<br /><br /> **8** = Fourth<br /><br /> **16** = Last|  
|**frequency_recurrence_factor**|**int**|Number of weeks or months between the scheduled execution of the agent.|  
|**active_start_date**|**int**|Date when the agent is first scheduled to run, formatted as YYYYMMDD.|  
|**active_end_date**|**int**|Date when the agent is last scheduled to run, formatted as YYYYMMDD.|  
|**active_start_time**|**int**|Time when the agent is first scheduled to run, formatted as HHMMSS.|  
|**active_end_time**|**int**|Time when the agent is last scheduled to run, formatted as HHMMSS.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpdynamicsnapshot_job** is used in merge replication.  
  
 If all of the default parameter values are used, information on all partitioned data snapshot jobs for the entire publication database is returned.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, and the publication access list for the publication can execute **sp_helpdynamicsnapshot_job**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
