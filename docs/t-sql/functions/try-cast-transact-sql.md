---
title: "TRY_CAST (Transact-SQL)"
description: "Returns a value cast to the specified data type if the cast succeeds; otherwise, returns null."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 06/06/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "TRY_CAST_TSQL"
  - "TRY_CAST"
helpviewer_keywords:
  - "TRY_CAST function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2016 || >= sql-server-linux-2017 || >= aps-pdw-2016 || = azure-sqldw-latest || =fabric"
---
# TRY_CAST (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns a value cast to the specified data type if the cast succeeds; otherwise, returns null.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
TRY_CAST ( expression AS data_type [ ( length ) ] )
```

## Arguments

#### *expression*

Specifies any valid expression to be cast.

#### *data_type*

The data type into which to cast *expression*.

#### *length*

Optional integer that specifies the length of the target data type.

The range of acceptable values is determined by the value of *data_type*.

## Return types

Returns a value cast to the specified data type if the cast succeeds; otherwise, returns null.

## Remarks

`TRY_CAST` takes the value passed to it and tries to convert it to the specified *data_type*. If the cast succeeds, `TRY_CAST` returns the value as the specified *data_type*; if an error occurs, null is returned. However if you request a conversion that is explicitly not permitted, then `TRY_CAST` fails with an error.

`TRY_CAST` isn't a new reserved keyword and is available in all compatibility levels. `TRY_CAST` has the same semantics as `TRY_CONVERT` when connecting to remote servers.

`TRY_CAST` doesn't work for **varchar(max)** if the length is over 8000.

## Examples

### A. TRY_CAST returns null

The following example demonstrates that `TRY_CAST` returns null when the cast fails.

```sql
SELECT
CASE WHEN TRY_CAST('test' AS FLOAT) IS NULL
     THEN 'Cast failed'
     ELSE 'Cast succeeded'
END AS Result;
GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
Result
------------
Cast failed
  
(1 row(s) affected)
```

The following example demonstrates that the expression must be in the expected format.

```sql
SET DATEFORMAT dmy;

SELECT TRY_CAST('12/31/2022' AS DATETIME2) AS Result;
GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
Result
----------------------
NULL
  
(1 row(s) affected)
```

### B. TRY_CAST fails with an error

The following example demonstrates that `TRY_CAST` returns an error when the cast is explicitly not permitted.

```sql
SELECT TRY_CAST(4 AS XML) AS Result;
GO
```

The result of this statement is an error, because an integer can't be cast into an **xml** data type.

```output
Explicit conversion from data type int to xml is not allowed.
```

### C. TRY_CAST succeeds

This example demonstrates that the expression must be in the expected format.

```sql
SET DATEFORMAT mdy;

SELECT TRY_CAST('12/31/2022' AS DATETIME2) AS Result;
GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
Result
----------------------------------
2022-12-31 00:00:00.0000000
  
(1 row(s) affected)
```

## See also

- [TRY_CONVERT (Transact-SQL)](../../t-sql/functions/try-convert-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](../../t-sql/functions/cast-and-convert-transact-sql.md)
