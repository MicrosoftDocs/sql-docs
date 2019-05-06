---
title: "sp_addmergesubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addmergesubscription_TSQL"
  - "sp_addmergesubscription"
helpviewer_keywords: 
  - "sp_addmergesubscription"
ms.assetid: a191d817-0132-49ff-93ca-76f13e609b38
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addmergesubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a push or pull merge subscription. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addmergesubscription [ @publication= ] 'publication'  
    [ , [ @subscriber = ] 'subscriber' ]  
    [ , [ @subscriber_db= ] 'subscriber_db' ]  
    [ , [ @subscription_type= ] 'subscription_type' ]  
    [ , [ @subscriber_type= ] 'subscriber_type' ]  
    [ , [ @subscription_priority= ] subscription_priority ]  
    [ , [ @sync_type= ] 'sync_type' ]  
    [ , [ @frequency_type= ] frequency_type ]  
    [ , [ @frequency_interval= ] frequency_interval ]  
    [ , [ @frequency_relative_interval= ] frequency_relative_interval ]  
    [ , [ @frequency_recurrence_factor= ] frequency_recurrence_factor ]  
    [ , [ @frequency_subday= ] frequency_subday ]  
    [ , [ @frequency_subday_interval= ] frequency_subday_interval ]  
    [ , [ @active_start_time_of_day= ] active_start_time_of_day ]  
    [ , [ @active_end_time_of_day= ] active_end_time_of_day ]  
    [ , [ @active_start_date= ] active_start_date ]  
    [ , [ @active_end_date= ] active_end_date ]  
    [ , [ @optional_command_line= ] 'optional_command_line' ]  
    [ , [ @description= ] 'description' ]  
    [ , [ @enabled_for_syncmgr= ] 'enabled_for_syncmgr' ]  
    [ , [ @offloadagent= ] remote_agent_activation]  
    [ , [ @offloadserver= ] 'remote_agent_server_name' ]  
    [ , [ @use_interactive_resolver= ] 'use_interactive_resolver' ]  
    [ , [ @merge_job_name= ] 'merge_job_name' ]  
    [ , [ @hostname = ] 'hostname'  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default. The publication must already exist.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of NULL.  
  
`[ @subscriber_db = ] 'subscriber_db'`
 Is the name of the subscription database. *subscriber_db*is **sysname**, with a default of NULL.  
  
`[ @subscription_type = ] 'subscription_type'`
 Is the type of subscription. *subscription_type*is **nvarchar(15)**, with a default of PUSH. If **push**, a push subscription is added and the Merge Agent is added at the Distributor. If **pull**, a pull subscription is added without adding a Merge Agent at the Distributor.  
  
> [!NOTE]  
>  Anonymous subscriptions do not need to use this stored procedure.  
  
`[ @subscriber_type = ] 'subscriber_type'`
 Is the type of Subscriber. *subscriber_type*is **nvarchar(15)**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**local** (default)|Subscriber known only to the Publisher.|  
|**global**|Subscriber known to all servers.|  
  
 In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions, local subscriptions are referred to as client subscriptions, and global subscriptions are referred to as server subscriptions  
  
`[ @subscription_priority = ] subscription_priority`
 Is a number indicating the priority for the subscription. *subscription_priority*is **real**, with a default of NULL. For local and anonymous subscriptions, the priority is 0.0. For global subscriptions, the priority must be less than 100.0.  
  
`[ @sync_type = ] 'sync_type'`
 Is the subscription synchronization type. *sync_type*is **nvarchar(15)**, with a default of **automatic**. Can be **automatic** or **none**. If **automatic**, the schema and initial data for published tables are transferred to the Subscriber first. If **none**, it is assumed the Subscriber already has the schema and initial data for published tables. System tables and data are always transferred.  
  
> [!NOTE]  
>  We recommend to not specifying a value of **none**.  
  
`[ @frequency_type = ] frequency_type`
 Is a value indicating when the Merge Agent will run. *frequency_type* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**4**|Daily|  
|**8**|Weekly|  
|**10**|Monthly|  
|**20**|Monthly, relative to the frequency interval|  
|**40**|When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts|  
|NULL (default)||  
  
`[ @frequency_interval = ] frequency_interval`
 The day or days that the Merge Agent runs. *frequency_interval* is **int**, and can be one of the following values.  
  
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
 Is the scheduled merge occurrence of the frequency interval in each month. *frequency_relative_interval* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|First|  
|**2**|Second|  
|**4**|Third|  
|**8**|Fourth|  
|**16**|Last|  
|NULL (default)||  
  
`[ @frequency_recurrence_factor = ] frequency_recurrence_factor`
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor*is **int**, with a default of NULL.  
  
`[ @frequency_subday = ] frequency_subday`
 Is the unit for *frequency_subday_interval*. *frequency_subday* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4**|Minute|  
|**8**|Hour|  
|NULL (default)||  
  
`[ @frequency_subday_interval = ] frequency_subday_interval`
 Is the frequency for *frequency_subday* to occur between each merge. *frequency_subday_interval* is **int**, with a default of NULL.  
  
`[ @active_start_time_of_day = ] active_start_time_of_day`
 Is the time of day when the Merge Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_end_time_of_day = ] active_end_time_of_day`
 Is the time of day when the Merge Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_start_date = ] active_start_date`
 Is the date when the Merge Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of NULL.  
  
