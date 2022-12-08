---
title: FETCH_STATUS (Transact-SQL)
description: "@@FETCH_STATUS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@FETCH_STATUS"
  - "@@FETCH_STATUS_TSQL"
helpviewer_keywords:
  - "FETCH statement"
  - "status information [SQL Server], FETCH"
  - "@@FETCH_STATUS function"
dev_langs:
  - "TSQL"
---

# &#x40;&#x40;FETCH_STATUS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This function returns the status of the last cursor FETCH statement issued against any cursor currently opened by the connection.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@FETCH_STATUS  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
```sql  
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
  
  
