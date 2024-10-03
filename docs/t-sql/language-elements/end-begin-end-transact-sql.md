---
title: "END (BEGIN...END) (Transact-SQL)"
description: "END (BEGIN...END) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "END"
  - "END_TSQL"
helpviewer_keywords:
  - "enclosing statements [SQL Server]"
  - "END keyword"
  - "BEGIN...END keyword"
  - "END (BEGIN...END) keyword"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current||=fabric"
---
# END (BEGIN...END) (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

  Encloses a series of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that will execute as a group. BEGIN...END blocks can be nested.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
BEGIN   
     { sql_statement | statement_block }   
END   
```  
  
## Arguments
 { *sql_statement*| *statement_block*}  
 Is any valid [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or statement grouping as defined with a statement block. To define a statement block (batch), use the control-of-flow language keywords BEGIN and END. Although all [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are valid within a BEGIN...END block, certain [!INCLUDE[tsql](../../includes/tsql-md.md)] statements should not be grouped together within the same batch (statement block).  
  
## Result Types  
 **Boolean**  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 In the following example, `BEGIN` and `END` define a series of [!INCLUDE[DWsql](../../includes/dwsql-md.md)] statements that run together. If the `BEGIN...END` block are not included, the following example will be in a continuous loop.  
  
```sql  
-- Uses AdventureWorks  
  
DECLARE @Iteration INTEGER = 0  
WHILE @Iteration <10  
BEGIN  
    SELECT FirstName, MiddleName   
    FROM dbo.DimCustomer WHERE LastName = 'Adams';  
SET @Iteration += 1  
END;  
```  
  
## See Also  
 [ALTER TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-trigger-transact-sql.md)   
 [BEGIN...END &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-end-transact-sql.md)   
 [Control-of-Flow Language &#40;Transact-SQL&#41;](~/t-sql/language-elements/control-of-flow.md)   
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [ELSE &#40;IF...ELSE&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/else-if-else-transact-sql.md)   
 [IF...ELSE &#40;Transact-SQL&#41;](../../t-sql/language-elements/if-else-transact-sql.md)   
 [WHILE &#40;Transact-SQL&#41;](../../t-sql/language-elements/while-transact-sql.md)  
  
  


