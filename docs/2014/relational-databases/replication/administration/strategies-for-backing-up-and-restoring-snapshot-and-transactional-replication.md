---
title: "Strategies for Backing Up and Restoring Snapshot and Transactional Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "backups [SQL Server replication], snapshot replication"
  - "restoring [SQL Server replication], transactional replication"
  - "snapshot replication [SQL Server], backup and restore"
  - "restoring [SQL Server replication], snapshot replication"
  - "recovery [SQL Server replication], transactional replication"
  - "transactional replication, backup and restore"
  - "recovery [SQL Server replication], snapshot replication"
  - "sync with backup [SQL Server replication]"
  - "backups [SQL Server replication], transactional replication"
ms.assetid: a8afcdbc-55db-4916-a219-19454f561f9e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Strategies for Backing Up and Restoring Snapshot and Transactional Replication
  When you design a backup and restore strategy for snapshot and transactional replication, there are three areas to consider:  
  
-   Which databases to back up.  
  
-   Backup settings for transactional replication.  
  
-   The steps that are required to restore a database. These depend on the type of replication and options chosen.  
  
 This topic covers each of these areas in the next three sections. For information about backup and restore for Oracle publishing, see [Backup and Restore for Oracle Publishers](../non-sql/backup-and-restore-for-oracle-publishers.md).  
  
## Backing up Databases  
 For snapshot and transactional replication, you should regularly back up the following databases:  
  
-   The publication database at the Publisher.  
  
-   The distribution database at the Distributor.  
  
-   The subscription database at each Subscriber.  
  
-   The **master** and **msdb** system databases at the Publisher, Distributor and all Subscribers. These databases should be backed up at the same time as each other and the relevant replication database. For example, back up the **master** and **msdb** databases at the Publisher at the same time that you back up the publication database. If the publication database is restored, make sure that the **master** and **msdb** databases are consistent with the publication database with regard to replication configuration and settings.  
  
 If you perform regular log backups, any replication-related changes should be captured in the log backups. If you do not perform log backups, a backup should be performed whenever a setting relevant to replication is changed. For more information, see [Common Actions Requiring an Updated Backup](common-actions-requiring-an-updated-backup.md).  
  
## Backup Settings for Transactional Replication  
 Transactional replication includes using the **sync with backup** option, which can be set on the distribution database and the publication database:  
  
-   We recommend that you always set this option on the distribution database.  
  
     Setting this option on the distribution database ensures that transactions in the log of the publication database will not be truncated until they have been backed up at the distribution database. The distribution database can be restored to the last backup, and any missing transactions are delivered from the publication database to the distribution database. Replication continues unaffected.  
  
     Setting this option on the distribution database does not affect replication latency. However, the option will delay the truncation of the log on the publication database until the corresponding transactions in the distribution database have been backed up. (This can create a larger transaction log in the publication database.)  
  
-   We recommend that you set this option on the publication database if your application can tolerate additional latency.  
  
     Setting this option on the publication database ensures that transactions are not delivered to the distribution database until they are backed up at the publication database. The last publication database backup can then be restored at the Publisher without any chance of the distribution database having transactions that the restored publication database does not have.  
  
     Latency and throughput are affected because transactions cannot be delivered to the distribution database until they have been backed up at the Publisher. For example, if the transaction log is backed up every five minutes, there is an additional five minutes of latency between when a transaction is committed at the Publisher and when the transaction is delivered to the distribution database, and subsequently the Subscriber.  
  
    > [!NOTE]  
    >  The **sync with backup** option ensures consistency between the publication database and the distribution database, but the option does not guarantee against data loss. For example, if the transaction log is lost, transactions that have been committed since the last transaction log backup will not be available in the publication database or the distribution database. This is the same behavior as a nonreplicated database.  
  
 **To set the sync with backup option**  
  
