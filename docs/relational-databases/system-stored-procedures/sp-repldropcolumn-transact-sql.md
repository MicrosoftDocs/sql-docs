---
title: "sp_repldropcolumn (Transact-SQL)"
description: sp_repldropcolumn drops a column from an existing table article that was published.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_repldropcolumn_TSQL"
  - "sp_repldropcolumn"
helpviewer_keywords:
  - "sp_repldropcolumn"
dev_langs:
  - "TSQL"
---
# sp_repldropcolumn (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Drops a column from an existing table article that was published. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> This stored procedure has been deprecated and is being supported mainly for backward-compatibility. It should only be used with [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] Publishers and [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] republishing Subscribers. This procedure shouldn't be used on columns with data types that were introduced in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_repldropcolumn
    [ @source_object = ] N'source_object'
    , [ @column = ] N'column'
    [ , [ @from_agent = ] from_agent ]
    [ , [ @schema_change_script = ] N'schema_change_script' ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
[ ; ]
```

## Arguments

#### [ @source_object = ] N'*source_object*'

The name of the table article that contains the column to drop. *@source_object* is **nvarchar(270)**, with no default.

#### [ @column = ] N'*column*'

The name of the column in the table to be dropped. *@column* is **sysname**, with no default.

#### [ @from_agent = ] *from_agent*

Specifies whether the stored procedure is being executed by a replication agent. *@from_agent* is **int**, with a default of `0`.

Use `1` when this stored procedure is being executed by a replication agent, otherwise use the default value of `0`.

#### [ @schema_change_script = ] N'*schema_change_script*'

Specifies the name and path of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] script used to modify the system generated custom stored procedures. *@schema_change_script* is **nvarchar(4000)**, with a default of `NULL`.

Replication allows user-defined custom stored procedures to replace one or more of the default procedures used in transactional replication. *@schema_change_script* is executed after a schema change is made to a replicated table article using `sp_repldropcolumn`, and can be used for one of the following options:

- If custom stored procedures are automatically regenerated, *@schema_change_script* can be used to drop these custom stored procedures and replace them with user-defined custom stored procedures that support the new schema.

- If custom stored procedures aren't automatically regenerated, *@schema_change_script*can be used to regenerate these stored procedures or to create user-defined custom stored procedures.

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Enables or disables the ability to have a snapshot invalidated. *@force_invalidate_snapshot* is **bit**, with a default of `1`.

- `1` specifies that changes to the article might cause the snapshot to be invalid, and if that is the case, a value of `1` gives permission for the new snapshot to occur.

- `0` specifies that changes to the article don't cause the snapshot to be invalid.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Enables or disables the ability to have the subscription reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that changes to the article don't cause the subscription to be reinitialized.

- `1` specifies that changes to the article might cause the subscription to be reinitialized, and if that is the case, a value of `1` gives permission for the subscription reinitialization to occur.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Only members of the **sysadmin** fixed server role at the Publisher or members of the **db_owner** or **db_ddladmin** fixed database roles on the publication database can execute `sp_repldropcolumn`.

## Related content

- [Deprecated Features in SQL Server Replication](../replication/deprecated-features-in-sql-server-replication.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
