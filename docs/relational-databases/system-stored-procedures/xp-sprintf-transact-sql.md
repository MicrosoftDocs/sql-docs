---
title: "xp_sprintf (Transact-SQL)"
description: "Formats and stores a series of characters and values in the string output parameter."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "xp_sprintf_TSQL"
  - "xp_sprintf"
helpviewer_keywords:
  - "xp_sprintf"
dev_langs:
  - "TSQL"
---
# xp_sprintf (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Formats and stores a series of characters and values in the string output parameter. Each format argument is replaced with the corresponding argument.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
xp_sprintf { 'string' OUTPUT , 'format' }
     [ , 'argument' [ , ...n ] ]
```

## Arguments

#### '*string*' OUTPUT

A **varchar** variable that receives the output.

When `OUTPUT` is specified, this option puts the value of the variable in the output parameter.

#### '*format*'

A format character string with placeholders for *argument* values, similar to the values supported by the C-language `sprintf` function. Currently, only the `%s` format argument is supported.

#### '*argument*'

A character string that represents the value of the corresponding format argument.

#### *n*

A placeholder that indicates that a maximum of 50 arguments can be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

`xp_sprintf` returns the following message:

```output
The command(s) completed successfully.
```

## Permissions

Requires membership in the **public** role.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [General extended stored procedures (Transact-SQL)](general-extended-stored-procedures-transact-sql.md)
- [xp_sscanf (Transact-SQL)](xp-sscanf-transact-sql.md)
