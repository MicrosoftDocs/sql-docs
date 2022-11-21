---
title: "What is a contained availability group?"
description: "An overview of the contained availability group feature of Always On availability groups within SQL Server."
author: kfarlee
ms.author: kfarlee
ms.reviewer: mathoma
ms.date: "05/11/2020"
ms.service: sql
ms.subservice: high-availability
ms.topic: conceptual
ms.custom: event-tier1-build-2022
helpviewer_keywords:
  - "availability groups [SQL Server], contained availability groups"
  - "availability groups [SQL Server], configuring"
monikerRange: ">=sql-server-ver16"
---

# What is a contained availability group?

[!INCLUDE [sqlserver2022](../../../includes/applies-to-version/sqlserver2022.md)]

A contained availability group is an Always On availability group that supports:

- managing metadata objects (users, logins, permissions, SQL Agent jobs etc.) at the availability group level in addition to the instance level.
- specialized contained system databases within the availability group.

This article details the similarities, differences, and functionalities of contained availability groups.

## Overview

Always On availability groups generally consist of one or more user databases intended to operate as a coordinated group, and which are replicated on some number of nodes in a cluster. When there is a failure in the node, or in the health of SQL Server on the node that hosts the primary copy, the group of databases are moved as a unit to another replica node in the availability group.  All of the user databases are kept in sync across all replicas of the availability group, either in synchronous or asynchronous mode.

This works well for applications that only interact with that set of user databases, but there are challenges when applications also rely on objects such as users, logins, permissions, agent jobs, etc., which are stored in one of the system databases (`master` or `msdb`). For the applications to function smoothly and predictably, the admin must _manually_ ensure that any change to these objects is duplicated across all replica instances in the availability group.  If a new instance is brought into the availability group, the databases can be automatically or manually seeded in a straightforward process, but then all of the system database customizations must be reconfigured on the new instance to match the other replicas.
  
Contained availability groups extend the concept of the group of databases being replicated to include relevant portions of the `master` and `msdb` databases.  Think of it as the execution context for applications using the contained availability group. The idea is that the contained AG environment includes settings that would affect the application relying on them.  As such, the contained AG environment concerns all databases the application interacts with, the authentication it uses (logins, users, permissions), any scheduled jobs that it expects to be running, and other configuration settings that impact the application.

This is different from contained databases, which use a different mechanism for the user accounts, storing the user information within the database itself.  Contained databases only replicate logins and users, and the scope of the replicated login or user is limited to that single database (and its replicas).

In contrast, in a contained availability group, you can create users, logins, permissions, etc. at the availability group level, and they will _automatically_ be consistent across replicas in the availability group, as well as consistent across databases within that contained availability group. This saves the admin from having to manually make these changes themselves.

## Differences

There are some practical differences to consider when working with contained availability groups, such as the creation of contained system databases, and forcing the  connection at the contained availability group level, rather than connecting at the instance level.

### Contained System Databases

Each contained availability group has its own `master` and `msdb` system databases, named after the name of the availability group. For example, in contained availability group `MyContainedAG`, you will have databases named `MyContainedAG_master` and `MyContainedAG_msdb`. These system databases are automatically seeded to new replicas and updates are replicated to these databases just like any other database in an availability group.  This means that when you add an object such as a login, or agent job while connected to the contained availability group, when the contained availability group fails over to another instance, connecting to the contained availability group, you will still see the agent jobs, and be able to authenticate using the login created in the contained availability group.

>[!IMPORTANT]
>Contained availability groups are a mechanism for keeping execution environment configurations consistent across the replicas of an availability group.  They do NOT represent a security boundary.  There is no boundary which keeps a connection to a contained availability group from accessing databases outside of the AG, for example.

The system databases in a newly created contained availability group are not copies from the instance where the CREATE AVAILABILITY GROUP command is run.  They are initially empty templates without any data.  Immediately after creation, the admin accounts on the instance creating the contained AG are copied into Contained Master.  That way the admin can log into the contained AG and set up the rest of the configuration.  If you've created local users or configurations in your instance, they will not automatically appear when you create your contained system databases, and they will not be visible when you connect to the contained availability group. You need to manually re-create them in the contained system databases within the context of the contained availability group.  The exception to this is that all of the logins in the sysadmin role in the parent instance are copied into the new AG specific master DB.

### Connect (Contained environment)

It's important to distinguish the difference between connecting to the instance, and connecting to the contained availability group. The only way to access the environment of the contained availability group is to connect to the contained availability group listener, or to connect to a database which is in the contained availability group. i.e.

```csharp
"Persist Security Info=False;
User ID=MyUser;Password=*****;
Initial Catalog=MyContainedDatabase;
Server=MyServer;"
```

Where MyContainedDatabase is a database within the contained availability group which you wish to interact with.

**This means that you must create a listener for the contained availability group to effectively use a contained availability group.** If you connect to one of the _instances_ hosting the  contained availability group rather than _directly to the contained availability group through the listener_, you will be in the environment of the instance, and not the contained availability group.

