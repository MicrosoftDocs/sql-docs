---
title: "sp_manage_jobs_by_login (Transact-SQL)"
description: sp_manage_jobs_by_login deletes or reassigns jobs that belong to the specified login.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_manage_jobs_by_login"
  - "sp_manage_jobs_by_login_TSQL"
helpviewer_keywords:
  - "sp_manage_jobs_by_login"
dev_langs:
  - "TSQL"
---
# sp_manage_jobs_by_login (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes or reassigns jobs that belong to the specified login.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_manage_jobs_by_login
    [ @action = ] 'action'
    , [ @current_owner_login_name = ] N'current_owner_login_name'
    [ , [ @new_owner_login_name = ] N'new_owner_login_name' ]
[ ; ]
```

## Arguments

#### [ @action = ] '*action*'

The action to take for the specified login. *@action* is **varchar(10)**, with no default. When *@action* is `DELETE`, `sp_manage_jobs_by_login` deletes all jobs owned by *@current_owner_login_name*. When *@action* is `REASSIGN`, all jobs are assigned to *@new_owner_login_name*.

#### [ @current_owner_login_name = ] N'*current_owner_login_name*'

The login name of the current job owner. *@current_owner_login_name* is **sysname**, with no default.

#### [ @new_owner_login_name = ] N'*new_owner_login_name*'

The login name of the new job owner. *@new_owner_login_name* is **sysname**, with a default of `NULL`. Use this parameter only if *@action* is `REASSIGN`. *@new_owner_login_name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

To run this stored procedure, users must be granted the **sysadmin** fixed server role.

## Examples

The following example reassigns all jobs from `danw` to `françoisa`.

```sql
USE msdb;
GO

EXEC dbo.sp_manage_jobs_by_login
    @action = N'REASSIGN',
    @current_owner_login_name = N'danw',
    @new_owner_login_name = N'françoisa';
GO
```

## Related content

- [sp_delete_job (Transact-SQL)](sp-delete-job-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
