---
title: "= (Assignment Operator) (Transact-SQL)"
description: "= (Assignment Operator) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
helpviewer_keywords:
  - "operators [Transact-SQL], assignment"
  - "assignment operators [Transact-SQL]"
  - "headings [SQL Server columns]"
  - "relationships [SQL Server], assignment operators"
  - "column headings [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# = (Assignment Operator) (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics Parallel Data Warehouse](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The equal sign (=) is the only [!INCLUDE[tsql](../../includes/tsql-md.md)] assignment operator. In the following example, the `@MyCounter` variable is created, and then the assignment operator sets `@MyCounter` to a value returned by an expression.  
  
```sql  
DECLARE @MyCounter INT;  
SET @MyCounter = 1;  
```  
  
 The assignment operator can also be used to establish the relationship between a column heading and the expression that defines the values for the column. The following example displays the column headings `FirstColumnHeading` and `SecondColumnHeading`. The string `xyz` is displayed in the `FirstColumnHeading` column heading for all rows. Then, each product ID from the `Product` table is listed in the `SecondColumnHeading` column heading.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT FirstColumnHeading = 'xyz',  
       SecondColumnHeading = ProductID  
FROM Production.Product;  
GO  
```  
  
## See Also  
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
  
  
