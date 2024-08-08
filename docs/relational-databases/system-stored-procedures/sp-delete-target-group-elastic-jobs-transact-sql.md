---
title: "jobs.sp_delete_target_group (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_delete_target_group removes a target group in the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/30/2023
ms.service: azure-sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_target_group_TSQL"
  - "sp_delete_target_group"
  - "jobs.sp_delete_target_group_TSQL"
  - "jobs.sp_delete_target_group"
helpviewer_keywords:
  - "sp_delete_target_group"
  - "jobs.sp_delete_target_group"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_delete_target_group (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Deletes a target group in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_delete_target_group [ @target_group_name = ] 'target_group_name'
```

## Arguments

#### @target_group_name

The name of the target group to delete. *target_group_name* is nvarchar(128), with no default.

## Return Code Values

0 (success) or 1 (failure)

## Remarks

Use [jobs.sp_delete_target_group_member](sp-delete-target-group-member-elastic-jobs-transact-sql.md) to remove servers or databases from a target group.

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure.

## Examples

### Create a target group

The following example removes a target group named `ServerGroup1`.

```sql
-- Remove a target group member of type server
EXEC jobs.sp_delete_target_group
@target_group_name = N'ServerGroup1';
```

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
