---
description: "sp_changesubscriber_schedule (Transact-SQL)"
title: "sp_changesubscriber_schedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_changesubscriber_schedule"
  - "sp_changesubscriber_schedule_TSQL"
helpviewer_keywords: 
  - "sp_changesubscriber_schedule"
ms.assetid: ff84e8e2-d496-482c-b23e-38a6626596e6
author: markingmyname
ms.author: maghan
---
# sp_changesubscriber_schedule (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Changes the Distribution Agent or Merge Agent schedule for a subscriber. This stored procedure is executed at the Publisher on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changesubscriber_schedule [ @subscriber = ] 'subscriber', [ @agent_type = ] type  
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
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**. The name of the Subscriber must be unique in the database, must not already exist, and cannot be NULL.  
  
`[ @agent_type = ] type`
 Is the type of agent. *type* is **smallint**, with a default of **0**. **0** indicates a Distribution Agent. **1** indicates a Merge Agent.  
  
`[ @frequency_type = ] frequency_type`
 Is the frequency with which to schedule the distribution task. *frequency_type* is **int**, with a default of **64**. There are 10 schedule columns.  
  
`[ @frequency_interval = ] frequency_interval`
 Is the value applied to the frequency set by *frequency_type*. *frequency_interval* is **int**, with a default of **1**.  
  
`[ @frequency_relative_interval = ] frequency_relative_interval`
 Is the date of the distribution task. *frequency_relative_interval* is **int**, with a default of **1**.  
  
`[ @frequency_recurrence_factor = ] frequency_recurrence_factor`
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of **0**.  
  
`[ @frequency_subday = ] frequency_subday`
 Is how often, in minutes, to reschedule during the defined period. *frequency_subday* is **int**, with a default of **4**.  
  
`[ @frequency_subday_interval = ] frequency_subday_interval`
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with a default of **5**.  
  
`[ @active_start_time_of_day = ] active_start_time_of_day`
 Is the time of day when the distribution task is first scheduled. *active_start_time_of_day* is **int**, with a default of **0**.  
  
`[ @active_end_time_of_day = ] active_end_time_of_day`
 Is the time of day when the distribution task stops being scheduled. *active_end_time_of_day* is **int**, with a default of **235959**, which means 11:59:59 P.M. on a 24-hour clock.  
  
`[ @active_start_date = ] active_start_date`
 Is the date when the distribution task is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of **0**.  
  
`[ @active_end_date = ] active_end_date`
 Is the date when the distribution task stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of **99991231**, which means December 31, 9999.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when changing article properties on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changesubscriber_schedule** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_changesubscriber_schedule**.  
  
## See Also  
 [sp_addsubscriber_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscriber-schedule-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
