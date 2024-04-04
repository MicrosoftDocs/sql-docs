---
title: "sp_delete_proxy (Transact-SQL)"
description: sp_delete_proxy removes the specified proxy.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_proxy"
  - "sp_delete_proxy_TSQL"
helpviewer_keywords:
  - "sp_delete_proxy"
  - "DROP PROXY statement"
dev_langs:
  - "TSQL"
---
# sp_delete_proxy (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the specified proxy.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_proxy
    [ [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
[ ; ]
```

## Arguments

#### [ @proxy_id = ] *proxy_id*

The proxy identification number of the proxy to remove. *@proxy_id* is **int**, with a default of `NULL`.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy to remove. *@proxy_name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Either *@proxy_name* or *@proxy_id* must be specified. If both arguments are specified, the arguments must both refer to the same proxy or the stored procedure fails.

If a job step refers to the proxy specified, the proxy can't be deleted and the stored procedure fails.

## Permissions

By default, only members of the **sysadmin** fixed server role can execute `sp_delete_proxy`.

## Examples

The following example deletes the proxy `Catalog application proxy`.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_proxy
    @proxy_name = N'Catalog application proxy' ;
GO
```

## Related content

- [sp_add_proxy (Transact-SQL)](sp-add-proxy-transact-sql.md)
