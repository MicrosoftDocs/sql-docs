---
description: "sys.sp_generate_database_ledger_digest (Transact-SQL)"
title: "sys.sp_generate_database_ledger_digest (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/22/2021"
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

# sys.sp_generate_database_ledger_digest (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Generates the ledger digest, which is the hash of the last block in sys.database_ledger_blocks. If the last block is open (transactions have been grouped to the block but no final block hash has been generated), this stored procedure will close the block and generate the hash. Future transactions will then be assigned to the next block.

For more information on database ledger, see [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview)

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md) 

## Syntax  
  
```syntaxsql
EXEC sys.sp_generate_database_ledger_digest
```
## Arguments
None

## Return code values

0 (success) or 1 (failure).

## Result sets

|Column name|Description|  
|-----------------|---------------|
|**latest_digest**|A JSON document containing the following data:<br/><br/>`database_name` - the name of the database. <br/>`block_id` – same as block_id from the last row in sys.database_ledger_blocks.<br/>`hash` – a hexadecimal string representing a SHA-256 hash of the last row in sys.database_ledger_blocks.<br/>`last_transaction_commit_time` - same as commit_time from the last row in sys.database_ledger_blocks in the ISO 8601 format.<br/>`digest_time` – the time when the digest was generated in the ISO 8601 format.

**Example JSON**

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

## See also

- [Digest Management](/docs/relational-databases/security/ledger/ledger-digest-management.md)
- [Ledger Overview](/doc/relational-databases/security/ledger/ledger-overview.md)
