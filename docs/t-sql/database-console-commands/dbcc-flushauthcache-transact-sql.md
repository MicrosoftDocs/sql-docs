---
title: DBCC FLUSHAUTHCACHE (Transact-SQL)
description: DBCC FLUSHAUTHCACHE empties the database authentication cache containing information about logins and firewall rules, for the current user database in Azure SQL Database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/13/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DBCC FLUSHAUTHCACHE"
  - "FLUSHAUTHCACHE"
  - "DBCC_FLUSHAUTHCACHE_TSQL"
  - "FLUSHAUTHCACHE_TSQL"
helpviewer_keywords:
  - "DBCC FLUSHAUTHCACHE"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = fabric-sqldb"
---

# DBCC FLUSHAUTHCACHE (Transact-SQL)

[!INCLUDE [Azure SQL Database FabricSQLDB](../../includes/applies-to-version/asdb-fabricsqldb.md)]

Empties the database authentication cache containing information about logins and firewall rules, for the current user database. Additionally, it clears all cached Microsoft Entra group membership data stored in the database.

`DBCC FLUSHAUTHCACHE` doesn't apply to the logical `master` database, because the `master` database contains the physical storage for the information about logins and firewall rules.

The user executing the statement and other currently connected users remain connected.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC FLUSHAUTHCACHE
[;]
```

## Arguments

None.

## Remarks

The authentication cache makes a copy of logins and server firewall rules stored in the `master` database, and places them in memory in the user database. 

A user with the **KILL DATABASE CONNECTION** permission can explicitly terminate a connection to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] by using the [KILL (Transact-SQL)](../../t-sql/language-elements/kill-transact-sql.md) command.

 `DBCC FLUSHAUTHCACHE` is not supported for [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].
 
## Permissions

Requires the **KILL DATABASE CONNECTION** permission [!INCLUDE[ssSDS](../../includes/sssds-md.md)] or the admin account.

## Example

The following statement clears the authentication cache for the current database.

```sql
DBCC FLUSHAUTHCACHE;
```

## Related content

- [DBCC (Transact-SQL)](dbcc-transact-sql.md)
