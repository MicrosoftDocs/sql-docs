---
title: How Merge Replication Detects and Resolves Conflicts
description: Learn how merge replication resolves when a change made at one node could conflict with a change made to the same data at another node.
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
  - "merge replication [SQL Server replication], detect and resolve conflicts"
---
# How merge replication detects and resolves conflicts

Merge replication allows multiple nodes to make autonomous data changes, so situations exist in which a change made at one node could conflict with a change made to the same data at another node. In other situations, the Merge Agent encounters an error such as a constraint violation and can't propagate a change made at a particular node to another node. This article describes types of conflicts, how conflicts are detected and resolved, and factors that affect detection and resolution.

## Detect and resolving conflicts

The Merge Agent detects conflicts by using the `lineage` column of the `MSmerge_contents` system table; if column-level tracking is enabled for an article, the `COLV1` column is also used. These columns contain metadata about when a row or column is inserted or updated, and about which nodes in a merge replication topology made changes to the row or column. You can use the [sp_showrowreplicainfo](../../system-stored-procedures/sp-showrowreplicainfo-transact-sql.md) system stored procedure to view this metadata.

As the Merge Agent enumerates changes to be applied during synchronization, it compares the metadata for each row at the Publisher and Subscriber. The Merge Agent uses this metadata to determine if a row or column has changed at more than one node in the topology, which indicates a potential conflict. After a conflict is detected, the Merge Agent launches the conflict resolver specified for the article with a conflict and uses the resolver to determine the conflict winner. The winning row is applied at the Publisher and Subscriber, and the data from the losing row is written to a conflict table.

Conflicts are resolved automatically and immediately by the Merge Agent unless you have chosen interactive conflict resolution for the article. For more information, see [Microsoft Replication Interactive Conflict Resolver](../microsoft-replication-interactive-conflict-resolver.md). If you manually change the winning row for a conflict using the merge replication Conflict Viewer, the Merge Agent applies the winning version of the row to the losing server during the next synchronization.

## Log resolved conflicts

After the Merge Agent has resolved the conflict according to the logic in the conflict resolver, it logs conflict data according to the type of conflict:

- For `UPDATE` and `INSERT` conflicts, it writes the losing version of the row to the conflict table for the article, which is named in the form `conflict_<PublicationName>_<ArticleName>`. The general conflict information, such as the type of conflict, is written to the table `MSmerge_conflicts_info`.

- For `DELETE` conflicts, it writes the losing version of the row to the `MSmerge_conflicts_info` table. When a delete loses against an update, there's no data for the losing row (because it was a delete), so nothing is written to `conflict_<PublicationName>_<ArticleName>`.

The conflict tables for each article are created in the publication database, subscription database, or both (the default), depending on the value specified for the `@conflict_logging` parameter of `sp_addmergepublication`. Each conflict table has the same structure as the article on which it's based, with the addition of the `origin_datasource_id` column. The Merge Agent deletes data from the conflict table if it's older than the conflict retention period for the publication, which is specified using the `@conflict_retention` parameter of `sp_addmergepublication` (the default is 14 days).

Replication provides the Replication Conflict Viewer and stored procedures (`sp_helpmergearticleconflicts`, `sp_helpmergeconflictrows`, and `sp_helpmergedeleteconflictrows`) to view conflict data. For more information, see [Conflict resolution for Merge Replication](../view-and-resolve-data-conflicts-for-merge-publications.md).

## Factors that affect conflict resolution

There are two factors that affect how the Merge Agent resolves a conflict it has detected:

