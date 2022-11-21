---
description: "sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)"
title: "sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL) | Microsoft Docs"
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
monikerRange: "=azuresqldb-current"
---

# sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Verifies the database ledger and the table ledgers using digests at the specified external digest storage locations.

This stored procedure implements the same ledger verification algorithm as [sp_verify_database_ledger](sys-sp-verify-database-ledger-transact-sql.md). A caller is expected to provider a JSON, which contains the paths pointing to digest storage locations, such as [Azure Blob storage](/azure/storage/blobs/storage-blobs-introduction) containers.

For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview)

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md) 

## Syntax  
  
```syntaxsql
sp_verify_database_ledger_from_digest_storage [@locations = ] 'JSON_document_with_digest_storage_locations' [ , [@table_name = ] 'table_name' ] 
```

## Arguments

[ @locations = ] '*JSON_document_with_digest_storage_locations*'

A JSON document containing a list of ledger digests locations:

|Column name|JSON data type|Description|  
|-----------------|---------------|-----------------|
|**path**|**string**|The location of storage digests. For example, a path for a container in Azure Blob Storage.|
|**last_digest_block_id**|**int**|The block ID for the last digest uploaded.|
|**is_current**|**boolean**|Indicates whether this is the current path or a path used in the past.|

[ @table_name = ] '*table_name*'
 
The name of the ledger table you want to verify. This is an optional argument, if this is not specified the whole database ledger and the ledger tables are verified.

Example of the input JSON document:

```json
[{"path": "https://mystorage.blob.core.windows.net/sqldbledgerdigests/serverName/DatabaseName/2020-1-1 00:00:00Z", "last_digest_block_id":42, "is_current:true"} , … ]
```

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