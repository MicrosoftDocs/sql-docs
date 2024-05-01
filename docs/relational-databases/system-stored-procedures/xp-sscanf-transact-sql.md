---
title: "xp_sscanf (Transact-SQL)"
description: "Reads data from the string into the argument locations specified by each format argument."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "xp_sscanf_TSQL"
  - "xp_sscanf"
helpviewer_keywords:
  - "xp_sscanf"
dev_langs:
  - "TSQL"
---
# xp_sscanf (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reads data from the string into the argument locations specified by each format argument.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
xp_sscanf { 'string' OUTPUT , 'format' } [ , 'argument' [ , ...n ] ]
```

## Arguments

#### '*string*' OUTPUT

The character string to read the argument values from.

When `OUTPUT`is specified, *string* puts the value of *argument* in the output parameter.

#### '*format*'

A formatted character string, similar to the values supported by the C-language `sscanf` function. Currently, only the `%s` format argument is supported.

#### '*argument*'

A **varchar** variable set to the value of the corresponding *format* argument.

#### *n*

A placeholder that indicates that a maximum of 50 arguments can be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

`xp_sscanf` returns the following message:

```output
Command(s) completed successfully.
```

## Permissions

Requires membership in the **public** role.

## Examples

The following example uses `xp_sscanf` to extract two values from a source string based on their positions in the format of the source string.

```sql
DECLARE @filename VARCHAR(20),
    @message VARCHAR(20);

EXEC xp_sscanf 'sync -b -fproducts10.tmp -rrandom',
    'sync -b -f%s -r%s',
    @filename OUTPUT,
    @message OUTPUT;

SELECT @filename, @message;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
(No column name) (No column name)
---------------- --------------------
products10.tmp   random
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [General extended stored procedures (Transact-SQL)](general-extended-stored-procedures-transact-sql.md)
- [xp_sprintf (Transact-SQL)](xp-sprintf-transact-sql.md)
