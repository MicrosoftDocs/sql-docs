---
description: "sp_describe_cursor (Transact-SQL)"
title: "sp_describe_cursor (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_describe_cursor"
  - "sp_describe_cursor_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_describe_cursor"
ms.assetid: 0c836c99-1147-441e-998c-f0a30cd05275
author: markingmyname
ms.author: maghan
---
# sp_describe_cursor (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Reports the attributes of a server cursor.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_describe_cursor [ @cursor_return = ] output_cursor_variable OUTPUT   
     { [ , [ @cursor_source = ] N'local'  
    , [ @cursor_identity = ] N'local_cursor_name' ]   
   | [ , [ @cursor_source = ] N'global'  
    , [ @cursor_identity = ] N'global_cursor_name' ]   
   | [ , [ @cursor_source = ] N'variable'  
     , [ @cursor_identity = ] N'input_cursor_variable' ]   
     }   
[;]  
```  
  
## Arguments  
 [ @cursor_return= ] *output_cursor_variable* OUTPUT  
 Is the name of a declared cursor variable to receive the cursor output. *output_cursor_variable* is **cursor**, with no default, and must not be associated with any cursors at the time sp_describe_cursor is called. The cursor returned is a scrollable, dynamic, read-only cursor.  
  
 [ @cursor_source= ] { N'local' | N'global' | N'variable' }  
 Specifies whether the cursor being reported on is specified by using the name of a local cursor, a global cursor, or a cursor variable. The parameter is **nvarchar(30)**.  
  
 [ @cursor_identity= ] N'*local_cursor_name*' ]  
 Is the name of a cursor created by a DECLARE CURSOR statement that either has the LOCAL keyword or that defaulted to LOCAL. *local_cursor_name* is **nvarchar(128)**.  
  
 [ @cursor_identity= ] N'*global_cursor_name*' ]  
 Is the name of a cursor created by a DECLARE CURSOR statement that either has the GLOBAL keyword or that defaulted to GLOBAL. *global_cursor_name* is **nvarchar(128)**.  
  
 *global_cursor_name* can also be the name of an API server cursor that is opened by an ODBC application that then named by calling SQLSetCursorName.  
  
 [ @cursor_identity= ] N'*input_cursor_variable*' ]  
 Is the name of a cursor variable associated with an open cursor. *input_cursor_variable* is **nvarchar(128)**.  
  
## Return Code Values  
 None  
  
## Cursors Returned  
 sp_describe_cursor encapsulates its result set in a [!INCLUDE[tsql](../../includes/tsql-md.md)] **cursor** output parameter. This enables [!INCLUDE[tsql](../../includes/tsql-md.md)] batches, stored procedures, and triggers to work with the output one row at a time. This also means that the procedure cannot be called directly from database API functions. The **cursor** output parameter must be bound to a program variable, but the database APIs do not support binding **cursor** parameters or variables.  
  
 The following table shows the format of the cursor that is returned by using sp_describe_cursor. The format of the cursor is the same as the format returned by using sp_cursor_list.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|reference_name|**sysname**|Name used to refer to the cursor. If the reference to the cursor was through the name specified on a DECLARE CURSOR statement, the reference name is the same as cursor name. If the reference to the cursor was through a variable, the reference name is the name of the variable.|  
|cursor_name|**sysname**|Name of the cursor from a DECLARE CURSOR statement. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the cursor was created by setting a cursor variable to a cursor, cursor_name returns the name of the cursor variable. In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this output column returns a system-generated name.|  
|cursor_scope|**tinyint**|1 = LOCAL<br /><br /> 2 = GLOBAL|  
|status|**int**|Same values as reported by the CURSOR_STATUS system function:<br /><br /> 1 = The cursor referenced by the cursor name or variable is open. If the cursor is insensitive, static, or keyset, it has at least one row. If the cursor is dynamic, the result set has zero or more rows.<br /><br /> 0 = The cursor referenced by the cursor name or variable is open but has no rows. Dynamic cursors never return this value.<br /><br /> -1 = The cursor referenced by the cursor name or variable is closed.<br /><br /> -2 = Applies only to cursor variables. There is no cursor assigned to the variable. Possibly, an OUTPUT parameter assigned a cursor to the variable, but the stored procedure closed the cursor before returning.<br /><br /> -3 = A cursor or cursor variable with the specified name does not exist, or the cursor variable has not had a cursor allocated to it.|  
|model|**tinyint**|1 = Insensitive (or static)<br /><br /> 2 = Keyset<br /><br /> 3 = Dynamic<br /><br /> 4 = Fast Forward|  
|concurrency|**tinyint**|1 = Read-only<br /><br /> 2 = Scroll locks<br /><br /> 3 = Optimistic|  
|scrollable|**tinyint**|0 = Forward-only<br /><br /> 1 = Scrollable|  
|open_status|**tinyint**|0 = Closed<br /><br /> 1 = Open|  
|cursor_rows|**decimal(10,0)**|Number of qualifying rows in the result set. For more information, see [@@CURSOR_ROWS &#40;Transact-SQL&#41;](../../t-sql/functions/cursor-rows-transact-sql.md).|  
|fetch_status|**smallint**|Status of the last fetch on this cursor. For more information, see [@@FETCH_STATUS &#40;Transact-SQL&#41;](../../t-sql/functions/fetch-status-transact-sql.md).<br /><br /> 0 = Fetch successful.<br /><br /> -1 = Fetch failed or is beyond the bounds of the cursor.<br /><br /> -2 = The requested row is missing.<br /><br /> -9 = There has been no fetch on the cursor.|  
|column_count|**smallint**|Number of columns in the cursor result set.|  
|row_count|**decimal(10,0)**|Number of rows affected by the last operation on the cursor. For more information, see [@@ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/rowcount-transact-sql.md).|  
|last_operation|**tinyint**|Last operation performed on the cursor:<br /><br /> 0 = No operations have been performed on the cursor.<br /><br /> 1 = OPEN<br /><br /> 2 = FETCH<br /><br /> 3 = INSERT<br /><br /> 4 = UPDATE<br /><br /> 5 = DELETE<br /><br /> 6 = CLOSE<br /><br /> 7 = DEALLOCATE|  
|cursor_handle|**int**|A unique value for the cursor within the scope of the server.|  
  
## Remarks  
 sp_describe_cursor describes the attributes that are global to a server cursor, such as the ability to scroll and update. Use sp_describe_cursor_columns for a description of the attributes of the result set returned by the cursor. Use sp_describe_cursor_tables for a report of the base tables referenced by the cursor. To obtain a report of the [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursors visible on the connection, use sp_cursor_list.  
  
 A DECLARE CURSOR statement may request a cursor type that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot support using the SELECT statement that is contained in the DECLARE CURSOR. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implicitly converts the cursor to a type it can support using the SELECT statement. If TYPE_WARNING is specified in the DECLARE CURSOR statement, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sends the application an informational message that a conversion has been completed. sp_describe_cursor can then be called to determine the type of cursor that has been implemented.  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
 The following example opens a global cursor and uses `sp_describe_cursor` to report on the attributes of the cursor.  
  
```  
USE AdventureWorks2012;  
GO  
-- Declare and open a global cursor.  
DECLARE abc CURSOR STATIC FOR  
SELECT LastName  
FROM Person.Person;  
  
