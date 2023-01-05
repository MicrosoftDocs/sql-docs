---
title: "Create and use updatable ledger tables"
description: Learn how to create and use updatable ledger tables.
ms.date: "05/24/2022"
ms.service: sql-database
ms.subservice: security
ms.reviewer: kendralittle, mathoma
ms.topic: how-to
author: VanMSFT
ms.author: vanto
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
---

# Create and use updatable ledger tables

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

This article shows you how to create an [updatable ledger table](ledger-updatable-ledger-tables.md). Next, you'll insert values in your updatable ledger table and then make updates to the data. Finally, you'll view the results by using the ledger view. We'll use an example of a banking application that tracks banking customers' balances in their accounts. Our example will give you a practical look at the relationship between the updatable ledger table and its corresponding history table and ledger view.

## Prerequisites

- [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

## Create an updatable ledger table

We'll create an account balance table with the following schema.

| Column name | Data type      | Description                         |
| ----------- | -------------- | ----------------------------------- |
| CustomerID  | int            | Customer ID - Primary key clustered |
| LastName    | varchar (50)   | Customer last name                  |
| FirstName   | varchar (50)   | Customer first name                 |
| Balance     | decimal (10,2) | Account balance                     |

1. Use [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md) to create a new schema and table called `[Account].[Balance]`.

   ```sql
   CREATE SCHEMA [Account];
   GO  
   CREATE TABLE [Account].[Balance]
   (
       [CustomerID] INT NOT NULL PRIMARY KEY CLUSTERED,
       [LastName] VARCHAR (50) NOT NULL,
       [FirstName] VARCHAR (50) NOT NULL,
       [Balance] DECIMAL (10,2) NOT NULL
   )
   WITH 
   (
   	SYSTEM_VERSIONING = ON (HISTORY_TABLE = [Account].[BalanceHistory]),
   	LEDGER = ON
   );
   ```

    > [!NOTE]
    > Specifying the `LEDGER = ON` argument is optional if you enabled a ledger database when you created your database.

1. When your [updatable ledger table](ledger-updatable-ledger-tables.md) is created, the corresponding history table and ledger view are also created. Run the following T-SQL commands to see the new table and the new view.

   ```sql
   SELECT 
   ts.[name] + '.' + t.[name] AS [ledger_table_name]
   , hs.[name] + '.' + h.[name] AS [history_table_name]
   , vs.[name] + '.' + v.[name] AS [ledger_view_name]
   FROM sys.tables AS t
   JOIN sys.tables AS h ON (h.[object_id] = t.[history_table_id])
   JOIN sys.views v ON (v.[object_id] = t.[ledger_view_id])
   JOIN sys.schemas ts ON (ts.[schema_id] = t.[schema_id])
   JOIN sys.schemas hs ON (hs.[schema_id] = h.[schema_id])
   JOIN sys.schemas vs ON (vs.[schema_id] = v.[schema_id])
   WHERE t.[name] = 'Balance';
   ```

   :::image type="content" source="media/ledger/ledger-updatable-how-to-new-tables.png" alt-text="Screenshot that shows querying new ledger tables.":::

1. Insert the name `Nick Jones` as a new customer with an opening balance of $50.

   ```sql
   INSERT INTO [Account].[Balance]
   VALUES (1, 'Jones', 'Nick', 50);
   ```

1. Insert the names `John Smith`, `Joe Smith`, and `Mary Michaels` as new customers with opening balances of $500, $30, and $200, respectively.

   ```sql
   INSERT INTO [Account].[Balance]
   VALUES (2, 'Smith', 'John', 500),
   (3, 'Smith', 'Joe', 30),
   (4, 'Michaels', 'Mary', 200);
   ```

1. View the `[Account].[Balance]` updatable ledger table, and specify the [GENERATED ALWAYS](../../../t-sql/statements/create-table-transact-sql.md#generate-always-columns) columns added to the table.

   ```sql
   SELECT [CustomerID]
	  ,[LastName]
	  ,[FirstName]
	  ,[Balance]
      ,[ledger_start_transaction_id]
      ,[ledger_end_transaction_id]
      ,[ledger_start_sequence_number]
      ,[ledger_end_sequence_number]
    FROM [Account].[Balance];  
   ```
   In the results window, you'll first see the values inserted by your T-SQL commands, along with the system metadata that's used for data lineage purposes.

   - The `ledger_start_transaction_id` column notes the unique transaction ID associated with the transaction that inserted the data. Because `John`, `Joe`, and `Mary` were inserted by using the same transaction, they share the same transaction ID.
   - The `ledger_start_sequence_number` column notes the order by which values were inserted by the transaction.

      :::image type="content" source="media/ledger/sql-updatable-how-to-1.png" alt-text="Screenshot that shows ledger table example 1.":::

1. Update `Nick`'s balance from `50` to `100`.

   ```sql
   UPDATE [Account].[Balance] SET [Balance] = 100
   WHERE [CustomerID] = 1;
   ```
1. View the `[Account].[Balance]` ledger view, along with the transaction ledger system view to identify users that made the changes.

   ```sql
  	SELECT
	t.[commit_time] AS [CommitTime] 
	, t.[principal_name] AS [UserName]
	, l.[CustomerID]
	, l.[LastName]
	, l.[FirstName]
	, l.[Balance]
	, l.[ledger_operation_type_desc] AS Operation
	FROM [Account].[Balance_Ledger] l
	JOIN sys.database_ledger_transactions t
	ON t.transaction_id = l.ledger_transaction_id
	ORDER BY t.commit_time DESC;
   ```

   > [!TIP]
   > We recommend that you query the history of changes through the [ledger view](ledger-updatable-ledger-tables.md#ledger-view) and not the [history table](ledger-updatable-ledger-tables.md#history-table).

    `Nick`'s account balance was successfully updated in the updatable ledger table to `100`.  
    The ledger view shows that updating the ledger table is a `DELETE` of the original row with `50`. The balance with a corresponding `INSERT` of a new row with `100` shows the new balance for `Nick`.

   :::image type="content" source="media/ledger/sql-updatable-how-to-3.png" alt-text="Screenshot that shows ledger table example 3.":::

## Permissions
Creating updatable ledger tables requires the `ENABLE LEDGER` permission. For more information on permissions related to ledger tables, see [Permissions](../permissions-database-engine.md). 

## Next steps

- [Database ledger](ledger-database-ledger.md)
- [Updatable ledger tables](ledger-updatable-ledger-tables.md)
- [Append-only ledger tables](ledger-append-only-ledger-tables.md)
- [How to migrate data from regular tables to ledger tables](ledger-how-to-migrate-data-to-ledger-tables.md)
