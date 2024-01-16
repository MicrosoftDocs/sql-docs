---
title: "sp_vupgrade_replication (Transact-SQL)"
description: Activated by setup when upgrading a replication server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_vupgrade_replication_TSQL"
  - "sp_vupgrade_replication"
helpviewer_keywords:
  - "sp_vupgrade_replication"
dev_langs:
  - "TSQL"
---
# sp_vupgrade_replication (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Activated by setup when upgrading a replication server. Upgrades schema and system data as needed to support replication at the current product level. Creates new replication system objects in system and user databases. This stored procedure is executed at the machine where the replication upgrade is to occur.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_vupgrade_replication
    [ [ @login = ] N'login' ]
    [ , [ @password = ] N'password' ]
    [ , [ @ver_old = ] ver_old ]
    [ , [ @force_remove = ] force_remove ]
    [ , [ @security_mode = ] security_mode ]
    [ , [ @db_id = ] db_id ]
[ ; ]
```

## Arguments

#### [ @login = ] N'*login*'

The system administrator login to use when creating new system objects in the Distribution database. *@login* is **sysname**, with a default of `NULL`. This parameter isn't required if *@security_mode* is set to `1`, which is Windows Authentication.

> [!NOTE]  
> This parameter is ignored when you're upgrading to [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.

#### [ @password = ] N'*password*'

The system administrator password to use when creating new system objects in the Distribution database. *@password* is **sysname**, with a default of an empty string. This parameter isn't required if *@security_mode* is set to `1`, which is Windows Authentication.

> [!NOTE]  
> This parameter is ignored when you're upgrading to [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.

#### [ @ver_old = ] *ver_old*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

This stored procedure is deprecated and will be removed in a future release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

#### [ @force_remove = ] *force_remove*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @security_mode = ] *security_mode*

The login security mode to use when creating new system objects in the Distribution database. *@security_mode* is **bit**, with a default of `1`. If `0`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is used. If `1`, Windows Authentication is used.

> [!NOTE]  
> This parameter is ignored when you're upgrading to [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.

#### [ @db_id = ] *db_id*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_vupgrade_replication` is used when upgrading all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_vupgrade_replication`.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [Validate Replicated Data](../replication/validate-data-at-the-subscriber.md)
