---
title: "sp_validname (Transact-SQL)"
description: Checks for valid SQL Server identifier names.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_validname"
  - "sp_validname_TSQL"
helpviewer_keywords:
  - "sp_validname"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_validname (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Checks for valid [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] identifier names. All nonbinary and nonzero data, including Unicode data that can be stored by using the **nchar**, **nvarchar**, or **ntext** data types, are accepted as valid characters for identifier names.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_validname
    [ @name = ] N'name'
    [ , [ @raise_error = ] raise_error ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the [identifiers](../databases/database-identifiers.md) for which to check validity. *@name* is **sysname**, with no default. *@name* can't be `NULL`, can't be an empty string, and can't contain a binary-zero character.

#### [ @raise_error = ] *raise_error*

Specifies whether to raise an error. *@raise_error* is **bit**, with a default of `1`, which means that errors are displayed. `0` causes no error messages to appear.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires membership in the **public** role.

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [NCHAR (Transact-SQL)](../../t-sql/functions/nchar-transact-sql.md)
- [nchar and nvarchar (Transact-SQL)](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md)
- [ntext, text, and image (Transact-SQL)](../../t-sql/data-types/ntext-text-and-image-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
