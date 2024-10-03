---
title: "Snapshot Replication"
description: Snapshot replication distributes data as it appears at a moment in time. It doesn't monitor for updates. A snapshot is generated and sent to Subscribers.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "snapshot replication [SQL Server], about snapshot replication"
  - "snapshot replication [SQL Server]"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Snapshot replication

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Snapshot replication distributes data exactly as it appears at a specific moment in time and doesn't monitor for updates to the data. When synchronization occurs, the entire snapshot is generated and sent to Subscribers.

> [!NOTE]  
> Snapshot replication can be used by itself, but the snapshot process (which creates a copy of all of the objects and data specified by a publication) is also commonly used to provide the initial set of data and database objects for transactional and merge publications.

Using snapshot replication by itself is most appropriate when one or more of the following scenarios is true:

- Data changes infrequently.
- Copies of data that are out of date with respect to the Publisher for a period of time.
- Replicating small volumes of data.
- A large volume of changes occurs over a short period of time.

Snapshot replication is most appropriate when data changes are substantial but infrequent. For example, if a sales organization maintains a product price list and the prices are all updated at the same time once or twice each year, you should replicate the entire snapshot of data after it changes. Given certain types of data, more frequent snapshots might also be appropriate. For example, if a relatively small table is updated at the Publisher during the day, but some latency is acceptable, changes can be delivered nightly as a snapshot.

Snapshot replication has a lower continuous overhead on the Publisher than transactional replication, because incremental changes aren't tracked. However, if the data set being replicated is large, it requires substantial resources to generate and apply the snapshot. Consider the size of the entire data set and the frequency of changes to the data when evaluating whether to utilize snapshot replication.

## <a id="HowWorks"></a> How snapshot replication works

By default, all three types of replication use a snapshot to initialize Subscribers. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Snapshot Agent always generates the snapshot files, but the agent that delivers the files differs depending on the type of replication being used. Snapshot replication and transactional replication use the Distribution Agent to deliver the files, whereas merge replication uses the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Merge Agent. The Snapshot Agent runs at the Distributor. The Distribution Agent and Merge Agent run at the Distributor for push subscriptions, or at Subscribers for pull subscriptions.

Snapshots can be generated and applied either immediately after the subscription is created or according to a schedule set at the time the publication is created. The Snapshot Agent prepares snapshot files containing the schema and data of published tables and database objects, stores the files in the snapshot folder for the Publisher, and records tracking information in the distribution database on the Distributor. You specify a default snapshot folder when you configure a Distributor, but you can specify an alternate location for a publication instead of or in addition to the default.

In addition to the standard snapshot process described in this article, a two-part snapshot process is used for merge publications with parameterized filters.

The following illustration shows the principal components of snapshot replication.

:::image type="content" source="media/snapshot-replication/snapshot.png" alt-text="Screenshot of Snapshot replication components and data flow.":::

## <a id="SnapshotAgent"></a> Snapshot Agent

For merge replication, a snapshot is generated every time the Snapshot Agent runs. For transactional replication, snapshot generation depends on the setting of the publication property `immediate_sync`. If the property is set to `true` (the default when using the New Publication Wizard), a snapshot is generated every time the Snapshot Agent runs, and it can be applied to a Subscriber at any time. If the property is set to `false` (the default when using `sp_addpublication`), the snapshot is generated only if a new subscription was added since the last Snapshot Agent run; Subscribers must wait for the Snapshot Agent to complete before they can synchronize.

The Snapshot Agent performs the following steps:

1. Establishes a connection from the Distributor to the Publisher, and then takes locks on published tables if necessary:

   - For merge publications, the Snapshot Agent doesn't take any locks.

   - For transactional publications, by default the Snapshot Agent takes locks only during the initial phase of snapshot generation.

   - For snapshot publications, locks are held during the entire snapshot generation process.

1. Writes a copy of the table schema for each article to a `.sch` file. More script files are generated if other database objects are published, such as indexes, constraints, stored procedures, views, user-defined functions, and so on.

1. Copies the data from the published table at the Publisher and writes the data to the snapshot folder. The snapshot is generated as a set of bulk copy program (BCP) files.

1. For snapshot and transactional publications, the Snapshot Agent appends rows to the `MSrepl_commands` and `MSrepl_transactions` tables in the distribution database. The entries in the `MSrepl_commands` table are commands indicating the location of `.sch` and `.bcp` files, any other snapshot files, and references to any pre- or post-snapshot scripts. The entries in the `MSrepl_transactions` table are commands relevant to synchronizing the Subscriber.

   For merge publications, the Snapshot Agent performs extra steps.

1. Releases any locks on published tables.

During snapshot generation, you can't make schema changes on published tables. After the snapshot files are generated, you can view them in the snapshot folder using Windows Explorer.

## <a id="DistAgent"></a> Distribution Agent and Merge Agent

For snapshot publications, each time the Distribution Agent runs for the publication, it moves a new snapshot to each Subscriber that:

- isn't yet synchronized,
- is marked for reinitialization, or
- includes new articles.

For snapshot and transactional replication, the Distribution Agent performs the following steps:

1. Establishes a connection to the Distributor.

1. Examines the `MSrepl_commands` and `MSrepl_transactions` tables in the distribution database on the Distributor. The agent reads the location of the snapshot files from the first table and Subscriber synchronization commands from both tables.

1. Applies the schema and commands to the subscription database.

For an unfiltered merge replication publication, the Merge Agent performs the following steps:

1. Establishes a connection to the Publisher.

1. Examines the `sysmergeschemachange` table on the Publisher and determines whether there's a new snapshot that should be applied at the Subscriber.

1. If a new snapshot is available, the Merge Agent applies to the subscription database the snapshot files from the location specified in `sysmergeschemachange`.

## Related content

- [Snapshot Agent](snapshot-agent.md)
- [Modify Snapshot Initialization Options for SQL Replication](snapshot-options.md)
- [Configure the Windows Firewall to allow SQL Server access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)
