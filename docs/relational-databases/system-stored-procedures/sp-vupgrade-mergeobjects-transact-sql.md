---
title: "sp_vupgrade_mergeobjects (Transact-SQL)"
description: Regenerates the article-specific triggers, stored procedures, and views that are used to track and apply data changes for merge replication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_vupgrade_mergeobjects"
  - "sp_vupgrade_mergeobjects_TSQL"
helpviewer_keywords:
  - "sp_vupgrade_mergeobjects"
dev_langs:
  - "TSQL"
---
# sp_vupgrade_mergeobjects (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Regenerates the article-specific triggers, stored procedures, and views that are used to track and apply data changes for merge replication. Execute this procedure in the following situations:

- If an object required by replication is accidentally dropped.

- If you apply an update, such as a hotfix, which requires modification to one or more replication objects. Execute the procedure on each node after applying the update.

Executing this stored procedure doesn't require reinitialization of subscriptions. This procedure isn't required if you install a service pack or upgrade to a new version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_vupgrade_mergeobjects
    [ [ @login = ] N'login' ]
    [ , [ @password = ] N'password' ]
    [ , [ @security_mode = ] security_mode ]
[ ; ]
```

## Arguments

#### [ @login = ] N'*login*'

The system administrator login to use when creating new system objects in the distribution database. *@login* is **sysname**, with a default of `NULL`. This parameter isn't required if *@security_mode* is set to `1`, which is Windows Authentication.

#### [ @password = ] N'*password*'

The system administrator password to use when creating new system objects in the distribution database. *@password* is **sysname**, with a default of an empty string. This parameter isn't required if *@security_mode* is set to `1`, which is Windows Authentication.

#### [ @security_mode = ] *security_mode*

The login security mode to use when creating new system objects in the distribution database. *@security_mode* is **bit**, with a default of `1`. If `0`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is used. If `1`, Windows Authentication is used. [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_vupgrade_mergeobjects` is used only for merge replication.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [Upgrade or patch replicated databases](../../database-engine/install-windows/upgrade-replicated-databases.md)