-   Replication [!INCLUDE[tsql](../../../includes/tsql-md.md)] programming: [Enable Coordinated Backups for Transactional Replication &#40;Replication Transact-SQL Programming&#41;](enable-coordinated-backups-for-transactional-replication.md)  
  
## Restoring Databases Involved in Replication  
 You can restore all databases in a replication topology if recent backups are available and the appropriate steps are followed. The restore steps for the publication database depend on the type of replication and options that are used; however, the restore steps for all other databases are independent of the type and options.  
  
 Replication supports restoring replicated databases to the same server and database from which the backup was created. If you restore a backup of a replicated database to another server or database, replication settings cannot be preserved. In this case, you must re-create all publications and subscriptions after backups are restored.  
  
### Publisher  
 There are restore steps provided for the following types of replication:  
  
-   Snapshot replication  
  
-   Read-only transactional replication  
  
-   Transactional replication with updating subscriptions  
  
-   Peer-to-peer transactional replication  
  
 The restore of the **msdb** and **master** databases, which are also covered in this section, is the same for all four types.  
  
#### Publication Database: Snapshot Replication  
  
1.  Restore the latest backup of the publication database. Go to step 2.  
  
2.  Does the publication database backup contain the latest configuration for all publications and subscriptions? If yes, the restore is completed. If no, go to step 3.  
  
3.  Remove the replication configuration from the Publisher, Distributor and Subscribers, and then re-create the configuration. Restore is completed.  
  
     For more information about how to remove replication, see [sp_removedbreplication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql).  
  
#### Publication Database: Read-Only Transactional Replication  
  
1.  Restore the latest backup of the publication database. Go to step 2.  
  
2.  Was the **sync with backup** setting enabled on the publication database before the failure? If yes, go to step 3; if no, go to step 5.  
  
     If the setting is enabled, the query `SELECT DATABASEPROPERTYEX('<PublicationDatabaseName>', 'IsSyncWithBackup')` returns '1'.  
  
3.  Is the restored backup complete and up-to-date? Does it contain the latest configuration for all publications and subscriptions? If yes, the restore is completed. If no, go to step 4.  
  
4.  The configuration information in the restored publication database is not up-to-date. Therefore, you must make sure that the Subscribers have all outstanding commands in the distribution database, and then drop and re-create the replication configuration.  
  
    1.  Run the Distribution Agent until all Subscribers are synchronized with the outstanding commands in the distribution database. Verify that all commands are delivered to Subscribers by using the **Undistributed Commands** tab in Replication Monitor or by querying the [MSdistribution_status](/sql/relational-databases/system-views/msdistribution-status-transact-sql) view in the distribution database. Go to step b.  
  
         For more information about how to run the Distribution Agent, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md) and [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md).  
  
         For more information about how to verify commands, see [View Replicated Commands and Other Information in the Distribution Database &#40;Replication Transact-SQL Programming&#41;](../monitor/view-replicated-commands-and-information-in-distribution-database.md) and [View Information and Perform Tasks using Replication Monitor](../monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
    2.  Remove the replication configuration from the Publisher, Distributor and Subscribers, and then re-create the configuration. When you re-create subscriptions, specify that the Subscriber already has the data. The restore is completed.  
  
         For more information about how to remove replication, see [sp_removedbreplication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql).  
  
         For more information about how to specify that the Subscriber already has the data, see [Initialize a Subscription Manually](../initialize-a-subscription-manually.md).  
  
5.  The **sync with backup** option was not set on the publication database. Therefore, transactions that were not included in the restored backup might have been delivered to the Distributor and Subscribers. You must now make sure that Subscribers have all outstanding commands in the distribution database, and then manually apply to the publication database any transactions that are not included in the restored backup.  
  
    > [!IMPORTANT]  
    >  Performing this process can cause published tables to be restored to a point in time that is more recent than the point in time of other nonpublished tables that are restored from the backup.  
  
    1.  Run the Distribution Agent until all Subscribers are synchronized with the outstanding commands in the distribution database. Verify that all commands are delivered to Subscribers by using the **Undistributed Commands** tab in Replication Monitor or by querying the [MSdistribution_status](/sql/relational-databases/system-views/msdistribution-status-transact-sql) view in the distribution database. Go to step b.  
  
         For more information about how to run the Distribution Agent, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md) and [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md).  
  
         For more information about how to verify commands, see [View Replicated Commands and Other Information in the Distribution Database &#40;Replication Transact-SQL Programming&#41;](../monitor/view-replicated-commands-and-information-in-distribution-database.md) and [View Information and Perform Tasks using Replication Monitor](../monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
    2.  Use the [tablediff utility](../../../tools/tablediff-utility.md) or another tool to manually synchronize the Publisher with the Subscriber. This enables you to recover data from the subscription database that was not contained in the publication database backup. Go to step c.  
  
         For more information about the **tablediff** utility, see [Compare Replicated Tables for Differences &#40;Replication Programming&#41;](compare-replicated-tables-for-differences-replication-programming.md).  
  
    3.  Is the restored backup complete and up-to-date? Does it contain the latest configuration for all publications and subscriptions? If yes, execute the [sp_replrestart](/sql/relational-databases/system-stored-procedures/sp-replrestart-transact-sql) stored procedure to resynchronize the Publisher metadata with the Distributor metadata. The restore is completed. If no, go to step d.  
  
    4.  Remove the replication configuration from the Publisher, Distributor and Subscribers, and then re-create the configuration. When you re-create subscriptions, specify that the Subscriber already has the data. The restore is completed.  
  
         For more information about how to remove replication, see [sp_removedbreplication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql).  
  
         For more information about how to specify that the Subscriber already has the data, see [Initialize a Subscription Manually](../initialize-a-subscription-manually.md).  
  
#### Publication Database: Transactional Replication with Updating Subscriptions  
  
1.  Restore the latest backup of the publication database. Go to step 2.  
  
2.  Run the Distribution Agent until all Subscribers are synchronized with the outstanding commands in the distribution database. Verify that all commands are delivered to Subscribers by using the **Undistributed Commands** tab in Replication Monitor, or by querying the [MSdistribution_status](/sql/relational-databases/system-views/msdistribution-status-transact-sql) view in the distribution database. Go to step 3.  
  
     For more information about how to run the Distribution Agent, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md) and [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md).  
  
     For more information about how to verify commands, see [View Replicated Commands and Other Information in the Distribution Database &#40;Replication Transact-SQL Programming&#41;](../monitor/view-replicated-commands-and-information-in-distribution-database.md) and [View Information and Perform Tasks using Replication Monitor](../monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
3.  If you are using queued updating subscriptions, connect to each Subscriber and delete all rows from the [MSreplication_queue &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/msreplication-queue-transact-sql) table in the subscription database. Go to step 4.  
  
    > [!NOTE]  
    >  If you are using queued updating subscriptions and any tables contain identity columns, you must make sure that the correct identity ranges are assigned after a restore. For more information, see [Replicate Identity Columns](../publish/replicate-identity-columns.md).  
  
4.  You must now make sure that Subscribers have all outstanding commands in the distribution database, and then manually apply to the publication database any transactions that are not included in the restored backup.  
  
    > [!IMPORTANT]  
    >  Performing this process can cause published tables to be restored to a point in time that is more recent than the point in time of other nonpublished tables that are restored from the backup.  
  
    1.  Run the Distribution Agent until all Subscribers are synchronized with the outstanding commands in the distribution database. Verify that all commands are delivered to Subscribers by using Replication Monitor or by querying the [MSdistribution_status](/sql/relational-databases/system-views/msdistribution-status-transact-sql) view in the distribution database. Go to step b.  
  
    2.  Use the [tablediff Utility](../../../tools/tablediff-utility.md) or another tool to manually synchronize the Publisher with the Subscriber. This enables you to recover data from the subscription database that was not contained in the publication database backup. Go to step c.  
  
         For more information about the **tablediff** utility, see [Compare Replicated Tables for Differences &#40;Replication Programming&#41;](compare-replicated-tables-for-differences-replication-programming.md).  
  
    3.  Is the restored backup complete and up-to-date? Does it contain the latest configuration for all publications and subscriptions? If yes, execute the [sp_replrestart](/sql/relational-databases/system-stored-procedures/sp-replrestart-transact-sql) stored procedure to resynchronize the Publisher metadata with the Distributor metadata. The restore is completed. If no, go to step d.  
  
    4.  Remove the replication configuration from the Publisher, Distributor and Subscribers, and then re-create the configuration. When you re-create subscriptions, specify that the Subscriber already has the data. The restore is completed.  
  
         For more information about how to remove replication, see and [sp_removedbreplication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql).  
  
         For more information about how to specify that the Subscriber already has the data, see [Initialize a Subscription Manually](../initialize-a-subscription-manually.md).  
  
#### Publication Database: Peer-to-Peer Transactional Replication  
 In the following steps, publication databases **A**, **B**, and **C** are in a peer-to-peer transactional replication topology. Databases **A** and **C** are online and functioning properly; database **B** is the database to be restored. The process described here, especially steps 7, 10, and 11, is very similar to the process required to add a node to a peer-to-peer topology. The most straightforward way to perform these steps is to use the Configure Peer-to-Peer Topology Wizard, but you can also use stored procedures.  
  
1.  Run the Distribution Agents to synchronize the subscriptions at databases **A** and **C**. Go to step 2.  
  
     For more information about how to run the Distribution Agent, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md) and [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md).  
  
2.  If the distribution database that **B** uses is still available, run Distribution Agents to synchronize subscriptions between databases **B** and **A** and databases and B and **C**. Go to step 3.  
  
3.  Remove metadata from the distribution database that **B** uses by executing [sp_removedistpublisherdbreplication](/sql/relational-databases/system-stored-procedures/sp-removedistpublisherdbreplication-transact-sql) at the distribution database for **B**. Go to step 4.  
  
4.  At databases **A** and **C**, drop the subscriptions to the publication at database **B**. Go to step 5.  
  
     For more information about how to drop subscriptions, see [Subscribe to Publications](../subscribe-to-publications.md).  
  
5.  Perform a log backup or full backup of database **A**. Go to step 6.  
  
6.  Restore the backup of database **A** at database **B**. Database **B** now has the data from database **A**, but not the replication configuration. When you restore a backup to another server, replication is removed; therefore, replication has been removed from database **B**. Go to step 7.  
  
7.  Re-create the publication at database **B**, and then re-create subscriptions between databases **A** and **B**. (Subscriptions that involve database **C** are handled at a later stage.).  
  
    1.  Re-create the publication at database **B**. Go to step b.  
  
    2.  Re-create the subscription at database **B** to the publication at database **A**, specifying that the subscription should be initialized with a backup (a value of **initialize with backup** for the **@sync_type** parameter of [sp_addsubscription](/sql/relational-databases/system-stored-procedures/sp-addsubscription-transact-sql)). Go to step c.  
  
    3.  Re-create the subscription at database **A** to the publication at database **B**, specifying that the Subscriber already has the data (a value of **replication support only** for the **@sync_type** parameter of [sp_addsubscription](/sql/relational-databases/system-stored-procedures/sp-addsubscription-transact-sql)). Go to step 8.  
  
8.  Run the Distribution Agents to synchronize the subscriptions at databases **A** and **B**. If there are any identity columns in published tables, go to step 9. If not, go to step 10.  
  
9. After the restore, the identity range that you assigned for each table in database **A** would also be used in database **B**. Make sure that the restored database **B** has received all changes from the failed database **B** that were propagated to database **A** and database **C**; and then reseed the identity range for each table.  
  
    1.  Execute [sp_requestpeerresponse](/sql/relational-databases/system-stored-procedures/sp-requestpeerresponse-transact-sql) at database **B** and retrieve the output parameter **@request_id**. Go to step b.  
  
    2.  By default, the Distribution Agent is set to run continuously; therefore, tokens should be sent to all nodes automatically. If the Distribution Agent is not running in continuous mode, run the agent. For more information, see [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md) or [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md). Go to step c.  
  
    3.  Execute [sp_helppeerresponses](/sql/relational-databases/system-stored-procedures/sp-helppeerresponses-transact-sql), providing the **@request_id** value retrieved in step b. Wait until all nodes indicate they have received the peer request. Go to step d.  
  
    4.  Use [DBCC CHECKIDENT](/sql/t-sql/database-console-commands/dbcc-checkident-transact-sql) to reseed each table in database **B** to make sure that an appropriate range is used. Go to step 10.  
  
     For more information about how to manage identity ranges, see the "Assigning ranges for manual identity range management" section of [Replicate Identity Columns](../publish/replicate-identity-columns.md).  
  
10. At this point, database **B** and database **C** are not directly connected, but they will receive changes through database **A**. If the topology contains any nodes that are running [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], go to step 11; otherwise, go to step 12.  
  
11. Quiesce the system, and then re-create the subscription between databases **B** and **C**. Quiescing a system involves stopping activity on published tables at all nodes and making sure of that each node has received all changes from all other nodes.  
  
    1.  Stop all activity on published tables in the peer-to-peer topology. Go to step b.  
  
    2.  Execute [sp_requestpeerresponse](/sql/relational-databases/system-stored-procedures/sp-requestpeerresponse-transact-sql) at database **B** and retrieve the output parameter **@request_id**. Go to step c.  
  
    3.  By default, the Distribution Agent is set to run continuously; therefore, tokens should be sent to all nodes automatically. If the Distribution Agent is not running in continuous mode, run the agent. Go to step d.  
  
    4.  Execute [sp_helppeerresponses](/sql/relational-databases/system-stored-procedures/sp-helppeerresponses-transact-sql), providing the **@request_id** value retrieved in step b. Wait until all nodes indicate they have received the peer request. Go to step e.  
  
    5.  Re-create the subscription at database **B** to the publication at database **C**, specifying that the Subscriber already has the data. Go to step b.  
  
    6.  Re-create the subscription at database **C** to the publication at database **B**, specifying that the Subscriber already has the data. Go to step 13.  
  
12. Re-create the subscription between databases **B** and **C**:  
  
    1.  At database **B**, query the [MSpeer_lsns](/sql/relational-databases/system-tables/mspeer-lsns-transact-sql) table to retrieve the log sequence number (LSN) of the most recent transaction that database **B** has received from database **C**.  
  
    2.  Re-create the subscription at database **B** to the publication at database **C**, specifying that the subscription should be initialized based on LSN (a value of **initialize from lsn** for the **@sync_type** parameter of [sp_addsubscription](/sql/relational-databases/system-stored-procedures/sp-addsubscription-transact-sql)). Go to step b.  
  
    3.  Re-create the subscription at database **C** to the publication at database **B**, specifying that the Subscriber already has the data. Go to step 13.  
  
13. Run the Distribution Agents to synchronize the subscriptions at databases **B** and **C**. The restore is completed.  
  
#### msdb Database (Publisher)  
  
1.  Restore the latest backup of the **msdb** database.  
  
2.  Is the restored backup complete and up-to-date? Does it contain the latest configuration for all publications and subscriptions? If yes, recovery is completed. If no, go to step 3.  
  
3.  Re-create the subscription cleanup job from your replication scripts. Recovery is completed.  
  
#### master Database (Publisher)  
  
1.  Restore the latest backup of the **master** database.  
  
2.  Make sure that the database is consistent with the publication database with regard to replication configuration and settings.  
  
### Databases at the Distributor  
  
#### Distribution Database  
  
1.  Restore the latest backup of the distribution database.  
  
2.  Was the **sync with backup** setting enabled on the distribution database before the failure? If yes, go to step 3; if no, go to step 4.  
  
     If the setting is enabled, the query `SELECT DATABASEPROPERTYEX('<DistributionDatabaseName>', 'IsSyncWithBackup')` returns '1'.  
  
3.  Is the restored backup complete and up-to-date? Does it contain the latest configuration for all publications and subscriptions? If yes, recovery is completed. If no, go to step 4.  
  
4.  Either the configuration information in the restored distribution database is not up-to-date, or the **sync with backup** option was not set on the distribution database. (After the restore, the distribution database might be missing transactions that were committed at the Publisher but have not yet been delivered to Subscribers.) Drop and re-create replication, and then run validation.  
  
    1.  Remove the replication configuration from the Publisher, Distributor and Subscribers, and then re-create the configuration. When you re-create subscriptions, specify that the Subscriber already has the data. Go to step b.  
  
         For more information about how to remove replication, see [sp_removedbreplication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql).  
  
         For more information about how to specify that the Subscriber already has the data, see [Initialize a Subscription Manually](../initialize-a-subscription-manually.md).  
  
    2.  Mark all publications for validation. Reinitialize any subscriptions that fail validation. Recovery is completed.  
  
         For more information about validation, see [Validate Replicated Data](../validate-data-at-the-subscriber.md). For more information about reinitialization, see [Reinitialize Subscriptions](../reinitialize-subscriptions.md).  
  
#### msdb Database (Distributor)  
  
1.  Restore the latest backup of the **msdb** database.  
  
2.  Is the restored backup complete and up-to-date? Does it contain the latest configuration for all publications and subscriptions? If yes, recovery is completed. If no, go to step 3.  
  
3.  Remove the replication configuration from the Publisher, Distributor and Subscribers, and then re-create the configuration. When you re-create subscriptions, specify that the Subscriber already has the data. Go to step 4.  
  
     For more information about how to remove replication, see [sp_removedbreplication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql).  
  
     For more information about how to specify that the Subscriber already has the data, see [Initialize a Subscription Manually](../initialize-a-subscription-manually.md).  
  
4.  Mark all publications for validation. Reinitialize any subscriptions that fail validation. Recovery is completed.  
  
     For more information about validation, see [Validate Replicated Data](../validate-data-at-the-subscriber.md). For more information about reinitialization, see [Reinitialize Subscriptions](../reinitialize-subscriptions.md).  
  
#### master Database (Distributor)  
  
1.  Restore the latest backup of the **master** database.  
  
2.  Make sure that the database is consistent with the publication database with regard to replication configuration and settings.  
  
### Databases at the Subscriber  
  
#### Subscription Database  
  
1.  Is the latest subscription database backup more recent than the minimum distribution retention setting on the distribution database? (This determines whether the Distributor still has all the commands that are required to bring the Subscriber up-to-date.) If yes, go to step 2. If no, reinitialize the subscription. Recovery is completed.  
  
     To determine the maximum distribution retention setting, execute [sp_helpdistributiondb](/sql/relational-databases/system-stored-procedures/sp-helpdistributiondb-transact-sql) and retrieve the value from the **max_distretention** column (this value is in hours).  
  
     For more information about how to reinitialize a subscription, see [Reinitialize a Subscription](../reinitialize-a-subscription.md).  
  
2.  Restore the latest subscription database backup. Go to step 3.  
  
3.  If the subscription database contains only push subscriptions, go to step 4. If the subscription database contains any pull subscriptions, ask the following questions: Is the subscription information current? Does the database include all tables and options that were set at the time of failure. If yes, go to step 4. If no, reinitialize the subscription. Recovery is completed.  
  
4.  To synchronize the Subscriber, run the Distribution Agent. Recovery is completed.  
  
     For more information about how to run the Distribution Agent, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md) and [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md).  
  
#### msdb Database (Subscriber)  
  
1.  Restore the latest backup of the **msdb** database. Are pull subscriptions used at this Subscriber? If no, the restore is completed. If yes, go to step 2.  
  
2.  Is the restored backup complete and up-to-date? Does it contain the latest configuration for all pull subscriptions? If yes, recovery is completed. If no, go to step 3.  
  
3.  Drop and re-create the pull subscriptions. When you re-create the subscriptions, specify that the Subscriber already has the data. The restore is completed.  
  
     For more information about how to drop subscriptions, see [Subscribe to Publications](../subscribe-to-publications.md).  
  
     For more information about how to specify that the Subscriber already has the data, see [Initialize a Subscription Manually](../initialize-a-subscription-manually.md).  
  
#### master Database (Subscriber)  
  
1.  Restore the latest backup of the **master** database.  
  
2.  Make sure that the database is consistent with the publication database with regard to replication configuration and settings.  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](../../backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Back Up and Restore Replicated Databases](back-up-and-restore-replicated-databases.md)   
 [Configure Distribution](../configure-distribution.md)   
 [Publish Data and Database Objects](../publish/publish-data-and-database-objects.md)   
 [Subscribe to Publications](../subscribe-to-publications.md)   
 [Initialize a Subscription](../initialize-a-subscription.md)   
 [Synchronize Data](../synchronize-data.md)  
  
  
