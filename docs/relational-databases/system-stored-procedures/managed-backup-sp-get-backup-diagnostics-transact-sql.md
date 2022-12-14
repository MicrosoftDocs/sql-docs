---
description: "managed_backup.sp_get_backup_diagnostics (Transact-SQL)"
title: "managed_backup.sp_get_backup_diagnostics (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_get_backup_diagnostics_TSQL"
  - "sp_get_backup_diagnostics"
  - "smart_admin.sp_get_backup_diagnostics_TSQL"
  - "smart_admin.sp_get_backup_diagnostics"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_get_backup_diagnostics"
  - "smart_admin.sp_get_backup_diagnostics"
ms.assetid: 2266a233-6354-464b-91ec-824ca4eb9ceb
author: markingmyname
ms.author: maghan
---
# managed_backup.sp_get_backup_diagnostics (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Returns the Extended Events logged by Smart Admin.  
  
 Use this stored procedure to monitor Extended Events logged by Smart Admin. [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] events are logged in this system and can be reviewed  and monitored using this stored procedure.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
managed_backup.sp_get_backup_diagnostics [@xevent_channel = ] 'event type' [, [@begin_time = ] 'time1' ] [, [@end_time = ] 'time2'VARCHAR(255) = 'Xevent',@begin_time DATETIME = NULL,@end_time DATETIME = NULL  
```  
  
##  <a name="Arguments"></a> Arguments  
 @xevent_channel  
 The type of Extended Event. The default value is set to return all events logged for the previous 30 minutes. The events logged depend on the type of Extended Events enabled. You can use this parameter to filter the stored procedure to show only events of a certain type. You can either specify the full event name or specify a substring such as: **'Admin'**, **'Analytic'**, **'Operational'**, and **'Debug'**. The @event_channel is **VARCHAR (255)**.  
  
 To get a list of event types currently enabled use the **managed_backup.fn_get_current_xevent_settings** function.  
  
 [@begin_time  
 The start of the time period from which the events should be displayed. The @begin_time parameter is DATETIME with a default value of NULL. If this is not specified then the events from the past 30 minutes is displayed.  
  
 @end_time  
 The end of the time period up to which the events should be displayed. The @end_time parameter is DATATIME with a default value of NULL.  If this is not specified then the events up to the current time is displayed.  
  
## Table Returned  
 This stored procedure returns a table with the following information:  
  
| Column Name | Data Type | Description |  
| ----------- | --------- | ----------- |
|event_type|NVARCHAR(512)|Type of Extended Event.|  
|Event|NVARCHAR(512)|Summary of the event logs.|  
|Timestamp|TIMESTAMP|Timestamp of the event that shows when the event was raised.|  
  
## Security  
  
### Permissions  
 Requires **EXECUTE** permissions on the stored procedure. It also requires **VIEW SERVER STATE** permissions since it internally calls other system objects that require this permission.  
  
## Examples  
 The following example returns all the events logged for the past 30 minutes  
  
```  
Use msdb  
Go  
EXEC managed_backup.sp_get_backup_diagnostics  
  
```  
  
 The following example returns all the events logged  for a specific time range.  
  
```  
Use msdb  
Go  
EXEC managed_backup.sp_get_backup_diagnostics @xevent_channel = 'Admin',  
  @begin_time = '2013-06-01', @end_time = '2013-06-10'  
  
```  
  
 The following example returns all the analytical events logged for the past 30 minutes  
  
```  
Use msdb  
Go  
EXEC managed_backup.sp_get_backup_diagnostics @xevent_channel = 'Analytic'  
  
```  
  
  
