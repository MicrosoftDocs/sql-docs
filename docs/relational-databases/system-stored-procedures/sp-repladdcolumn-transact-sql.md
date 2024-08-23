---
title: "sp_repladdcolumn (Transact-SQL)"
description: sp_repladdcolumn adds a column to an existing published table article in SQL Server 2000.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_repladdcolumn_TSQL"
  - "sp_repladdcolumn"
helpviewer_keywords:
  - "sp_repladdcolumn"
dev_langs:
  - "TSQL"
---
# sp_repladdcolumn (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a column to an existing published table article. Allows the new column to be added to all publishers that publish this table, or just add the column to a specific publication that publishes the table. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> This stored procedure is deprecated, and is being supported for backward compatibility. It should only be used with [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] Publishers and [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] republishing Subscribers. This procedure shouldn't be used on columns with data types that were introduced in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_repladdcolumn
    [ @source_object = ] N'source_object'
    , [ @column = ] N'column'
    , [ @typetext = ] N'typetext'
    [ , [ @publication_to_add = ] N'publication_to_add' ]
    [ , [ @from_agent = ] from_agent ]
    [ , [ @schema_change_script = ] N'schema_change_script' ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
[ ; ]
```

## Arguments

#### [ @source_object = ] N'*source_object*'

The name of the table article that contains the new column to add. *@source_object* is **nvarchar(358)**, with no default.

#### [ @column = ] N'*column*'

The name of the column in the table to be added for replication. *@column* is **sysname**, with no default.

#### [ @typetext = ] N'*typetext*'

The definition of the column being added. *@typetext* is **nvarchar(3000)**, with no default. For example, if the column order_filled is being added, and it's a single character field, not `NULL`, and has a default value of **N**, order_filled would be the *column* parameter, while the definition of the column, **char(1) NOT `NULL` CONSTRAINT constraint_name DEFAULT 'N'** would be the *@typetext* parameter value.

#### [ @publication_to_add = ] N'*publication_to_add*'

The name of the publication to which the new column is added. *@publication_to_add* is **nvarchar(4000)**, with a default of `all`. If `all`, then all publications containing this table are affected. If *@publication_to_add* is specified, then only this publication has the new column added.

#### [ @from_agent = ] *from_agent*

Specifies whether the stored procedure is being executed by a replication agent. *@from_agent* is **int**, with a default of `0`. A value of `1` is used when this stored procedure is being executed by a replication agent, and in every other case the default value of `0` should be used.

#### [ @schema_change_script = ] N'*schema_change_script*'

Specifies the name and path of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] script used to modify the system generated custom stored procedures. *@schema_change_script* is **nvarchar(4000)**, with a default of `NULL`. Replication allows user-defined custom stored procedures to replace one or more of the default procedures used in transactional replication. *@schema_change_script* is executed after a schema change is made to a replicated table article using `sp_repladdcolumn`, and can be used as follows:

- If custom stored procedures are automatically regenerated, *@schema_change_script* can be used to drop these custom stored procedures, and replace them with user-defined custom stored procedures that support the new schema.

- If custom stored procedures aren't automatically regenerated, *@schema_change_script* can be used to regenerate these stored procedures or to create user-defined custom stored procedures.

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

Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute `sp_repladdcolumn`.

## Related content

- [Deprecated Features in SQL Server Replication](../replication/deprecated-features-in-sql-server-replication.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
