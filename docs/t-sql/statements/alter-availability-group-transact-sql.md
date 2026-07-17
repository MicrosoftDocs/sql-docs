---
title: "ALTER AVAILABILITY GROUP (Transact-SQL)"
description: ALTER AVAILABILITY GROUP (Transact-SQL)
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 01/02/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - build-2025
f1_keywords:
  - "ALTER_AVAILABILITY_GROUP_TSQL"
  - "ALTER_AVAILABILITY_TSQL"
  - "ALTER AVAILABILITY GROUP"
  - "ALTER AVAILABILITY"
helpviewer_keywords:
  - "Availability Groups [SQL Server], availability replicas"
  - "ALTER AVAILABILITY GROUP statement"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], Transact-SQL statements"
dev_langs:
  - "TSQL"
---

# ALTER AVAILABILITY GROUP (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Alters an existing Always On availability group in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Most `ALTER AVAILABILITY GROUP` arguments are supported only on the current primary replica. However, the `JOIN`, `FAILOVER`, and `FORCE_FAILOVER_ALLOW_DATA_LOSS` arguments are supported only on secondary replicas.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ALTER AVAILABILITY GROUP group_name
  {
     SET ( <set_option_spec> )
   | ADD DATABASE database_name
   | REMOVE DATABASE database_name
   | ADD REPLICA ON <add_replica_spec>
   | MODIFY REPLICA ON <modify_replica_spec>
   | REMOVE REPLICA ON <server_instance>
   | JOIN
   | JOIN AVAILABILITY GROUP ON <add_availability_group_spec> [ , ...2 ]
   | MODIFY AVAILABILITY GROUP ON <modify_availability_group_spec> [ , ...2 ]
   | GRANT CREATE ANY DATABASE
   | DENY CREATE ANY DATABASE
   | FAILOVER
   | FORCE_FAILOVER_ALLOW_DATA_LOSS
   | ADD LISTENER 'dns_name' ( <add_listener_option> )
   | MODIFY LISTENER 'dns_name' ( <modify_listener_option> )
   | RESTART LISTENER 'dns_name'
   | REMOVE LISTENER 'dns_name'
   | OFFLINE
  }
[ ; ]

<set_option_spec> ::=
    AUTOMATED_BACKUP_PREFERENCE = { PRIMARY | SECONDARY_ONLY | SECONDARY | NONE }
  | FAILURE_CONDITION_LEVEL  = { 1 | 2 | 3 | 4 | 5 }
  | HEALTH_CHECK_TIMEOUT = milliseconds
  | DB_FAILOVER  = { ON | OFF }
  | DTC_SUPPORT  = { PER_DB | NONE }
  | REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = { integer }
  | ROLE = SECONDARY
  | CLUSTER_CONNECTION_OPTIONS = 'key_value_pairs> [ ;... ] '

<server_instance> ::=
 { 'system_name [ \instance_name ] ' | 'FCI_network_name [ \instance_name ] ' }

<add_replica_spec>::=
  <server_instance> WITH
    (
       ENDPOINT_URL = 'TCP://system-address:port' ,
       AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT | CONFIGURATION_ONLY } ,
       FAILOVER_MODE = { AUTOMATIC | MANUAL }
       [ , <add_replica_option> [ , ...n ] ]
    )

  <add_replica_option>::=
       SEEDING_MODE = { AUTOMATIC | MANUAL }
     | BACKUP_PRIORITY = n
     | SECONDARY_ROLE ( {
            [ ALLOW_CONNECTIONS = { NO | READ_ONLY | ALL } ]
        [ , ] [ READ_ONLY_ROUTING_URL = 'TCP://system-address:port' ]
     } )
     | PRIMARY_ROLE ( {
            [ ALLOW_CONNECTIONS = { READ_WRITE | ALL } ]
        [ , ] [ READ_ONLY_ROUTING_LIST = { ( '<server_instance>' [ , ...n ] ) | NONE } ]
        [ , ] [ READ_WRITE_ROUTING_URL = 'TCP://system-address:port' ]
     } )
     | SESSION_TIMEOUT = integer

<modify_replica_spec>::=
  <server_instance> WITH
    (
       ENDPOINT_URL = 'TCP://system-address:port'
     | AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT }
     | FAILOVER_MODE = { AUTOMATIC | MANUAL }
     | SEEDING_MODE = { AUTOMATIC | MANUAL }
     | BACKUP_PRIORITY = n
     | SECONDARY_ROLE ( {
          [ ALLOW_CONNECTIONS = { NO | READ_ONLY | ALL }  ]
        | [ READ_ONLY_ROUTING_URL = { 'TCP://system-address:port' | NONE } ]
          } )
     | PRIMARY_ROLE ( {
          [ ALLOW_CONNECTIONS = { READ_WRITE | ALL }   ]
        | [ READ_ONLY_ROUTING_LIST = { ( '<server_instance>' [ , ...n ] ) | NONE } ]
        | [ READ_WRITE_ROUTING_URL = { 'TCP://system-address:port' | NONE }  ]
          } )
     | SESSION_TIMEOUT = seconds
    )

<add_availability_group_spec>::=
 <ag_name> WITH
    (
       LISTENER_URL = 'TCP://system-address:port' ,
       AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT } ,
       FAILOVER_MODE = MANUAL ,
       SEEDING_MODE = { AUTOMATIC | MANUAL }
    )

<modify_availability_group_spec>::=
 <ag_name> WITH
    (
       LISTENER = 'TCP://system-address:port'
       | AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT }
       | SEEDING_MODE = { AUTOMATIC | MANUAL }
    )

