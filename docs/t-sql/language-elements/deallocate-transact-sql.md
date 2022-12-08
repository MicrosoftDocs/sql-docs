---
title: "DEALLOCATE (Transact-SQL)"
description: "DEALLOCATE (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "DEALLOCATE"
  - "DEALLOCATE_TSQL"
helpviewer_keywords:
  - "locking [SQL Server], cursors"
  - "DEALLOCATE statement"
  - "deallocations [SQL Server]"
  - "deleting cursor references"
  - "removing cursor references"
dev_langs:
  - "TSQL"
---
# DEALLOCATE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Removes a cursor reference. When the last cursor reference is deallocated, the data structures comprising the cursor are released by [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DEALLOCATE { { [ GLOBAL ] cursor_name } | @cursor_variable_name }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *cursor_name*  
 Is the name of an already declared cursor. If both a global and a local cursor exist with *cursor_name* as their name, *cursor_name* refers to the global cursor if `GLOBAL` is specified and to the local cursor if `GLOBAL` is not specified.  
  
 @*cursor_variable_name*  
 Is the name of a **cursor** variable. @*cursor_variable_name* must be of type **cursor**.  
  
## Remarks  
Statements that operate on cursors use either a cursor name or a cursor variable to refer to the cursor. `DEALLOCATE` removes the association between a cursor and the cursor name or cursor variable. If a name or variable is the last one referencing the cursor, the cursor is deallocated and any resources used by the cursor are freed. Scroll locks used to protect the isolation of fetches are freed at `DEALLOCATE`. Transaction locks used to protect updates, including positioned updates made through the cursor, are held until the end of the transaction.  
  
The `DECLARE CURSOR` statement allocates and associates a cursor with a cursor name.  
  
```sql  
DECLARE abc SCROLL CURSOR FOR  
SELECT * FROM Person.Person;  
```  
  
After a cursor name is associated with a cursor, the name cannot be used for another cursor of the same scope (global or local) until this cursor has been deallocated.  
  
 A cursor variable is associated with a cursor using one of two methods:  
  
-   By name using a `SET` statement that sets a cursor to a cursor variable.  
  
    ```sql  
    DECLARE @MyCrsrRef CURSOR;  
    SET @MyCrsrRef = abc;  
    ```  
  
-   A cursor can also be created and associated with a variable without having a cursor name defined.  
  
    ```sql  
    DECLARE @MyCursor CURSOR;  
    SET @MyCursor = CURSOR LOCAL SCROLL FOR  
    SELECT * FROM Person.Person;  
    ```  
  
 A `DEALLOCATE <@cursor_variable_name>` statement removes only the reference of the named variable to the cursor. The variable is not deallocated until it goes out of scope at the end of the batch, stored procedure, or trigger. After a `DEALLOCATE <@cursor_variable_name>` statement, the variable can be associated with another cursor using the SET statement.  
  
```sql  
USE AdventureWorks2012;  
GO  
  
DECLARE @MyCursor CURSOR;  
SET @MyCursor = CURSOR LOCAL SCROLL FOR  
    SELECT * FROM Sales.SalesPerson;  
  
DEALLOCATE @MyCursor;  
  
SET @MyCursor = CURSOR LOCAL SCROLL FOR  
    SELECT * FROM Sales.SalesTerritory;  
GO  
```  
  
A cursor variable does not have to be explicitly deallocated. The variable is implicitly deallocated when it goes out of scope.  
  
## Permissions  
 Permissions for `DEALLOCATE` default to any valid user.  
  
## Examples  
 The following script shows how cursors persist until the last name or until the variable referencing them has been deallocated.  
  
```sql  
USE AdventureWorks2012;  
GO  
-- Create and open a global named cursor that  
-- is visible outside the batch.  
DECLARE abc CURSOR GLOBAL SCROLL FOR  
    SELECT * FROM Sales.SalesPerson;  
OPEN abc;  
GO  
-- Reference the named cursor with a cursor variable.  
DECLARE @MyCrsrRef1 CURSOR;  
SET @MyCrsrRef1 = abc;  
-- Now deallocate the cursor reference.  
DEALLOCATE @MyCrsrRef1;  
-- Cursor abc still exists.  
FETCH NEXT FROM abc;  
GO  
-- Reference the named cursor again.  
DECLARE @MyCrsrRef2 CURSOR;  
SET @MyCrsrRef2 = abc;  
-- Now deallocate cursor name abc.  
DEALLOCATE abc;  
-- Cursor still exists, referenced by @MyCrsrRef2.  
FETCH NEXT FROM @MyCrsrRef2;  
-- Cursor finally is deallocated when last referencing  
-- variable goes out of scope at the end of the batch.  
GO  
-- Create an unnamed cursor.  
DECLARE @MyCursor CURSOR;  
SET @MyCursor = CURSOR LOCAL SCROLL FOR  
SELECT * FROM Sales.SalesTerritory;  
-- The following statement deallocates the cursor  
-- because no other variables reference it.  
DEALLOCATE @MyCursor;  
GO  
```  
  
## See Also  
 [CLOSE &#40;Transact-SQL&#41;](../../t-sql/language-elements/close-transact-sql.md)   
 [Cursors](../../relational-databases/cursors.md)   
 [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [FETCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/fetch-transact-sql.md)   
 [OPEN &#40;Transact-SQL&#41;](../../t-sql/language-elements/open-transact-sql.md)  
  
  
