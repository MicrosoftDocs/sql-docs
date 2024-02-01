---
title: "sp_addscriptexec (Transact-SQL)"
description: sp_addscriptexec posts a Transact-SQL script to all Subscribers of a publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addscriptexec"
  - "sp_addscriptexec_TSQL"
helpviewer_keywords:
  - "sp_addscriptexec"
dev_langs:
  - "TSQL"
---
# sp_addscriptexec (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Posts a Transact-SQL script (`.sql` file) to all Subscribers of a publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addscriptexec
    [ @publication = ] N'publication'
    , [ @scriptfile = ] N'scriptfile'
    [ , [ @skiperror = ] skiperror ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @scriptfile = ] N'*scriptfile*'

The full path to the SQL script file. *@scriptfile* is **nvarchar(4000)**, with no default.

#### [ @skiperror = ] *skiperror*

Indicates whether the Distribution Agent or Merge Agent should stop when an error is encountered during script processing. *@skiperror* is **bit**, with a default of `0`.

- `0` = the agent stops.
- `1` = the agent continues the script and ignores the error.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when publishing from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addscriptexec` is used in transactional replication and merge replication.

`sp_addscriptexec` isn't used for snapshot replication.

To use `sp_addscriptexec`, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account must have read and write permissions on the snapshot location and read permissions on the location where any scripts are stored.

The [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md) is used to execute the script at the Subscriber, and the script is executed in the security context used by the Distribution Agent or Merge Agent when connecting to the subscription database. When the agent is run on a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the [osql Utility](../../tools/osql-utility.md) is used instead of the [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md).

`sp_addscriptexec` is useful in applying scripts to subscribers, and uses the [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md) to apply the contents of the script to the Subscriber. However, because Subscriber configurations can vary, scripts tested before posting to the Publisher might still cause errors on a Subscriber. *@skiperror* allows for the Distribution Agent or Merge Agent ignore errors and continue on. Use the [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md) to test scripts before running `sp_addscriptexec`.

> [!NOTE]  
> Skipped errors continue to be logged in the Agent history for reference.

Using `sp_addscriptexec` to post a script file for publications using FTP for snapshot delivery is only supported for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addscriptexec`.

## Related content

- [Execute Scripts During Synchronization (Replication Transact-SQL Programming)](../replication/execute-scripts-during-synchronization-replication-transact-sql-programming.md)
- [Synchronize Data](../replication/synchronize-data.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
