---
description: "sp_changesubstatus (Transact-SQL)"
title: "sp_changesubstatus (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_changesubstatus"
  - "sp_changesubstatus_TSQL"
helpviewer_keywords: 
  - "sp_changesubstatus"
ms.assetid: 9370e47a-d128-4f15-9224-1c3642770c39
author: markingmyname
ms.author: maghan
---
# sp_changesubstatus (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Changes the status of an existing Subscriber. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changesubstatus [ [ @publication = ] 'publication' ]  
    [ , [ @article = ] 'article' ]  
    [ , [ @subscriber = ] 'subscriber' ]  
        , [ @status = ] 'status'  
    [ , [ @previous_status = ] 'previous_status' ]  
    [ , [ @destination_db = ] 'destination_db' ]  
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
    [ , [ @distribution_jobid = ] distribution_jobid ]  
    [ , [ @from_auto_sync = ] from_auto_sync ]  
    [ , [ @ignore_distributor = ] ignore_distributor ]  
    [ , [ @offloadagent= ] remote_agent_activation ]  
    [ , [ @offloadserver= ] 'remote_agent_server_name' ]  
    [ , [ @dts_package_name= ] 'dts_package_name' ]  
    [ , [ @dts_package_password= ] 'dts_package_password' ]  
    [ , [ @dts_package_location= ] dts_package_location ]  
    [ , [ @skipobjectactivation = ] skipobjectactivation  
  [ , [ @distribution_job_name= ] 'distribution_job_name' ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of **%**. If *publication* is not specified, all publications are affected.  
  
`[ @article = ] 'article'`
 Is the name of the article. It must be unique to the publication. *article* is **sysname**, with a default of **%**. If *article* is not specified, all articles are affected.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber to change the status of. *subscriber* is **sysname**, with a default of **%**. If *subscriber* is not specified, status is changed for all Subscribers to the specified article.  
  
`[ @status = ] 'status'`
 Is the subscription status in the **syssubscriptions** table. *status* is **sysname**, with no default, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**active**|Subscriber is synchronized and is receiving data.|  
|**inactive**|Subscriber entry exists without a subscription.|  
|**subscribed**|Subscriber is requesting data, but is not yet synchronized.|  
  
`[ @previous_status = ] 'previous_status'`
 Is the previous status for the subscription. *previous_status* is **sysname**, with a default of NULL. This parameter allows you to change any subscriptions that currently have that status, thus allowing group functions on a specific set of subscriptions (for example, setting all active subscriptions back to **subscribed**).  
  
`[ @destination_db = ] 'destination_db'`
 Is the name of the destination database. *destination_db* is **sysname**, with a default of **%**.  
  
`[ @frequency_type = ] frequency_type`
 Is the frequency with which to schedule the distribution task. *frequency_type* is **int**, with a default of NULL.  
  
`[ @frequency_interval = ] frequency_interval`
 Is the value to apply to the frequency set by *frequency_type*. *frequency_interval* is **int**, with a default of NULL.  
  
`[ @frequency_relative_interval = ] frequency_relative_interval`
 Is the date of the distribution task. This parameter is used when *frequency_type* is set to 32 (monthly relative). *frequency_relative_interval* is **int**, and can be one of these values.  
  
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
 Is how often, in minutes, to reschedule during the defined period. *frequency_subday* is **int**, and can be one of these values.  
  
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
 Is the time of day when the distribution task is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_end_time_of_day = ] active_end_time_of_day`
 Is the time of day when the distribution task stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_start_date = ] active_start_date`
 Is the date when the distribution task is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of NULL.  
  
`[ @active_end_date = ] active_end_date`
 Is the date when the distribution task stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of NULL.  
  
`[ @optional_command_line = ] 'optional_command_line'`
 Is an optional command prompt. *optional_command_line* is **nvarchar(4000)**, with a default of NULL.  
  
`[ @distribution_jobid = ] distribution_jobid`
 Is the job ID of the Distribution Agent at the Distributor for the subscription when changing the subscription status from inactive to active. In other cases, it is not defined. If more than one Distribution Agent is involved in a single call to this stored procedure, the result is not defined. *distribution_jobid* is **binary(16)**, with a default of NULL.  
  
`[ @from_auto_sync = ] from_auto_sync`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
`[ @ignore_distributor = ] ignore_distributor`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
`[ @offloadagent = ] remote_agent_activation`
 > [!NOTE]  
>  Remote agent activation has been deprecated and is no longer supported. This parameter is supported only to maintain backward compatibility of scripts. Setting *remote_agent_activation* to a value other than **0** generates an error.  
  
`[ @offloadserver = ] 'remote_agent_server_name'`
 > [!NOTE]  
>  Remote agent activation has been deprecated and is no longer supported. This parameter is supported only to maintain backward compatibility of scripts. Setting *remote_agent_server_name* to any non-NULL value generates an error.  
  
`[ @dts_package_name = ] 'dts_package_name'`
 Specifies the name of the Data Transformation Services (DTS) package. *dts_package_name* is a **sysname**, with a default of NULL. For example, for a package named **DTSPub_Package** you would specify `@dts_package_name = N'DTSPub_Package'`.  
  
`[ @dts_package_password = ] 'dts_package_password'`
 Specifies the password on the package. *dts_package_password* is **sysname** with a default of NULL, which specifies that the password property is to be left unchanged.  
  
> [!NOTE]  
>  A DTS package must have a password.  
  
`[ @dts_package_location = ] dts_package_location`
 Specifies the package location. *dts_package_location* is an **int**, with a default of **0**. If **0**, the package location is at the Distributor. If **1**, the package location is at the Subscriber. The location of the package can be **distributor** or **subscriber**.  
  
`[ @skipobjectactivation = ] skipobjectactivation`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
`[ @distribution_job_name = ] 'distribution_job_name'`
 Is the name of the distribution job. *distribution_job_name* is **sysname**, with a default of NULL.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when changing article properties on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changesubstatus** is used in snapshot replication and transactional replication.  
  
 **sp_changesubstatus** changes the status of the Subscriber in the **syssubscriptions** table with the changed status. If required, it updates the article status in the **sysarticles** table to indicate active or inactive. If required, it sets the replication flag on or off in the **sysobjects** table for the replicated table.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, **db_owner** fixed database role, or the creator of the subscription can execute **sp_changesubstatus**.  
  
## See Also  
 [sp_addsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md)   
 [sp_dropsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscription-transact-sql.md)   
 [sp_helpdistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributor-transact-sql.md)   
 [sp_helpsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
