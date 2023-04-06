---
title: "sp_serveroption (Transact-SQL)"
description: "sp_serveroption (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/06/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_serveroption_TSQL"
  - "sp_serveroption"
helpviewer_keywords:
  - "7343 (Database Engine error)"
  - "sp_serveroption"
dev_langs:
  - "TSQL"
---
# sp_serveroption (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Sets server options for remote servers and linked servers.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_serveroption [ @server = ] 'server'
      , [ @optname = ] 'option_name'
      , [ @optvalue = ] 'option_value' ;
```

## Arguments

#### [ @server = ] 'server'

The name of the server for which to set the option. *server* is **sysname**, with no default.

#### [ @optname = ] 'option_name'

The option to set for the specified server. *option_name* is **varchar(35)**, with no default. *option_name* can be any of the following values.

| Value | Description |
| --- | --- |
| **collation compatible** | Affects distributed query execution against linked servers. If this option is set to **true**, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assumes that all characters in the linked server are compatible with the local server, regarding character set and collation sequence (or sort order). This enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to send comparisons on character columns to the provider. If this option isn't set, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always evaluates comparisons on character columns locally.<br /><br />This option should be set only if it is certain that the data source corresponding to the linked server has the same character set and sort order as the local server. |
| **collation name** | Specifies the name of the collation used by the remote data source if **use remote collation** is **true** and the data source isn't a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source. The name must be one of the collations supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />Use this option when accessing an OLE DB data source other than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but whose collation matches one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations.<br /><br />The linked server must support a single collation to be used for all columns in that server. Don't set this option if the linked server supports multiple collations within a single data source, or if the linked server's collation can't be determined to match one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations. |
| **connect timeout** | Timeout value in seconds for connecting to a linked server.<br /><br />If **0**, the **connect timeout** setting uses the default value that is configured for the `remote login timeout (s)` setting. The default value for `remote login timeout (s)` is **10**.<br /><br />You can view this setting from the `sys.configurations` catalog view with the following query: `SELECT name, value_in_use FROM sys.configurations WHERE name like 'remote login timeout (s)';`. |
| **data access** | Enables and disables a linked server for distributed query access. Can be used only for `sys.server` entries added through `sp_addlinkedserver`. |
| **dist** | Distributor. |
| **lazy schema validation** | Determines whether the schema of remote tables is checked.<br /><br />If **true**, skip schema checking of remote tables at the beginning of the query. |
| **pub** | Publisher. |
| **query timeout** | Time-out value for queries against a linked server.<br /><br />If **0**, use the `sp_configure` default. |
| **rpc** | Enables RPC from the given server. |
| **rpc out** | Enables RPC to the given server. |
| **sub** | Subscriber. |
| **system** | [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| **use remote collation** | Determines whether the collation of a remote column or of a local server is used.<br /><br />If **true**, the collation of remote columns is used for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data sources, and the collation specified in **collation name** is used for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data sources. This is the default.<br /><br />If **false**, distributed queries always use the default collation of the local server, while **collation name** and the collation of remote columns are ignored. |
| **remote proc transaction promotion** | Use this option to protect the actions of a server-to-server procedure through a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) transaction. When this option is **true** (or **on**), calling a remote stored procedure starts a distributed transaction and enlists the transaction with MS DTC. The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] making the remote stored procedure call is the transaction originator and controls the completion of the transaction. When a subsequent COMMIT TRANSACTION or ROLLBACK TRANSACTION statement is issued for the connection, the controlling instance requests that MS DTC manages the completion of the distributed transaction across the computers involved.<br /><br />After a [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction has been started, remote stored procedure calls can be made to other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that have been defined as linked servers. The linked servers are all enlisted in the [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction, and MS DTC ensures that the transaction is completed against each linked server.<br /><br />If this option is set to **false** (or **off**), a local transaction isn't promoted to a distributed transaction while calling a remote procedure call on a linked server.<br /><br />If before making a server-to-server procedure call, the transaction is already a distributed transaction, then this option has no effect. The procedure call against linked server runs under the same distributed transaction.<br /><br />If before making a server-to-server procedure call, there is no transaction active in the connection, then this option has no effect. The procedure then runs against linked server without active transactions.<br /><br />The default value for this option is **true** (or **on**). |

#### [ @optvalue = ] 'option_value'

Specifies whether or not the *option_name* should be enabled (**true** or **on**) or disabled (**false** or **off**). *option_value* is **varchar(10)**, with no default.

*option_value* may be a non-negative integer for the **connect timeout** and **query timeout** options. For the **collation name** option, *option_value* may be a collation name or NULL.

## Return code values

0 (success) or 1 (failure)

## Remarks

If the **collation compatible** option is set to **true**, **collation name** automatically is set to NULL. If **collation name** is set to a non-null value, **collation compatible** automatically is set to **false**.

## Permissions

Requires ALTER ANY LINKED SERVER permission on the server.

## Examples

The following example configures a linked server corresponding to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], `SEATTLE3`, to be collation compatible with the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
USE master;
GO
EXEC sp_serveroption 'SEATTLE3', 'collation compatible', 'true';
GO
```

## See also

- [Distributed Queries Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/distributed-queries-stored-procedures-transact-sql.md)
- [sp_adddistpublisher (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md)
- [sp_addlinkedserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [sp_dropdistpublisher (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md)
- [sp_helpserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)
- [System Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
