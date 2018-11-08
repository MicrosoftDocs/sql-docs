---
title: "@@NESTLEVEL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@NESTLEVEL"
  - "@@NESTLEVEL_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@NESTLEVEL function"
  - "nesting stored procedures"
  - "stored procedure nesting levels [SQL Server]"
ms.assetid: 8c0b2134-8616-44f6-addc-6583c432fb62
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;NESTLEVEL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the nesting level of the current stored procedure execution (initially 0) on the local server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@NESTLEVEL  
```  
  
## Return Types  
 **int**  
  
## Remarks  
 Each time a stored procedure calls another stored procedure or executes managed code by referencing a common language runtime (CLR) routine, type, or aggregate, the nesting level is incremented. When the maximum of 32 is exceeded, the transaction is terminated.  
  
 When @@NESTLEVEL is executed within a [!INCLUDE[tsql](../../includes/tsql-md.md)] string, the value returned is 1 + the current nesting level. When @@NESTLEVEL is executed dynamically by using sp_executesql the value returned is 2 + the current nesting level.  
  
## Examples  
  
### A. Using @@NESTLEVEL in a procedure  
 The following example creates two procedures: one that calls the other, and one that displays the `@@NESTLEVEL` setting of each.  
  
```  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID (N'usp_OuterProc', N'P')IS NOT NULL  
    DROP PROCEDURE usp_OuterProc;  
GO  
IF OBJECT_ID (N'usp_InnerProc', N'P')IS NOT NULL  
    DROP PROCEDURE usp_InnerProc;  
GO  
CREATE PROCEDURE usp_InnerProc AS   
    SELECT @@NESTLEVEL AS 'Inner Level';  
GO  
CREATE PROCEDURE usp_OuterProc AS   
    SELECT @@NESTLEVEL AS 'Outer Level';  
    EXEC usp_InnerProc;  
GO  
EXECUTE usp_OuterProc;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
Outer Level  
-----------  
1  
  
Inner Level  
-----------  
2
```  
  
### B. Calling @@NESTLEVEL  
 The following example shows the difference in values returned by `SELECT`, `EXEC`, and `sp_executesql` when each of them calls `@@NESTLEVEL`.  
  
```  
CREATE PROC usp_NestLevelValues AS  
    SELECT @@NESTLEVEL AS 'Current Nest Level';  
EXEC ('SELECT @@NESTLEVEL AS OneGreater');   
EXEC sp_executesql N'SELECT @@NESTLEVEL as TwoGreater' ;  
GO  
EXEC usp_NestLevelValues;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
Current Nest Level  
------------------  
1  
  
(1 row(s) affected)  
  
OneGreater  
-----------  
2  
  
(1 row(s) affected)  
  
TwoGreater  
-----------  
3  
  
(1 row(s) affected)
```  
  
## See Also  
 [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [Create a Stored Procedure](../../relational-databases/stored-procedures/create-a-stored-procedure.md)   
 [@@TRANCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/trancount-transact-sql.md)  
  
  
