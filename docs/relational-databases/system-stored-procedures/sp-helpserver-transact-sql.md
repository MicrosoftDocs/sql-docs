---
title: "sp_helpserver (Transact-SQL)"
description: sp_helpserver reports information about a particular remote or replication server, or about all servers of both types.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpserver"
  - "sp_helpserver_TSQL"
helpviewer_keywords:
  - "sp_helpserver"
dev_langs:
  - "TSQL"
---
# sp_helpserver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports information about a particular remote or replication server, or about all servers of both types. Provides the server name, the network name of the server, the replication status of the server, the identification number of the server, and the collation name. Also provides time-out values for connecting to, or queries against, linked servers.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpserver
    [ [ @server = ] N'server' ]
    [ , [ @optname = ] 'optname' ]
    [ , [ @show_topology = ] 'show_topology' ]
[ ; ]
```

## Arguments

#### [ @server = ] N'*server*'

Specifies the server about which information is reported. *@server* is **sysname**, with a default of `NULL`. When *server* isn't specified, returns information about all servers in `master.sys.servers`.

#### [ @optname = ] '*optname*'

The option describing the server. *@optname* is **varchar(35)**, and must be one of these values.

| Value | Description |
| --- | --- |
| `collation compatible` | Affects the Distributed Query execution against linked servers. If this option is set to true, |
| `data access` | Enables and disables a linked server for distributed query access. |
| `dist` | Distributor. |
| `dpub` | Remote Publisher to this Distributor. |
| `lazy schema validation` | Skips schema checking of remote tables at the beginning of the query. |
| `pub` | Publisher. |
| `rpc` | Enables RPC from the specified server. |
| `rpc out` | Enables RPC to the specified server. |
| `sub` | Subscriber. |
| `system` | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| `use remote collation` | Uses the collation of a remote column instead of the local server's collation. |

#### [ @show_topology = ] '*show_topology*'

The relationship of the specified server to other servers. *@show_topology* is **varchar(1)**, with a default of `NULL`. If *@show_topology* isn't equal to `t` or is `NULL`, `sp_helpserver` returns columns listed in the Result Sets section. If *@show_topology* is equal to `t`, in addition to the columns listed in the [result set](#result-set), `sp_helpserver` also returns `topx` and `topy` information.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Server name. |
| `network_name` | **sysname** | Network name of the server. |
| `status` | **varchar(70)** | Server status. |
| `id` | **char(4)** | Identification number of the server. |
| `collation_name` | **sysname** | Collation of the server. |
| `connect_timeout` | **int** | Time-out value for connecting to linked server. |
| `query_timeout` | **int** | Time-out value for queries against linked server. |

## Remarks

A server can have more than one status.

## Permissions

No permissions are checked.

## Examples

### A. Display information about all servers

The following example displays information about all servers by using `sp_helpserver` with no parameters.

```sql
USE master;
GO
EXEC sp_helpserver;
```

### B. Display information about a specific server

The following example displays all information about the `SEATTLE2` server.

```sql
USE master;
GO
EXEC sp_helpserver 'SEATTLE2';
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sp_adddistpublisher (Transact-SQL)](sp-adddistpublisher-transact-sql.md)
- [sp_addserver (Transact-SQL)](sp-addserver-transact-sql.md)
- [sp_addsubscriber (Transact-SQL)](sp-addsubscriber-transact-sql.md)
- [sp_changesubscriber (Transact-SQL)](sp-changesubscriber-transact-sql.md)
- [sp_dropserver (Transact-SQL)](sp-dropserver-transact-sql.md)
- [sp_dropsubscriber (Transact-SQL)](sp-dropsubscriber-transact-sql.md)
- [sp_helpdistributor (Transact-SQL)](sp-helpdistributor-transact-sql.md)
- [sp_helpremotelogin (Transact-SQL)](sp-helpremotelogin-transact-sql.md)
- [sp_helpsubscriberinfo (Transact-SQL)](sp-helpsubscriberinfo-transact-sql.md)
- [sp_serveroption (Transact-SQL)](sp-serveroption-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
