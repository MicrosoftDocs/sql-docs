---
title: "binary and varbinary (Transact-SQL)"
description: "Binary data types of either fixed length or variable length."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/22/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "binary_TSQL"
  - "varbinary_TSQL"
  - "binary"
  - "varbinary"
helpviewer_keywords:
  - "varbinary data type"
  - "binary [SQL Server], about binary data type"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# binary and varbinary (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Binary data types of either fixed length or variable length.

## Arguments

#### binary [ ( *n* ) ]

Fixed-length binary data with a length of *n* bytes, where *n* is a value from 1 through 8,000. The storage size is *n* bytes.

#### varbinary [ ( *n* | max ) ]

Variable-length binary data. *n* can be a value from 1 through 8,000. **max** indicates that the maximum storage size is 2^31-1 bytes. The storage size is the actual length of the data entered + 2 bytes. The data that is entered can be 0 bytes in length. The ANSI SQL synonym for **varbinary** is **binary varying**.

## Remarks

The default length is 1 when *n* isn't specified in a data definition or variable declaration statement. When *n* isn't specified with the `CAST` function, the default length is 30.

| Data type | Use when ... |
| --- | --- |
| **binary** | the sizes of the column data entries are consistent.|
| **varbinary** | the sizes of the column data entries vary considerably.|
| **varbinary(max)** | the column data entries exceed 8,000 bytes.|

## Convert binary and varbinary data

When converting data from a string data type to a **binary** or **varbinary** data type of unequal length, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] pads or truncates the data on the right. These string data types are:

- **char**
- **varchar**
- **nchar**
- **nvarchar**
- **binary**
- **varbinary**
- **text**
- **ntext**
- **image**

When other data types are converted to **binary** or **varbinary**, the data is padded or truncated on the left. Padding is achieved by using hexadecimal zeros.

Converting data to the **binary** and **varbinary** data types is useful if **binary** data is the easiest way to move around data. At some point, you might convert a value type to a binary value of large enough size and then convert it back. This conversion always results in the same value if both conversions are taking place on the same version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The binary representation of a value might change from version to version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

You can convert **int**, **smallint**, and **tinyint** to **binary** or **varbinary**. If you convert the **binary** value back to an integer value, this value will be different from the original integer value if truncation has occurred. For example, the following SELECT statement shows that the integer value `123456` is stored as a binary `0x0001e240`:

```sql
SELECT CAST( 123456 AS BINARY(4) );
```

However, the following `SELECT` statement shows that if the **binary** target is too small to hold the entire value, the leading digits are silently truncated so that the same number is stored as `0xe240`:

```sql
SELECT CAST( 123456 AS BINARY(2) );
```

The following batch shows that this silent truncation can affect arithmetic operations without raising an error:

```sql
DECLARE @BinaryVariable2 BINARY(2);
  
SET @BinaryVariable2 = 123456;
SET @BinaryVariable2 = @BinaryVariable2 + 1;
  
SELECT CAST( @BinaryVariable2 AS INT);
GO
```

The final result is `57921`, not `123457`.

> [!NOTE]  
> Conversions between any data type and the **binary** data types are not guaranteed to be the same between versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## See also

- [CAST and CONVERT (Transact-SQL)](../../t-sql/functions/cast-and-convert-transact-sql.md)
- [Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)
- [Data Types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
