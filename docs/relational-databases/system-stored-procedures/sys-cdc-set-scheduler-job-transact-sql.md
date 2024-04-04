---
title: "sys.sp_cdc_set_scheduler_job (Transact-SQL)"
description: "For Azure SQL Database, instruct the change data capture (CDC) scheduler to pause or resume scheduling of CDC scan and/or CDC cleanup jobs."
author: abhimantiwari
ms.author: abhtiwar
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_cdc_set_scheduler_job_TSQL"
  - "sp_cdc_set_scheduler_job"
  - "sys.sp_cdc_set_scheduler_job"
  - "sp_cdc_set_scheduler_job_TSQL"
helpviewer_keywords:
  - "sp_cdc_set_scheduler_job"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current"
---
# sys.sp_cdc_set_scheduler_job (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Instruct the change data capture (CDC) scheduler to pause or resume scheduling of CDC scan and/or CDC cleanup jobs for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], or abort the currently running CDC scan and/or CDC cleanup job.

This system stored procedure is applicable to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] only.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_cdc_set_scheduler_job
    [ @jobType = ] N'JobType'
    , [ @state = ] N'state'
    , [ @abortTask = ] abortTask
[ ; ]
```

## Arguments

#### [ @jobType = ] N'*JobType*'

The type of CDC job, such as a capture job, or clean up job. *@jobType* is **nvarchar(20)**. Valid values are `capture`, `cleanup` or `both`. There's no default value.

#### [ @state = ] N'*state*'

Instructs the CDC scheduler to pause or resume scheduling the job. Valid values are `pause` or `resume`. There's no default value.

#### [ @abortTask = ] *abortTask*

Indicates whether you want to abort the current running task or not. Valid **int** values are `1` or `0`, with no default value. The *@abortTask* value is only used when the *@state* value is `pause`. However, it only accepts `1` as the valid input.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sys.sp_cdc_set_scheduler_job` applies to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] only.

- The CDC scheduler periodically schedules CDC scan and CDC cleanup jobs. Use `sp_cdc_set_scheduler_job` to instruct the scheduler to either pause the scheduling or resume scheduling of these jobs.

- The *@abortTask* parameter is used to indicate whether the currently running job should be aborted or not.

## Permissions

Requires membership in the **db_owner** fixed database role.

## Related content

- [dbo.cdc_jobs (Transact-SQL)](../system-tables/dbo-cdc-jobs-transact-sql.md)
