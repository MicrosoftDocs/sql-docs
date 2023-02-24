---
title: "Transactions (Transact-SQL)"
description: "Transactions (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "09/25/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "Transactions"
  - "Transactions_TSQL"
helpviewer_keywords:
  - "transactions [SQL Server]"
  - "transactions [SQL Server], about transactions"
  - "UOW [SQL Server]"
  - "unit of work [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# Transactions (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

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
> For special considerations related to Data Warehouse products, see [Transactions (Azure Synapse Analytics)](transactions-sql-data-warehouse.md).   

## In This Section  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following transaction statements:  
  
:::row:::
    :::column:::
        [BEGIN DISTRIBUTED TRANSACTION](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md)
    :::column-end:::
    :::column:::
        [ROLLBACK TRANSACTION](../../t-sql/language-elements/rollback-transaction-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [BEGIN TRANSACTION](../../t-sql/language-elements/begin-transaction-transact-sql.md)
    :::column-end:::
    :::column:::
        [ROLLBACK WORK](../../t-sql/language-elements/rollback-work-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [COMMIT TRANSACTION](../../t-sql/language-elements/commit-transaction-transact-sql.md)
    :::column-end:::
    :::column:::
        [SAVE TRANSACTION](../../t-sql/language-elements/save-transaction-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [COMMIT WORK](../../t-sql/language-elements/commit-work-transact-sql.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
  
## See Also  
 [SET IMPLICIT_TRANSACTIONS &#40;Transact-SQL&#41;](../../t-sql/statements/set-implicit-transactions-transact-sql.md)   
 [@@TRANCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/trancount-transact-sql.md)  
  
  
