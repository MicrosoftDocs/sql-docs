---
title: "sp_helparticledts (Transact-SQL)"
description: sp_helparticledts is used to get information on the correct custom task names to use, when creating a transformation subscription using Visual Basic.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helparticledts"
  - "sp_helparticledts_TSQL"
helpviewer_keywords:
  - "sp_helparticledts"
dev_langs:
  - "TSQL"
---
# sp_helparticledts (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Used to get information on the correct custom task names to use when creating a transformation subscription using [!INCLUDE [msCoName](../../includes/msconame-md.md)] Visual Basic. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helparticledts
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of an article in the publication. *@article* is **sysname**, with no default.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `pre_script_ignore_error_task_name` | **sysname** | Task name for the programming task that occurs before the snapshot data is copied. Program execution should continue if a script error is encountered. |
| `pre_script_task_name` | **sysname** | Task name for the programming task that occurs before the snapshot data is copied. Program execution halts on error. |
| `transformation_task_name` | **sysname** | Task name for the programming task when using a Data Driven Query task. |
| `post_script_ignore_error_task_name` | **sysname** | Task name for the programming task that occurs after the snapshot data is copied. Program execution should continue if a script error is encountered. |
| `post_script_task_name` | **sysname** | Task name for the programming task that occurs after the snapshot data is copied. Program execution halts on error. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helparticledts` is used in snapshot replication and transactional replication.

There are naming conventions, required by the replication agents, which must be followed when naming tasks in a replication Data Transformation Services (DTS) program. For custom tasks, such as an Execute SQL task, the name is a concatenated string consisting of the article name, a prefix, and an optional part. When you write the code, if you're unsure what the task names should be, the result set gives the task names that should be used.

## Permissions

Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute `sp_helparticledts`.
