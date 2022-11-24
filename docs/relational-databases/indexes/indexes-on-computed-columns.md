---
title: Indexes on computed columns
description: Learn how to create indexes on computed columns
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/14/2022
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "computed columns, index creation"
  - "index creation [SQL Server], computed columns"
  - "imprecise columns"
  - "persisted computed columns"
  - "precise [SQL Server]"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Indexes on computed columns

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can define indexes on computed columns as long as the following requirements are met:

- Ownership requirements
- Determinism requirements
- Precision requirements
- Data type requirements
- SET option requirements

> [!NOTE]  
> `SET QUOTED_IDENTIFIER` must be `ON` when you are creating or changing indexes on computed columns or indexed views. For more information, see [SET QUOTED_IDENTIFIER (Transact-SQL)](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

## Ownership requirements

All function references in the computed column must have the same owner as the table.

## Determinism requirements

Expressions are deterministic if they always return the same result for a specified set of inputs. The `IsDeterministic` property of the [COLUMNPROPERTY](../../t-sql/functions/columnproperty-transact-sql.md) function reports whether a *computed_column_expression* is deterministic.

The *computed_column_expression* must be deterministic. A *computed_column_expression* is deterministic when all of the following are true:

- All functions that are referenced by the expression are deterministic and precise. These functions include both user-defined and built-in functions. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md). Functions might be imprecise if the computed column is `PERSISTED`. For more information, see [Create indexes on persisted computed columns](#create-indexes-on-persisted-computed-columns) late in this article.

- All columns that are referenced in the expression come from the table that contains the computed column.

- No column reference pulls data from multiple rows. For example, aggregate functions such as `SUM` or `AVG` depend on data from multiple rows and would make a *computed_column_expression* nondeterministic.

- The *computed_column_expression* has no system data access or user data access.

Any computed column that contains a common language runtime (CLR) expression must be deterministic and marked `PERSISTED` before the column can be indexed. CLR user-defined type expressions are allowed in computed column definitions. Computed columns whose type is a CLR user-defined type can be indexed as long as the type is comparable. For more information, see [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md).

#### CAST and CONVERT

When you refer to string literals of the date data type in indexed computed columns in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], we recommend that you explicitly convert the literal to the date type that you want by using a deterministic date format style. For a list of the date format styles that are deterministic, see [CAST and CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md).

For more information, see [Nondeterministic conversion of literal date strings into DATE values](../../t-sql/data-types/nondeterministic-convert-date-literals.md).

#### Compatibility level

Implicit conversion of non-Unicode character data between collations is considered nondeterministic, unless the compatibility level is set to `80` or earlier.

When the database compatibility level setting is `90`, you can't create indexes on computed columns that contain these expressions. However, existing computed columns that contain these expressions from an upgraded database are maintainable. If you use indexed computed columns that contain implicit string to date conversions, to avoid possible index corruption, make sure that the `LANGUAGE` and `DATEFORMAT` settings are consistent in your databases and applications.

Compatibility level `90` corresponds to [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)].

## Precision requirements

 The *computed_column_expression* must be precise. A *computed_column_expression* is precise when one or more of the following is true:

- It isn't an expression of the **float** or **real** data types.
- It doesn't use a **float** or **real** data type in its definition. For example, in the following statement, column `y` is **int** and deterministic but not precise.

  ```sql
  CREATE TABLE t2 (a int, b int, c int, x float,
      y AS CASE x
            WHEN 0 THEN a
            WHEN 1 THEN b
            ELSE c
        END);
  ```

> [!NOTE]  
> Any **float** or **real** expression is considered imprecise and cannot be a key of an index; a **float** or **real** expression can be used in an indexed view but not as a key. This is true also for computed columns. Any function, expression, or user-defined function is considered imprecise if it contains any **float** or **real** expressions. This includes logical ones (comparisons).

The `IsPrecise` property of the `COLUMNPROPERTY` function reports whether a *computed_column_expression* is precise.

## Data type requirements

- The *computed_column_expression* defined for the computed column can't evaluate to the **text**, **ntext**, or **image** data types.
- Computed columns derived from **image**, **ntext**, **text**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and **xml** data types can be indexed as long as the computed column data type is allowable as an index key column.
- Computed columns derived from **image**, **ntext**, and **text** data types can be nonkey (included) columns in a nonclustered index as long as the computed column data type is allowable as a nonkey index column.

## SET option requirements

- The `ANSI_NULLS` connection-level option must be set to `ON` when the `CREATE TABLE` or `ALTER TABLE` statement that defines the computed column is executed. The [OBJECTPROPERTY](../../t-sql/functions/objectproperty-transact-sql.md) function reports whether the option is on through the `IsAnsiNullsOn` property.
- The connection on which the index is created, and all connections trying `INSERT`, `UPDATE`, or `DELETE` statements that will change values in the index, must have six `SET` options set to `ON` and one option set to `OFF`. The optimizer ignores an index on a computed column for any `SELECT` statement executed by a connection that doesn't have these same option settings.

  The `NUMERIC_ROUNDABORT` option must be set to `OFF`, and the following options must be set to `ON`:  
  - `ANSI_NULLS`  
  - `ANSI_PADDING`  
  - `ANSI_WARNINGS`  
  - `ARITHABORT`
  - `CONCAT_NULL_YIELDS_NULL`  
  - `QUOTED_IDENTIFIER`

> [!NOTE]  
> Setting `ANSI_WARNINGS` to `ON` implicitly sets `ARITHABORT` to `ON` when the database compatibility level is set to `90` or higher.

## Create indexes on persisted computed columns

Sometimes you can create a computed column that is defined with an expression that is deterministic yet imprecise. You can do this when the column is marked `PERSISTED` in the `CREATE TABLE` or `ALTER TABLE` statement.

This means that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] stores the computed values in the table, and updates them when any other columns on which the computed column depends are updated. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses these persisted values when it creates an index on the column, and when the index is referenced in a query.

This option enables you to create an index on a computed column when [!INCLUDE[ssDE](../../includes/ssde-md.md)] can't prove with accuracy whether a function that returns computed column expressions, particularly a CLR function that is created in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)], is both deterministic and precise.

> [!NOTE]  
> You can't create a [filtered index](create-filtered-indexes.md) on a computed column.

## Next steps

- [COLUMNPROPERTY (Transact-SQL)](../../t-sql/functions/columnproperty-transact-sql.md)  
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)  
- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
