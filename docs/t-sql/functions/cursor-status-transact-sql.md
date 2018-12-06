---
title: "CURSOR_STATUS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CURSOR_STATUS"
  - "CURSOR_STATUS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "status information [SQL Server], cursors"
  - "CURSOR_STATUS function"
  - "cursors [SQL Server], status information"
ms.assetid: 3a4a840e-04f8-43bd-aada-35d78c3cb6b0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# CURSOR_STATUS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

For a given parameter, `CURSOR_STATUS` shows whether or not a cursor declaration has returned a cursor and result set.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CURSOR_STATUS   
     (  
          { 'local' , 'cursor_name' }   
          | { 'global' , 'cursor_name' }   
          | { 'variable' , 'cursor_variable' }   
     )  
```  
  
## Arguments  
'local'  
Specifies a constant indicating that the cursor source is a local cursor name.
  
'*cursor_name*'  
The name of the cursor. A cursor name must conform to the [database identifier rules](../../relational-databases/databases/database-identifiers.md).
  
'global'  
Specifies a constant indicating that the source of the cursor is a global cursor name.
  
'variable'  
Specifies a constant indicating that the source of the cursor is a local variable.
  
'*cursor_variable*'  
The name of the cursor variable. A cursor variable must be defined using the **cursor** data type.
  
## Return types
**smallint**
  
|Return value|Cursor name|Cursor variable|  
|---|---|---|
|1|The cursor result set has at least one row.<br /><br /> For insensitive and keyset cursors, the result set has at least one row.<br /><br /> For dynamic cursors, the result set can have zero, one, or more rows.|The cursor allocated to this variable is open.<br /><br /> For insensitive and keyset cursors, the result set has at least one row.<br /><br /> For dynamic cursors, the result set can have zero, one, or more rows.|  
|0|The cursor result set is empty.*|The cursor allocated to this variable is open, but the result set is definitely empty.*|  
|-1|The cursor is closed.|The cursor allocated to this variable is closed.|  
|-2|Not applicable.|Has one of these possibilities:<br /><br /> The previously called procedure did not assign a cursor to this OUTPUT variable.<br /><br /> The previously assigned procedure assigned a cursor to this OUTPUT variable, but the cursor was in a closed state when the procedure completed. Therefore, the cursor is deallocated, and not returned to the calling procedure.<br /><br /> No cursor is assigned to the declared cursor variable.|  
|-3|A cursor with the specified name does not exist.|A cursor variable with the specified name does not exist, or if one exists, no cursor is yet allocated to it.|  
  
*Dynamic cursors never return this result.
  
## Examples  
This example uses the `CURSOR_STATUS` function to show the status of a cursor, after its declaration, after it opens, and after it closes.
  
```sql
CREATE TABLE #TMP  
(  
   ii int  
)  
GO  
  
INSERT INTO #TMP(ii) VALUES(1)  
INSERT INTO #TMP(ii) VALUES(2)  
INSERT INTO #TMP(ii) VALUES(3)  
  
GO  
  
--Create a cursor.  
DECLARE cur CURSOR  
FOR SELECT * FROM #TMP  
  
--Display the status of the cursor before and after opening  
--closing the cursor.  
  
SELECT CURSOR_STATUS('global','cur') AS 'After declare'  
OPEN cur  
SELECT CURSOR_STATUS('global','cur') AS 'After Open'  
CLOSE cur  
SELECT CURSOR_STATUS('global','cur') AS 'After Close'  
  
--Remove the cursor.  
DEALLOCATE cur  
  
--Drop the table.  
DROP TABLE #TMP  
  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
After declare
---------------
-1  
  
After Open
----------
1  
  
After Close
-----------
-1
```  
  
## See also
[Cursor Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cursor-functions-transact-sql.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
  
  
