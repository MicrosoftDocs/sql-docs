---
title: "sp_delete_targetserver (Transact-SQL)"
description: Removes the specified server from the list of available target servers.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_targetserver"
  - "sp_delete_targetserver_TSQL"
helpviewer_keywords:
  - "sp_delete_targetserver"
dev_langs:
  - "TSQL"
---
# sp_delete_targetserver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the specified server from the list of available target servers.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_targetserver
    [ @server_name = ] N'server_name'
    [ , [ @clear_downloadlist = ] clear_downloadlist ]
    [ , [ @post_defection = ] post_defection ]
[ ; ]
```

## Arguments

#### [ @server_name = ] N'*server_name*'

The name of the server to remove as an available target server. *@server_name* is **sysname**, with no default.

#### [ @clear_downloadlist = ] *clear_downloadlist*

Specifies whether to clear the download list for the target server. *@clear_downloadlist* is **bit**, with a default of `1`.

- When *@clear_downloadlist* is `1`, the procedure clears the download list for the server before deleting the server.
- When *@clear_downloadlist* is `0`, the download list isn't cleared.

#### [ @post_defection = ] *post_defection*

Specifies whether to post a defect instruction to the target server. *@post_defection* is **bit**, with a default of `1`.

- When *@post_defection* is `1`, the procedure posts a defect instruction to the target server before deleting the server.
- When *@post_defection* is `0`, the procedure doesn't post a defect instruction to the target server.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

The normal way to delete a target server is to call `sp_msx_defect` at the target server. Use `sp_delete_targetserver` only when a manual defection is necessary.

## Permissions

To run this stored procedure, users must be granted the **sysadmin** fixed server role.

## Examples

The following example removes the server `LONDON1` from the available job servers.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_targetserver
    @server_name = N'LONDON1';
GO
```

## Related content

- [sp_help_targetserver (Transact-SQL)](sp-help-targetserver-transact-sql.md)
- [sp_msx_defect (Transact-SQL)](sp-msx-defect-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
