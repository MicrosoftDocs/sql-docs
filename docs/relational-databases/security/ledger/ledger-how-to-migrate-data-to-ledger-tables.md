---
title: "Migrate data from regular tables to ledger tables"
description: Learn how to migrate data from regular tables to ledger tables.
ms.date: "05/24/2022"
ms.service: sql-database
ms.subservice: security
ms.custom:
- event-tier1-build-2022
ms.reviewer: kendralittle, mathoma
ms.topic: how-to
author: VanMSFT
ms.author: vanto
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
---

# Migrate data from regular tables to ledger tables

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

Converting regular tables to ledger tables isn't possible, but you can migrate the data from an existing regular table to a ledger table, and then replace the original table with the ledger table.

When you're performing a database ledger verification, the process needs to order all operations within each transaction. If you use a `SELECT INTO` or `BULK INSERT` statement to copy a few billion rows from a regular table to a ledger table, it will all be done in one single transaction. This means lots of data needs to be fully sorted, which will be done in a single thread. The sorting operation will take a long time to complete.

To convert a regular table into a ledger table, Microsoft recommends using the [sys.sp_copy_data_in_batches](../../../relational-databases/system-stored-procedures/sys-sp-copy-data-in-batches-transact-sql.md) stored procedure. This will split the copy operation in batches of 10-100K rows per transaction. As a result, the database ledger verification will have smaller transactions that can be sorted in parallel. This helps the time of the database ledger verification tremendously.

> [!NOTE]
> The customer can still use other commands, services, or tools to copy the data from the source table to the target table. Make sure you avoid large transactions because this will have a performance impact on the database ledger verification.

This article shows you how can convert a regular table into a ledger table.

## Prerequisites

- [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

## Create an append-only or updatable ledger table

Before you can use the [sys.sp_copy_data_in_batches](../../system-stored-procedures/sys-sp-copy-data-in-batches-transact-sql.md) stored procedure, you need to create an [append-only ledger table](ledger-append-only-ledger-tables.md) or [updatable ledger table](ledger-updatable-ledger-tables.md) with the same schema as the source table. The schema should be identical in terms of number of columns, column names, and their data types. `TRANSACTION ID`, `SEQUENCE NUMBER`, and [GENERATED ALWAYS](../../../t-sql/statements/create-table-transact-sql.md#generate-always-columns) columns are ignored since they're system generated. Indexes between the tables can be different but the target table can only be a Heap table or have a clustered index. Non-clustered indexes should be created afterwards.

Assume we have the following regular `Employees` table in the database.

```sql
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[SSN] [char](11) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Salary] [money] NOT NULL
	);
```

The easiest way to create an [append-only ledger table](ledger-append-only-ledger-tables.md) or [updatable ledger table](ledger-updatable-ledger-tables.md) is scripting the original table and add the `LEDGER = ON` clause. In the script below, we're creating a [new updatable ledger table](ledger-how-to-updatable-ledger-tables.md), called `Employees_LedgerTable` based on the schema of the `Employees` table.

```sql
	CREATE TABLE [dbo].[Employees_LedgerTable](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[SSN] [char](11) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Salary] [money] NOT NULL
	)
    WITH 
    (
      SYSTEM_VERSIONING = ON,
      LEDGER = ON
    ); 
```

## Copy data from a regular table to a ledger table

The stored procedure [sys.sp_copy_data_in_batches](../../system-stored-procedures/sys-sp-copy-data-in-batches-transact-sql.md) copies data from the source table to the target table after verifying that their schema is identical. The data is copied in batches in individual transactions. If the operation fails, the target table will be partially populated. The target table should also be empty.

In the script below, we're copying the data from the regular `Employees` table to the new updatable ledger table, `Employees_LedgerTable`. 

 ```sql
sp_copy_data_in_batches @source_table_name = N'Employees' , @target_table_name = N'Employees_LedgerTable'
```

## Next steps

- [Append-only ledger tables](ledger-append-only-ledger-tables.md)
- [Updatable ledger tables](ledger-updatable-ledger-tables.md)