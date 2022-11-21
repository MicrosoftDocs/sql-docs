---
description: "sp_trace_generateevent (Transact-SQL)"
title: "sp_trace_generateevent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_trace_generateevent_TSQL"
  - "sp_trace_generateevent"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_trace_generateevent"
ms.assetid: 3ef05bfb-b467-4403-89cc-6e77ef9247dd
author: markingmyname
ms.author: maghan
---
# sp_trace_generateevent (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates a user-defined event in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
> This stored procedure is **not** deprecated. All other trace-related stored procedures are deprecated.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_trace_generateevent [ @eventid = ] event_id   
     [ , [ @userinfo = ] 'user_info' ]  
     [ , [ @userdata = ] user_data ]  
```  
  
## Arguments  
`[ @eventid = ] event_id`
 Is the ID of the event to turn on. *event_id* is **int**, with no default. The ID must be one of the event numbers from 82 through 91, which represent user-defined events as set with [sp_trace_setevent](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md).  
  
`[ @userinfo = ] 'user_info'`
 Is the optional user-defined string identifying the reason for the event. *user_info* is **nvarchar(128)**, with a default of NULL.  
  
`[ @userdata = ] user_data`
 Is the optional user-specified data for the event. *user_data* is **varbinary(8000)**, with a default of NULL.  
  
## Return Code Values  
 The following table describes the code values that users may get following completion of the stored procedure.  
  
|Return code|Description|  
|-----------------|-----------------|  
|**0**|No error.|  
|**1**|Unknown error.|  
|**3**|The specified event is not valid. The event may not exist or it is not an appropriate one for the store procedure.|  
|**13**|Out of memory. Returned when there is not enough memory to perform the specified action.|  
  
## Remarks  
 **sp_trace_generateevent** performs many of the actions previously executed by the **xp_trace_\*** extended stored procedures. Use **sp_trace_generateevent** instead of **xp_trace_generate_event**.  
  
 Only ID numbers of user-defined events may be used with **sp_trace_generateevent**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will raise an error if other event ID numbers are used.  
  
 Parameters of all SQL Trace stored procedures (**sp_trace_xx**) are strictly typed. If these parameters are not called with the correct input parameter data types, as specified in the argument description, the stored procedure will return an error.  
  
## Permissions  
 User must have ALTER TRACE permission.  
  
## Examples  
 The following example creates a user-configurable event on a sample table.  
  
```  
--Create a sample table.  
CREATE TABLE user_config_test(col1 int, col2 char(10));  
  
--DROP the trigger if it already exists.  
IF EXISTS  
   (SELECT * FROM sysobjects WHERE name = 'userconfig_trg')  
   DROP TRIGGER userconfig_trg;  
  
--Create an ON INSERT trigger on the sample table.  
CREATE TRIGGER userconfig_trg  
   ON user_config_test FOR INSERT;  
AS  
EXEC master..sp_trace_generateevent  
   @event_class = 82, @userinfo = N'Inserted row into user_config_test';  
  
--When an insert action happens, the user-configurable event fires. If   
you were capturing the event id=82, you will see it in the Profiler output.  
INSERT INTO user_config_test VALUES(1, 'abc');  
```  
  
## See also  
 [sys.fn_trace_geteventinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-geteventinfo-transact-sql.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [SQL Trace](../../relational-databases/sql-trace/sql-trace.md)  
  
  
