---
title: "sys.database_ledger_blocks (Transact-SQL)"
description: sys.database_ledger_blocks (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-ver16||>=sql-server-linux-ver16"
---
# sys.database_ledger_blocks (Transact-SQL)
[!INCLUDE [SQL Server 2022 Azure SQL Database](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Captures the cryptographically chained blocks, each of which represents a block of transactions against ledger tables.

For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview)

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|
|**block_id**|**bigint**|A sequence number identifying the row in this view.|
|**transactions_root_hash**|**binary(32)**|The hash of the root of the Merkle tree, formed by transactions stored in the block.|
|**block_size**|**bigint**|The number of transactions in the block.|
|**previous_block_hash**|**binary(32)**|A SHA-256 hash of the previous row in the view.|

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## See also

- [What is the database ledger?](../security/ledger/ledger-database-ledger.md)
- [Ledger Overview](../security/ledger/ledger-overview.md)