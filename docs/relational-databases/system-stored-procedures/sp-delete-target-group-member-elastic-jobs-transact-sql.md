---
title: "jobs.sp_delete_target_group_member (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_delete_target_group_member removes a member from a target group for the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/30/2023
ms.service: azure-sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_target_group_member_TSQL"
  - "sp_delete_target_group_member"
  - "jobs.sp_delete_target_group_member_TSQL"
  - "jobs.sp_delete_target_group_member"
helpviewer_keywords:
  - "sp_delete_target_group_member"
  - "jobs.sp_delete_target_group_member"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_delete_target_group_member (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Removes a database or group of databases from a target group in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_delete_target_group_member [ @target_group_name = ] 'target_group_name'
   [ , [ @target_id = ] 'target_id']
```

## Arguments

#### @target_group_name

The name of the target group from which to remove the target group member. *target_group_name* is nvarchar(128), with no default.

#### @target_id

The target identification number assigned to the target group member to be removed. *target_id* is a uniqueidentifier, with a default of `NULL`.

## Return Code Values

0 (success) or 1 (failure)

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure.  Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Examples

### Remove a server from a target group

The following example removes the `London` server from the group "Servers Maintaining Customer Information". You must connect to the jobs database specified when creating the job agent, in this case `ElasticJobs`.

```sql
--Connect to the jobs database specified when creating the job agent
USE ElasticJobs ;
GO

-- Retrieve the target_id for a target_group_members
DECLARE @tid uniqueidentifier
SELECT @tid = target_id 
FROM [jobs].target_group_members 
WHERE target_group_name = 'Servers Maintaining Customer Information' 
AND server_name = 'London.database.windows.net';

-- Remove a target group member of type server
EXEC jobs.sp_delete_target_group_member
@target_group_name = N'Servers Maintaining Customer Information',
@target_id = @tid;
GO
```

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
