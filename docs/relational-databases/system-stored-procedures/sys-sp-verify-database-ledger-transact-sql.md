---
title: "sys.sp_verify_database_ledger (Transact-SQL)"
description: "Verifies the database ledger and the table ledgers."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-ver16 || >=sql-server-linux-ver16"
---
# sys.sp_verify_database_ledger (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Verifies the database ledger and the table ledgers. For each row in the `sys.database_ledger` view, the stored procedure:

1. Recomputes a value stored in the previous_block_hash column of the row.
1. Checks if the recomputed value matches the value currently stored in the previous_block_hash column.
1. If the specified list of digests contains a digest for the ledger block the row represents, it verifies the recomputed value matches the hash in the digest.
1. If a ledger table name is specified using the `table_name` argument, the stored procedure verifies a table hash for the specified table, if it exists in the table_hashes column of `sys.database_ledger`. Otherwise, it verifies all table hashes existing in the table_hashes column of `sys.database_ledger`, except table hashes for non-existing (dropped) tables. When verifying a table hash for a ledger table, the stored procedure:
   1. Scans the history table of the ledger table to recompute the table hash, which is a hash of all rows updated by the transaction represented by the current row in `sys.database_ledger` in the ledger table.
   1. Checks if the recomputed table hash matches the value stored in the table_hashes column of `sys.database_ledger` for the given ledger table.

In addition, the stored procedure verifies all nonclustered indexes are consistent with the specified ledger table. If no ledger table is specified, it verifies all nonclustered indexes for each existing ledger table referenced in the table_hashes column in any row of `sys.database_ledger`.

For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_verify_database_ledger
    [ @digests = ] 'digests'
    [ , [ @table_name = ] 'table_name' ]
```

## Arguments

#### [ @digests = ] '*digests*'

A JSON document containing a list of transaction digests, each of which has been obtained by querying the `sys.database_ledger_latest_digest` view. The JSON document must contain at least one digest.

#### [ @table_name = ] '*table_name*'

The name of the table that you want to verify.

## Return code values

`0` (success) or `1` (failure).

## Result set

One row, with one column called `last_verified_block_id`.

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## Related content

- [Database Verification](../security/ledger/ledger-database-verification.md)
- [Verify a ledger table to detect tampering](../security/ledger/ledger-verify-database.md)
- [Ledger Overview](../security/ledger/ledger-overview.md)
