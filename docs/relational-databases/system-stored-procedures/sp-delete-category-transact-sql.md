---
title: "sp_delete_category (Transact-SQL)"
description: Removes the specified category of jobs, alerts, or operators from the current server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_category_TSQL"
  - "sp_delete_category"
helpviewer_keywords:
  - "sp_delete_category"
dev_langs:
  - "TSQL"
---
# sp_delete_category (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the specified category of jobs, alerts, or operators from the current server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_category
    [ @class = ] 'class'
    , [ @name = ] N'name'
[ ; ]
```

## Arguments

#### [ @class = ] '*class*'

The class of the category. *@class* is **varchar(8)**, with no default, and must be one of these values.

| Value | Description |
| --- | --- |
| `JOB` | Deletes a job category. |
| `ALERT` | Deletes an alert category. |
| `OPERATOR` | Deletes an operator category. |

#### [ @name = ] N'*name*'

The name of the category to be removed. *@name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_delete_category` must be run from the `msdb` database.

Deleting a category recategorizes any jobs, alerts, or operators in that category to the default category for the class.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example deletes the job category named `AdminJobs`.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_category
    @name = N'AdminJobs',
    @class = N'JOB';
GO
```

## Related content

- [sp_add_category (Transact-SQL)](sp-add-category-transact-sql.md)
- [sp_help_category (Transact-SQL)](sp-help-category-transact-sql.md)
- [sp_update_category (Transact-SQL)](sp-update-category-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
