---
description: "sys.database_ledger_blocks (Transact-SQL)"
title: "sys.database_ledger_blocks (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/25/2021"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
author: VanMSFT
ms.author: vanto
monikerRange: "=azuresqldb-current"
---
# sys.database_ledger_blocks (Transact-SQL)
[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Captures the cryptographically chained blocks, each of which represents a block of transactions against ledger tables.

For more information on database ledger, see [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview)

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|
|**block_id**|**bigint**|A sequence number identifying the row in this view.|
|**transaction_root_hash**|**binary(32)**|The hash of the root of the Merkle tree, formed by transactions stored in the block.|
|**block_size**|**bigint**|The number of transactions in the block.|
|**previous_block_hash**|**binary(32)**|A SHA-256 hash of the previous row in the view.|

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## See also

- [sys.database_ledger_transaction (Transact-SQL)](sys-database-ledger-transaction-transact-sql.md)
- [sys.ledger_table_history (Transact-SQL)](sys-ledger-table-history-transact-sql.md)
- [sys.ledger_column_history (Transact-SQL)](sys-ledger-column-history-transact-sql.md)
- [sys.database_ledger_digest_locations (Transact-SQL)](sys-database-ledger-digest-locations-transact-sql.md)
- [sys.sp_generate_database_ledger_digest (Transact-SQL)](../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md)
- [sys.sp_verify_database_ledger (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md)
- [sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)
- [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview)
