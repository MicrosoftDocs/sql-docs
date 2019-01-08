---
title: "sp_changepublication_snapshot (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changepublication_snapshot_TSQL"
  - "sp_changepublication_snapshot"
helpviewer_keywords: 
  - "sp_changepublication_snapshot"
ms.assetid: 518a4618-3592-4edc-8425-cbc33cdff891
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changepublication_snapshot (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes properties of the Snapshot Agent for the specified publication. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changepublication_snapshot [ @publication= ] 'publication'  
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
    [ , [ @snapshot_job_name = ] 'snapshot_agent_name' ]  
    [ , [ @publisher_security_mode = ] publisher_security_mode ]  
    [ , [ @publisher_login = ] 'publisher_login' ]  
    [ , [ @publisher_password = ] 'publisher_password' ]   
    [ , [ @job_login = ] 'job_login' ]  
    [ , [ @job_password = ] 'job_password' ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
 [ **@publication =**] **'***publication***'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@frequency_type =**] *frequency_type*  
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
  
 [ **@frequency_interval =**] *frequency_interval*  
 Specifies the days that the agent runs. *frequency_interval* is **int**, and can be one of the following values.  
  
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
  
 [ **@frequency_subday =**] *frequency_subday*  
 Is the units for *freq_subday_interval*. *frequency_subday* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4**|Minute|  
|**8**|Hour|  
|NULL (default)||  
  
 [ **@frequency_subday_interval =**] *frequency_subday_interval*  
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with a default of NULL.  
  
 [ **@frequency_relative_interval =**] *frequency_relative_interval*  
 Is the date the Snapshot Agent runs. *frequency_relative_interval* is **int**, with a default of NULL.  
  
 [ **@frequency_recurrence_factor =**] *frequency_recurrence_factor*  
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of NULL.  
  
 [ **@active_start_date =**] *active_start_date*  
 Is the date when the Snapshot Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of NULL.  
  
 [ **@active_end_date =**] *active_end_date*  
 Is the date when the Snapshot Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of NULL.  
  
 [ **@active_start_time_of_day =**] *active_start_time_of_day*  
 Is the time of day when the Snapshot Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of NULL.  
  
 [ **@active_end_time_of_day =**] *active_end_time_of_day*  
 Is the time of day when the Snapshot Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of NULL.  
  
 [ **@snapshot_job_name =** ] **'***snapshot_agent_name***'**  
 Is the name of an existing Snapshot Agent job name if an existing job is being used. *snapshot_agent_name* is **nvarchar(100)** with a default value of NULL.  
  
 [ **@publisher_security_mode =** ] *publisher_security_mode*  
 Is the security mode used by the agent when connecting to the Publisher. *publisher_security_mode* is **smallint**, with a default of NULL. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, and **1** specifies Windows Authentication. A value of **0** must be specified for non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
 [ **@publisher_login =** ] **'***publisher_login***'**  
 Is the login used when connecting to the Publisher. *publisher_login* is **sysname**, with a default of NULL. *publisher_login* must be specified when *publisher_security_mode* is **0**. If *publisher_login* is NULL and *publisher_security_mode* is **1**, then the Windows account specified in *job_login* is used when connecting to the Publisher.  
  
 [ **@publisher_password =** ] **'***publisher_password***'**  
 Is the password used when connecting to the Publisher. *publisher_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  Do not use a blank password. Use a strong password. When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
 [ **@job_login** = ] **'***job_login***'**  
 Is the login for the Windows account under which the agent runs. *job_login* is **nvarchar(257)**, with a default of NULL. This Windows account is always used for agent connections to the Distributor. You must supply this parameter when creating a new Snapshot Agent job. This cannot be changed for a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher.  
  
 [ **@job_password =** ] **'***job_password***'**  
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with a default of NULL. You must supply this parameter when creating a new Snapshot Agent job.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
 [ **@publisher =** ] **'***publisher***'**  
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when creating a Snapshot Agent at a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changepublication_snapshot** is used in snapshot replication, transactional replication, and merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_changepublication_snapshot**.  
  
## See Also  
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
