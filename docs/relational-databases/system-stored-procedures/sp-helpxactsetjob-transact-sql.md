---
title: "sp_helpxactsetjob (Transact-SQL)"
description: sp_helpxactsetjob displays information on the Xactset job for an Oracle Publisher.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpxactsetjob"
  - "sp_helpxactsetjob_TSQL"
helpviewer_keywords:
  - "sp_helpxactsetjob"
dev_langs:
  - "TSQL"
---
# sp_helpxactsetjob (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays information on the Xactset job for an Oracle Publisher. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpxactsetjob [ @publisher = ] N'publisher'
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher to which the job belongs. *@publisher* is **sysname**, with no default.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `jobnumber` | **int** | Oracle job number. |
| `lastdate` | **varchar(22)** | Last date that the job ran. |
| `thisdate` | **varchar(22)** | Time of change |
| `nextdate` | **varchar(22)** | Next date that the job will run. |
| `broken` | **varchar(1)** | Flag indicating if the job is broken. |
| `interval` | **varchar(200)** | Interval for the job. |
| `failures` | **int** | Number of failures for the job. |
| `xactsetjobwhat` | **varchar(200)** | Name of procedure executed by the job. |
| `xactsetjob` | **varchar(1)** | Status of the job, which can be one of the following values:<br /><br />`1` - the job is enabled.<br />`0` - the job is disabled. |
| `xactsetlonginterval` | **int** | Long interval for the job. |
| `xactsetlongthreshold` | **int** | Long threshold for the job. |
| `xactsetshortinterval` | **int** | Short interval for the job. |
| `xactsetshortthreshold` | **int** | Short threshold for the job. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpxactsetjob` is used in snapshot replication and transactional replication for an Oracle Publishers.

`sp_helpxactsetjob` always returns the current settings for the Xactset job (`HREPL_XactSetJob`) at the publisher. If the Xactset job is currently in the job queue, it additionally returns attributes of the job from the `USER_JOB` data dictionary view created under the administrator account at the Oracle Publisher.

## Permissions

Only a member of the **sysadmin** fixed server role can execute `sp_helpxactsetjob`.

## Related content

- [Configure the Transaction Set Job for an Oracle Publisher](../replication/administration/configure-the-transaction-set-job-for-an-oracle-publisher.md)
- [sp_publisherproperty (Transact-SQL)](sp-publisherproperty-transact-sql.md)
