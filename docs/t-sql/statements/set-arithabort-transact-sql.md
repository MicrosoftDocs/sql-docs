---
title: "SET ARITHABORT (Transact-SQL)"
description: SET ARITHABORT (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/03/2024
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
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SET ARITHABORT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Ends a query when an overflow or divide-by-zero error occurs during query execution. 
  
:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

### Syntax for [!INCLUDE [ssnoversion-md.md](../../includes/ssnoversion-md.md)], [!INCLUDE [sssodfull-md.md](../../includes/sssodfull-md.md)], [!INCLUDE [fabric](../../includes/fabric.md)]
```syntaxsql
SET ARITHABORT { ON | OFF }
```

### Syntax for [!INCLUDE [ssazuresynapse-md.md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [sspdw-md.md](../../includes/sspdw-md.md)]
```syntaxsql
SET ARITHABORT ON
```
  

## Remarks
When the setting ANSI_WARNINGS is ON (the default), the setting of ARITHABORT has no functional effect. Arithmetic errors cause the query to end, but the batch will not be aborted (as long as the setting XACT_ABORT is OFF).
  
> [!WARNING]  
> The default ARITHABORT setting for [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is ON, while a client connection in an application defaults to ARITHABORT OFF. Even if there is no functional difference as long as ANSI_WARNINGS is ON, the setting for ARITHABORT is still a cache key. Therefore, SSMS and an application both using their respective defaults, will have different cache entries, and may get different query plans, making it difficult to troubleshoot poorly performing queries. That is, the same query might execute fast in management studio but slow in the application. When troubleshooting queries with [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)], always match the client ARITHABORT setting.    
 
When SET ARITHABORT is ON and SET ANSI WARNINGS is OFF, these arithmetic errors cause the batch to end. If the errors occur in a transaction, the transaction is rolled back. 
  
If both SET ARITHABORT and SET ANSI WARNINGS are OFF and one of these errors occurs, a warning message appears (unless SET ARITHIGNORE is ON), and the result of the arithmetic operation is `NULL`.  

  
For expression evaluation, if both SET ANSI_WARNINGS and SET ARITHABORT are OFF and an INSERT, UPDATE, or DELETE statement comes across an arithmetic, overflow, divide-by-zero, or domain error, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] inserts or updates a `NULL` value. If the target column isn't nullable, the insert or update action fails and the user sees an error.  
  
The setting of SET ARITHABORT happens at execute or run time and not at parse time.  

SET ARITHABORT OFF is not supported in Azure Synapse Analytics dedicated SQL pools.

To view the current setting for SET ARITHABORT, run the following query:
  
```sql  
DECLARE @ARITHABORT VARCHAR(3) = 'OFF';  
IF ( (64 & @@OPTIONS) = 64 ) SET @ARITHABORT = 'ON';  
SELECT @ARITHABORT AS ARITHABORT;  
```
  
## Permissions

Requires membership in the **public** role.  
  
## Examples

The following example demonstrates the divide-by-zero and overflow errors that have `SET ARITHABORT` settings.  
  
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

-- First run with ANSI_WARNINGS to see the default behaviour.
PRINT '*** SET ANSI_WARNINGS ON';  
SET ANSI_WARNINGS ON;
SET XACT_ABORT OFF;    -- To make sure that we have the default setting for this option.  
go  
PRINT '*** Testing divide by zero during SELECT';  
GO  
SELECT a / b AS ab   
FROM t1;  
PRINT 'This prints, despite the error message.';
GO  
  
PRINT '*** Testing divide by zero during INSERT';  
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

-- Turn off ANSI_WARNINGS so that ARITHABORT has any affect, and let ARITHABORT be ON.
PRINT '*** SET ANSI_WARNINGS OFF; SET ARITHABORT ON';  
GO  
SET ANSI_WARNINGS OFF; 
SET ARITHABORT ON;  
GO  
  
PRINT '*** Testing divide by zero during SELECT';  
GO  
SELECT a / b AS ab    
FROM t1;  
PRINT 'This does not print.';
GO  
  
PRINT '*** Testing divide by zero during INSERT';  
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
PRINT 'This does not print.' ;
GO  
  
PRINT '*** Resulting data - should be 0 rows';  
GO  
SELECT *   
FROM t2;  
GO  
  
-- Truncate table t2.  
TRUNCATE TABLE t2;  
GO  

-- SET ARITHABORT to OFF
PRINT '*** SET ARITHABORT OFF';  
GO  
SET ARITHABORT OFF;  
GO  
  
PRINT '*** Testing divide by zero during SELECT';  
GO  
-- Returns NULL.
SELECT a / b AS ab    
FROM t1;  
GO  
  
PRINT '*** Testing divide by zero during INSERT';  
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

- [SET Statements (Transact-SQL)](../../t-sql/statements/set-statements-transact-sql.md)
- [SET ARITHIGNORE (Transact-SQL)](../../t-sql/statements/set-arithignore-transact-sql.md)
- [SESSIONPROPERTY (Transact-SQL)](../../t-sql/functions/sessionproperty-transact-sql.md)

