---
title: "money and smallmoney (Transact-SQL)"
description: money and smallmoney are data types that represent monetary or currency values.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/21/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "money_TSQL"
  - "money"
  - "smallmoney"
  - "smallmoney_TSQL"
helpviewer_keywords:
  - "money data type, about money data type"
  - "money data type"
  - "smallmoney data type"
  - "values [SQL Server], monetary"
  - "currency [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# money and smallmoney (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Data types that represent monetary or currency values.

## Remarks

| Data type | Range | Storage |
| --- | --- | --- |
| **money** | -922,337,203,685,477.5808 to 922,337,203,685,477.5807 (-922,337,203,685,477.58<br />to 922,337,203,685,477.58 for Informatica. Informatica only supports two decimals, not four.) | 8 bytes |
| **smallmoney** | -214,748.3648 to 214,748.3647 | 4 bytes |

The **money** and **smallmoney** data types are accurate to a ten-thousandth of the monetary units that they represent. For Informatica, the **money** and **smallmoney** data types are accurate to a one-hundredth of the monetary units that they represent.

Use a period to separate partial monetary units, like cents, from whole monetary units. For example, `2.15` specifies 2 dollars and 15 cents.

These data types can use any one of the following currency symbols.

| Symbol | Currency | Hexadecimal value |
| --- | --- | --- |
| &#x0024; | Dollar sign  | `0024` |
| &#x00A2; | Cent sign  | `00A2` |
| &#x00A3; | Pound sign  | `00A3` |
| &#x00A4; | Currency sign  | `00A4` |
| &#x00A5; | Yen sign  | `00A5` |
| &#x09F2; | Bengali Rupee mark  | `09F2` |
| &#x09F3; | Bengali Rupee sign  | `09F3` |
| &#x0E3F; | Thai Baht currency symbol  | `0E3F` |
| &#x17DB; | Khmer Riel currency symbol  | `17DB` |
| &#x20A0; | Euro currency sign  | `20A0` |
| &#x20A1; | Colon sign  | `20A1` |
| &#x20A2; | Cruzeiro sign  | `20A2` |
| &#x20A3; | French Franc sign  | `20A3` |
| &#x20A4; | Lira sign  | `20A4` |
| &#x20A5; | Mill sign  | `20A5` |
| &#x20A6; | Naira sign  | `20A6` |
| &#x20A7; | Peseta sign  | `20A7` |
| &#x20A8; | Rupee sign  | `20A8` |
| &#x20A9; | Won sign  | `20A9` |
| &#x20AA; | New Sheqel sign  | `20AA` |
| &#x20AB; | Dong sign  | `20AB` |
| &#x20AC; | Euro sign  | `20AC` |
| &#x20AD; | Kip sign  | `20AD` |
| &#x20AE; | Tugrik sign  | `20AE` |
| &#x20AF; | Drachma sign  | `20AF` |
| &#x20B0; | German Penny sign  | `20B0` |
| &#x20B1; | Peso sign  | `20B1` |
| &#xFDFC; | Rial sign  | `FDFC` |
| &#xFE69; | Small Dollar sign  | `FE69` |
| &#xFF04; | Full-width Dollar sign  | `FF04` |
| &#xFFE0; | Full-width Cent sign  | `FFE0` |
| &#xFFE1; | Full-width Pound sign  | `FFE1` |
| &#xFFE5; | Full-width Yen sign  | `FFE5` |
| &#xFFE6; | Full-width Won sign  | `FFE6` |

You don't need to enclose currency or monetary data in single quotation marks (`'`). While you can specify monetary values preceded by a currency symbol, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't store any currency information associated with the symbol, it only stores the numeric value.

> [!WARNING]  
> You can experience rounding errors through truncation, when storing monetary values as **money** and **smallmoney**. Avoid using this data type if your money or currency values are used in calculations. Instead, use the **[decimal](decimal-and-numeric-transact-sql.md)** data type with at least four decimal places.

## Convert money data

When you convert to **money** from integer data types, units are assumed to be in monetary units. For example, the integer value of `4` is converted to the **money** equivalent of 4 monetary units.

The following example converts **smallmoney** and **money** values to **varchar** and **decimal** data types, respectively.

```sql
DECLARE @mymoney_sm SMALLMONEY = 3148.29,
    @mymoney MONEY = 3148.29;

SELECT CAST(@mymoney_sm AS VARCHAR(20)) AS 'SM_MONEY VARCHAR(20)',
    CAST(@mymoney AS DECIMAL) AS 'MONEY DECIMAL';
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)] Because the **decimal** type in the example doesn't have a [scale](decimal-and-numeric-transact-sql.md#s-scale), the value is truncated.

```output
SM_MONEY VARCHAR(20)           MONEY DECIMAL
------------------------------ ----------------------
3148.29                        3148
```

## Related content

- [ALTER TABLE (Transact-SQL)](../statements/alter-table-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../statements/create-table-transact-sql.md)
- [Data types (Transact-SQL)](data-types-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](../language-elements/declare-local-variable-transact-sql.md)
- [SET @local_variable (Transact-SQL)](../language-elements/set-local-variable-transact-sql.md)
- [sys.types (Transact-SQL)](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)
