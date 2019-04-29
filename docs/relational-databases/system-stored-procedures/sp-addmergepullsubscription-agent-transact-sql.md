---
title: "sp_addmergepullsubscription_agent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addmergepullsubscription_agent"
  - "sp_addmergepullsubscription_agent_TSQL"
helpviewer_keywords: 
  - "sp_addmergepullsubscription_agent"
ms.assetid: a2f4b086-078d-49b5-8971-8a1e3f6a6feb
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addmergepullsubscription_agent (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a new agent job used to schedule synchronization of a pull subscription to a merge publication. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addmergepullsubscription_agent [ [ @name = ] 'name' ]   
        , [ @publisher = ] 'publisher'   
        , [ @publisher_db = ] 'publisher_db'  
        , [ @publication =] 'publication'   
    [ , [ @publisher_security_mod e= ] publisher_security_mode ]   
    [ , [ @publisher_login = ] 'publisher_login' ]   
    [ , [ @publisher_password = ] 'publisher_password' ]   
    [ , [ @publisher_encrypted_password = ] publisher_encrypted_password ]   
    [ , [ @subscriber = ] 'subscriber' ]   
    [ , [ @subscriber_db = ] 'subscriber_db' ]   
    [ , [ @subscriber_security_mode = ] subscriber_security_mode ]   
    [ , [ @subscriber_login = ] 'subscriber_login' ]   
    [ , [ @subscriber_password= ] 'subscriber_password' ]   
    [ , [ @distributor = ] 'distributor' ]   
    [ , [ @distributor_security_mode = ] distributor_security_mode ]   
    [ , [ @distributor_login = ] 'distributor_login' ]   
    [ , [ @distributor_password = ] 'distributor_password' ]   
    [ , [ @encrypted_password = ] encrypted_password ]   
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
    [ , [ @optional_command_line = ] 'optional_command_line' ]   
    [ , [ @merge_jobid = ] merge_jobid ]   
    [ , [ @enabled_for_syncmgr = ] 'enabled_for_syncmgr' ]   
    [ , [ @ftp_address = ] 'ftp_address' ]   
    [ , [ @ftp_port = ] ftp_port ]   
    [ , [ @ftp_login = ] 'ftp_login' ]   
    [ , [ @ftp_password = ] 'ftp_password' ]    
    [ , [ @alt_snapshot_folder = ] 'alternate_snapshot_folder' ]   
    [ , [ @working_directory = ] 'working_directory' ]   
    [ , [ @use_ftp = ] 'use_ftp' ]   
    [ , [ @reserved = ] 'reserved' ]   
    [ , [ @use_interactive_resolver = ] 'use_interactive_resolver' ]   
    [ , [ @offloadagent = ] 'remote_agent_activation' ]   
    [ , [ @offloadserver = ] 'remote_agent_server_name']   
    [ , [ @job_name = ] 'job_name' ]   
    [ , [ @dynamic_snapshot_location = ] 'dynamic_snapshot_location' ]  
    [ , [ @use_web_sync = ] use_web_sync ]  
        [ , [ @internet_url = ] 'internet_url' ]  
    [ , [ @internet_login = ] 'internet_login' ]  
        [ , [ @internet_password = ] 'internet_password' ]  
    [ , [ @internet_security_mode = ] internet_security_mode ]  
        [ , [ @internet_timeout = ] internet_timeout ]  
    [ , [ @hostname = ] 'hostname' ]  
        [ , [ @job_login = ] 'job_login' ]   
    [ , [ @job_password = ] 'job_password' ]   
```  
  
## Arguments  
`[ @name = ] 'name'`
 Is the name of the agent. *name* is **sysname**, with a default of NULL.  
  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher server. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @publisher_security_mode = ] publisher_security_mode`
 Is the security mode to use when connecting to a Publisher when synchronizing. *publisher_security_mode* is **int**, with a default of 1. If **0**, specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. If **1**, specifies Windows Authentication.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
`[ @publisher_login = ] 'publisher_login'`
 Is the login to use when connecting to a Publisher when synchronizing. *publisher_login* is **sysname**, with a default of NULL.  
  
`[ @publisher_password = ] 'publisher_password'`
 Is the password used when connecting to the Publisher. *publisher_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
`[ @publisher_encrypted_password = ]publisher_encrypted_password`
 Setting *publisher_encrypted_password* is no longer supported. Attempting to set this **bit** parameter to **1** will result in an error.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of NULL.  
  
`[ @subscriber_db = ] 'subscriber_db'`
 Is the name of the subscription database. *subscriber_db* is **sysname**, with a default of NULL.  
  
`[ @subscriber_security_mode = ] subscriber_security_mode`
 Is the security mode to use when connecting to a Subscriber when synchronizing. *subscriber_security_mode* is **int**, with a default of 1. If **0**, specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. If **1**, specifies Windows Authentication.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The Merge Agent always connects to the local Subscriber using Windows Authentication. If a value is specified for this parameter, a warning message will be returned, but the value will be ignored.  
  
`[ @subscriber_login = ] 'subscriber_login'`
 Is the Subscriber login to use when connecting to a Subscriber when synchronizing. *subscriber_login* is required if *subscriber_security_mode* is set to **0**. *subscriber_login* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. If a value is specified for this parameter, a warning message will be returned, but the value will be ignored.  
  
`[ @subscriber_password = ] 'subscriber_password'`
 Is the Subscriber password for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. *subscriber_password* is required if *subscriber_security_mode* is set to **0**. *subscriber_password* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. If a value is specified for this parameter, a warning message will be returned, but the value will be ignored.  
  
`[ @distributor = ] 'distributor'`
 Is the name of the Distributor. *distributor* is **sysname**, with a default of *publisher*; that is, the Publisher is also the Distributor.  
  
`[ @distributor_security_mode = ] distributor_security_mode`
 Is the security mode to use when connecting to a Distributor when synchronizing. *distributor_security_mode* is **int**, with a default of 0. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. **1** specifies Windows Authentication.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
`[ @distributor_login = ] 'distributor_login'`
 Is the Distributor login to use when connecting to a Distributor when synchronizing. *distributor_login* is required if *distributor_security_mode* is set to **0**. *distributor_login* is **sysname**, with a default of NULL.  
  
`[ @distributor_password = ] 'distributor_password'`
 Is the Distributor password. *distributor_password* is required if *distributor_security_mode* is set to **0**. *distributor_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
`[ @encrypted_password = ] encrypted_password`
 Setting *encrypted_password* is no longer supported. Attempting to set this **bit** parameter to **1** will result in an error.  
  
`[ @frequency_type = ] frequency_type`
 Is the frequency with which to schedule the Merge Agent. *frequency_type* is **int**, and can be one of the following values.  
  
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
  
> [!NOTE]  
>  Specifying a value of **64** causes the Merge Agent to run in continuous mode. This corresponds to setting the **-Continuous** parameter for the agent. For more information, see [Replication Merge Agent](../../relational-databases/replication/agents/replication-merge-agent.md).  
  
`[ @frequency_interval = ] frequency_interval`
 The day or days that the Merge Agent runs. *frequency_interval* is **int**, and can be one of these values.  
  
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
  
`[ @frequency_relative_interval = ] frequency_relative_interval`
 Is the date of the Merge Agent. This parameter is used when *frequency_type* is set to **32** (monthly relative). *frequency_relative_interval* is **int**, and can be one of these values.  
  
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
  
`[ @frequency_subday = ] frequency_subday`
 Is how often to reschedule during the defined period. *frequency_subday* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4**|Minute|  
|**8**|Hour|  
|NULL (default)||  
  
`[ @frequency_subday_interval = ] frequency_subday_interval`
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with a default of NULL.  
  
`[ @active_start_time_of_day = ] active_start_time_of_day`
 Is the time of day when the Merge Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_end_time_of_day = ] active_end_time_of_day`
 Is the time of day when the Merge Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_start_date = ] active_start_date`
 Is the date when the Merge Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of NULL.  
  
