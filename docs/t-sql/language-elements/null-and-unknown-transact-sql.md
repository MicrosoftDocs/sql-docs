---
title: "NULL and UNKNOWN (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 9d491846-4730-4740-a680-77c69fae4a58
caps.latest.revision: 5
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# NULL and UNKNOWN (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-pdw_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-pdw-md.md)]

  NULL indicates that the value is unknown. A null value is different from an empty or zero value. No two null values are equal. Comparisons between two null values, or between a null value and any other value, return unknown because the value of each NULL is unknown.  
  
 Null values generally indicate data that is unknown, not applicable, or to be added later. For example, a customer's middle initial may not be known at the time the customer places an order.  
  
 Note the following about null values:  
  
-   To test for null values in a query, use IS NULL or IS NOT NULL in the WHERE clause.  
  
-   Null values can be inserted into a column by explicitly stating NULL in an INSERT or UPDATE statement or by leaving a column out of an INSERT statement.  
  
-   Null values cannot be used as information that is required to distinguish one row in a table from another row in a table, such as primary keys, or for information used to distribute rows, such as distribution keys.  
  
 When null values are present in data, logical and comparison operators can potentially return a third result of UNKNOWN instead of just TRUE or FALSE. This need for three-valued logic is a source of many application errors. These tables outline the effect of introducing null comparisons.  
  
 The following table shows the results of applying an AND operator to two Boolean operands where one operand returns NULL.  
  
|Operand 1|Operand 2|Result|  
|---------------|---------------|------------|  
|TRUE|NULL|FALSE|  
|NULL|NULL|FALSE|  
|FALSE|NULL|FALSE|  
  
 The following table shows the results of applying an OR operator to two Boolean operands where one operand returns NULL.  
  
|Operand 1|Operand 2|Result|  
|---------------|---------------|------------|  
|TRUE|NULL|TRUE|  
|NULL|NULL|UNKNOWN|  
|FALSE|NULL|UNKNOWN|  
  
## See Also  
 [AND &#40;Transact-SQL&#41;](../../t-sql/language-elements/and-transact-sql.md)   
 [OR &#40;Transact-SQL&#41;](../../t-sql/language-elements/or-transact-sql.md)   
 [NOT &#40;Transact-SQL&#41;](../../t-sql/language-elements/not-transact-sql.md)   
 [IS NULL &#40;Transact-SQL&#41;](../../t-sql/queries/is-null-transact-sql.md)  
  
  