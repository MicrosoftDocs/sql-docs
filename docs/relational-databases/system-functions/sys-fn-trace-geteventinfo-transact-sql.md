---
title: "sys.fn_trace_geteventinfo (Transact-SQL)"
description: "sys.fn_trace_geteventinfo (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "fn_trace_geteventinfo"
  - "fn_trace_geteventinfo_TSQL"
helpviewer_keywords:
  - "events [SQL Server], status information"
  - "fn_trace_geteventinfo function"
  - "sys.fn_trace_geteventinfo function"
  - "status information [SQL Server], events"
dev_langs:
  - "TSQL"
---
# sys.fn_trace_geteventinfo (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information about an event being traced.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_trace_geteventinfo ( trace_id )  
```  
  
## Arguments  
 *trace_id*  
 Is the ID of the trace. *trace_id* is **int**, with no default.  
  
## Tables Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**eventid**|**int**|ID of the traced event|  
|**columnid**|**int**|ID numbers of all columns collected for each event|  
  
## Remarks  
 When passed the ID of a specific trace, **fn_trace_geteventinfo** returns information about that trace. When passed an invalid ID, this function returns an empty rowset.  
  
## Permissions  
 Requires ALTER TRACE permission on the server.  
  
## Examples  
 The following example returns information about trace number 2.  
  
```  
SELECT * FROM fn_trace_geteventinfo(2) ;  
GO  
  
```  
  
## Related content

- [sp_trace_setevent (Transact-SQL)](../system-stored-procedures/sp-trace-setevent-transact-sql.md)
- [sp_trace_setfilter (Transact-SQL)](../system-stored-procedures/sp-trace-setfilter-transact-sql.md)
- [Create a Trace (Transact-SQL)](../sql-trace/create-a-trace-transact-sql.md)
- [sp_trace_create (Transact-SQL)](../system-stored-procedures/sp-trace-create-transact-sql.md)
- [sp_trace_generateevent (Transact-SQL)](../system-stored-procedures/sp-trace-generateevent-transact-sql.md)
- [sp_trace_setstatus (Transact-SQL)](../system-stored-procedures/sp-trace-setstatus-transact-sql.md)
- [sys.fn_trace_getinfo (Transact-SQL)](sys-fn-trace-getinfo-transact-sql.md)
- [sys.fn_trace_gettable (Transact-SQL)](sys-fn-trace-gettable-transact-sql.md)
- [sys.fn_trace_getfilterinfo (Transact-SQL)](sys-fn-trace-getfilterinfo-transact-sql.md)
