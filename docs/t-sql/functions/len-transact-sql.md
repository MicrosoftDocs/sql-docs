---
title: "LEN (Transact-SQL)"
description: LEN returns the number of characters of the specified string expression, excluding trailing spaces.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/20/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "LEN"
  - "LEN_TSQL"
helpviewer_keywords:
  - "LEN function"
  - "characters [SQL Server], number of"
  - "number of characters"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# LEN (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns the number of characters of the specified string expression, excluding trailing spaces.

> [!NOTE]  
> To return the number of bytes used to represent an expression, use the [DATALENGTH](datalength-transact-sql.md) function.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
LEN ( string_expression )
```

## Arguments

#### *string_expression*

The string [expression](../language-elements/expressions-transact-sql.md) to be evaluated. *string_expression* can be a constant, variable, or column of either character or binary data.

## Return types

**bigint** if *expression* is of the **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** data types; otherwise, **int**.

If you're using SC collations, the returned integer value counts UTF-16 surrogate pairs as a single character. For more information, see [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md).

## Remarks

`LEN` excludes trailing spaces. If that is a problem, consider using the [DATALENGTH](datalength-transact-sql.md) function, which doesn't trim the string. If processing a unicode string, `DATALENGTH` returns a number that might not be equal to the number of characters. The following example demonstrates `LEN` and `DATALENGTH` with a trailing space.

```sql
DECLARE @v1 AS VARCHAR (40), @v2 AS NVARCHAR (40);

SELECT @v1 = 'Test of 22 characters ',
       @v2 = 'Test of 22 characters ';

SELECT LEN(@v1) AS [VARCHAR LEN],
       DATALENGTH(@v1) AS [VARCHAR DATALENGTH];

SELECT LEN(@v2) AS [NVARCHAR LEN],
       DATALENGTH(@v2) AS [NVARCHAR DATALENGTH];
```

> [!NOTE]  
> Use `LEN` to return the number of characters encoded into a given string expression, and [DATALENGTH](datalength-transact-sql.md) to return the size in bytes for a given string expression. These outputs might differ depending on the data type and type of encoding used in the column. For more information on storage differences between different encoding types, see [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md).

## Examples

The following example selects the number of characters and the data in `FirstName` for people located in `Australia`. This example uses the AdventureWorks database.

```sql
SELECT LEN(FirstName) AS Length,
       FirstName,
       LastName
FROM Sales.vIndividualCustomer
WHERE CountryRegionName = 'Australia';
GO
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

The following example returns the number of characters in the column `FirstName` and the first name (`FirstName`) and family name (`LastName`) of employees located in `Australia`.

```sql
USE AdventureWorks2022;
GO

SELECT DISTINCT LEN(FirstName) AS FNameLength,
                FirstName,
                LastName
FROM dbo.DimEmployee AS e
     INNER JOIN dbo.DimGeography AS g
         ON e.SalesTerritoryKey = g.SalesTerritoryKey
WHERE EnglishCountryRegionName = 'Australia';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
FNameLength  FirstName  LastName
-----------  ---------  ---------------
4            Lynn       Tsoflias
```

## Related content

- [DATALENGTH (Transact-SQL)](datalength-transact-sql.md)
- [CHARINDEX (Transact-SQL)](charindex-transact-sql.md)
- [PATINDEX (Transact-SQL)](patindex-transact-sql.md)
- [LEFT (Transact-SQL)](left-transact-sql.md)
- [RIGHT (Transact-SQL)](right-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
