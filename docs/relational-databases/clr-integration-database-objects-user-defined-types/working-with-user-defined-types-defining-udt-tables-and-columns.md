---
title: "Defining UDT Tables and Columns"
description: After you register the assembly that contains a UDT definition, you can use it in a column definition.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "user-defined types [CLR integration], columns"
  - "UDTs [CLR integration], columns"
  - "columns [CLR integration]"
  - "user-defined types [CLR integration], tables"
  - "tables [CLR integration]"
  - "UDTs [CLR integration], tables"
  - "UDTs [CLR integration], indexes"
  - "user-defined types [CLR integration], indexes"
  - "indexes [CLR integration]"
dev_langs:
  - "TSQL"
---
# Define user-defined type (UDT) tables and columns

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Once the assembly containing the user-defined type (UDT) definition is registered in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database, it can be used in a column definition. For more information, see [CREATE TYPE](../../t-sql/statements/create-type-transact-sql.md).

## Create tables with UDTs

There's no special syntax for creating a UDT column in a table. You can use the name of the UDT in a column definition as though it were one of the intrinsic [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data types. The following `CREATE TABLE` [!INCLUDE [tsql](../../includes/tsql-md.md)] statement creates a table named `Points`, with a column named `ID`, which is defined as an **int** identity column and the primary key for the table. The second column is named `PointValue`, with a data type of `Point`. The schema name used in this example is `dbo`. You must have the necessary permissions to specify a schema name. If you omit the schema name, the default schema for the database user is used.

```sql
CREATE TABLE dbo.Points
(
    ID INT IDENTITY (1, 1) PRIMARY KEY,
    PointValue Point
);
```

## Create indexes on UDT columns

There are two options for indexing a UDT column:

- **Index the full value.** In this case, if the UDT is binary-ordered, you can create an index over the entire UDT column by using the `CREATE INDEX` [!INCLUDE [tsql](../../includes/tsql-md.md)] statement.

- **Index UDT expressions.** You can create indexes on persisted computed columns over UDT expressions. The UDT expression can be a field, method, or property of a UDT. The expression must be deterministic and must not perform data access.

For more information, see [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md).

## Related content

- [Work with user-defined types in SQL Server](working-with-user-defined-types-in-sql-server.md)
- [CREATE TYPE (Transact-SQL)](../../t-sql/statements/create-type-transact-sql.md)
- [CLR user-defined types](clr-user-defined-types.md)
