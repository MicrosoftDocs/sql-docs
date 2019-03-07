---
title: "sys.fn_trace_getinfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "fn_trace_getinfo"
  - "fn_trace_getinfo_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "traces [SQL Server], status information"
  - "status information [SQL Server], traces"
  - "sys.fn_trace_getinfo function"
  - "fn_trace_getinfo function"
ms.assetid: 04b140fe-110a-47b8-98b5-e4c161beb6c9
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.fn_trace_getinfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about a specified trace or all existing traces.  
  
> **IMPORTANT!** [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.    
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_trace_getinfo ( { trace_id | NULL | 0 | DEFAULT } )  
```  
  
## Arguments  
 *trace_id*  
 Is the ID of the trace. *trace_id* is **int**.  Valid inputs are the ID number of a trace, NULL, 0, or DEFAULT. NULL, 0, and DEFAULT are equivalent values in this context. Specify NULL, 0, or DEFAULT to return information for all traces in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Tables Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|traceid|**int**|ID of the trace.|  
|property|**int**|Property of the trace:<br /><br /> 1= Trace options. For more information, see @options in [sp_trace_create &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-create-transact-sql.md).<br /><br /> 2 = File name<br /><br /> 3 = Max size<br /><br /> 4 = Stop time<br /><br /> 5 = Current trace status. 0 = stopped. 1 = running.|  
|value|**sql_variant**|Information about the property of the trace specified.|  
  
## Remarks  
 When passed the ID of a specific trace, fn_trace_getinfo returns information about that trace. When passed an invalid ID, this function returns an empty rowset.  
  
 fn_trace_getinfo appends a .trc extension to the name of any trace file included in its result set. For information on defining a trace, see [sp_trace_create &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-create-transact-sql.md). For similar information about trace filters, see [sys.fn_trace_getfilterinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getfilterinfo-transact-sql.md).  
  
 For a complete example of using trace stored procedures, see [Create a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/create-a-trace-transact-sql.md).  
  
## Permissions  
 Requires ALTER TRACE permission on the server.  
  
## Examples  
 The following example returns information about all active traces.  
  
```  
SELECT * FROM sys.fn_trace_getinfo(0) ;  
GO  
```  
  
## See Also  
 [Create a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/create-a-trace-transact-sql.md)   
 [sp_trace_create &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-create-transact-sql.md)   
 [sp_trace_generateevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-generateevent-transact-sql.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sp_trace_setfilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql.md)   
 [sp_trace_setstatus &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setstatus-transact-sql.md)   
 [sys.fn_trace_getfilterinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getfilterinfo-transact-sql.md)   
 [sys.fn_trace_geteventinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-geteventinfo-transact-sql.md)   
 [sys.fn_trace_gettable &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-gettable-transact-sql.md)  
  
  
