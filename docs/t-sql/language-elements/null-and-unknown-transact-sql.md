---
title: "NULL and UNKNOWN (Transact-SQL)"
description: "NULL and UNKNOWN (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current || = azure-sqldw-latest"
---
# NULL and UNKNOWN (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  NULL indicates that the value is unknown. A null value is different from an empty or zero value. No two null values are equal. Comparisons between two null values, or between a null value and any other value, return unknown because the value of each NULL is unknown.  
  
 Null values generally indicate data that is unknown, not applicable, or to be added later. For example, a customer's middle initial may not be known at the time the customer places an order.  
  
 Note the following about null values:  
  
-   To test for null values in a query, use IS NULL or IS NOT NULL in the WHERE clause.  
  
-   Null values can be inserted into a column by explicitly stating NULL in an INSERT or UPDATE statement or by leaving a column out of an INSERT statement.  
  
-   Null values cannot be used as information that is required to distinguish one row in a table from another row in a table, such as primary keys, or for information used to distribute rows, such as distribution keys.  
  
 When null values are present in data, logical and comparison operators can potentially return a third result of UNKNOWN instead of just TRUE or FALSE. This need for three-valued logic is a source of many application errors. Logical operators in a boolean expression that includes UNKNOWNs will return UNKNOWN unless the result of the operator does not depend on the UNKNOWN expression. These tables provide examples of this behavior.  
  
 The following table shows the results of applying an AND operator to two Boolean expressions where one expression returns UNKNOWN.  
  
|Expression 1|Expression 2|Result|  
|---------------|---------------|------------|  
|TRUE|UNKNOWN|UNKNOWN|  
|UNKNOWN|UNKNOWN|UNKNOWN|  
|FALSE|UNKNOWN|FALSE|  
  
 The following table shows the results of applying an OR operator to two Boolean expressions where one expression returns UNKNOWN.  
  
|Expression 1|Expression 2|Result|  
|---------------|---------------|------------|  
|TRUE|UNKNOWN|TRUE|  
|UNKNOWN|UNKNOWN|UNKNOWN|  
|FALSE|UNKNOWN|UNKNOWN|  
  
## See Also  
 [AND &#40;Transact-SQL&#41;](../../t-sql/language-elements/and-transact-sql.md)   
 [OR &#40;Transact-SQL&#41;](../../t-sql/language-elements/or-transact-sql.md)   
 [NOT &#40;Transact-SQL&#41;](../../t-sql/language-elements/not-transact-sql.md)   
 [IS NULL &#40;Transact-SQL&#41;](../../t-sql/queries/is-null-transact-sql.md)  
  
  
