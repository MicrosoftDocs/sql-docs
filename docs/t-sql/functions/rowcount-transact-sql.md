---
title: "@@ROWCOUNT (Transact-SQL)"
description: "@@ROWCOUNT (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "08/29/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "@@ROWCOUNT_TSQL"
  - "@@ROWCOUNT"
helpviewer_keywords:
  - "@@ROWCOUNT function"
  - "number of rows affected by statement"
  - "row affected by statements [SQL Server]"
  - "statements [SQL Server], last statement"
  - "counting rows"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;ROWCOUNT (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the number of rows affected by the last statement. If the number of rows is more than 2 billion, use [ROWCOUNT_BIG](../../t-sql/functions/rowcount-big-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
@@ROWCOUNT  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **int**  
  
## Remarks  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] statements can set the value in @@ROWCOUNT in the following ways:  
  
-   Set @@ROWCOUNT to the number of rows affected or read. Rows may or may not be sent to the client.  
  
-   Preserve @@ROWCOUNT from the previous statement execution.  
  
-   Reset @@ROWCOUNT to 0 but do not return the value to the client.  
  
 Statements that make a simple assignment always set the @@ROWCOUNT value to 1. No rows are sent to the client. Examples of these statements are: SET @*local_variable*, RETURN, READTEXT, and select without query statements such as SELECT GETDATE() or SELECT **'***Generic Text***'**.  
  
 Statements that make an assignment in a query or use RETURN in a query set the @@ROWCOUNT value to the number of rows affected or read by the query, for example: SELECT @*local_variable* = c1 FROM t1.  
  
 Data manipulation language (DML) statements set the @@ROWCOUNT value to the number of rows affected by the query and return that value to the client. The DML statements may not send any rows to the client.  
  
 DECLARE CURSOR and FETCH set the @@ROWCOUNT value to 1.  
  
 EXECUTE statements preserve the previous @@ROWCOUNT.  
  
 Statements such as USE, SET \<option>, DEALLOCATE CURSOR, CLOSE CURSOR, PRINT, RAISERROR, BEGIN TRANSACTION, or COMMIT TRANSACTION reset the ROWCOUNT value to 0.  
  
 Natively compiled stored procedures preserve the previous @@ROWCOUNT. [!INCLUDE[tsql](../../includes/tsql-md.md)] statements inside natively compiled stored procedures do not set @@ROWCOUNT. For more information, see [Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/a-guide-to-query-processing-for-memory-optimized-tables.md).  
  
## Examples  
 The following example executes an `UPDATE` statement and uses `@@ROWCOUNT` to detect if any rows were changed.  
  
```sql  
USE AdventureWorks2012;  
GO  
UPDATE HumanResources.Employee   
SET JobTitle = N'Executive'  
WHERE NationalIDNumber = 123456789  
IF @@ROWCOUNT = 0  
PRINT 'Warning: No rows were updated';  
GO  
```  
  
## See Also  
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)   
 [SET ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/statements/set-rowcount-transact-sql.md)  
  
