---
title: "sys.database_ledger_digest_locations (Transact-SQL)"
description: sys.database_ledger_digest_locations (Transact-SQL)
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
# sys.database_ledger_digest_locations (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Captures the current and the historical ledger digest storage endpoints for the ledger feature.

For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|
|**path**|**nvarchar(4000)**|The location of storage digests. For example, a path for a container in [Azure Blob storage](/azure/storage/blobs/storage-blobs-introduction).|
|**last_digest_block_id**|**bigint**|The block ID for the last digest uploaded. |
|**is_current**|**bit**|Indicates whether this is the current path or a path used in the past.|

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## See also

- [Digest Management](../security/ledger/ledger-digest-management.md)
- [Configure automatic database digests](../security/ledger/ledger-how-to-enable-automatic-digest-storage.md)
- [Ledger Overview](../security/ledger/ledger-overview.md)