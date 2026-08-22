---
title: How Merge Replication Initializes Publications and Subscriptions
description: Merge replication must initialize both the publisher and subscriber before data can flow between them.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 07/18/2025
ms.service: sql
ms.subservice: replication
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "merge replication [SQL Server replication], initialize publications and subscriptions"
---
# How merge replication initializes publications and subscriptions

Merge replication must initialize both the Publisher and Subscriber before data can flow between them. This article provides information on the steps that occur during initialization.

## Initialize the publication

The following list details the initialization steps for a publication, which occur as you execute each stored procedure listed or after you complete the New Publication Wizard. Further initialization occurs after the Snapshot Agent runs for the first time for a publication.

- `sp_replicationdboption`

  - The publication database is marked for replication. The database can't be dropped unless replication is removed.

  - System tables are added to the publication database (unless a merge publication already exists in the database). For a complete list of system tables, see the section "System Tables Created in the Publication and Subscription Databases" in this article.

- `sp_addmergepublication`

  - Entries for the publication are added to the system tables.

- `sp_addpublication_snapshot`

  - A Snapshot Agent job is added to the SQL Server Agent system. The job name is in the form `<Publisher>-<PublicationDatabase>-<Publication>-<Integer>`.

- `sp_addmergearticle`

  - Each replicated object is marked for replication. The object can't be deleted unless the corresponding article is dropped from all publications.

  - Entries for each article are added to the system tables.

The remainder of the initialization for the publication database occurs during the initial run of the Snapshot Agent for a publication. The publication database isn't reinitialized during subsequent runs of the Snapshot Agent. If you use the New Publication Wizard, the initial snapshot is created by default after you complete the wizard. If you use stored procedures, you must run the agent job or run the agent directly. For more information about running agents, see [Start and Stop a Replication Agent (SQL Server Management Studio)](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md) and [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md).

The first time the Snapshot Agent for a publication runs:

- A column named `rowguid` is added to each published table, unless the table already has a column of data type uniqueidentifier with the `ROWGUIDCOL` property set (in which case this column is used). The `rowguid` column is used to uniquely identify every row in every published table. If the table is dropped from the publication, the `rowguid` column is removed; if an existing column was used for tracking, the column isn't removed.

- The following objects are created in the publication database for each published table (all objects are created in the `dbo` schema):

  - Insert triggers, update triggers, and delete triggers are added to published tables to track changes. The triggers are named in the form `MSmerge_ins_<GUID>`, `MSmerge_upd_<GUID>`, and `MSmerge_del_<GUID>`. The GUID value is derived from the entry for the article in the system table `sysmergearticles`.

  - Stored procedures are created to handle inserts, updates, and deletes to published tables, and to perform several other replication-related operations.

  - Views are created to manage inserts, updates, deletes, and filtering.

  - Conflict tables are created to store conflict information. The conflict tables match the schema of the published tables: each published table is scripted, and then the script is used to create the conflict table in the publication database. Conflict tables are named in the form `dbo.MSmerge_conflict_<Publication>_<Article>`.

Every time the Snapshot Agent runs, the following types of files (with their corresponding file extensions) are created for each article in the publication database:

- Schema (`.sch`)

- Constraints and indexes (`.dri`)

- Triggers (`.trg`)

- System table data (`.sys`)

- Conflict tables (`.cft`)

- Data (`.bcp`): not created for publications with parameterized filters.

  If the publication doesn't use any parameterized filters, the snapshot contains the data for the published tables in a set of `.bcp` files. If the publication uses parameterized filters (which is typical for merge publications), the initial snapshot doesn't contain any data. Data is provided using a snapshot for a Subscriber's partition, which is discussed in the next section.

## Initialize a subscription

