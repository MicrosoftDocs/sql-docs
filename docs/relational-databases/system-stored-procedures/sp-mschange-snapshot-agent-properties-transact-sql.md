---
title: "sp_MSchange_snapshot_agent_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_MSchange_snapshot_agent_properties_TSQL"
  - "sp_MSchange_snapshot_agent_properties"
helpviewer_keywords: 
  - "sp_MSchange_snapshot_agent_properties"
ms.assetid: 7947a788-3fd7-469f-84db-b03ba89a153c
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_MSchange_snapshot_agent_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the properties of a Snapshot Agent job that runs at a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later version Distributor. This stored procedure is used to change properties when the Publisher runs on an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]. This stored procedure is executed at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_MSchange_snapshot_agent_properties [ @publisher = ] 'publisher'  
        , [ @publisher_db = ] 'publisher_db'  
        , [ @publication = ] 'publication'   
        , [ @frequency_type= ] frequency_type  
        , [ @frequency_interval= ] frequency_interval  
        , [ @frequency_subday= ] frequency_subday  
        , [ @frequency_subday_interval= ] frequency_subday_interval  
        , [ @frequency_relative_interval= ] frequency_relative_interval  
        , [ @frequency_recurrence_factor= ] frequency_recurrence_factor  
        , [ @active_start_date= ] active_start_date  
        , [ @active_end_date= ] active_end_date  
        , [ @active_start_time_of_day= ] active_start_time_of_day  
        , [ @active_end_time_of_day= ] active_end_time_of_day  
        , [ @snapshot_job_name = ] 'snapshot_agent_name'  
        , [ @publisher_security_mode = ] publisher_security_mode  
        , [ @publisher_login = ] 'publisher_login'  
        , [ @publisher_password = ] 'publisher_password'   
        , [ @job_login = ] 'job_login'  
        , [ @job_password = ] 'job_password'  
        , [ @publisher_type = ] 'publisher_type'  
```  
  
## Arguments  
 [ **@publisher** = ] **'**_publisher_**'**  
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
 [ **@publisher_db=** ] **'**_publisher_db_**'**  
 Is the name of the publication database. *publisher_db* is **sysname**, with no default.  
  
 [ **@publication =** ] **'**_publication_**'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@frequency_type =** ] *frequency_type*  
 Is the frequency with which the Snapshot Agent is executed. *frequency_type* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|On demand|  
|**4**|Daily|  
|**8**|Weekly|  
|**10**|Monthly|  
|**20**|Monthly, relative to the frequency interval|  
|**40**|When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts|  
  
 [ **@frequency_interval =** ] *frequency_interval*  
 Is the value to apply to the frequency set by *frequency_type*. *frequency_interval* is **int**, with no default.  
  
 [ **@frequency_subday =** ] *frequency_subday*  
 Is the units for *freq_subday_interval*. *frequency_subday* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4**|Minute|  
|**8**|Hour|  
  
 [ **@frequency_subday_interval=**] *frequency_subday_interval*  
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with no default.  
  
 [ **@frequency_relative_interval =** ] *frequency_relative_interval*  
 Is the date the Snapshot Agent runs. *frequency_relative_interval* is **int**, with no default.  
  
 [ **@frequency_recurrence_factor =** ] *frequency_recurrence_factor*  
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with no default.  
  
 [ **@active_start_date =** ] *active_start_date*  
 Is the date when the Snapshot Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with no default.  
  
 [ **@active_end_date =** ] *active_end_date*  
 Is the date when the Snapshot Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with no default.  
  
 [ **@active_start_time_of_day=**] *active_start_time_of_day*  
 Is the time of day when the Snapshot Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with no default.  
  
 [ **@active_end_time_of_day=**] *active_end_time_of_day*  
 Is the time of day when the Snapshot Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with no default.  
  
 [ **@snapshot_job_name =** ] **'**_snapshot_agent_name_**'**  
 Is the name of an existing Snapshot Agent job name if an existing job is being used. *snapshot_agent_name* is **nvarchar(100)**, with no default.  
  
 [ **@publisher_security_mode**= ] *publisher_security_mode*  
 Is the security mode used by the agent when connecting to the Publisher. *publisher_security_mode* is **int**, with no default. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, and **1** specifies Windows Authentication. A value of **0** must be specified for non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
 [ **@publisher_login**= ] **'**_publisher_login_**'**  
 Is the login used when connecting to the Publisher. *publisher_login* is **sysname**, with no default. *publisher_login* must be specified when *publisher_security_mode* is **0**. If *publisher_login* is NULL and publisher*_**security_mode* is **1**, then the Windows account specified in *job_login* will be used when connecting to the Publisher.  
  
 [ **@publisher_password**= ] **'**_publisher_password_**'**  
 Is the password used when connecting to the Publisher. *publisher_password* is **nvarchar(524)**, with no default.  
  
> [!IMPORTANT]  
>  Do not store authentication information in script files. To help improve security, we recommend that you provide login names and passwords at run time.  
  
 [ **@job_login**= ] **'**_job_login_**'**  
 Is the login for the Windows account under which the agent runs. *job_login* is **nvarchar(257)**, with no default. This Windows account is always used for agent connections to the Distributor. You must supply this parameter when creating a new Snapshot Agent job. *This cannot be changed for a non-*[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *Publisher.*  
  
 [ **@job_password**= ] **'**_job_password_**'**  
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with no default. You must supply this parameter when creating a new Snapshot Agent job.  
  
> [!IMPORTANT]  
>  Do not store authentication information in script files. To help improve security, we recommend that you provide login names and passwords at run time.  
  
 [ **@publisher_type**= ] **'**_publisher_type_**'**  
 Specifies the Publisher type when the Publisher is not running in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. *publisher_type* is **sysname**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**MSSQLSERVER**|Specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.|  
|**ORACLE**|Specifies a standard Oracle Publisher.|  
|**ORACLE GATEWAY**|Specifies an Oracle Gateway Publisher.|  
  
 For more information about the differences between an Oracle Publisher and an Oracle Gateway Publisher, see [Oracle Publishing Overview](../../relational-databases/replication/non-sql/oracle-publishing-overview.md).  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_MSchange_snapshot_agent_properties** is used in snapshot replication, transactional replication, and merge replication.  
  
 You must specify all parameters when executing **sp_MSchange_snapshot_agent_properties**. Execute [sp_helppublication_snapshot](../../relational-databases/system-stored-procedures/sp-helppublication-snapshot-transact-sql.md) to return the current properties of the Snapshot Agent job.  
  
 When the Publisher runs on an instance of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later version, you should use [sp_changepublication_snapshot](../../relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql.md) to change properties of a Snapshot Agent job.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role at the Distributor can execute **sp_MSchange_snapshot_agent_properties**.  
  
## See Also  
 [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md)  
  
  
