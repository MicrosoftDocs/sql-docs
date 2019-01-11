---
title: "BEGIN TRANSACTION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, database-engine, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "BEGIN_TRANSACTION_TSQL"
  - "TRANSACTION_TSQL"
  - "TRANSACTION"
  - "BEGIN TRANSACTION"
  - "BEGIN TRAN"
  - "BEGIN_TRAN_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "transaction logs [SQL Server], BEGIN TRANSACTION statement"
  - "marked transactions [SQL Server], BEGIN TRANSACTION statement"
  - "BEGIN TRANSACTION statement"
  - "local transactions [SQL Server]"
  - "marking transactions [SQL Server]"
  - "transactions [SQL Server], starting"
  - "transaction names [SQL Server]"
  - "starting point marked for transactions"
  - "starting transactions"
ms.assetid: c6258df4-11f1-416a-816b-54f98c11145e
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# BEGIN TRANSACTION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

  Marks the starting point of an explicit, local transaction. Explicit transactions start with the BEGIN TRANSACTION statement and end with the COMMIT or ROLLBACK statement.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
--Applies to SQL Server and Azure SQL Database
 
BEGIN { TRAN | TRANSACTION }   
    [ { transaction_name | @tran_name_variable }  
      [ WITH MARK [ 'description' ] ]  
    ]  
[ ; ]  
```  
 
```  
--Applies to Azure SQL Data Warehouse and Parallel Data Warehouse
 
BEGIN { TRAN | TRANSACTION }   
[ ; ]  
```  

  
## Arguments  
 *transaction_name*  
 **APPLIES TO:** SQL Server (starting with 2008), Azure SQL Database
 
 Is the name assigned to the transaction. *transaction_name* must conform to the rules for identifiers, but identifiers longer than 32 characters are not allowed. Use transaction names only on the outermost pair of nested BEGIN...COMMIT or BEGIN...ROLLBACK statements. *transaction_name* is always case sensitive, even when the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not case sensitive.  
  
 @*tran_name_variable*  
 **APPLIES TO:** SQL Server (starting with 2008), Azure SQL Database
 
 Is the name of a user-defined variable containing a valid transaction name. The variable must be declared with a **char**, **varchar**, **nchar**, or **nvarchar** data type. If more than 32 characters are passed to the variable, only the first 32 characters will be used; the remaining characters will be truncated.  
  
 WITH MARK [ '*description*' ]  
**APPLIES TO:** SQL Server (starting with 2008), Azure SQL Database

Specifies that the transaction is marked in the log. *description* is a string that describes the mark. A *description* longer than 128 characters is truncated to 128 characters before being stored in the msdb.dbo.logmarkhistory table.  
  
 If WITH MARK is used, a transaction name must be specified. WITH MARK allows for restoring a transaction log to a named mark.  
  
## General Remarks
BEGIN TRANSACTION increments @@TRANCOUNT by 1.
  
BEGIN TRANSACTION represents a point at which the data referenced by a connection is logically and physically consistent. If errors are encountered, all data modifications made after the BEGIN TRANSACTION can be rolled back to return the data to this known state of consistency. Each transaction lasts until either it completes without errors and COMMIT TRANSACTION is issued to make the modifications a permanent part of the database, or errors are encountered and all modifications are erased with a ROLLBACK TRANSACTION statement.  
  
BEGIN TRANSACTION starts a local transaction for the connection issuing the statement. Depending on the current transaction isolation level settings, many resources acquired to support the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements issued by the connection are locked by the transaction until it is completed with either a COMMIT TRANSACTION or ROLLBACK TRANSACTION statement. Transactions left outstanding for long periods of time can prevent other users from accessing these locked resources, and also can prevent log truncation.  
  
 Although BEGIN TRANSACTION starts a local transaction, it is not recorded in the transaction log until the application subsequently performs an action that must be recorded in the log, such as executing an INSERT, UPDATE, or DELETE statement. An application can perform actions such as acquiring locks to protect the transaction isolation level of SELECT statements, but nothing is recorded in the log until the application performs a modification action.  
  
 Naming multiple transactions in a series of nested transactions with a transaction name has little effect on the transaction. Only the first (outermost) transaction name is registered with the system. A rollback to any other name (other than a valid savepoint name) generates an error. None of the statements executed before the rollback is, in fact, rolled back at the time this error occurs. The statements are rolled back only when the outer transaction is rolled back.  
  
 The local transaction started by the BEGIN TRANSACTION statement is escalated to a distributed transaction if the following actions are performed before the statement is committed or rolled back:  
  
-   An INSERT, DELETE, or UPDATE statement that references a remote table on a linked server is executed. The INSERT, UPDATE, or DELETE statement fails if the OLE DB provider used to access the linked server does not support the ITransactionJoin interface.  
  
-   A call is made to a remote stored procedure when the REMOTE_PROC_TRANSACTIONS option is set to ON.  
  
 The local copy of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] becomes the transaction controller and uses [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) to manage the distributed transaction.  
  
 A transaction can be explicitly executed as a distributed transaction by using BEGIN DISTRIBUTED TRANSACTION. For more information, see [BEGIN DISTRIBUTED TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md).  
  
 When SET IMPLICIT_TRANSACTIONS is set to ON, a BEGIN TRANSACTION statement creates two nested transactions. For more information see, [SET IMPLICIT_TRANSACTIONS &#40;Transact-SQL&#41;](../../t-sql/statements/set-implicit-transactions-transact-sql.md)  
  
## Marked Transactions  
 The WITH MARK option causes the transaction name to be placed in the transaction log. When restoring a database to an earlier state, the marked transaction can be used in place of a date and time. For more information, see [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md) and [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
 Additionally, transaction log marks are necessary if you need to recover a set of related databases to a logically consistent state. Marks can be placed in the transaction logs of the related databases by a distributed transaction. Recovering the set of related databases to these marks results in a set of databases that are transactionally consistent. Placement of marks in related databases requires special procedures.  
  
 The mark is placed in the transaction log only if the database is updated by the marked transaction. Transactions that do not modify data are not marked.  
  
 BEGIN TRAN *new_name* WITH MARK can be nested within an already existing transaction that is not marked. Upon doing so, *new_name* becomes the mark name for the transaction, despite the name that the transaction may already have been given. In the following example, `M2` is the name of the mark.  
  
```  
BEGIN TRAN T1;  
UPDATE table1 ...;  
BEGIN TRAN M2 WITH MARK;  
UPDATE table2 ...;  
SELECT * from table1;  
COMMIT TRAN M2;  
UPDATE table3 ...;  
COMMIT TRAN T1;  
```  
  
 When nesting transactions, trying to mark a transaction that is already marked results in a warning (not error) message:  
  
 "BEGIN TRAN T1 WITH MARK ...;"  
  
 "UPDATE table1 ...;"  
  
 "BEGIN TRAN M2 WITH MARK ...;"  
  
 "Server: Msg 3920, Level 16, State 1, Line 3"  
  
 "WITH MARK option only applies to the first BEGIN TRAN WITH MARK."  
  
 "The option is ignored."  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
  
### A. Using an explicit transaction
**APPLIES TO:** SQL Server (starting with 2008), Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse

This example uses AdventureWorks. 

```
BEGIN TRANSACTION;  
DELETE FROM HumanResources.JobCandidate  
    WHERE JobCandidateID = 13;  
