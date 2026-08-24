---
title: How Merge Replication Tracks and Enumerates Changes
description: After a publication or subscription is initialized, merge replication tracks and enumerates all changes to the data in published tables.
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
  - "merge replication [SQL Server replication], track and enumerate changes"
---
# How merge replication tracks and enumerates changes

After a publication or subscription is initialized, merge replication tracks and enumerates all changes to the data in published tables. Changes are tracked through triggers (which replication creates for each published table) and system tables in the publication and subscription databases. These replication system tables are populated with metadata that indicates which changes should be propagated. When the Merge Agent runs during synchronization, changes are enumerated by the agent and then applied to the Publisher and Subscriber as necessary.

## Change tracking

Merge replication uses the following triggers and system tables to track changes for all published tables:

- `MSmerge_ins_<GUID>`: insert trigger (the GUID value for this trigger and the other triggers is derived from `sysmergearticles`)
- `MSmerge_upd_<GUID>`: update trigger
- `MSmerge_del_<GUID>`: delete trigger
- `MSmerge_contents`
- `MSmerge_tombstone`
- `MSmerge_genhistory`

Merge replication uses the following extra system tables to track changes for filtered tables:

- `MSmerge_partition_groups`
- `MSmerge_current_partition_mappings`
- `MSmerge_past_partition_mappings`

> [!NOTE]  
> The system tables listed are used by all merge publications and subscriptions in a database; for example, if you have more than one publication in a publication database, `MSmerge_contents` contains rows from articles in all publications.

## Change tracking for unfiltered tables

### System tables

The system tables used for unfiltered and filtered tables contain the following metadata:

- `MSmerge_contents` contains one row for each row inserted or updated in a published table in the database.

- `MSmerge_tombstone` contains one row for each row deleted from a published table in the database.

- `MSmerge_genhistory` contains one row for each generation. A generation is a collection of changes that is delivered to a Publisher or Subscriber. Generations are *closed* each time the Merge Agent runs; subsequent changes in a database are added to one or more *open generations*.

### Change tracking process

The following change tracking process is used for all unfiltered tables:

- When an insert or update occurs on a published table, the `MSmerge_ins_<GUID>` or `MSmerge_upd_<GUID>` trigger fires, and a row is inserted into the `MSmerge_contents` system table. The `rowguid` column of `MSmerge_contents` contains the GUID for the inserted or updated row, indicating that the next time synchronization occurs, the corresponding inserted or updated row in the user table should be sent to the Publisher or Subscribers. If subsequent updates occur on a row in a user table, the row in `MSmerge_contents` is updated to reflect this state.

- When a delete occurs on a published table, the `MSmerge_del_<GUID>` trigger fires, and a row is inserted into the `MSmerge_tombstone` system table. The `rowguid` column of `MSmerge_tombstone` contains the GUID for the deleted row, indicating that the next time synchronization occurs, a delete should be sent to the Publisher or Subscribers for the corresponding deleted row in the user table. If the deleted row is referenced in `MSmerge_contents` (because it was inserted or updated since the last synchronization), the row is deleted from `MSmerge_contents`.

## Change tracking for filtered tables

### System tables

In addition to the system tables described in the previous section, three tables in the publication database contain metadata for tracking changes to filtered tables:

- `MSmerge_partition_groups` contains one row for each partition that is defined in a publication. Partitions can be:

  - Defined explicitly using `sp_addmergepartition` or the **Data Partitions** page of the Publication Properties dialog box.

  - Created automatically when a Subscriber synchronizes if the Subscriber requires a partition that doesn't yet have an entry in `MSmerge_partition_groups`.

- `MSmerge_current_partition_mappings` contains one row for each unique combination of rows in `MSmerge_contents` and `MSmerge_partition_groups`. For example, if a row in a user table belongs to two partitions, and the row is updated, one row is inserted into `MSmerge_contents` to reflect the update, and two rows are inserted into `MSmerge_current_partition_mappings`, to indicate that the updated row belongs to the two partitions.

- `MSmerge_past_partition_mappings` contains one row for each row that no longer belongs in a given partition. A row moves out of a partition if:

  - The row is deleted. If a row is deleted from a user table, a row is inserted into `MSmerge_tombstone` and one or more rows are inserted into `MSmerge_past_partition_mappings`.

  - The value in a column used for filtering has changed. For example, if a parameterized filter is based on the state in which a company is headquartered and the company moves, the row for the company (and related rows in other tables) might move out of one sales person's partition of data into the partition for another sales person. If a row is updated such that it no longer belongs in a partition, a row is inserted or updated in `MSmerge_contents` and one or more rows are inserted into `MSmerge_past_partition_mappings`.

> [!NOTE]  
> If nonoverlapping partitions with one subscription per partition (a value of `3` for the `@partition_options` parameter of `sp_addmergearticle`) are used, the system tables `MSmerge_current_partition_mappings` and `MSmerge_past_partition_mappings` aren't used to track the rows' partition mappings, because each row belongs to only one partition and can be changed at only one Subscriber.

### Change tracking process

