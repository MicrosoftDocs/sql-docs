---
title: "SUBSTRING (Transact-SQL)"
description: The SUBSTRING function returns a portion of a specified character, binary, text, or image expression.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 01/08/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SUBSTRING"
  - "SUBSTRING_TSQL"
helpviewer_keywords:
  - "portion of expression returned [SQL Server]"
  - "part of expression returned [SQL Server]"
  - "SUBSTRING function"
  - "offsets [SQL Server]"
  - "binary [SQL Server], returning part of"
  - "expressions [SQL Server], part returned"
  - "characters [SQL Server], returning part of"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# SUBSTRING (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns part of a character, binary, text, or image expression in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

::: moniker range="<=sql-server-ver16 || <=sql-server-linux-ver16"

Syntax for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions.

```syntaxsql
SUBSTRING ( expression , start , length )
```

::: moniker-end
::: moniker range=">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-ver17 || >=sql-server-linux-ver17 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"

Syntax for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE [ssazurepdw_md](../../includes/ssazurepdw_md.md)], and [!INCLUDE [fabric-dw-short](../../includes/fabric-dw-short.md)] and [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)] in [!INCLUDE [fabric](../../includes/fabric.md)].

```syntaxsql
SUBSTRING ( expression , start [ , length ] )
```

