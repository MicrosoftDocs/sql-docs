---
title: "FILEGROUPPROPERTY (Transact-SQL)"
description: FILEGROUPPROPERTY returns the filegroup property value for a specified name and filegroup value.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/05/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FILEGROUPPROPERTY_TSQL"
  - "FILEGROUPPROPERTY"
helpviewer_keywords:
  - "filegroups [SQL Server], property values"
  - "FILEGROUPPROPERTY function"
  - "viewing filegroup properties"
  - "displaying filegroup properties"
dev_langs:
  - "TSQL"
---
# FILEGROUPPROPERTY (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This function returns the filegroup property value for a specified name and filegroup value.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
FILEGROUPPROPERTY ( filegroup_name , property )
```

## Arguments

#### *filegroup_name*

An expression of type **sysname** that represents the filegroup name for which `FILEGROUPPROPERTY` returns the named property information.

#### *property*

An expression of type **varchar(128)** that returns the name of the filegroup property. *Property* can return one of these values:

| Value | Description | Value returned |
| --- | --- | --- |
| `IsReadOnly` | Filegroup is read-only. | 1 = TRUE<br /><br />0 = FALSE<br />NULL = Invalid input. |
| `IsUserDefinedFG` | Filegroup is a user-defined filegroup. | 1 = TRUE<br /><br />0 = FALSE<br />NULL = Invalid input. |
| `IsDefault` | Filegroup is the default filegroup. | 1 = TRUE<br /><br />0 = FALSE<br />NULL = Invalid input. |

## Return types

**int**

## Remarks

*filegroup_name* corresponds to the **name** column from the `sys.filegroups` catalog view.

## Examples

This example returns the `IsDefault` property setting for the primary filegroup in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
SELECT FILEGROUPPROPERTY('PRIMARY', 'IsDefault') AS 'Default Filegroup';
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Default Filegroup
---------------------
1
```

## Related content

- [FILEGROUP_ID (Transact-SQL)](filegroup-id-transact-sql.md)
- [FILEGROUP_NAME (Transact-SQL)](filegroup-name-transact-sql.md)
- [Metadata functions (Transact-SQL)](metadata-functions-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [sys.filegroups (Transact-SQL)](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)
