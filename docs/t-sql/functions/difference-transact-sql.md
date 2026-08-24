---
title: "DIFFERENCE (Transact-SQL)"
description: DIFFERENCE returns an integer value measuring the difference between the SOUNDEX values of two different character expressions.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/21/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "DIFFERENCE"
  - "DIFFERENCE_TSQL"
helpviewer_keywords:
  - "DIFFERENCE function"
  - "comparing SOUNDEX values"
  - "SOUNDEX values"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# DIFFERENCE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

This function returns an integer value measuring the difference between the [SOUNDEX()](soundex-transact-sql.md) values of two different character expressions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DIFFERENCE ( character_expression , character_expression )
```

## Arguments

#### *character_expression*

An alphanumeric [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data. *character_expression* can be a constant, variable, or column.

## Return types

**int**

## Remarks

`DIFFERENCE` compares two different `SOUNDEX` values, and returns an integer value. This value measures the degree that the `SOUNDEX` values match, on a scale of `0` to `4`. A value of `0` indicates weak or no similarity between the `SOUNDEX` values; `4` indicates strongly similar, or even identically matching, `SOUNDEX` values.

`DIFFERENCE` and `SOUNDEX` have collation sensitivity.

## Examples

The first part of this example compares the `SOUNDEX` values of two very similar strings. For a `Latin1_General` collation, `DIFFERENCE` returns a value of `4`. The second part of the example compares the `SOUNDEX` values for two very different strings, and for a `Latin1_General` collation, `DIFFERENCE` returns a value of `0`.

### A. Return a DIFFERENCE value of 4, the least possible difference

```sql
SELECT SOUNDEX('Green'),
       SOUNDEX('Greene'),
       DIFFERENCE('Green', 'Greene');
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
----- ----- -----------
G650  G650  4
```

### B. Return a DIFFERENCE value of 0, the highest possible difference

```sql
SELECT SOUNDEX('Blotchet-Halls'),
       SOUNDEX('Greene'),
       DIFFERENCE('Blotchet-Halls', 'Greene');
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
----- ----- -----------
B432  G650  0
```

## Related content

- [SOUNDEX (Transact-SQL)](soundex-transact-sql.md)
