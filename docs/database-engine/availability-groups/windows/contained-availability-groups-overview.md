---
title: "What is a contained availability group?"
description: An overview of the contained availability group feature of Always On availability groups within SQL Server.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: mathoma, randolphwest
ms.date: 01/19/2024
ms.service: sql
ms.subservice: high-availability
ms.topic: conceptual
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

AGs generally consist of one or more user databases intended to operate as a coordinated group, and which are replicated on some number of nodes in a cluster. When there's a failure in the node, or in the health of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on the node that hosts the primary copy, the group of databases are moved as a unit to another replica node in the AG. All of the user databases are kept in sync across all replicas of the AG, either in synchronous or asynchronous mode.

This works well for applications that only interact with that set of user databases, but there are challenges when applications also rely on objects such as users, logins, permissions, agent jobs, etc., which are stored in one of the system databases (`master` or `msdb`). For the applications to function smoothly and predictably, the admin must *manually* ensure that any change to these objects is duplicated across all replica instances in the AG. If a new instance is brought into the AG, the databases can be automatically or manually seeded in a straightforward process, but then all of the system database customizations must be reconfigured on the new instance to match the other replicas.

Contained AGs extend the concept of the group of databases being replicated to include relevant portions of the `master` and `msdb` databases. Think of it as the execution context for applications using the contained AG. The idea is that the contained AG environment includes settings that would affect the application relying on them. As such, the contained AG environment concerns all databases the application interacts with, the authentication it uses (logins, users, permissions), any scheduled jobs that it expects to be running, and other configuration settings that impact the application.

This is different from contained databases, which use a different mechanism for the user accounts, storing the user information within the database itself. Contained databases only replicate logins and users, and the scope of the replicated login or user is limited to that single database (and its replicas).

In contrast, in a contained AG, you can create users, logins, permissions, and so on, at the AG level, and they are *automatically* consistent across replicas in the AG, as well as consistent across databases within that contained AG. This saves the admin from having to manually make these changes themselves.

## Differences

There are some practical differences to consider when working with contained AGs, such as the creation of contained system databases, and forcing the connection at the contained AG level, rather than connecting at the instance level.

### Contained system databases

Each contained AG has its own `master` and `msdb` system databases, named after the name of the availability group. For example, in contained AG `MyContainedAG`, you have databases named `MyContainedAG_master` and `MyContainedAG_msdb`. These system databases are automatically seeded to new replicas and updates are replicated to these databases just like any other database in an availability group. This means that when you add an object such as a login, or agent job while connected to the contained AG, when the contained AG fails over to another instance, connecting to the contained AG, you still see the agent jobs, and be able to authenticate using the login created in the contained AG.

> [!IMPORTANT]  
> Contained AGs are a mechanism for keeping execution environment configurations consistent across the replicas of an availability group. They **don't** represent a security boundary. There's no boundary which keeps a connection to a contained AG from accessing databases outside of the AG, for example.

The system databases in a newly created contained AG aren't copies from the instance where the `CREATE AVAILABILITY GROUP` command is run. They are initially empty templates without any data. Immediately after creation, the admin accounts on the instance creating the contained AG are copied into the contained AG `master`. That way, the administrator can log into the contained AG and set up the rest of the configuration. 

If you create local users or configurations in your instance, they don't automatically appear when you create your contained system databases, and they aren't visible when you connect to the contained AG. Once the user database has been joined to a contained AG, it will immediately become inaccessible to these users. You need to manually re-create them in the contained system databases within the context of the contained AG, by connecting directly to the database or by using the listener endpoint. The exception to this is that all of the logins in the sysadmin role in the parent instance are copied into the new AG specific `master` database.

#### Restore a contained system database

You can restore a contained system database using one of two different ways.

- **Restore a contained database using a secondary replica**:

  1. Restore the contained `master` and `msdb` database onto a server instance that hosts the secondary replica, using `RESTORE WITH NORECOVERY` for every restore operation. For more information, see [Prepare a secondary database for an Always On availability group](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md).

  1. Join each contained database to the availability group. For more information, see [Join a secondary database to an Always On availability group](join-a-secondary-database-to-an-availability-group-sql-server.md).

- **Restore a contained database by dropping the contained AG**:

  1. Drop the contained AG.

  1. Restore the contained `master` and `msdb` database in each of the instances participating in the contained AG.

  1. Recreate the contained AG using original nodes and name, using `WITH (CONTAINED, REUSE_SYSTEM_DATABASES)` syntax.

### Connect (contained environment)

It's important to distinguish the difference between connecting to the instance, and connecting to the contained AG. The only way to access the environment of the contained AG is to connect to the contained AG listener, or to connect to a database that is in the contained AG.

```output
"Persist Security Info=False;
User ID=MyUser;Password=*****;
Initial Catalog=MyContainedDatabase;
Server=MyServer;"
```

Where `MyContainedDatabase` is a database within the contained AG that you wish to interact with.

**This means that you must create a listener for the contained AG to effectively use a contained AG.** If you connect to one of the *instances* hosting the contained AG rather than *directly to the contained AG through the listener*, you're in the environment of the instance, and not the contained AG.

