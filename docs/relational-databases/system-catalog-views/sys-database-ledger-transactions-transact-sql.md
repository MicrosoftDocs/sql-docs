---
title: "sys.database_ledger_transactions (Transact-SQL)"
description: sys.database_ledger_transactions (Transact-SQL)
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
# sys.database_ledger_transactions (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Captures the cryptographically protected history of database transactions against ledger tables in the database. A row in this view represents a database transaction.

For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).

| Column name | Data type | Description |
| --- | --- | --- |
| **transaction_id** | **bigint** | A transaction ID that is unique for the database (it corresponds to a transaction ID in the database transaction log). |
| **block_id** | **bigint** | A sequence number identifying a row. |
| **transactional_ordinal** | **int** | Offset of the transaction in the block. |
| **commit_time** | **datetime2(7)** | The time of the committing transaction. |
| **principal_name** | **sysname** | The name of the user who started the transaction. Captured by calling `ORIGINAL_LOGIN()`. |
| **table_hashes** | **varbinary(max)** | This is a set of key-values pairs, stored in a binary format. The keys are object IDs (from **sys.objects**) of ledger database tables, modified by the transaction. Each value is a SHA-256 hash of all row versions a transaction created or invalidated.<br /><br /> The binary format of data stored in this row is: `<version><length>[<key><value>]`, where<br /><br /> - `version` - indicates the encoding version. Length: 1 byte.<br /> - `length` - the number of entries in the key-value pair list. Length: 1 byte.<br /> - `key` - an object ID. Length: 4 bytes.<br /> - `value` - the hash of rows the transaction cached in the table with the object ID stored as the key. Length: 32 bytes. |

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## See also

- [What is the database ledger?](../security/ledger/ledger-database-ledger.md)
- [Ledger overview](../security/ledger/ledger-overview.md)