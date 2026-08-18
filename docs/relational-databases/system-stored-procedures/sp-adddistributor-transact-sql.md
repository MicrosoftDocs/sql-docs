---
title: "sys.sp_adddistributor (Transact-SQL)"
description: Adds an entry in the sys.servers and marks the server entry as a Distributor, storing property information.
author: mashamsft
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/18/2026
ms.service: sql
ms.subservice: replication
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sp_adddistributor"
  - "sp_adddistributor_TSQL"
helpviewer_keywords:
  - "sp_adddistributor"
dev_langs:
  - TSQL
---
# sys.sp_adddistributor (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates an entry in the [sys.servers](../system-catalog-views/sys-servers-transact-sql.md) table (if there isn't one), marks the server entry as a Distributor, and stores property information. This stored procedure is executed at the Distributor on the `master` database to register and mark the server as a distributor. In the case of a remote distributor, it's also executed at the Publisher from the `master` database to register the remote distributor.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_adddistributor
    [ @distributor = ] N'distributor'
    [ , [ @heartbeat_interval = ] heartbeat_interval ]
    [ , [ @password = ] N'password' ]
    [ , [ @from_scripting = ] from_scripting ]
    [ , [ @security_mode = ] security_mode ]
    [ , [ @encrypt_distributor_connection = ] N'encrypt_distributor_connection' ]
    [ , [ @trust_distributor_certificate = ] N'trust_distributor_certificate' ]
    [ , [ @host_name_in_distributor_certificate = ] N'host_name_in_distributor_certificate' ]
    [ , [ @multi_subnet_failover = ] multi_subnet_failover ]
[ ; ]
```

## Arguments

#### [ @distributor = ] N'*distributor*'

The distribution server name. *@distributor* is **sysname**, with no default. This parameter is only used if setting up a remote Distributor. It adds entries for the Distributor properties in the `msdb..MSdistributor` table.

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15"
[!INCLUDE [custom-port](includes/custom-port.md)]

::: moniker-end

#### [ @heartbeat_interval = ] *heartbeat_interval*

The maximum number of minutes that an agent can go without logging a progress message. *@heartbeat_interval* is **int**, with a default of `10` minutes. A [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent job is created that runs on this interval to check the status of the replication agents that are running.

#### [ @password = ] N'*password*'

The password of the **distributor_admin** login. *@password* is **sysname**, with a default of `NULL`. If the password is `NULL` or an empty string, *@password* is reset to a random value. The password must be configured when the first remote distributor is added. **distributor_admin** login and *@password* are stored for linked server entry used for a *@distributor* RPC connection, including local connections. If *@distributor* is local, the password for **distributor_admin** is set to a new value. For Publishers with a remote Distributor, the same value for *@password* must be specified when executing `sp_adddistributor` at both the Publisher and Distributor. [sp_changedistributor_password](sp-changedistributor-password-transact-sql.md) can be used to change the Distributor password.

> [!IMPORTANT]  
> When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @from_scripting = ] *from_scripting*

*@from_scripting* is **bit**, with a default of `0`. [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @security_mode = ] *security_mode*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @encrypt_distributor_connection = ] N'*encrypt_distributor_connection*'

**Applies to:** [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

Determines whether the internal linked server connection from the publisher to distributor is encrypted. The values are mapped to the OLE DB provider's `Encrypt` property. *@encrypt_distributor_connection* is **nvarchar(10)**, and can't be `NULL`.

*@encrypt_distributor_connection* can be one of the following values:

- `mandatory` (default with Microsoft OLE DB provider 19)
- `no` or `false` (default with Microsoft OLE DB provider 18)
- `true` or `yes`
- `optional`
- `strict`

#### [ @trust_distributor_certificate = ] N'*trust_distributor_certificate*'

**Applies to:** [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

Indicates whether the distributor's TLS certificate should be trusted without validation. The value is mapped to the OLE DB provider's `TrustServerCertificate` property, and is typically used in conjunction with the `Mandatory` encryption setting when using self-signed certificates. *@trust_distributor_certificate* is **nvarchar(5)**, and can't be `NULL`.

*@trust_distributor_certificate* can be one of the following values:

- `no` (default)
- `yes`

[!INCLUDE [sql-25-repl-distributor-info](../../includes/sql-25-repl-distributor-info.md)]

#### [ @host_name_in_distributor_certificate = ] N'*host_name_in_distributor_certificate*'

**Applies to:** [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

Specifies the host name from the Distributor's certificate, when it's different from the Distributor name, such as when the IP address or DNS alias is used as the Distributor name. Leave the *@host_name_in_distributor_certificate* parameter empty if the host name in the certificate matches the Distributor name. *@host_name_in_distributor_certificate* is **nvarchar(255)** of any string value, with a default of `NULL`.

#### [ @multi_subnet_failover = ] *multi_subnet_failover*

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU 8 and later versions.

Used to pass information for the creation of the dynamic linked server between publisher and remote distributor. *@multi_subnet_failover* is **bit**, with a default of `0`.

Set the parameter value to `1` in replication topologies with a remote distributor accessed through an Always On availability group listener, or a multi-subnet failover cluster instance. This setting helps replication agents reconnect faster after failover and prevents connection timeouts. For more information, see  [Connecting with MultiSubnetFailover parameter](../../connect/oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md#connecting-with-multisubnetfailover).

- If `0`, the dynamic linked server isn't created with the `MultiSubnetFailover` parameter.
- If `1`, the dynamic linked server is created with the `MultiSubnetFailover` parameter as `1`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_adddistributor` is used in snapshot replication, transactional replication, and merge replication.

[!INCLUDE [sql-25-repl-info](../../includes/sql-25-repl-info.md)]

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-adddistributor-transa_1.sql":::

### Configure distributor to trust the self-signed certificate

To override the secure default of the OLEDB 19 provider and set `trust_distributor_certificate=yes` so the distributor trusts the self-signed certificate, use the following example:

```sql
EXECUTE sys.sp_adddistributor @trust_distributor_certificate = 'yes';
```

[!INCLUDE [sql-25-repl-distributor-info](../../includes/sql-25-repl-distributor-info.md)]

For more information, review the [remote distributor breaking change in SQL Server 2025](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md#adding-a-remote-replication-distributor-fails).

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_adddistributor`.

## Related content

- [Configure Publishing and Distribution](../replication/configure-publishing-and-distribution.md)
- [sp_changedistributor_property (Transact-SQL)](sp-changedistributor-property-transact-sql.md)
- [sp_dropdistributor (Transact-SQL)](sp-dropdistributor-transact-sql.md)
- [sp_helpdistributor (Transact-SQL)](sp-helpdistributor-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Configure Distribution](../replication/configure-distribution.md)
