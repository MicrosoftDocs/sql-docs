---
title: "sp_describe_cursor_tables (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_describe_cursor_tables_TSQL"
  - "sp_describe_cursor_tables"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_describe_cursor_tables"
ms.assetid: 02c0f81a-54ed-4ca4-aa4f-bb7463a9ab9a
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_describe_cursor_tables (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports the objects or base tables referenced by a server cursor.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_describe_cursor_tables   
     [ @cursor_return = ] output_cursor_variable OUTPUT   
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
 [ @cursor_return= ] *output_cursor_variable*OUTPUT  
 Is the name of a declared cursor variable to receive the cursor output. *output_cursor_variable* is **cursor**, with no default, and must not be associated with any cursors at the time sp_describe_cursor_tables is called. The cursor returned is a scrollable, dynamic, read-only cursor.  
  
 [ @cursor_source= ] { N'local' | N'global' | N'variable' }  
 Specifies whether the cursor being reported on is specified by using the name of a local cursor, a global cursor, or a cursor variable. The parameter is **nvarchar(30)**.  
  
 [ @cursor_identity= ] N'*local_cursor_name*'  
 Is the name of a cursor created by a DECLARE CURSOR statement either having the LOCAL keyword, or that defaulted to LOCAL. *local_cursor_name* is **nvarchar(128)**.  
  
 [ @cursor_identity= ] N'*global_cursor_name*'  
 Is the name of a cursor created by a DECLARE CURSOR statement either having the GLOBAL keyword, or that defaulted to GLOBAL. *global_cursor_name* can also be the name of an API server cursor opened by an ODBC application that then named the cursor by calling SQLSetCursorName.*global_cursor_name* is **nvarchar(128)**.  
  
 [ @cursor_identity= ] N'*input_cursor_variable*'  
 Is the name of a cursor variable associated with an open cursor. *input_cursor_variable* is **nvarchar(128)**.  
  
## Return Code Values  
 None  
  
## Cursors Returned  
 sp_describe_cursor_tables encapsulates its report as a [!INCLUDE[tsql](../../includes/tsql-md.md)] **cursor** output parameter. This enables [!INCLUDE[tsql](../../includes/tsql-md.md)] batches, stored procedures, and triggers to work with the output one row at a time. This also means that the procedure cannot be called directly from API functions. The **cursor** output parameter must be bound to a program variable, but the APIs do not support bind **cursor** parameters or variables.  
  
 The following table shows the format of the cursor that is returned by sp_describe_cursor_tables.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|table owner|**sysname**|User ID of the table owner.|  
|Table_name|**sysname**|Name of the object or base table. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], server cursors always return the user-specified object, not the base tables.|  
|Optimizer_hints|**smallint**|Bitmap that is made up of one or more of the following:<br /><br /> 1 = Row-level locking (ROWLOCK)<br /><br /> 4 = Page-level locking (PAGELOCK)<br /><br /> 8 = Table lock (TABLOCK)<br /><br /> 16 = Exclusive table lock (TABLOCKX)<br /><br /> 32 = Update lock (UPDLOCK)<br /><br /> 64 = No lock (NOLOCK)<br /><br /> 128 = Fast first-row option (FASTFIRST)<br /><br /> 4096 = Read repeatable semantic when used with DECLARE CURSOR (HOLDLOCK)<br /><br /> When multiple options are supplied, the system uses the most restrictive. However, sp_describe_cursor_tables shows the flags that are specified in the query.|  
|lock_type|**smallint**|Scroll-lock type requested either explicitly or implicitly for each base table that underlies this cursor. The value can be one of the following:<br /><br /> 0 = None<br /><br /> 1 = Shared<br /><br /> 3 = Update|  
|server_name|**sysname, nullable**|Name of the linked server that the table resides on. NULL when OPENQUERY or OPENROWSET are used.|  
|Objectid|**int**|Object ID of the table. 0 when OPENQUERY or OPENROWSET are used.|  
|dbid|**int**|ID of the database that the table resides in. 0 when OPENQUERY or OPENROWSET are used.|  
|dbname|**sysname**, **nullable**|Name of the database that the table resides in. NULL when OPENQUERY or OPENROWSET are used.|  
  
## Remarks  
 sp_describe_cursor_tables describes the base tables that are referenced by a server cursor. For a description of the attributes of the result set returned by the cursor, use sp_describe_cursor_columns. For a description of the global characteristics of the cursor, such as its scrollability and updatability, use sp_describe_cursor. To obtain a report of the [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursors that are visible on the connection, use sp_cursor_list.  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
 The following example opens a global cursor and uses `sp_describe_cursor_tables` to report on the tables that are referenced by the cursor.  
  
```  
USE AdventureWorks2012;  
GO  
-- Declare and open a global cursor.  
DECLARE abc CURSOR KEYSET FOR  
SELECT LastName  
FROM Person.Person  
WHERE LastName LIKE 'S%';  
  
OPEN abc;  
GO  
-- Declare a cursor variable to hold the cursor output variable  
-- from sp_describe_cursor_tables.  
DECLARE @Report CURSOR;  
  
-- Execute sp_describe_cursor_tables into the cursor variable.  
EXEC master.dbo.sp_describe_cursor_tables  
      @cursor_return = @Report OUTPUT,  
      @cursor_source = N'global', @cursor_identity = N'abc';  
  
-- Fetch all the rows from the sp_describe_cursor_tables output cursor.  
FETCH NEXT from @Report;  
WHILE (@@FETCH_STATUS <> -1)  
BEGIN  
   FETCH NEXT from @Report;  
END  
  
-- Close and deallocate the cursor from sp_describe_cursor_tables.  
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
 [sp_describe_cursor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-cursor-transact-sql.md)   
 [sp_describe_cursor_columns &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-cursor-columns-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
