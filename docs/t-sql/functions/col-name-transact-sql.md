---
title: "COL_NAME (Transact-SQL)"
description: "COL_NAME (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/07/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "COL_NAME"
  - "COL_NAME_TSQL"
helpviewer_keywords:
  - "column properties [SQL Server]"
  - "COL_NAME function"
  - "column names [SQL Server]"
  - "names [SQL Server], columns"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# COL_NAME (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

This function returns the name of a table column, based on the table identification number and column identification number values of that table column.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
COL_NAME ( table_id , column_id )
```

## Arguments

#### *table_id*

The identification number of the table containing that column. The *table_id* argument has an **int** data type.

#### *column_id*

The identification number of the column. The *column_id* argument has an **int** data type.

## Return types

**sysname**

## Exceptions

Returns `NULL` on error, or if a caller doesn't have the correct permission to view the object.

A user can only view the metadata of securables that the user owns, or on which the user is granted permission. This means that metadata-emitting, built-in functions such as `COL_NAME` might return `NULL`, if the user doesn't have correct permissions on the object. For more information, see [Metadata visibility configuration](../../relational-databases/security/metadata-visibility-configuration.md).

## Remarks

The *table_id* and *column_id* parameters together produce a column name string.

For more information about obtaining table and column identification numbers, see [OBJECT_ID](object-id-transact-sql.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Return names of first two columns in a table

This example returns the name of the first two columns in the `Person.Person` table.

```sql
USE AdventureWorks2022;
GO

SELECT COL_NAME(OBJECT_ID('Person.Person'), 1) AS FirstColumnName,
       COL_NAME(OBJECT_ID('Person.Person'), 2) AS SecondColumnName;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
FirstColumnName    SecondColumnName
-----------------  -----------------
BusinessEntityID    PersonType
```

## Related content

- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [Metadata Functions (Transact-SQL)](metadata-functions-transact-sql.md)
- [COLUMNPROPERTY (Transact-SQL)](columnproperty-transact-sql.md)
- [COL_LENGTH (Transact-SQL)](col-length-transact-sql.md)