For example, if your availability group `MyContainedAG` is hosted on server `SERVER\MSSQLSERVER`, and instead of connecting to the listener `MyContainedAG_Listener`, you connect to the instance using `SERVER\MSSQLSERVER`, you will be in the environment of the instance, and not in the environment of `MyContainedAG`. This means you will be subject to the contents (users, permissions, jobs, etc.) that are found in the system databases of the instance. To access the contents found in the contained system databases of the contained availability group, connect to the contained availability group listener (`MyContainedAG_Listener`, for example) instead. When you are connected to the instance through the contained availability group listener,  when you interact with `master`, you are actually redirected to the contained `master` database (for example,  `MyContainedAG_`master`).

#### Read-only routing and contained availability groups

If you have configured read-only routing to redirect connections with read intent to a secondary replica (see [Configure read-only routing for an Always On availability group](./configure-read-only-routing-for-an-availability-group-sql-server.md)) and you wish to connect using a login which is created in the contained availability group only, there are some additional considerations:

- You must specify a database which is part of the contained availability group in the connection string
- The user specified in the connection string must have permission to access the database(s) in the contained availability group.

For example in the following connection string, where AdventureWorks is a database within the contained availability group which has MyContainedListener, and where _MyUser_ is a user defined in the contained availability group and none of the participating instances:

```csharp
"Persist Security Info=False;
User ID=MyUser;Password=*****;
Initial Catalog=AdventureWorks;
Server=MyContainedListener;
ApplicationIntent=ReadOnly"
```

This connection string would get you connected to the readable secondary which is part of the ReadOnly Routing configuration, and you would be within the context of the contained availability group.

#### Differences between connecting to the instance and connecting to the contained availability group

- When connected to contained AG, users will only see databases in the contained AG, plus tempdb.
- At instance level, contained AG master and msdb names will be [contained AG]_master, and [contained AG]_msdb. Inside contained AG, their names are master and msdb.
- Database ID for contained AG master is 1 from inside contained AG, but something else when connected to the instance.
- While users will not see databases outside of the contained AG in sys.databases when connected in a contained AG connection, they will be able to access those databases by three part name or through the _use_ command.
- Server configuration through sp_configure can be read from contained AG connection but can only be written from instance level.
- From contained AG connections, sysadmin is able to perform instance level operations, such as shutting down SQL Server.
- Most DB level, end point level, or AG level operations can only be performed from instance connections, not contained AG connections.

## Interactions with other features

There are additional considerations when using certain features with contained availability groups, and there are some features that are currently unsupported.

### Not supported

Currently, the following SQL Server features are not supported with a contained availability group:

- SQL Server Replication of any type (transactional, merge, snapshot, etc.).
- Distributed availability groups.
- Log shipping where the target database is in the contained availability group. Log shipping with the source database in the contained availability group is supported.

### Change Data Capture

Change data capture (CDC) is implemented as SQL Agent jobs, so the SQL Agent needs to be running on all instances with replicas in the contained availability group.

To use change data capture with a contained availability group, connect to the availability group listener when you configure CDC so that the CDC metadata is configured using the contained system databases.  

### Log Shipping

Log shipping can be configured if the source database is in the contained availability group. However, a log shipping target is not supported within a contained availability  group. Additionally, there is an extra step to modify the log shipping job after CDC is configured.

To configure log shipping with a contained availability group, do the following:

1. Connect to the contained availability group listener.
1. Configure [log shipping](../../log-shipping/configure-log-shipping-sql-server.md) as you normally would.
1. After the log shipping job is configured, alter the job to connect to the contained availability group listener before taking a backup.

### Transparent Data Encryption (TDE)

To use transparent data encryption (TDE) with databases in a contained availability group, manually install the Database Master Key (DMK) to the contained `master` database within the contained availability group.

Databases that use TDE rely on certificates in the `master` database to decrypt the Database Encryption Key (DEK). Without that certificate, SQL Server cannot decrypt databases encrypted with TDE or bring them online. In a contained availability group, SQL Server will check both `master` databases for the Database Master Key (DMK), the `master` database for the instance, and the contained `master` database within the contained availability group to decrypt the database. If it cannot find the certificate in either location, then SQL Server will be unable to bring the database online.

To transfer the DMK from the `master` database of the instance, to the contained `master` database, see [Move a TDE Protected Database to Another SQL Server](../../../relational-databases/security/encryption/move-a-tde-protected-database-to-another-sql-server.md), primarily focusing on the portions where the DMK is transferred from the old server to the new one.

## DDL changes

The only DDL changes are in the CREATE AVAILABILITY GROUP workflow.  There are two new 'WITH' clauses:

```sql

<with_option_spec>::=
CONTAINED |
REUSE_SYSTEM_DATABASES

```  

`CONTAINED`
This specifies that the availability group being created should be a contained availability group

`REUSE_SYSTEM_DATABASES`
This option is only valid for CONTAINED availability groups, and specifies that the newly created availability group should reuse existing contained system databases for a previous contained availability group of the same name.  For example, if you had a contained availability group with the name _MyContainedAG_, and wanted to drop and recreate it, you could use this option to reuse the contents of the original contained system databases.

## DMV Changes

 There are two additions to DMVs related to contained availability groups:

- The DMV `SYS.DM_EXEC_SESSIONS` has an added column: CONTAINED_AVAILABILITY_GROUP_ID
- The `SYS.AVAILABILITY_GROUPS` catalog view has the added column: IS_CONTAINED

## Next steps

To configure an availability group, see [CREATE AVAILABILITY GROUP (Transact-SQL)](../../../t-sql/statements/create-availability-group-transact-sql.md).
