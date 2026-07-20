---
title: "^= (Bitwise Exclusive OR Assignment) (Transact-SQL)"
description: Performs a bitwise logical exclusive OR operation between two integer values, and sets a value to the result of the operation.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "^="
  - "^=_TSQL"
helpviewer_keywords:
  - "^= (bitwise exclusive OR equals)"
  - "compound operators, ^="
  - "assignment operators, ^="
  - "augmented operators, ^="
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# ^= (Bitwise exclusive OR assignment) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Performs a bitwise logical exclusive `OR` operation between two integer values, and sets a value to the result of the operation.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression ^= expression
```

## Arguments

#### *expression*

Any valid [expression](expressions-transact-sql.md) of any one of the data types in the numeric category, except the **bit** data type.

## Return types

Returns the data type of the argument with the higher precedence. For more information, see [Data type precedence](../data-types/data-type-precedence-transact-sql.md).

## Remarks

For more information, see [^ (Bitwise exclusive OR)](bitwise-exclusive-or-transact-sql.md).

## Related content

- [Compound operators (Transact-SQL)](compound-operators-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [Bitwise operators (Transact-SQL)](bitwise-operators-transact-sql.md)