<add_listener_option> ::=
   {
      WITH DHCP [ ON ( <network_subnet_option> ) ]
    | WITH IP ( { ( <ip_address_option> ) } [ , ...n ] ) [ , PORT = listener_port ]
   }

  <network_subnet_option> ::=
     'ipv4_address' , 'ipv4_mask'

  <ip_address_option> ::=
     {
        'four_part_ipv4_address' , 'four_part_ipv4_mask'
      | 'ipv6_address'
     }

<modify_listener_option>::=
    {
       ADD IP ( <ip_address_option> )
     | PORT = listener_port
     | REMOVE IP ( 'ipv4_address' | 'ipv6_address')
    }
```

## Arguments

#### *group_name*

Specifies the name of the new availability group. *group_name* must be a valid [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] identifier, and it must be unique across all availability groups in the WSFC cluster.

#### AUTOMATED_BACKUP_PREFERENCE = { PRIMARY | SECONDARY_ONLY| SECONDARY | NONE }

Specifies a preference about how a backup job evaluates the primary replica when choosing where to perform backups. You can script a given backup job to take the automated backup preference into account. It's important to understand that the preference isn't enforced by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], so it has no effect on ad hoc backups.

Supported only on the primary replica.

The values are as follows:

#### PRIMARY

Specifies that the backups always occur on the primary replica. This option is useful if you need backup features, such as creating differential backups, that aren't supported when backup runs on a secondary replica.

> [!IMPORTANT]  
> If you plan to use log shipping to prepare any secondary databases for an availability group, set the automated backup preference to `Primary` until all the secondary databases are prepared and joined to the availability group.

#### SECONDARY_ONLY

Specifies that backups never occur on the primary replica. If the primary replica is the only replica online, the backup doesn't occur.

#### SECONDARY

Specifies that backups occur on a secondary replica except when the primary replica is the only replica online. In that case, the backup occurs on the primary replica. This is the default behavior.

#### NONE

Specifies that you prefer that backup jobs ignore the role of the availability replicas when choosing the replica to perform backups. Note backup jobs might evaluate other factors such as backup priority of each availability replica in combination with its operational state and connected state.

> [!IMPORTANT]  
> There's no enforcement of the `AUTOMATED_BACKUP_PREFERENCE` setting. The interpretation of this preference depends on the logic, if any, that you script into backup jobs for the databases in a given availability group. The automated backup preference setting has no effect on ad hoc backups. For more information, see [Configure backups on secondary replicas of an Always On availability group](../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md).

> [!NOTE]  
> To view the automated backup preference of an existing availability group, select the `automated_backup_preference` or `automated_backup_preference_desc` column of the [sys.availability_groups](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md) catalog view. Additionally, [sys.fn_hadr_backup_is_preferred_replica](../../relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql.md) can be used to determine the preferred backup replica. This function always returns `1` for at least one of the replicas, even when `AUTOMATED_BACKUP_PREFERENCE = NONE`.

#### FAILURE_CONDITION_LEVEL = { 1 | 2 | 3 | 4 | 5 }

Specifies what failure conditions trigger an automatic failover for this availability group. `FAILURE_CONDITION_LEVEL` is set at the group level but is relevant only on availability replicas that are configured for synchronous-commit availability mode (`AVAILABILITY_MODE = SYNCHRONOUS_COMMIT`). Furthermore, failure conditions can trigger an automatic failover only if both the primary and secondary replicas are configured for automatic failover mode (`FAILOVER_MODE = AUTOMATIC`) and the secondary replica is currently synchronized with the primary replica.

Supported only on the primary replica.

The failure-condition levels (1-5) range from the least restrictive, level 1, to the most restrictive, level 5. A given condition level encompasses all of the less restrictive levels. Thus, the strictest condition level, 5, includes the four less restrictive condition levels (1-4), level 4 includes levels 1-3, and so forth. The following table describes the failure condition that corresponds to each level.

| Level | Failure Condition |
| --- | --- |
| 1 | Specifies that an automatic failover initiates when any of the following occurs:<br /><br />The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service is down.<br /><br />The lease of the availability group for connecting to the WSFC cluster expires because no `ACK` is received from the server instance. For more information, see [How It Works: SQL Server Always On Lease Timeout](https://techcommunity.microsoft.com/blog/sqlserversupport/how-it-works-sql-server-alwayson-lease-timeout/317268). |
| 2 | Specifies that an automatic failover initiates when any of the following occurs:<br /><br />The instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't connect to cluster, and the user-specified `HEALTH_CHECK_TIMEOUT` threshold of the availability group is exceeded.<br /><br />The availability replica is in failed state. |
| 3 | Specifies that an automatic failover initiates on critical [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] internal errors, such as orphaned spinlocks, serious write-access violations, or too much dumping.<br /><br />This is the default behavior. |
| 4 | Specifies that an automatic failover initiates on moderate [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] internal errors, such as a persistent out-of-memory condition in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] internal resource pool. |
| 5 | Specifies that an automatic failover initiates on any qualified failure conditions, including:<br /><br />Exhaustion of SQL Engine worker-threads.<br /><br />Detection of an unsolvable deadlock. |

> [!NOTE]  
> Lack of response by an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to client requests isn't relevant to availability groups.

The `FAILURE_CONDITION_LEVEL` and `HEALTH_CHECK_TIMEOUT` values, define a *flexible failover policy* for a given group. This flexible failover policy provides you with granular control over what conditions must cause an automatic failover. For more information, see [Configure a flexible automatic failover policy for an Always On availability group](../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md).

#### `HEALTH_CHECK_TIMEOUT` = *milliseconds*

Specifies the wait time, in milliseconds, for the [sp_server_diagnostics](../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) system stored procedure to return server-health information before WSFC cluster assumes that the server instance is slow or not responding. Set `HEALTH_CHECK_TIMEOUT` at the group level, but it's relevant only on availability replicas that you configure for synchronous-commit availability mode with automatic failover (`AVAILABILITY_MODE = SYNCHRONOUS_COMMIT`). Furthermore, a health-check timeout can trigger an automatic failover only if both the primary and secondary replicas are configured for automatic failover mode (`FAILOVER_MODE = AUTOMATIC`) and the secondary replica is currently synchronized with the primary replica.

The default `HEALTH_CHECK_TIMEOUT` value is 30,000 milliseconds (30 seconds). The minimum value is 15,000 milliseconds (15 seconds), and the maximum value is 4,294,967,295 milliseconds.

Supported only on the primary replica.

> [!IMPORTANT]  
> `sp_server_diagnostics` doesn't perform health checks at the database level.

#### DB_FAILOVER = { ON | OFF }

Specifies the response to take when a database on the primary replica is offline. When set to `ON`, any status other than `ONLINE` for a database in the availability group triggers an automatic failover. When you set this option to `OFF`, only the health of the instance triggers automatic failover.

For more information regarding this setting, see [Availability group database level health detection failover option](../../database-engine/availability-groups/windows/sql-server-always-on-database-health-detection-failover-option.md).

#### DTC_SUPPORT = { PER_DB | NONE }

Specifies whether distributed transactions are enabled for this availability group. Distributed transactions are only supported for availability group databases beginning in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], and cross-database transactions are only supported beginning in [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] SP2. `PER_DB` creates the availability group with support for these transactions, and automatically promotes cross-database transactions involving databases in the availability group into distributed transactions. `NONE` prevents the automatic promotion of cross-database transactions to distributed transactions and doesn't register the database with a stable RMID in DTC. Distributed transactions aren't prevented when the `NONE` setting is used, but database failover and automatic recovery might not succeed under some circumstances. For more information, see [Transactions - availability groups and database mirroring](../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md).

> [!NOTE]  
> Support for changing the `DTC_SUPPORT` setting of an availability group was introduced in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Service Pack 2. This option can't be used with earlier versions. To change this setting in earlier versions of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], you must `DROP` and `CREATE` the availability group again.

> [!IMPORTANT]  
> DTC has a limit of 32 enlistments per distributed transaction. Because each database within an availability group enlists with the DTC separately, if your transaction involves more than 32 databases, you can get the following error when [!INCLUDE [SQLServer](../../includes/ssnoversion-md.md)] attempts to enlist the 33rd database:
>
> ```output
> Enlist operation failed: 0x8004d101(XACT_E_TOOMANY_ENLISTMENTS). SQL Server could not register with Microsoft Distributed Transaction Coordinator (MS DTC) as a resource manager for this transaction. The transaction may have been stopped by the client or the resource manager.
> ```

For more detail on distributed transactions in [!INCLUDE [SQLServer](../../includes/ssnoversion-md.md)], see [Distributed transactions](../../database-engine/availability-groups/windows/configure-availability-group-for-distributed-transactions.md#distTran).

#### REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT

Introduced in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. Sets a minimum number of synchronous secondary replicas required to commit before the primary replica commits a transaction. Guarantees that SQL Server transactions wait until the transaction logs are updated on the minimum number of secondary replicas.

- Default: 0. Provides same behavior as [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)].
- Minimum: 0.
- Maximum: Number of replicas minus 1.

`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` relates to replicas in synchronous commit mode. When replicas are in synchronous commit mode, writes on the primary replica wait until writes on synchronous replicas commit to the replica database transaction log. If a SQL Server that hosts a secondary synchronous replica stops responding, the SQL Server that hosts the primary replica marks that secondary replica as `NOT SYNCHRONIZED` and proceeds. When the unresponsive database comes back online it is in a "not synced" state and the replica is marked as unhealthy until the primary can synchronize it again. This setting guarantees that the primary replica doesn't proceed until the minimum number of replicas have committed each transaction. If the minimum number of replicas isn't available, then commits on the primary fail. For cluster type `EXTERNAL` the setting is changed when the availability group is added to a cluster resource. See [High availability and data protection for availability group configurations](../../linux/sql-server-linux-availability-group-ha.md).

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can set `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` on a distributed availability group. This setting isn't supported for `CREATE AVAILABILITY GROUP`. You can use `ALTER AVAILABILITY GROUP` to set `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT`. For example:

   ```sql
   ALTER AVAILABILITY GROUP [<name>]
     SET (REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = <integer>);
   ```

#### ROLE

The only valid parameter is `SECONDARY`, and this `SET` option is only valid in distributed availability groups. Use it to fail over a [distributed availability group](../../database-engine/availability-groups/windows/configure-distributed-availability-groups.md).

#### CLUSTER_CONNECTION_OPTIONS

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions

Use the `CLUSTER_CONNECTION_OPTIONS` clause to enforce [TLS 1.3](../../relational-databases/security/networking/tls-1-3.md) encryption for communication between the Windows Server Failover Cluster and your availability group replicas. Specify the options as a list of key-value pairs, separated by semicolons. Use the key-value pairs to configure connection string encryption for the availability group.

To revert back to default encryption, set the `CLUSTER_CONNECTION_OPTIONS` clause to an empty string. [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] defaults to `Encrypt=Mandatory`, and `TrustServerCertificate=Yes` for connections to availability group replicas and listeners.

For more information, review [connect to an availability group with strict encryption](../../relational-databases/security/networking/connect-with-strict-encryption.md#connect-to-an-always-on-availability-group) and [TDS 8.0](../../relational-databases/security/networking/tds-8.md).

The following table describes the key-value pairs that you can use in the `CLUSTER_CONNECTION_OPTIONS` clause:

[!INCLUDE [cluster-connection-options](../../includes/cluster-connection-options.md)]

Check the [examples](#c-force-encryption-in-connections-to-the-availability-group) to learn how to use the `CLUSTER_CONNECTION_OPTIONS` clause.

#### ADD DATABASE *database_name*

Specifies a list of one or more user databases that you want to add to the availability group. These databases must reside on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the current primary replica. You can specify multiple databases for an availability group, but each database can belong to only one availability group. For information about the type of databases that an availability group can support, see [Prerequisites, restrictions, and recommendations for Always On availability groups](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md). To find out which local databases already belong to an availability group, see the `replica_id` column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

Supported only on the primary replica.

> [!NOTE]  
> After you create the availability group, you need to connect to each server instance that hosts a secondary replica. Then prepare each secondary database and join it to the availability group. For more information, see [Start Data Movement on an Always On Secondary Database (SQL Server)](../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).

#### REMOVE DATABASE *database_name*

Removes the specified primary database and the corresponding secondary databases from the availability group. Supported only on the primary replica.

For information about recommended steps after removing an availability database from an availability group, see [Remove a primary database from an Always On availability group](../../database-engine/availability-groups/windows/remove-a-primary-database-from-an-availability-group-sql-server.md).

#### ADD REPLICA ON

Specifies from one to eight SQL Server instances to host secondary replicas in an availability group. Each replica is specified by its server instance address followed by a `WITH (...)` clause.

Supported only on the primary replica.

You need to join every new secondary replica to the availability group. For more information, see the description of the `JOIN` option, later in this section.

#### \<server_instance>

Specifies the address of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that's the host for a replica. The address format depends on whether the instance is the default instance or a named instance and whether it's a standalone instance or a failover cluster instance (FCI). The syntax is as follows:

{ '*system_name*[\\*instance_name*]' | '*FCI_network_name*[\\*instance_name*]' }

The components of this address are as follows:

#### *system_name*

The NetBIOS name of the computer system on which the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resides. This computer must be a WSFC node.

#### *FCI_network_name*

The network name that you use to access a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster. Use this name if the server instance participates as a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover partner. Executing `SELECT @@SERVERNAME` on an FCI server instance returns its entire '*FCI_network_name*[\\*instance_name*]' string (which is the full replica name).

For more information, see [@@SERVERNAME](../functions/servername-transact-sql.md).

#### *instance_name*

The name of an instance of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that *system_name* or *FCI_network_name* hosts and that has Always On enabled. For a default server instance, *instance_name* is optional. The instance name is case insensitive. On a stand-alone server instance, this value name is the same as the value returned by executing `SELECT @@SERVERNAME`.

#### \

A separator used only when specifying *instance_name*, in order to separate it from *system_name* or *FCI_network_name*.

For information about the prerequisites for WSFC nodes and server instances, see [Prerequisites, restrictions, and recommendations for Always On availability groups](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).

#### ENDPOINT_URL = '*TCP://*system-address*:*port'

Specifies the URL path for the [database mirroring endpoint](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md) on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the availability replica that you're adding or modifying.

`ENDPOINT_URL` is required in the `ADD REPLICA ON` clause and optional in the `MODIFY REPLICA ON` clause. For more information, see [Specify Endpoint URL - Adding or Modifying Availability Replica](../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md).

#### 'TCP://*system-address*:*port*'

Specifies a URL for specifying an endpoint URL or read-only routing URL. The URL parameters are as follows:

#### *system-address*

A string, such as a system name, a fully qualified domain name, or an IP address, that unambiguously identifies the destination computer system.

#### *port*

A port number that's associated with the mirroring endpoint of the server instance (for the `ENDPOINT_URL` option) or the port number used by the [!INCLUDE [ssDE](../../includes/ssde-md.md)] of the server instance (for the `READ_ONLY_ROUTING_URL` option).

#### AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT | CONFIGURATION_ONLY }

Specifies whether the primary replica waits for the secondary replica to acknowledge the hardening (writing) of the log records to disk before the primary replica can commit the transaction on a given primary database. The transactions on different databases on the same primary replica can commit independently.

#### SYNCHRONOUS_COMMIT

Specifies that the primary replica waits to commit transactions until they are hardened on this secondary replica (synchronous-commit mode). You can specify `SYNCHRONOUS_COMMIT` for up to three replicas, including the primary replica.

#### ASYNCHRONOUS_COMMIT

Specifies that the primary replica commits transactions without waiting for this secondary replica to harden the log (synchronous-commit availability mode). You can specify `ASYNCHRONOUS_COMMIT` for up to five availability replicas, including the primary replica.

#### CONFIGURATION_ONLY

Specifies that the primary replica synchronously commit availability group configuration metadata to the `master` database on this replica. The replica doesn't contain user data. This option:

- Can be hosted on any edition of SQL Server, including Express Edition.
- Requires the data mirroring endpoint of the `CONFIGURATION_ONLY` replica to be type `WITNESS`.
- Can't be altered.
- Isn't valid when `CLUSTER_TYPE = WSFC`.

  For more information, see [High availability and data protection for availability group configurations](../../linux/sql-server-linux-availability-group-ha.md).

`AVAILABILITY_MODE` is required in the `ADD REPLICA ON` clause and optional in the `MODIFY REPLICA ON` clause. For more information, see [Differences between availability modes for an Always On availability group](../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md).

#### FAILOVER_MODE = { AUTOMATIC | MANUAL }

Specifies the failover mode of the availability replica that you're defining.

#### AUTOMATIC

Enables automatic failover. `AUTOMATIC` is supported only if you also specify `AVAILABILITY_MODE = SYNCHRONOUS_COMMIT`. You can specify `AUTOMATIC` for three availability replicas, including the primary replica.

> [!NOTE]  
>
> - Before [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you're limited to two automatic failover replicas, including the primary replica.
> - SQL Server Failover Cluster Instances (FCIs) don't support automatic failover by availability groups, so any availability replica that an FCI hosts can only be configured for manual failover.

#### MANUAL

Enables manual failover or forced manual failover (*forced failover*) by the database administrator.

You must specify `FAILOVER_MODE` in the `ADD REPLICA ON` clause. You can optionally specify it in the `MODIFY REPLICA ON` clause. Two types of manual failover exist: manual failover without data loss, and forced failover (with possible data loss). Different conditions support these types. For more information, see [Failover and Failover Modes (Always On Availability Groups)](../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md).

#### SEEDING_MODE = { AUTOMATIC | MANUAL }

Specifies how the secondary replica is initially seeded.

#### AUTOMATIC

Enables direct seeding. This method seeds the secondary replica over the network. This method doesn't require you to back up and restore a copy of the primary database on the replica.

> [!NOTE]  
> For direct seeding, you must allow database creation on each secondary replica by calling `ALTER AVAILABILITY GROUP` with the `GRANT CREATE ANY DATABASE` option.

#### MANUAL

Specifies manual seeding (default). This method requires you to create a backup of the database on the primary replica and manually restore that backup on the secondary replica.

#### BACKUP_PRIORITY = *n*

Specifies your priority for performing backups on this replica relative to the other replicas in the same availability group. The value is an integer in the range of 0..100. These values have the following meanings:

- `1..100` indicates that the availability replica could be chosen for performing backups. 1 indicates the lowest priority, and 100 indicates the highest priority. If `BACKUP_PRIORITY = 1`, the availability replica is chosen for performing backups only if no higher priority availability replicas are currently available.

- `0` indicates that this availability replica is never chosen for performing backups. This option is useful, for example, for a remote availability replica to which you never want backups to fail over.

For more information, see [Offload supported backups to secondary replicas of an availability group](../../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md).

#### SECONDARY_ROLE ( ... )

Specifies role-specific settings that take effect if this availability replica currently owns the secondary role (whenever it's a secondary replica). Within the parentheses, specify either or both secondary-role options. If you specify both, use a comma-separated list.

The secondary role options are as follows:

#### ALLOW_CONNECTIONS = { NO | READ_ONLY | ALL }

Specifies whether the databases of a given availability replica that's performing the secondary role (acting as a secondary replica) can accept connections from clients, one of:

#### NO

No user connections are allowed to secondary databases of this replica. They're not available for read access. This is the default behavior.

#### READ_ONLY

Only connections are allowed to the databases in the secondary replica where the Application Intent property is set to `ReadOnly`. For more information about this property, see [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).

#### ALL

All connections are allowed to the databases in the secondary replica for read-only access.

For more information, see [Offload read-only workload to secondary replica of an Always On availability group](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).

<a id="read_only_routing_url-tcpsystem-addressport--none"></a>

#### READ_ONLY_ROUTING_URL = { '*TCP://*system-address*:*port' | NONE }

Specifies the URL to use for routing read-intent connection requests to this availability replica. This URL is where the SQL Server Database Engine listens. Typically, the default instance of the SQL Server Database Engine listens on TCP port 1433.

Starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], you can specify `NONE` as the `READ_ONLY_ROUTING_URL` destination to revert the specified read-only routing for the availability replica, and route traffic based on the default behavior.

For a named instance, query the `port` and `type_desc` columns of the [sys.dm_tcp_listener_states](../../relational-databases/system-dynamic-management-views/sys-dm-tcp-listener-states-transact-sql.md) dynamic management view to get the port number. The server instance uses the Transact-SQL listener (`type_desc='TSQL'`).

For more information about calculating the read-only routing URL for an availability replica, see [Calculating read_only_routing_url for Always On](/archive/blogs/mattn/calculating-read_only_routing_url-for-alwayson).

> [!NOTE]  
> For a named instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], configure the Transact-SQL listener to use a specific port. For more information, see [Configure SQL Server to listen on a specific TCP port](../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md).

#### PRIMARY_ROLE ( ... )

Specifies role-specific settings that take effect if this availability replica currently owns the primary role (whenever it's the primary replica). Within the parentheses, specify either or both primary-role options. If you specify both, use a comma-separated list.

The primary role options are as follows:

#### ALLOW_CONNECTIONS = { READ_WRITE | ALL }

Specifies the type of connection that the databases of a given availability replica that's performing the primary role (acting as a primary replica) can accept from clients, one of:

#### READ_WRITE

Connections where the Application Intent connection property is set to `ReadOnly` are disallowed. When the Application Intent property is set to `ReadWrite` or the Application Intent connection property isn't set, the connection is allowed. For more information about Application Intent connection property, see [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).

#### ALL

All connections are allowed to the databases in the primary replica. This is the default behavior.

#### READ_ONLY_ROUTING_LIST = { ('*\<server_instance>*' [ , ...*n* ] ) | NONE }

Specifies a comma-separated list of server instances that host availability replicas for this availability group that meet the following requirements when running under the secondary role:

- Be configured to allow all connections or read-only connections (see the `ALLOW_CONNECTIONS` argument of the `SECONDARY_ROLE` option, previously in this article).

- Have their read-only routing URL defined (see the `READ_ONLY_ROUTING_URL` argument of the `SECONDARY_ROLE` option, previously in this article).

The `READ_ONLY_ROUTING_LIST` values are as follows:

#### \<server_instance>

Specifies the address of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that's the host for an availability replica that is a readable secondary replica when running under the secondary role.

Use a comma-separated list to specify all of the server instances that might host a readable secondary replica. Read-only routing follows the order in which server instances are specified in the list. If you include a replica's host server instance on the replica's read-only routing list, placing this server instance at the end of the list is typically a good practice, so that read-intent connections go to a secondary replica, if one is available.

Beginning with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you can load-balance read-intent requests across readable secondary replicas. You specify this by placing the replicas in a nested set of parentheses within the read-only routing list. For more information and examples, see [Configure load-balancing across read-only replicas](../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md#configure-load-balancing-across-read-only-replicas).

#### NONE

Specifies that when this availability replica is the primary replica, read-only routing won't be supported. This is the default behavior. When used with `MODIFY REPLICA ON`, this value disables an existing list, if any.

<a id="read_write_routing_url--tcpsystem-addressport--none"></a>

#### { READ_WRITE_ROUTING_URL = '*TCP://*system-address*:*port' | NONE }

**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions

Specifies server instances that host availability replicas for this availability group that meet the following requirements when running under the primary role:

- The replica spec `PRIMARY_ROLE` includes `READ_WRITE_ROUTING_URL`.
- The connection string is ReadWrite either by defining ApplicationIntent as ReadWrite or by not setting ApplicationIntent and letting the default (ReadWrite) take effect.

Starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], you can specify `NONE` as the `READ_WRITE_ROUTING_URL` destination to revert the specified read-write routing for the availability replica, and route traffic based on the default behavior.

For more information, see [Secondary to primary replica read/write connection redirection (Always On Availability Groups)](../../database-engine/availability-groups/windows/secondary-replica-connection-redirection-always-on-availability-groups.md).

#### SESSION_TIMEOUT = *seconds*

Specifies the session-timeout period in seconds. If you don't specify this option, the default time period is 10 seconds. The minimum value is 5 seconds.

> [!IMPORTANT]  
> Keep the timeout period at 10 seconds or greater.

For more information about the session-timeout period, see [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)

#### MODIFY REPLICA ON

Modifies any of the replicas of the availability group. The list of replicas to modify contains the server instance address and a `WITH (...)` clause for each replica.

Supported only on the primary replica.

#### REMOVE REPLICA ON

Removes the specified secondary replica from the availability group. You can't remove the current primary replica from an availability group. When you remove a replica, it stops receiving data. The replica's secondary databases are removed from the availability group and enter the `RESTORING` state.

Supported only on the primary replica.

> [!NOTE]  
> If you remove a replica while it's unavailable or failed, when it comes back online it discovers that it no longer belongs to the availability group.

#### JOIN

Causes the local server instance to host a secondary replica in the specified availability group.

Supported only on a secondary replica that isn't yet joined to the availability group.

For more information, see [Join a secondary replica to an Always On availability group](../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md).

#### FAILOVER

Initiates a manual failover of the availability group without data loss to the secondary replica to which you're connected. The replica that hosts the primary replica is the *failover target*. The failover target takes over the primary role and recovers its copy of each database, bringing them online as the new primary databases. The former primary replica concurrently transitions to the secondary role, and its databases become secondary databases and are immediately suspended. Potentially, these roles can switch back and forth by a series of failures.

Failover is only supported to a synchronous-commit secondary replica that's currently synchronized with the primary replica. For a secondary replica to be synchronized, the primary replica must also be running in synchronous-commit mode.

For an availability group between two SQL Server instances, you must issue the failover command on the secondary replica (the failover target).
For instances replicated through the [Managed Instance link](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview), you must issue the failover command on the primary replica.

> [!NOTE]  
>
> - For an availability group, a failover command returns as soon as the failover target accepts the command. However, database recovery occurs asynchronously after the availability group finishes failing over.  
> - For a [Managed Instance link failover](/azure/azure-sql/managed-instance/managed-instance-link-failover-how-to), the failover command returns after a successful failover where the source and target switch roles, or if the failover command fails after the failover precondition checks fail.
> - You can't use the failover command for a planned failover of a [distributed availability group](../../database-engine/availability-groups/windows/distributed-availability-groups.md) between two SQL Server instances.

For information about the limitations, prerequisites, and recommendations for performing a planned manual failover, see [Perform a planned manual failover of an Always On availability group (SQL Server)](../../database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server.md).

#### FORCE_FAILOVER_ALLOW_DATA_LOSS

> [!CAUTION]  
> Only initiate a forced failover as a disaster recovery measure, as it can result in data loss. Force failover should be performed only when the primary replica is unavailable, you are prepared to accept potential data loss, and you must restore service to the availability group immediately.

Supported only on a replica whose role is in the `SECONDARY` or `RESOLVING` state. The replica on which you enter a failover command is the *failover target*.

Forces failover of the availability group, with possible data loss, to the failover target. The failover target takes over the primary role and recovers its copy of each database, bringing them online as the new primary databases. On any remaining secondary replicas, every secondary database is suspended until manually resumed. When the former primary replica becomes available, it switches to the secondary role, and its databases become suspended secondary databases.

For instances replicated through the [Managed Instance link](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview), you must issue the `FORCE_FAILOVER_ALLOW_DATA_LOSS` command on the secondary replica (the failover target).

> [!NOTE]  
> A failover command returns as soon as the failover target accepts the command. However, database recovery occurs asynchronously after the availability group finishes failing over.

For information about the limitations, prerequisites, and recommendations for forcing failover and the effect of a forced failover on the former primary databases in the availability group, see [Perform a forced manual failover of an Always On availability group (SQL Server)](../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md).

#### ADD LISTENER '*dns_name*' ( \<add_listener_option> )

Defines a new availability group listener for this availability group. Supported only on the primary replica.

> [!IMPORTANT]  
> Before you create your first listener, read [Configure a listener for an Always On availability group](../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).  
>
> After you create a listener for a given availability group, do the following steps:  
>
> - Ask your network administrator to reserve the listener's IP address for its exclusive use.  
> - Give the listener's DNS host name to application developers to use in connection strings when requesting client connections to this availability group.

#### *dns_name*

Specifies the DNS host name of the availability group listener. The DNS name of the listener must be unique in the domain and in NetBIOS.

*dns_name* is a string value. This name can contain only alphanumeric characters, dashes (-), and hyphens (_), in any order. DNS host names are case insensitive. The maximum length is 63 characters.

Specify a meaningful string. For example, for an availability group named `AG1`, a meaningful DNS host name would be `ag1-listener`.

> [!IMPORTANT]  
> NetBIOS recognizes only the first 15 characters in the `dns_name`. If you have two WSFC clusters that are controlled by the same Active Directory and you try to create availability group listeners in both clusters using names with more than 15 characters and an identical 15-character prefix, you get an error reporting that the Virtual Network Name resource couldn't be brought online. For information about prefix naming rules for DNS names, see [Assigning Domain Names](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc731265(v=ws.10)).

#### JOIN AVAILABILITY GROUP ON

Joins to a *distributed availability group*. When you create a distributed availability group, the availability group on the cluster where you create it is the primary availability group. The availability group that joins the distributed availability group is the secondary availability group.

#### \<ag_name>

Specifies the name of the availability group that makes up one half of the distributed availability group.

#### LISTENER = '*TCP://*system-address*:*port'

Specifies the URL path for the listener associated with the availability group.

The `LISTENER` clause is required.

#### '*TCP://*system-address*:*port'

Specifies a URL for the listener associated with the availability group. The URL parameters are as follows:

#### *system-address*

A string, such as a system name, a fully qualified domain name, or an IP address, that unambiguously identifies the listener.

#### *port*

A port number that's associated with the mirroring endpoint of the availability group. This isn't the port of the listener.

#### AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT }

Specifies whether the primary replica waits for the secondary availability group to acknowledge the hardening (writing) of the log records to disk before the primary replica can commit the transaction on a given primary database.

#### SYNCHRONOUS_COMMIT

Specifies that the primary replica waits to commit transactions until it receives confirmation that the transactions are hardened on the secondary availability group. You can specify `SYNCHRONOUS_COMMIT` for up to two availability groups, including the primary availability group.

#### ASYNCHRONOUS_COMMIT

Specifies that the primary replica commits transactions without waiting for this secondary availability group to harden the log. You can specify `ASYNCHRONOUS_COMMIT` for up to two availability groups, including the primary availability group.

The `AVAILABILITY_MODE` clause is required.

#### FAILOVER_MODE = { MANUAL }

Specifies the failover mode of the distributed availability group.

#### MANUAL

Enables planned manual failover or forced manual failover (typically called *forced failover*) by the database administrator.

Automatic failover to the secondary availability group isn't supported.

#### SEEDING_MODE = { AUTOMATIC | MANUAL }

Specifies how the secondary availability group is initially seeded.

#### AUTOMATIC

Enables automatic seeding. This method seeds the secondary availability group over the network. This method doesn't require you to back up and restore a copy of the primary database on the replicas of the secondary availability group.

#### MANUAL

Specifies manual seeding. This method requires you to create a backup of the database on the primary replica and manually restore that backup on the replicas of the secondary availability group.

#### MODIFY AVAILABILITY GROUP ON

Modifies any of the availability group settings of a distributed availability group. The list of availability groups to modify contains the availability group name and a `WITH (...)` clause for each availability group.

> [!IMPORTANT]  
> You must run this command on both the primary availability group and secondary availability group instances as `sysadmin`. 

#### GRANT CREATE ANY DATABASE

Permits the availability group to create databases on behalf of the primary replica, which supports direct seeding (`SEEDING_MODE = AUTOMATIC`). Run this parameter on every secondary replica that supports direct seeding after that secondary joins the availability group. Requires the `CREATE ANY DATABASE` permission.

#### DENY CREATE ANY DATABASE

Removes the ability of the availability group to create databases on behalf of the primary replica.

#### \<add_listener_option>

`ADD LISTENER` takes one of the following options:

#### WITH DHCP [ ON { ('*four_part_ipv4_address*','*four_part_ipv4_mask*') } ]

Specifies that the availability group listener uses the Dynamic Host Configuration Protocol (DHCP). Optionally, use the `ON` clause to identify the network on which this listener is created. DHCP is limited to a single subnet that's used for every server instance that hosts an availability replica in the availability group.

> [!IMPORTANT]  
> Don't use DHCP in a production environment. If there's downtime and the DHCP IP lease expires, extra time is required to register the new DHCP network IP address that's associated with the listener DNS name and affects the client connectivity. However, DHCP is good for setting up your development and testing environment to verify basic functions of availability groups and for integration with your applications.

For example:

`WITH DHCP ON ('10.120.19.0','255.255.254.0')`

#### WITH IP ( { ('*four_part_ipv4_address*','*four_part_ipv4_mask*') | ('*ipv6_address*') } [ , ...*n* ] ) [ , PORT = *listener_port* ]

Instead of using DHCP, the availability group listener uses one or more static IP addresses. To create an availability group across multiple subnets, each subnet requires one static IP address in the listener configuration. For a given subnet, the static IP address can be either an IPv4 address or an IPv6 address. Contact your network administrator to get a static IP address for each subnet that hosts an availability replica for the new availability group.

For example:

`WITH IP ( ('10.120.19.155','255.255.254.0') )`

#### *ipv4_address*

An IPv4 four-part address for an availability group listener. For example, `10.120.19.155`.

#### *ipv4_mask*

An IPv4 four-part mask for an availability group listener. For example, `255.255.254.0`.

#### *ipv6_address*

An IPv6 address for an availability group listener. For example, `2001::4898:23:1002:20f:1fff:feff:b3a3`.

#### PORT = *listener_port*

The port number (*listener_port*) to use by an availability group listener that the `WITH IP` clause specifies. `PORT` is optional.

The default port number, `1433`, is supported. However, you can choose a different port number.

For example: `WITH IP ( ('2001::4898:23:1002:20f:1fff:feff:b3a3') ) , PORT = 7777`

#### MODIFY LISTENER '*dns_name*' ( \<modify_listener_option\> )

Modifies an existing availability group listener for this availability group. Supported only on the primary replica.

#### \<modify_listener_option\>

`MODIFY LISTENER` takes one of the following options:

#### ADD IP { ('*four_part_ipv4_address*','*four_part_ipv4_mask*') | ('*ipv6_address*') }

Adds the specified IP address to the availability group listener specified by *dns_name*.

#### PORT = *listener_port*

See the description of this argument earlier in this section.

#### REMOVE IP { ('*four_part_ipv4_address*') | ('*ipv6_address*') }

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions

Removes the specified IP address from the specified availability group listener.

> [!NOTE]  
> The last remaining IP address can't be removed. Attempting to do so results in error 19535.

#### RESTART LISTENER '*dns_name*'

Restarts the listener that's associated with the specified DNS name. Supported only on the primary replica.

#### REMOVE LISTENER '*dns_name*'

Removes the listener that's associated with the specified DNS name. Supported only on the primary replica.

#### OFFLINE

Takes an online availability group offline. There's no data loss for synchronous-commit databases.

When an availability group goes offline, its databases become unavailable to clients, and you can't bring the availability group back online. Therefore, use the `OFFLINE` option only during a cross-cluster migration of [!INCLUDE [ssHADR](../../includes/sshadr-md.md)], when you're migrating availability group resources to a new WSFC cluster.

For more information, see [Take an Availability Group Offline (SQL Server)](../../database-engine/availability-groups/windows/take-an-availability-group-offline-sql-server.md).

## Prerequisites and restrictions

For information about prerequisites and restrictions on availability replicas and on their host server instances and computers, see [Prerequisites, restrictions, and recommendations for Always On availability groups](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).

For information about restrictions on the `AVAILABILITY GROUP` Transact-SQL statements, see [Transact-SQL statements for Always On availability groups](../../database-engine/availability-groups/windows/transact-sql-statements-for-always-on-availability-groups.md).

## Permissions

You need `ALTER AVAILABILITY GROUP` permission on the availability group, `CONTROL AVAILABILITY GROUP` permission, `ALTER ANY AVAILABILITY GROUP` permission, or `CONTROL SERVER` permission. You also need `ALTER ANY DATABASE` permission.

## Examples

<a id="Join_Secondary_Replica"></a>

<a id="a-joining-a-secondary-replica-to-an-availability-group"></a>

### A. Join a secondary replica to an availability group

The following example joins a secondary replica to which you're connected to the `AccountsAG` availability group.

```sql
ALTER AVAILABILITY GROUP AccountsAG JOIN;
GO
```

<a id="Force_Failover"></a>

<a id="b-forcing-failover-of-an-availability-group"></a>

### B. Force failover of an availability group

The following example forces the `AccountsAG` availability group to fail over to the secondary replica to which you're connected.

```sql
ALTER AVAILABILITY GROUP AccountsAG FORCE_FAILOVER_ALLOW_DATA_LOSS;
GO
```

### C. Force encryption in connections to the availability group

The examples in this section [force encryption](#cluster_connection_options) in connections to the `AccountsAG` availability group.

If the server name appears in each certificate as defined by either [method](../../database-engine/configure-windows/certificate-requirements.md#always-on-availability-group), you can omit the `HostNameInCertificate` option:

```sql
ALTER AVAILABILITY GROUP [AccountsAG]
   SET (
   CLUSTER_CONNECTION_OPTIONS = 'Encrypt=Strict')
