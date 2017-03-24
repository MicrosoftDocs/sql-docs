---
title: "@@CURSOR_ROWS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 36
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# @@CURSOR_ROWS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the number of qualifying rows currently in the last cursor opened on the connection. To improve performance, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can populate large keyset and static cursors asynchronously. @@CURSOR_ROWS can be called to determine that the number of the rows that qualify for a cursor are retrieved at the time @@CURSOR_ROWS is called.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
@@CURSOR_ROWS  
```  
  
## Return Types  
 **integer**  
  
## Return Value  
  
|Return value|Description|  
|------------------|-----------------|  
|-*m*|The cursor is populated asynchronously. The value returned (-*m*) is the number of rows currently in the keyset.|  
|-1|The cursor is dynamic. Because dynamic cursors reflect all changes, the number of rows that qualify for the cursor is constantly changing. It can never be definitely stated that all qualified rows have been retrieved.|  
|0|No cursors have been opened, no rows qualified for the last opened cursor, or the last-opened cursor is closed or deallocated.|  
|*n*|The cursor is fully populated. The value returned (*n*) is the total number of rows in the cursor.|  
  
## Remarks  
 The number returned by @@CURSOR_ROWS is negative if the last cursor was opened asynchronously. Keyset-driver or static cursors are opened asynchronously if the value for sp_configure cursor threshold is greater than 0 and the number of rows in the cursor result set is greater than the cursor threshold.  
  
## Examples  
 The following example declares a cursor and uses `SELECT` to display the value of `@@CURSOR_ROWS`. The setting has a value of `0` before the cursor is opened and a value of `-1` to indicate that the cursor keyset is populated asynchronously.  
  
```  
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
  
 `-----------`  
  
 `0`  
  
 `LastName`  
  
 `---------------`  
  
 `Sanchez`  
  
 `-----------`  
  
 `-1`  
  
## See Also  
 [Cursor Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cursor-functions-transact-sql.md)   
 [OPEN &#40;Transact-SQL&#41;](../../t-sql/language-elements/open-transact-sql.md)  
  
  