---
title: "SET LANGUAGE (Transact-SQL)"
description: SET LANGUAGE specifies the language environment for the session.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET_LANGUAGE_TSQL"
  - "SET LANGUAGE"
helpviewer_keywords:
  - "LANGUAGE option"
  - "languages [SQL Server], setting language"
  - "SET LANGUAGE statement"
  - "options [SQL Server], date"
  - "default languages"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# SET LANGUAGE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw.md)]

Specifies the language environment for the session. The session language determines the **datetime** formats and system messages.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET LANGUAGE { [ N ] 'language' | @language_var }
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### [N]'*language*' | @*language_var*

The name of the language as stored in [sys.syslanguages](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md). This argument can be either Unicode or DBCS converted to Unicode. To specify a language in Unicode, use `N'<language>'`. If specified as a variable, the variable must be **sysname**.

## Remarks

The setting of `SET LANGUAGE` is set at execute or run time and not at parse time.

`SET LANGUAGE` implicitly sets the setting of [SET DATEFORMAT](set-dateformat-transact-sql.md).

## Permissions

Requires membership in the **public** role.

## Examples

The following example sets the default language to `Italian`, displays the month name, and then switches back to `us_english` and displays the month name again.

```sql
DECLARE @Today DATETIME;
SET @Today = '2024-08-05';

SET LANGUAGE Italian;
SELECT DATENAME(month, @Today) AS 'Month Name';

SET LANGUAGE us_english;
SELECT DATENAME(month, @Today) AS 'Month Name';
GO
```

## Related content

- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [syslanguages](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md)
- [sp_helplanguage (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-helplanguage-transact-sql.md)
- [SET Statements (Transact-SQL)](set-statements-transact-sql.md)
