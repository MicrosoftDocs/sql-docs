---
title: "TRY_CAST (Transact-SQL)"
description: TRY_CAST returns a value cast to the specified data type if the cast succeeds; otherwise, returns null.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 12/08/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "TRY_CAST_TSQL"
  - "TRY_CAST"
helpviewer_keywords:
  - "TRY_CAST function"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || >=aps-pdw-2016 || =azure-sqldw-latest || =fabric || =fabric-sqldb"
---
# TRY_CAST (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

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

The data type into which to cast *expression*. This value determines the range of acceptable values.

#### *length*

Optional integer that specifies the length of the target data type.

## Return types

Returns a value cast to the specified data type if the cast succeeds; otherwise, returns null.

## Remarks

`TRY_CAST` takes the value passed to it and tries to convert it to the specified *data_type*. If the cast succeeds, `TRY_CAST` returns the value as the specified *data_type*; if an error occurs, null is returned. However if you request a conversion that is explicitly not permitted, then `TRY_CAST` fails with an error.

`TRY_CAST` isn't a new reserved keyword and is available in all compatibility levels. `TRY_CAST` has the same semantics as `TRY_CONVERT` when connecting to remote servers.

`TRY_CAST` doesn't work for an *expression* in the following cases:

- **varchar(max)** if the length is over 8,000
- **nvarchar(max)** if the length is over 4,000

## Examples

### A. TRY_CAST returns NULL

- The following example demonstrates that `TRY_CAST` returns null when the cast fails.

  ```sql
  SELECT
  CASE WHEN TRY_CAST('test' AS FLOAT) IS NULL
       THEN 'Cast failed'
       ELSE 'Cast succeeded'
  END AS Result;
  GO
  ```

  This query returns a result of `Cast failed`.

- The following example demonstrates that the expression must be in the expected format.

  ```sql
  SET DATEFORMAT dmy;
  
  SELECT TRY_CAST('12/31/2022' AS DATETIME2) AS Result;
  GO
  ```

  This query returns a result of `NULL`.

### B. TRY_CAST fails with an error

The following example demonstrates that `TRY_CAST` returns an error when the cast is explicitly not permitted.

```sql
SELECT TRY_CAST(4 AS XML) AS Result;
GO
```

The result of this statement is an error, because an integer can't be cast into the **xml** data type.

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

This query returns a result of `2022-12-31 00:00:00.0000000`.

## Related content

- [TRY_CONVERT (Transact-SQL)](try-convert-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
