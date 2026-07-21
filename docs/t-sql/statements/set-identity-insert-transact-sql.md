---
title: "SET IDENTITY_INSERT (Transact-SQL)"
description: Transact-SQL reference for the SET IDENTITY_INSERT statement. When set to ON, this permits inserting explicit values into the identity column of a table.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 01/16/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SET IDENTITY_INSERT"
  - "SET_IDENTITY_INSERT_TSQL"
  - "IDENTITY_INSERT_TSQL"
  - "IDENTITY_INSERT"
helpviewer_keywords:
  - "IDENTITY_INSERT option"
  - "SET IDENTITY_INSERT statement"
  - "identity values [SQL Server], explicit values"
  - "identity columns [SQL Server], explicit values"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azure-sqldw-latest || =fabric-sqldb"
---
# SET IDENTITY_INSERT (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

Allows explicit values to be inserted into the identity column of a table.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET IDENTITY_INSERT [ [ database_name . ] schema_name . ] table_name { ON | OFF }
```

## Arguments

#### *database_name*

The name of the database in which the specified table resides.

#### *schema_name*

The name of the schema to which the table belongs.

#### *table_name*

The name of a table with an identity column.

## Remarks

At any time, only one table in a session can have the `IDENTITY_INSERT` property set to `ON`. If a table already has this property set to `ON`, and a `SET IDENTITY_INSERT ON` statement is issued for another table, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message that states `SET IDENTITY_INSERT` is already `ON`, and reports the table for which `ON` is set.

If the value inserted is larger than the current identity value for the table, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically uses the new inserted value as the current identity value.

The setting of `SET IDENTITY_INSERT` is set at execute or run time and not at parse time.

## Permissions

User must own the table or have `ALTER` permission on the table.

## Examples

The following example creates a table with an identity column and shows how the `SET IDENTITY_INSERT` setting can be used to fill a gap in the identity values caused by a `DELETE` statement.

```sql
USE AdventureWorks2022;
GO
```

Create tool table.

```sql
CREATE TABLE dbo.Tool
(
    ID INT IDENTITY NOT NULL PRIMARY KEY,
    Name VARCHAR (40) NOT NULL
);
GO
```

Insert values into products table.

```sql
INSERT INTO dbo.Tool (Name)
VALUES ('Screwdriver'),
    ('Hammer'),
    ('Saw'),
    ('Shovel');
GO
```

Create a gap in the identity values.

```sql
DELETE dbo.Tool
WHERE Name = 'Saw';
GO

SELECT *
FROM dbo.Tool;
GO
```

Try to insert an explicit ID value of 3.

```sql
INSERT INTO dbo.Tool (ID, Name)
VALUES (3, 'Garden shovel');
GO
```

The previous `INSERT` code should return the following error:

```output
An explicit value for the identity column in table 'AdventureWorks2022.dbo.Tool' can only be specified when a column list is used and IDENTITY_INSERT is ON.
```

Set `IDENTITY_INSERT` to `ON`.

```sql
SET IDENTITY_INSERT dbo.Tool ON;
GO
```

Try to insert an explicit ID value of 3.

```sql
INSERT INTO dbo.Tool (ID, Name)
VALUES (3, 'Garden shovel');
GO

SELECT *
FROM dbo.Tool;
GO
```

Drop tool table.

```sql
DROP TABLE dbo.Tool;
GO
```

## Related content

- [CREATE TABLE (Transact-SQL)](create-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL) IDENTITY (Property)](create-table-transact-sql-identity-property.md)
- [SCOPE_IDENTITY (Transact-SQL)](../functions/scope-identity-transact-sql.md)
- [INSERT (Transact-SQL)](insert-transact-sql.md)
- [SET Statements (Transact-SQL)](set-statements-transact-sql.md)