The process described previously (in the section [Change tracking for unfiltered tables](#change-tracking-for-unfiltered-tables)) for unfiltered tables is also used for filtered tables, with the following additions:

- When an insert occurs on a published table, in addition to data being updated or inserted into `MSmerge_contents`, a partition mapping is added to `MSmerge_current_partition_mappings` for each partition that the row belongs to.

- When an update occurs on a published table, in addition to data being updated or inserted into `MSmerge_contents`, if a partition mapping doesn't exist in `MSmerge_current_partition_mappings` for each partition that the row belongs to, one is added. If the update resulted in a row being moved from one partition to another, a row is updated in `MSmerge_current_partition_mappings` and one is added to `MSmerge_past_partition_mappings`.

- When a delete occurs on a published table, in addition to a row being inserted into `MSmerge_tombstone`, a row is deleted from `MSmerge_current_partition_mappings` and one is added to `MSmerge_past_partition_mappings`.

## Change enumeration

### System tables and procedures

When the Merge Agent runs, changes are enumerated using several system tables and stored procedures:

- `MSmerge_genhistory` contains one row for each generation. A generation is a collection of changes that is delivered to a Publisher or Subscriber. Generations are *closed* each time the Merge Agent runs; subsequent changes in a database are added to one or more *open generations*.

- `sysmergesubscriptions` contains information on subscriptions, including a record of the last generations of changes a node has sent and received. In the publication database, this table contains a row for the Publisher and one row for each Subscriber. In a subscription database, this table typically contains a row for the Subscriber and a row for the Publisher.

- `MSmerge_generation_partition_mappings` is used only for filtered tables, recording whether a given generation contains any changes relevant to a given partition. This table in the publication database contains one row for each unique combination of rows in `MSmerge_genhistory` and `MSmerge_partition_groups`.

- `sp_MSmakegeneration` closes all open generations at the beginning of the enumeration process.

- `sp_MSenumchanges` enumerates changes for tables (several related procedures that have names beginning with `sp_MSenumchanges` are also used in this process).

- `sp_MSgetmetadata` determines whether a change from one node should be applied at another node as an insert, update, or delete.

### Change enumeration process

The following process occurs during change enumeration:

1. The system procedure `sp_MSmakegeneration` is called:

   - For unfiltered and filtered tables, this procedure closes all open generations referenced in `MSmerge_genhistory` (closed generations have a value of `1` or `2` in the column `genstatus`).

   - For filtered tables, this procedure populates the system table `MSmerge_generation_partition_mappings`. If a generation contains one or more changes that are relevant for a partition, a row is inserted into the system table. If a generation doesn't contain any changes relevant for a given partition, a row isn't inserted into `MSmerge_generation_partition_mappings`, and changes aren't enumerated for any Subscribers that receive that partition.

1. The stored procedure `sp_MSenumchanges` and related procedures are called. These procedures enumerate the changes that occurred since the last time synchronization occurred:

   1. The procedures first determine the generation at which enumeration starts, based on the columns `sentgen` (last generation sent) and `recgen` (last generation received) in the table `sysmergesubscriptions`.

      For example, when determining which generations' changes must be enumerated for a given Subscriber, the `sentgen` for the Subscriber (stored in the publication database), and the `recgen` for the Subscriber (stored in the subscription database) are compared. If the values are the same (which indicates that the last generation sent from the Publisher was successfully received by the Subscriber), changes are enumerated starting with the next generation in `MSmerge_genhistory`. If the values aren't the same, the lower of the two values is used to ensure all required changes are sent.

   1. The procedures then enumerate changes:

      For unfiltered tables, all changes contained in generations after the generation in `sentgen` or `recgen` are enumerated: `MSmerge_genhistory` is joined to `MSmerge_contents` and `MSmerge_tombstone` to determine which changes must be sent.

      For filtered tables, `MSmerge_generation_partition_mappings` is joined to: `MSmerge_current_partition_mappings` and `MSmerge_contents`; and `MSmerge_past_partition_mappings` and `MSmerge_tombstone` to determine which changes are relevant for the partition that the Subscriber receives.

1. The stored procedure `sp_MSgetmetadata` is called to determine whether a change should be applied as an insert, update, or delete. At this point, conflict detection and resolution are performed; for more information, see [How merge replication detects and resolves conflicts](merge-replication-detect-resolve-conflicts.md).

## Related content

- [Merge replication](merge-replication.md)
- [MSmerge_contents (Transact-SQL)](../../system-tables/msmerge-contents-transact-sql.md)
- [MSmerge_current_partition_mappings](../../system-tables/msmerge-current-partition-mappings.md)
- [MSmerge_generation_partition_mappings (Transact-SQL)](../../system-tables/msmerge-generation-partition-mappings-transact-sql.md)
- [MSmerge_genhistory (Transact-SQL)](../../system-tables/msmerge-genhistory-transact-sql.md)
- [MSmerge_partition_groups (Transact-SQL)](../../system-tables/msmerge-partition-groups-transact-sql.md)
- [MSmerge_past_partition_mappings (Transact-SQL)](../../system-tables/msmerge-past-partition-mappings-transact-sql.md)
- [MSmerge_tombstone (Transact-SQL)](../../system-tables/msmerge-tombstone-transact-sql.md)
- [sys.sp_addmergearticle (Transact-SQL)](../../system-stored-procedures/sp-addmergearticle-transact-sql.md)
- [sys.sp_addmergepartition (Transact-SQL)](../../system-stored-procedures/sp-addmergepartition-transact-sql.md)
- [sysmergearticles (Transact-SQL)](../../system-tables/sysmergearticles-transact-sql.md)
- [sysmergesubscriptions (Transact-SQL)](../../system-tables/sysmergesubscriptions-transact-sql.md)
- [Join Filters](join-filters.md)
- [Parameterized Filters - Parameterized Row Filters](parameterized-filters-parameterized-row-filters.md)
