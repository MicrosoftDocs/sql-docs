---
title: "SET ARITHABORT (Transact-SQL)"
description: SET ARITHABORT (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "12/04/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET ARITHABORT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Ends a query when an overflow or divide-by-zero error occurs during query execution.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  

### Syntax for [!INCLUDE[ssnoversion-md.md](../../includes/ssnoversion-md.md)] and [!INCLUDE[sssodfull-md.md](../../includes/sssodfull-md.md)]
```syntaxsql
SET ARITHABORT { ON | OFF }
```

### Syntax for [!INCLUDE[sssdw-md.md](../../includes/sssdw-md.md)] and [!INCLUDE[sspdw-md.md](../../includes/sspdw-md.md)]
```syntaxsql
SET ARITHABORT ON
```
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks
Always set ARITHABORT to ON in your logon sessions. Setting ARITHABORT to OFF can negatively impact query optimization, leading to performance issues.  
  
> [!WARNING]  
>  The default ARITHABORT setting for [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is ON. Client applications setting ARITHABORT to OFF might receive different query plans, making it difficult to troubleshoot poorly performing queries. That is, the same query might execute fast in management studio but slow in the application. When troubleshooting queries with [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], always match the client ARITHABORT setting.  
  
When SET ARITHABORT and SET ANSI WARNINGS are ON, these error conditions cause the query to end.  
  
When SET ARITHABORT is ON and SET ANSI WARNINGS is OFF, these error conditions cause the batch to end. If the errors occur in a transaction, the transaction is rolled back. When SET ARITHABORT is OFF and one of these errors occurs, a warning message appears and the result of the arithmetic operation is NULL.  
  
If SET ARITHABORT and SET ANSI WARNINGS are OFF and one of these errors occurs, a warning message appears, and the result of the arithmetic operation is NULL.  
  
> [!NOTE]  
>  If neither SET ARITHABORT nor SET ARITHIGNORE is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns NULL and a warning message appears after the query runs.  
  
When ANSI_WARNINGS has a value of ON and the database compatibility level is set to 90 or higher then ARITHABORT is implicitly ON regardless of its value setting. If the database compatibility level is set to 80 or earlier, the ARITHABORT option must be explicitly set to ON.  
  
For expression evaluation, if SET ARITHABORT is OFF and an INSERT, UPDATE, or DELETE statement comes across an arithmetic, overflow, divide-by-zero, or domain error, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] inserts or updates a NULL value. If the target column isn't nullable, the insert or update action fails and the user sees an error.  
  
When either SET ARITHABORT or SET ARITHIGNORE is OFF and SET ANSI_WARNINGS is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] still returns an error message when encountering divide-by-zero or overflow errors.  
  
When SET ARITHABORT is OFF and an abort error occurs during the evaluation of the Boolean condition of an IF statement, the FALSE branch executes.
  
SET ARITHABORT must be ON when you're creating or changing indexes on computed columns or indexed views. If SET ARITHABORT is OFF, CREATE, UPDATE, INSERT, and DELETE statements on tables with indexes on computed columns or indexed views fail.
  
The setting of SET ARITHABORT happens at execute or run time and not at parse time.  
  
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
   a TINYINT  
);  
GO  
INSERT INTO t1   
VALUES (1, 0);  
INSERT INTO t1   
VALUES (255, 1);  
GO  
  
PRINT '*** SET ARITHABORT ON';  
GO  
-- SET ARITHABORT ON and testing.  
SET ARITHABORT ON;  
GO  
  
PRINT '*** Testing divide by zero during SELECT';  
GO  
SELECT a / b AS ab   
FROM t1;  
GO  
  
PRINT '*** Testing divide by zero during INSERT';  
GO  
INSERT INTO t2  
SELECT a / b AS ab    
FROM t1;  
GO  
  
PRINT '*** Testing tinyint overflow';  
GO  
INSERT INTO t2  
SELECT a + b AS ab   
FROM t1;  
GO  
  
PRINT '*** Resulting data - should be no data';  
GO  
SELECT *   
FROM t2;  
GO  
  
-- Truncate table t2.  
TRUNCATE TABLE t2;  
GO  
  
-- SET ARITHABORT OFF and testing.  
PRINT '*** SET ARITHABORT OFF';  
GO  
SET ARITHABORT OFF;  
GO  
  
-- This works properly.  
PRINT '*** Testing divide by zero during SELECT';  
GO  
SELECT a / b AS ab    
FROM t1;  
GO  
  
-- This works as if SET ARITHABORT was ON.  
PRINT '*** Testing divide by zero during INSERT';  
GO  
INSERT INTO t2  
SELECT a / b AS ab    
FROM t1;  
GO  
PRINT '*** Testing tinyint overflow';  
GO  
INSERT INTO t2  
SELECT a + b AS ab   
FROM t1;  
GO  
  
PRINT '*** Resulting data - should be 0 rows';  
GO  
SELECT *   
FROM t2;  
GO  
  
-- Drop tables t1 and t2.  
DROP TABLE t1;  
DROP TABLE t2;  
GO  
```  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET ARITHIGNORE &#40;Transact-SQL&#41;](../../t-sql/statements/set-arithignore-transact-sql.md)   
 [SESSIONPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/sessionproperty-transact-sql.md)  
  
  
