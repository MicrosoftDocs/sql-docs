---
title: "OPEN (Transact-SQL)"
description: "OPEN (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "OPEN_TSQL"
  - "OPEN"
helpviewer_keywords:
  - "opening cursors"
  - "cursors [SQL Server], opening"
  - "populating cursors [SQL Server]"
  - "OPEN statement"
  - "Transact-SQL cursors, opening"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# OPEN (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Opens a [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursor and populates the cursor by executing the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement specified on the DECLARE CURSOR or SET *cursor_variable* statement.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
OPEN { { [ GLOBAL ] cursor_name } | cursor_variable_name }  
```  
  
## Arguments
 GLOBAL  
 Specifies that *cursor_name* refers to a global cursor.  
  
 *cursor_name*  
 Is the name of a declared cursor. If both a global and a local cursor exist with *cursor_name* as their name, *cursor_name* refers to the global cursor if GLOBAL is specified; otherwise, *cursor_name* refers to the local cursor.  
  
 *cursor_variable_name*  
 Is the name of a cursor variable that references a cursor.  
  
## Remarks  
 If the cursor is declared with the INSENSITIVE or STATIC option, OPEN creates a temporary table to hold the result set. OPEN fails when the size of any row in the result set exceeds the maximum row size for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables. If the cursor is declared with the KEYSET option, OPEN creates a temporary table to hold the keyset. The temporary tables are stored in tempdb.  
  
 After a cursor has been opened, use the @@CURSOR_ROWS function to receive the number of qualifying rows in the last opened cursor.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support generating keyset-driven or static [!INCLUDE[tsql](../../includes/tsql-md.md)] cursors asynchronously. [!INCLUDE[tsql](../../includes/tsql-md.md)] cursor operations such as OPEN or FETCH are batched, so there is no need for the asynchronous generation of [!INCLUDE[tsql](../../includes/tsql-md.md)] cursors. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] continues to support asynchronous keyset-driven or static application programming interface (API) server cursors where low latency OPEN is a concern, due to client round trips for each cursor operation.  
  
## Examples  
 The following example opens a cursor and fetches all the rows.  
  
```sql  
DECLARE Employee_Cursor CURSOR FOR  
SELECT LastName, FirstName  
FROM AdventureWorks2022.HumanResources.vEmployee  
WHERE LastName like 'B%';  
  
OPEN Employee_Cursor;  
  
FETCH NEXT FROM Employee_Cursor;  
WHILE @@FETCH_STATUS = 0  
BEGIN  
    FETCH NEXT FROM Employee_Cursor  
END;  
  
CLOSE Employee_Cursor;  
DEALLOCATE Employee_Cursor;  
```  
  
## Related content

- [CLOSE (Transact-SQL)](close-transact-sql.md)
- [@@CURSOR_ROWS (Transact-SQL)](../functions/cursor-rows-transact-sql.md)
- [DEALLOCATE (Transact-SQL)](deallocate-transact-sql.md)
- [DECLARE CURSOR (Transact-SQL)](declare-cursor-transact-sql.md)
- [FETCH (Transact-SQL)](fetch-transact-sql.md)
