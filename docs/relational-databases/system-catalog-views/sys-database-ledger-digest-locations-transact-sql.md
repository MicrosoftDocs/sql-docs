---
description: "sys.database_ledger_digest_locations (Transact-SQL)"
title: "sys.database_ledger_digest_locations (Transact-SQL) | Microsoft Docs"
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
# sys.database_ledger_digest_locations (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Captures the current and the historical ledger digest storage endpoints for the ledger feature.

For more information on database ledger, see [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview).

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|
|**path**|**nvarchar(4000)**|The location of storage digests. For example, a path for a container in [Azure Blob storage](/azure/storage/blobs/storage-blobs-introduction).|
|**last_digest_block_id**|**bigint**|The block ID for the last digest uploaded. |
|**is_current**|**bit**|Indicates whether this is the current path or a path used in the past.|

## Permissions

Requires the **VIEW LEDGER CONTENT** or **ALTER LEDGER CONFIGURATION** permission.

## See also

- [sys.database_ledger_transaction (Transact-SQL)](sys-database-ledger-transaction-transact-sql.md)
- [sys.database_ledger_blocks (Transact-SQL)](sys-database-ledger-blocks-transact-sql.md)
- [sys.ledger_table_history (Transact-SQL)](sys-ledger-table-history-transact-sql.md)
- [sys.ledger_column_history (Transact-SQL)](sys-ledger-column-history-transact-sql.md)
- [sys.sp_generate_database_ledger_digest (Transact-SQL)](../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md)
- [sys.sp_verify_database_ledger (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md)
- [sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)
- [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview).
