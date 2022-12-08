---
description: "sp_addsubscriber (Transact-SQL)"
title: "sp_addsubscriber (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_addsubscriber"
  - "sp_addsubscriber_TSQL"
helpviewer_keywords: 
  - "sp_addsubscriber"
ms.assetid: b8a584ea-2a26-4936-965b-b84f026e39c0
author: markingmyname
ms.author: maghan
---
# sp_addsubscriber (Transact-SQL)
[!INCLUDE[sql-asdb](../../includes/applies-to-version/sql-asdb.md)]

  Adds a new Subscriber to a Publisher, enabling it to receive publications. This stored procedure is executed at the Publisher on the publication database for snapshot and transactional publications; and for merge publications using a remote Distributor, this stored procedure is executed at the Distributor.  
  
> [!IMPORTANT]  
>  This stored procedure has been deprecated. You are no longer required to explicitly register a Subscriber at the Publisher.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addsubscriber [ @subscriber = ] 'subscriber'  
    [ , [ @type = ] type ]   
    [ , [ @login = ] 'login' ]  
    [ , [ @password = ] 'password' ]  
    [ , [ @commit_batch_size = ] commit_batch_size ]  
    [ , [ @status_batch_size = ] status_batch_size ]  
    [ , [ @flush_frequency = ] flush_frequency ]  
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
    [ , [ @description = ] 'description' ]  
    [ , [ @security_mode = ] security_mode ]  
    [ , [ @encrypted_password = ] encrypted_password ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @subscriber = ] 'subscriber'`
 Is the name of the server to be added as a valid Subscriber to the publications on this server. *subscriber* is **sysname**, with no default.  
  
`[ @type = ] type`
 Is the type of Subscriber. *type* is **tinyint**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0** (default)|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber|  
|**1**|ODBC data source server|  
|**2**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Jet database|  
|**3**|OLE DB provider|  
  
`[ @login = ] 'login'`
 Is the login ID for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. *login* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @password = ] 'password'`
 Is the password for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. *password* is **nvarchar(524)**, with a default of NULL.  
  
> [!IMPORTANT]  
>  Do not use a blank password. Use a strong password.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @commit_batch_size = ] commit_batch_size`
 This parameter has been deprecated and is maintained for backward compatibility of scripts.  
  
> [!NOTE]  
>  When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @status_batch_size = ] status_batch_size`
 This parameter has been deprecated and is maintained for backward compatibility of scripts.  
  
> [!NOTE]  
>  When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @flush_frequency = ] flush_frequency`
 This parameter has been deprecated and is maintained for backward compatibility of scripts.  
  
> [!NOTE]  
>  When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @frequency_type = ] frequency_type`
 Is the frequency with which to schedule the replication agent. *frequency_type* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|One time|  
|**2**|On demand|  
|**4**|Daily|  
|**8**|Weekly|  
|**16**|Monthly|  
|**32**|Monthly relative|  
|**64** (default)|Autostart|  
|**128**|Recurring|  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @frequency_interval = ] frequency_interval`
 Is the value applied to the frequency set by *frequency_type*. *frequency_interval* is **int**, with a default of 1.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @frequency_relative_interval = ] frequency_relative_interval`
 Is the date of the replication agent. This parameter is used when *frequency_type* is set to **32** (monthly relative). *frequency_relative_interval* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1** (default)|First|  
|**2**|Second|  
|**4**|Third|  
|**8**|Fourth|  
|**16**|Last|  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @frequency_recurrence_factor = ] frequency_recurrence_factor`
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of **0**.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @frequency_subday = ] frequency_subday`
 Is how often to reschedule during the defined period. *frequency_subday* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4** (default)|Minute|  
|**8**|Hour|  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @frequency_subday_interval = ] frequency_subday_interval`
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with a default of **5**.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @active_start_time_of_day = ] active_start_time_of_day`
 Is the time of day when the replication agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of **0**.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @active_end_time_of_day = ] active_end_time_of_day`
 Is the time of day when the replication agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day*is **int**, with a default of 235959, which means 11:59:59 P.M. as measured on a 24-hour clock.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @active_start_date = ] active_start_date`
 Is the date when the replication agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of 0.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @active_end_date = ] active_end_date`
 Is the date when the replication agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of 99991231, which means December 31, 9999.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @description = ] 'description'`
 Is a text description of the Subscriber. *description* is **nvarchar(255)**, with a default of NULL.  
  
`[ @security_mode = ] security_mode`
 Is the implemented security mode. *security_mode* is **int**, with a default of 1. **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. **1** specifies Windows Authentication.  
  
> [!NOTE]  
>  This parameter has been deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). When a value is specified, it will be used as a default when creating subscriptions at this Subscriber and a warning message will be returned.  
  
`[ @encrypted_password = ] encrypted_password`
 This parameter has been deprecated and is provided for backward-compatibility only Setting *encrypted_password* to any value but **0** will result in an error.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when publishing from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addsubscriber** is used in snapshot replication, transactional replication, and merge replication.  
  
 **sp_addsubscriber** is not required when the Subscriber will only have anonymous subscriptions to merge publications.  
  
 **sp_addsubscriber** writes to the [MSsubscriber_info](../../relational-databases/system-tables/mssubscriber-info-transact-sql.md) table in the **distribution** database.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_addsubscriber**.  
  
## See Also  
 [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md)   
 [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md)   
 [sp_changesubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changesubscriber-transact-sql.md)   
 [sp_dropsubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscriber-transact-sql.md)   
 [sp_helpsubscriberinfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscriberinfo-transact-sql.md)  
  
  
