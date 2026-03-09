---
title: "What Is a Contained Availability Group?"
description: An overview of the contained availability group feature of Always On availability groups within SQL Server.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 02/23/2026
ms.service: sql
ms.subservice: high-availability
ms.topic: overview
ms.custom:
  - build-2025
  - sfi-ropc-blocked
helpviewer_keywords:
  - "availability groups [SQL Server], contained availability groups"
  - "availability groups [SQL Server], configuring"
monikerRange: ">=sql-server-ver16"
---

# What is a contained availability group?

[!INCLUDE [sqlserver2022](../../../includes/applies-to-version/sqlserver2022.md)]

A contained availability group is an Always On availability group (AG) that supports:

- managing metadata objects (users, logins, permissions, SQL Agent jobs, and so on) at the AG level in addition to the instance level.

- specialized contained system databases within the AG.

This article details the similarities, differences, and functionalities of contained AGs.

## Overview

AGs generally consist of one or more user databases intended to operate as a coordinated group, and which are replicated on some number of nodes in a cluster. When there's a failure in the node, or in the health of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on the node that hosts the primary copy, the group of databases moves as a unit to another replica node in the AG. All of the user databases stay synchronized across all replicas of the AG, either in synchronous or asynchronous mode.

This architecture works well for applications that only interact with that set of user databases. However, challenges arise when applications also rely on objects such as users, logins, permissions, agent jobs, and other objects that are stored in one of the system databases (`master` or `msdb`). To ensure that applications function smoothly and predictably, the admin must *manually* ensure that any change to these objects is duplicated across all replica instances in the AG. If you add a new instance to the AG, you can automatically or manually seed the databases in a straightforward process. However, you must reconfigure all of the system database customizations on the new instance to match the other replicas.

Contained AGs extend the concept of the group of databases being replicated to include relevant portions of the `master` and `msdb` databases. Think of it as the execution context for applications using the contained AG. The idea is that the contained AG environment includes settings that affect the application relying on them. As such, the contained AG environment concerns all databases the application interacts with, the authentication it uses (logins, users, permissions), any scheduled jobs that it expects to be running, and other configuration settings that impact the application.

This concept differs from contained databases, which use a different mechanism for the user accounts, by storing the user information within the database itself. Contained databases only replicate logins and users, and the scope of the replicated login or user is limited to that single database (and its replicas).

In contrast, in a contained AG, you create users, logins, permissions, and so on, at the AG level. These objects are *automatically* consistent across replicas in the AG, as well as consistent across databases within that contained AG. This consistency saves the admin from having to manually make these changes.

## SQL Server 2025 changes

