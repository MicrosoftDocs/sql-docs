---
description: "sp_add_alert (Transact-SQL)"
title: "sp_add_alert (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_add_alert"
  - "sp_add_alert_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_alert"
ms.assetid: d9b41853-e22d-4813-a79f-57efb4511f09
author: MashaMSFT
ms.author: mathoma
---
# sp_add_alert (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates an alert.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_alert [ @name = ] 'name'   
     [ , [ @message_id = ] message_id ]   
     [ , [ @severity = ] severity ]   
     [ , [ @enabled = ] enabled ]  
     [ , [ @delay_between_responses = ] delay_between_responses ]   
     [ , [ @notification_message = ] 'notification_message' ]   
     [ , [ @include_event_description_in = ] include_event_description_in ]   
     [ , [ @database_name = ] 'database' ]   
     [ , [ @event_description_keyword = ] 'event_description_keyword_pattern' ]   
     [ , { [ @job_id = ] job_id | [ @job_name = ] 'job_name' } ]   
     [ , [ @raise_snmp_trap = ] raise_snmp_trap ]   
     [ , [ @performance_condition = ] 'performance_condition' ]   
     [ , [ @category_name = ] 'category' ]   
     [ , [ @wmi_namespace = ] 'wmi_namespace' ]  
     [ , [ @wmi_query = ] 'wmi_query' ]  
```  
  
## Arguments  
`[ @name = ] 'name'`
 The name of the alert. The name appears in the e-mail or pager message sent in response to the alert. It must be unique and can contain the percent (**%**) character. *name* is **sysname**, with no default.  
  
`[ @message_id = ] message_id`
 The message error number that defines the alert. (It usually corresponds to an error number in the **sysmessages** table.) *message_id* is **int**, with a default of **0**. If *severity* is used to define the alert, *message_id* must be **0** or NULL.  
  
> [!NOTE]  
>  Only **sysmessages** errors written to the Microsoft Windows application log can cause an alert to be sent.  
  
`[ @severity = ] severity`
 The severity level (from **1** through **25**) that defines the alert. Any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] message stored in the **sysmessages** table sent to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application log with the indicated severity causes the alert to be sent. *severity* is **int**, with a default of 0. If *message_id* is used to define the alert, *severity* must be **0**.  
  
`[ @enabled = ] enabled`
 Indicates the current status of the alert. *enabled* is **tinyint**, with a default of 1 (enabled). If **0**, the alert is not enabled and does not fire.  
  
`[ @delay_between_responses = ] delay_between_responses`
 The wait period, in seconds, between responses to the alert. *delay_between_responses*is **int**, with a default of **0**, which means there is no waiting between responses (each occurrence of the alert generates a response). The response can be in either or both of these forms:  
  
-   One or more notifications sent through e-mail or pager.  
  
-   A job to execute.  
  
 By setting this value, it is possible to prevent, for example, unwanted e-mail messages from being sent when an alert repeatedly occurs in a short period of time.  
  
`[ @notification_message = ] 'notification_message'`
 Is an optional additional message sent to the operator as part of the e-mail, **net send**, or pager notification. *notification_message* is **nvarchar(512)**, with a default of NULL. Specifying *notification_message* is useful for adding special notes such as remedial procedures.  
  
`[ @include_event_description_in = ] include_event_description_in`
 Is whether the description of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error should be included as part of the notification message. *include_event_description_in*is **tinyint**, with a default of **5** (e-mail and **net send**), and can have one or more of these values combined with an **OR** logical operator.  
  
> [!IMPORTANT]
>  The Pager and **net send** options will be removed from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these features in new development work, and plan to modify applications that currently use these features.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|None|  
|**1**|E-mail|  
|**2**|Pager|  
|**4**|**net send**|  
  
`[ @database_name = ] 'database'`
 The database in which the error must occur for the alert to fire. If *database*is not supplied, the alert fires regardless of where the error occurred. *database* is **sysname**. Names that are enclosed in brackets ([ ]) are not allowed. The default value is NULL.  
  
`[ @event_description_keyword = ] 'event_description_keyword_pattern'`
 The sequence of characters that the description of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error must be like. [!INCLUDE[tsql](../../includes/tsql-md.md)] LIKE expression pattern-matching characters can be used. *event_description_keyword_pattern* is **nvarchar(100)**, with a default of NULL. This parameter is useful for filtering object names (for example, **%customer_table%**).  
  
`[ @job_id = ] job_id`
 The job identification number of the job to run in response to this alert. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
`[ @job_name = ] 'job_name'`
 The name of the job to be executed in response to this alert. *job_name*is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
`[ @raise_snmp_trap = ] raise_snmp_trap`
 Not implemented in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0. *raise_snmp_trap* is **tinyint**, with a default of 0.  
  
`[ @performance_condition = ] 'performance_condition'`
 Is a value expressed in the format '*itemcomparatorvalue*'. *performance_condition* is **nvarchar(512)** with a default of NULL, and consists of these elements.  
  
|Format element|Description|  
|--------------------|-----------------|  
|*Item*|A performance object, performance counter, or named instance of the counter|  
|*Comparator*|One of these operators: >, <, or =|  
|*Value*|Numeric value of the counter|  
  
`[ @category_name = ] 'category'`
 The name of the alert category. *category* is **sysname**, with a default of NULL.  
  
`[ @wmi_namespace = ] 'wmi_namespace'`
 The WMI namespace to query for events. *wmi_namespace* is **sysname**, with a default of NULL. Only namespaces on the local server are supported.  
  
`[ @wmi_query = ] 'wmi_query'`
 The query that specifies the WMI event for the alert. *wmi_query* is **nvarchar(512)**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_add_alert** must be run from the **msdb** database.  
  
 These are the circumstances under which errors/messages generated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applications are sent to the Windows application log and can therefore raise alerts:  
  
-   Severity 19 or higher **sys.messages** errors  
  
-   Any RAISERROR statement invoked with WITH LOG syntax  
  
-   Any **sys.messages** error modified or created using **sp_altermessage**  
  
-   Any event logged using **xp_logevent**  
  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage the entire alerting system and is the recommended way to configure an alert infrastructure.  
  
 If an alert is not functioning properly, check whether:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is running.  
  
-   The event appeared in the Windows application log.  
  
-   The alert is enabled.  
  
-   Events generated with **xp_logevent** occur in the master database. Therefore, **xp_logevent** does not trigger an alert unless the **\@database_name** for the alert is **'master'** or NULL.  
  
## Permissions  
 By default, only members of the **sysadmin** fixed server role can execute **sp_add_alert**.  
  
## Examples  
 The following example adds an alert (Test Alert) that runs the `Back up the AdventureWorks2012 Database` job when fired.  
  
> [!NOTE]  
>  This example assumes that the message 55001 and the `Back up the AdventureWorks2012 Database` job already exist. The example is shown for illustrative purposes only.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_alert  
    @name = N'Test Alert',  
    @message_id = 55001,   
   @severity = 0,   
   @notification_message = N'Error 55001 has occurred. The database will be backed up...',   
   @job_name = N'Back up the AdventureWorks2012 Database' ;  
GO  
```  
  
## See Also  
 [sp_add_notification &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-notification-transact-sql.md)   
 [sp_altermessage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-altermessage-transact-sql.md)   
 [sp_delete_alert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-alert-transact-sql.md)   
 [sp_help_alert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-alert-transact-sql.md)   
 [sp_update_alert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-alert-transact-sql.md)   
 [sys.sysperfinfo &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysperfinfo-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
