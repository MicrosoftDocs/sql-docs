---
title: How Merge Replication Evaluates Partitions in Filtered Publications
description: When one or more tables in a merge publication is filtered using parameterization, data in the published tables is partitioned.
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
  - "merge replication [SQL Server replication], evaluate partitions"
---
# How merge replication evaluates partitions in filtered publications

When one or more tables in a merge publication is filtered using parameterized filters and join filters, data in the published tables is partitioned. A partition is simply a subset of the rows in a table. When a Subscriber synchronizes with a Publisher, the Publisher must determine which rows belong to a Subscriber's partition based on the values the Subscriber supplies for the system functions `SUSER_SNAME()` and/or `HOST_NAME()`. This process of determining partition membership of changes at the Publisher for each Subscriber receiving a filtered dataset is referred to as partition evaluation.

Depending on the data in published tables and the settings chosen when creating a parameterized filter and any related join filters, each row in a published table can:

- Belong to one partition and be replicated to only one Subscriber (a value of `3` for the `sp_addmergearticle` parameter `@partition_options`). For example, in the [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] database, you could filter the `Employee` table with the following filter clause: `WHERE Employee.LoginID = SUSER_SNAME()`. The row matching each Login ID is sent to only one Subscriber.

- Belong to one partition and be replicated to more than one Subscriber (a value of `2` for `@partition_options`). For example, in the [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] database, you could filter the `Products` table with the following filter clause: `WHERE Products.ProductLine = HOST_NAME()`. The `HOST_NAME()` value is overridden such that the group of salespeople responsible for mountain bike sales receive all rows with a value of "M" in the `ProductLine` column; each row with a value of "M" belongs to only one partition, but it's sent to more than one Subscriber. For more information on using `HOST_NAME()`, see [Filtering with HOST_NAME()](parameterized-filters-parameterized-row-filters.md#filtering-with-host_name).

- Belong to more than one partition and be replicated to more than one Subscriber (a value of `0` or `1` for `@partition_options`). For example, in the [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] database, you could filter the `Products` table with the following filter clause: `WHERE Products.ProductLine = HOST_NAME() OR Products.ListPrice < 100`. In this case, sales people responsible for mountain bike sales can also sell products in other categories as long as the sale price is less than $100 dollars. Because of the OR in the filter clause, the row can belong to more than one partition.

## How partitions are evaluated

Partition evaluation occurs in one of two ways in merge replication, depending on whether the precomputed partitions feature is used. If several requirements are met, new merge publications are, by default, created with precomputed partitions enabled, and existing publications are automatically upgraded to use the feature. For a complete list of requirements, see [Parameterized Filters - Optimize for Precomputed Partitions](parameterized-filters-optimize-for-precomputed-partitions.md).

### Partition evaluation using precomputed partitions

With precomputed partitions, partition membership for all changes at the Publisher is precomputed and persisted at the time that changes are made to published tables. As a result, when a Subscriber synchronizes with the Publisher, it can immediately start to download changes relevant to its partition without having to go through the partition evaluation process. This can lead to significant performance gains when a publication has a large number of changes, Subscribers, or articles in the publication.

The system tables involved in precomputed partition evaluation are:

- `MSmerge_partition_groups`
- `MSmerge_current_partition_mappings`
- `MSmerge_past_partition_mappings`

`MSmerge_partition_groups` contains one row for each partition that is defined in a publication. Partitions can be:

- Defined explicitly using `sp_addmergepartition` or the `Data Partitions` page of the Publication Properties dialog box.

- Created automatically when a Subscriber synchronizes if the Subscriber requires a partition that doesn't yet have an entry in `MSmerge_partition_groups`.

The other two tables (`MSmerge_current_partition_mappings` and `MSmerge_past_partition_mappings`) are populated as changes are made to published tables. Every time a change is made on a published table in the publication database, a merge trigger fires and records metadata:

- `MSmerge_current_partition_mappings` contains one row for each unique combination of rows in `MSmerge_contents` and `MSmerge_partition_groups`. For example, if a row in a user table belongs to two partitions, and the row is updated, one row is inserted into `MSmerge_contents` to reflect the update, and two rows are inserted into `MSmerge_current_partition_mappings`, to indicate that the updated row belongs to the two partitions.

- `MSmerge_past_partition_mappings` contains one row for each row that no longer belongs in a given partition. A row moves out of a partition if:

  - The row is deleted. If a row is deleted from a user table, a row is inserted into `MSmerge_tombstone` and one or more rows are inserted into `MSmerge_past_partition_mappings`.

  - The value in a column used for filtering has changed. For example, if a parameterized filter is based on the state in which a company is headquartered and the company moves, the row for the company (and related rows in other tables) might move out of one sales person's partition of data into the partition for another sales person. If a row is updated such that it no longer belongs in a partition, a row is inserted or updated in `MSmerge_contents` and one or more rows are inserted into `MSmerge_past_partition_mappings`.

    > [!NOTE]  
    > If nonoverlapping partitions with one subscription per partition (a value of `3` for the `sp_addmergearticle` parameter `@partition_options`) is used, the system tables `MSmerge_current_partition_mappings` and `MSmerge_past_partition_mappings` aren't used to track the rows' partition mappings, because each row belongs to only one partition and can be changed at only one Subscriber.

### Partition evaluation using the SetupBelongs process

Without precomputed partitions, a process known as SetupBelongs is used. During synchronization, partition evaluation is performed for each change made to a filtered table at the Publisher since the last time the Merge Agent ran for a specific Subscriber. This process is repeated for every Subscriber that synchronizes with the Publisher.

To perform partition evaluation for a Subscriber, the Merge Agent calls the system stored procedure `sp_MSsetupbelongs`, which:

1. Creates two temporary tables for each filtered article: `#belongs_<RandomNumber>` and `#notbelongs_<RandomNumber>`.

1. Uses the value returned by the `SUSER_SNAME()` and/or `HOST_NAME()` functions at the Subscriber to query a system view; the query is used to determine if a row in `MSmerge_contents` or `MSmerge_tombstone` is relevant for the Subscriber's partition.

1. If the row isn't relevant to the Subscriber at all (such as an insert for another partition), metadata for this row isn't stored in `#belongs` or `#notbelongs`. If the row is relevant, there are two possible outcomes:

   - A row is added to `#belongs` if: the row in `MSmerge_contents` is an insert that belongs to the partition, or the row in `MSmerge_contents` is an update that doesn't change values in any columns used for filtering.

   - A row is added to `#notbelongs` if: the row in `MSmerge_contents` is an update that changes a value in any column used for filtering (in other words, it moves the row to a new partition), or the row in `MSmerge_tombstone` represents the deletion of a row in the partition.

> [!NOTE]  
> Even if precomputed partitions are enabled, the SetupBelongs process is used the first time a subscription synchronizes if the subscription is created after other subscriptions have started receiving changes.

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
