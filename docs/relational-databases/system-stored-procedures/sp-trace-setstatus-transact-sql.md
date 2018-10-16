---
title: "sp_trace_setstatus (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_trace_setstatus_TSQL"
  - "sp_trace_setstatus"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_trace_setstatus"
ms.assetid: 29e7a7d7-b9c1-414a-968a-fc247769750d
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_trace_setstatus (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Modifies the current state of the specified trace.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_trace_setstatus [ @traceid = ] trace_id , [ @status = ] status  
```  
  
## Arguments  
 [ **@traceid=** ] *trace_id*  
 Is the ID of the trace to be modified. *trace_id* is **int**, with no default. The user employs this *trace_id* value to identify, modify, and control the trace. For information about retrieving the *trace_id*, see [sys.fn_trace_getinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getinfo-transact-sql.md).  
  
 [ **@status=** ] *status*  
 Specifies the action to implement on the trace. *status* is **int**, with no default.  
  
 The following table lists the status that may be specified.  
  
|Status|Description|  
|------------|-----------------|  
|**0**|Stops the specified trace.|  
|**1**|Starts the specified trace.|  
|**2**|Closes the specified trace and deletes its definition from the server.|  
  
> [!NOTE]  
>  A trace must be stopped first before it can be closed. A trace must be stopped and closed first before it can be viewed.  
  
## Return Code Values  
 The following table describes the code values that users may get following completion of the stored procedure.  
  
|Return code|Description|  
|-----------------|-----------------|  
|**0**|No error.|  
|**1**|Unknown error.|  
|**8**|The specified Status is not valid.|  
|**9**|The specified Trace Handle is not valid.|  
|**13**|Out of memory. Returned when there is not enough memory to perform the specified action.|  
  
 If the trace is already in the state specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will return **0**.  
  
## Remarks  
 Parameters of all SQL Trace stored procedures (**sp_trace_xx**) are strictly typed. If these parameters are not called with the correct input parameter data types, as specified in the argument description, the stored procedure will return an error.  
  
 For an example of using trace stored procedures, see [Create a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/create-a-trace-transact-sql.md).  
  
## Permissions  
 User must have ALTER TRACE permission.  
  
## See Also  
 [sys.fn_trace_geteventinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-geteventinfo-transact-sql.md)   
 [sys.fn_trace_getfilterinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getfilterinfo-transact-sql.md)   
 [sp_trace_generateevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-generateevent-transact-sql.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sp_trace_setfilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql.md)   
 [SQL Trace](../../relational-databases/sql-trace/sql-trace.md)  
  
  
