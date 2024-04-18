---
title: "Create primary keys in SQL Server"
description: Define a primary key in the SQL Server Database Engine by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 04/18/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "primary keys [SQL Server], creating"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Create primary keys

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You can define a primary key in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. Creating a primary key automatically creates a corresponding unique clustered index. However, your primary key can be specified as a nonclustered index instead.

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## Limitations

A table can contain only one `PRIMARY KEY` constraint.

All columns defined within a `PRIMARY KEY` constraint must be defined as `NOT NULL`. If nullability isn't specified, all columns participating in a `PRIMARY KEY` constraint have their nullability set to `NOT NULL`.

## Permissions

Creating a new table with a primary key requires `CREATE TABLE` permission in the database and `ALTER` permission on the schema in which the table is being created.

Creating a primary key in an existing table requires `ALTER` permission on the table.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In Object Explorer, right-click the table to which you want to add a unique constraint, and select **Design**.
1. In **Table Designer**, select the row selector for the database column you want to define as the primary key. If you want to select multiple columns, hold down the CTRL key while you select the row selectors for the other columns.
1. Right-click the row selector for the column and select **Set Primary Key**.

> [!CAUTION]  
> If you want to redefine the primary key, any relationships to the existing primary key must be deleted before the new primary key can be created. A message will warn you that existing relationships will be automatically deleted as part of this process.

A primary key column is identified by a primary key symbol in its row selector.

If a primary key consists of more than one column, duplicate values are allowed in one column, but each combination of values from all the columns in the primary key must be unique.

If you define a compound key, the order of columns in the primary key matches the order of columns as shown in the table. However, you can change the order of columns after the primary key is created. For more information, see [Modify Primary Keys](modify-primary-keys.md).

## <a id="TsqlProcedure"></a> Use Transact-SQL

### Create a primary key in an existing table

The following example creates a primary key on the column `TransactionID` in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
ALTER TABLE [Production].[TransactionHistoryArchive]
   ADD CONSTRAINT PK_TransactionHistoryArchive_TransactionID PRIMARY KEY CLUSTERED (TransactionID);
```

### Create a primary key in a new table

The following example creates a table and defines a primary key on the column `TransactionID` in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
CREATE TABLE [Production].[TransactionHistoryArchive1] (
    TransactionID INT IDENTITY(1, 1) NOT NULL,
    CONSTRAINT PK_TransactionHistoryArchive1_TransactionID PRIMARY KEY CLUSTERED (TransactionID)
);
```

### Create a nonclustered primary key with separate clustered index in a new table

The following example creates a table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database, with a nonclustered primary key on the column `CustomerID`. Then, it adds a clustered index on `TransactionID`.

1. Create a table to add the clustered index.

   ```sql
   CREATE TABLE [Production].[TransactionHistoryArchive1] (
       CustomerID UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID(),
       TransactionID INT IDENTITY(1, 1) NOT NULL,
       CONSTRAINT PK_TransactionHistoryArchive1_CustomerID PRIMARY KEY NONCLUSTERED (CustomerID)
   );
   ```

1. Now add the clustered index.

   ```sql
   CREATE CLUSTERED INDEX CIX_TransactionID
   ON [Production].[TransactionHistoryArchive1] (TransactionID);
   ```

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [ALTER TABLE table_constraint (Transact-SQL)](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)
