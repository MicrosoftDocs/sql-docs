---
title: "sys.sp_generate_database_ledger_digest (Transact-SQL)"
description: Generates the ledger digest, which is the hash of the last block in sys.database_ledger_blocks.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-ver16 || >=sql-server-linux-ver16"
---
# sys.sp_generate_database_ledger_digest (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Generates the ledger digest, which is the hash of the last block in `sys.database_ledger_blocks`. If the last block is open (transactions are grouped to the block but no final block hash has been generated), this stored procedure closes the block and generates the hash. Future transactions will then be assigned to the next block.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_generate_database_ledger_digest
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

The results are returned in a column called `latest_digest`, which is a JSON document containing the following data:

| JSON property | Description |
| --- | --- |
| `database_name` | The name of the database. |
| `block_id` | Same as `block_id` from the last row in `sys.database_ledger_blocks`. |
| `hash` | A hexadecimal string representing the SHA-256 hash of the last row in `sys.database_ledger_blocks`. |
| `last_transaction_commit_time` | Same as `commit_time` from the last row in `sys.database_ledger_blocks` in the ISO 8601 format. |
| `digest_time` | The time when the digest was generated in the ISO 8601 format. |

Here's a sample of the JSON document:

```json
{
    "database_name": "contoso",
    "block_id": 0,
    "hash": "0x6D7D609DE43DDBF84A0346463D6F93CA979846CD5609E02E4FFC96338FC64DD5",
    "last_transaction_commit_time": "2020-10-06T16:50:55.1066667",
    "digest_time": "2020-10-07T01:13:23.3601279"
}
```

## Permissions

Users with the **public** role are allowed to execute this stored procedure.

## Related content

- [Digest management](../security/ledger/ledger-digest-management.md)
- [Ledger overview](../security/ledger/ledger-overview.md)
