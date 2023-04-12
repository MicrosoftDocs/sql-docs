---
title: DBCC FLUSHAUTHCACHE (Transact-SQL)
description: DBCC FLUSHAUTHCACHE empties the database authentication cache containing information about logins and firewall rules, for the current user database in Azure SQL Database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC FLUSHAUTHCACHE"
  - "FLUSHAUTHCACHE"
  - "DBCC_FLUSHAUTHCACHE_TSQL"
  - "FLUSHAUTHCACHE_TSQL"
helpviewer_keywords:
  - "DBCC FLUSHAUTHCACHE"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current"
---

# DBCC FLUSHAUTHCACHE (Transact-SQL)

[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Empties the database authentication cache containing information about logins  and firewall rules, for the current user database in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].

`DBCC FLUSHAUTHCACHE` doesn't apply to the logical `master` database, because the `master` database contains the physical storage for the information about logins and firewall rules.

The user executing the statement and other currently connected users remain connected. (`DBCC FLUSHAUTHCACHE` isn't currently supported for [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].)

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC FLUSHAUTHCACHE
[;]
```

## Arguments

None.

## Remarks

The authentication cache makes a copy of logins and server firewall rules stored in the `master` database, and places them in memory in the user database. Since information about contained database users is already stored in the user database, contained database users aren't part of the authentication cache.

Continuously active connections to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] require reauthorization (performed by the [!INCLUDE[ssDE](../../includes/ssde-md.md)]) at least every 10 hours. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] attempts reauthorization using the originally submitted password and no user input is required. For performance reasons, when a password is reset in [!INCLUDE[ssSDS](../../includes/sssds-md.md)], the connection won't be reauthenticated, even if the connection is reset because of connection pooling. This behavior is different from the behavior of on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the password has changed since the connection was initially authorized, the connection must be terminated and a new connection made using the new password.

A user with the **KILL DATABASE CONNECTION** permission can explicitly terminate a connection to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] by using the [KILL (Transact-SQL)](../../t-sql/language-elements/kill-transact-sql.md) command.

## Permissions

Requires the **KILL DATABASE CONNECTION** permission [!INCLUDE[ssSDS](../../includes/sssds-md.md)] or the admin account.

## Example

The following statement clears the authentication cache for the current database.

```sql
DBCC FLUSHAUTHCACHE;
```

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