COMMIT;  
```

### B. Rolling back a transaction
**APPLIES TO:** SQL Server (starting with 2008), Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse

The following example shows the effect of rolling back a transaction. In this example, the ROLLBACK statement will roll back the INSERT statement, but the created table will still exist.

```
 
CREATE TABLE ValueTable (id int);  
BEGIN TRANSACTION;  
       INSERT INTO ValueTable VALUES(1);  
       INSERT INTO ValueTable VALUES(2);  
ROLLBACK;  

```

### C. Naming a transaction 
**APPLIES TO:** SQL Server (starting with 2008), Azure SQL Database

The following example shows how to name a transaction.  
  
```  
DECLARE @TranName VARCHAR(20);  
SELECT @TranName = 'MyTransaction';  
  
BEGIN TRANSACTION @TranName;  
USE AdventureWorks2012;  
DELETE FROM AdventureWorks2012.HumanResources.JobCandidate  
    WHERE JobCandidateID = 13;  
  
COMMIT TRANSACTION @TranName;  
GO  
```  
  
### D. Marking a transaction  
**APPLIES TO:** SQL Server (starting with 2008), Azure SQL Database

The following example shows how to mark a transaction. The transaction `CandidateDelete` is marked.  
  
```  
BEGIN TRANSACTION CandidateDelete  
    WITH MARK N'Deleting a Job Candidate';  
GO  
USE AdventureWorks2012;  
GO  
DELETE FROM AdventureWorks2012.HumanResources.JobCandidate  
    WHERE JobCandidateID = 13;  
GO  
COMMIT TRANSACTION CandidateDelete;  
GO  
```  
  
## See Also  
 [BEGIN DISTRIBUTED TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md)   
 [COMMIT TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-transaction-transact-sql.md)   
 [COMMIT WORK &#40;Transact-SQL&#41;](../../t-sql/language-elements/commit-work-transact-sql.md)   
 [ROLLBACK TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-transaction-transact-sql.md)   
 [ROLLBACK WORK &#40;Transact-SQL&#41;](../../t-sql/language-elements/rollback-work-transact-sql.md)   
 [SAVE TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/save-transaction-transact-sql.md)  
  
  
