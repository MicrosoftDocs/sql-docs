---
title: "Upgrade or Patch Replicated Databases"
description: SQL Server supports upgrading replicated databases from previous versions of SQL Server without stopping activity on other nodes.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/26/2025
ms.service: sql
ms.subservice: install
ms.topic: upgrade-and-migration-article
helpviewer_keywords:
  - "merge replication database upgrades [SQL Server replication]"
  - "replication [SQL Server], upgrading"
  - "transactional replication, upgrading databases"
  - "snapshot replication [SQL Server], upgrading databases"
  - "upgrading replicated databases"
monikerRange: ">=sql-server-2017"
---
# Upgrade or patch replicated databases

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

[!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)] supports upgrading replicated databases from previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]; it isn't required to stop activity at other nodes while a node is being upgraded.

For peer-to-peer replication, see [Upgrade or patch Peer-to-peer replicated databases](upgrade-peer-to-peer-replicated-databases.md).

## Prerequisites

Ensure that you adhere to the rules regarding which versions are supported in a topology:

- A Distributor can be any version as long as it's greater than or equal to the Publisher version (in many cases the Distributor is the same instance as the Publisher).

- A Publisher can be any version as long as it less than or equal to the Distributor version.

- The Publisher and Distributor must be the same product. Either both are SQL Server, or both are Azure SQL Managed Instance.

- Subscriber version depends on the type of publication:

  - A Subscriber to a transactional publication can be any version within two versions of the Publisher version. For example: a [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] Publisher can have [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Subscribers; and a [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Publisher can have [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] Subscribers.

  - A Subscriber to a merge publication can be all versions equal to or lower than the Publisher version, whichever is supported as per the versions life cycle support cycle.

## Upgrade paths

The upgrade path to SQL Server is different depending on the deployment pattern. SQL Server offers two upgrade paths in general:

- Side-by-side: Deploy a parallel environment and move databases along with the associated instance level objects, such as logins, jobs, etc. to the new environment.

- In-place upgrade: Allow the SQL Server installation media to upgrade the existing SQL Server installation by replacing the SQL Server bits, and upgrading the database objects. For environments running availability groups (AGs) or failover cluster instances (FCIs), an in-place upgrade is combined with a [rolling upgrade](choose-a-database-engine-upgrade-method.md#rolling-upgrade) to minimize downtime.

A common approach for side-by-side upgrades of replication topologies is to move publisher-subscriber pairs in parts to the new side-by-side environment, as opposed to a movement of the entire topology. This phased approach helps control downtime, and minimizes the impact to a certain extent for the business dependent on replication.

Most this article is scoped toward upgrading the version of SQL Server. However, the in-place upgrade process should also be used when patching SQL Server with a service pack or cumulative update as well.

## Remarks

Upgrading a replication topology is a multi-step process. We recommend attempting an upgrade of a replica of your replication topology in a test environment before running the upgrade on the actual production environment. This helps iron out any operational documentation that is required for handling the upgrade smoothly without incurring expensive and long downtimes during the actual upgrade process. You can reduce downtime significantly with the use of AGs and/or FCIs for their production environments while upgrading their replication topology. Additionally, we recommend taking backups of all the databases including `msdb`, `master`, Distribution databases, and the user databases participating in replication before attempting the upgrade.

When you have a distribution database in a failover cluster instance, make sure that all participating nodes use the same build. We don't recommend a setup in which one node is a SQL Server version earlier than [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2-CU3 or [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU6 and the other node is a SQL Server version later than [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2-CU3 or [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU6. Beginning in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2-CU3 and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU6, support is added for using a distribution database in an AG and for new objects (tables, stored procedures) in distribution databases. If your distribution database is in a failover cluster instance and you're doing a phased migration (and you can't upgrade all nodes to the same version of SQL Server), for the narrow migration timeframe, we recommend that you do account tasks like adding a new subscriber, subscription, publisher, or publication on the node that has the later version of SQL Server.

[!INCLUDE [sql-25-repl-info](../../includes/sql-25-repl-info.md)]

## Replication matrix

### Transactional and snapshot replication compatibility matrix

[!INCLUDE [replication-compat-matrix-transactional](../../includes/replication-compat-matrix-transactional.md)]

### Merge replication compatibility matrix

[!INCLUDE [replication-compat-matrix-merge](../../includes/replication-compat-matrix-merge.md)]

## Upgrade considerations

### Run the Log Reader Agent for transactional replication before upgrade

Before you upgrade [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)], you must make sure that all committed transactions from published tables were processed by the Log Reader Agent. To make sure that all transactions are processed, perform the following steps for each database that contains transactional publications:

1. Make sure that the Log Reader Agent is running for the database. By default, the agent runs continuously.

1. Stop user activity on published tables.

1. Allow time for the Log Reader Agent to copy transactions to the distribution database, and then stop the agent.

1. Execute [sp_replcmds](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md) to verify that all transactions are processed. The result set from this procedure should be empty.

1. Execute [sp_replflush](../../relational-databases/system-stored-procedures/sp-replflush-transact-sql.md) to close the connection from `sp_replcmds`

1. Perform the server upgrade to the latest version of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)].

1. Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent and the Log Reader Agent if they don't start automatically after the upgrade.

### Run agents for merge replication after upgrade

After upgrade, run the Snapshot Agent for each merge publication and the Merge Agent for each subscription to update replication metadata. You don't have to apply the new snapshot, because it isn't necessary to reinitialize subscriptions. Subscription metadata is updated the first time the Merge Agent is run after upgrade. This means that the subscription database can remain online and active during the Publisher upgrade.

Merge replication stores publication and subscription metadata in several system tables in the publication and subscription databases. Running the Snapshot Agent updates publication metadata and running the Merge Agent updates subscription metadata. It's only required to generate a publication snapshot. If a merge publication uses parameterized filters, each partition also has a snapshot. It isn't necessary to update these partitioned snapshots.

Run the agents from [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], Replication Monitor, or from the command line. For more information about running the Snapshot Agent, see the following articles:

- [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)
- [Start and Stop a Replication Agent (SQL Server Management Studio)](../../relational-databases/replication/agents/start-and-stop-a-replication-agent-sql-server-management-studio.md)
- [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)
- [Replication Agent Executables Concepts](../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)

For more information about running the Merge Agent, see the following articles:

- [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md)
- [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md)

After upgrading [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in a topology that uses merge replication, change the publication compatibility level of any publications if you want to use new features.

### Upgrade to Standard, Workgroup, or Express editions

Before upgrading from one edition of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] to another, verify that the functionality you're currently using is supported in the edition to which you're upgrading. [!INCLUDE [editions-latest](../../includes/editions-latest.md)]

## Steps to upgrade a replication topology

These steps outline the order in which servers in a replication topology should be upgraded. The same steps apply whether you're running transactional or merge replication. However, these steps don't cover Peer-to-Peer replication, queued updating subscriptions, nor immediate updating subscriptions.

### In-place upgrade

1. Upgrade the Distributor.
1. Upgrade the Publisher and the Subscriber. These can be upgraded in any order.

> [!NOTE]  
> For [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)], the upgrade of the publisher and subscriber must be done at the same time to align with the replication topology matrix. [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] publishers or subscribers can't have a [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] (or greater) publisher nor subscriber. If upgrading at the same time isn't possible, use an intermediate upgrade to upgrade the SQL Server instances to [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], and then upgrade them again to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] (or greater).

