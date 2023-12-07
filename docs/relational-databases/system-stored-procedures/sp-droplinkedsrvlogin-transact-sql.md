---
title: "sp_droplinkedsrvlogin (Transact-SQL)"
description: Removes an existing mapping between a login on the local server running SQL Server, and a login on the linked server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_droplinkedsrvlogin_TSQL"
  - "sp_droplinkedsrvlogin"
helpviewer_keywords:
  - "sp_droplinkedsrvlogin"
dev_langs:
  - "TSQL"
---
# sp_droplinkedsrvlogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes an existing mapping between a login on the local server running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and a login on the linked server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_droplinkedsrvlogin
    [ @rmtsrvname = ] N'rmtsrvname'
    , [ @locallogin = ] N'locallogin'
[ ; ]
```

## Arguments

#### [ @rmtsrvname = ] N'*rmtsrvname*'

The name of a linked server that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login mapping applies to. *@rmtsrvname* is **sysname**, with no default.

#### [ @locallogin = ] N'*locallogin*'

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login on the local server that's a mapping to the linked server *@rmtsrvname*. *@locallogin* is **sysname**, with no default. A mapping for *@locallogin* to *@rmtsrvname* must already exist. If `NULL`, the default mapping created by `sp_addlinkedserver`, which maps all logins on the local server to logins on the linked server, is deleted.

## Return code values

`0` (success) or `1` (failure).

## Remarks

When the existing mapping for a login is deleted, the local server uses the default mapping created by `sp_addlinkedserver` when it connects to the linked server on behalf of that login. To change the default mapping, use `sp_addlinkedsrvlogin`.

If the default mapping is also deleted, only logins that were explicitly given a login mapping to the linked server, by using `sp_addlinkedsrvlogin`, can access the linked server.

`sp_droplinkedsrvlogin` can't be executed from within a user-defined transaction.

## Permissions

Requires `ALTER ANY LOGIN` permission on the server.

## Examples

### A. Remove the login mapping for an existing user

The following example removes the mapping for the login `Mary` from the local server to the linked server `Accounts`. Therefore, login `Mary` uses the default login mapping.

```sql
EXEC sp_droplinkedsrvlogin 'Accounts', 'Mary';
```

### B. Remove the default login mapping

The following example removes the default login mapping originally created by executing `sp_addlinkedserver` on the linked server `Accounts`.

```sql
EXEC sp_droplinkedsrvlogin 'Accounts', NULL;
```

## Related content

- [sp_addlinkedserver (Transact-SQL)](sp-addlinkedserver-transact-sql.md)
- [sp_addlinkedsrvlogin (Transact-SQL)](sp-addlinkedsrvlogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
