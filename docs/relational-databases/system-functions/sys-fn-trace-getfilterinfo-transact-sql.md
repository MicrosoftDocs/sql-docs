---
title: "sys.fn_trace_getfilterinfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "fn_trace_getfilterinfo"
  - "fn_trace_getfilterinfo_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "status information [SQL Server], filters"
  - "sys.fn_trace_getfilterinfo function"
  - "filters [SQL Server], traces"
  - "fn_trace_getfilterinfo function"
ms.assetid: 09fe4a28-ff8a-4655-9da1-4654d5bc514d
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.fn_trace_getfilterinfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about the filters applied to a specified trace.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.  
  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_trace_getfilterinfo ( trace_id )  
```  
  
## Arguments  
 *trace_id*  
 Is the ID of the trace. *trace_id* is **int**, with no default.  
  
## Tables Returned  
 Returns the following information. For more information about the columns, see [sp_trace_setfilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**columnid**|**int**|The ID of the column on which the filter is applied.|  
|**logical_operator**|**int**|Specifies whether the AND or OR operator is applied.|  
|**comparison_operator**|**int**|Specifies the type of comparison made:<br /><br /> 0 = Equal<br /><br /> 1 = Not equal<br /><br /> 2 = Greater than<br /><br /> 3 = Less than<br /><br /> 4 = Greater than or equal<br /><br /> 5 = Less than or equal<br /><br /> 6 = Like<br /><br /> 7 = Not like|  
|**value**|**sql_variant**|Specifies the value on which the filter is applied.|  
  
## Remarks  
 The user sets *trace_id* value to identify, modify, and control the trace. When passed the ID of a specific trace, **fn_trace_getfilterinfo** returns information about any filter on that trace. If the specified trace does not have a filter, this function returns an empty rowset. When passed an invalid ID, this function returns an empty rowset. For similar information about traces, see [sys.fn_trace_getinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getinfo-transact-sql.md).  
  
## Permissions  
 Requires ALTER TRACE permission on the server.  
  
## Examples  
 The following example returns information about all filters on trace number 2.  
  
```  
SELECT * FROM fn_trace_getfilterinfo(2) ;  
GO  
  
```  
  
## See Also  
 [Create a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/create-a-trace-transact-sql.md)   
 [sp_trace_setfilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql.md)   
 [sp_trace_create &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-create-transact-sql.md)   
 [sp_trace_generateevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-generateevent-transact-sql.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sp_trace_setstatus &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setstatus-transact-sql.md)   
 [sys.fn_trace_geteventinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-geteventinfo-transact-sql.md)   
 [sys.fn_trace_getinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getinfo-transact-sql.md)   
 [sys.fn_trace_gettable &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-gettable-transact-sql.md)  
  
  
