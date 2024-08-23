---
title: "sp_register_custom_scripting (Transact-SQL)"
description: sp_register_custom_scripting registers a stored procedure or Transact-SQL script file that is executed when a schema change occurs.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_register_custom_scripting"
  - "sp_register_custom_scripting_TSQL"
helpviewer_keywords:
  - "sp_register_custom_scripting"
dev_langs:
  - "TSQL"
---
# sp_register_custom_scripting (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Replication allows user-defined custom stored procedures to replace one or more of the default procedures used in transactional replication. When a schema change is made to a replicated table, these stored procedures are re-created.

`sp_register_custom_scripting` registers a stored procedure or [!INCLUDE [tsql](../../includes/tsql-md.md)] script file that is executed when a schema change occurs to script out the definition for a new user-defined custom stored procedure. This new user-defined custom stored procedure should reflect the new schema for the table. `sp_register_custom_scripting` is executed at the Publisher on the publication database, and the registered script file or stored procedure is executed at the Subscriber when a schema change occurs.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_register_custom_scripting
    [ @type = ] 'type'
    , [ @value = ] N'value'
    [ , [ @publication = ] N'publication' ]
    [ , [ @article = ] N'article' ]
[ ; ]
```

## Arguments

#### [ @type = ] '*type*'

The type of custom stored procedure or script being registered. *@type* is **varchar(16)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `insert` | Registered custom stored procedure is executed when an `INSERT` statement is replicated. |
| `update` | Registered custom stored procedure is executed when an `UPDATE` statement is replicated. |
| `delete` | Registered custom stored procedure is executed when a `DELETE` statement is replicated. |
| `custom_script` | Script is executed at the end of the data definition language (DDL) trigger. |

#### [ @value = ] N'*value*'

Name of a stored procedure or name and fully qualified path to the [!INCLUDE [tsql](../../includes/tsql-md.md)] script file that is being registered. *@value* is **nvarchar(2048)**, with no default.

Specifying `NULL` for *@value* unregisters a previously registered script, which is the same as running [sp_unregister_custom_scripting](sp-unregister-custom-scripting-transact-sql.md).

When the value of *@type* is **custom_script**, the name and full path of a [!INCLUDE [tsql](../../includes/tsql-md.md)] script file is expected. Otherwise, *@value* must be the name of a registered stored procedure.

#### [ @publication = ] N'*publication*'

Name of the publication for which the custom stored procedure or script is being registered. *@publication* is **sysname**, with a default of `NULL`.

#### [ @article = ] N'*article*'

Name of the article for which the custom stored procedure or script is being registered. *@article* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_register_custom_scripting` is used in snapshot and transactional replication.

This stored procedure should be executed before making a schema change to a replicated table. For more information about using this stored procedure, see [Transactional Articles - Regenerate to Reflect Schema Changes](../replication/transactional/transactional-articles-regenerate-to-reflect-schema-changes.md).

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role can execute `sp_register_custom_scripting`.

## Related content

- [sp_unregister_custom_scripting (Transact-SQL)](sp-unregister-custom-scripting-transact-sql.md)
