---
title: "sp_changedistributor_password (Transact-SQL)"
description: "Changes the password for a Distributor."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/08/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changedistributor_password"
  - "sp_changedistributor_password_TSQL"
helpviewer_keywords:
  - "sp_changedistributor_password"
dev_langs:
  - "TSQL"
---
# sp_changedistributor_password (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the password for a Distributor. This stored procedure is executed at the Distributor on any database. If this is a remote Distributor, then it needs to be run on all the Publisher servers that are using this Distributor. If the distribution or Publisher database is in an availability group, then it needs to be run on all the Distributor and Publisher nodes. It doesn't matter if the node is primary or secondary.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changedistributor_password [ @password = ] 'password'
[ ; ]
```

## Arguments

#### [ @password = ] '*password*'

The new password. *@password* is **sysname**, with no default. If the Distributor is local, the password of the `distributor_admin` system login is changed.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changedistributor_password` is used in all types of replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-changedistributor-pas_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_changedistributor_password`.

## See also

- [View and modify replication security settings](../replication/security/view-and-modify-replication-security-settings.md)
- [Secure the Distributor](../replication/security/secure-the-distributor.md)
- [sp_adddistributor (Transact-SQL)](sp-adddistributor-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
