---
title: "sys.sp_changedistributor_property (Transact-SQL)"
description: sp_changedistributor_property changes the properties of the Distributor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest, mathoma
ms.date: 06/19/2026
ms.service: sql
ms.subservice: replication
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sp_changedistributor_property_TSQL"
  - "sp_changedistributor_property"
helpviewer_keywords:
  - "sp_changedistributor_property"
dev_langs:
  - TSQL
---
# sys.sp_changedistributor_property (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the properties of the Distributor. This stored procedure is executed at the Distributor on any database. For remote Distributors, this stored procedure needs to be executed on all the Publisher servers that connect to the remote Distributor.

If the distribution or Publisher database is in an availability group, the stored procedure needs to be executed on *all* Distributor and Publisher nodes, regardless of their current role in the availability group.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_changedistributor_property
    [ [ @property = ] N'property' ]
    [ , [ @value = ] N'value' ]
[ ; ]
```

## Arguments

#### [ @property = ] N'*property*'

The property for a given Distributor. *@property* is **sysname**, and can be one of these values:

| Property name | Acceptable values | Description |
| --- | --- | --- |
| `heartbeat_interval` | Any **int** value (in minutes) | Maximum number of minutes that an agent can run without logging a progress message. *@heartbeat_interval* is **int**, with a default of `10` minutes. |
| `encrypt_distributor_connection` | `mandatory`, `optional`, `strict`, `true`, `false`, `yes`, `no` | Specifies the encryption type between the Distributor and other replication components.<br /><br />**Applies to:** [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions. |
| `trust_distributor_certificate` | `yes`, `no` | Specifies whether to trust the certificate used by the Distributor for encrypted connections. The default is `no`.<br /><br />**Applies to:** [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions. |
| `host_name_in_distributor_certificate` | Any string | Specifies the expected host name in the Distributor's certificate.<br /><br />**Applies to:** [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions. |
| `NULL` (default) | All available *@property* values are printed. |

[!INCLUDE [sql-25-repl-distributor-info](../../includes/sql-25-repl-distributor-info.md)]

#### [ @value = ] N'*value*'

The value for the given Distributor property. *@value* is **nvarchar(255)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changedistributor_property` is used in all types of replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-changedistributor-pro_1.sql":::

### Configure distributor to trust the self-signed certificate

To override the secure default of the OLEDB provider 19 and set `trust_distributor_certificate=yes` so the distributor trusts the self-signed certificate, use the following example:

```sql
EXECUTE sp_changedistributor_property
    @property = N'trust_distributor_certificate',
    @value = N'yes';
```

[!INCLUDE [sql-25-repl-distributor-info](../../includes/sql-25-repl-distributor-info.md)]

For more information, review the [remote distributor breaking change in SQL Server 2025](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md#replication-components-fail-after-an-upgrade).

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_changedistributor_property`.

## Related content

- [View and Modify Distributor and Publisher Properties](../replication/view-and-modify-distributor-and-publisher-properties.md)
- [sys.sp_adddistributor (Transact-SQL)](sp-adddistributor-transact-sql.md)
- [sys.sp_dropdistributor (Transact-SQL)](sp-dropdistributor-transact-sql.md)
- [sys.sp_helpdistributor (Transact-SQL)](sp-helpdistributor-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
