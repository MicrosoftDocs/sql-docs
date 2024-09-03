---
title: "TRY_CONVERT (Transact-SQL)"
description: Returns a value cast to the specified data type if the cast succeeds; otherwise, returns NULL.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/23/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "TRY_CONVERT_TSQL"
  - "TRY_CONVERT"
helpviewer_keywords:
  - "TRY_CONVERT function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || >=aps-pdw-2016 || =azure-sqldw-latest || =fabric"
---
# TRY_CONVERT (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns a value cast to the specified data type if the cast succeeds; otherwise, returns `NULL`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
TRY_CONVERT ( data_type [ ( length ) ] , expression [ , style ] )
```

## Arguments

#### *data_type*

The data type into which to cast *expression*.

#### *length*

An optional integer that specifies the length of the target data type, for data types that allow a user specified length. The maximum value for *length* is 8,000 bytes.

#### *expression*

The value to cast.

#### *style*

Optional integer expression that specifies how the `TRY_CONVERT` function is to translate *expression*.

*style* accepts the same values as the *style* parameter of the `CONVERT` function. For more information, see [CAST and CONVERT](cast-and-convert-transact-sql.md).

The value of *data_type* determines the range of acceptable values. If *style* is `NULL`, then `TRY_CONVERT` returns `NULL`.

## Return types

Returns a value cast to the specified data type if the cast succeeds; otherwise, returns `NULL`.

## Remarks

`TRY_CONVERT` takes the value passed to it and tries to convert it to the specified *data_type*. If the cast succeeds, `TRY_CONVERT` returns the value as the specified *data_type*; if an error occurs, `NULL` is returned. However if you request a conversion that is explicitly not permitted, then `TRY_CONVERT` fails with an error.

`TRY_CONVERT` is a reserved keyword, starting with compatibility level `110`.

This function is capable of being remoted to servers that have [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions. It isn't remoted to servers that have a version earlier than [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].

## Examples

### A. TRY_CONVERT returns NULL

The following example demonstrates that `TRY_CONVERT` returns `NULL` when the cast fails.

```sql
SELECT
    CASE WHEN TRY_CONVERT(FLOAT, 'test') IS NULL
    THEN 'Cast failed'
    ELSE 'Cast succeeded'
END AS Result;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Result  
------------  
Cast failed
```

The following example demonstrates that the expression must be in the expected format.

```sql
SET DATEFORMAT dmy;
SELECT TRY_CONVERT(DATETIME2, '12/31/2022') AS Result;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Result  
----------------------  
NULL
```

### B. TRY_CONVERT fails with an error

The following example demonstrates that `TRY_CONVERT` returns an error when the cast is explicitly not permitted.

```sql
SELECT TRY_CONVERT(XML, 4) AS Result;
GO
```

The result of this statement is an error, because an integer can't be cast into an **xml** data type.

```output
Explicit conversion from data type int to xml is not allowed.
```

### C. TRY_CONVERT succeeds

This example demonstrates that the expression must be in the expected format.

```sql
SET DATEFORMAT mdy;
SELECT TRY_CONVERT(DATETIME2, '12/31/2022') AS Result;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Result
----------------------------------
2022-12-31 00:00:00.0000000
```

## Related content

- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
