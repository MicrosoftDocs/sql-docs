---
title: "COMMIT TRANSACTION (Transact-SQL)"
description: "COMMIT TRANSACTION (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "09/09/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "COMMIT"
  - "COMMIT TRANSACTION"
  - "COMMIT_TSQL"
  - "COMMIT_TRANSACTION_TSQL"
helpviewer_keywords:
  - "ending transactions [SQL Server]"
  - "user-defined transactions [SQL Server]"
  - "committed transactions"
  - "transactions [SQL Server], ending"
  - "marking end of transactions [SQL Server]"
  - "implicit transactions"
  - "distributed transactions [SQL Server], committed"
  - "transactions [SQL Server], committed"
  - "COMMIT TRANSACTION statement"
  - "rolling back transactions, COMMIT TRANSACTION"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# COMMIT TRANSACTION (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Marks the end of a successful implicit or explicit transaction. If @@TRANCOUNT is 1, COMMIT TRANSACTION makes all data modifications since the start of the transaction a permanent part of the database, frees the transaction's resources, and decrements @@TRANCOUNT to 0. When @@TRANCOUNT is greater than 1, COMMIT TRANSACTION decrements @@TRANCOUNT only by 1 and the transaction stays active.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Applies to SQL Server (starting with 2008) and Azure SQL Database
  
COMMIT [ { TRAN | TRANSACTION }  [ transaction_name | @tran_name_variable ] ] [ WITH ( DELAYED_DURABILITY = { OFF | ON } ) ]  
[ ; ]  
```  
 
```syntaxsql
-- Applies to Azure Synapse Analytics and Parallel Data Warehouse Database
  
COMMIT [ TRAN | TRANSACTION ] 
[ ; ]  
``` 
 
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *transaction_name*  
 **Applies to:** SQL Server and Azure SQL Database
 
 Is ignored by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. *transaction_name* specifies a transaction name assigned by a previous BEGIN TRANSACTION. *transaction_name*must conform to the rules for identifiers, but can't exceed 32 characters. *transaction_name* indicates to programmers which nested BEGIN TRANSACTION the COMMIT TRANSACTION is associated with.  
  
 *\@tran_name_variable*  
 **Applies to:** SQL Server and Azure SQL Database  
 
Is the name of a user-defined variable containing a valid transaction name. The variable must be declared with a char, varchar, nchar, or nvarchar data type. If more than 32 characters are passed to the variable, only 32 characters will be used; the remaining characters are truncated.  
  
 DELAYED_DURABILITY  
 **Applies to:** SQL Server and Azure SQL Database   

 Option that requests this transaction should be committed with delayed durability. The request is ignored if the database has been altered with `DELAYED_DURABILITY = DISABLED` or `DELAYED_DURABILITY = FORCED`. For more information, see [Control Transaction Durability](../../relational-databases/logs/control-transaction-durability.md).  
  
## Remarks  
 It's the responsibility of the [!INCLUDE[tsql](../../includes/tsql-md.md)] programmer to issue COMMIT TRANSACTION only at a point when all data referenced by the transaction is logically correct.  
  
 If the transaction committed was a [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction, COMMIT TRANSACTION triggers MS DTC to use a two-phase commit protocol to commit all of the servers involved in the transaction. When a local transaction spans two or more databases on the same instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], the instance uses an internal two-phase commit to commit all of the databases involved in the transaction.  
  
 When used in nested transactions, commits of the inner transactions don't free resources or make their modifications permanent. The data modifications are made permanent and resources freed only when the outer transaction is committed. Each COMMIT TRANSACTION issued when @@TRANCOUNT is greater than one simply decrements @@TRANCOUNT by 1. When @@TRANCOUNT is finally decremented to 0, the entire outer transaction is committed. Because *transaction_name* is ignored by the [!INCLUDE[ssDE](../../includes/ssde-md.md)], issuing a COMMIT TRANSACTION referencing the name of an outer transaction when there are outstanding inner transactions only decrements @@TRANCOUNT by 1.  
  
 Issuing a COMMIT TRANSACTION when @@TRANCOUNT is zero results in an error; there's no corresponding BEGIN TRANSACTION.  
  
 You can't roll back a transaction after a COMMIT TRANSACTION statement is issued because the data modifications have been made a permanent part of the database.  
  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] increments the transaction count within a statement only when the transaction count is 0 at the start of the statement.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
  
### A. Committing a transaction  
**Applies to:** SQL Server, Azure SQL Database, [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)], and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]   

The following example deletes a job candidate. It uses AdventureWorks. 
  
```sql   
BEGIN TRANSACTION;   
DELETE FROM HumanResources.JobCandidate  
    WHERE JobCandidateID = 13;   