Each subscription is initialized when the Merge Agent for the subscription runs and copies the initial snapshot to the subscription database. In addition to the schema and data from replicated objects, the snapshot contains the system tables, views, triggers, and stored procedures that exist in the publication database. One or two extra system tables are also copied to the subscription database. For a complete list of system tables, see the section [System tables created in the publication and subscription databases](#system-tables-created-in-the-publication-and-subscription-databases) in this article. If a subscription is reinitialized, all replicated objects and replication system objects are overwritten.

If none of the tables in the publication database use parameterized filters, the same publication snapshot is copied to each Subscriber. If one or more parameterized filters are used, the way in which each subscription is initialized is governed by the following logic:

- If the snapshot location is provided to the Merge Agent on the command line:

  - Apply the snapshot from this location.

- Else if the snapshot was pregenerated:

  - Retrieve the location of the snapshot from `MSmerge_dynamic_snapshots` in the publication database and apply the snapshot from that location.

- Else if the publication allows Subscribers to initiate snapshots:

  - If a snapshot has already been generated for another Subscriber with the same partition, apply that snapshot to the Subscriber.

  - Else generate and apply a snapshot to the Subscriber.

- Else initialize the Subscriber using `SELECT` statements against the tables in the publication. This approach is slower than using a snapshot for the Subscriber's partition.

If the snapshot transfer is interrupted at any point, it automatically resumes, and doesn't resend any files that have already been completely transferred. The unit of delivery for the Snapshot Agent is the bcp file for each publication article, so files that are partially delivered must be completely redelivered. However, resuming the snapshot can significantly reduce the amount of data transmitted and ensure timely snapshot delivery even if the connection is unreliable. For more information about creating snapshots, see [Parameterized Filters - Parameterized Row Filters](parameterized-filters-parameterized-row-filters.md).

### Snapshot location

The snapshot location depends on: the path specified for the default or alternate snapshot location; whether the publication uses a UNC path or FTP share for the snapshot folder; and whether the publication uses parameterized filters. In these examples, assume that the snapshot folder location is: `\\<MyComputer>\<MyFolder>`:

- If the publication uses UNC, the first part of the path is: `\\<MyComputer>\<MyFolder>\unc\`. If it uses FTP, it's `\\<MyComputer>\<MyFolder>\ftp\`.

- If the publication uses UNC and doesn't use parameterized filters, the path is`\\<MyComputer>\<MyFolder>\unc\<Publisher><Publicationdb><publication>`

- If the publication uses UNC and uses parameterized filters, the location is based on the snapshot folder path and the parameterized row filtering criteria for the publication. For example, if the article is filtered using the `HOST_NAME()` function and the value of `HOST_NAME()` for the partition is `SalesLaptop`, the path to the snapshot for that partition is `\\<MyComputer>\<MyFolder>\unc\<Publisher><Publicationdb><publication>\SalesLaptop_12`, where `12` is the ID used internally for the partition.

## System tables created in the publication and subscription databases

The following tables are created in the publication database and each subscription database.

| Table | Description |
| --- | --- |
| [MSdynamicsnapshotjobs](../../system-tables/msdynamicsnapshotjobs-transact-sql.md) | Contains information on snapshot jobs for publications with parameterized filters. |
| [MSdynamicsnapshotviews](../../system-tables/msdynamicsnapshotviews-transact-sql.md) | Tracks all the temporary snapshot views created by the Snapshot Agent. It's used by the system for cleaning up views in the case of an abnormal shutdown of SQL Server Agent or the Snapshot Agent. |
| [MSmerge_altsyncpartners](../../system-tables/msmerge-altsyncpartners-transact-sql.md) | Tracks the association of who the current synchronization partners are for a Publisher. |
| [MSmerge_articlehistory](../../system-tables/msmerge-articlehistory-transact-sql.md) | Tracks changes made to articles during a Merge Agent synchronization session, with one row for each article to which changes were made. |
| [MSmerge_conflicts_info](../../system-tables/msmerge-conflicts-info-transact-sql.md) | Tracks conflicts that occur when synchronizing a subscription to a merge publication. |
| [MSmerge_contents](../../system-tables/msmerge-contents-transact-sql.md) | Contains one row for each row modified in the current database since it was published. This table is used by the merge process to determine the rows that have changed. |
| [MSmerge_current_partition_mappings](../../system-tables/msmerge-current-partition-mappings.md) | Contains one row for each partition that a given changed row belongs to. |
| [MSmerge_dynamic_snapshots](../../system-tables/msmerge-dynamic-snapshots-transact-sql.md) | Tracks the location of the snapshot for each partition defined for a merge publication. |
| [MSmerge_errorlineage](../../system-tables/msmerge-errorlineage-transact-sql.md) | Contains rows that have been deleted at the Subscriber, but whose delete isn't propagated to the Publisher. |
| [MSmerge_generation_partition_mappings](../../system-tables/msmerge-generation-partition-mappings-transact-sql.md) | Tracks whether a given generation contains any changes relevant to a given partition. |
| [MSmerge_genhistory](../../system-tables/msmerge-genhistory-transact-sql.md) | Contains one row for each generation. A generation is a collection of changes that is delivered to a Publisher or Subscriber. Generations are *closed* each time the Merge Agent runs; subsequent changes in a database are added to one or more *open generations*. |
| [MSmerge_history](../../system-tables/msmerge-history-transact-sql.md) | Contains history rows with detailed descriptions of the outcomes of previous Merge Agent job sessions. |
| [MSmerge_identity_range](../../system-tables/msmerge-identity-range-transact-sql.md) | Tracks the numeric ranges assigned to identity columns for subscriptions to publications for which replication is automatically managing range assignments. |
| [MSmerge_metadataaction_request](../../system-tables/msmerge-metadataaction-request-transact-sql.md) | Contains one row for each compensating action that is required. A compensating action is used to roll back a change at one node if the change failed at another node. |
| [MSmerge_partition_groups](../../system-tables/msmerge-partition-groups-transact-sql.md) | Contains one row for each precomputed partition in a given database. |
| [MSmerge_past_partition_mappings](../../system-tables/msmerge-past-partition-mappings-transact-sql.md) | Contains one row for each partition a given changed row used to belong to, but no longer belongs to. |
| [MSmerge_replinfo](../../system-tables/msmerge-replinfo-transact-sql.md) | Contains one row for each subscription. This table tracks internal information about sent and received generations. |
| [MSmerge_sessions](../../system-tables/msmerge-sessions-transact-sql.md) | Contains history rows with the outcomes of previous Merge Agent job sessions. |
| [MSmerge_settingshistory](../../system-tables/msmerge-settingshistory-transact-sql.md) | Contains a history of changes made to article and publication properties, with one row for each change made. |
| [MSmerge_tombstone](../../system-tables/msmerge-tombstone-transact-sql.md) | Contains information on deleted rows and allows deletes to be propagated to other Subscribers. |
| [MSrepl_errors](../../system-tables/msrepl-errors-transact-sql.md) | Contains detailed information about any agent failures. |
| [sysmergearticles](../../system-tables/sysmergearticles-transact-sql.md) | Contains one row for each merge article. |
| [sysmergepartitioninfo](../../system-tables/sysmergepartitioninfo-transact-sql.md) | Contains information about partitions for each article, with one row for each article. |
| [sysmergepartitioninfoview](../../system-views/sysmergepartitioninfoview-transact-sql.md) | Contains partitioning information for table articles. |
| [sysmergepublications](../../system-tables/sysmergepublications-transact-sql.md) | Contains one row for each merge publication. |
| [sysmergeschemaarticles](../../system-tables/sysmergeschemaarticles-transact-sql.md) | Tracks schema-only articles, such as stored procedures. |
| [sysmergeschemachange](../../system-tables/sysmergeschemachange-transact-sql.md) | Contains information about the published articles generated by the Snapshot Agent. |
| [sysmergesubscriptions](../../system-tables/sysmergesubscriptions-transact-sql.md) | Contains one row for each Subscriber. |
| [sysmergesubsetfilters](../../system-tables/sysmergesubsetfilters-transact-sql.md) | Contains join filter information for partitioned articles. |

In addition, the `MSsnapshotdeliveryprogress` table is created in each subscription database, and the `MSsubscription_properties` table is created in each subscription database that uses a pull subscription:

| Table | Description |
| --- | --- |
| [MSsnapshotdeliveryprogress](../../system-tables/mssnapshotdeliveryprogress-transact-sql.md) | Tracks files that were successfully delivered to the Subscriber when a snapshot is being applied. This data is used to resume the delivery of files in case the Merge Agent fails to deliver all of the files during the session. |
| [MSsubscription_properties](../../system-tables/mssubscription-properties-transact-sql.md) | Contains the parameter information required to run replication agents at the Subscriber |

## Related content

- [Merge replication](merge-replication.md)
- [sys.sp_addmergearticle (Transact-SQL)](../../system-stored-procedures/sp-addmergearticle-transact-sql.md)
- [sys.sp_addmergepublication (Transact-SQL)](../../system-stored-procedures/sp-addmergepublication-transact-sql.md)
- [sys.sp_addpublication_snapshot (Transact-SQL)](../../system-stored-procedures/sp-addpublication-snapshot-transact-sql.md)
- [sys.sp_replicationdboption (Transact-SQL)](../../system-stored-procedures/sp-replicationdboption-transact-sql.md)
