---
title: "sp_update_category (Transact-SQL)"
description: Changes the name of a category.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_category"
  - "sp_update_category_TSQL"
helpviewer_keywords:
  - "sp_update_category"
dev_langs:
  - "TSQL"
---
# sp_update_category (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the name of a category.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_update_category
    [ @class = ] 'class'
    , [ @name = ] N'name'
    , [ @new_name = ] N'new_name'
[ ; ]
```

## Arguments

#### [ @class = ] '*class*'

The class of the category to update. *@class* is **varchar(8)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `ALERT` | Updates an alert category. |
| `JOB` | Updates a job category. |
| `OPERATOR` | Updates an operator category. |

#### [ @name = ] N'*name*'

The current name of the category. *@name* is **sysname**, with no default.

#### [ @new_name = ] N'*new_name*'

The new name for the category. *@new_name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_update_category` must be run from the `msdb` database.

## Permissions

To run this stored procedure, users must be granted the **sysadmin** fixed server role.

## Examples

The following example renames a job category from `AdminJobs` to `Administrative Jobs`.

```sql
USE msdb;
GO

EXEC dbo.sp_update_category
  @class = N'JOB',
  @name = N'AdminJobs',
  @new_name = N'Administrative Jobs';
GO
```

## Related content

- [sp_add_category (Transact-SQL)](sp-add-category-transact-sql.md)
- [sp_delete_category (Transact-SQL)](sp-delete-category-transact-sql.md)
- [sp_help_category (Transact-SQL)](sp-help-category-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