> [!NOTE]  
> In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, [*length* is required](?view=sql-server-ver16&preserve-view=true#syntax).

::: moniker-end

## Arguments

#### *expression*

A **character**, **binary**, **text**, **ntext**, or **image** [expression](../language-elements/expressions-transact-sql.md).

#### *start*

An integer or **bigint** expression that specifies where the returned characters start. (The numbering is 1 based, meaning that the first character in the expression is 1). If *start* is less than 1, the returned expression begins at the first character that is specified in *expression*. In this case, the number of characters that are returned is the largest value of either the sum of *start* + *length* - 1, or 0. If *start* is greater than the number of characters in the value expression, a zero-length expression is returned.

#### *length*

A positive integer or **bigint** expression that specifies how many characters of the *expression* are returned. If *length* is negative, an error is generated and the statement is terminated. If the sum of *start* and *length* is greater than the number of characters in *expression*, the whole value expression beginning at *start* is returned. If *length* is omitted, all characters from the start position to the end of the expression is returned.

You can use substring with an optional *length* argument. However, if you use `NULL` for *length*, `SUBSTRING` returns `NULL`. Review [E. Use SUBSTRING with optional length argument](#e-use-substring-with-optional-length-argument) for an example.

## Return types

Returns character data if *expression* is one of the supported character data types. Returns binary data if *expression* is one of the supported **binary** data types. The returned string is the same type as the specified expression with the exceptions shown in the following table.

| Specified expression | Return type |
| --- | --- |
| **char** / **varchar** / **text** | **varchar** |
| **nchar** / **nvarchar** / **ntext** | **nvarchar** |
| **binary** / **varbinary** / **image** | **varbinary** |

## Remarks

You must specify the values for *start* and *length* in number of characters for **ntext**, **char**, or **varchar** data types. For **text**, **image**, **binary**, or **varbinary** data types, you must specify these values in bytes.

The *expression* must be **varchar(max)** or **varbinary(max)** when the *start* or *length* contains a value larger than 2,147,483,647.

## Supplementary characters (surrogate pairs)

When you use supplementary character (SC) collations, both *start* and *length* count each surrogate pair in *expression* as a single character. For more information, see [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md).

## Examples

### A. Use SUBSTRING with a character string

The following example shows how to return only a part of a character string. From the `sys.databases` table, this query returns the system database names in the first column, the first letter of the database in the second column, and the third and fourth characters in the final column.

```sql
SELECT name,
       SUBSTRING(name, 1, 1) AS Initial,
       SUBSTRING(name, 3, 2) AS ThirdAndFourthCharacters
FROM sys.databases
WHERE database_id < 5;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

| name | Initial | ThirdAndFourthCharacters |
| --- | --- | --- |
| `master` | `m` | `st` |
| `tempdb` | `t` | `mp` |
| `model` | `m` | `de` |
| `msdb` | `m` | `db` |

To display the second, third, and fourth characters of the string constant `abcdef`, use the following query.

```sql
SELECT SUBSTRING('abcdef', 2, 3) AS x;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
x
----------
bcd
```

### B. Use SUBSTRING with text, ntext, and image data

> [!NOTE]  
> To run the following examples, you must install the [**pubs** database](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs).

The following example shows how to return the first 10 characters from each of a **text** and **image** data column in the `pub_info` table of the `pubs` database. The query returns **text** data as **varchar**, and **image** data as **varbinary**.

```sql
USE pubs;
GO

SELECT pub_id,
       SUBSTRING(logo, 1, 10) AS logo,
       SUBSTRING(pr_info, 1, 10) AS pr_info
FROM pub_info
WHERE pub_id = '1756';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
 pub_id logo    pr_info
------ ---------------------- ----------
1756   0x474946383961E3002500 This is sa
```

The following example shows the effect of `SUBSTRING` on both **text** and **ntext** data. First, this example creates a new table in the `pubs` database named `npub_info`. Second, the example creates the `pr_info` column in the `npub_info` table from the first 80 characters of the `pub_info.pr_info` column and adds an `ü` as the first character. Lastly, an `INNER JOIN` retrieves all publisher identification numbers and the `SUBSTRING` of both the **text** and **ntext** publisher information columns.

```sql
IF EXISTS (SELECT table_name
           FROM INFORMATION_SCHEMA.TABLES
           WHERE table_name = 'npub_info')
    DROP TABLE npub_info;
GO

-- Create npub_info table in pubs database. Borrowed from instpubs.sql.
USE pubs;
GO

CREATE TABLE npub_info
(
    pub_id CHAR (4) NOT NULL FOREIGN KEY
        REFERENCES publishers (pub_id)
        CONSTRAINT UPKCL_npubinfo PRIMARY KEY CLUSTERED,
    pr_info NTEXT NULL
);
GO

-- Fill the pr_info column in npub_info with international data.
RAISERROR ('Now at the inserts to pub_info...', 0, 1);
GO

INSERT npub_info
VALUES ('0736', N'üThis is sample text data for New Moon Books, publisher 0736 in the pubs database'),
    ('0877', N'üThis is sample text data for Binnet & Hardley, publisher 0877 in the pubs databa'),
    ('1389', N'üThis is sample text data for Algodata Infosystems, publisher 1389 in the pubs da'),
    ('9952', N'üThis is sample text data for Scootney Books, publisher 9952 in the pubs database'),
    ('1622', N'üThis is sample text data for Five Lakes Publishing, publisher 1622 in the pubs d'),
    ('1756', N'üThis is sample text data for Ramona Publishers, publisher 1756 in the pubs datab'),
    ('9901', N'üThis is sample text data for GGG&G, publisher 9901 in the pubs database. GGG&G i'),
    ('9999', N'üThis is sample text data for Lucerne Publishing, publisher 9999 in the pubs data');
GO

-- Join between npub_info and pub_info on pub_id.
SELECT pr.pub_id,
       SUBSTRING(pr.pr_info, 1, 35) AS pr_info,
       SUBSTRING(npr.pr_info, 1, 35) AS npr_info
FROM pub_info AS pr
     INNER JOIN npub_info AS npr
         ON pr.pub_id = npr.pub_id
ORDER BY pr.pub_id ASC;
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### C. Use SUBSTRING with a character string

The following example shows how to return only a part of a character string. From the `dbo.DimEmployee` table, this query returns the family name in one column with only the first initial in the second column.

```sql
-- Uses AdventureWorks
SELECT LastName,
       SUBSTRING(FirstName, 1, 1) AS Initial
FROM dbo.DimEmployee
WHERE LastName LIKE 'Bar%'
ORDER BY LastName;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
LastName             Initial
-------------------- -------
Barbariol            A
Barber               D
Barreto de Mattos    P
```

The following example shows how to return the second, third, and fourth characters of the string constant `abcdef`.

```sql
USE ssawPDW;

SELECT TOP 1 SUBSTRING('abcdef', 2, 3) AS x
FROM dbo.DimCustomer;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
x
-----
bcd
```

### D. Use SUBSTRING with `NULL` length argument

```sql
SELECT SUBSTRING('123abc', 4, NULL) AS [NULL length];
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
NULL length
-----------
NULL
```

### E. Use SUBSTRING with optional length argument

**Applies to:** [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE [ssazurepdw_md](../../includes/ssazurepdw_md.md)], [!INCLUDE [fabric-se-short](../../includes/fabric-se-short.md)], and [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)]

The following example shows how to return only a part of a character string from a given start position. Since the *length* argument isn't provided, the function returns the remaining characters in the string.

```sql
SELECT SUBSTRING('123abc', 4) AS y;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
y
-----
abc
```

### F. Use SUBSTRING without a length argument to find replacement parts in AdventureWorks2022 inventory

```sql
USE AdventureWorks2025;
GO

SELECT [ProductDescriptionID],
       [Description],
       SUBSTRING([Description], LEN('Replacement') + 1) AS [Replacement-Part]
FROM [Production].[ProductDescription]
WHERE [Description] LIKE 'Replacement%';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

| ProductDescriptionID | Description | Replacement-Part |
| --- | --- | --- |
| 686 | Replacement mountain wheel for entry-level rider. | mountain wheel for entry-level rider. |
| 687 | Replacement mountain wheel for the casual to serious rider. | mountain wheel for the casual to serious rider. |
| 689 | Replacement road front wheel for entry-level cyclist. | road front wheel for entry-level cyclist. |
| 867 | Replacement rear mountain wheel for entry-level rider. | rear mountain wheel for entry-level rider. |
| 868 | Replacement rear mountain wheel for the casual to serious rider. | rear mountain wheel for the casual to serious rider. |
| 870 | Replacement rear wheel for entry-level cyclist. | rear wheel for entry-level cyclist. |
| 1981 | Replacement mountain wheel for entry-level rider. | mountain wheel for entry-level rider. |
| 1987 | Replacement mountain wheel for the casual to serious rider. | mountain wheel for the casual to serious rider. |
| 1999 | Replacement road rear wheel for entry-level cyclist. | road rear wheel for entry-level cyclist. |

## Related content

- [LEFT (Transact-SQL)](left-transact-sql.md)
- [LTRIM (Transact-SQL)](ltrim-transact-sql.md)
- [RIGHT (Transact-SQL)](right-transact-sql.md)
- [RTRIM (Transact-SQL)](rtrim-transact-sql.md)
- [STRING_SPLIT (Transact-SQL)](string-split-transact-sql.md)
- [TRIM (Transact-SQL)](trim-transact-sql.md)

