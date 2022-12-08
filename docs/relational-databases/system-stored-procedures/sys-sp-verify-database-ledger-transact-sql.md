---
description: "sys.sp_verify_database_ledger (Transact-SQL)"
title: "sys.sp_verify_database_ledger (Transact-SQL) | Microsoft Docs"
ms.custom:
- event-tier1-build-2022
ms.date: "05/24/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
author: VanMSFT
ms.author: vanto
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
---

# sys.sp_verify_database_ledger (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Verifies the database ledger and the table ledgers. For each row in the sys.database_ledger view, the stored procedure:

1. Recomputes a value stored in the previous_block_hash column of the row.
1. Checks if the recomputed value matches the value currently stored in the previous_block_hash column.
1. If the specified list of digests contains a digest for the ledger block the row represents, it verifies the recomputed value matches the hash in the digest.
1. If a ledger table name is specified using the `table_name` argument, the stored procedure verifies a table hash for the specified table, if it exists in the table_hashes column of sys.database_ledger. Otherwise, it verifies all table hashes existing in the table_hashes column of sys.database_ledger, except table hashes for non-existing (dropped) tables. When verifying a table hash for a ledger table, the stored procedure:
    1. Scans the history table of the ledger table to recompute the table hash, which is a hash of all rows updated by the transaction represented by the current row in sys.database_ledger in the ledger table.
    1. Checks if the recomputed table hash matches the value stored in the table_hashes column of sys.database_ledger for the given ledger table.

In addition, the stored procedure verifies all non-clustered indexes are consistent with the specified ledger table. If no ledger table is specified, it verifies all non-clustered indexes for each existing ledger table referenced in the table_hashes column in any row of sys.database_ledger.

For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview)

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md) 

## Syntax  
  
```syntaxsql
sp_verify_database_ledger [@digests = ] 'digests' [ , [@table_name = ] 'table_name' ]
```

## Arguments

[ @digests = ] '*digests*'

A JSON document containing a list of transaction digests, each of which has been obtained by querying the sys.database_ledger_latest_digest view. The JSON document must contain at least one digest.

[ @table_name = ] '*table_name*'

The name of the table that you want to verify.

## Return code values

0 (success) or 1 (failure).

## Result sets

1 row with 1 column called: last_verified_block_id

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## See also

- [Database Verification](../security/ledger/ledger-database-verification.md)
- [Verify a ledger table to detect tampering](../security/ledger/ledger-verify-database.md)
- [Ledger Overview](../security/ledger/ledger-overview.md)