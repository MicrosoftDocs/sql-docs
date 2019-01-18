---
title: "sys.fn_get_sql (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "fn_get_sql"
  - "sys.fn_get_sql_TSQL"
  - "fn_get_sql_TSQL"
  - "sys.fn_get_sql"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_get_sql function"
  - "text [SQL Server], SQL handles"
  - "sys.fn_get_sql function"
  - "valid SQL handles [SQL Server]"
  - "SQL handles"
ms.assetid: d5fe49b5-0813-48f2-9efb-9187716b2fd4
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.fn_get_sql (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the text of the SQL statement for the specified SQL handle.  
  
> [!IMPORTANT]  
>  This feature will be removed in a future version of Microsoft SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Use sys.dm_exec_sql_text instead. For more information, see [sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md).  
  
 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_get_sql ( SqlHandle )  
```  
  
## Arguments  
 *SqlHandle*  
 Is the handle value. *SqlHandle* is **varbinary(64)** with no default.  
  
## Tables Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|dbid|**smallint**|Database ID. For ad hoc and prepared SQL statements, the ID of the database where the statements were compiled.|  
|objectid|**int**|ID of the database object. Is NULL for ad hoc SQL statements.|  
|number|**smallint**|Indicates the number of the group, if the procedures are grouped.<br /><br /> 0 = Entries are not procedures.<br /><br /> NULL = Ad hoc SQL statements.|  
|encrypted|**bit**|Indicates whether the object is encrypted.<br /><br /> 0 = Not encrypted<br /><br /> 1 = Encrypted|  
|text|**text**|Is the text of the SQL statement. Is NULL for encrypted objects.|  
  
## Remarks  
 You can obtain a valid SQL handle from the sql_handle column of the [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) dynamic management view.  
  
 If you pass a handle that no longer exists in cache, fn_get_sq**l** returns an empty result set. If you pass a handle that is not valid, the batch stops, and an error message is returned.  
  
 The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] cannot cache some [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, such as bulk copy statements and statements with string literals that are larger than 8 KB. Handles to those statements cannot be retrieved by using fn_get_sql.  
  
 The **text** column of the result set is filtered for text that may contain passwords. For more information about security related stored procedures that are not monitored, see [Filter a Trace](../../relational-databases/sql-trace/filter-a-trace.md).  
  
 The fn_get_sql function returns information that is similar to the [DBCC INPUTBUFFER](../../t-sql/database-console-commands/dbcc-inputbuffer-transact-sql.md) command. The following are examples of when the fn_get_sql function can be used because DBCC INPUTBUFFER cannot be:  
  
-   When events have more than 255 characters.  
  
-   When you have to return the highest current nesting level of a stored procedure. For example, there are two stored procedures that are named sp_1 and sp_2. If sp_1 calls sp_2 and you obtain the handle from the sys.dm_exec_requests dynamic management view while sp_2 is running, the fn_get_sql function returns information about sp_2. Additionally, the fn_get_sql function returns the complete text of the stored procedure at the highest current nesting level.  
  
## Permissions  
 The user needs VIEW SERVER STATE permission on the server.  
  
## Examples  
 Database administrators can use the fn_get_sql function, as shown in the following example, to help diagnose problem processes. After an administrator identifies a problem session ID, the administrator can retrieve the SQL handle for that session, call fn_get_sql with the handle, and then use the start and end offsets to determine the SQL text of the problem session ID.  
  
```  
DECLARE @Handle varbinary(64);  
SELECT @Handle = sql_handle   
FROM sys.dm_exec_requests   
WHERE session_id = 52 and request_id = 0;  
SELECT * FROM sys.fn_get_sql(@Handle);  
GO  
```  
  
## See Also  
 [DBCC INPUTBUFFER &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-inputbuffer-transact-sql.md)   
 [sys.sysprocesses &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysprocesses-transact-sql.md)   
 [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)  
  
  
