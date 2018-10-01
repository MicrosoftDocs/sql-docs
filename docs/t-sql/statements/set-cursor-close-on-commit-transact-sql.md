---
title: "SET CURSOR_CLOSE_ON_COMMIT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CURSOR_CLOSE_ON_COMMIT"
  - "SET CURSOR_CLOSE_ON_COMMIT"
  - "CURSOR_CLOSE_ON_COMMIT_TSQL"
  - "SET_CURSOR_CLOSE_ON_COMMIT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CURSOR_CLOSE_ON_COMMIT option"
  - "transactions [SQL Server], cursors"
  - "closing cursors"
  - "cursors [SQL Server], closing"
  - "SET CURSOR_CLOSE_ON_COMMIT statement"
ms.assetid: 7b976154-98ce-4a06-bbae-7e59c34211f7
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SET CURSOR_CLOSE_ON_COMMIT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Controls the behavior of the [!INCLUDE[tsql](../../includes/tsql-md.md)] COMMIT TRANSACTION statement. The default value for this setting is OFF. This means that the server will not close cursors when you commit a transaction.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SET CURSOR_CLOSE_ON_COMMIT { ON | OFF }  
```  
  
## Remarks  
 When SET CURSOR_CLOSE_ON_COMMIT is ON, this setting closes any open cursors on commit or rollback in compliance with ISO. When SET CURSOR_CLOSE_ON_COMMIT is OFF, the cursor is not closed when a transaction is committed.  
  
> [!NOTE]  
>  SET CURSOR_CLOSE_ON_COMMIT to ON will not close open cursors on rollback when the rollback is applied to a savepoint_name from a SAVE TRANSACTION statement.  
  
 When SET CURSOR_CLOSE_ON_COMMIT is OFF, a ROLLBACK statement closes only open asynchronous cursors that are not fully populated. STATIC or INSENSITIVE cursors that were opened after modifications were made will no longer reflect the state of the data if the modifications are rolled back.  
  
 SET CURSOR_CLOSE_ON_COMMIT controls the same behavior as the CURSOR_CLOSE_ON_COMMIT database option. If CURSOR_CLOSE_ON_COMMIT is set to ON or OFF, that setting is used on the connection. If SET CURSOR_CLOSE_ON_COMMIT has not been specified, the value in the **is_cursor_close_on_commit_on** column in the **sys.databases** catalog view applies.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver both set CURSOR_CLOSE_ON_COMMIT to OFF when they connect. DB-Library does not automatically set the CURSOR_CLOSE_ON_COMMIT value.  
  
 When SET ANSI_DEFAULTS is ON, SET CURSOR_CLOSE_ON_COMMIT is enabled.  
  
 The setting of SET CURSOR_CLOSE_ON_COMMIT is set at execute or run time and not at parse time.  
  
 To view the current setting for this setting, run the following query.  
  
```  
DECLARE @CURSOR_CLOSE VARCHAR(3) = 'OFF';  
IF ( (4 & @@OPTIONS) = 4 ) SET @CURSOR_CLOSE = 'ON';  
SELECT @CURSOR_CLOSE AS CURSOR_CLOSE_ON_COMMIT;  
```  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example defines a cursor in a transaction and attempts to use it after the transaction is committed.  
  
```  
-- SET CURSOR_CLOSE_ON_COMMIT  
-------------------------------------------------------------------------------  
SET NOCOUNT ON;  
  
CREATE TABLE t1 (a INT);  
GO   
  
INSERT INTO t1   
VALUES (1), (2);  
GO  
  
PRINT '-- SET CURSOR_CLOSE_ON_COMMIT ON';  
GO  
SET CURSOR_CLOSE_ON_COMMIT ON;  
GO  
PRINT '-- BEGIN TRAN';  
BEGIN TRAN;  
PRINT '-- Declare and open cursor';  
DECLARE testcursor CURSOR FOR  
    SELECT a FROM t1;  
OPEN testcursor;  
PRINT '-- Commit tran';  
COMMIT TRAN;  
PRINT '-- Try to use cursor';  
FETCH NEXT FROM testcursor;  
CLOSE testcursor;  
DEALLOCATE testcursor;  
GO  
PRINT '-- SET CURSOR_CLOSE_ON_COMMIT OFF';  
GO  
SET CURSOR_CLOSE_ON_COMMIT OFF;  
GO  
PRINT '-- BEGIN TRAN';  
BEGIN TRAN;  
PRINT '-- Declare and open cursor';  
DECLARE testcursor CURSOR FOR  
    SELECT a FROM t1;  
OPEN testcursor;  
PRINT '-- Commit tran';  
COMMIT TRAN;  
PRINT '-- Try to use cursor';  
FETCH NEXT FROM testcursor;  
CLOSE testcursor;  
DEALLOCATE testcursor;  
GO  
DROP TABLE t1;  
GO  
```  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [BEGIN TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-transaction-transact-sql.md)   
 [CLOSE &#40;Transact-SQL&#41;](../../t-sql/language-elements/close-transact-sql.md)   
 [COMMIT TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-transaction-transact-sql.md)   
 [ROLLBACK TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-transaction-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET ANSI_DEFAULTS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-defaults-transact-sql.md)  
  
  