[!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] introduces [distributed availability group](#distributed-availability-groups) support for contained availability groups.

## Differences

When you work with contained AGs, consider some practical differences. These differences include the creation of contained system databases and forcing the connection at the contained AG level instead of connecting at the instance level.

### Contained system databases

Each contained AG has its own `master` and `msdb` system databases, named after the name of the availability group. For example, in contained AG `MyContainedAG`, you have databases named `MyContainedAG_master` and `MyContainedAG_msdb`. These system databases are automatically seeded to new replicas, and updates replicate to these databases just like any other database in an availability group. When you add an object such as a login or agent job while connected to the contained AG, you still see the agent jobs, and can authenticate by using the login created in the contained AG when the contained AG fails over to another instance.

> [!IMPORTANT]  
> Contained AGs are a mechanism for keeping execution environment configurations consistent across the replicas of an availability group. They **don't** represent a security boundary. For example, there's no boundary that keeps a connection to a contained AG from accessing databases outside of the AG.

The system databases in a newly created contained AG aren't copies from the instance where you run the `CREATE AVAILABILITY GROUP` command. They're initially empty templates without any data. Immediately after creation, the process copies the admin accounts on the instance creating the contained AG into the contained AG `master`. That way, the administrator can sign in to the contained AG and set up the rest of the configuration.

If you create local users or configurations in your instance, they don't automatically appear when you create your contained system databases, and they aren't visible when you connect to the contained AG. Once the user database joins a contained AG, these users immediately lose access. You need to manually re-create them in the contained system databases within the context of the contained AG, by connecting directly to the database or by using the listener endpoint. The exception to this rule is that all of the logins in the **sysadmin** role in the parent instance are copied into the new AG specific `master` database during creation of contained AG.

> [!NOTE]  
> Because the `master` database is separate for each contained availability group, server-scope activities performed in the context of the contained AG only persist in the contained system database. This rule includes auditing. If you audit server level activity with SQL Server Auditing, you must create the same server audits within each contained AG.

#### Initial data synchronization

The contained system databases only support automatic seeding as the initial data synchronization method.

In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and earlier versions, contained availability groups must use automatic seeding during creation. When using the `CREATE AVAILABILITY GROUP` statement or the New Availability Group wizard in SQL Server Management Studio, only include user databases that support automatic seeding. To add large databases by using manual seeding (`JOIN ONLY`), wait until after the contained AG is created.

In [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], contained system databases always use automatic seeding, even if the `CREATE AVAILABILITY GROUP` statement specifies manual seeding. You can set seeding mode to manual to create a contained AG, and later add user databases by using synchronization methods other than automatic seeding.

#### Restore a contained system database

To restore backups of contained system databases, follow these steps:

1. Drop the contained AG.

1. Restore the contained `master` and `msdb` databases on the original primary replica of the contained AG.

1. Drop the contained `master` and `msdb` databases from secondary replicas.

1. On the primary replica, recreate the contained AG using the original name and nodes, with `WITH (CONTAINED, REUSE_SYSTEM_DATABASES)` and `SEEDING_MODE = AUTOMATIC` syntax.

When recreating a contained availability group, don't include contained system databases in the `CREATE AVAILABILITY GROUP` statement. [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] detects them automatically when you specify `REUSE_SYSTEM_DATABASES`. In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and earlier versions, include only small user databases that support automatic seeding. Add large databases separately after the contained AG is created, using `JOIN ONLY`.

### Contained availability group jobs

Jobs that belong to a contained availability group run on the primary replica only. They don't run on secondary replicas.

### Connect (contained environment)

It's important to distinguish the difference between connecting to the instance, and connecting to the contained AG. The only way to access the environment of the contained AG is to connect to the contained AG listener, or to connect to a database that is in the contained AG.

```output
"Persist Security Info=False;
User ID=MyUser;Password=*****;
Initial Catalog=MyContainedDatabase;
Server=MyServer;"
```

Where `MyContainedDatabase` is a database within the contained AG that you want to interact with.

**You must create a listener for the contained AG to effectively use a contained AG.** If you connect to one of the *instances* hosting the contained AG rather than *directly to the contained AG through the listener*, you're in the environment of the instance, and not the contained AG.

For example, if your availability group `MyContainedAG` is hosted on server `SERVER\MSSQLSERVER`, and instead of connecting to the listener `MyContainedAG_Listener`, you connect to the instance using `SERVER\MSSQLSERVER`, you're in the environment of the instance, and not in the environment of `MyContainedAG`. You're subject to the contents (users, permissions, jobs, and so on) that are found in the system databases of the instance. To access the contents found in the contained system databases of the contained AG, connect to the contained AG listener (`MyContainedAG_Listener`, for example) instead. When you're connected to the instance through the contained AG listener, when you interact with `master`, you're actually redirected to the contained `master` database (for example, `MyContainedAG_master`).

#### Read-only routing and contained availability groups

If you configure read-only routing to redirect connections with read intent to a secondary replica (see [Configure read-only routing for an Always On availability group](configure-read-only-routing-for-an-availability-group-sql-server.md)) and you want to connect using a login that is created in the contained AG only, there are further considerations:

- You must specify a database that is part of the contained AG in the connection string.
- The user specified in the connection string must have permission to access the databases in the contained AG.

For example, in the following connection string, `AdventureWorks` is a database within the contained AG that has `MyContainedListener`, and `MyUser` is a user defined in the contained AG and none of the participating instances:

```output
"Persist Security Info=False;
User ID=MyUser;Password=*****;
Initial Catalog=AdventureWorks;
Server=MyContainedListener;
ApplicationIntent=ReadOnly"
```

This example connects you to the readable secondary that is part of the ReadOnly Routing configuration, and you're within the context of the contained AG.

#### Differences between connecting to the instance and connecting to the contained availability group

- When connected to a contained AG, users only see databases in the contained AG, plus `tempdb`.
- At instance level, the contained AG `master` and `msdb` names are `[contained AG]_master`, and `[contained AG]_msdb`. Inside the contained AG, their names are `master` and `msdb`.
- The database ID for the contained AG `master` is `1` from inside the contained AG, but something else when connected to the instance.
- While users don't see databases outside of the contained AG in `sys.databases` when connected in a contained AG connection, they can access those databases by three-part name, or through the `USE` command.
- Server configuration through `sp_configure` can be read from the contained AG connection but can only be written from instance level.
- From contained AG connections, the sysadmin is able to perform instance level operations, such as shutting down [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].
- Most database level, endpoint level, or AG level operations can only be performed from instance connections, not contained AG connections.

## Interactions with other features

Consider other factors when using certain features with contained AGs. Some features are currently unsupported.

### Back up

The procedures to back up databases in a contained AG are the same as any user database backup procedures. This statement is true for both the contained AG user databases and the contained AG system databases.

If you use a local backup location, the backup files are placed on the server that runs the backup job. This means your backup files might be in different locations.

If you use a network resource for the backup location, all servers that host replicas need access to that resource.

### Enable database creation or restoration in contained availability group sessions

**Applies to**: [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] CU 1 and later versions.

In [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] Cumulative Update (CU) 1, you can enable database creation and restoration directly within a contained availability group session, through the contained AG listener. This enhancement streamlines workflows for users assigned the appropriate roles, allowing seamless operations within contained AG environments.

Only users with the **dbcreator** role can create databases in a contained AG session. Only users with the **db_owner** or **sysadmin** role can restore databases.

The following example enables the feature for your session, using the session context key `allow_cag_create_db` in the `sp_set_session_contex` stored procedure. To disable it, set `@value` to `0`.

```sql
EXECUTE sp_set_session_context
    @key = N'allow_cag_create_db',
    @value = 1;
```

### Distributed availability groups

A [distributed availability group](distributed-availability-groups.md) is a special type of availability group that spans two underlying availability groups. When you configure a distributed availability group, changes made on the global primary (which is the primary replica of your first AG) are then replicated to the primary replica of your second AG, known as the forwarder.

Starting with [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], you can configure a distributed availability group between two contained AGs. Since a contained AG relies on contained `master` and `msdb` system databases, to create a distributed availability group, the second AG (forwarder) must have the same contained AG system database as the global primary.

If you intend to use a contained AG as the forwarder in a distributed availability group, you must create the contained AG by using the `AUTOSEEDING_SYSTEM_DATABASES` clause for the `WITH | CONTAINED` option of the [CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md#contained-reuse_system_databases--autoseeding_system_databases) statement. The `AUTOSEEDING_SYSTEM_DATABASES` clause tells SQL Server to skip creating its own contained AG system databases, and instead seeds the contained AG system databases from the global primary.

### Resource governor

**Applies to**: [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] CU 18 and later versions.

In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] before Cumulative Update (CU) 18, and in older versions of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], configuring or using resource governor on contained availability group connections isn't supported.

