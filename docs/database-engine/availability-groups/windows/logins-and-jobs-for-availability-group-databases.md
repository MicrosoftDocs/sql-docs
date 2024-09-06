---
title: "Manage job logins for databases in an availability group"
description: Learn how to manage logins for jobs that use databases that participate in an availability group.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 09/06/2024
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
helpviewer_keywords:
  - "Availability Groups [SQL Server], deploying"
  - "Availability Groups [SQL Server], failover"
  - "failover [SQL Server], AlwaysOn Availability Groups"
  - "failover [SQL Server], Always On Availability Groups"
---
# Manage logins for jobs using databases in an Always On availability group

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

You should routinely maintain the same set of user logins and [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Agent jobs on every primary database of an Always On availability group (AG), and the corresponding secondary databases. The logins and jobs must be reproduced on every instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts an availability replica for the AG.

- **SQL Server Agent jobs**

  You need to manually copy relevant jobs from the server instance that hosts the original primary replica to the server instances that host the original secondary replicas. For all databases, you need to add logic at the beginning of each relevant job to make the job execute only on the primary database, that is, only when the local replica is the primary replica for the database.

  The server instances that host the availability replicas of an AG might be configured differently, with different drive letters for example. The jobs for each availability replica must allow for any such differences.

  Backup jobs can use the [sys.fn_hadr_backup_is_preferred_replica](../../../relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql.md) function to identify whether the local replica is the preferred one for backups, according to the AG backup preferences. Backup jobs created using the [Use the Maintenance Plan Wizard](../../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md) natively use this function. For other backup jobs, we recommend that you use this function as a condition in your backup jobs, so they execute only on the preferred replica. For more information, see [Offload supported backups to secondary replicas of an availability group](active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md).

- **Logins**

  If you're using contained databases, you can configure contained users in the databases, and for these users, you don't need to create logins on the server instances that host a secondary replica. For a non-contained availability database, you need to create users for the logins on the server instances that host the availability replicas. For more information, see [CREATE USER](../../../t-sql/statements/create-user-transact-sql.md).

  If any of your applications use [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication or a local Windows login, see [Logins Of Applications That Use SQL Server Authentication or a Local Windows Login](../../../database-engine/availability-groups/windows/logins-and-jobs-for-availability-group-databases.md#SSauthentication), later in this article.

  > [!NOTE]  
  > A database user for which the SQL Server login is undefined or is incorrectly defined on a server instance can't log in to the instance. Such a user is said to be an *orphaned user* of the database on that server instance. If a user is orphaned on a given server instance, you can set up the user logins at any time. For more information, see [Troubleshoot orphaned users (SQL Server)](../../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md).

- **Additional metadata**

  Logins and jobs aren't the only information that need to be recreated on each of the server instances that hosts a secondary replica for a given AG. For example, you might need to recreate server configuration settings, credentials, encrypted data, permissions, replication settings, service broker applications, triggers (at server level), and so on. For more information, see [Manage Metadata When Making a Database Available on Another Server](../../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md).

## <a id="SSauthentication"></a> SQL Server authentication or a local Windows login

If an application uses SQL Server Authentication or a local Windows login, mismatched security identifiers (SIDs) can prevent the application's login from resolving on a remote instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. The mismatched SIDs cause the login to become an orphaned user on the remote server instance. This issue can occur when an application connects to a mirrored or log shipping database after a failover or to a replication subscriber database that was initialized from a backup.

You should take preventative measures when you set up an application to use a database hosted by a remote instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. Prevention involves transferring the logins and the passwords from the local instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] to the remote instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information about how to prevent this issue, see KB article 918992-[Transfer logins and passwords between instances of SQL Server](/troubleshoot/sql/database-engine/security/transfer-logins-passwords-between-instances)).

> [!NOTE]  
> This problem affects Windows local accounts on different computers. However, this problem doesn't occur for domain accounts because the SID is the same on each of the computers.

For more information, see [Orphaned Users with Database Mirroring and Log Shipping](/archive/blogs/sqlserverfaq/orphaned-users-with-database-mirroring-and-log-shipping) (a Database Engine blog).

## Related tasks

- [Create a Login](../../../relational-databases/security/authentication-access/create-a-login.md)
- [Create a database user](../../../relational-databases/security/authentication-access/create-a-database-user.md)
- [Create a Job](../../../ssms/agent/create-a-job.md)
- [Manage Metadata When Making a Database Available on Another Server](../../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md)

## Related content

- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Contained Databases](../../../relational-databases/databases/contained-databases.md)
- [Create Jobs](../../../ssms/agent/create-jobs.md)
