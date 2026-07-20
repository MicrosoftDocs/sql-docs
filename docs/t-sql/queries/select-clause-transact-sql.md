---
title: "SELECT Clause (Transact-SQL)"
description: The SELECT clause specifies the columns that the Transact-SQL query returns.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 02/02/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SELECT Clause"
  - "SELECT_Clause_TSQL"
  - "DISTINCT_TSQL"
helpviewer_keywords:
  - "parentheses [SQL Server]"
  - "identity columns [SQL Server], SELECT clause"
  - "SELECT clause"
  - "$IDENTITY keyword"
  - "user-defined types [SQL Server], invoking methods and properties"
  - "SELECT statement [SQL Server], processing orders"
  - "clauses [SQL Server], SELECT"
  - "$ROWGUID keyword"
  - "queries [SQL Server], results"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SELECT clause (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]

Specifies the columns that the Transact-SQL (T-SQL) query returns.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SELECT [ ALL | DISTINCT ]
[ TOP ( expression ) [ PERCENT ] [ WITH TIES ] ]
<select_list>
<select_list> ::=
    {
      *
      | { table_name | view_name | table_alias } .*
      | {
          [ { table_name | view_name | table_alias } . ]
               { column_name | $IDENTITY | $ROWGUID }
          | udt_column_name [ { . | :: } { { property_name | field_name }
            | method_name ( argument [ , ...n ] ) } ]
          | expression
         }
        [ [ AS ] column_alias ]
      | column_alias = expression
    } [ , ...n ]
```

## Arguments

#### ALL

Specifies that duplicate rows can appear in the result set. `ALL` is the default.

#### DISTINCT

Specifies that only unique rows can appear in the result set. Null values are considered equal for the purposes of the `DISTINCT` keyword.

#### TOP (*expression*) [ PERCENT ] [ WITH TIES ]

Indicates that only a specified first set or percent of rows are returned from the query result set. *expression* can be either a number or a percent of the rows.

Although `TOP <expression>` without parentheses is supported in `SELECT` statements for backward compatibility, avoid this syntax. For more information, see [TOP](top-transact-sql.md).

#### \<select_list>

Specifies the columns to select for the result set. The select list is a series of expressions separated by commas. The maximum number of expressions that you can specify in the select list is 4,096.

#### \*

Specifies that all columns from all tables and views in the `FROM` clause should be returned. The columns are returned by table or view, as specified in the `FROM` clause, and in the order in which they exist in the table or view.

#### *table_name* | *view_name* | *table*_*alias*.*

Limits the scope of the \* to the specified table or view.

#### *column_name*

The name of a column to return. Qualify *column_name* to prevent an ambiguous reference, such as occurs when two tables in the `FROM` clause have columns with duplicate names. For example, the `SalesOrderHeader` and `SalesOrderDetail` tables in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database both have a column named `ModifiedDate`. If the two tables are joined in a query, you can specify the modified date of the `SalesOrderDetail` entries in the select list as `SalesOrderDetail.ModifiedDate`.

#### *expression*

A constant, function, any combination of column names, constants, and functions connected by an operator or operators, or a subquery.

#### $IDENTITY

Returns the identity column. For more information, see [IDENTITY (Property)](../statements/create-table-transact-sql-identity-property.md), [ALTER TABLE](../statements/alter-table-transact-sql.md), and [CREATE TABLE](../statements/create-table-transact-sql.md).

If more than one table in the `FROM` clause has a column with the `IDENTITY` property, you must qualify `$IDENTITY` with the specific table name, such as `T1.$IDENTITY`.

#### $ROWGUID

Returns the row GUID column.

If more than one table in the `FROM` clause has the `ROWGUIDCOL` property, you must qualify `$ROWGUID` with the specific table name, such as `T1.$ROWGUID`.

#### *udt_column_name*

The name of a common language runtime (CLR) user-defined type column to return.

> [!NOTE]  
> [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] returns user-defined type values in binary representation. To return user-defined type values in string or XML format, use [CAST and CONVERT](../functions/cast-and-convert-transact-sql.md) or [CAST and CONVERT](../functions/cast-and-convert-transact-sql.md).

#### { . | :: }

Specifies a method, property, or field of a CLR user-defined type. Use a period (`.`) for an instance (nonstatic) method, property, or field. Use a double-colon (`::`) for a static method, property, or field. To invoke a method, property, or field of a CLR user-defined type, you must have `EXECUTE` permission on the type.

#### *property_name*

A public property of *udt_column_name*.

#### *field_name*

A public data member of *udt_column_name*.

#### *method_name*

A public method of *udt_column_name* that takes one or more arguments. *method_name* can't be a mutator method.

The following example selects the values for the `Location` column, defined as type **point**, from the `Cities` table, by invoking a method of the type called `Distance`:

```sql
CREATE TABLE dbo.Cities
(
    Name VARCHAR (20),
    State VARCHAR (20),
    Location POINT
);
GO

DECLARE @p AS POINT (32, 23),
        @distance AS FLOAT;

SELECT Location.Distance(@p)
FROM Cities;
```

#### *column_alias*

An alternative name to replace the column name in the query result set. For example, an alias such as `Quantity`, or `Quantity` to `Date`, or `Qty` can be specified for a column named `quantity`.

You can use aliases to specify names for the results of expressions, for example:

```sql
USE AdventureWorks2025;
GO

SELECT AVG(UnitPrice) AS [Average Price]
FROM Sales.SalesOrderDetail;
```

*column_alias* can be used in an `ORDER BY` clause. However, you can't use it in a `WHERE`, `GROUP BY`, or `HAVING` clause. If the query expression is part of a `DECLARE CURSOR` statement, *column_alias* can't be used in the `FOR UPDATE` clause.

## Remarks

When you include **text** or **ntext** columns in the select list, the length of the returned data is the smallest value of the following options:

- the actual size of the **text** column,
- the default `TEXTSIZE` session setting, or
- the hard-coded application limit.

To change the length of returned text for the session, use the `SET` statement. By default, the limit on the length of text data returned by a `SELECT` statement is 4,000 bytes.

The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] raises exception 511 and rolls back the current running statement if either of the following behaviors occurs:

- The `SELECT` statement produces a result row or an intermediate work table row that exceeds 8,060 bytes.

- The `DELETE`, `INSERT`, or `UPDATE` statement tries an action on a row that exceeds 8,060 bytes.

An error occurs if you don't specify a column name for a column created by a `SELECT INTO` or `CREATE VIEW` statement.

## Related content

- [SELECT examples (Transact-SQL)](select-examples-transact-sql.md)
- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [SELECT (Transact-SQL)](select-transact-sql.md)
