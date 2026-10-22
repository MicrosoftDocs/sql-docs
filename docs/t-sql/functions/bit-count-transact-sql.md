---
title: BIT_COUNT (Transact-SQL)
description: Transact-SQL reference for the BIT_COUNT function.
author: uc-msft
ms.author: umajay
ms.reviewer: derekw
ms.date: 06/23/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "BIT_COUNT"
  - "BIT_COUNT_TSQL"
helpviewer_keywords:
  - "bit manipulation [SQL Server], bit count"
  - "BIT_COUNT function"
  - "bit shifting [SQL Server], bit count"
dev_langs:
  - TSQL
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-mi-current || =azuresqldb-current || =fabric || =fabric-sqldb"
---
# BIT_COUNT (Transact-SQL)

[!INCLUDE [SQL Server 2022, SQL Database, SQL Managed Instance FabricSE FabricDW FabricSQLDB](../../includes/applies-to-version/sqlserver2022-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

`BIT_COUNT` takes one parameter and returns the number of bits set to 1 in that parameter as a **bigint** type.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
BIT_COUNT ( expression_value )
```

## Arguments

#### *expression_value*

Any integer or binary expression that isn't a large object ([LOB](#remarks)).

## Return types

**bigint**

`BIT_COUNT` doesn't cast its input before counting bits. Because the binary representation of the same number depends on its data type, the result depends on the input type.

For example, the following queries return `16` and `32`, respectively:

```sql
SELECT BIT_COUNT(CAST (-1 AS SMALLINT)); -- Returns 16
SELECT BIT_COUNT(CAST (-1 AS INT)); -- Returns 32
```

`BIT_COUNT` returns `NULL` when *expression_value* is a typed `NULL` of a supported data type. You must cast an untyped `NULL` literal to a supported type before passing it in:

```sql
SELECT BIT_COUNT(CAST (NULL AS INT)); -- Returns NULL
```

## Remarks

Distributed query functionality for the bit manipulation functions within linked server or ad hoc queries (`OPENQUERY`) aren't supported.

Large object (LOB) data types in the Database Engine can store data that exceeds 8,000 bytes. These data types store data on a [row-overflow](../../relational-databases/pages-and-extents-architecture-guide.md#large-row-support) data page. A LOB also encompasses data types that store data on dedicated LOB page structures, which use a text or an image pointer of in-row references to LOB data pages. For more information about data storage, see the [Page and extent architecture guide](../../relational-databases/pages-and-extents-architecture-guide.md).

The bit manipulation functions operate on the **tinyint**, **smallint**, **int**, **bigint**, **binary(*n*)**, and **varbinary(*n*)** data types. These functions don't support large object (LOB) data types, such as **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **image**, **ntext**, **text**, **xml**, and common language runtime (CLR) BLOB types.

## Examples

### A. Calculate the BIT_COUNT in a binary value

The following example calculates the number of bits set to `1` in a binary value.

```sql
SELECT BIT_COUNT(0xABCDEF) AS Count;
```

The result is `17`. This result occurs because `0xABCDEF` in binary is `1010 1011 1100 1101 1110 1111`, which has 17 bits set to `1`.

### B. Calculate the BIT_COUNT in an integer

The following example calculates the number of bits set to `1` in an integer.

```sql
SELECT BIT_COUNT(17) AS Count;
```

The result is `2`. This result occurs because `17` in binary is `0001 0001`, which has only two bits set to `1`.

## Related content

- [LEFT_SHIFT (Transact-SQL)](left-shift-transact-sql.md)
- [RIGHT_SHIFT (Transact-SQL)](right-shift-transact-sql.md)
- [SET_BIT (Transact-SQL)](set-bit-transact-sql.md)
- [GET_BIT (Transact-SQL)](get-bit-transact-sql.md)
- [Bit manipulation functions](bit-manipulation-functions-overview.md)
