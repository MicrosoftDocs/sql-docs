---
description: "sys.ledger_column_history (Transact-SQL)"
title: "sys.ledger_column_history (Transact-SQL) | Microsoft Docs"
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
# sys.ledger_column_history (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Captures the cryptographically protected history of operations on columns of ledger tables: adding, renaming, and dropping columns.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|
|**object_id**|**int**|The object ID of the ledger table.|
|**column_id**|**int**|The column ID of the column in a ledger table. |
|**column_name**|**sysname**|The name of the column in ledger table. If the operation has changed the column name, this column captures the new column name.|
|**operation_type**|**tinyint**|The numeric value indicating the type of the operation<br/><br/>0 = CREATE – creating a column as part of creating the table containing the column using CREATE TABLE.<br/>1 = ADD – adding a column in a ledger table, using ALTER TABLE/ADD COLUMN.<br/>2 = RENAME – renaming a column in a ledger table.<br/>3 = DROP – dropping a column in a ledger table.|
|**operation_type_desc**|**nvarchar(60)**|Textual description of the value of operation_type.|

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## See also

- [sys.database_ledger_transaction (Transact-SQL)](sys-database-ledger-transaction-transact-sql.md)
- [sys.database_ledger_blocks (Transact-SQL)](sys-database-ledger-blocks-transact-sql.md)
- [sys.ledger_table_history (Transact-SQL)](sys-ledger-table-history-transact-sql.md)
- [sys.database_ledger_digest_locations (Transact-SQL)](sys-database-ledger-digest-locations-transact-sql.md)
- [sys.sp_generate_database_ledger_digest (Transact-SQL)](../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md)
- [sys.sp_verify_database_ledger (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md)
- [sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)