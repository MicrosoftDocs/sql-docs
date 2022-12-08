---
title: "BEGIN DISTRIBUTED TRANSACTION (Transact-SQL)"
description: "BEGIN DISTRIBUTED TRANSACTION (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/21/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DISTRIBUTED"
  - "BEGIN_DISTRIBUTED_TRANSACTION_TSQL"
  - "DISTRIBUTED_TSQL"
  - "BEGIN DISTRIBUTED TRANSACTION"
helpviewer_keywords:
  - "BEGIN DISTRIBUTED TRANSACTION statement"
  - "distributed transactions [SQL Server], starting"
  - "MS DTC, transaction management"
  - "distributed transactions [SQL Server], BEGIN DISTRIBUTED TRANSACTION statement"
  - "servers [SQL Server], distributed transactions"
  - "starting distributed transactions"
  - "remote servers [SQL Server], distributed transactions"
  - "starting transactions"
dev_langs:
  - "TSQL"
---
# BEGIN DISTRIBUTED TRANSACTION (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]


  Specifies the start of a [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction. When using SQL Server the distributed transaction is managed by [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC).

 - In case of Azure SQL Managed Instance, the distributed transaction is managed by the service itself and not MS DTC. For information on distributed transactions in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], see [Distributed transactions across cloud databases](/azure/azure-sql/database/elastic-transactions-overview).

 :::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
BEGIN DISTRIBUTED { TRAN | TRANSACTION }
     [ transaction_name | @tran_name_variable ]
[ ; ]
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *transaction_name*

 Is a user-defined transaction name used to track the distributed transaction within MS DTC utilities. *transaction_name* must conform to the rules for identifiers and must be \<= 32 characters.

#### @*tran_name_variable*

 Is the name of a user-defined variable containing a transaction name used to track the distributed transaction within MS DTC utilities. The variable must be declared with a **char**, **varchar**, **nchar**, or **nvarchar** data type.

## Remarks

 The instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] executing the BEGIN DISTRIBUTED TRANSACTION statement is the transaction originator and controls the completion of the transaction. When a subsequent COMMIT TRANSACTION or ROLLBACK TRANSACTION statement is issued for the session, the controlling instance requests that MS DTC manage the completion of the distributed transaction across all of the instances involved.

 Transaction-level snapshot isolation does not support distributed transactions.

 The primary way remote instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are enlisted in a distributed transaction is when a session already enlisted in the distributed transaction executes a distributed query referencing a linked server.

 For example, if BEGIN DISTRIBUTED TRANSACTION is issued on ServerA, the session calls a stored procedure on ServerB and another stored procedure on ServerC. The stored procedure on ServerC executes a distributed query against ServerD, and then all four computers are involved in the distributed transaction. The instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on ServerA is the originating controlling instance for the transaction.

 The sessions involved in [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transactions do not get a transaction object they can pass to another session for it to explicitly enlist in the distributed transaction. The only way for a remote server to enlist in the transaction is to be the target of a distributed query or a remote stored procedure call.

 When a distributed query is executed in a local transaction, the transaction is automatically promoted to a distributed transaction if the target OLE DB data source supports ITransactionLocal. If the target OLE DB data source does not support ITransactionLocal, only read-only operations are allowed in the distributed query.

 A session already enlisted in the distributed transaction performs a remote stored procedure call referencing a remote server.

 The `sp_configure remote proc trans` option controls whether calls to remote stored procedures in a local transaction automatically cause the local transaction to be promoted to a distributed transaction managed by MS DTC. The connection-level SET option REMOTE_PROC_TRANSACTIONS can be used to override the instance default established by `sp_configure remote proc trans`. With this option set on, a remote stored procedure call causes a local transaction to be promoted to a distributed transaction. The connection that creates the MS DTC transaction becomes the originator for the transaction. COMMIT TRANSACTION initiates an MS DTC coordinated commit. If the `sp_configure remote proc trans` option is ON, remote stored procedure calls in local transactions are automatically protected as part of distributed transactions without having to rewrite applications to specifically issue BEGIN DISTRIBUTED TRANSACTION instead of BEGIN TRANSACTION.

 For more information about the distributed transaction environment and process, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator documentation.

## Permissions

 Requires membership in the public role.

## Examples

 This example deletes a candidate from the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database on both the local instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and an instance on a remote server. Both the local and remote databases will either commit or roll back the transaction.

> [!NOTE]  
>  Unless MS DTC is currently installed on the computer running the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], this example produces an error message. For more information about installing MS DTC, see the Microsoft Distributed Transaction Coordinator documentation.

```sql
USE AdventureWorks2012;
GO
BEGIN DISTRIBUTED TRANSACTION;
-- Delete candidate from local instance.
DELETE AdventureWorks2012.HumanResources.JobCandidate
    WHERE JobCandidateID = 13;
-- Delete candidate from remote instance.
DELETE RemoteServer.AdventureWorks2012.HumanResources.JobCandidate
    WHERE JobCandidateID = 13;
COMMIT TRANSACTION;
GO
```

## Next steps

- [BEGIN TRANSACTION (Transact-SQL)](../../t-sql/language-elements/begin-transaction-transact-sql.md)
- [COMMIT TRANSACTION (Transact-SQL)](../../t-sql/language-elements/commit-transaction-transact-sql.md)
- [COMMIT WORK (Transact-SQL)](../../t-sql/language-elements/commit-work-transact-sql.md)
- [ROLLBACK TRANSACTION (Transact-SQL)](../../t-sql/language-elements/rollback-transaction-transact-sql.md)
- [ROLLBACK WORK (Transact-SQL)](../../t-sql/language-elements/rollback-work-transact-sql.md)
- [SAVE TRANSACTION (Transact-SQL)](../../t-sql/language-elements/save-transaction-transact-sql.md)
