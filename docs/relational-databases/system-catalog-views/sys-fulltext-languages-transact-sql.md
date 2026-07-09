---
title: sys.fulltext_languages (Transact-SQL)
description: sys.fulltext_languages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.fulltext_languages_TSQL"
  - "sys.fulltext_languages"
  - "fulltext_languages_TSQL"
  - "fulltext_languages"
helpviewer_keywords:
  - "sys.fulltext_languages catalog view"
  - "languages [full-text search]"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.fulltext_languages (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This catalog view contains one row per language whose word breakers are registered with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Each row displays the LCID and name of the language.

When word breakers are registered for a language, its other linguistic resources (such as stemmers, noise words or *stopwords*, and thesaurus files) become available to full-text indexing/querying operations. The value of `name` or `lcid` can be specified in the full-text queries and full-text index [!INCLUDE [tsql](../../includes/tsql-md.md)] statements.

| Column name | Data type | Description |
| --- | --- | --- |
| `lcid` | **int** | Windows locale identifier (LCID) for the language. |
| `name` | **sysname** | Is either the value of the alias in [sys.syslanguages](../system-compatibility-views/sys-syslanguages-transact-sql.md) corresponding to the value of `lcid` or the string representation of the numeric LCID. |

## Values returned for default languages

The following table shows values for the languages whose word breakers are registered by default.

| Language | LCID |
| --- | --- |
| Arabic | 1025 |
| Bangla <sup>1</sup> | 2117 |
| Bengali (India) | 1093 |
| Bokmål | 1044 |
| Brazilian <sup>1</sup> | 1046 |
| British English | 2057 |
| Bulgarian | 1026 |
| Catalan | 1027 |
| Chinese (Hong Kong SAR, PRC) | 3076 |
| Chinese (Macao SAR) | 5124 |
| Chinese (Singapore) | 4100 |
| Croatian | 1050 |
| Czech | 1029 |
| Danish | 1030 |
| Dutch | 1043 |
| English | 1033 |
| Estonian <sup>1</sup> | 1061 |
| Finnish <sup>1</sup> | 1035 |
| French | 1036 |
| German | 1031 |
| Greek <sup>1</sup> | 1032 |
| Gujarati | 1095 |
| Hebrew | 1037 |
| Hindi | 1081 |
| Hungarian <sup>1</sup> | 1038 |
| Icelandic | 1039 |
| Indonesian | 1057 |
| Italian | 1040 |
| Japanese | 1041 |
| Kannada | 1099 |
| Korean | 1042 |
| Latvian | 1062 |
| Lithuanian | 1063 |
| Malay - Malaysia | 1086 |
| Malayalam | 1100 |
| Marathi | 1102 |
| Neutral | 0 |
| Norwegian <sup>1</sup> | 2068 |
| Polish <sup>1</sup> | 1045 |
| Portuguese (Brazil) | 1046 |
| Portuguese (Portugal) | 2070 |
| Punjabi | 1094 |
| Romanian | 1048 |
| Russian | 1049 |
| Serbian (Cyrillic) | 3098 |
| Serbian (Latin) | 2074 |
| Simplified Chinese | 2052 |
| Slovak | 1051 |
| Slovenian | 1060 |
| Spanish | 3082 |
| Swedish | 1053 |
| Tamil | 1097 |
| Telugu | 1098 |
| Thai | 1054 |
| Traditional Chinese | 1028 |
| Turkish <sup>1</sup> | 1055 |
| Ukrainian | 1058 |
| Urdu | 1056 |
| Vietnamese | 1066 |

<sup>1</sup> **Applies to**: [!INCLUDE [ssSQL25](../../includes/sssql25-md.md)] and later versions.

## Remarks

To update the list of languages registered with full-text search, use [sp_fulltext_service](../system-stored-procedures/sp-fulltext-service-transact-sql.md) with the `update_languages` option.

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)]

## Related content

- [sp_fulltext_load_thesaurus_file (Transact-SQL)](../system-stored-procedures/sp-fulltext-load-thesaurus-file-transact-sql.md)
- [sp_fulltext_service (Transact-SQL)](../system-stored-procedures/sp-fulltext-service-transact-sql.md)
- [Configure and manage word breakers and stemmers](../search/configure-and-manage-word-breakers-and-stemmers-for-search.md)
- [Configure and Manage Thesaurus Files for Full-Text Search](../search/configure-and-manage-thesaurus-files-for-full-text-search.md)
- [Configure and Manage Stopwords and Stoplists for Full-Text Search](../search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)
- [Upgrade Full-Text Search (SQL Server Search)](../search/upgrade-full-text-search.md)
