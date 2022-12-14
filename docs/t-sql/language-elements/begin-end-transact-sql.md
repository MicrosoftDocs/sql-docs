---
title: "BEGIN...END (Transact-SQL)"
description: "BEGIN...END (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "BEGIN"
  - "BEGIN_TSQL"
helpviewer_keywords:
  - "enclosing statements [SQL Server]"
  - "BEGIN statement"
  - "control-of-flow language [SQL Server], BEGIN...END statement"
  - "BEGIN...END keyword"
  - "grouping statements, BEGIN...END statement"
  - "executing Transact-SQL statements together [SQL Server]"
  - "statements [SQL Server], grouping"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# BEGIN...END (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Encloses a series of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements so that a group of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements can be executed. BEGIN and END are control-of-flow language keywords.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
BEGIN  
    { sql_statement | statement_block }   
END  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 { *sql_statement* | *statement_block* }  
 Is any valid [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or statement grouping as defined by using a statement block.  
  
## Remarks  
 BEGIN...END blocks can be nested.  
  
 Although all [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are valid within a BEGIN...END block, certain [!INCLUDE[tsql](../../includes/tsql-md.md)] statements should not be grouped together within the same batch, or statement block.  
  
## Examples  
 In the following example, `BEGIN` and `END` define a series of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that execute together. If the `BEGIN...END` block were not included, both `ROLLBACK TRANSACTION` statements would execute and both `PRINT` messages would be returned.  
  
```sql
USE AdventureWorks2012
GO  
BEGIN TRANSACTION
GO  
IF @@TRANCOUNT = 0  
BEGIN  
    SELECT FirstName, MiddleName   
    FROM Person.Person WHERE LastName = 'Adams';
    ROLLBACK TRANSACTION;
    PRINT N'Rolling back the transaction two times would cause an error.';
END;
ROLLBACK TRANSACTION;
PRINT N'Rolled back the transaction.';
GO  
/*  
Rolled back the transaction.  
*/  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 In the following example, `BEGIN` and `END` define a series of [!INCLUDE[DWsql](../../includes/dwsql-md.md)] statements that run together. If the `BEGIN...END` block are not included, the following example will be in a continuous loop.  
  
```sql
-- Uses AdventureWorks  

DECLARE @Iteration Integer = 0;
WHILE @Iteration <10  
BEGIN  
    SELECT FirstName, MiddleName   
    FROM dbo.DimCustomer WHERE LastName = 'Adams';
    SET @Iteration += 1  ;
END;
```  
  
## See Also  
 [ALTER TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-trigger-transact-sql.md)   
 [Control-of-Flow Language &#40;Transact-SQL&#41;](~/t-sql/language-elements/control-of-flow.md)   
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [END &#40;BEGIN...END&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/end-begin-end-transact-sql.md)