In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] CU 18 and later versions, if you configure resource governor on an instance connection, resource consumption on either instance connections or contained availability group connections is governed as expected. If you attempt to configure resource governor on a contained availability group connection, you receive an error.

Resource governor works at the [!INCLUDE [ssde-md](../../../includes/ssde-md.md)] instance level. Resource governor configuration at the instance level doesn't propagate to availability replicas. You must configure resource governor on each instance hosting an availability replica.

> [!TIP]  
> You should use the same resource governor configuration for all [!INCLUDE [ssde-md](../../../includes/ssde-md.md)] instances hosting availability replicas to ensure consistent behavior as availability group failovers occur.

For more information, see [Resource governor](../../../relational-databases/resource-governor/resource-governor.md) and [Tutorial: Resource governor configuration examples and best practices](../../../relational-databases/resource-governor/resource-governor-walkthrough.md).

### Change data capture

Change data capture (CDC) is implemented as SQL Agent jobs, so the SQL Agent needs to be running on all instances with replicas in the contained AG.

To use change data capture with a contained AG, connect to the AG listener when you configure CDC so that the CDC metadata is configured using the contained system databases.

### Log shipping

You can configure log shipping if the source database is in the contained AG. However, a log shipping target isn't supported within a contained AG. Additionally, you need to modify the log shipping job after you configure log shipping.

