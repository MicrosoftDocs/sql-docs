---
title: "Create and use append-only ledger tables"
description: Learn how to create and use append-only ledger tables.
ms.date: "05/24/2022"
ms.service: sql-database
ms.subservice: security
ms.reviewer: kendralittle, mathoma
ms.topic: how-to
author: VanMSFT
ms.author: vanto
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
---

# Create and use append-only ledger tables

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

This article shows you how to create an [append-only ledger table](ledger-append-only-ledger-tables.md). Next, you'll insert values in your append-only ledger table and then attempt to make updates to the data. Finally, you'll view the results by using the ledger view. We'll use an example of a card key access system for a facility, which is an append-only system pattern. Our example will give you a practical look at the relationship between the append-only ledger table and its corresponding ledger view.

For more information, see [Append-only ledger tables](ledger-append-only-ledger-tables.md).

## Prerequisites

- [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

## Create an append-only ledger table

We'll create a `KeyCardEvents` table with the following schema.

| Column name | Data type | Description |
|--|--|--|
| EmployeeID | int | The unique ID of the employee accessing the building |
| AccessOperationDescription | nvarchar (MAX) | The access operation of the employee |
| Timestamp | datetime2 | The date and time the employee accessed the building |

1. Use [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md) to create a new schema and table called `[AccessControl].[KeyCardEvents]`.

   ```sql
   CREATE SCHEMA [AccessControl];
   GO
   CREATE TABLE [AccessControl].[KeyCardEvents]
      (
         [EmployeeID] INT NOT NULL,
         [AccessOperationDescription] NVARCHAR (1024) NOT NULL,
         [Timestamp] Datetime2 NOT NULL
      )
      WITH (LEDGER = ON (APPEND_ONLY = ON));
   ```

1. Add a new building access event in the `[AccessControl].[KeyCardEvents]` table with the following values.

   ```sql
   INSERT INTO [AccessControl].[KeyCardEvents]
   VALUES ('43869', 'Building42', '2020-05-02T19:58:47.1234567');
   ```

1. View the contents of your KeyCardEvents table, and specify the [GENERATED ALWAYS](../../../t-sql/statements/create-table-transact-sql.md#generate-always-columns) columns that are added to your [append-only ledger table](ledger-append-only-ledger-tables.md).

   ```sql
   SELECT *
        ,[ledger_start_transaction_id]
        ,[ledger_start_sequence_number]
   FROM [AccessControl].[KeyCardEvents];
   ```

   :::image type="content" source="media/ledger/append-only-how-to-keycard-event-table.png" alt-text="Screenshot that shows results from querying the KeyCardEvents table.":::

1. View the contents of your KeyCardEvents ledger view along with the ledger transactions system view to identify who added records into the table.

   ```sql
	SELECT
	t.[commit_time] AS [CommitTime] 
	, t.[principal_name] AS [UserName]
	, l.[EmployeeID]
	, l.[AccessOperationDescription]
	, l.[Timestamp]
	, l.[ledger_operation_type_desc] AS Operation
	FROM [AccessControl].[KeyCardEvents_Ledger] l
	JOIN sys.database_ledger_transactions t
	ON t.transaction_id = l.ledger_transaction_id
	ORDER BY t.commit_time DESC;
   ```

1. Try to update the `KeyCardEvents` table by changing the `EmployeeID` from `43869` to `34184.`

   ```sql
   UPDATE [AccessControl].[KeyCardEvents] SET [EmployeeID] = 34184;
   ```

   You'll receive an error message that states the updates aren't allowed for your append-only ledger table.

   :::image type="content" source="media/ledger/append-only-how-to-1.png" alt-text="Screenshot that shows the append-only error message.":::

## Permissions
Creating append-only ledger tables requires the `ENABLE LEDGER` permission. For more information on permissions related to ledger tables, see [Permissions](../permissions-database-engine.md). 

## Next steps

- [Append-only ledger tables](ledger-append-only-ledger-tables.md)
- [How to migrate data from regular tables to ledger tables](ledger-how-to-migrate-data-to-ledger-tables.md)
