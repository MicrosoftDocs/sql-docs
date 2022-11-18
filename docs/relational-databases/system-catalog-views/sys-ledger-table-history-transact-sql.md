---
title: "sys.ledger_table_history (Transact-SQL)"
description: sys.ledger_table_history (Transact-SQL)
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

# sys.ledger_table_history (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Captures the cryptographically protected history of operations on ledger tables: creating ledger tables, renaming ledger tables or ledger views, and dropping ledger tables.

For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview)

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|
|**object_id**|**int**|The object ID of the ledger table.|
|**schema_name**|**sysname**|The name of the schema containing the ledger table. If the operation has changed the schema name, this column captures the new schema name.|
|**table_name**|**sysname**|The name of the ledger table. If the operation has changed the table name, this column captures the new table name.|
|**ledger_view_schema_name**|**sysname**|The name of the schema containing the ledger view for the ledger table. If the operation has changed the schema name, this column captures the new schema name.|
|**ledger_view_name**|**sysname**|The name of the ledger view for the ledger table. If the operation has changed the view name, this column captures the new view name.|
|**operation_type**|**tinyint**|The numeric value indicating the type of the operation<br/><br/>0 = CREATE – creating a ledger table.<br/>1 = DROP – dropping a ledger table.<br/>2 = RENAME - renaming a ledger table. <br/>3 = RENAME_VIEW - renaming the ledger view for a ledger table.|
|**operation_type_desc**|**nvarchar(60)**|Textual description of the value of operation_type.|
|**transaction_id**|**bigint**|The transaction of the ID that included the operation on the ledger table. It identifies a row in [sys.database_ledger_transactions](sys-database-ledger-transactions-transact-sql.md).|
|**sequence_number**|**bigint**|The sequence number of the operation within the transaction.|

## Permissions

Requires the **VIEW LEDGER CONTENT** permission.

## Examples

Consider the following sequence of operations on ledger tables.

1. A user creates a ledger table.

    ```sql
    CREATE TABLE [Employees]
    (
        EmployeeID INT NOT NULL,
        Salary Money NOT NULL
    )
    WITH (SYSTEM_VERSIONING = ON, LEDGER = ON);
    GO
    ```

1. A user renames the ledger table.

    ```sql
    EXEC sp_rename 'Employees', 'Employees_Copy';
    ```

1. A user renames the ledger view of the ledger table.

    ```sql
    EXEC sp_rename 'Employees_Ledger', 'Employees_Ledger_Copy';
    ```

1. A user drops the ledger table.

    ```sql
    DROP TABLE [Employees];
    ```

The below query joins sys.ledger_table_history and sys.database_ledger_transactions to produce the history of changes on ledger tables, including the time of each and change and the name of the user who triggered it.

```sql
SELECT 
t.[principal_name]
, t.[commit_time]
, h.[schema_name] + '.' + h.[table_name] AS [table_name]
, h.[ledger_view_schema_name] + '.' + h.[ledger_view_name] AS [view_name]
, h.[operation_type_desc]
FROM sys.ledger_table_history h
JOIN sys.database_ledger_transactions t
ON h.transaction_id = t.transaction_id
```

## See also

- [Ledger considerations and limitations](../security/ledger/ledger-limits.md)
- [Ledger overview](../security/ledger/ledger-overview.md)