---
title: "@@FETCH_STATUS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@FETCH_STATUS"
  - "@@FETCH_STATUS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "FETCH statement"
  - "status information [SQL Server], FETCH"
  - "@@FETCH_STATUS function"
ms.assetid: 93659193-e4ff-4dfb-9043-0c4114921b91
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;FETCH_STATUS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the status of the last cursor FETCH statement issued against any cursor currently opened by the connection.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@FETCH_STATUS  
```  
  
## Return Type  
 **integer**  
  
## Return Value  
  
|Return value|Description|  
|------------------|-----------------|  
|&nbsp;0|The FETCH statement was successful.|  
|-1|The FETCH statement failed or the row was beyond the result set.|  
|-2|The row fetched is missing.|
|-9|The cursor is not performing a fetch operation.|  
  
## Remarks  
Because `@@FETCH_STATUS` is global to all cursors on a connection, use it carefully. After a FETCH statement executes, the test for `@@FETCH_STATUS` must occur before any other FETCH statement executes against another cursor. `@@FETCH_STATUS` is undefined before any fetches have occurred on the connection.  
  
For example, a user executes a FETCH statement from one cursor, and then calls a stored procedure that opens and processes results from another cursor. When control returns from that called stored procedure, `@@FETCH_STATUS` reflects the last FETCH executed inside that stored procedure, not the FETCH statement executed before the call to the stored procedure.  
  
To retrieve the last fetch status of a specific cursor, query the **fetch_status** column of the **sys.dm_exec_cursors** dynamic management function.  
  
## Examples  
This example uses `@@FETCH_STATUS` to control cursor activities in a `WHILE` loop.  
  
```  
DECLARE Employee_Cursor CURSOR FOR  
SELECT BusinessEntityID, JobTitle  
FROM AdventureWorks2012.HumanResources.Employee;  
OPEN Employee_Cursor;  
FETCH NEXT FROM Employee_Cursor;  
WHILE @@FETCH_STATUS = 0  
   BEGIN  
      FETCH NEXT FROM Employee_Cursor;  
   END;  
CLOSE Employee_Cursor;  
DEALLOCATE Employee_Cursor;  
GO  
```  
  
## See Also  
 [Cursor Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cursor-functions-transact-sql.md)   
 [FETCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/fetch-transact-sql.md)  
  
  
