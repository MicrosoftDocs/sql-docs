---
title: "sp_addmergepushsubscription_agent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addmergepushsubscription_agent_TSQL"
  - "sp_addmergepushsubscription_agent"
helpviewer_keywords: 
  - "sp_addmergepushsubscription_agent"
ms.assetid: 808a1925-be46-4999-8d69-b3a83010ec81
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addmergepushsubscription_agent (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a new agent job used to schedule synchronization of a push subscription to a merge publication. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addmergepushsubscription_agent [ @publication =] 'publication'   
    [ , [ @subscriber = ] 'subscriber' ]   
    [ , [ @subscriber_db = ] 'subscriber_db' ]   
    [ , [ @subscriber_security_mode = ] subscriber_security_mode ]   
    [ , [ @subscriber_login = ] 'subscriber_login' ]   
    [ , [ @subscriber_password = ] 'subscriber_password' ]   
    [ , [ @publisher_security_mode = ] publisher_security_mode ]   
    [ , [ @publisher_login = ] 'publisher_login' ]   
    [ , [ @publisher_password = ] 'publisher_password' ]   
    [ , [ @job_login = ] 'job_login' ]   
    [ , [ @job_password = ] 'job_password' ]   
    [ , [ @job_name = ] 'job_name' ]   
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
    [ , [ @enabled_for_syncmgr = ] 'enabled_for_syncmgr' ]   
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of NULL.  
  
`[ @subscriber_db = ] 'subscriber_db'`
 Is the name of the subscription database. *subscriber_db* is **sysname**, with a default of NULL.  
  
`[ @subscriber_security_mode = ] subscriber_security_mode`
 Is the security mode to use when connecting to a Subscriber when synchronizing. *subscriber_security_mode* is **int**, with a default of 1. If **0**, specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. If **1**, specifies Windows Authentication.  
  
`[ @subscriber_login = ] 'subscriber_login'`
 Is the Subscriber login to use when connecting to a Subscriber when synchronizing. *subscriber_login* is required if *subscriber_security_mode* is set to **0**. *subscriber_login* is **sysname**, with a default of NULL.  
  
`[ @subscriber_password = ] 'subscriber_password'`
 Is the Subscriber password for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. *subscriber_password* is required if *subscriber_security_mode* is set to **0**. *subscriber_password* is **sysname**, with a default of NULL. If a subscriber password is used, it is automatically encrypted.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
`[ @publisher_security_mode = ] publisher_security_mode`
 Is the security mode to use when connecting to a Publisher when synchronizing. *publisher_security_mode* is **int**, with a default of 1. If **0**, specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. If **1**, specifies Windows Authentication.  
  
`[ @publisher_login = ] 'publisher_login'`
 Is the login to use when connecting to a Publisher when synchronizing. *publisher_login* is **sysname**, with a default of NULL.  
  
`[ @publisher_password = ] 'publisher_password'`
 Is the password used when connecting to the Publisher. *publisher_password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
`[ @job_login = ] 'job_login'`
 Is the login for the Windows account under which the agent runs. *job_login* is **nvarchar(257)**, with a default value of NULL. This Windows account is always used for agent connections to the Distributor and for connections to the Subscriber and Publisher when using Windows Integrated authentication.  
  
`[ @job_password = ] 'job_password'`
 Is the password for the Windows account under which the agent runs. *job_password* is **sysname**, with no default.  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
`[ @job_name = ] 'job_name'`
 Is the name of an existing agent job. *job_name* is **sysname**, with a default value of NULL. This parameter is only specified when the subscription is synchronized using an existing job instead of a newly created job (the default). If you are not a member of the **sysadmin** fixed server role, you must specify *job_login* and *job_password* when you specify *job_name*.  
  
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
 The days that the Merge Agent runs. *frequency_interval* is **int**, and can be one of the following values.  
  
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
 Is the date of the Merge Agent. This parameter is used when *frequency_type* is set to **32** (monthly relative). *frequency_relative_interval* is **int**, and can be one of the following values.  
  
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
  
`[ @active_start_time_of_day = ] active_start_time_of_day`
 Is the time of day when the Merge Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_end_time_of_day = ] active_end_time_of_day`
 Is the time of day when the Merge Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_start_date = ] active_start_date`
 Is the date when the Merge Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of NULL.  
  
`[ @active_end_date = ] active_end_date`
 Is the date when the Merge Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of NULL.  
  
`[ @enabled_for_syncmgr = ] 'enabled_for_syncmgr'`
 Specifies if the subscription can be synchronized through Windows Synchronization Manager. *enabled_for_syncmgr* is **nvarchar(5)**, with a default of FALSE. If **false**, the subscription is not registered with Synchronization Manager. If **true**, the subscription is registered with Synchronization Manager and can be synchronized without starting [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_addmergepushsubscription_agent** is used in merge replication and uses functionality similar to [sp_addpushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md).  
  
## Example  
 [!code-sql[HowTo#sp_addmergepushsubscriptionagent](../../relational-databases/replication/codesnippet/tsql/sp-addmergepushsubscript_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addmergepushsubscription_agent**.  
  
## See Also  
 [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)   
 [sp_addmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md)   
 [sp_changemergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergesubscription-transact-sql.md)   
 [sp_dropmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergesubscription-transact-sql.md)   
 [sp_helpmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergesubscription-transact-sql.md)  
  
  
