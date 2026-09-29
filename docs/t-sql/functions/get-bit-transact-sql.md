---
title: "GET_BIT (Transact-SQL)"
description: "Transact-SQL reference for the GET_BIT function."
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 08/25/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "GET_BIT"
  - "GET_BIT_TSQL"
helpviewer_keywords:
  - "bit manipulation [SQL Server], get bit"
  - "GET_BIT function"
  - "bit shifting [SQL Server], get bit"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-mi-current || =azuresqldb-current || =fabric || =fabric-sqldb"
---
# GET_BIT (Transact-SQL)

[!INCLUDE [SQL Server 2022, SQL Database, SQL Managed Instance FabricSE FabricDW FabricSQLDB](../../includes/applies-to-version/sqlserver2022-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

`GET_BIT` takes two parameters and returns the bit in *expression_value* that is in the offset defined by *bit_offset*.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
GET_BIT ( expression_value , bit_offset )
```

## Arguments

#### *expression_value*

Any integer or binary expression that isn't a large object ([LOB](#remarks)).

#### *bit_offset*

Any integer.

## Return types

**bit**

The *bit_offset* parameter in `GET_BIT` is used to identify the *n*th bit of the data to get or set. In integer types, the `0`th bit is the least significant bit. In binary types, the `0`th bit is the least significant bit in the rightmost byte.

`GET_BIT` will throw an error if *bit_offset* is negative or greater than the last bit in the data type.

## Remarks

The `GET_BIT` function isn't available in SQL Server 2019 or earlier versions.

Distributed Query functionality for the bit manipulation functions within linked server or ad hoc queries (`OPENQUERY`) aren't supported.

Large object (LOB) data types in the Database Engine can store data that exceeds 8,000 bytes. These data types store data on a [row-overflow](../../relational-databases/pages-and-extents-architecture-guide.md#large-row-support) data page. A LOB also encompasses data types that store data on dedicated LOB page structures, which use a text or an image pointer of in-row references to LOB data pages. For more information about data storage, see the [Page and extent architecture guide](../../relational-databases/pages-and-extents-architecture-guide.md).

The bit manipulation functions operate on the **tinyint**, **smallint**, **int**, **bigint**, **binary(*n*)**, and **varbinary(*n*)** data types. Large object (LOB) data types, such as **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **image**, **ntext**, **text**, **xml**, and common language runtime (CLR) BLOB types, aren't supported.

## Examples

In this example, the second and fourth bits are returned.

```sql
SELECT GET_BIT(0xabcdef, 2) AS Get_2nd_Bit,
       GET_BIT(0xabcdef, 4) AS Get_4th_Bit;
```

The results are as follows:

| Get_2nd_Bit | Get_4th_Bit |
| --- | --- |
| 1 | 0 |

> [!NOTE]  
> `0xabcdef` in binary is 1010 1011 1100 1101 111**0** 1**1**11. The second and fourth bits are highlighted.

## Related content

- [SET_BIT (Transact-SQL)](set-bit-transact-sql.md)
- [LEFT_SHIFT (Transact-SQL)](left-shift-transact-sql.md)
- [RIGHT_SHIFT (Transact-SQL)](right-shift-transact-sql.md)
- [BIT_COUNT (Transact-SQL)](bit-count-transact-sql.md)
- [Bit manipulation functions](bit-manipulation-functions-overview.md)
