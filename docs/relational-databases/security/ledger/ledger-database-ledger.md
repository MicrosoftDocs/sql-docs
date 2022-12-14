---
title: "Database ledger"
description: This article provides information on ledger database tables and associated views.
ms.date: "05/24/2022"
ms.service: sql-database
ms.subservice: security
ms.custom:
- event-tier1-build-2022
ms.reviewer: kendralittle, mathoma
ms.topic: conceptual
author: VanMSFT
ms.author: vanto
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
---

# What is the database ledger?

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

The database ledger is part of the ledger feature. The database ledger incrementally captures the state of a database as the database evolves over time, while updates occur on ledger tables. It logically uses a blockchain and [Merkle tree data structures](/archive/msdn-magazine/2018/march/blockchain-blockchain-fundamentals). 

Any operations that update a ledger table need to perform some additional tasks to maintain the historical data and compute the digests captured in the database ledger. Specifically, for every row updated, we must: 
- Persist the earlier version of the row in the history table.
- Assign the transaction ID and generate a new sequence number, persisting them in the appropriate system columns. 
- Serialize the row content and include it when computing the hash for all rows updated by this transaction.

Ledger achieves that by extending the [Data Manipulation Language](../../../t-sql/queries/queries.md) (DML) query plans of all insert, update and delete operations targeting ledger tables. The transaction ID and newly generated sequence number are set for the new version of the row. Then, the query plan operator executes a special expression that serializes the row content and computes its hash, appending it to a Merkle Tree that is stored at the transaction level and contains the hashes of all row versions updated by this transaction for this ledger table. The root of the tree represents all the updates and deletes performed by this transaction in this ledger table. If the transaction updates multiple tables, a separate Merkle Tree is maintained for each table. The figure below shows an example of a Merkle Tree storing the updated row versions of a ledger table and the format used to serialize the rows. Other than the serialized value of each column, we include metadata regarding the number of columns in the row, the ordinal of individual columns, the data types, lengths and other information that affects how the values are interpreted. 

:::image type="content" source="media/ledger/merkle-tree-rows.png" alt-text="Diagram that shows a Merkle Tree storing the updated row versions of a ledger table and the format used to serialize the rows":::

To capture the state of the database, the database ledger stores an entry for every transaction. It captures metadata about the transaction, such as its commit timestamp and the identity of the user who executed it. It also captures the Merkle tree root of the rows updated in each ledger table (see above). These entries are then appended to a tamper-evident data structure to allow verification of integrity in the future. A block is closed:

- Approximately every 30 seconds, when your database is configured for [automatic database digest storage](ledger-how-to-enable-automatic-digest-storage.md)
- When the user manually generates a database digest by running the [sys.sp_generate_database_ledger_digest](../../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md) stored procedure
- When it contains 100K transactions.

When a block is closed, new transactions will be inserted in a new block. The block generation process then:

1. Retrieves all transactions that belong to the *closed* block from both the in-memory queue and the [sys.database_ledger_transactions](../../system-catalog-views/sys-database-ledger-transactions-transact-sql.md) system catalog view.
1. Computes the Merkle tree root over these transactions and the hash of the previous block.
1. Persists the closed block in the [sys.database_ledger_blocks](../../system-catalog-views/sys-database-ledger-blocks-transact-sql.md) system catalog view. 

Because this is a regular table update, the system automatically guarantees its durability. To maintain the single chain of blocks, this operation is single-threaded. But it's also efficient, because it only computes the hashes over the transaction information and happens asynchronously. It doesn't affect the transaction performance. 

:::image type="content" source="media/ledger/merkle-tree-transactions.png" alt-text="Diagram that shows a Merkle Tree storing the transactions of a ledger table.":::

For more information on how ledger provides data integrity, see the articles, [Digest management](ledger-digest-management.md) and [Database verification](ledger-database-verification.md).

## Where are database transaction and block data stored?

The data for transactions and blocks is physically stored as rows in two system catalog views:

- [sys.database_ledger_transactions](../../system-catalog-views/sys-database-ledger-transactions-transact-sql.md): Maintains a row with the information of each transaction in the database ledger. The information includes the ID of the block where this transaction belongs and the ordinal of the transaction within the block. 
- [sys.database_ledger_blocks](../../system-catalog-views/sys-database-ledger-blocks-transact-sql.md): Maintains a row for every block in the ledger, including the root of the Merkle tree over the transactions within the block and the hash of the previous block to form a blockchain.

To view the database ledger, run the following T-SQL statements in [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

```sql
SELECT * FROM sys.database_ledger_transactions;
GO

SELECT * FROM sys.database_ledger_blocks;
GO
```

The following example of a ledger table consists of four transactions that made up one block in the blockchain of the database ledger:

:::image type="content" source="media/ledger/database-ledger-1.png" alt-text="Screenshot of an example ledger table.":::

### Permissions
Viewing the database ledger requires the `VIEW LEDGER CONTENT` permission. For details on permissions related to ledger tables, see [Permissions](../permissions-database-engine.md).

## See also

- [Ledger overview](ledger-overview.md)
- [Data Manipulation Language (DML)](../../../t-sql/queries/queries.md)
- [Ledger views](../../system-catalog-views/security-catalog-views-transact-sql.md#ledger-views)