COMMIT TRANSACTION;   
```  
  
### B. Committing a nested transaction  
**Applies to:** SQL Server and Azure SQL Database    

The following example creates a table, generates three levels of nested transactions, and then commits the nested transaction. Although each `COMMIT TRANSACTION` statement has a *transaction_name* parameter, there's no relationship between the `COMMIT TRANSACTION` and `BEGIN TRANSACTION` statements. The *transaction_name* parameters help the programmer ensure that the correct number of commits are coded to decrement `@@TRANCOUNT` to 0 and so to commit the outer transaction. 
  
```sql   
IF OBJECT_ID(N'TestTran',N'U') IS NOT NULL  
    DROP TABLE TestTran;  
GO  
CREATE TABLE TestTran (Cola INT PRIMARY KEY, Colb CHAR(3));  
GO  
-- This statement sets @@TRANCOUNT to 1.  
BEGIN TRANSACTION OuterTran;  
  
PRINT N'Transaction count after BEGIN OuterTran = '  
    + CAST(@@TRANCOUNT AS NVARCHAR(10));  
 
INSERT INTO TestTran VALUES (1, 'aaa');  
 
-- This statement sets @@TRANCOUNT to 2.  
BEGIN TRANSACTION Inner1;  
 
PRINT N'Transaction count after BEGIN Inner1 = '  
    + CAST(@@TRANCOUNT AS NVARCHAR(10));  
  
INSERT INTO TestTran VALUES (2, 'bbb');  
  
-- This statement sets @@TRANCOUNT to 3.  
BEGIN TRANSACTION Inner2;  
  
PRINT N'Transaction count after BEGIN Inner2 = '  
    + CAST(@@TRANCOUNT AS NVARCHAR(10));  
  
INSERT INTO TestTran VALUES (3, 'ccc');  
  
-- This statement decrements @@TRANCOUNT to 2.  
-- Nothing is committed.  
COMMIT TRANSACTION Inner2;  
 
PRINT N'Transaction count after COMMIT Inner2 = '  
    + CAST(@@TRANCOUNT AS NVARCHAR(10));  
 
-- This statement decrements @@TRANCOUNT to 1.  
-- Nothing is committed.  
COMMIT TRANSACTION Inner1;  
 
PRINT N'Transaction count after COMMIT Inner1 = '  
    + CAST(@@TRANCOUNT AS NVARCHAR(10));  
  
-- This statement decrements @@TRANCOUNT to 0 and  
-- commits outer transaction OuterTran.  
COMMIT TRANSACTION OuterTran;  
  
PRINT N'Transaction count after COMMIT OuterTran = '  
    + CAST(@@TRANCOUNT AS NVARCHAR(10));  
```  
  
## See Also  
 [BEGIN DISTRIBUTED TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md)   
 [BEGIN TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-transaction-transact-sql.md)   
 [COMMIT WORK &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-work-transact-sql.md)   
 [ROLLBACK TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-transaction-transact-sql.md)   
 [ROLLBACK WORK &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-work-transact-sql.md)   
 [SAVE TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/save-transaction-transact-sql.md)   
 [@@TRANCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/trancount-transact-sql.md)  
  