- The type of subscription: client or server (whether the subscription is a pull subscription or a push subscription doesn't affect conflict resolution).

- The type of conflict tracking used: row-level, column-level, or logical record-level.

### Subscription types

When you create a subscription, in addition to specifying whether it's a push or pull subscription, you specify whether it's a client or server subscription; after a subscription is created, the type can't be changed (in previous versions of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], client and server subscriptions were referred to, respectively, as local and global subscriptions).

A subscription with an assigned priority value (from 0.00 to 99.99) is called a server subscription; a subscription using the priority value of the Publisher is called a client subscription. Additionally, Subscribers with server subscriptions can republish data to other Subscribers. The following table summarizes the main differences and uses of each Subscriber type.

| Type | Priority value | Used |
| --- | --- | --- |
| Server | Assigned by user | When you want different Subscribers to have different priorities. |
| Client | 0.00, but data changes assume the priority value of the Publisher after synchronization | When you want all Subscribers to have the same priority, and the first Subscriber to merge with the Publisher to win the conflict. |

If a row is changed in a client subscription, no priority is assigned to the change until the subscription is synchronized. During synchronization, the changes from the Subscriber are assigned the priority of the Publisher and retain that priority for subsequent synchronizations. In a sense, the Publisher assumes ownership of the change. This behavior permits the first Subscriber to synchronize with the Publisher to win subsequent conflicts with other Subscribers for a given row or column.

When you change a row in a server subscription, the subscription priority is stored in the metadata for the change. This priority value travels with the changed row as it merges with changes at other Subscribers. This assures that a change made by a higher priority subscription doesn't lose to a subsequent change made by a subscription with a lower priority.

A subscription can't have an explicit priority value that is higher than its Publisher. The top-level Publisher in a merge replication topology always has an explicit priority value of 100.00. All subscriptions to that publication must have a priority value less than this value. In a republishing topology:

- If the Subscriber is republishing data, the subscription must be a server subscription with a priority value less than the Publisher above the Subscriber.

- If the Subscriber isn't republishing data (because it's at the leaf-level of the republishing tree), the subscription must be a client subscription.

For more information about server subscriptions and priorities, see [Example of merge conflict resolution based on subscription type and assigned priorities](merge-replication-resolve-subscription-priorities.md).

#### Delayed conflict notification

Delayed conflict notification can occur with server subscriptions that have different conflict priorities. Consider the following scenario, in which non-conflicting changes are exchanged between the Publisher and a lower-priority Subscriber that result in conflicting changes when a higher-priority Subscriber synchronizes with the Publisher:

1. The Publisher and a low-priority Subscriber, named LowPrioritySub, exchange changes over several synchronizations without conflict.

1. A higher-priority Subscriber, named HighPrioritySub, hasn't synchronized with the Publisher in some time, and has made changes to the same rows that the LowPrioritySub Subscriber has made.

1. The HighPrioritySub Subscriber synchronizes with the Publisher and wins the conflicts between its changes and the LowPrioritySub Subscriber because it has a higher priority than the LowPrioritySub Subscriber. The Publisher now contains the changes made by the HighPrioritySub Subscriber.

1. The LowPrioritySub Subscriber then merges with the Publisher and downloads a large number of changes because of the conflicts with the HighPrioritySub Subscriber.

This situation can become problematic when the lower-priority Subscriber has made changes to the same rows that are now conflict losers. This can result in a loss of all of the changes made by this Subscriber. A potential solution to this problem is to make sure that all the Subscribers have the same priority, unless business logic dictates otherwise.

### Tracking level

Whether or not a data change qualifies as a conflict depends on the type of conflict tracking you set for an article: row-level, column-level, or logical record-level. For more information on logical record-level tracking, see [Advanced Merge Replication Conflict - Resolving in Logical Record](advanced-merge-replication-conflict-resolving-in-logical-record.md).

When conflicts are recognized at the row level, changes made to corresponding rows are judged as a conflict, whether or not the changes are made to the same column. For example, suppose one change is made to the address column of a Publisher row, and a second change is made to the phone number column of the corresponding Subscriber row (in the same table). With row-level tracking, a conflict is detected because changes were made to the same row. With column-level tracking, no conflict is detected, because changes were made to different columns in the same row.

For row-level and column-level tracking, resolution of the conflict is the same: the entire row of data is overwritten by data from the conflict winner (for logical record-level tracking, resolution depends on the article property `logical_record_level_conflict_resolution`).

The application semantics usually determine which tracking option to use. For example, if you're updating customer data that is generally entered at the same time, such as an address and phone number, row-level tracking should be chosen. If column-level tracking was chosen in this situation, changes to the customer address in one location and to the customer phone number in another location wouldn't be detected as a conflict: the data would be merged on synchronization and the error would be missed. In other situations, updating individual columns from different sites might be the most logical choice. For example, two sites might have access to different types of statistical information on a customer, such as income level, and total dollar amount of credit card purchases. Selecting column-level tracking ensures that both sites can enter the statistical data for different columns without generating unnecessary conflicts.

> [!NOTE]  
> If your application doesn't require column-level tracking, it's recommended that you use row-level tracking (the default) because it typically results in better synchronization performance. If row tracking is used, the base table can include a maximum of 1,024 columns, but columns must be filtered from the article so that a maximum of 246 columns is published. If column tracking is used, the base table can include a maximum of 246 columns.

