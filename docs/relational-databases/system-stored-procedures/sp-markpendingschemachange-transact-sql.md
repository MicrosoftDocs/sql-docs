---
title: "sp_markpendingschemachange (Transact-SQL)"
description: sp_markpendingschemachange is used for supportability of merge publications.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_markpendingschemachange"
  - "sp_markpendingschemachange_TSQL"
helpviewer_keywords:
  - "sp_markpendingschemachange"
dev_langs:
  - "TSQL"
---
# sp_markpendingschemachange (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Used for supportability of merge publications by enabling an administrator to skip selected pending schema changes, so that they aren't replicated. This stored procedure is executed at the Publisher on the publication database.

> [!CAUTION]  
> This stored procedure can cause schema changes not to be replicated. It should only be used to resolve issues after other methods, such as reinitialization, have already been tried or are too expensive in terms of performance.

## Syntax

```syntaxsql
sp_markpendingschemachange
    [ @publication = ] N'publication'
    [ , [ @schemaversion = ] schemaversion ]
    [ , [ @status = ] N'status' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @schemaversion = ] *schemaversion*

Identifies a pending schema change. *@schemaversion* is **int**, with a default of `0`. Use [sp_enumeratependingschemachanges](sp-enumeratependingschemachanges-transact-sql.md) to list the pending schema changes for the publication.

#### [ @status = ] N'*status*'

Specifies whether a pending schema change is skipped. *@status* is **nvarchar(10)**, with a default of `active`. If the value of *@status* is `skipped`, then the selected schema change isn't replicated.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_markpendingschemachange` is used with merge replication.

`sp_markpendingschemachange` is a stored procedure intended for the supportability of merge replication and should be used only when other corrective actions, such as reinitialization, have failed to correct the situation or are too expensive in terms of performance.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_markpendingschemachange`.

## Related content

- [sysmergeschemachange (Transact-SQL)](../system-tables/sysmergeschemachange-transact-sql.md)
