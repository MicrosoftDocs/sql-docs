---
title: "sp_add_targetsvrgrp_member (Transact-SQL)"
description: "Adds the specified target server to the specified target server group."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_targetsvrgrp_member"
  - "sp_add_targetsvrgrp_member_TSQL"
helpviewer_keywords:
  - "sp_add_targetsvrgrp_member"
dev_langs:
  - "TSQL"
---
# sp_add_targetsvrgrp_member (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds the specified target server to the specified target server group.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_targetsvrgrp_member
    [ @group_name = ] 'group_name'
    , [ @server_name = ] N'server_name'
[ ; ]
```

## Arguments

#### [ @group_name = ] '*group_name*'

The name of the group. *@group_name* is **sysname**, with no default.

#### [ @server_name = ] N'*server_name*'

The name of the server that should be added to the specified group. *@server_name* is **nvarchar(30)**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

A target server can be a member of more than one target server group.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example adds the group `Servers Maintaining Customer Information` and adds the `LONDON1` server to that group.

```sql
USE msdb;
GO

EXEC dbo.sp_add_targetsvrgrp_member
    @group_name = N'Servers Maintaining Customer Information',
    @server_name = N'LONDON1';
GO
```

## Related content

- [sp_delete_targetsvrgrp_member (Transact-SQL)](sp-delete-targetsvrgrp-member-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