## Conflict types

Although most conflicts are related to updates (an update at one node conflicts with an update or delete at another node), there are other conflict types. Each type of conflict discussed in this section can occur during the upload phase or the download phase of merge processing. Upload processing is the first reconciliation of changes performed in a particular merge session, and is the phase during which the Merge Agent replicates changes from the Subscriber up to the Publisher. Conflicts detected during this processing are referred to as upload conflicts. Download processing involves moving changes from the Publisher to the Subscriber, and occurs after upload processing. Conflicts during this phase of processing are referred to as download conflicts.

For more information about conflict types, see [MSmerge_conflicts_info](../../system-tables/msmerge-conflicts-info-transact-sql.md), especially the `conflict_type` and `reason_code` columns.

### Update-update conflicts

The Merge Agent detects update-update conflicts when an update to a row (or column, or logical record) at one node conflicts with another update to the same row at another node. The behavior of the default resolver in this case is to send the winning version of the row to the losing node and log the losing row version in the article conflict table.

### Update-delete conflicts

The Merge Agent detects update-delete conflicts when an update of data at one node conflicts with a delete at another. In this case, the Merge Agent updates a row; however, when the Merge Agent searches for that row at the destination, it can't find the row because it was deleted. If the winner is the node that updated the row, the delete at the losing node is discarded and the Merge Agent sends the newly updated row to the conflict loser. The Merge Agent logs information about the losing version of the row to the `MSmerge_conflicts_info` table.

### Failed change conflicts

The Merge Agent raises these conflicts when it can't apply a particular change. This typically occurs because of a difference in constraint definitions between the Publisher and Subscriber, and the use of the `NOT FOR REPLICATION` (NFR) property on the constraint. Examples include:

- A foreign key conflict at the Subscriber, which can occur when the Subscriber-side constraint isn't marked as NFR.

- Differences in constraints between the Publisher and Subscribers, and the constraints aren't marked as NFR.

- Unavailability of dependent objects at the Subscriber. For example, if you publish a view, but not the table on which that view depends, a failure occurs if you attempt to insert through that view at the Subscriber.

- Join filtering logic for a publication that doesn't match the primary key and foreign key constraints. Conflicts can occur when the SQL Server relational engine tries to honor a constraint but the Merge Agent is honoring the join filter definition between the articles. The Merge Agent can't apply the change at the destination node because of the table-level constraints, which results in a conflict.

- Conflicts because of unique index or unique constraint violations or primary key violations can occur if identity columns are defined for the article, and automated identity management isn't used. This can be a problem if two Subscribers were to use the same identity value for a newly inserted row. For more information about identity range management, see [Replicate Identity Columns](../publish/replicate-identity-columns.md).

- Conflicts due to trigger logic preventing the Merge Agent from inserting a row in the destination table. Consider an update trigger that is defined at the Subscriber; the trigger isn't marked as NFR and includes a `ROLLBACK` in its logic. If a failure occurs, the trigger issues a `ROLLBACK` of the transaction, which results in the Merge Agent detecting a failed change conflict.

## Related content

- [Merge replication](merge-replication.md)
- [MSmerge_conflicts_info (Transact-SQL)](../../system-tables/msmerge-conflicts-info-transact-sql.md)
- [MSmerge_contents (Transact-SQL)](../../system-tables/msmerge-contents-transact-sql.md)
- [sys.sp_addmergepublication (Transact-SQL)](../../system-stored-procedures/sp-addmergepublication-transact-sql.md)
- [sys.sp_helpmergearticleconflicts (Transact-SQL)](../../system-stored-procedures/sp-helpmergearticleconflicts-transact-sql.md)
- [sys.sp_helpmergeconflictrows (Transact-SQL)](../../system-stored-procedures/sp-helpmergeconflictrows-transact-sql.md)
- [sys.sp_helpmergedeleteconflictrows (Transact-SQL)](../../system-stored-procedures/sp-helpmergedeleteconflictrows-transact-sql.md)
- [Control Behavior of Triggers and Constraints in Synchronization](../control-behavior-of-triggers-and-constraints-in-synchronization.md)
- [Advanced Merge Replication - Conflict Detection and Resolution](advanced-merge-replication-conflict-detection-and-resolution.md)
- [Republish Data](../republish-data.md)
