---
title: "sys.syslanguages (Transact-SQL)"
description: sys.syslanguages contains one row for each language present in the instance of the SQL Server Database Engine.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/19/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.syslanguages"
  - "sys.syslanguages_TSQL"
  - "syslanguages"
  - "syslanguages_TSQL"
helpviewer_keywords:
  - "syslanguages system table"
  - "sys.syslanguages compatibility view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sys.syslanguages (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Contains one row for each language present in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

| Column name | Data type | Description |
| --- | --- | --- |
| `langid` | **smallint** | Unique language ID. |
| `dateformat` | **nchar(3)** | Date order, for example, `DMY`. |
| `datefirst` | **tinyint** | First day of the week: `1` for Monday, `2` for Tuesday, and so on, through `7` for Sunday. |
| `upgrade` | **int** | Reserved for system use. |
| `name` | **sysname** | Official language name, for example, `Français`. |
| `alias` | **sysname** | Alternative language name, for example, `French`. |
| `months` | **nvarchar(372)** | Comma-separated list of full-length month names in order from January through December, with each name having up to 20 characters. |
| `shortmonths` | **nvarchar(132)** | Comma-separated list of short-month names in order from January through December, with each name having up to nine characters. |
| `days` | **nvarchar(217)** | Comma-separated list of day names in order from Monday through Sunday, with each name having up to 30 characters. |
| `lcid` | **int** | [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows language code ID (LCID) for the locale. |
| `msglangid` | **smallint** | [!INCLUDE [ssDE](../../includes/ssde-md.md)] message group ID. |

The [!INCLUDE [ssDE](../../includes/ssde-md.md)] contains the following installed languages.

| Name in English | Windows LCID | [!INCLUDE [ssDE](../../includes/ssde-md.md)] message group ID |
| --- | --- | --- |
| English | 1033 | 1033 |
| German | 1031 | 1031 |
| French | 1036 | 1036 |
| Japanese | 1041 | 1041 |
| Danish | 1030 | 1030 |
| Spanish | 3082 | 3082 |
| Italian | 1040 | 1040 |
| Dutch | 1043 | 1043 |
| Norwegian | 2068 | 2068 |
| Portuguese | 2070 | 2070 |
| Finnish | 1035 | 1035 |
| Swedish | 1053 | 1053 |
| Czech | 1029 | 1029 |
| Hungarian | 1038 | 1038 |
| Polish | 1045 | 1045 |
| Romanian | 1048 | 1048 |
| Croatian | 1050 | 1050 |
| Slovak | 1051 | 1051 |
| Slovenian | 1060 | 1060 |
| Greek | 1032 | 1032 |
| Bulgarian | 1026 | 1026 |
| Russian | 1049 | 1049 |
| Turkish | 1055 | 1055 |
| British English | 2057 | 1033 |
| Estonian | 1061 | 1061 |
| Latvian | 1062 | 1062 |
| Lithuanian | 1063 | 1063 |
| Brazilian | 1046 | 1046 |
| Traditional Chinese | 1028 | 1028 |
| Korean | 1042 | 1042 |
| Simplified Chinese | 2052 | 2052 |
| Arabic | 1025 | 1025 |
| Thai | 1054 | 1054 |
| Bokmål | 1044 | 1044 |

## Related content

- [System Compatibility Views (Transact-SQL)](system-compatibility-views-transact-sql.md)
- [Mapping System Tables to System Views (Transact-SQL)](../system-tables/mapping-system-tables-to-system-views-transact-sql.md)
