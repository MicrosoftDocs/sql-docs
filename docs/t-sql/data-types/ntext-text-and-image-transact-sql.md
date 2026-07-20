---
title: "ntext, text, and image (Transact-SQL)"
description: "The ntext, text, and image data types are deprecated data types for storing large non-Unicode and Unicode character and binary data."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 03/27/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "ntext_TSQL"
  - "ntext"
helpviewer_keywords:
  - "text data type, about text data type"
  - "text [SQL Server], data types"
  - "ntext data type"
  - "ntext data type, about ntext data type"
  - "image data type, about image data type"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# ntext, text, and image (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Fixed and variable-length data types for storing large non-Unicode and Unicode character and binary data. Unicode data uses the Unicode UCS-2 character set.

> [!IMPORTANT]  
> The **ntext**, **text**, and **image** data types will be removed in a future version of SQL Server. Avoid using these data types in new development work, and plan to modify applications that currently use them. Use [**nvarchar(max)**](nchar-and-nvarchar-transact-sql.md), [**varchar(max)**](char-and-varchar-transact-sql.md), and [**varbinary(max)**](binary-and-varbinary-transact-sql.md) instead.

## Arguments

#### ntext

Variable-length Unicode data with a maximum string length of 2^30 - 1 (1,073,741,823). Storage size, in bytes, is two times the string length that is entered. The ISO synonym for **ntext** is **national text**.

#### text

Variable-length non-Unicode data in the code page of the server and with a maximum string length of 2^31 - 1 (2,147,483,647). When the server code page uses double-byte characters, the storage is still 2,147,483,647 bytes. Depending on the character string, the storage size might be less than 2,147,483,647 bytes.

#### image

Variable-length binary data from 0 through 2^31-1 (2,147,483,647) bytes.

## Remarks

The following functions and statements can be used with **ntext**, **text**, or **image** data.

| Functions | Statements |
| --- | --- |
| [DATALENGTH](../functions/datalength-transact-sql.md) | [READTEXT](../queries/readtext-transact-sql.md) |
| [PATINDEX](../functions/patindex-transact-sql.md) | [SET TEXTSIZE](../statements/set-textsize-transact-sql.md) |
| [SUBSTRING](../functions/substring-transact-sql.md) | [UPDATETEXT](../queries/updatetext-transact-sql.md) |
| [Text and Image Functions - TEXTPTR](../functions/text-and-image-functions-textptr-transact-sql.md) | [WRITETEXT](../queries/writetext-transact-sql.md) |
| [Text and Image Functions - TEXTVALID](../functions/text-and-image-functions-textvalid-transact-sql.md) | |

When you drop columns using the deprecated **ntext** data type, the cleanup of the deleted data occurs as a serialized operation on all rows. The cleanup can require a large amount of time. When you drop an **ntext** column in a table with lots of rows, update the **ntext** column to `NULL` value first, then drop the column. You can run this option with parallel operations and make it much faster.

## Related content

- [Data types (Transact-SQL)](data-types-transact-sql.md)
- [LIKE (Transact-SQL)](../language-elements/like-transact-sql.md)
- [SET @local_variable (Transact-SQL)](../language-elements/set-local-variable-transact-sql.md)
- [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md)
- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
- [Data type conversion (Database Engine)](data-type-conversion-database-engine.md)
- [ALTER TABLE (Transact-SQL)](../statements/alter-table-transact-sql.md)
