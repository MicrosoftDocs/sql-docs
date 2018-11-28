---
title: "SQL Server Replication | Microsoft Docs"
ms.custom: ""
ms.date: "11/20/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], about"
  - "replication [SQL Server]"
ms.assetid: 3a5f4592-3c61-4b4d-9ceb-39716aeeba41
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# SQL Server Replication
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Replication is a set of technologies for copying and distributing data and database objects from one database to another and then synchronizing between databases to maintain consistency. Use replication to distribute data to different locations and to remote or mobile users over local and wide area networks, dial-up connections, wireless connections, and the Internet.  
  
 Transactional replication is typically used in server-to-server scenarios that require high throughput, including: improving scalability and availability; data warehousing and reporting; integrating data from multiple sites; integrating heterogeneous data; and offloading batch processing. Merge replication is primarily designed for mobile applications or distributed server applications that have possible data conflicts. Common scenarios include: exchanging data with mobile users; consumer point of sale (POS) applications; and integration of data from multiple sites. Snapshot replication is used to provide the initial data set for transactional and merge replication; it can also be used when complete refreshes of data are appropriate. With these three types of replication, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a powerful and flexible system for synchronizing data across your enterprise. Replication to SQLCE 3.5 and SQLCE 4.0 is supported on both [!INCLUDE[win8srv](../../includes/win8srv-md.md)] and [!INCLUDE[win8](../../includes/win8-md.md)].  

 As an alternative to replication, you can synchronize databases by using Microsoft Sync Framework. Sync Framework includes components and an intuitive and flexible API that make it easy to synchronize among SQL Server, SQL Server Express, SQL Server Compact, and SQL Azure databases. Sync Framework also includes classes that can be adapted to synchronize between a SQL Server database and any other database that is compatible with ADO.NET. For detailed documentation of the Sync Framework database synchronization components, see [Synchronizing Databases](https://go.microsoft.com/fwlink/?LinkId=209079). For an overview of Sync Framework, see [Microsoft Sync Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=209078). For a comparison between Sync Framework and Merge Replication, see [Synchronizing Databases Overview](https://msdn.microsoft.com/library/bb902818\(SQL.110\).aspx)  
  
 **Browse by area**  
 - [What’s New](../../relational-databases/replication/what-s-new-replication.md)  
 - [Backward Compatibility](../../relational-databases/replication/replication-backward-compatibility.md)  

 ## Securing a Replication Topology  
  
-   [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)  
-   [Manage Logins in the Publication Access List](../../relational-databases/replication/security/manage-logins-in-the-publication-access-list.md)  
  
## Configuring, Modifying, and Disabling Publishing and Distribution (Replication)  
  
-   [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
-   [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
-   [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md)  
  
## Creating, Modifying, and Deleting Publications and Articles  
  
-   [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)    
-   [Define an Article](../../relational-databases/replication/publish/define-an-article.md)   
-   [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
-   [View and Modify Article Properties](../../relational-databases/replication/publish/view-and-modify-article-properties.md)    
-   [Delete a Publication](../../relational-databases/replication/publish/delete-a-publication.md)   
-   [Delete an Article](../../relational-databases/replication/publish/delete-an-article.md)    
-   [Create a Publication from an Oracle Database](../../relational-databases/replication/publish/create-a-publication-from-an-oracle-database.md)   
-   [Set the Expiration Period for Subscriptions](../../relational-databases/replication/publish/set-the-expiration-period-for-subscriptions.md)  
-   [Specify Schema Options](../../relational-databases/replication/publish/specify-schema-options.md)  
-   [Replicate Schema Changes](../../relational-databases/replication/publish/replicate-schema-changes.md)    
-   [Manage Identity Columns](../../relational-databases/replication/publish/manage-identity-columns.md)   
-   [Set the Compatibility Level for Merge Publications](../../relational-databases/replication/publish/set-the-compatibility-level-for-merge-publications.md)  
  
### Snapshot Options  
  
-   [Configure Snapshot Properties &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/publish/configure-snapshot-properties-replication-transact-sql-programming.md)    
-   [Deliver a Snapshot Through FTP](../../relational-databases/replication/publish/deliver-a-snapshot-through-ftp.md) 
  
### Filtering Data  
  
-   [Define and Modify a Column Filter](../../relational-databases/replication/publish/define-and-modify-a-column-filter.md)    
-   [Define and Modify a Static Row Filter](../../relational-databases/replication/publish/define-and-modify-a-static-row-filter.md)    
-   [Define and Modify a Parameterized Row Filter for a Merge Article](../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md)    
-   [Optimize Parameterized Row Filters](../../relational-databases/replication/publish/optimize-parameterized-row-filters.md)    
-   [Define and Modify a Join Filter Between Merge Articles](../../relational-databases/replication/publish/define-and-modify-a-join-filter-between-merge-articles.md)  
  
### Transactional Replication Options  
  
-   [Set the Propagation Method for Data Changes to Transactional Articles](../../relational-databases/replication/publish/set-the-propagation-method-for-data-changes-to-transactional-articles.md)    
-   [Enable Updating Subscriptions for Transactional Publications](../../relational-databases/replication/publish/enable-updating-subscriptions-for-transactional-publications.md)  
  
### Merge Replication Options  
  
-   [Define a Logical Record Relationship Between Merge Table Articles](../../relational-databases/replication/publish/define-a-logical-record-relationship-between-merge-table-articles.md)    
-   [Specify Merge replication properties](../../relational-databases/replication/publish/specify-merge-replication-properties.md)    
-   [Specify a Merge Article Resolver](../../relational-databases/replication/publish/specify-a-merge-article-resolver.md)    

  
## Creating, Modifying, and Deleting Subscriptions  
  
-   [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md)    
-   [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md)    
-   [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md)    
-   [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md)   
-   [View and Modify Push Subscription Properties](../../relational-databases/replication/view-and-modify-push-subscription-properties.md)   
-   [Delete a Push Subscription](../../relational-databases/replication/delete-a-push-subscription.md)   
-   [Specify Synchronization Schedules](../../relational-databases/replication/specify-synchronization-schedules.md)    
-   [Create an Updatable Subscription to a Transactional Publication](../../relational-databases/replication/publish/create-an-updatable-subscription-to-a-transactional-publication.md)  
-   [Create a Subscription for a Non-SQL Server Subscriber](../../relational-databases/replication/create-a-subscription-for-a-non-sql-server-subscriber.md)  
  
## Synchronizing Subscriptions  
  
-   [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)   
-   [Create a Snapshot for a Merge Publication with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md)    
-   [Initialize a Transactional Subscription from a Backup &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/initialize-a-transactional-subscription-from-a-backup.md)    
-   [Initialize a Subscription Manually](../../relational-databases/replication/initialize-a-subscription-manually.md)    
-   [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md)    
-   [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md)   
-   [Reinitialize a Subscription](../../relational-databases/replication/reinitialize-a-subscription.md)    
-   [Execute Scripts During Synchronization &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/execute-scripts-during-synchronization-replication-transact-sql-programming.md)    
-   [Implement a Business Logic Handler for a Merge Article](../../relational-databases/replication/implement-a-business-logic-handler-for-a-merge-article.md)  
-   [Debug a Business Logic Handler &#40;Replication Programming&#41;](../../relational-databases/replication/debug-a-business-logic-handler-replication-programming.md)    
-   [Control the Behavior of Triggers and Constraints During Synchronization &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/control-behavior-of-triggers-and-constraints-in-synchronization.md)    
-   [Implement a Custom Conflict Resolver for a Merge Article](../../relational-databases/replication/implement-a-custom-conflict-resolver-for-a-merge-article.md)  
  
## Administering a Replication Topology  
  
-   [Work with Replication Agent Profiles](../../relational-databases/replication/agents/work-with-replication-agent-profiles.md)   
-   [Validate Data at the Subscriber](../../relational-databases/replication/validate-data-at-the-subscriber.md)    
-   [Manage Partitions for a Merge Publication with Parameterized Filters](../../relational-databases/replication/publish/manage-partitions-for-a-merge-publication-with-parameterized-filters.md)    
-   [Bulk-Load Data into Tables in a Merge Publication &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/bulk-load-data-into-tables-in-a-merge-publication.md)    
-   [Clean Up Merge Metadata &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/clean-up-merge-metadata-replication-transact-sql-programming.md)    
-   [Perform a Dummy Update for a Merge Article &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/perform-a-dummy-update-for-a-merge-article-replication-transact-sql-programming.md)    
-   [View Replicated Commands and Other Information in the Distribution Database &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/monitor/view-replicated-commands-and-information-in-distribution-database.md)    
-   [Enable Coordinated Backups for Transactional Replication &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/enable-coordinated-backups-for-transactional-replication.md)   
-   [Administer a Peer-to-Peer Topology &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/administer-a-peer-to-peer-topology-replication-transact-sql-programming.md)    
-   [Quiesce a Replication Topology &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/quiesce-a-replication-topology-replication-transact-sql-programming.md)    
-   [Configure the Transaction Set Job for an Oracle Publisher &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/configure-the-transaction-set-job-for-an-oracle-publisher.md)   
-   [Upgrade Replication Scripts &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/upgrade-replication-scripts-replication-transact-sql-programming.md)  
  
## Monitoring a Replication Topology  
  
-   [Allow Non-Administrators to Use Replication Monitor](../../relational-databases/replication/monitor/allow-non-administrators-to-use-replication-monitor.md)    
-   [Programmatically Monitor Replication](../../relational-databases/replication/monitor/programmatically-monitor-replication.md)    
-   [View Replicated Commands and Other Information in the Distribution Database &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/monitor/view-replicated-commands-and-information-in-distribution-database.md)    
-   [View Conflict Information for Merge Publications &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/view-conflict-information-for-merge-publications.md)    
-   [Measure Latency and Validate Connections for Transactional Replication](../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)  
  
  
