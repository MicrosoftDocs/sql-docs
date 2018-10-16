---
title: "Transactions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/25/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "Transactions"
  - "Transactions_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "transactions [SQL Server]"
  - "transactions [SQL Server], about transactions"
  - "UOW [SQL Server]"
  - "unit of work [SQL Server]"
ms.assetid: 1485c375-921a-42af-a871-bb333cc08d3e
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Transactions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  A transaction is a single unit of work. If a transaction is successful, all of the data modifications made during the transaction are committed and become a permanent part of the database. If a transaction encounters errors and must be canceled or rolled back, then all of the data modifications are erased.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] operates in the following transaction modes:  
  
 Autocommit transactions  
 Each individual statement is a transaction.  
  
 Explicit transactions  
 Each transaction is explicitly started with the BEGIN TRANSACTION statement and explicitly ended with a COMMIT or ROLLBACK statement.  
  
 Implicit transactions  
 A new transaction is implicitly started when the prior transaction completes, but each transaction is explicitly completed with a COMMIT or ROLLBACK statement.  
  
 Batch-scoped transactions  
 Applicable only to multiple active result sets (MARS), a [!INCLUDE[tsql](../../includes/tsql-md.md)] explicit or implicit transaction that starts under a MARS session becomes a batch-scoped transaction. A batch-scoped transaction that is not committed or rolled back when a batch completes is automatically rolled back by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

> [!NOTE] 
> For special considerations related to Data Warehouse products, see [Transactions (SQL Data Warehouse)](transactions-sql-data-warehouse.md).   

## In This Section  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following transaction statements:  
  
|||  
|-|-|  
|[BEGIN DISTRIBUTED TRANSACTION](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md)|[ROLLBACK TRANSACTION](../../t-sql/language-elements/rollback-transaction-transact-sql.md)|  
|[BEGIN TRANSACTION](../../t-sql/language-elements/begin-transaction-transact-sql.md)|[ROLLBACK WORK](../../t-sql/language-elements/rollback-work-transact-sql.md)|  
|[COMMIT TRANSACTION](../../t-sql/language-elements/commit-transaction-transact-sql.md)|[SAVE TRANSACTION](../../t-sql/language-elements/save-transaction-transact-sql.md)|  
|[COMMIT WORK](../../t-sql/language-elements/commit-work-transact-sql.md)||  
  
## See Also  
 [SET IMPLICIT_TRANSACTIONS &#40;Transact-SQL&#41;](../../t-sql/statements/set-implicit-transactions-transact-sql.md)   
 [@@TRANCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/trancount-transact-sql.md)  
  
  
