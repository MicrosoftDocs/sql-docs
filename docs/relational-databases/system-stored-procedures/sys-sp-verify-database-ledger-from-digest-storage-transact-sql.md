---
description: "sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)"
title: "sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL) | Microsoft Docs"
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

# sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Verifies the database ledger and the table ledgers using digests at the specified external digest storage locations.

This stored procedure implements the same ledger verification algorithm as [sp_verify_database_ledger](sys-sp-verify-database-ledger-transact-sql.md). A caller is expected to provider a JSON, which contains the paths pointing to digest storage locations, such as [Azure Blob storage](/azure/storage/blobs/storage-blobs-introduction) containers.

For more information on database ledger, see [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview)

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
|**path**|**string**|The location of storage digests. For example, a path for a container in Azure blob storage.|
|**last_digest_block_id**|**int**|The block ID for the last digest uploaded.|
|**is_current**|**boolean**|Indicates whether this is the current path or a path used in the past.|

Example of the input JSON document:

```json
[{"path": “https://mystorage.blob.core.windows.net/sqldbledgerdigests/serverName/DatabaseName/2020-1-1 00:00:00Z”, “last_digest_block_id”:42, "is_current:true"} , … ]
```

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
- [sys.sp_verify_database_ledger (Transact-SQL)](sys-sp-verify-database-ledger-transact-sql.md)
- [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview)
