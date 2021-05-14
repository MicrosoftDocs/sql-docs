---
description: "sys.sp_verify_database_ledger (Transact-SQL)"
title: "sys.sp_verify_database_ledger (Transact-SQL) | Microsoft Docs"
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

# sys.sp_verify_database_ledger (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Verifies the database ledger and the table ledgers. For each row in the sys.database_ledger view, the stored procedure:

1. Recomputes a value stored in the previous_block_hash column of the row.
1. Checks if the recomputed value matches the value currently stored in the previous_block_hash column.
1. If the specified list of digests contains a digest for the ledger block the row represents, it verifies the recomputed value matches the hash in the digest.
1. If a ledger table name is specified using the `table_name` argument, the stored procedure verifies a table hash for the specified table, if it exists in the table_hashes column of sys.database_ledger. Otherwise, it verifies all table hashes existing in the table_hashes column of sys.database_ledger, except table hashes for non-existing (dropped) tables. When verifying a table hash for a ledger table, the stored procedure:
    1. Scans the history table of the ledger table to recompute the table hash, which is a hash of all rows updated by the transaction represented by the current row in sys.database_ledger in the ledger table.
    1. Checks if the recomputed table hash matches the value stored in the table_hashes column of sys.database_ledger for the given ledger table.

In addition, the stored procedure verifies all non-clustered indexes are consistent with the specified ledger table. If no ledger table is specified, it verifies all non-clustered indexes for each existing ledger table referenced in the table_hashes column in any row of sys.database_ledger.

For information database ledger, see [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview)

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md) 

## Syntax  
  
```syntaxsql
sp_verify_database_ledger [@digests = ] 'digests' [ , [@table_name = ] 'table_name' ]
```

## Arguments

[ @digests = ] '*digests*'

A JSON document containing a list of transaction digests, each of which has been obtained by querying the sys.database_ledger_latest_digest view. The JSON document must contain at least one digest.

## Return code values

0 (success) or 1 (failure).

## Result sets

None.

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## See also

- [sys.database_ledger_transaction (Transact-SQL)](../system-catalog-views/sys-database-ledger-transaction-transact-sql.md)
- [sys.database_ledger_blocks (Transact-SQL)](../system-catalog-views/sys-database-ledger-blocks-transact-sql.md)
- [sys.ledger_table_history (Transact-SQL)](../system-catalog-views/sys-ledger-table-history-transact-sql.md)
- [sys.ledger_column_history (Transact-SQL)](../system-catalog-views/sys-ledger-column-history-transact-sql.md)
- [sys.database_ledger_digest_locations (Transact-SQL)](../system-catalog-views/sys-database-ledger-digest-locations-transact-sql.md)
- [sys.sp_generate_database_ledger_digest (Transact-SQL)](sys-sp-generate-database-ledger-digest-transact-sql.md)
- [sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)](sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)
- [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview)