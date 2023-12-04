---
title: "sp_dropdistributor (Transact-SQL)"
description: Uninstalls the Distributor. This stored procedure is executed at the Distributor on any database except the distribution database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropdistributor"
  - "sp_dropdistributor_TSQL"
helpviewer_keywords:
  - "sp_dropdistributor"
dev_langs:
  - "TSQL"
---
# sp_dropdistributor (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Uninstalls the Distributor. This stored procedure is executed at the Distributor on any database except the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropdistributor
    [ [ @no_checks = ] no_checks ]
    [ , [ @ignore_distributor = ] ignore_distributor ]
[ ; ]
```

## Arguments

#### [ @no_checks = ] *no_checks*

Indicates whether to check for dependent objects before dropping the Distributor. *@no_checks* is **bit**, with a default of `0`.

- If `0`, `sp_dropdistributor` checks to make sure that all publishing and distribution objects were dropped, in addition to the Distributor.

- If `1`, `sp_dropdistributor` drops all the publishing and distribution objects before uninstalling the distributor.

#### [ @ignore_distributor = ] *ignore_distributor*

Indicates whether this stored procedure is executed without connecting to the Distributor. *@ignore_distributor* is **bit**, with a default of `0`.

- If `0`, `sp_dropdistributor` connects to the Distributor and removes all replication objects. If `sp_dropdistributor` is unable to connect to the Distributor, the stored procedure fails.

- If `1`, no connection is made to the Distributor and the replication objects aren't removed. This option is used if the Distributor is being uninstalled or is permanently offline. The objects for this Publisher at the Distributor aren't removed until the Distributor is reinstalled at some future time.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropdistributor` is used in all types of replication.

If other Publisher or distribution objects exist on the server, `sp_dropdistributor` fails unless *@no_checks* is set to `1`.

This stored procedure must be executed after dropping the distribution database by executing `sp_dropdistributiondb`.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-dropdistributor-trans_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_dropdistributor`.

## Related content

- [Disable Publishing and Distribution](../replication/disable-publishing-and-distribution.md)
- [sp_adddistributor (Transact-SQL)](sp-adddistributor-transact-sql.md)
- [sp_changedistributor_property (Transact-SQL)](sp-changedistributor-property-transact-sql.md)
- [sp_helpdistributor (Transact-SQL)](sp-helpdistributor-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
