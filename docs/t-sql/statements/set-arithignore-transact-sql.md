---
title: SET ARITHIGNORE (Transact-SQL)
description: The SET ARITHIGNORE setting controls whether the query returns error messages for arithmetic overflow or divide-by-zero errors.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/21/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SET ARITHIGNORE"
  - "SET_ARITHIGNORE_TSQL"
  - "ARITHIGNORE"
  - "ARITHIGNORE_TSQL"
helpviewer_keywords:
  - "SET ARITHIGNORE statement"
  - "overflow errors [SQL Server]"
  - "ARITHIGNORE option"
  - "divide-by-zero errors"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SET ARITHIGNORE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

The `SET ARITHIGNORE` setting controls whether the query returns error messages for arithmetic overflow or divide-by-zero errors. The `SET ARITHIGNORE ON` setting suppresses the messages "Division by zero occurred." or "Arithmetic overflow occurred."

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

#### Syntax for [!INCLUDE [ssnoversion-md.md](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-dw](../../includes/fabric-dw-full.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

```syntaxsql

SET ARITHIGNORE { ON | OFF }
```

#### Syntax for [!INCLUDE [ssazuresynapse-md.md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [sspdw-md.md](../../includes/sspdw-md.md)]

```syntaxsql

SET ARITHIGNORE OFF
```

## Remarks

- If both `SET ARITHABORT` and `SET ANSI_WARNINGS` are `OFF` and an arithmetic error occurs, a warning message appears when `SET ARITHIGNORE` is `OFF`, and the result of the arithmetic operation is `NULL`.
- If both `SET ARITHABORT` and `SET ANSI_WARNINGS` are `OFF` and an arithmetic error occurs, a warning message does not appear if `SET ARITHIGNORE` is `ON`, and the result of the arithmetic operation is `NULL`.

The `ARITHIGNORE` setting only controls whether the query returns an error message. The SQL Database Engine returns `NULL` in a calculation involving an overflow or divide-by-zero error, regardless of this setting. This setting doesn't affect errors that occur during `INSERT`, `UPDATE`, and `DELETE` statements.

If either `SET ARITHABORT` or `SET ARITHIGNORE` is `OFF`, and `SET ANSI_WARNINGS` is `ON`, the query still returns an error message when it encounters divide-by-zero or overflow errors. When `ANSI_WARNINGS` is `ON` (the default), the setting of `ARITHABORT` has no functional effect.

The setting of `SET ARITHIGNORE` is set at execute or run time and not at parse time.

[!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## View current setting of ARITHIGNORE

To view the current setting of `ARITHIGNORE`, run the following T-SQL query.

```sql  
DECLARE @ARITHIGNORE VARCHAR(3) = 'OFF';  
IF ( (128 & @@OPTIONS) = 128 ) SET @ARITHIGNORE = 'ON';
SELECT @ARITHIGNORE AS ARITHIGNORE;  
```  

## Permissions

 Requires membership in the **public** role.  

## Examples

 The following example demonstrates using both `SET ARITHIGNORE` settings with both types of query errors: divide-by-zero and arithmetic overflow. 

```sql  
SET ARITHABORT OFF;  
SET ANSI_WARNINGS OFF  
GO  

PRINT 'Setting ARITHIGNORE ON';  
GO  
-- SET ARITHIGNORE ON and testing.  
SET ARITHIGNORE ON;  
GO  
SELECT 1 / 0 AS DivideByZero;  
GO  
SELECT CAST(256 AS TINYINT) AS Overflow;  
GO  

PRINT 'Setting ARITHIGNORE OFF';  
GO  
-- SET ARITHIGNORE OFF and testing.  
SET ARITHIGNORE OFF;  
GO  
SELECT 1 / 0 AS DivideByZero;  
GO  
SELECT CAST(256 AS TINYINT) AS Overflow;  
GO  
```

## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

The following example demonstrates divide-by-zero and the overflow errors. This example doesn't return an error message for these errors because `ARITHIGNORE` is `OFF`.    

```sql  
-- SET ARITHIGNORE OFF and testing.  
SET ARITHIGNORE OFF;  
SELECT 1 / 0 AS DivideByZero;  
SELECT CAST(256 AS TINYINT) AS Overflow;  
```  

## Related content

- [SET Statements (Transact-SQL)](set-statements-transact-sql.md)
- [SET ARITHABORT (Transact-SQL)](set-arithabort-transact-sql.md)