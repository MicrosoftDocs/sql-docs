---
title: "FETCH (Transact-SQL)"
description: "FETCH (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "FETCH"
  - "FETCH_TSQL"
helpviewer_keywords:
  - "FETCH statement"
  - "cursors [SQL Server], fetching"
  - "Transact-SQL cursors, fetching and scrolling"
  - "retrieving rows"
  - "fetching [SQL Server]"
  - "SCROLL option"
  - "row fetching [SQL Server]"
dev_langs:
  - "TSQL"
---
# FETCH (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Retrieves a specific row from a [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursor.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
FETCH   
          [ [ NEXT | PRIOR | FIRST | LAST   
                    | ABSOLUTE { n | @nvar }   
                    | RELATIVE { n | @nvar }   
               ]   
               FROM   
          ]   
{ { [ GLOBAL ] cursor_name } | @cursor_variable_name }   
[ INTO @variable_name [ ,...n ] ]   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 NEXT  
 Returns the result row immediately following the current row and increments the current row to the row returned. If `FETCH NEXT` is the first fetch against a cursor, it returns the first row in the result set. `NEXT` is the default cursor fetch option.  
  
 PRIOR  
 Returns the result row immediately preceding the current row, and decrements the current row to the row returned. If `FETCH PRIOR` is the first fetch against a cursor, no row is returned and the cursor is left positioned before the first row.  
  
 FIRST  
 Returns the first row in the cursor and makes it the current row.  
  
 LAST  
 Returns the last row in the cursor and makes it the current row.  
  
 ABSOLUTE { *n*| \@*nvar*}  
 If *n* or \@*nvar* is positive, returns the row *n* rows from the front of the cursor and makes the returned row the new current row. If *n* or \@*nvar* is negative, returns the row *n* rows before the end of the cursor and makes the returned row the new current row. If *n* or \@*nvar* is 0, no rows are returned. *n* must be an integer constant and \@*nvar* must be **smallint**, **tinyint**, or **int**.  
  
 RELATIVE { *n*| \@*nvar*}  
 If *n* or \@*nvar* is positive, returns the row *n* rows beyond the current row and makes the returned row the new current row. If *n* or \@*nvar* is negative, returns the row *n* rows prior to the current row and makes the returned row the new current row. If *n* or \@*nvar* is 0, returns the current row. If `FETCH RELATIVE` is specified with *n* or \@*nvar* set to negative numbers or 0 on the first fetch done against a cursor, no rows are returned. *n* must be an integer constant and \@*nvar* must be **smallint**, **tinyint**, or **int**.  
  
 GLOBAL  
 Specifies that *cursor_name* refers to a global cursor.  
  
 *cursor_name*  
 Is the name of the open cursor from which the fetch should be made. If both a global and a local cursor exist with *cursor_name* as their name, *cursor_name* to the global cursor if GLOBAL is specified and to the local cursor if GLOBAL is not specified.  
  
 \@*cursor_variable_name*  
 Is the name of a cursor variable referencing the open cursor from which the fetch should be made.  
  
 INTO \@*variable_name*[ ,...*n*]  
 Allows data from the columns of a fetch to be placed into local variables. Each variable in the list, from left to right, is associated with the corresponding column in the cursor result set. The data type of each variable must either match or be a supported implicit conversion of the data type of the corresponding result set column. The number of variables must match the number of columns in the cursor select list.  
  
## Remarks  
 If the `SCROLL` option is not specified in an ISO style `DECLARE CURSOR` statement, `NEXT` is the only `FETCH` option supported. If `SCROLL` is specified in an ISO style `DECLARE CURSOR`, all `FETCH` options are supported.  
  
 When the [!INCLUDE[tsql](../../includes/tsql-md.md)] DECLARE cursor extensions are used, these rules apply:  
  
-   If either `FORWARD_ONLY` or `FAST_FORWARD` is specified, `NEXT` is the only `FETCH` option supported.  
  
-   If `DYNAMIC`, `FORWARD_ONLY` or `FAST_FORWARD` are not specified, and one of `KEYSET`, `STATIC`, or `SCROLL` are specified, all `FETCH` options are supported.  
  
