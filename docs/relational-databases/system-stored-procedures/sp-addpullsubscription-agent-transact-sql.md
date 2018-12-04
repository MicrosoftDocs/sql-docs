---
title: "sp_addpullsubscription_agent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addpullsubscription_agent"
  - "sp_addpullsubscription_agent_TSQL"
helpviewer_keywords:
  - "sp_addpullsubscription_agent"
ms.assetid: b9c2eaed-6d2d-4b78-ae9b-73633133180b
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addpullsubscription_agent (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
 
  Adds a new scheduled agent job used to synchronize a pull subscription to a transactional publication. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addpullsubscription_agent [ @publisher = ] 'publisher'  
    [ , [ @publisher_db = ] 'publisher_db' ]          , [ @publication = ] 'publication'  
    [ , [ @subscriber = ] 'subscriber' ]  
    [ , [ @subscriber_db = ] 'subscriber_db' ]  
    [ , [ @subscriber_security_mode = ] subscriber_security_mode ]  
    [ , [ @subscriber_login = ] 'subscriber_login' ]  
    [ , [ @subscriber_password = ] 'subscriber_password' ]  
    [ , [ @distributor = ] 'distributor' ]  
    [ , [ @distribution_db = ] 'distribution_db' ]  
    [ , [ @distributor_security_mode = ] distributor_security_mode ]  
    [ , [ @distributor_login = ] 'distributor_login' ]  
    [ , [ @distributor_password = ] 'distributor_password' ]  
    [ , [ @optional_command_line = ] 'optional_command_line' ]  
    [ , [ @frequency_type = ] frequency_type ]  
    [ , [ @frequency_interval = ] frequency_interval ]  
    [ , [ @frequency_relative_interval = ] frequency_relative_interval ]  
    [ , [ @frequency_recurrence_factor = ] frequency_recurrence_factor ]  
    [ , [ @frequency_subday = ] frequency_subday ]  
    [ , [ @frequency_subday_interval = ] frequency_subday_interval ]  
    [ , [ @active_start_time_of_day = ] active_start_time_of_day ]  
    [ , [ @active_end_time_of_day = ] active_end_time_of_day ]  
    [ , [ @active_start_date = ] active_start_date ]  
    [ , [ @active_end_date = ] active_end_date ]  
    [ , [ @distribution_jobid = ] distribution_jobid OUTPUT ]  
    [ , [ @encrypted_distributor_password = ] encrypted_distributor_password ]  
    [ , [ @enabled_for_syncmgr = ] 'enabled_for_syncmgr' ]  
    [ , [ @ftp_address = ] 'ftp_address' ]  
    [ , [ @ftp_port = ] ftp_port ]  
    [ , [ @ftp_login = ] 'ftp_login' ]  
    [ , [ @ftp_password = ] 'ftp_password' ]  
    [ , [ @alt_snapshot_folder = ] 'alternate_snapshot_folder' ]  
    [ , [ @working_directory = ] 'working_directory' ]  
    [ , [ @use_ftp = ] 'use_ftp' ]  
    [ , [ @publication_type = ] publication_type ]  
    [ , [ @dts_package_name = ] 'dts_package_name' ]  
    [ , [ @dts_package_password = ] 'dts_package_password' ]  
    [ , [ @dts_package_location = ] 'dts_package_location' ]  
    [ , [ @reserved = ] 'reserved' ]  
    [ , [ @offloadagent = ] 'remote_agent_activation' ]  
    [ , [ @offloadserver = ] 'remote_agent_server_name']  
    [ , [ @job_name = ] 'job_name' ]  
    [ , [ @job_login = ] 'job_login' ]   
    [ , [ @job_password = ] 'job_password' ]   
```  
  
## Arguments  
 [ **@publisher=**] **'**_publisher_**'**  
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
 [ **@publisher_db=**] **'**_publisher_db'_  
 Is the name of the Publisher database. *publisher_db* is **sysname**, with a default value of NULL. *publisher_db* is ignored by Oracle Publishers.  
  
 [ **@publication=**] **'**_publication_**'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@subscriber=**] **'**_subscriber_**'**  
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts.  
  
 [ **@subscriber_db=**] **'**_subscriber_db_**'**  
 Is the name of the subscription database. *subscriber_db* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts.  
  
 [ **@subscriber_security_mode=**] *subscriber_security_mode*  
 Is the security mode to use when connecting to a Subscriber when synchronizing. *subscriber_security_mode* is **int,** with a default of NULL. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. **1** specifies Windows Authentication.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The Distribution Agent always connects to the local Subscriber using Windows Authentication. If a value other than NULL or **1** is specified for this parameter, a warning message is returned.  
  
 [ **@subscriber_login =**] **'**_subscriber_login_**'**  
 Is the Subscriber login to use when connecting to a Subscriber when synchronizing.*subscriber_login* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. If a value is specified for this parameter, a warning message is returned, but the value is ignored.  
  
 [ **@subscriber_password=**] **'**_subscriber_password_**'**  
 Is the Subscriber password. *subscriber_password* is required if *subscriber_security_mode* is set to **0**. *subscriber_password* is **sysname**, with a default of NULL. If a subscriber password is used, it is automatically encrypted.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. If a value is specified for this parameter, a warning message is returned, but the value is ignored.  
  
 [ **@distributor=**] **'**_distributor_**'**  
 Is the name of the Distributor. *distributor* is **sysname**, with a default of the value specified by *publisher*.  
  
 [ **@distribution_db=**] **'**_distribution_db_**'**  
 Is the name of the distribution database. *distribution_db* is **sysname**, with a default value of NULL.  
  
 [ **@distributor_security_mode=**] *distributor_security_mode*  
 Is the security mode to use when connecting to a Distributor when synchronizing. *distributor_security_mode* is **int**, with a default of **1**. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. **1** specifies Windows Authentication.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
 [ **@distributor_login=**] **'**_distributor_login_**'**  
 Is the Distributor login to use when connecting to a Distributor when synchronizing. *distributor_login* is required if *distributor_security_mode* is set to **0**. *distributor_login* is **sysname**, with a default of NULL.  
  
 [ **@distributor_password =**] **'**_distributor_password_**'**  
 Is the Distributor password. *distributor_password* is required if *distributor_security_mode* is set to **0**. *distributor_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  Do not use a blank password. Use a strong password. When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
 [ **@optional_command_line=**] **'**_optional_command_line_**'**  
 Is an optional command prompt supplied to the Distribution Agent. For example, **-DefinitionFile** C:\Distdef.txt or **-CommitBatchSize** 10. *optional_command_line* is **nvarchar(4000)**, with a default of empty string.  
  
 [ **@frequency_type=**] *frequency_type*  
 Is the frequency with which to schedule the Distribution Agent. *frequency_type* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|One time|  
|**2** (default)|On demand|  
|**4**|Daily|  
|**8**|Weekly|  
|**16**|Monthly|  
|**32**|Monthly relative|  
|**64**|Autostart|  
|**128**|Recurring|  
  
> [!NOTE]  
>  Specifying a value of **64** causes the Distribution Agent to run in continuous mode. This corresponds to setting the **-Continuous** parameter for the agent. For more information, see [Replication Distribution Agent](../../relational-databases/replication/agents/replication-distribution-agent.md).  
  
 [ **@frequency_interval=**] *frequency_interval*  
 Is the value to apply to the frequency set by *frequency_type*. *frequency_interval* is **int**, with a default of 1.  
  
 [ **@frequency_relative_interval=**] *frequency_relative_interval*  
 Is the date of the Distribution Agent. This parameter is used when *frequency_type* is set to **32** (monthly relative). *frequency_relative_interval* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1** (default)|First|  
|**2**|Second|  
|**4**|Third|  
|**8**|Fourth|  
|**16**|Last|  
  
 [ **@frequency_recurrence_factor=**] *frequency_recurrence_factor*  
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of **1**.  
  
 [ **@frequency_subday=**] *frequency_subday*  
 Is how often to reschedule during the defined period. *frequency_subday* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1** (default)|Once|  
|**2**|Second|  
|**4**|Minute|  
|**8**|Hour|  
  
 [ **@frequency_subday_interval=**] *frequency_subday_interval*  
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with a default of **1**.  
  
 [ **@active_start_time_of_day=**] *active_start_time_of_day*  
 Is the time of day when the Distribution Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of **0**.  
  
 [ **@active_end_time_of_day=**] *active_end_time_of_day*  
 Is the time of day when the Distribution Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of **0**.  
  
 [ **@active_start_date=**] *active_start_date*  
 Is the date when the Distribution Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of **0**.  
  
 [ **@active_end_date=**] *active_end_date*  
 Is the date when the Distribution Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of **0**.  
  
 [ **@distribution_jobid =**] _distribution_jobid_**OUTPUT**  
 Is the ID of the Distribution Agent for this job. *distribution_jobid* is **binary(16)**, with a default of NULL, and it is an OUTPUT parameter.  
  
 [ **@encrypted_distributor_password=**] *encrypted_distributor_password*  
 Setting *encrypted_distributor_password* is no longer supported. Attempting to set this **bit** parameter to **1** will result in an error.  
  
 [ **@enabled_for_syncmgr=**] **'**_enabled_for_syncmgr_**'**  
 Is whether the subscription can be synchronized through [!INCLUDE[msCoName](../../includes/msconame-md.md)] Synchronization Manager. *enabled_for_syncmgr* is **nvarchar(5)**, with a default of FALSE. If **false**, the subscription is not registered with Synchronization Manager. If **true**, the subscription is registered with Synchronization Manager and can be synchronized without starting [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 [ **@ftp_address=**] **'**_ftp_address_**'**  
 For backward compatibility only.  
  
 [ **@ftp_port=**] *ftp_port*  
 For backward compatibility only.  
  
 [ **@ftp_login=**] **'**_ftp_login_**'**  
 For backward compatibility only.  
  
 [ **@ftp_password=**] **'**_ftp_password_**'**  
 For backward compatibility only.  
  
 [ **@alt_snapshot_folder=** ] **'**_alternate_snapshot_folder'_  
 Specifies the location of the alternate folder for the snapshot. *alternate_snapshot_folder* is **nvarchar(255)**, with a default of NULL.  
  
 [ **@working_directory**= ] **'**_working_director_**'**  
 Is the name of the working directory used to store data and schema files for the publication. *working_directory* is **nvarchar(255)**, with a default of NULL. The name should be specified in UNC format.  
  
 [ **@use_ftp**= ] **'**_use_ftp_**'**  
 Specifies the use of FTP instead of the regular protocol to retrieve snapshots. *use_ftp* is **nvarchar(5)**, with a default of FALSE.  
  
 [ **@publication_type**= ] *publication_type*  
 Specifies the replication type of the publication. *publication_type* is a **tinyint** with a default of **0**. If **0**, publication is a transaction type. If **1**, publication is a snapshot type. If **2**, publication is a merge type.  
  
 [ **@dts_package_name**= ] **'**_dts_package_name_**'**  
 Specifies the name of the DTS package. *dts_package_name* is a **sysname** with a default of NULL. For example, to specify a package of `DTSPub_Package`, the parameter would be `@dts_package_name = N'DTSPub_Package'`.  
  
 [ **@dts_package_password**= ] **'**_dts_package_password_**'**  
 Specifies the password on the package, if there is one. *dts_package_password* is **sysname** with a default of NULL, which means a password is not on the package.  
  
> [!NOTE]  
>  You must specify a password if *dts_package_name* is specified.  
  
 [ **@dts_package_location**= ] **'**_dts_package_location_**'**  
 Specifies the package location. *dts_package_location* is a **nvarchar(12)**, with a default of **subscriber**. The location of the package can be **distributor** or **subscriber**.  
  
 [ **@reserved**= ] **'**_reserved_**'**  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ **@offloadagent**= ] '*remote_agent_activation*'  
 > [!NOTE]  
>  Remote agent activation has been deprecated and is no longer supported. This parameter is supported only to maintain backward compatibility of scripts. Setting *remote_agent_activation* to a value other than **false** will generate an error.  
  
 [ **@offloadserver**= ] '*remote_agent_server_name*'  
 > [!NOTE]  
>  Remote agent activation has been deprecated and is no longer supported. This parameter is supported only to maintain backward compatibility of scripts. Setting *remote_agent_server_name* to any non-NULL value will generate an error.  
  
 [ **@job_name**= ] '*job_name*'  
 Is the name of an existing agent job. *job_name* is **sysname**, with a default value of NULL. This parameter is only specified when the subscription will be synchronized using an existing job instead of a newly created job (the default). If you are not a member of the **sysadmin** fixed server role, you must specify *job_login* and *job_password* when you specify *job_name*.  
  
 [ **@job_login**= ] **'**_job_login_**'**  
 Is the login for the Windows account under which the agent runs. *job_login* is **nvarchar(257)**, with no default. This Windows account is always used for agent connections to the Subscriber.  
  
 [ **@job_password**= ] **'**_job_password_**'**  
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with no default.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addpullsubscription_agent** is used in snapshot replication and transactional replication.  
  
## Example  
 [!code-sql[HowTo#sp_addtranpullsubscriptionagent](../../relational-databases/replication/codesnippet/tsql/sp-addpullsubscription-a_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addpullsubscription_agent**.  
  
## See Also  
 [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)   
 [sp_addpullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md)   
 [sp_change_subscription_properties &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-subscription-properties-transact-sql.md)   
 [sp_droppullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droppullsubscription-transact-sql.md)   
 [sp_helppullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppullsubscription-transact-sql.md)   
 [sp_helpsubscription_properties &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-properties-transact-sql.md)  
  
  
