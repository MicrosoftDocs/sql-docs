---
title: "sp_changesubscriber (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changesubscriber"
  - "sp_changesubscriber_TSQL"
helpviewer_keywords: 
  - "sp_changesubscriber"
ms.assetid: d453c451-e957-490f-b968-5e03aeddaf10
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changesubscriber (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the options for a Subscriber. Any distribution task for the Subscribers to this Publisher is updated. This stored procedure writes to the **MSsubscriber_info** table in the distribution database. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changesubscriber [ @subscriber= ] 'subscriber'  
    [ , [ @type= ] type ]  
    [ , [ @login= ] 'login' ]  
    [ , [ @password= ] 'password' ]  
    [ , [ @commit_batch_size= ] commit_batch_size ]  
    [ , [ @status_batch_size= ] status_batch_size ]  
    [ , [ @flush_frequency= ] flush_frequency ]  
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
    [ , [ @description= ] 'description' ]  
    [ , [ @security_mode= ] security_mode ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber on which to change the options. *subscriber* is **sysname**, with no default.  
  
`[ @type = ] type`
 Is the Subscriber type. *type* is **tinyint**, with a default of NULL. **0** indicates a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber. **1** specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or other ODBC data source server Subscriber.  
  
`[ @login = ] 'login'`
 Is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login ID. *login* is **sysname**, with a default of NULL.  
  
`[ @password = ] 'password'`
 Is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication password. *password* is **sysname**, with a default of **%**. **%** indicates there is no change to the password property.  
  
`[ @commit_batch_size = ] commit_batch_size`
 Supported for backward compatibility only.  
  
`[ @status_batch_size = ] status_batch_size`
 Supported for backward compatibility only.  
  
`[ @flush_frequency = ] flush_frequency`
 Supported for backward compatibility only.  
  
`[ @frequency_type = ] frequency_type`
 Is the frequency with which to schedule the distribution task. *frequency_type* is **int**, and can be one of these values.  
  
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
  
`[ @frequency_interval = ] frequency_interval`
 Is the interval for *frequency_type*. *frequency_interval* is **int**, with a default of NULL.  
  
`[ @frequency_relative_interval = ] frequency_relative_interval`
 Is the date of the distribution task. This parameter is used when *frequency_type* is set to **32** (monthly relative). *frequency_relative_interval* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|First|  
|**2**|Second|  
|**4**|Third|  
|**8**|Fourth|  
|**16**|Last|  
  
`[ @frequency_recurrence_factor = ] frequency_recurrence_factor`
 Is how often the distribution task should recur during the defined *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of NULL.  
  
`[ @frequency_subday = ] frequency_subday`
 Is how often to reschedule during the defined period. *frequency_subday* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4**|Minute|  
|**8**|Hour|  
  
`[ @frequency_subday_interval = ] frequency_subday_interval`
 Is the interval for *frequence_subday*. *frequency_subday_interval* is **int**, with a default of NULL.  
  
`[ @active_start_time_of_day = ] active_start_time_of_day`
 Is the time of day when the distribution task is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of NULL.  
  
`[ @active_end_time_of_day = ] active_end_time_of_day`
 Is the time of day when the distribution task stops being scheduled, formatted as HHMMSS. *active_end_time_of_day*is **int**, with a default of NULL.  
  
`[ @active_start_date = ] active_start_date`
 Is the date when the distribution task is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of NULL.  
  
`[ @active_end_date = ] active_end_date`
 Is the date when the distribution task stops being scheduled, formatted as YYYYMMDD. *active_end_date*is **int**, with a default of NULL.  
  
`[ @description = ] 'description'`
 Is an optional text description. *description* is **nvarchar(255)**, with a default of NULL.  
  
`[ @security_mode = ] security_mode`
 Is the implemented security mode. *security_mode* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication|  
|**1**|Windows Authentication|  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when changing article properties on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changesubscriber** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_changesubscriber**.  
  
## See Also  
 [sp_addsubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscriber-transact-sql.md)   
 [sp_dropsubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscriber-transact-sql.md)   
 [sp_helpdistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributiondb-transact-sql.md)   
 [sp_helpserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)   
 [sp_helpsubscriberinfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscriberinfo-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