-   `DYNAMIC SCROLL` cursors support all the `FETCH` options except `ABSOLUTE`.  
  
 The `@@FETCH_STATUS` function reports the status of the last `FETCH` statement. The same information is recorded in the fetch_status column in the cursor returned by sp_describe_cursor. This status information should be used to determine the validity of the data returned by a `FETCH` statement prior to attempting any operation against that data. For more information, see [@@FETCH_STATUS &#40;Transact-SQL&#41;](../../t-sql/functions/fetch-status-transact-sql.md).  
  
## Permissions  
 Permissions for `FETCH` default to any valid user.  
  
## Examples  
  
### A. Using FETCH in a simple cursor  
 The following example declares a simple cursor for the rows in the `Person.Person` table with a last name that starts with `B`, and uses `FETCH NEXT` to step through the rows. The `FETCH` statements return the value for the column specified in `DECLARE CURSOR` as a single-row result set.  
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE contact_cursor CURSOR FOR  
SELECT LastName FROM Person.Person  
WHERE LastName LIKE 'B%'  
ORDER BY LastName;  
  
OPEN contact_cursor;  
  
-- Perform the first fetch.  
FETCH NEXT FROM contact_cursor;  
  
-- Check @@FETCH_STATUS to see if there are any more rows to fetch.  
WHILE @@FETCH_STATUS = 0  
BEGIN  
   -- This is executed as long as the previous fetch succeeds.  
   FETCH NEXT FROM contact_cursor;  
END  
  
CLOSE contact_cursor;  
DEALLOCATE contact_cursor;  
GO  
```  
  
### B. Using FETCH to store values in variables  
 The following example is similar to example A, except the output of the `FETCH` statements is stored in local variables instead of being returned directly to the client. The `PRINT` statement combines the variables into a single string and returns them to the client.  
  
```sql  
USE AdventureWorks2012;  
GO  
-- Declare the variables to store the values returned by FETCH.  
DECLARE @LastName VARCHAR(50), @FirstName VARCHAR(50);  
  
DECLARE contact_cursor CURSOR FOR  
SELECT LastName, FirstName FROM Person.Person  
WHERE LastName LIKE 'B%'  
ORDER BY LastName, FirstName;  
  
OPEN contact_cursor;  
  
-- Perform the first fetch and store the values in variables.  
-- Note: The variables are in the same order as the columns  
-- in the SELECT statement.   
  
FETCH NEXT FROM contact_cursor  
INTO @LastName, @FirstName;  
  
-- Check @@FETCH_STATUS to see if there are any more rows to fetch.  
WHILE @@FETCH_STATUS = 0  
BEGIN  
  
   -- Concatenate and display the current values in the variables.  
   PRINT 'Contact Name: ' + @FirstName + ' ' +  @LastName  
  
   -- This is executed as long as the previous fetch succeeds.  
   FETCH NEXT FROM contact_cursor  
   INTO @LastName, @FirstName;  
END  
  
CLOSE contact_cursor;  
DEALLOCATE contact_cursor;  
GO  
```  
  
### C. Declaring a SCROLL cursor and using the other FETCH options  
 The following example creates a `SCROLL` cursor to allow full scrolling capabilities through the `LAST`, `PRIOR`, `RELATIVE`, and `ABSOLUTE` options.  
  
```sql  
USE AdventureWorks2012;  
GO  
-- Execute the SELECT statement alone to show the   
-- full result set that is used by the cursor.  
SELECT LastName, FirstName FROM Person.Person  
ORDER BY LastName, FirstName;  
  
-- Declare the cursor.  
DECLARE contact_cursor SCROLL CURSOR FOR  
SELECT LastName, FirstName FROM Person.Person  
ORDER BY LastName, FirstName;  
  
OPEN contact_cursor;  
  
-- Fetch the last row in the cursor.  
FETCH LAST FROM contact_cursor;  
  
-- Fetch the row immediately prior to the current row in the cursor.  
FETCH PRIOR FROM contact_cursor;  
  
-- Fetch the second row in the cursor.  
FETCH ABSOLUTE 2 FROM contact_cursor;  
  
-- Fetch the row that is three rows after the current row.  
FETCH RELATIVE 3 FROM contact_cursor;  
  
-- Fetch the row that is two rows prior to the current row.  
FETCH RELATIVE -2 FROM contact_cursor;  
  
CLOSE contact_cursor;  
DEALLOCATE contact_cursor;  
GO  
```  
  
## See Also  
 [CLOSE &#40;Transact-SQL&#41;](../../t-sql/language-elements/close-transact-sql.md)   
 [DEALLOCATE &#40;Transact-SQL&#41;](../../t-sql/language-elements/deallocate-transact-sql.md)   
 [DECLARE CURSOR &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-cursor-transact-sql.md)   
 [OPEN &#40;Transact-SQL&#41;](../../t-sql/language-elements/open-transact-sql.md)  
  
  
