---
title: "sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)"
description: "Verifies the database ledger and the table ledgers using digests at the specified external digest storage locations."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Verifies the database ledger and the table ledgers using digests at the specified external digest storage locations.

This stored procedure implements the same ledger verification algorithm as [sys.sp_verify_database_ledger](sys-sp-verify-database-ledger-transact-sql.md). A caller is expected to provide a JSON document that contains the paths pointing to digest storage locations, such as [Azure Blob storage](/azure/storage/blobs/storage-blobs-introduction) containers.

For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_verify_database_ledger_from_digest_storage
    [ @locations = ] 'JSON_document_with_digest_storage_locations'
    [ , [ @table_name = ] 'table_name' ]
```

## Arguments

#### [ @locations = ] '*JSON_document_with_digest_storage_locations*'

A JSON document containing a list of ledger digests locations:

| Column name | JSON data type | Description |
| --- | --- | --- |
| `path` | **string** | The location of storage digests. For example, a path for a container in Azure Blob Storage. |
| `last_digest_block_id` | **int** | The block ID for the last digest uploaded. |
| `is_current` | **boolean** | Indicates whether this is the current path or a path used in the past. |

#### [ @table_name = ] '*table_name*'

The name of the ledger table you want to verify. This argument is optional. If this isn't specified, the whole database ledger and the ledger tables are verified.

Example of the input JSON document:

```json
[
    {
        "path": "https://mystorage.blob.core.windows.net/sqldbledgerdigests/serverName/DatabaseName/2020-1-1 00:00:00Z",
        "last_digest_block_id": 42,
        "is_current:true"
    },
    ...
]
```

## Return code values

`0` (success) or `1` (failure).

## Result set

One row, with one column called `last_verified_block_id`.

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## Related content

- [Database verification](../security/ledger/ledger-database-verification.md)
- [Verify a ledger table to detect tampering](../security/ledger/ledger-verify-database.md)
- [Ledger overview](../security/ledger/ledger-overview.md)