OPEN abc;  
  
-- Declare a cursor variable to hold the cursor output variable  
-- from sp_describe_cursor.  
DECLARE @Report CURSOR;  
  
-- Execute sp_describe_cursor into the cursor variable.  
EXEC master.dbo.sp_describe_cursor @cursor_return = @Report OUTPUT,  
        @cursor_source = N'global', @cursor_identity = N'abc';  
  
-- Fetch all the rows from the sp_describe_cursor output cursor.  
FETCH NEXT from @Report;  
WHILE (@@FETCH_STATUS <> -1)  
BEGIN  
    FETCH NEXT from @Report;  
END  
  
-- Close and deallocate the cursor from sp_describe_cursor.  
CLOSE @Report;  
DEALLOCATE @Report;  
GO  
  
-- Close and deallocate the original cursor.  
CLOSE abc;  
DEALLOCATE abc;  
GO  
```  
  
## See Also  
 [Cursors](../../relational-databases/cursors.md)   
 [CURSOR_STATUS &#40;Transact-SQL&#41;](../../t-sql/functions/cursor-status-transact-sql.md)   
 [DECLARE CURSOR &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-cursor-transact-sql.md)   
 [sp_cursor_list &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursor-list-transact-sql.md)   
 [sp_describe_cursor_columns &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-cursor-columns-transact-sql.md)   
 [sp_describe_cursor_tables &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-cursor-tables-transact-sql.md)  
  
  
