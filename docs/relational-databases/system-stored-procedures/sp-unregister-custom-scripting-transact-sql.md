---
title: "sp_unregister_custom_scripting (Transact-SQL)"
description: Removes a user-defined custom stored procedure or Transact-SQL script file registered with sp_register_custom_scripting.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_unregister_custom_scripting_TSQL"
  - "sp_unregister_custom_scripting"
helpviewer_keywords:
  - "sp_unregister_custom_scripting"
dev_langs:
  - "TSQL"
---
# sp_unregister_custom_scripting (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure removes a user-defined custom stored procedure or [!INCLUDE [tsql](../../includes/tsql-md.md)] script file that was registered by executing [sp_register_custom_scripting](sp-register-custom-scripting-transact-sql.md). This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_unregister_custom_scripting
    [ @type = ] 'type'
    [ , [ @publication = ] N'publication' ]
    [ , [ @article = ] N'article' ]
[ ; ]
```

## Arguments

#### [ @type = ] '*type*'

The type of custom stored procedure or script being removed. *@type* is **varchar(16)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `insert` | Registered custom stored procedure or script is executed when an `INSERT` statement is replicated. |
| `update` | Registered custom stored procedure or script is executed when an `UPDATE` statement is replicated. |
| `delete` | Registered custom stored procedure or script is executed when a `DELETE` statement is replicated. |
| `custom_script` | Registered custom stored procedure or script is executed at the end of the data definition language (DDL) trigger. |

#### [ @publication = ] N'*publication*'

Name of the publication for which the custom stored procedure or script is being removed. *@publication* is **sysname**, with a default of `NULL`.

#### [ @article = ] N'*article*'

Name of the article for which the custom stored procedure or script is being removed. *@article* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_unregister_custom_scripting` is used in snapshot and transactional replication.

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role can execute `sp_unregister_custom_scripting`.

## Related content

- [sp_register_custom_scripting (Transact-SQL)](sp-register-custom-scripting-transact-sql.md)