`[ @active_end_date = ] active_end_date`
 Is the date when the Merge Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of NULL.  
  
`[ @optional_command_line = ] 'optional_command_line'`
 Is an optional command prompt that is supplied to the Merge Agent. *optional_command_line* is **nvarchar(255)**, with a default of ' '. Can be used to supply additional parameters to the Merge Agent, such as in the following example that increases the default query time-out to `600` seconds:  
  
```  
@optional_command_line = N'-QueryTimeOut 600'  
```  
  
`[ @merge_jobid = ] merge_jobid`
 Is the output parameter for the job ID. *merge_jobid* is **binary(16)**, with a default of NULL.  
  
`[ @enabled_for_syncmgr = ] 'enabled_for_syncmgr'`
 Specifies if the subscription can be synchronized through Windows Synchronization Manager. *enabled_for_syncmgr* is **nvarchar(5)**, with a default of FALSE. If **false**, the subscription is not registered with Synchronization Manager. If **true**, the subscription is registered with Synchronization Manager and can be synchronized without starting [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
`[ @ftp_address = ] 'ftp_address'`
 For backward compatibility only.  
  
`[ @ftp_port = ] ftp_port`
 For backward compatibility only.  
  
`[ @ftp_login = ] 'ftp_login'`
 For backward compatibility only.  
  
`[ @ftp_password = ] 'ftp_password'`
 For backward compatibility only.  
  
`[ @alt_snapshot_folder = ] 'alternate_snapshot_folder'`
 Specifies the location from which to pick up the snapshot files. *alternate_snapshot_folder* is **nvarchar(255)**, with a default of NULL. If NULL, the snapshot files will be picked up from the default location specified by the Publisher.  
  
`[ @working_directory = ] 'working_directory'`
 Is the name of the working directory used to temporarily store data and schema files for the publication when FTP is used to transfer snapshot files. *working_directory* is **nvarchar(255)**, with a default of NULL.  
  
`[ @use_ftp = ] 'use_ftp'`
 Specifies the use of FTP instead of the typical protocol to retrieve snapshots. *use_ftp* is **nvarchar(5)**, with a default of FALSE.  
  
`[ @reserved = ] 'reserved'`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
`[ @use_interactive_resolver = ] 'use_interactive_resolver' ]`
 Uses interactive resolver to resolve conflicts for all articles that allow interactive resolution. *use_interactive_resolver* is **nvarchar(5)**, with a default of FALSE.  
  
`[ @offloadagent = ] 'remote_agent_activation'`
 > [!NOTE]  
>  Remote agent activation has been deprecated and is no longer supported. This parameter is supported only to maintain backward compatibility of scripts. Setting *remote_agent_activation* to a value other than **false** will generate an error.  
  
`[ @offloadserver = ] 'remote_agent_server_name'`
 > [!NOTE]  
>  Remote agent activation has been deprecated and is no longer supported. This parameter is supported only to maintain backward compatibility of scripts. Setting *remote_agent_server_name* to any non-NULL value will generate an error.  
  
`[ @job_name = ] 'job_name' ]`
 Is the name of an existing agent job. *job_name* is **sysname**, with a default value of NULL. This parameter is only specified when the subscription will be synchronized using an existing job instead of a newly created job (the default). If you are not a member of the **sysadmin** fixed server role, you must specify *job_login* and *job_password* when you specify *job_name*.  
  
`[ @dynamic_snapshot_location = ] 'dynamic_snapshot_location' ]`
 The path to the folder where the snapshot files will be read from if a filtered data snapshot is to be used. *dynamic_snapshot_location* is **nvarchar(260)**, with a default of NULL. For more information, see [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
`[ @use_web_sync = ] use_web_sync`
 Indicates that Web synchronization is enabled. *use_web_sync* is **bit**, with a default of 0. **1** specifies that the pull subscription can be synchronized over the internet using HTTP.  
  
`[ @internet_url = ] 'internet_url'`
 Is the location of the replication listener (REPLISAPI.DLL) for Web synchronization. *internet_url* is **nvarchar(260)**, with a default of NULL. *internet_url* is a fully qualified URL, in the format `http://server.domain.com/directory/replisapi.dll`. If the server is configured to listen on a port other than port 80, the port number must also be supplied in the format `http://server.domain.com:portnumber/directory/replisapi.dll`, where `portnumber` represents the port.  
  
`[ @internet_login = ] 'internet_login'`
 Is the login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using HTTP Basic Authentication. *internet_login* is **sysname**, with a default of NULL.  
  
`[ @internet_password = ] 'internet_password'`
 Is the password that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using HTTP Basic Authentication. *internet_password* is **nvarchar(524)**, with a default value of NULL.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]  
  
`[ @internet_security_mode = ] internet_security_mode`
 Is the authentication method used by the Merge Agent when connecting to the Web server during Web synchronization using HTTPS. *internet_security_mode* is **int** and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Basic Authentication is used.|  
|**1** (default)|Windows Integrated Authentication is used.|  
  
> [!NOTE]  
>  We recommend using Basic Authentication with Web synchronization. To use Web synchronization, you must make an SSL connection to the Web server. For more information, see [Configure Web Synchronization](../../relational-databases/replication/configure-web-synchronization.md).  
  
`[ @internet_timeout = ] internet_timeout`
 Is the length of time, in seconds, before a Web synchronization request expires. *internet_timeout* is **int**, with a default of **300** seconds.  
  
`[ @hostname = ] 'hostname'`
 Overrides the value of HOST_NAME() when this function is used in the WHERE clause of a parameterized filter. *hostname* is **sysname**, with a default of NULL.  
  
`[ @job_login = ] 'job_login'`
 Is the login for the Windows account under which the agent runs. *job_login* is **nvarchar(257)**, with no default. This Windows account is always used for agent connections to the Subscriber and for connections to the Distributor and Publisher when using Windows Integrated authentication.  
  
`[ @job_password = ] 'job_password'`
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with no default.  
  
> [!IMPORTANT]  
>  Do not store authentication information in script files. For best security, login names and passwords should be supplied at runtime.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_addmergepullsubscription_agent** is used in merge replication and uses functionality similar to [sp_addpullsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md).  
  
 For an example of how to correctly specify security settings when executing **sp_addmergepullsubscription_agent**, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md).  
  
## Example  
 [!code-sql[HowTo#sp_addmergepullsubscriptionagent](../../relational-databases/replication/codesnippet/tsql/sp-addmergepullsubscript_1_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addmergepullsubscription_agent**.  
  
## See Also  
 [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)   
 [sp_addmergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql.md)   
 [sp_changemergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergepullsubscription-transact-sql.md)   
 [sp_dropmergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergepullsubscription-transact-sql.md)   
 [sp_helpmergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergepullsubscription-transact-sql.md)   
 [sp_helpsubscription_properties &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-properties-transact-sql.md)  
  
  
