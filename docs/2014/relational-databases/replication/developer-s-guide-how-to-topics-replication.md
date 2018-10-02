---
title: "Developer&#39;s Guide: How-to Topics (Replication) | Microsoft Docs"
ms.custom: ""
ms.date: "06/30/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "replication"
ms.topic: "reference"
ms.assetid: c6c15ae6-da52-4638-93d3-61c7242e8a0b
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Developer&#39;s Guide: How-to Topics (Replication)
  This topic provides links to information about how to programmatically perform replication-related tasks.  
  
## Securing a Replication Topology  
  
-   [View and Modify Replication Security Settings](security/view-and-modify-replication-security-settings.md)  
  
-   [Manage Logins in the Publication Access List](security/manage-logins-in-the-publication-access-list.md)  
  
## Configuring, Modifying, and Disabling Publishing and Distribution (Replication)  
  
-   [Configure Publishing and Distribution](configure-publishing-and-distribution.md)  
  
-   [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md)  
  
-   [Disable Publishing and Distribution](disable-publishing-and-distribution.md)  
  
## Creating, Modifying, and Deleting Publications and Articles  
  
-   [Create a Publication](publish/create-a-publication.md)  
  
-   [Define an Article](publish/define-an-article.md)  
  
-   [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md)  
  
-   [View and Modify Article Properties](publish/view-and-modify-article-properties.md)  
  
-   [Delete a Publication](publish/delete-a-publication.md)  
  
-   [Delete an Article](publish/delete-an-article.md)  
  
-   [Create a Publication from an Oracle Database](publish/create-a-publication-from-an-oracle-database.md)  
  
-   [Set the Expiration Period for Subscriptions](publish/set-the-expiration-period-for-subscriptions.md)  
  
-   [Specify Schema Options](publish/specify-schema-options.md)  
  
-   [Replicate Schema Changes](publish/replicate-schema-changes.md)  
  
-   [Manage Identity Columns](publish/manage-identity-columns.md)  
  
-   [Set the Compatibility Level for Merge Publications](publish/set-the-compatibility-level-for-merge-publications.md)  
  
### Snapshot Options  
  
