---
title: "nchar and nvarchar (Transact-SQL)"
description: "Unicode character data types that are either fixed-size (nchar), or variable-size (nvarchar)."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/22/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
helpviewer_keywords:
  - "nvarchar data type"
  - "nchar data type"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# nchar and nvarchar (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Character data types that are either fixed-size, **nchar**, or variable-size, **nvarchar**. Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], when a [Supplementary Character (SC)](../../relational-databases/collations/collation-and-unicode-support.md#Supplementary_Characters) enabled collation is used, these data types store the full range of [Unicode](../../relational-databases/collations/collation-and-unicode-support.md#Unicode_Defn) character data and use the [UTF-16](https://www.wikipedia.org/wiki/UTF-16) character encoding. If a non-SC collation is specified, then these data types store only the subset of character data supported by the [UCS-2](https://www.wikipedia.org/wiki/Universal_Coded_Character_Set#Encoding_forms) character encoding.

## Arguments

#### nchar [ ( *n* ) ]

Fixed-size string data. *n* defines the string size in byte-pairs, and must be a value from 1 through 4,000. The storage size is two times *n* bytes. For [UCS-2](https://www.wikipedia.org/wiki/UTF-16#U+0000_to_U+D7FF_and_U+E000_to_U+FFFF) encoding, the storage size is two times *n* bytes and the number of characters that can be stored is also *n*. For UTF-16 encoding, the storage size is still two times *n* bytes, but the number of characters that can be stored may be smaller than *n* because Supplementary Characters use two byte-pairs (also called [surrogate-pair](https://www.wikipedia.org/wiki/UTF-16#Code_points_from_U+010000_to_U+10FFFF)). The ISO synonyms for **nchar** are **national char** and **national character**.

#### nvarchar [ ( *n* | max ) ]

Variable-size string data. *n* defines the string size in byte-pairs, and can be a value from 1 through 4,000. **max** indicates that the maximum storage size is 2^30-1 characters (2 GB). The storage size is two times *n* bytes + 2 bytes. For [UCS-2](https://www.wikipedia.org/wiki/UTF-16#U+0000_to_U+D7FF_and_U+E000_to_U+FFFF) encoding, the storage size is two times *n* bytes + 2 bytes and the number of characters that can be stored is also *n*. For UTF-16 encoding, the storage size is still two times *n* bytes + 2 bytes, but the number of characters that can be stored may be smaller than *n* because Supplementary Characters use two byte-pairs (also called [surrogate-pair](https://www.wikipedia.org/wiki/UTF-16#Code_points_from_U+010000_to_U+10FFFF)). The ISO synonyms for **nvarchar** are **national char varying** and **national character varying**.

## Remarks

A common misconception is to think that with **nchar(*n*)** and **nvarchar(*n*)**, the *n* defines the number of characters. However, in **nchar(*n*)** and **nvarchar(*n*)**, the *n* defines the string length in **byte-pairs** (0-4,000). *n* never defines numbers of characters that can be stored. This is similar to the definition of [**char(*n*)** and **varchar(*n*)**](../../t-sql/data-types/char-and-varchar-transact-sql.md).

The misconception happens because when using characters defined in the Unicode range 0 to 65,535, one character can be stored per each byte-pair. However, in higher Unicode ranges (65,536 to 1,114,111) one character may use two byte-pairs. For example, in a column defined as **nchar(10)**, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] can store 10 characters that use one byte-pair (Unicode range 0 to 65,535), but fewer than 10 characters when using two byte-pairs (Unicode range 65,536 to 1,114,111). For more information about Unicode storage and character ranges, see [Storage differences between UTF-8 and UTF-16](../../relational-databases/collations/collation-and-unicode-support.md#storage_differences).

When *n* isn't specified in a data definition or variable declaration statement, the default length is 1. When *n* isn't specified with the CAST function, the default length is 30.

If you use **nchar** or **nvarchar**, we recommend that you:

- Use **nchar** when the sizes of the column data entries are consistent.
- Use **nvarchar** when the sizes of the column data entries vary considerably.
- Use **nvarchar(max)** when the sizes of the column data entries vary considerably, and the string length might exceed 4,000 byte-pairs.

**sysname** is a system-supplied user-defined data type that is functionally equivalent to **nvarchar(128)**, except that it isn't nullable. **sysname** is used to reference database object names.

Objects that use **nchar** or **nvarchar** are assigned the default collation of the database unless a specific collation is assigned using the `COLLATE` clause.

`SET ANSI_PADDING` is always `ON` for **nchar** and **nvarchar**. `SET ANSI_PADDING OFF` doesn't apply to the **nchar** or **nvarchar** data types.

Prefix a Unicode character string constants with the letter `N` to signal UCS-2 or UTF-16 input, depending on whether an SC collation is used or not. Without the `N` prefix, the string is converted to the default code page of the database that may not recognize certain characters. Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], when a UTF-8 enabled collation is used, the default code page is capable of storing the Unicode UTF-8 character set.

When prefixing a string constant with the letter `N`, the implicit conversion will result in a UCS-2 or UTF-16 string if the constant to convert doesn't exceed the max length for the **nvarchar** string data type (4,000). Otherwise, the implicit conversion will result in a large-value **nvarchar(max)**.

> [!WARNING]  
> Each non-null **varchar(max)** or **nvarchar(max)** column requires 24 bytes of additional fixed allocation, which counts against the 8,060-byte row limit during a sort operation. These additional bytes can create an implicit limit to the number of non-null **varchar(max)** or **nvarchar(max)** columns in a table. No special error is provided when the table is created (beyond the usual warning that the maximum row size exceeds the allowed maximum of 8,060 bytes) or at the time of data insertion. This large row size can cause errors (such as error 512) that users may not anticipate during some normal operations.  Two examples of operations are a clustered index key update, or sorts of the full column set.

## Convert character data

For information about converting character data, see [char and varchar (Transact-SQL)](../../t-sql/data-types/char-and-varchar-transact-sql.md).

## See also

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](../../t-sql/functions/cast-and-convert-transact-sql.md)
- [COLLATE (Transact-SQL)](../statements/collations.md)
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [Data Types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](../../t-sql/language-elements/declare-local-variable-transact-sql.md)
- [LIKE (Transact-SQL)](../../t-sql/language-elements/like-transact-sql.md)
- [SET ANSI_PADDING (Transact-SQL)](../../t-sql/statements/set-ansi-padding-transact-sql.md)
- [SET @local_variable (Transact-SQL)](../../t-sql/language-elements/set-local-variable-transact-sql.md)
- [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)
- [Single-Byte and Multibyte Character Sets](/cpp/c-runtime-library/single-byte-and-multibyte-character-sets)
