---
title: "jobs.sp_add_target_group (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_add_target_group creates a target group for jobs in the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/30/2023
ms.service: azure-sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_target_group_TSQL"
  - "sp_add_target_group"
  - "jobs.sp_add_target_group_TSQL"
  - "jobs.sp_add_target_group"
helpviewer_keywords:
  - "sp_add_target_group"
  - "jobs.sp_add_target_group"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_add_target_group (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Creates a target group for jobs in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

Target groups provide an easy way to target a job at a collection of servers and/or databases.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_add_target_group 
 @target_group_name = 'target_group_name'
[ , [ @target_group_id = ] target_group_id OUTPUT ]
```

## Arguments

#### @target_group_name

The name of the target group to create. *target_group_name* is nvarchar(128), with no default.

#### @target_group_id OUTPUT

 The target group identification number assigned to the job if created successfully. *target_group_id* is an output variable of type uniqueidentifier, with a default of `NULL`.

## Return Code Values

0 (success) or 1 (failure)

## Remarks

Use [jobs.sp_add_target_group_member](sp-add-target-group-member-elastic-jobs-transact-sql.md) to add members to a target group.

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure.  Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Examples

### Create a target group

The following example creates a target group named `ServerGroup1`.

```sql
-- Add a target group containing server(s)
EXEC [jobs].sp_add_target_group @target_group_name = N'ServerGroup1';
```

## Related content

- [jobs.sp_delete_target_group (Azure Elastic Jobs) (Transact-SQL)](sp-delete-target-group-elastic-jobs-transact-sql.md)
- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
