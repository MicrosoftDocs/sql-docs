---
title: QUOTENAME (Transact-SQL)
description: QUOTENAME returns a Unicode string with the delimiters added to make the input string a valid SQL Engine delimited identifier.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 04/08/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "QUOTENAME_TSQL"
  - "QUOTENAME"
helpviewer_keywords:
  - "delimited identifiers [SQL Server]"
  - "input strings [SQL Server]"
  - "Unicode [SQL Server], delimited identifiers"
  - "QUOTENAME function"
  - "valid identifiers [SQL Server]"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# QUOTENAME (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns a Unicode string with the delimiters added to make the input string a valid [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [delimited identifier](../../relational-databases/databases/database-identifiers.md#rules-for-delimited-identifiers).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
QUOTENAME ( 'character_string' [ , 'quote_character' ] )
```

## Arguments

#### '*character_string*'

A string of Unicode character data. *character_string* is **sysname** and is limited to 128 characters. Inputs greater than 128 characters return `NULL`.

#### '*quote_character*'

A one-character string to use as the delimiter. Can be a single quotation mark (`'`), a left or right bracket (`[` or `]`), a double quotation mark (`"`), a left or right parenthesis (`(` or `)`), a greater than or less than sign (`>` or `<`), a left or right brace (`{` or `}`) or a backtick (`\``).

If you provide an unacceptable quote character, `NULL` is returned. If *quote_character* isn't specified, brackets are used.

## Return types

**nvarchar(258)**

## Examples

The following example takes the character string `abc[]def` and uses the `[` and `]` characters to create a valid [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] delimited identifier.

```sql
SELECT QUOTENAME('abc[]def');
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
[abc[]]def]
```

The right bracket in the string `abc[]def` is doubled to indicate an escape character.

The following example prepares a quoted string to use in naming a column.

```sql
DECLARE @columnName AS NVARCHAR (255) = 'user''s "custom" name';

DECLARE @sql AS NVARCHAR (MAX) = 'SELECT FirstName AS ' + QUOTENAME(@columnName) + ' FROM dbo.DimCustomer';

EXECUTE sp_executesql @sql;
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

The following example takes the character string `abc def` and uses the `[` and `]` characters to create a valid [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] delimited identifier.

```sql
SELECT QUOTENAME('abc def');
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
[abc def]
```

## Related content

- [Database identifiers](../../relational-databases/databases/database-identifiers.md)
- [PARSENAME (Transact-SQL)](parsename-transact-sql.md)
- [CONCAT (Transact-SQL)](concat-transact-sql.md)
- [CONCAT_WS (Transact-SQL)](concat-ws-transact-sql.md)
- [FORMATMESSAGE (Transact-SQL)](formatmessage-transact-sql.md)
- [REPLACE (Transact-SQL)](replace-transact-sql.md)
- [REVERSE (Transact-SQL)](reverse-transact-sql.md)
- [STRING_AGG (Transact-SQL)](string-agg-transact-sql.md)
- [STRING_ESCAPE (Transact-SQL)](string-escape-transact-sql.md)
- [STUFF (Transact-SQL)](stuff-transact-sql.md)
- [TRANSLATE (Transact-SQL)](translate-transact-sql.md)
