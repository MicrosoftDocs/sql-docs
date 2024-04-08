---
title: "Constants (Transact-SQL)"
description: "Constants (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 04/04/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
helpviewer_keywords:
  - "uniqueidentifier data type"
  - "bit data type"
  - "constants [SQL Server]"
  - "scalar values"
  - "money data type, constants"
  - "binary [SQL Server], constants"
  - "float data type"
  - "Unicode [SQL Server], constants"
  - "constants [Transact-SQL]"
  - "integer constants"
  - "decimal data type, constants"
  - "character strings [SQL Server], constants"
  - "positive values [SQL Server]"
  - "formats [SQL Server], constants"
  - "datetime data type [SQL Server]"
  - "literals [SQL Server], constants"
  - "real data type"
  - "negative values"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Constants (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

A constant, also known as a literal or a scalar value, is a symbol that represents a specific data value. The format of a constant depends on the data type of the value it represents.

> [!NOTE]
> The term *constant* in application development and the concept of a *constant* in Transact-SQL (T-SQL) are not the same. There is no specific way to set a global static value in T-SQL. Constants in T-SQL are the equivalent of string literal values.

## Character string constants

Character string constants are enclosed in single quotation marks and include alphanumeric characters (`a`-`z`, `A`-`Z`, and `0`-`9`) and special characters, such as exclamation point (`!`), at sign (`@`), and number sign (`#`). Character string constants are assigned the default collation of the current database. If the COLLATE clause is used, the conversion to the database default code page still happens before the conversion to the collation specified by the COLLATE clause. Character strings typed by users are evaluated through the code page of the computer and are translated to the database default code page if it is required.

> [!NOTE]  
> When a [UTF8-enabled collation](../../relational-databases/collations/collation-and-unicode-support.md#utf8) is specified using the COLLATE clause, conversion to the database default code page still happens before the conversion to the collation specified by the COLLATE clause. Conversion is not done directly to the specified Unicode-enabled collation. For more information, see [Unicode string](#unicode-strings).

If the QUOTED_IDENTIFIER option has been set OFF for a connection, character strings can also be enclosed in double quotation marks, but the Microsoft [OLE DB Driver for SQL Server](../../connect/oledb/oledb-driver-for-sql-server.md) and [ODBC Driver for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md) automatically use `SET QUOTED_IDENTIFIER ON`. We recommend using single quotation marks.

If a character string enclosed in single quotation marks contains an embedded quotation mark, represent the embedded single quotation mark with two single quotation marks. This isn't required in strings embedded in double quotation marks.

The following are examples of character strings:

```text
'Cincinnati'
'O''Brien'
'Process X is 50% complete.'
'The level for job_id: %d should be between %d and %d.'
"O'Brien"
```

Empty strings are represented as two single quotation marks with nothing in between. In 6.x compatibility mode, an empty string is treated as a single space.

Character string constants support enhanced [collations](../../relational-databases/collations/collation-and-unicode-support.md).

> [!NOTE]  
> Character constants greater than 8000 bytes are typed as **varchar(max)** data.

## Unicode strings

Unicode strings have a format similar to character strings, but are prefixed with an `N` identifier (N stands for National Language in the SQL-92 standard).

> [!IMPORTANT]  
> The `N` prefix must be uppercase.

For example, `'Michél'` is a character constant while `N'Michél'` is a Unicode constant. Unicode constants are interpreted as Unicode data, and aren't evaluated by using a code page. Unicode constants do have a collation. This collation primarily controls comparisons and case sensitivity. Unicode constants are assigned the default collation of the current database. If the COLLATE clause is used, the conversion to the database default collation still happens before the conversion to the collation specified by the COLLATE clause. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md#storage_differences).

Unicode string constants support enhanced collations.

> [!NOTE]  
> Unicode constants greater than 8000 bytes are typed as **nvarchar(max)** data.

## Binary constants

Binary constants have the prefix `0x` and are a string of hexadecimal numbers. They aren't enclosed in quotation marks.

The following are examples of binary strings are:

```text
0xAE
0x12Ef
0x69048AEFDD010E
0x  (empty binary string)
```

> [!NOTE]  
> Binary constants greater than 8000 bytes are typed as **varbinary(max)** data.

## `bit` constants

**bit** constants are represented by the numbers `0` or `1`, and aren't enclosed in quotation marks. If a number larger than `1` is used, it is converted to `1`.

## `datetime` constants

**datetime** constants are represented by using character date values in specific formats, enclosed in single quotation marks.

The following are examples of **datetime** constants:

```text
'December 5, 1985'
'5 December, 1985'
'851205'
'12/5/98'
```

Examples of datetime constants are:

```text
'14:30:24'
'04:24 PM'
```

## `integer` constants

**integer** constants are represented by a string of numbers that aren't enclosed in quotation marks and don't contain decimal points. **integer** constants must be whole numbers; they can't contain decimals.

The following are examples of **integer** constants:

```text
1894
2
```

## `decimal` constants

**decimal** constants are represented by a string of numbers that aren't enclosed in quotation marks and contain a decimal point.

The following are examples of **decimal** constants:

```text
1894.1204
2.0
```

## `float` and `real` constants

**float** and **real** constants are represented by using scientific notation.

The following are examples of **float** or **real** values:

```text
101.5E5
0.5E-2
```

## `money` constants

**money** constants are represented as string of numbers with an optional decimal point and an optional currency symbol as a prefix. **money** constants aren't enclosed in quotation marks.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't enforce any kind of grouping rules such as inserting a comma (`,`) every three digits in strings that represent money.

> [!NOTE]  
> Commas are ignored anywhere in a string literal that is cast to the **money** data type.

The following are examples of **money** constants:

```text
$12
$542023.14
$-23
```

## `uniqueidentifier` constants

**uniqueidentifier** constants are a string representing a GUID. They can be specified in either a character or binary string format.

The following examples both specify the same GUID:

```text
'6F9619FF-8B86-D011-B42D-00C04FC964FF'
0xff19966f868b11d0b42d00c04fc964ff
```

## Specify negative and positive numbers

To indicate whether a number is positive or negative, apply the `+` or `-` unary operators to a numeric constant. This creates a numeric expression that represents the signed numeric value. Numeric constants use positive when the `+` or `-` unary operators aren't applied.

Signed **integer** expressions:

```text
+145345234
-2147483648
```

Signed **decimal** expressions:

```text
+145345234.2234
-2147483648.10
```

Signed **float** expressions:

```text
+123E-3
-12E5
```

Signed **money** expressions:

```text
-$45.56
+$423456.99
```

## Enhanced collations

The [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] supports character and Unicode string constants that support enhanced collations. For more information, see the [COLLATE (Transact-SQL)](../../t-sql/statements/collations.md) clause.

## Related content

- [Data Types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [Expressions (Transact-SQL)](../../t-sql/language-elements/expressions-transact-sql.md)
- [Operators (Transact-SQL)](../../t-sql/language-elements/operators-transact-sql.md)
- [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)
- [Collation Precedence](../../t-sql/statements/collation-precedence-transact-sql.md)