-   [Configure Snapshot Properties &#40;Replication Transact-SQL Programming&#41;](publish/configure-snapshot-properties-replication-transact-sql-programming.md)  
  
-   [Deliver a Snapshot Through FTP](publish/deliver-a-snapshot-through-ftp.md)  
  
### Filtering Data  
  
-   [Define and Modify a Column Filter](publish/define-and-modify-a-column-filter.md)  
  
-   [Define and Modify a Static Row Filter](publish/define-and-modify-a-static-row-filter.md)  
  
-   [Define and Modify a Parameterized Row Filter for a Merge Article](publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md)  
  
-   [Optimize Parameterized Row Filters](merge/parameterized-filters-parameterized-row-filters.md)  
  
-   [Define and Modify a Join Filter Between Merge Articles](publish/define-and-modify-a-join-filter-between-merge-articles.md)  
  
### Transactional Replication Options  
  
-   [Set the Propagation Method for Data Changes to Transactional Articles](publish/set-the-propagation-method-for-data-changes-to-transactional-articles.md)  
  
-   [Enable Updating Subscriptions for Transactional Publications](publish/enable-updating-subscriptions-for-transactional-publications.md)  
  
### Merge Replication Options  
  
-   [Define a Logical Record Relationship Between Merge Table Articles](publish/define-a-logical-record-relationship-between-merge-table-articles.md)  
  
-   [Specify the Processing Order of Merge Table Articles &#40;Replication Transact-SQL Programming&#41;](publish/specify-the-processing-order-of-merge-table-articles.md)  
  
-   [Specify That a Merge Table Article is Download-Only](publish/specify-that-a-merge-table-article-is-download-only.md)  
  
-   [Specify That Deletes Should Not Be Tracked For Merge Articles &#40;Replication Transact-SQL Programming&#41;](publish/specify-that-deletes-should-not-be-tracked-for-merge-articles.md)  
  
-   [Specify the Conflict Tracking and Resolution Level for Merge Articles](publish/specify-the-conflict-tracking-and-resolution-level-for-merge-articles.md)  
  
-   [Specify a Merge Article Resolver](publish/specify-a-merge-article-resolver.md)  
  
-   [Specify Interactive Conflict Resolution for Merge Articles](publish/specify-interactive-conflict-resolution-for-merge-articles.md)  
  
## Creating, Modifying, and Deleting Subscriptions  
  
-   [Create a Pull Subscription](create-a-pull-subscription.md)  
  
-   [View and Modify Pull Subscription Properties](view-and-modify-pull-subscription-properties.md)  
  
-   [Delete a Pull Subscription](delete-a-pull-subscription.md)  
  
-   [Create a Push Subscription](create-a-push-subscription.md)  
  
-   [View and Modify Push Subscription Properties](view-and-modify-push-subscription-properties.md)  
  
-   [Delete a Push Subscription](delete-a-push-subscription.md)  
  
-   [Specify Synchronization Schedules](specify-synchronization-schedules.md)  
  
-   [Create an Updatable Subscription to a Transactional Publication](create-updatable-subscription-transactional-publication-transact-sql.md)  
  
-   [Create a Subscription for a Non-SQL Server Subscriber](create-a-subscription-for-a-non-sql-server-subscriber.md)  
  
## Synchronizing Subscriptions  
  
-   [Create and Apply the Initial Snapshot](create-and-apply-the-initial-snapshot.md)  
  
-   [Create a Snapshot for a Merge Publication with Parameterized Filters](create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md)  
  
-   [Initialize a Transactional Subscription from a Backup &#40;Replication Transact-SQL Programming&#41;](initialize-a-transactional-subscription-from-a-backup.md)  
  
-   [Initialize a Subscription Manually](initialize-a-subscription-manually.md)  
  
-   [Synchronize a Pull Subscription](synchronize-a-pull-subscription.md)  
  
-   [Synchronize a Push Subscription](synchronize-a-push-subscription.md)  
  
-   [Reinitialize a Subscription](reinitialize-a-subscription.md)  
  
-   [Execute Scripts During Synchronization &#40;Replication Transact-SQL Programming&#41;](execute-scripts-during-synchronization-replication-transact-sql-programming.md)  
  
-   [Implement a Business Logic Handler for a Merge Article](implement-a-business-logic-handler-for-a-merge-article.md)  
  
-   [Debug a Business Logic Handler &#40;Replication Programming&#41;](debug-a-business-logic-handler-replication-programming.md)  
  
-   [Control the Behavior of Triggers and Constraints During Synchronization &#40;Replication Transact-SQL Programming&#41;](control-behavior-of-triggers-and-constraints-in-synchronization.md)  
  
-   [Implement a Custom Conflict Resolver for a Merge Article](implement-a-custom-conflict-resolver-for-a-merge-article.md)  
  
## Administering a Replication Topology  
  
-   [Work with Replication Agent Profiles](agents/replication-agent-profiles.md)  
  
-   [Validate Data at the Subscriber](validate-data-at-the-subscriber.md)  
  
-   [Manage Partitions for a Merge Publication with Parameterized Filters](publish/manage-partitions-for-a-merge-publication-with-parameterized-filters.md)  
  
-   [Bulk-Load Data into Tables in a Merge Publication &#40;Replication Transact-SQL Programming&#41;](bulk-load-data-into-tables-in-a-merge-publication.md)  
  
-   [Clean Up Merge Metadata &#40;Replication Transact-SQL Programming&#41;](administration/clean-up-merge-metadata-replication-transact-sql-programming.md)  
  
-   [Perform a Dummy Update for a Merge Article &#40;Replication Transact-SQL Programming&#41;](administration/perform-a-dummy-update-for-a-merge-article-replication-transact-sql-programming.md)  
  
-   [View Replicated Commands and Other Information in the Distribution Database &#40;Replication Transact-SQL Programming&#41;](monitor/view-replicated-commands-and-information-in-distribution-database.md)  
  
-   [Enable Coordinated Backups for Transactional Replication &#40;Replication Transact-SQL Programming&#41;](administration/enable-coordinated-backups-for-transactional-replication.md)  
  
-   [Administer a Peer-to-Peer Topology &#40;Replication Transact-SQL Programming&#41;](administration/administer-a-peer-to-peer-topology-replication-transact-sql-programming.md)  
  
-   [Quiesce a Replication Topology &#40;Replication Transact-SQL Programming&#41;](administration/quiesce-a-replication-topology-replication-transact-sql-programming.md)  
  
-   [Configure the Transaction Set Job for an Oracle Publisher &#40;Replication Transact-SQL Programming&#41;](administration/configure-the-transaction-set-job-for-an-oracle-publisher.md)  
  
-   [Upgrade Replication Scripts &#40;Replication Transact-SQL Programming&#41;](administration/upgrade-replication-scripts-replication-transact-sql-programming.md)  
  
## Monitoring a Replication Topology  
  
-   [Allow Non-Administrators to Use Replication Monitor](monitor/allow-non-administrators-to-use-replication-monitor.md)  
  
-   [Programmatically Monitor Replication](monitor/monitoring-replication-overview.md)  
  
-   [View Replicated Commands and Other Information in the Distribution Database &#40;Replication Transact-SQL Programming&#41;](monitor/view-replicated-commands-and-information-in-distribution-database.md)  
  
-   [View Conflict Information for Merge Publications &#40;Replication Transact-SQL Programming&#41;](view-conflict-information-for-merge-publications.md)  
  
-   [Measure Latency and Validate Connections for Transactional Replication](monitor/measure-latency-and-validate-connections-for-transactional-replication.md)  
  
  
