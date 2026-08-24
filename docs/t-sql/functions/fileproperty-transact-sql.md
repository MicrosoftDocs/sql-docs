---
title: "FILEPROPERTY (Transact-SQL)"
description: FILEPROPERTY returns the specified file name property value when a file name in the current database and a property name are specified.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/05/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FILEPROPERTY_TSQL"
  - "FILEPROPERTY"
helpviewer_keywords:
  - "viewing file properties"
  - "names [SQL Server], files"
  - "displaying file properties"
  - "file properties [SQL Server]"
  - "FILEPROPERTY function"
  - "file names [SQL Server], FILEPROPERTY"
dev_langs:
  - "TSQL"
---
# FILEPROPERTY (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the specified file name property value when a file name in the current database and a property name are specified. Returns `NULL` for files that aren't in the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
FILEPROPERTY ( file_name , property )
```

## Arguments

#### *file_name*

An expression that contains the name of the file associated with the current database for which to return property information. *file_name* is **nchar(128)**.

#### *property*

An expression that contains the name of the file property to return. *property* is **varchar(128)**, and can be one of the following values.

| Value | Description | Value returned |
| --- | --- | --- |
| `IsReadOnly`| File is read-only. | 1 = True<br />0 = False<br />NULL = Input isn't valid. |
| `IsPrimaryFile` | File is the primary file. | 1 = True<br />0 = False<br />NULL = Input isn't valid. |
| `IsLogFile` | File is a log file. | 1 = True<br />0 = False<br />NULL = Input isn't valid. |
| `SpaceUsed` | Amount of space that is used by the specified file. | Number of pages allocated in the file |

## Return types

**int**

## Remarks

*file_name* corresponds to the **name** column in the `sys.master_files` or `sys.database_files` catalog view.

## Examples

The following example returns the setting for the `IsPrimaryFile` property for the `AdventureWorks_Data` file name in [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] the database.

```sql
SELECT FILEPROPERTY('AdventureWorks2022_Data', 'IsPrimaryFile') AS [Primary File];
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Primary File
-------------
1
```

## Related content

- [FILEGROUPPROPERTY (Transact-SQL)](filegroupproperty-transact-sql.md)
- [Metadata functions (Transact-SQL)](metadata-functions-transact-sql.md)
- [sys.sp_spaceused (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)
- [sys.database_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [sys.master_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)
