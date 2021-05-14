---
description: "sys.ledger_table_history (Transact-SQL)"
title: "sys.ledger_table_history (Transact-SQL) | Microsoft Docs"
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
# sys.ledger_table_history (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Captures the cryptographically protected history of operations on ledger tables: creating ledger tables, renaming ledger tables or ledger views, and dropping ledger tables.

For more information on database ledger, see [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview)

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|
|**object_id**|**int**|The object ID of the ledger table.|
|**schema_name**|**sysname**|The name of the schema containing the ledger table. If the operation has changed the schema name, this column captures the new schema name.|
|**table_name**|**sysname**|The name of the ledger table. If the operation has changed the table name, this column captures the new table name.|
|**ledger_view_schema_name**|**sysname**|The name of the schema containing the ledger view for the ledger table. If the operation has changed the schema name, this column captures the new schema name.|
|**ledger_view_name**|**sysname**|The name of the ledger view for the ledger table. If the operation has changed the view name, this column captures the new view name.|
|**operation_type**|**tinyint**|The numeric value indicating the type of the operation<br/><br/>0 = CREATE – creating a ledger table.<br/>1 = DROP – dropping a ledger table.|
|**operation_type_desc**|**nvarchar(60)**|Textual description of the value of operation_type.|
|**transaction_id**|**bigint**|The transaction of the ID that included the operation on the ledger table. It identifies a row in [sys.database_ledger_transaction](sys-database-ledger-transaction-transact-sql.md).|
|**sequence_number**|**bigint**|The sequence number of the operation within the transaction.|

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## Examples

Consider the following sequence of operations on ledger tables.

1. A user creates a ledger table.

    ```sql
    CREATE TABLE [HR].[Employees]
    (
        EmployeeID INT NOT NULL,
        Salary Money NOT NULL
    )
    WITH (SYSTEM_VERSIONING = ON, LEDGER = ON);
    GO
    ```

1. A user renames the ledger table.

    ```sql
    EXEC sp_rename 'HR.Employees', 'dbo.Employees123';  
    ```

1. A user renames the ledger view of the ledger table.

    ```sql
    ALTER TABLE [dbo].[Employees123]
    WITH (SYSTEM_VERSIONING = ON, LEDGER = ON(LEDGER_VIEW=[dbo].[Employees123Ledger1]));
    GO
    ```

1. A user drops the ledger table.

    ```sql
    DROP TABLE [HR].[Employees];
    ```

The below query joins sys.ledger_table_history and sys.database_ledger_transactions to produce the history of changes on ledger tables, including the time of each and change and the name of the user who triggered it.

```sql
SELECT 
t.[user_name]
, t.[commit_time]
, h.[schema_name] + '.' + h.[table_name] AS [table_name]
, h.[ledger_view_schema_name] + '.' + h.[ledger_view_name] AS [view_name]
, h.[operation_type_desc]
FROM sys.ledger_table_history h
JOIN sys.database_ledger_transactions t
ON h.transaction_id = t.transaction_id
```

## See also

- [sys.database_ledger_transaction (Transact-SQL)](sys-database-ledger-transaction-transact-sql.md)
- [sys.database_ledger_blocks (Transact-SQL)](sys-database-ledger-blocks-transact-sql.md)
- [sys.ledger_column_history (Transact-SQL)](sys-ledger-column-history-transact-sql.md)
- [sys.database_ledger_digest_locations (Transact-SQL)](sys-database-ledger-digest-locations-transact-sql.md)
- [sys.sp_generate_database_ledger_digest (Transact-SQL)](../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md)
- [sys.sp_verify_database_ledger (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md)
- [sys.sp_verify_database_ledger_from_digest_storage (Transact-SQL)](../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)
- [Azure SQL Database ledger](/azure/azure-sql/database/ledger-overview)

