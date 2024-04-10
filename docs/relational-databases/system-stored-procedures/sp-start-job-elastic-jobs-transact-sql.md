---
title: "jobs.sp_start_job (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_start_job starts an existing job in the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_start_job (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Starts an existing job in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

This stored procedure shares the name of `sp_start_job` with a similar object in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service. For information about the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent version, see [sp_start_job (Transact-SQL)](sp-start-job-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_start_job [ @job_name = ] 'job_name'
     [ , [ @job_execution_id = ] job_execution_id OUTPUT ]
```

## Arguments

#### @job_name

The name of the job to start. *job_name* is nvarchar(128), with no default.

#### @job_execution_id

Output parameter that will be assigned the job execution's ID. *job_version* is uniqueidentifier.

## Return Code Values

0 (success) or 1 (failure)

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure.  Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
