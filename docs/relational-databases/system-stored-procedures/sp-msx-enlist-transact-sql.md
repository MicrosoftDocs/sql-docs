---
title: "sp_msx_enlist (Transact-SQL)"
description: sp_msx_enlist adds the current server to the list of available servers on the master server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_msx_enlist_TSQL"
  - "sp_msx_enlist"
helpviewer_keywords:
  - "sp_msx_enlist"
dev_langs:
  - "TSQL"
---
# sp_msx_enlist (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds the current server to the list of available servers on the master server.

> [!CAUTION]  
> The `sp_msx_enlist` stored procedure edits the registry. Manual editing of the registry isn't recommended, because inappropriate or incorrect changes can cause serious configuration problems for your system. Therefore, only experienced users should use the Registry Editor program to edit the registry. For more information, see the Microsoft Windows documentation.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_msx_enlist
    [ @msx_server_name = ] N'msx_server_name'
    [ , [ @location = ] N'location' ]
[ ; ]
```

## Arguments

#### [ @msx_server_name = ] N'*msx_server_name*'

The name of the multiserver administration (master) server. *@msx_server_name* is **sysname**, with no default.

#### [ @location = ] N'*location*'

The location of the target server to add. *@location* is **nvarchar(100)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

Permissions to execute this procedure default to members of the **sysadmin** fixed server role.

## Examples

The following example enlists the current server into the `AdventureWorks1` master server. The location for the current server is `Building 21, Room 309, Rack 5`.

```sql
USE msdb;
GO

EXEC dbo.sp_msx_enlist N'AdventureWorks1',
    N'Building 21, Room 309, Rack 5';
GO
```

## Related content

- [sp_msx_defect (Transact-SQL)](sp-msx-defect-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [xp_cmdshell (Transact-SQL)](xp-cmdshell-transact-sql.md)
