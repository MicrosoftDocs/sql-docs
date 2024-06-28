---
title: float and real (Transact-SQL)
description: "float and real (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "09/10/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "float"
  - "real_TSQL"
  - "real"
  - "float_TSQL"
helpviewer_keywords:
  - "numeric data type, floating point"
  - "float data type"
  - "floating point data [SQL Server]"
  - "real data type"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---

# float and real (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Approximate-number data types for use with floating point numeric data. Floating point data is approximate; therefore, not all values in the data type range can be represented exactly. The ISO synonym for **real** is **float(24)**.
  
:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax

**float** [ **(**_n_**)** ]
Where *n* is the number of bits that are used to store the mantissa of the **float** number in scientific notation and, therefore, dictates the precision and storage size. If *n* is specified, it must be a value between **1** and **53**. The default value of *n* is **53**.
  
|*n* value|Precision|Storage size|  
|---|---|---|
|**1-24**|7 digits|4 bytes|  
|**25-53**|15 digits|8 bytes|  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] treats *n* as one of two possible values. If **1**<=n<=**24**, *n* is treated as **24**. If **25**<=n<=**53**, *n* is treated as **53**.  
  
The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **float**[**(n)**] data type complies with the ISO standard for all values of *n* from **1** through **53**. The synonym for **double precision** is **float(53)**.

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks  
  
|Data type|Range|Storage|  
|---|---|---|
|**float**|- 1.79E+308 to -2.23E-308, 0 and 2.23E-308 to 1.79E+308|Depends on the value of *n*|  
|**real**|- 3.40E + 38 to -1.18E - 38, 0 and 1.18E - 38 to 3.40E + 38|4 Bytes|  

The float and real data types are known as approximate data types. The behavior of float and real follows the [IEEE 754](https://ieeexplore.ieee.org/document/4610935) specification on approximate numeric data types.

Approximate numeric data types don't store the exact values specified for many numbers; they store a close approximation of the value. For some applications, the tiny difference between the specified value and the stored approximation isn't relevant. For others though, the difference is important. Because of the approximate nature of the float and real data types, don't use these data types when exact numeric behavior is required. Examples of precise numeric values applications include financial or business data, in operations involving rounding, or in equality checks. In those cases, use the integer, decimal, numeric, money, or smallmoney data types.

Avoid using float or real columns in WHERE clause search conditions, especially the = and <> operators. It's best to limit float and real columns to > or < comparisons.

## Converting float and real data  

Values of **float** are truncated when they're converted to any integer type.
  
When you want to convert from **float** or **real** to character data, using the STR string function is typically more useful than CAST( ). The reason is that STR() enables more control over formatting. For more information, see [STR &#40;Transact-SQL&#41;](../../t-sql/functions/str-transact-sql.md) and [Functions &#40;Transact-SQL&#41;](../../t-sql/functions/functions.md).
  
Prior to [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)], conversion of **float** values to **decimal** or **numeric** is restricted to values of precision 17 digits only. Any **float** value less than 5E-18 (when set using either the scientific notation of 5E-18 or the decimal notation of 0.000000000000000005) rounds down to 0. This is no longer a restriction as of [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)].
  
## See also

[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)  
[SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)
  