To configure log shipping with a contained AG, follow these steps:

1. Connect to the contained AG listener.

1. Configure [log shipping](../../log-shipping/configure-log-shipping-sql-server.md)

    - Use `@primary_server_with_port_override` in [sp_add_log_shipping_primary_database](../../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql.md#primary-server-with-port-override).

    - If you use a local monitor in your log shipping topology, leave the `@monitor_server` value in [sp_add_log_shipping_primary_database](../../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql.md#monitor-server) blank or *null*.

1. After you configure the log shipping job, alter the job to connect to the contained AG listener before taking a backup.

### Transparent data encryption (TDE)

To use transparent data encryption (TDE) with databases in a contained AG, manually install the Database Master Key (DMK) to the contained `master` database within the contained AG.

Databases that use TDE rely on certificates in the `master` database to decrypt the Database Encryption Key (DEK). Without that certificate, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] can't decrypt databases encrypted with TDE or bring them online. In a contained AG, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] checks both `master` databases for the DMK, the `master` database for the instance, and the contained `master` database within the contained AG to decrypt the database. If it can't find the certificate in either location, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] can't bring the database online.

To transfer the DMK from the `master` database of the instance to the contained `master` database, see [Move a TDE protected database to another SQL Server](../../../relational-databases/security/encryption/move-a-tde-protected-database-to-another-sql-server.md), primarily focusing on the portions where the DMK is transferred from the old server to the new one.

> [!NOTE]  
> Encrypting any database on a SQL Server instance also [encrypts the `tempdb` system database](../../../relational-databases/security/encryption/transparent-data-encryption.md#tde-and-the-tempdb-system-database).

### SSIS packages and maintenance plans

Using SSIS packages, including maintenance plans, isn't supported with contained availability groups.

## Not supported

Currently, the following [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] features aren't supported with a contained AG:

- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Replication of any type (transactional, merge, snapshot, and so on).
- Log shipping where the target database is in the contained AG. Log shipping with the source database in the contained AG is supported.

<a id="ddl-changes"></a>

## DDL support

In the [CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md) workflow, there's a `WITH` clause with several options:

```syntaxsql
<with_option_spec> ::=
CONTAINED [REUSE_SYSTEM_DATABASES | AUTOSEEDING_SYSTEM_DATABASES ]
```

### CONTAINED

This option specifies that the AG you're creating is a contained AG.

### REUSE_SYSTEM_DATABASES

The `REUSE_SYSTEM_DATABASES` option is only valid for contained AGs. It specifies that the new AG should reuse existing contained system databases from a previous contained AG with the same name. For example, if you had a contained AG named `MyContainedAG`, and you wanted to drop and recreate it, you could use this option to reuse the contents of the original contained system databases. When you use this option, don't specify system database names. SQL Server automatically detects them.

#### AUTOSEEDING_SYSTEM_DATABASES

**Applies to**: [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] and later versions.

If you want to use your contained AG as the forwarder in a [distributed availability group](distributed-availability-groups.md), you must use the `AUTOSEEDING_SYSTEM_DATABASES` option when you *create* the contained AG. This option tells SQL Server to skip creating its own contained AG system databases, and instead seeds the contained AG system databases from the global primary.

<a id="dmv-changes"></a>

## System object support for contained availability groups

Two system views include additions related to contained availability groups:

- The [sys.dm_exec_sessions](../../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md) dynamic management view includes a `contained_availability_group_id` column.
- The [sys.availability_groups](../../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md) catalog view includes the `is_contained` column.

## Related content

- [CREATE AVAILABILITY GROUP (Transact-SQL)](../../../t-sql/statements/create-availability-group-transact-sql.md)