### Side by side upgrade

1. Upgrade the Distributor.
1. Reconfigure [Configure Distribution](../../relational-databases/replication/configure-distribution.md) on the new SQL Server instance.
1. Upgrade the Publisher.
1. Upgrade the Subscriber.
1. Reconfigure all Publisher-Subscriber pairs, including reinitialization of the Subscriber.

## Steps for side-by-side migration of the Distributor to Windows Server

A side-by-side upgrade is the only upgrade path available for SQL Server instances participating in a failover cluster. The following steps can be performed on either a standalone SQL Server instance, or one within a Failover Cluster Instance (FCI).

1. Set up a new SQL Server instance (either standalone, or FCI), edition, and version as your distributor on Windows Server with a different Windows cluster and SQL Server FCI name or standalone host name. You need to keep the directory structure same as the old distributor to ensure that the replication agents executables, replication folders, and database file paths are found at the same path on the new environment. This reduces any post migration/upgrade steps required.

1. Ensure that your replication is synchronized and then shut down all of the replication agents.

1. Shut down the current SQL Server Distributor instance. If this is a standalone instance, shut down the server. If this is a SQL Server FCI, then take the entire SQL Server role offline in cluster manager, including the network name.

1. Remove the DNS and Active Directory computer object entries for the old (current distributor instance) environment.

1. Change the hostname of the new server to match that of the old server.

   1. If this is a SQL Server FCI, rename the new SQL Server FCI with the same virtual server name as the old instance.

1. Copy the database files from the previous instance using SAN redirection, storage copy, or file copy.

1. Bring the new SQL Server instance online.

1. Restart all of the replication agents and verify if the agents are running successfully.

1. Validate if replication is working as expected.

1. Use the SQL Server setup media to run an in-place upgrade of your SQL Server instance to the new version of SQL Server.

> [!NOTE]  
> To reduce downtime, we recommend that you perform the *side-by-side migration* of the distributor as one activity, and the *in-place upgrade to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]* as another activity. This allows you to take a phased approach, reduce risk, and minimize downtime.

## Web synchronization for merge replication

The Web synchronization option for merge replication requires that you copy the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener (`replisapi.dll`) to the virtual directory on the Internet Information Services (IIS) server used for synchronization. When you configure Web synchronization, the Configure Web Synchronization Wizard copies the file to the virtual directory. If you upgrade the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components installed on the IIS server, you must manually copy replisapi.dll from the COM directory to the virtual directory on the IIS server. For more information about configuring Web synchronization, see [Configure Web Synchronization](../../relational-databases/replication/configure-web-synchronization.md).

## Restore a replicated database from an earlier version

To ensure replication settings are retained when restoring a backup of a replicated database from a previous version: restore to a server and database with the same names as the server and database at which the backup was taken.

## Related content

- [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md)
- [Replication Administration FAQ](../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.yml)
- [Replication backward compatibility](../../relational-databases/replication/replication-backward-compatibility.md)
- [Supported version and edition upgrades (SQL Server 2022)](supported-version-and-edition-upgrades-2022.md)
- [Upgrade SQL Server](upgrade-sql-server.md)
- [Upgrading a Replication Topology to SQL Server 2016](/archive/blogs/sql_server_team/upgrading-a-replication-topology-to-sql-server-2016)
