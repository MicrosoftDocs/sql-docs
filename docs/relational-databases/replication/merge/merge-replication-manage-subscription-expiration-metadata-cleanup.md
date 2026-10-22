---
title: How Merge Replication Manages Subscription Expiration and Metadata Cleanup
description: A subscription to a merge publication expires if it hasn't synchronized with the Publisher within the publication retention period.
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 07/18/2025
ms.service: sql
ms.subservice: replication
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "merge replication [SQL Server replication], manage subscription expiration and metadata cleanup"
---
# How merge replication manages subscription expiration and metadata cleanup

A subscription to a merge publication expires if it hasn't synchronized with the Publisher within the publication retention period. The default retention period is 14 days. It's set using the `@retention` and `@retention_period_unit` parameters of [sp_addmergepublication](../../system-stored-procedures/sp-addmergepublication-transact-sql.md). The `@retention_period_unit` requires the publication compatibility level to be 90RTM or higher. For Subscribers running previous versions of Microsoft SQL Server, `@retention_period_unit` is always set to `day`. For more information about compatibility level, see the "Compatibility Level for Merge Publications" section in the article [Using Multiple Versions of SQL Server in a Replication Topology](/previous-versions/sql/sql-server-2008-r2/ms143241(v=sql.105)).

When a subscription expires, it must be reinitialized, because metadata for the subscription is removed. For more information, see [Metadata cleanup](#metadata-cleanup). Subscriptions that aren't reinitialized are dropped by the Expired Subscription Cleanup job that runs on the Publisher. By default, this job runs daily; it removes all push subscriptions that haven't synchronized for double the length of the publication retention period. For example:

- If a publication has a retention period of 14 days, a subscription can expire if it hasn't synchronized within 14 days.

  A subscription only expires if there were changes to the data in that subscription's partition. For example, suppose a Subscriber receives customer data only for customers in Germany. If the retention period is set to 14 days, the subscription expires on day 14 only if there were changes to the customer data from that region in the last 14 days.

- From 14 days to 27 days after the last synchronization, the subscription can be reinitialized.

- At 28 days after the last synchronization, the subscription is dropped by the Expired Subscription Cleanup job. If a push subscription expires, it is completely removed, but pull subscriptions aren't. You must clean up pull subscriptions at the Subscriber. For more information, see [Delete a Pull Subscription](../delete-a-pull-subscription.md).

## Considerations for setting the publication retention period

Keep the following considerations in mind when setting the retention period for merge publications:

- Cleanup of merge replication metadata is dependent on the publication retention period:

  - Replication can't clean up metadata in the publication and subscription databases until the retention period is reached. Use caution in specifying a high value for the retention period, because it can negatively affect replication performance. You should use a lower setting if you can reliably predict that all Subscribers will synchronize regularly within that time period.

  - It's possible to specify that subscriptions never expire (a value of 0 for `@retention`), but you shouldn't use this value, because metadata can't be cleaned up.

- The retention period for any republisher must be set to a value equal to or less than the retention period set at the original Publisher. If you use alternate synchronization partners, you should use the same publication retention values for the Publishers and all alternate synchronization partners. Using different values might lead to nonconvergence. If you need to change the publication retention value, reinitialize the Subscriber to avoid the nonconvergence of data.

- If, after a cleanup, the publication retention period is increased and a subscription tries to merge with the Publisher (which already deleted the metadata), the subscription doesn't expire because of the increased retention value. However, the Publisher doesn't have enough metadata to download changes to the Subscriber, which leads to nonconvergence.

## Metadata cleanup

Metadata cleanup in merge replication is performed by the stored procedure [sp_mergemetadataretentioncleanup](../../system-stored-procedures/sp-mergemetadataretentioncleanup-transact-sql.md). The timing of cleanup is based on the publication retention period. Every time the Merge Agent runs for a subscription, it calls the cleanup procedure. The procedure removes metadata older than the publication retention period from the following system tables:

- [MSmerge_contents](../../system-tables/msmerge-contents-transact-sql.md)
- [MSmerge_tombstone](../../system-tables/msmerge-tombstone-transact-sql.md)
- [MSmerge_genhistory](../../system-tables/msmerge-genhistory-transact-sql.md)
- [MSmerge_current_partition_mappings](../../system-tables/msmerge-current-partition-mappings.md)
- [MSmerge_past_partition_mappings](../../system-tables/msmerge-past-partition-mappings-transact-sql.md)
- [MSmerge_generation_partition_mappings](../../system-tables/msmerge-generation-partition-mappings-transact-sql.md)

These tables are used by all publications in a publication database: if there's more than one publication, the longest retention period is always used to determine when metadata can be removed.

## Related content

- [Merge replication](merge-replication.md)
- [Reinitialize a Subscription](../reinitialize-a-subscription.md)
