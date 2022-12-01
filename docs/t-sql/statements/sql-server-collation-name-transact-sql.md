---
title: "SQL Server Collation Name (Transact-SQL)"
description: SQL Server Collation Name (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "02/21/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "collations [SQL Server], SQL collations"
  - "SQL collations"
  - "names [SQL Server], collations"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Collation Name (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Is a single string that specifies the collation name for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports Windows collations. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also supports a limited number (<80) of collations called [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations which were developed before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supported Windows collations. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations are still supported for backward compatibility, but should not be used for new development work. For more information about Windows collations, see [Windows Collation Name](../../t-sql/statements/windows-collation-name-transact-sql.md).

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
<SQL_collation_name> :: =
SQL_SortRules[_Pref]_CPCodepage_<ComparisonStyle>

<ComparisonStyle> ::=
_CaseSensitivity_AccentSensitivity | _BIN
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*SortRules*
A string identifying the alphabet or language whose sorting rules are applied when dictionary sorting is specified. Examples are Latin1_General or Polish.

**Pref**
Specifies uppercase preference. Even if comparison is case-insensitive, the uppercase version of a letter sorts before the lowercase version, when there is no other distinction.

*Codepage*
Specifies a one- to four-digit number that identifies the code page used by the collation. **CP1** specifies code page 1252, for all other code pages the complete code page number is specified. For example, **CP1251** specifies code page 1251 and **CP850** specifies code page 850.

*CaseSensitivity*
**CI** specifies case-insensitive, **CS** specifies case-sensitive.

*AccentSensitivity*
**AI** specifies accent-insensitive, **AS** specifies accent-sensitive.

**BIN**
Specifies the binary sort order to be used.

## Remarks

To list the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations supported by your server, execute the following query.

```sql
SELECT * FROM sys.fn_helpcollations()
WHERE name LIKE 'SQL%';
```

> [!NOTE]
> For Sort Order ID 80, use any of the Window collations with the code page of 1250, and binary order. For example: Albanian_BIN, Croatian_BIN, Czech_BIN, Romanian_BIN, Slovak_BIN, Slovenian_BIN.

## See Also

- [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md)
- [Constants](../../t-sql/data-types/constants-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md)
- [DECLARE @local_variable](../../t-sql/language-elements/declare-local-variable-transact-sql.md)
- [table](../../t-sql/data-types/table-transact-sql.md)
- [sys.fn_helpcollations](../../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md)