```

If you followed [method 1](../../database-engine/configure-windows/certificate-requirements.md#always-on-availability-group) but didn't list the server name as a **Subject Alternative Name** in the certificate, you must specify the value that appears in the **Subject Alternative Name** in the `HostNameInCertificate` option:

```sql
ALTER AVAILABILITY GROUP [AccountsAG]
   SET (
   CLUSTER_CONNECTION_OPTIONS = 'Encrypt=Strict;HostNameInCertificate=<Subject Alternative Name>')
```

If you followed [method 1](../../database-engine/configure-windows/certificate-requirements.md#always-on-availability-group) and want to use the `ServerCertificate` property instead of providing a value for `HostNameInCertificate`:

```sql
ALTER AVAILABILITY GROUP [AccountsAG]
   SET (
   CLUSTER_CONNECTION_OPTIONS = 'Encrypt=Strict;ServerCertificate=C:\Users\admin\SqlAGCertificate.cer')
```

## Related content

- [CREATE AVAILABILITY GROUP (Transact-SQL)](create-availability-group-transact-sql.md)
- [ALTER DATABASE (Transact-SQL) SET HADR](alter-database-transact-sql-set-hadr.md)
- [DROP AVAILABILITY GROUP (Transact-SQL)](drop-availability-group-transact-sql.md)
- [sys.availability_replicas (Transact-SQL)](../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md)
- [sys.availability_groups (Transact-SQL)](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md)
- [Troubleshoot Always On Availability Groups Configuration (SQL Server)](../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [Connect to an Always On availability group listener](../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)
