---
title: "sp_add_category (Transact-SQL)"
description: "Adds the specified category of jobs, alerts, or operators to the server."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_category"
  - "sp_add_category_TSQL"
helpviewer_keywords:
  - "sp_add_category"
dev_langs:
  - "TSQL"
---
# sp_add_category (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds the specified category of jobs, alerts, or operators to the server. For alternative method, see [Create a Job Category](../../ssms/agent/create-a-job-category.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

## Syntax

```syntaxsql
sp_add_category
     [ [ @class = ] 'class' ]
     [ , [ @type = ] 'type' ]
     [ , [ @name = ] 'name' ]
[ ; ]
```

## Arguments

#### [ @class = ] '*class*'

The class of the category to be added. *@class* is **varchar(8)** with a default value of `JOB`, and can be one of these values.

| Value | Description |
| --- | --- |
| `JOB` | Adds a job category. |
| `ALERT` | Adds an alert category. |
| `OPERATOR` | Adds an operator category. |

#### [ @type = ] '*type*'

The type of category to be added. *@type* is **varchar(12)**, with a default value of `LOCAL`, and can be one of these values.

| Value | Description |
| --- | --- |
| `LOCAL` | A local job category. |
| `MULTI-SERVER` | A multiserver job category. |
| `NONE` | A category for a class other than `JOB`. |

#### [ @name = ] '*name*'

The name of the category to be added. The name must be unique within the specified class. *@name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_add_category` must be run from the `msdb` database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example creates a local job category named `AdminJobs`.

```sql
USE msdb;
GO

EXEC dbo.sp_add_category
    @class = N'JOB',
    @type = N'LOCAL',
    @name = N'AdminJobs';
GO
```

## Related content

- [sp_delete_category (Transact-SQL)](sp-delete-category-transact-sql.md)
- [sp_help_category (Transact-SQL)](sp-help-category-transact-sql.md)
- [sp_update_category (Transact-SQL)](sp-update-category-transact-sql.md)
- [dbo.sysjobs (Transact-SQL)](../system-tables/dbo-sysjobs-transact-sql.md)
- [dbo.sysjobservers (Transact-SQL)](../system-tables/dbo-sysjobservers-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
