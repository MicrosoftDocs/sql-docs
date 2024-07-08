---
title: "Windows collation name (Transact-SQL)"
description: The Windows collation name is composed of the collation designator and the comparison styles, and is specified in the COLLATE clause in SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/08/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "Windows collations [SQL Server]"
  - "names [SQL Server], collations"
  - "collations [SQL Server], Windows collations"
  - "Collation Designator"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# Windows collation name (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Specifies the Windows collation name in the `COLLATE` clause in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The Windows collation name is composed of the collation designator and the comparison styles.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
<Windows_collation_name> ::=
<CollationDesignator>_<ComparisonStyle>

<ComparisonStyle> ::=
{ <CaseSensitivity>_<AccentSensitivity> [ _<KanatypeSensitive> ] [ _<WidthSensitive> ] [ _<VariationSelectorSensitive> ]
}
| { _UTF8 }
| { _BIN | _BIN2 }
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *CollationDesignator*

Specifies the base collation rules used by the Windows collation. The base collation rules cover:

- The sorting and comparison rules that are applied when dictionary sorting is specified. Sorting rules are based on alphabet or language.
- The code page used to store **varchar** data.

Some examples are:

- `Latin1_General` or `French`: both use code page `1252`.
- `Turkish`: uses code page `1254`.

#### *CaseSensitivity*

`CI` specifies case-insensitive, `CS` specifies case-sensitive.

#### *AccentSensitivity*

`AI` specifies accent-insensitive, `AS` specifies accent-sensitive.

#### *KanatypeSensitive*

Omitting this option specifies kanatype-insensitive, `KS` specifies kanatype-sensitive.

#### *WidthSensitivity*

Omitting this option specifies width-insensitive, `WS` specifies width-sensitive.

#### *VariationSelectorSensitivity*

**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions

Omitting this option specifies variation selector-insensitive, `VSS` specifies variation selector-sensitive.

#### UTF8

**Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions

Specifies UTF-8 encoding to be used for eligible data types. For more information, see [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md).

#### { BIN | BIN2 }

- `BIN` specifies the backward-compatible binary sort order to be used.
- `BIN2` specifies the binary sort order that uses code-point comparison semantics.

## Remarks

Depending on the version of the collation, some code points might not have sort weights, or uppercase/lowercase mappings defined. For example, compare the output of the `LOWER` function when it's given the same character, but in different versions of the same collation:

```sql
SELECT NCHAR(504) COLLATE Latin1_General_CI_AS AS [Uppercase],
       NCHAR(505) COLLATE Latin1_General_CI_AS AS [Lowercase];
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| Column name | Result |
| --- | --- |
| `Uppercase` | `Ǹ` |
| `Lowercase` | `ǹ` |

The first statement shows both uppercase and lowercase forms of this character in the older collation (collation doesn't affect the availability of characters when working with Unicode data).

```sql
SELECT LOWER(NCHAR(504) COLLATE Latin1_General_CI_AS) AS [Version80Collation],
       LOWER(NCHAR(504) COLLATE Latin1_General_100_CI_AS) AS [Version100Collation];
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| Column name | Result |
| --- | --- |
| `Version80Collation` | `Ǹ` |
| `Version100Collation` | `ǹ` |

The second statement shows that an uppercase character is returned when the collation is `Latin1_General_CI_AS`, because this code point doesn't have a lowercase mapping defined in that collation.

When working with some languages, it can be critical to avoid the older collations. For example, this is true for Telegu.

In some cases, Windows collations and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] collations can generate different query plans for the same query.

## Examples

The following table describes some examples of Windows collation names.

| Collation | Description |
| --- | --- |
| `Latin1_General_100_CI_AS` | Collation uses the Latin1 General dictionary sorting rules and maps to code page `1252`. It's a version `_100` collation, and is case-insensitive (`CI`) and accent-sensitive (`AS`). |
| `Estonian_CS_AS` | Collation uses the Estonian dictionary sorting rules and maps to code page `1257`. It's a version `_80` collation (implied by no version number in the name), and is case-sensitive (`CS`) and accent-sensitive (`AS`). |
| `Japanese_Bushu_Kakusu_140_BIN2` | Collation uses binary code point sorting rules and maps to code page `932`. It's a version `_140` collation, and the Japanese Bushu Kakusu dictionary sorting rules are ignored. |

## Windows collations

To list the Windows collations supported by your instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], execute the following query.

```sql
SELECT *
FROM sys.fn_helpcollations()
WHERE [name] NOT LIKE N'SQL%';
```

## Related content

- [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md)
- [ALTER TABLE (Transact-SQL)](alter-table-transact-sql.md)
- [Constants (Transact-SQL)](../data-types/constants-transact-sql.md)
- [CREATE DATABASE](create-database-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](create-table-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](../language-elements/declare-local-variable-transact-sql.md)
- [Table (Transact-SQL)](../data-types/table-transact-sql.md)
- [sys.fn_helpcollations](../../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md)
