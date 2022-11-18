---
description: "Plan Guide Unsuccessful Event Class"
title: "Plan Guide Unsuccessful Event Class"
ms.custom: ""
ms.date: "06/22/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Plan Guide Unsuccessful event class"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Plan Guide Unsuccessful Event Class
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The Plan Guide Unsuccessful event class indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] could not produce an execution plan for a query or batch that contained a plan guide. Instead, the plan was compiled without using the plan guide. The event fires when the following conditions are true:  
  
-   The batch/module in the plan guide definition matches the batch that is being executed.  
  
-   The query in the plan guide definition matches the query that is being executed.  
  
-   The hints in the plan guide definition, including the `USE PLAN` hint, were not applied successfully to the query or batch. That is, the compiled query plan could not honor the specified hints and the plan was compiled without using the plan guide.  
  
 An invalid plan guide might cause this event to fire. Validate the plan guide that is used by the query or batch by using the [sys.fn_validate_plan_guide](../../relational-databases/system-functions/sys-fn-validate-plan-guide-transact-sql.md) function, and correct the error that is reported by this function.  
  
 This event is included in the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] Tuning template.  

> [!NOTE]
> This event class is not available in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
## Plan Guide Unsuccessful Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values that are passed by the application instead of the displayed name of the program.|10|Yes|  
|ClientProcessID|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|DatabaseID|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a specified instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|**nvarchar**|Name of the database in which the user statement is running.|35|Yes|  
|EventClass|**int**|Type of event = 218.|27|No|  
|EventSequence|**int**|Sequence of a specific event within the request.|51|No|  
|HostName|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IsSystem|**int**|Indicates whether the event occurred on a system process or a user process: 1 = system, 0 = user.|60|Yes|  
|LoginName|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\\*username*).|11|Yes|  
|LoginSid|**image**|Security identification number (SID) of the logged-in user. You can find this information in the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) or the [sys.sql_logins](../../relational-databases/system-catalog-views/sys-sql-logins-transact-sql.md) catalog views. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|**nvarchar**|Windows user name.|6|Yes|  
|ObjectID|**int**|Object ID of the module that was being compiled when the plan guide was applied. If the plan guide was not applied to a module, this column is set to NULL.|22|Yes|  
|RequestID|**int**|ID of the request that is containing the statement.|49|Yes|  
|ServerName|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is being traced.|26|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|Name of the plan guide.|1|Yes|  
|TransactionID|**bigint**|System-assigned ID of the transaction.|4|Yes|  
|XactSequence|**bigint**|Token that describes the current transaction.|50|Yes|  
  
## See Also  
 [Plan Guide Successful Event Class](../../relational-databases/event-classes/plan-guide-successful-event-class.md)   
 [Extended Events](../../relational-databases/extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sys.fn_validate_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-validate-plan-guide-transact-sql.md)   
 [sp_create_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
  
  
