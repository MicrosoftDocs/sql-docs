---
title: "@@CURSOR_ROWS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@CURSOR_ROWS"
  - "@@CURSOR_ROWS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@CURSOR_ROWS function"
  - "cursors [SQL Server], last-opened"
  - "last-opened cursor"
  - "asynchronous cursors [SQL Server]"
ms.assetid: 31bd7a97-7f28-42a8-ba24-24d16d22973d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;CURSOR_ROWS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This returns the number of qualifying rows currently in the last cursor opened on the connection. To improve performance, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can populate large keyset and static cursors asynchronously. `@@CURSOR_ROWS` can be called to determine that the number of the rows that qualify for a cursor are retrieved at the time of the @@CURSOR_ROWS call.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```
@@CURSOR_ROWS  
```  
  
## Return types
**integer**
  
## Return Value  
  
|Return value|Description|  
|---|---|
|-*m*|The cursor populates asynchronously. The value returned (-*m*) is the number of rows currently in the keyset.|  
|-1|The cursor is dynamic. Because dynamic cursors reflect all changes, the number of rows that qualify for the cursor constantly changes. The cursor does not necessarily retrieve all qualified rows.|  
|0|No cursors have been opened, no rows qualified for the last opened cursor, or the last-opened cursor is closed or deallocated.|  
|*n*|The cursor is fully populated. The value returned (*n*) is the total number of rows in the cursor.|  
  
## Remarks  
`@@CURSOR_ROWS` returns a negative number if the last cursor opened asynchronously. Keyset-driver or static cursors open asynchronously if the value for sp_configure cursor threshold exceeds 0, and the number of rows in the cursor result set exceeds the cursor threshold.
  
## Examples  
This example first declares a cursor, and then uses `SELECT` to display the value of `@@CURSOR_ROWS`. The setting has a value of `0` before the cursor opens and then has a value of `-1`, to indicate that the cursor keyset populates asynchronously.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT @@CURSOR_ROWS;  
DECLARE Name_Cursor CURSOR FOR  
SELECT LastName ,@@CURSOR_ROWS FROM Person.Person;  
OPEN Name_Cursor;  
FETCH NEXT FROM Name_Cursor;  
SELECT @@CURSOR_ROWS;  
CLOSE Name_Cursor;  
DEALLOCATE Name_Cursor;  
GO             
```  
  
Here are the result sets.
  
```
-----------
0  
```

```
LastName
---------------
Sanchez
```

```
-----------
-1
```  
  
## See also
[Cursor Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cursor-functions-transact-sql.md)  
[OPEN &#40;Transact-SQL&#41;](../../t-sql/language-elements/open-transact-sql.md)
  
  
