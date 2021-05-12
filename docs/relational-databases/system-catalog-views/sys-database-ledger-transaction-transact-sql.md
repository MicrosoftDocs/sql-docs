---
description: "sys.database_ledger_transaction (Transact-SQL)"
title: "sys.database_ledger_transaction (Transact-SQL) | Microsoft Docs"
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
---
# sys.database_ledger_transaction (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Captures the cryptographically protected history of database transactions that update ledger tables in the database. A row in this view represents a database transaction.

| Column name | Data type | Description |
| --- | --- | --- |
| **transaction_id** | **bigint** | A transaction ID that is unique for the database (it corresponds to a transaction ID in the database transaction log). |
| **block_id** | **bigint** | A sequence number identifying a row. |
| **transactional_ordinal** | **int** | Offset of the transaction in the block. |
| **user_name()** | **sysname** | The name of the user who started the transaction. Captured by calling `ORIGINAL_LOGIN()`. |
| **commit_time** | **datetime2(7)** | The time of the committing transaction. |
| **table_hashes** | **varbinary(max)** | This is a set of key-values pairs, stored in a binary format. The keys are object IDs (from **sys.objects**) of ledger database tables, modified by the transaction. Each value is a SHA-256 hash of all row versions a transaction created or invalidated.<br /><br /> The binary format of data stored in this row is: `<version><length>[<key><value>]`, where<br /><br /> - `version` - indicates the encoding version. Length: 1 byte.<br /> - `length` - the number of entries in the key-value pair list. Length: 1 byte.<br /> - `key` - an object ID. Length: 4 bytes.<br /> - `value` - the hash of rows the transaction cached in the table with the object ID stored as the key. Length: 32 bytes. |

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## See also

- [sys.database_ledger_blocks (Transact-SQL)](sys-database-ledger-blocks-transact-sql.md)
- [sys.ledger_table_history (Transact-SQL)](sys-ledger-table-history-transact-sql.md)
- [sys.ledger_column_history (Transact-SQL)](sys-ledger-column-history-transact-sql.md)
- [sys.database_ledger_digest_locations (Transact-SQL)](sys-database-ledger-digest-locations-transact-sql.md)
- [sys.sp_generate_database_ledger_digest (Transact-SQL)](../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md)
- [sys.sp_verify_database_ledger (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md)
- [sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)