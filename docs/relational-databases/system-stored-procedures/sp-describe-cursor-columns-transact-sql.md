---
title: "sp_describe_cursor_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_describe_cursor_columns"
  - "sp_describe_cursor_columns_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_describe_cursor_columns"
ms.assetid: 6eaa54af-7ba4-4fce-bf6c-6ac67cc1ac94
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_describe_cursor_columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports the attributes of the columns in the result set of a server cursor.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_describe_cursor_columns   
   [ @cursor_return = ] output_cursor_variable OUTPUT   
    { [ , [ @cursor_source = ] N'local' ,   
          [ @cursor_identity = ] N'local_cursor_name' ]   
    | [ , [ @cursor_source = ] N'global' ,   
          [ @cursor_identity = ] N'global_cursor_name' ]   
    | [ , [ @cursor_source = ] N'variable' ,   
          [ @cursor_identity = ] N'input_cursor_variable' ]   
   }  
```  
  
## Arguments  
 [ @cursor_return= ] *output_cursor_variable* OUTPUT  
 Is the name of a declared cursor variable to receive the cursor output. *output_cursor_variable* is **cursor**, with no default, and must not be associated with any cursors at the time sp_describe_cursor_columns is called. The cursor returned is a scrollable, dynamic, read-only cursor.  
  
 [ @cursor_source= ] { N'local' | N'global' | N'variable' }  
 Specifies whether the cursor being reported on is specified by using the name of a local cursor, a global cursor, or a cursor variable. The parameter is **nvarchar(30)**.  
  
 [ @cursor_identity= ] N'*local_cursor_name*'  
 Is the name of a cursor created by a DECLARE CURSOR statement that either has the LOCAL keyword or that defaulted to LOCAL. *local_cursor_name* is **nvarchar(128)**.  
  
 [ @cursor_identity= ] N'*global_cursor_name*'  
 Is the name of a cursor created by a DECLARE CURSOR statement that either has the GLOBAL keyword or that defaulted to GLOBAL. *global_cursor_name* is **nvarchar(128)**.  
  
 *global_cursor_name* can also be the name of an API server cursor that is opened by an ODBC application and then named by calling SQLSetCursorName.  
  
 [ @cursor_identity= ] N'*input_cursor_variable*'  
 Is the name of a cursor variable associated with an open cursor. *input_cursor_variable* is **nvarchar(128)**.  
  
## Return Code Values  
 None  
  
## Cursors Returned  
 sp_describe_cursor_columns encapsulates its report as a [!INCLUDE[tsql](../../includes/tsql-md.md)] **cursor** output parameter. This enables [!INCLUDE[tsql](../../includes/tsql-md.md)] batches, stored procedures, and triggers to work with the output one row at a time. This also means that the procedure cannot be called directly from database API functions. The **cursor** output parameter must be bound to a program variable, but the database APIs do not support binding **cursor** parameters or variables.  
  
 The following table shows the format of the cursor returned by using sp_describe_cursor_columns.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|column_name|**sysname** (nullable)|Name assigned to the result set column. The column is NULL if the column was specified without an accompanying AS clause.|  
|ordinal_position|**int**|Relative position of the column from the leftmost column in the result set. The first column is in position 0.|  
|column_characteristics_flags|**int**|A bitmask that indicates the information stored in DBCOLUMNFLAGS in OLE DB. Can be one or a combination of the following:<br /><br /> 1 = Bookmark<br /><br /> 2 = Fixed length<br /><br /> 4 = Nullable<br /><br /> 8 = Row versioning<br /><br /> 16 = Updatable column (set for projected columns of a cursor that has no FOR UPDATE clause and, if there is such a column, can be only one per cursor).<br /><br /> When bit values are combined, the characteristics of the combined bit values apply. For example, if the bit value is 6, the column is a fixed-length (2), nullable (4) column.|  
|column_size|**int**|Maximum possible size for a value in this column.|  
|data_type_sql|**smallint**|Number that indicates the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type of the column.|  
|column_precision|**tinyint**|Maximum precision of the column as per the *bPrecision* value in OLE DB.|  
|column_scale|**tinyint**|Number of digits to the right of the decimal point for the **numeric** or **decimal** data types as per the *bScale* value in OLE DB.|  
|order_position|**int**|If the column participates in the ordering of the result set, the position of the column in the order key relative to the leftmost column.|  
|order_direction|**varchar(1)**(nullable)|A = The column is in the order key and the ordering is ascending.<br /><br /> D = The column is in the order key and the ordering is descending.<br /><br /> NULL = The column does not participate in ordering.|  
|hidden_column|**smallint**|0 = this column appears in the select list.<br /><br /> 1 = Reserved for future use.|  
|columnid|**int**|Column ID of the base column. If the result set column was built from an expression, columnid is -1.|  
|objectid|**int**|Object ID of the object or base table that is supplying the column. If the result set column was built from an expression, objectid is -1.|  
|dbid|**int**|ID of the database that contains the base table that is supplying the column. If the result set column was built from an expression, dbid is -1.|  
|dbname|**sysname**<br /><br /> (nullable)|Name of the database that contains the base table that is supplying the column. If the result set column was built from an expression, dbname is NULL.|  
  
## Remarks  
 sp_describe_cursor_columns describes the attributes of the columns in the result set of a server cursor, such as the name and data type of each cursor. Use sp_describe_cursor for a description of the global attributes of the server cursor. Use sp_describe_cursor_tables for a report of the base tables referenced by the cursor. To obtain a report of the [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursors visible on the connection, use sp_cursor_list.  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
 The following example opens a global cursor and uses `sp_describe_cursor_columns` to report on the columns used in the cursor.  
  
```  
USE AdventureWorks2012;  
GO  
-- Declare and open a global cursor.  
DECLARE abc CURSOR KEYSET FOR  
SELECT LastName  
FROM Person.Person;  
GO  
OPEN abc;  
  
-- Declare a cursor variable to hold the cursor output variable  
-- from sp_describe_cursor_columns.  
DECLARE @Report CURSOR;  
  
-- Execute sp_describe_cursor_columns into the cursor variable.  
EXEC master.dbo.sp_describe_cursor_columns  
    @cursor_return = @Report OUTPUT  
    ,@cursor_source = N'global'   
    ,@cursor_identity = N'abc';  
  
-- Fetch all the rows from the sp_describe_cursor_columns output cursor.  
FETCH NEXT from @Report;  
WHILE (@@FETCH_STATUS <> -1)  
BEGIN  
   FETCH NEXT from @Report;  
END  
  
-- Close and deallocate the cursor from sp_describe_cursor_columns.  
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
 [sp_describe_cursor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-cursor-transact-sql.md)   
 [sp_cursor_list &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursor-list-transact-sql.md)   
 [sp_describe_cursor_tables &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-cursor-tables-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
