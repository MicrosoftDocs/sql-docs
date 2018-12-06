---
title: "SET XACT_ABORT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/07/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "XACT_ABORT_TSQL"
  - "XACT_ABORT"
  - "SET XACT_ABORT"
  - "SET_XACT_ABORT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "transaction rollbacks [SQL Server]"
  - "XACT_ABORT option"
  - "automatic transaction roll backs"
  - "transactions [SQL Server], rolling back"
  - "rolling back transactions, SET XACT_ABORT"
  - "roll back transactions [SQL Server]"
  - "SET XACT_ABORT statement"
ms.assetid: cbcaa433-58f2-4dc3-a077-27273bef65b5
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET XACT_ABORT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

    
> [!NOTE]  
>  The **THROW** statement honors **SET XACT_ABORT**. **RAISERROR** does not. New applications should use **THROW** instead of **RAISERROR**.  
  
 Specifies whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically rolls back the current transaction when a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement raises a run-time error.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```    
SET XACT_ABORT { ON | OFF }  
```  

  
## Remarks  
 When SET XACT_ABORT is ON, if a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement raises a run-time error, the entire transaction is terminated and rolled back.  
  
 When SET XACT_ABORT is OFF, in some cases only the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that raised the error is rolled back and the transaction continues processing. Depending upon the severity of the error, the entire transaction may be rolled back even when SET XACT_ABORT is OFF. OFF is the default setting.  
  
 Compile errors, such as syntax errors, are not affected by SET XACT_ABORT.  
  
 XACT_ABORT must be set ON for data modification statements in an implicit or explicit transaction against most OLE DB providers, including [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The only case where this option is not required is if the provider supports nested transactions.  
  
 When ANSI_WARNINGS=OFF, permissions violations cause transactions to abort.  
  
 The setting of SET XACT_ABORT is set at execute or run time and not at parse time.  
  
 To view the current setting for this setting, run the following query.  
  
```  
DECLARE @XACT_ABORT VARCHAR(3) = 'OFF';  
IF ( (16384 & @@OPTIONS) = 16384 ) SET @XACT_ABORT = 'ON';  
SELECT @XACT_ABORT AS XACT_ABORT;  
  
```  
  
## Examples  
 The following code example causes a foreign key violation error in a transaction that has other [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. In the first set of statements, the error is generated, but the other statements execute successfully and the transaction is successfully committed. In the second set of statements, `SET XACT_ABORT` is set to `ON`. This causes the statement error to terminate the batch and the transaction is rolled back.  
  
```  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID(N't2', N'U') IS NOT NULL  
    DROP TABLE t2;  
GO  
IF OBJECT_ID(N't1', N'U') IS NOT NULL  
    DROP TABLE t1;  
GO  
CREATE TABLE t1  
    (a INT NOT NULL PRIMARY KEY);  
CREATE TABLE t2  
    (a INT NOT NULL REFERENCES t1(a));  
GO  
INSERT INTO t1 VALUES (1);  
INSERT INTO t1 VALUES (3);  
INSERT INTO t1 VALUES (4);  
INSERT INTO t1 VALUES (6);  
GO  
SET XACT_ABORT OFF;  
GO  
BEGIN TRANSACTION;  
INSERT INTO t2 VALUES (1);  
INSERT INTO t2 VALUES (2); -- Foreign key error.  
INSERT INTO t2 VALUES (3);  
COMMIT TRANSACTION;  
GO  
SET XACT_ABORT ON;  
GO  
BEGIN TRANSACTION;  
INSERT INTO t2 VALUES (4);  
INSERT INTO t2 VALUES (5); -- Foreign key error.  
INSERT INTO t2 VALUES (6);  
COMMIT TRANSACTION;  
GO  
-- SELECT shows only keys 1 and 3 added.   
-- Key 2 insert failed and was rolled back, but  
-- XACT_ABORT was OFF and rest of transaction  
-- succeeded.  
-- Key 5 insert error with XACT_ABORT ON caused  
-- all of the second transaction to roll back.  
SELECT *  
    FROM t2;  
GO  
```  
  
## See Also  
 [THROW &#40;Transact-SQL&#41;](../../t-sql/language-elements/throw-transact-sql.md)   
 [BEGIN TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-transaction-transact-sql.md)   
 [COMMIT TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-transaction-transact-sql.md)   
 [ROLLBACK TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-transaction-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [@@TRANCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/trancount-transact-sql.md)  
  
  
