---
title: "TRY_PARSE (Transact-SQL)"
description: TRY_PARSE returns the result of an expression, translated to the requested data type, or NULL if the cast fails.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/08/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "TRY_PARSE_TSQL"
  - "TRY_PARSE"
helpviewer_keywords:
  - "TRY_PARSE function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azure-sqldw-latest || =fabric || =fabric-sqldb"
---
# TRY_PARSE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

Returns the result of an expression, translated to the requested data type, or `NULL` if the cast fails in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Use `TRY_PARSE` only for converting from string to date/time and number types.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
TRY_PARSE ( string_value AS data_type [ USING culture ] )
```

## Arguments

#### *string_value*

**nvarchar(4000)** value representing the formatted value to parse into the specified data type.

*string_value* must be a valid representation of the requested data type, or `TRY_PARSE` returns null.

#### *data_type*

Literal representing the data type requested for the result.

#### *culture*

Optional string that identifies the culture in which *string_value* is formatted.

If the *culture* argument isn't provided, the language of the current session is used. This language is set either implicitly or explicitly by using the `SET LANGUAGE` statement. *culture* accepts any culture supported by the .NET Framework. It isn't limited to the languages explicitly supported by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If the *culture* argument isn't valid, `PARSE` raises an error.

## Return types

Returns the result of the expression, translated to the requested data type, or `NULL` if the cast fails.

## Remarks

Use `TRY_PARSE` only for converting from string to date/time and number types. For general type conversions, continue to use `CAST` or `CONVERT`. Keep in mind that there's a certain performance overhead in parsing the string value.

`TRY_PARSE` relies on the presence of the .NET Framework common language runtime (CLR).

This function can't be remoted since it depends on the presence of the CLR. Remoting a function that requires the CLR would cause an error on the remote server.

### More information about the `data_type` parameter

The values for the *data_type* parameter are restricted to the types shown in the following table, together with styles. The style information is provided to help determine what types of patterns are allowed. For more information on styles, see the .NET Framework documentation for the `System.Globalization.NumberStyles` and `DateTimeStyles` enumerations.

| Category | Type | .NET type | Styles used |
| --- | --- | --- | --- |
| Numeric | **bigint** | `Int64` | `NumberStyles.Number` |
| Numeric | **int** | `Int32` | `NumberStyles.Number` |
| Numeric | **smallint** | `Int16` | `NumberStyles.Number` |
| Numeric | **tinyint** | `Byte` | `NumberStyles.Number` |
| Numeric | **decimal** | `Decimal` | `NumberStyles.Number` |
| Numeric | **numeric** | `Decimal` | `NumberStyles.Number` |
| Numeric | **float** | `Double` | `NumberStyles.Float` |
| Numeric | **real** | `Single` | `NumberStyles.Float` |
| Numeric | **smallmoney** | `Decimal` | `NumberStyles.Currency` |
| Numeric | **money** | `Decimal` | `NumberStyles.Currency` |
| Date and Time | **date** | `DateTime` | `DateTimeStyles.AllowWhiteSpaces` &#124; `DateTimeStyles.AssumeUniversal` |
| Date and Time | **time** | `TimeSpan` | `DateTimeStyles.AllowWhiteSpaces` &#124; `DateTimeStyles.AssumeUniversal` |
| Date and Time | **datetime** | `DateTime` | `DateTimeStyles.AllowWhiteSpaces` &#124; `DateTimeStyles.AssumeUniversal` |
| Date and Time | **smalldatetime** | `DateTime` | `DateTimeStyles.AllowWhiteSpaces` &#124; `DateTimeStyles.AssumeUniversal` |
| Date and Time | **datetime2** | `DateTime` | `DateTimeStyles.AllowWhiteSpaces` &#124; `DateTimeStyles.AssumeUniversal` |
| Date and Time | **datetimeoffset** | `DateTimeOffset` | `DateTimeStyles.AllowWhiteSpaces` &#124; `DateTimeStyles.AssumeUniversal` |

### More information about the `culture` parameter

The following table shows the mappings from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] languages to .NET Framework cultures.

| Full name | Alias | LCID | Specific culture |
| --- | --- | --- | --- |
| us_english | English | 1033 | `en-US` |
| Deutsch | German | 1031 | `de-DE` |
| Français | French | 1036 | `fr-FR` |
| 日本語 | Japanese | 1041 | `ja-JP` |
| Dansk | Danish | 1030 | `da-DK` |
| Español | Spanish | 3082 | `es-ES` |
| Italiano | Italian | 1040 | `it-IT` |
| Nederlands | Dutch | 1043 | `nl-NL` |
| Norsk | Norwegian | 2068 | `nn-NO` |
| Português | Portuguese | 2070 | `pt-PT` |
| Suomi | Finnish | 1035 | `fi-FI` |
| Svenska | Swedish | 1053 | `sv-SE` |
| čeština | Czech | 1029 | `Cs-CZ` |
| magyar | Hungarian | 1038 | `Hu-HU` |
| polski | Polish | 1045 | `Pl-PL` |
| română | Romanian | 1048 | `Ro-RO` |
| hrvatski | Croatian | 1050 | `hr-HR` |
| slovenčina | Slovak | 1051 | `Sk-SK` |
| slovenski | Slovenian | 1060 | `Sl-SI` |
| ελληνικά | Greek | 1032 | `El-GR` |
| български | Bulgarian | 1026 | `bg-BG` |
| русский | Russian | 1049 | `Ru-RU` |
| Türkçe | Turkish | 1055 | `Tr-TR` |
| British | British English | 2057 | `en-GB` |
| eesti | Estonian | 1061 | `Et-EE` |
| latviešu | Latvian | 1062 | `lv-LV` |
| lietuvių | Lithuanian | 1063 | `lt-LT` |
| Português (Brasil) | Brazilian | 1046 | `pt-BR` |
| 繁體中文 | Traditional Chinese | 1028 | `zh-TW` |
| 한국어 | Korean | 1042 | `Ko-KR` |
| 简体中文 | Simplified Chinese | 2052 | `zh-CN` |
| Arabic | Arabic | 1025 | `ar-SA` |
| ไทย | Thai | 1054 | `Th-TH` |

## Examples

### A. Basic example of TRY_PARSE

```sql
SELECT TRY_PARSE ('Jabberwokkie' AS DATETIME2 USING 'en-US') AS Result;
```

This query returns a result of `NULL`.

### B. Detect nulls with TRY_PARSE

```sql
SELECT
CASE WHEN TRY_PARSE ('Aragorn' AS DECIMAL USING 'sr-Latn-CS') IS NULL
     THEN 'True'
     ELSE 'False'
END AS Result;
```

This query returns a result of `True`.

### C. Use IIF with TRY_PARSE and implicit culture setting

```sql
SET LANGUAGE English;

SELECT IIF (TRY_PARSE ('01/01/2011' AS DATETIME2) IS NULL, 'True', 'False') AS Result;
```

This query returns a result of `False`.

## Related content

- [PARSE (Transact-SQL)](parse-transact-sql.md)
- [Conversion functions (Transact-SQL)](conversion-functions-transact-sql.md)
- [TRY_CONVERT (Transact-SQL)](try-convert-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](cast-and-convert-transact-sql.md)
