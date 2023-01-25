---
title: "Create Primary Keys in SQL Server"
description: "Create Primary Keys"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "primary keys [SQL Server], creating"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: FY22Q2Fresh
ms.date: 10/25/2021
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Primary Keys

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You can define a primary key in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Creating a primary key automatically creates a corresponding unique clustered index, or a nonclustered index if specified as such.

## <a name="BeforeYouBegin"></a> Before You Begin

### <a name="Restrictions"></a> Limitations and Restrictions

- A table can contain only one PRIMARY KEY constraint.

- All columns defined within a PRIMARY KEY constraint must be defined as NOT NULL. If nullability is not specified, all columns participating in a PRIMARY KEY constraint have their nullability set to NOT NULL.

### <a name="Security"></a> Security

#### <a name="Permissions"></a> Permissions

Creating a new table with a primary key requires CREATE TABLE permission in the database and ALTER permission on the schema in which the table is being created.

Creating a primary key in an existing table requires ALTER permission on the table.

## <a name="SSMSProcedure"></a> Using SQL Server Management Studio

### Create a primary key

1. In Object Explorer, right-click the table to which you want to add a unique constraint, and click **Design**.
2. In **Table Designer**, click the row selector for the database column you want to define as the primary key. If you want to select multiple columns, hold down the CTRL key while you click the row selectors for the other columns.
3. Right-click the row selector for the column and select **Set Primary Key**.

> [!CAUTION]
> If you want to redefine the primary key, any relationships to the existing primary key must be deleted before the new primary key can be created. A message will warn you that existing relationships will be automatically deleted as part of this process.

A primary key column is identified by a primary key symbol in its row selector.

If a primary key consists of more than one column, duplicate values are allowed in one column, but each combination of values from all the columns in the primary key must be unique.

If you define a compound key, the order of columns in the primary key matches the order of columns as shown in the table. However, you can change the order of columns after the primary key is created. For more information, see [Modify Primary Keys](../../relational-databases/tables/modify-primary-keys.md).

## <a name="TsqlProcedure"></a> Using Transact-SQL

### Create a primary key in an existing table

The following example creates a primary key on the column `TransactionID` in the AdventureWorks database.

```sql
ALTER TABLE Production.TransactionHistoryArchive
   ADD CONSTRAINT PK_TransactionHistoryArchive_TransactionID PRIMARY KEY CLUSTERED (TransactionID);
```

### Create a primary key in a new table

The following example creates a table and defines a primary key on the column `TransactionID` in the AdventureWorks database.

```sql
CREATE TABLE Production.TransactionHistoryArchive1
   (
      TransactionID int IDENTITY (1,1) NOT NULL
      , CONSTRAINT PK_TransactionHistoryArchive1_TransactionID PRIMARY KEY CLUSTERED (TransactionID)
   )
;
```

### Create a primary key with clustered index in a new table

The following example creates a table and defines a primary key on the column `CustomerID` and a clustered index on `TransactionID` in the AdventureWorks database.

```sql
-- Create table to add the clustered index
CREATE TABLE Production.TransactionHistoryArchive1
   (
      CustomerID uniqueidentifier DEFAULT NEWSEQUENTIALID()
      , TransactionID int IDENTITY (1,1) NOT NULL
      , CONSTRAINT PK_TransactionHistoryArchive1_CustomerID PRIMARY KEY NONCLUSTERED (CustomerID)
   )
;

-- Now add the clustered index
CREATE CLUSTERED INDEX CIX_TransactionID ON Production.TransactionHistoryArchive1 (TransactionID);
```

## Next steps

- [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md)
- [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) 
- [table_constraint](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)
