---
title: "sp_trace_setfilter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_trace_setfilter"
  - "sp_trace_setfilter_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_trace_setfilter"
ms.assetid: 11e7c7ac-a581-4a64-bb15-9272d5c1f7ac
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_trace_setfilter (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Applies a filter to a trace. **sp_trace_setfilter** may be executed only on existing traces that are stopped (*status* is **0**). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error if this stored procedure is executed on a trace that does not exist or whose *status* is not **0**.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_trace_setfilter [ @traceid = ] trace_id   
          , [ @columnid = ] column_id  
          , [ @logical_operator = ] logical_operator  
          , [ @comparison_operator = ] comparison_operator  
          , [ @value = ] value  
```  
  
## Arguments  
 [ **@traceid=** ] *trace_id*  
 Is the ID of the trace to which the filter is set. *trace_id* is **int**, with no default. The user employs this *trace_id* value to identify, modify, and control the trace.  
  
 [ **@columnid=** ] *column_id*  
 Is the ID of the column on which the filter is applied. *column_id* is **int**, with no default. If *column_id* is NULL, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clears all filters for the specified trace.  
  
 [ **@logical_operator** = ] *logical_operator*  
 Specifies whether the AND (**0**) or OR (**1**) operator is applied. *logical_operator* is **int**, with no default.  
  
 [ **@comparison_operator=** ] *comparison_operator*  
 Specifies the type of comparison to be made. *comparison_operator* is **int**, with no default. The table contains the comparison operators and their representative values.  
  
|Value|Comparison operator|  
|-----------|-------------------------|  
|**0**|= (Equal)|  
|**1**|<> (Not Equal)|  
|**2**|> (Greater Than)|  
|**3**|< (Less Than)|  
|**4**|>= (Greater Than Or Equal)|  
|**5**|<= (Less Than Or Equal)|  
|**6**|LIKE|  
|**7**|NOT LIKE|  
  
 [ **@value=** ] *value*  
 Specifies the value on which to filter. The data type of *value* must match the data type of the column to be filtered. For example, if the filter is set on an Object ID column that is an **int** data type, *value* must be **int**. If *value* is **nvarchar** or **varbinary**, it can have a maximum length of 8000.  
  
 When the comparison operator is LIKE or NOT LIKE, the logical operator can include "%" or other filter appropriate for the LIKE operation.  
  
 You can specify NULL for *value* to filter out events with NULL column values. Only **0** (= Equal) and **1** (<> Not Equal) operators are valid with NULL. In this case, these operators are equivalent to the [!INCLUDE[tsql](../../includes/tsql-md.md)] IS NULL and IS NOT NULL operators.  
  
 To apply the filter between a range of column values, **sp_trace_setfilter** must be executed twice -- once with a greater-than-or-equals ('>=') comparison operator, and another time with a less-than-or-equals ('<=') operator.  
  
 For more information about data column data types, see the [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md).  
  
## Return Code Values  
 The following table describes the code values that users may get following completion of the stored procedure.  
  
|Return code|Description|  
|-----------------|-----------------|  
|0|No error.|  
|1|Unknown error.|  
|2|The trace is currently running. Changing the trace at this time results in an error.|  
|4|The specified Column is not valid.|  
|5|The specified Column is not allowed for filtering. This value is returned only from **sp_trace_setfilter**.|  
|6|The specified Comparison Operator is not valid.|  
|7|The specified Logical Operator is not valid.|  
|9|The specified Trace Handle is not valid.|  
|13|Out of memory. Returned when there is not enough memory to perform the specified action.|  
|16|The function is not valid for this trace.|  
  
## Remarks  
 **sp_trace_setfilter** is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure that performs many of the actions previously executed by extended stored procedures available in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use **sp_trace_setfilter** instead of the **xp_trace_set\*filter** extended stored procedures to create, apply, remove, or manipulate filters on traces. For more information, see [Filter a Trace](../../relational-databases/sql-trace/filter-a-trace.md).  
  
 All filters for a particular column must be enabled together in one execution of **sp_trace_setfilter**. For example, if a user intends to apply two filters on the application name column and one filter on the username column, the user must specify the filters on application name in sequence. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error if the user attempts to specify a filter on application name in one stored procedure call, followed by a filter on username, then another filter on application name.  
  
 Parameters of all SQL Trace stored procedures (**sp_trace_xx**) are strictly typed. If these parameters are not called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.  
  
## Permissions  
 User must have ALTER TRACE permission.  
  
## Examples  
 The following example sets three filters on Trace `1`. The filters `N'SQLT%'` and `N'MS%'` operate on one column (`AppName`, value `10`) using the "`LIKE`" comparison operator. The filter `N'joe'` operates on a different column (`UserName`, value `11`) using the "`EQUAL`" comparison operator.  
  
```  
sp_trace_setfilter  1, 10, 0, 6, N'SQLT%';  
sp_trace_setfilter  1, 10, 0, 6, N'MS%';  
sp_trace_setfilter  1, 11, 0, 0, N'joe';  
```  
  
## See Also  
 [sys.fn_trace_getfilterinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getfilterinfo-transact-sql.md)   
 [sys.fn_trace_getinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getinfo-transact-sql.md)   
 [SQL Trace](../../relational-databases/sql-trace/sql-trace.md)  
  
  
