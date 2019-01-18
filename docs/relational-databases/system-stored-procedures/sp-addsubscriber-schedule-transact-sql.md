---
title: "sp_addsubscriber_schedule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addsubscriber_schedule_TSQL"
  - "sp_addsubscriber_schedule"
helpviewer_keywords: 
  - "sp_addsubscriber_schedule"
ms.assetid: a6225033-5c3b-452f-ae52-79890a3590ed
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addsubscriber_schedule (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a schedule for the Distribution Agent and Merge Agent. This stored procedure is executed at the Publisher on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addsubscriber_schedule [ @subscriber = ] 'subscriber'  
    [ , [ @agent_type = ] agent_type ]  
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
 [ **@subscriber =** ] **'***subscriber***'**  
 Is the name of the Subscriber. *subscriber* is **sysname**. The name of the Subscriber must be unique in the database, must not already exist, and cannot be NULL.  
  
 [ **@agent_type =** ] *agent_type*  
 Is the type of agent. *agent_type* is **smallint**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0** (default)|Distribution Agent|  
|**1**|Merge Agent|  
  
 [ **@frequency_type =** ] *frequency_type*  
 Is the frequency with which to schedule the Distribution Agent. *frequency_type* is **int**, and can be one of these values.  
  
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
  
 [ **@frequency_interval =** ] *frequency_interval*  
 Is the value to apply to the frequency set by *frequency_type*. *frequency_interval* is **int**, with a default of **1**.  
  
 [ **@frequency_relative_interval =** ] *frequency_relative_interval*  
 Is the date of the Distribution Agent. This parameter is used when *frequency_type* is set to **32** (monthly relative). *frequency_relative_interval* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1** (default)|First|  
|**2**|Second|  
|**4**|Third|  
|**8**|Fourth|  
|**16**|Last|  
  
 [ **@frequency_recurrence_factor =** ] *frequency_recurrence_factor*  
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of **0**.  
  
 [ **@frequency_subday =** ] *frequency_subday*  
 Is how often to reschedule during the defined period. *frequency_subday* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Once|  
|**2**|Second|  
|**4** (default)|Minute|  
|**8**|Hour|  
  
 [ **@frequency_subday_interval =** ] *frequency_subday_interval*  
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with a default of **5**.  
  
 [ **@active_start_time_of_day =** ] *active_start_time_of_day*  
 Is the time of day when the Distribution Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of **0**.  
  
 [ **@active_end_time_of_day =** ] *active_end_time_of_day*  
 Is the time of day when the Distribution Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day*is **int**, with a default of 235959, which means 11:59:59 P.M. as measured on a 24-hour clock.  
  
 [ **@active_start_date =** ] *active_start_date*  
 Is the date when the Distribution Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of **0**.  
  
 [ **@active_end_date =** ] *active_end_date*  
 Is the date when the Distribution Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of 99991231, which means December 31, 9999.  
  
 [ **@publisher =** ] **'***publisher***'**  
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be specified for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addsubscriber_schedule** is used in snapshot replication, transactional replication, and merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_addsubscriber_schedule**.  
  
## See Also  
 [sp_changesubscriber_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changesubscriber-schedule-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
