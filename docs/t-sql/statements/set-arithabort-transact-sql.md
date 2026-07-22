---
title: SET ARITHABORT (Transact-SQL)
description: The SET ARITHABORT setting determines whether a query stops when an overflow or divide-by-zero error occurs during query execution.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/21/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ARITHABORT_TSQL"
  - "ARITHABORT"
  - "SET ARITHABORT"
  - "SET_ARITHABORT_TSQL"
helpviewer_keywords:
  - "terminating queries"
  - "queries [SQL Server], terminating"
  - "overflow errors [SQL Server]"
  - "ARITHABORT option"
  - "divide-by-zero errors"
  - "SET ARITHABORT statement"
  - "ending queries [SQL Server]"
  - "stopping queries"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SET ARITHABORT (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

The `SET ARITHABORT` setting determines whether a query stops when an overflow or divide-by-zero error occurs during query execution. 

- When you set `SET ARITHABORT ON` and `SET ANSI_WARNINGS OFF`, arithmetic errors cause the batch to end. If the errors occur in a transaction, the transaction is rolled back.

- If both `SET ARITHABORT` and `SET ANSI_WARNINGS` are `OFF` and an arithmetic error occurs, a warning message appears (unless `SET ARITHIGNORE` is `ON`) and the result of the arithmetic operation is `NULL`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

#### Syntax for [!INCLUDE [ssnoversion-md.md](../../includes/ssnoversion-md.md)], [!INCLUDE [sssodfull-md.md](../../includes/sssodfull-md.md)], [!INCLUDE [fabric](../../includes/fabric.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

```syntaxsql

SET ARITHABORT { ON | OFF }
```

#### Syntax for [!INCLUDE [ssazuresynapse-md.md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [sspdw-md.md](../../includes/sspdw-md.md)]

```syntaxsql

SET ARITHABORT ON
```

## Remarks

When `ANSI_WARNINGS` is `ON` (the default), the setting of `ARITHABORT` has no functional effect. Arithmetic errors cause the query to end, but the batch doesn't abort (as long as the setting `XACT_ABORT` is `OFF`).

> [!WARNING]  
> The default `ARITHABORT` setting for [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) is `ON`, while a client connection in an application defaults to `ARITHABORT OFF`. Even if there's no functional difference as long as `ANSI_WARNINGS` is `ON`, the `ARITHABORT` setting is still a cache key. Therefore, SSMS and an application both using their respective defaults, have different cache entries, and might get different query plans, making it difficult to troubleshoot poorly performing queries. That is, the same query might execute slower in the application than in SSMS. When troubleshooting queries with [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)], always match the client `ARITHABORT` setting.

For expression evaluation, if both `SET ANSI_WARNINGS` and `SET ARITHABORT` are `OFF` and an `INSERT`, `UPDATE`, or `DELETE` statement comes across an arithmetic, overflow, divide-by-zero, or domain error, the query inserts or updates a `NULL` value. If the target column isn't nullable, the insert or update action fails and the user sees an error.

The setting of `SET ARITHABORT` happens at execute or run time and not at parse time.  

`SET ARITHABORT OFF` isn't supported in Azure Synapse Analytics dedicated SQL pools.

## Permissions

Requires membership in the **public** role.  


## View current setting of ARITHABORT

To view the current setting for `SET ARITHABORT`, run the following T-SQL query:

```sql  
DECLARE @ARITHABORT VARCHAR(3) = 'OFF';  
IF ( (64 & @@OPTIONS) = 64 ) SET @ARITHABORT = 'ON';  
SELECT @ARITHABORT AS ARITHABORT;  
```

## Examples

The following example demonstrates the divide-by-zero and overflow errors with different `SET ARITHABORT` settings.  

1. The script creates sample tables `t1` and `t2` and inserts sample data values.
1. Set `ANSI_WARNINGS ON` and run tests to see the default behavior of a divide-by-zero error and arithmetic overflow.
1. Set `ANSI_WARNINGS OFF` so that `ARITHABORT` has effect, and set `ARITHABORT ON`. Run tests to see the behavior with a divide-by-zero error and arithmetic overflow.
1. Set both `ANSI_WARNINGS OFF` and `ARITHABORT OFF`. Run tests to see the behavior with a divide-by-zero error and arithmetic overflow.
1. Clean up the sample tables.

```sql  
-- SET ARITHABORT  
-------------------------------------------------------------------------------  
-- Create tables t1 and t2 and insert data values.  
CREATE TABLE t1 (  
   a TINYINT,   
   b TINYINT  
);  
CREATE TABLE t2 (  
   a TINYINT NOT NULL  
);  
GO  
INSERT INTO t1   
VALUES (1, 0);  
INSERT INTO t1   
VALUES (255, 1);  
GO  

-- First run with ANSI_WARNINGS ON to see the default behavior.
PRINT '*** SET ANSI_WARNINGS ON';  
SET ANSI_WARNINGS ON;
SET XACT_ABORT OFF;    -- To make sure that we have the default setting for this option.  
GO
PRINT '*** Testing divide-by-zero during SELECT';  
GO  
SELECT a / b AS ab   
FROM t1;  
PRINT 'This prints, despite the error message.';
GO  

PRINT '*** Testing divide-by-zero during INSERT';  
GO  
INSERT INTO t2  
SELECT a / b AS ab    
FROM t1;  
PRINT 'This prints, despite the error message.';
GO  

PRINT '*** Testing tinyint overflow';  
GO  
INSERT INTO t2  
SELECT a + b AS ab   
FROM t1;  
PRINT 'This prints, despite the error message.';
GO  

PRINT '*** Resulting data - should be no data';  
GO  
SELECT *   
FROM t2;  
GO  

-- Truncate table t2.  
TRUNCATE TABLE t2;  
GO  

-- Set ANSI_WARNINGS OFF so that ARITHABORT has effect, and set ARITHABORT ON.
PRINT '*** SET ANSI_WARNINGS OFF; SET ARITHABORT ON';  
GO  
SET ANSI_WARNINGS OFF; 
SET ARITHABORT ON;  
GO  

PRINT '*** Testing divide-by-zero during SELECT';  
GO  
SELECT a / b AS ab    
FROM t1;  
PRINT 'This does not print.';
GO  

PRINT '*** Testing divide-by-zero during INSERT';  
GO  
INSERT INTO t2  
SELECT a / b AS ab    
FROM t1;  
PRINT 'This does not print.';
GO  
PRINT '*** Testing tinyint overflow';  
GO  
INSERT INTO t2  
SELECT a + b AS ab   
FROM t1;  
PRINT 'This does not print.';
GO  

PRINT '*** Resulting data - should be 0 rows';  
GO  
SELECT *   
FROM t2;  
GO  

-- Truncate table t2.  
TRUNCATE TABLE t2;  
GO  

-- Set both ANSI_WARNINGS OFF and ARITHABORT OFF.
PRINT '*** SET ARITHABORT OFF';  
GO  
SET ARITHABORT OFF;  
GO  

PRINT '*** Testing divide-by-zero during SELECT';  
GO  
-- Returns NULL.
SELECT a / b AS ab    
FROM t1;  
GO  

PRINT '*** Testing divide-by-zero during INSERT';  
GO  
-- Fails with NOT NULL violation.
INSERT INTO t2  
SELECT a / b AS ab    
FROM t1;  
GO  
PRINT '*** Testing tinyint overflow';  
GO  
-- Fails with NOT NULL violation
INSERT INTO t2  
SELECT a + b AS ab   
FROM t1;  
GO  

PRINT '*** Resulting data - should be 0 rows';  
GO  
SELECT *   
FROM t2;  
GO  

-- Drop tables t1 and t2 and restore ANSI_WARNINGS
DROP TABLE t1;  
DROP TABLE t2;  
SET ANSI_WARNINGS ON
GO
```  

## Related content

- [SET Statements (Transact-SQL)](set-statements-transact-sql.md)
- [SET ARITHIGNORE (Transact-SQL)](set-arithignore-transact-sql.md)
- [SESSIONPROPERTY (Transact-SQL)](../functions/sessionproperty-transact-sql.md)