For example, if your availability group `MyContainedAG` is hosted on server `SERVER\MSSQLSERVER`, and instead of connecting to the listener `MyContainedAG_Listener`, you connect to the instance using `SERVER\MSSQLSERVER`, you're in the environment of the instance, and not in the environment of `MyContainedAG`. This means you're subject to the contents (users, permissions, jobs, etc.) that are found in the system databases of the instance. To access the contents found in the contained system databases of the contained AG, connect to the contained AG listener (`MyContainedAG_Listener`, for example) instead. When you're connected to the instance through the contained AG listener, when you interact with `master`, you're actually redirected to the contained `master` database (for example, `MyContainedAG_master`).

#### Read-only routing and contained availability groups

If you configure read-only routing to redirect connections with read intent to a secondary replica (see [Configure read-only routing for an Always On availability group](configure-read-only-routing-for-an-availability-group-sql-server.md)) and you wish to connect using a login that is created in the contained AG only, there are some additional considerations:

- You must specify a database that is part of the contained AG in the connection string
- The user specified in the connection string must have permission to access the databases in the contained AG.

For example in the following connection string, where `AdventureWorks` is a database within the contained AG that has `MyContainedListener`, and where `MyUser` is a user defined in the contained AG and none of the participating instances:

```output
"Persist Security Info=False;
User ID=MyUser;Password=*****;
Initial Catalog=AdventureWorks;
Server=MyContainedListener;
ApplicationIntent=ReadOnly"
```

This connection string would get you connected to the readable secondary that is part of the ReadOnly Routing configuration, and you would be within the context of the contained AG.

#### Differences between connecting to the instance and connecting to the contained availability group

- When connected to a contained AG, users only see databases in the contained AG, plus `tempdb`.
- At instance level, the contained AG `master` and `msdb` names are `[contained AG]_master`, and `[contained AG]_msdb`. Inside the contained AG, their names are `master` and `msdb`.
- The database ID for the contained AG `master` is `1` from inside the contained AG, but something else when connected to the instance.
- While users don't see databases outside of the contained AG in `sys.databases` when connected in a contained AG connection, they can access those databases by three-part name, or through the `USE` command.
- Server configuration through `sp_configure` can be read from the contained AG connection but can only be written from instance level.
- From contained AG connections, the sysadmin is able to perform instance level operations, such as shutting down [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].
- Most database level, endpoint level, or AG level operations can only be performed from instance connections, not contained AG connections.

## Interactions with other features

There are additional considerations when using certain features with contained AGs, and there are some features that are currently unsupported.

### Not supported

Currently, the following [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] features aren't supported with a contained AG:

- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Replication of any type (transactional, merge, snapshot, and so on).
- Distributed availability groups.
- Log shipping where the target database is in the contained AG. Log shipping with the source database in the contained AG is supported.

### Change data capture

Change data capture (CDC) is implemented as SQL Agent jobs, so the SQL Agent needs to be running on all instances with replicas in the contained AG.

To use change data capture with a contained AG, connect to the AG listener when you configure CDC so that the CDC metadata is configured using the contained system databases.

### Log shipping

Log shipping can be configured if the source database is in the contained AG. However, a log shipping target isn't supported within a contained AG. Additionally, there's an extra step to modify the log shipping job after CDC is configured.

To configure log shipping with a contained AG, follow these steps:

1. Connect to the contained AG listener.
1. Configure [log shipping](../../log-shipping/configure-log-shipping-sql-server.md) as you normally would.
1. After the log shipping job is configured, alter the job to connect to the contained AG listener before taking a backup.

### Transparent data encryption (TDE)

To use transparent data encryption (TDE) with databases in a contained AG, manually install the Database Master Key (DMK) to the contained `master` database within the contained AG.

Databases that use TDE rely on certificates in the `master` database to decrypt the Database Encryption Key (DEK). Without that certificate, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] can't decrypt databases encrypted with TDE or bring them online. In a contained AG, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] checks both `master` databases for the DMK, the `master` database for the instance, and the contained `master` database within the contained AG to decrypt the database. If it can't find the certificate in either location, then [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] is unable to bring the database online.

To transfer the DMK from the `master` database of the instance, to the contained `master` database, see [Move a TDE protected database to another SQL Server](../../../relational-databases/security/encryption/move-a-tde-protected-database-to-another-sql-server.md), primarily focusing on the portions where the DMK is transferred from the old server to the new one.

### SSIS packages & maintenance plans

Using SSIS packages, including maintenance plans, is not supported with contained availability groups.

## DDL changes

The only DDL changes are in the `CREATE AVAILABILITY GROUP` workflow. There are two new `WITH` clauses:

```syntaxsql
<with_option_spec> ::=
CONTAINED |
REUSE_SYSTEM_DATABASES
```

#### CONTAINED

This specifies that the AG being created should be a contained AG.

#### REUSE_SYSTEM_DATABASES

This option is only valid for contained AGs, and specifies that the newly created AG should reuse existing contained system databases for a previous contained AG of the same name. For example, if you had a contained AG with the name `MyContainedAG`, and wanted to drop and recreate it, you could use this option to reuse the contents of the original contained system databases.

## DMV changes

There are two additions to DMVs related to contained AGs:

- The DMV `sys.dm_exec_sessions` has an added column: `contained_availability_group_id`
- The `sys.availability_groups` catalog view has the added column: `is_contained`

## Related content

- [CREATE AVAILABILITY GROUP (Transact-SQL)](../../../t-sql/statements/create-availability-group-transact-sql.md)
