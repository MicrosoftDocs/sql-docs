---
title: "CONCAT (Transact-SQL)"
description: This function returns a string resulting from the concatenation, or joining, of two or more string values in an end-to-end manner.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/22/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CONCAT"
  - "CONCAT_TSQL"
helpviewer_keywords:
  - "CONCAT function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# CONCAT (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

This function returns a string resulting from the concatenation, or joining, of two or more string values in an end-to-end manner.

> [!NOTE]  
> To add a separating value during concatenation, use [CONCAT_WS](concat-ws-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CONCAT ( argument1 , argument2 [ , argumentN ] ... )
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *argument1*, *argument2* [ , *argumentN* ]

An expression of any string value. The `CONCAT` function requires at least two arguments, and no more than 254 arguments.

## Return types

A string value whose length and type depend on the input.

## Remarks

`CONCAT` takes a variable number of string arguments and concatenates (or joins) them into a single string. It requires a minimum of two input values; otherwise, `CONCAT` raises an error. `CONCAT` implicitly converts all arguments to string types before concatenation. `CONCAT` implicitly converts null values to empty strings. If `CONCAT` receives arguments with all `NULL` values, it returns an empty string of type **varchar(1)**. The implicit conversion to strings follows the existing rules for data type conversions. For more information about data type conversions, see [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md).

The return type depends on the type of the arguments. This table illustrates the mapping:

| Input type | Output type and length |
| --- | --- |
| 1. Any argument of a SQL-CLR system type, a SQL-CLR UDT, or **nvarchar(max)** | **nvarchar(max)** |
| 2. Otherwise, any argument of type **varbinary(max)** or **varchar(max)** | **varchar(max)**, unless one of the parameters is an **nvarchar** of any length. In this case, `CONCAT` returns a result of type **nvarchar(max)**. |
| 3. Otherwise, any argument of type **nvarchar** of up to 4000 characters (**nvarchar(*<= 4000*)**) | **nvarchar(*<= 4000*)** |
| 4. In all other cases | any argument of type **varchar** of up to 8000 characters (**varchar(*<= 8000*)**), unless one of the parameters is an **nvarchar** of any length. In that case, `CONCAT` returns a result of type **nvarchar(max)**. |

When `CONCAT` receives **nvarchar** input arguments of length <= 4000 characters, or **varchar** input arguments of length <= 8000 characters, implicit conversions can affect the length of the result. Other data types have different lengths when implicitly converted to strings. For example, an **int** with value `14` has a string length of 2, while a **float** with value `1234.56789` has a string length of 7 (`1234.57`). Therefore, a concatenation of these two values returns a result with a length of no less than 9 characters.

If none of the input arguments has a supported large object (LOB) type, then the return type truncates to 8,000 characters in length, regardless of the return type. This truncation preserves space and supports plan generation efficiency.

`CONCAT` can be executed remotely on a linked server running [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions. For older linked servers, the `CONCAT` operation will happen locally, after the linked server returns the non-concatenated values.

## Examples

### A. Use CONCAT

```sql
SELECT CONCAT ('Happy ', 'Birthday ', 11, '/', '25') AS Result;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Result
--------------------
Happy Birthday 11/25
```

### B. Use CONCAT with NULL values

```sql
CREATE TABLE #temp (
    emp_name NVARCHAR(200) NOT NULL,
    emp_middlename NVARCHAR(200) NULL,
    emp_lastname NVARCHAR(200) NOT NULL
    );

INSERT INTO #temp
VALUES ('Name', NULL, 'Lastname');

SELECT CONCAT (emp_name, emp_middlename, emp_lastname) AS Result
FROM #temp;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Result
------------
NameLastname
```

## Related content

- [CONCAT_WS (Transact-SQL)](concat-ws-transact-sql.md)
- [FORMATMESSAGE (Transact-SQL)](formatmessage-transact-sql.md)
- [QUOTENAME (Transact-SQL)](quotename-transact-sql.md)
- [REPLACE (Transact-SQL)](replace-transact-sql.md)
- [REVERSE (Transact-SQL)](reverse-transact-sql.md)
- [STRING_AGG (Transact-SQL)](string-agg-transact-sql.md)
- [STRING_ESCAPE (Transact-SQL)](string-escape-transact-sql.md)
- [STUFF (Transact-SQL)](stuff-transact-sql.md)
- [TRANSLATE (Transact-SQL)](translate-transact-sql.md)
- [String Functions (Transact-SQL)](string-functions-transact-sql.md)
