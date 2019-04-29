---
title: "@@ROWCOUNT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@ROWCOUNT_TSQL"
  - "@@ROWCOUNT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@ROWCOUNT function"
  - "number of rows affected by statement"
  - "row affected by statements [SQL Server]"
  - "statements [SQL Server], last statement"
  - "counting rows"
ms.assetid: 97a47998-81d9-4331-a244-9eb8b6fe4a56
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;ROWCOUNT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the number of rows affected by the last statement. If the number of rows is more than 2 billion, use [ROWCOUNT_BIG](../../t-sql/functions/rowcount-big-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@ROWCOUNT  
```  
  
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
  
 Natively compiled stored procedures preserve the previous @@ROWCOUNT. [!INCLUDE[tsql](../../includes/tsql-md.md)] statements inside natively compiled stored procedures do not set @@ROWCOUNT. For more information, see [Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md).  
  
## Examples  
 The following example executes an `UPDATE` statement and uses `@@ROWCOUNT` to detect if any rows were changed.  
  
```  
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
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)   
 [SET ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/statements/set-rowcount-transact-sql.md)  
  
  