`[ @active_end_date = ] active_end_date`
 Is the date when the Merge Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of NULL.  
  
`[ @optional_command_line = ] 'optional_command_line'`
 Is the optional command prompt to execute. *optional_command_line*is **nvarchar(4000)**, with a default of NULL. This parameter is used to add a command that captures the output and saves it to a file or to specify a configuration file or attribute.  
  
`[ @description = ] 'description'`
 Is a brief description of this merge subscription. *description*is **nvarchar(255)**, with a default of NULL. This value is displayed by the Replication Monitor in the **Friendly Name** column, which can be used to sort the subscriptions for a monitored publication.  
  
`[ @enabled_for_syncmgr = ] 'enabled_for_syncmgr'`
 Specifies if the subscription can be synchronized through [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Synchronization Manager. *enabled_for_syncmgr* is **nvarchar(5)**, with a default of FALSE. If **false**, the subscription is not registered with Synchronization Manager. If **true**, the subscription is registered with Synchronization Manager and can be synchronized without starting [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
`[ @offloadagent = ] remote_agent_activation`
 Specifies that the agent can be activated remotely. *remote_agent_activation* is **bit** with a default of **0**.  
  
> [!NOTE]  
>  This parameter has been deprecated and is only maintained for backward compatibility of scripts.  
  
`[ @offloadserver = ] 'remote_agent_server_name'`
 Specifies the network name of server to be used for remote agent activation. *remote_agent_server_name*is **sysname**, with a default of NULL.  
  
`[ @use_interactive_resolver = ] 'use_interactive_resolver'`
 Allows conflicts to be resolved interactively for all articles that allow interactive resolution. *use_interactive_resolver* is **nvarchar(5)**, with a default of FALSE.  
  
`[ @merge_job_name = ] 'merge_job_name'`
 The *@merge_job_name* parameter is deprecated and cannot be set. *merge_job_name* is **sysname**, with a default of NULL.  
  
`[ @hostname = ] 'hostname'`
 Overrides the value returned by [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) when this function is used in the WHERE clause of a parameterized filter. *Hostname* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  For performance reasons, we recommend that you not apply functions to column names in parameterized row filter clauses, such as `LEFT([MyColumn]) = SUSER_SNAME()`. If you use [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) in a filter clause and override the HOST_NAME value, it might be necessary to convert data types using [CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md). For more information about best practices for this case, see the section "Overriding the HOST_NAME() Value" in the topic [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addmergesubscription** is used in merge replication.  
  
 When **sp_addmergesubscription** is executed by a member of the **sysadmin** fixed server role to create a push subscription, the Merge Agent job is implicitly created and runs under the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account. We recommend that you execute [sp_addmergepushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addmergepushsubscription-agent-transact-sql.md) and specify the credentials of a different, agent-specific Windows account for **@job_login** and **@job_password**. For more information, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
## Example  
 [!code-sql[HowTo#sp_addmergepushsubscriptionagent](../../relational-databases/replication/codesnippet/tsql/sp-addmergesubscription-_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addmergesubscription**.  
  
## See Also  
 [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md)   
 [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md)   
 [Interactive Conflict Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-interactive-resolution.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)   
 [sp_changemergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergesubscription-transact-sql.md)   
 [sp_dropmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergesubscription-transact-sql.md)   
 [sp_helpmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergesubscription-transact-sql.md)  
  
  
