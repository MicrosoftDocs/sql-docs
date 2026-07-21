---
title: "SET TEXTSIZE (Transact-SQL)"
description: Specifies the size, in bytes, of various data types returned to the client by a SELECT statement.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 04/17/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "TEXTSIZE_TSQL"
  - "TEXTSIZE"
  - "SET_TEXTSIZE_TSQL"
  - "SET TEXTSIZE"
helpviewer_keywords:
  - "SET TEXTSIZE statement"
  - "SELECT statement [SQL Server], text size returned"
  - "size [SQL Server], text and image data"
  - "TEXTSIZE option"
  - "text size returned [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# SET TEXTSIZE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Specifies the size, in bytes, of **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **text**, **ntext**, and **image** data returned to the client by a `SELECT` statement.

> [!IMPORTANT]  
> **ntext**, **text**, and **image** data types will be removed in a future version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these data types in new development work, and plan to modify applications that currently use them. Use **nvarchar(max)**, **varchar(max)**, and **varbinary(max)** instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET TEXTSIZE { number }
```

## Arguments

#### *number*

The length of **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **text**, **ntext**, or **image** data, in bytes. *number* is an integer with a maximum value of `2147483647` (2 GB). A value of `-1` indicates unlimited size. A value of `0` resets the size to the default value of 4 KB.

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client (10.0 and higher) and ODBC Driver for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically specify `-1` (unlimited) when connecting.

## Remarks

Setting `SET TEXTSIZE` affects the `@@TEXTSIZE` function.

The setting of set `TEXTSIZE` is set at execute or run time and not at parse time.

For more information, see [Manage Transact-SQL job steps](/ssms/agent/manage-job-steps#transact-sql-job-steps).

## Permissions

Requires membership in the **public** role.

## Related content

- [&#x40;&#x40;TEXTSIZE (Transact-SQL)](../functions/textsize-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [SET Statements (Transact-SQL)](set-statements-transact-sql.md)

