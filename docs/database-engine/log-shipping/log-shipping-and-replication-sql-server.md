---
title: "Log Shipping and Replication (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], log shipping and"
  - "log shipping [SQL Server], replication and"
ms.assetid: 132bebfd-0206-4d23-829a-b38e5ed17bc9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Log Shipping and Replication (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Log shipping involves two copies of a single database that typically reside on different computers. At any given time, only one copy of the database is currently available to clients. This copy is known as the primary database. Updates made by clients to the primary database are propagated by means of log shipping to the other copy of the database, known as the secondary database. Log shipping involves applying the transaction log from every insertion, update, or deletion made on the primary database onto the secondary database.  
  
 Log shipping can be used in conjunction with replication, with the following behavior:  
  
-   Replication does not continue after a log shipping failover. If a failover occurs, replication agents do not connect to the secondary, so transactions are not replicated to Subscribers. If a failback to the primary occurs, replication resumes. All transactions that log shipping copies from the secondary back to the primary are replicated to Subscribers.  
  
-   If the primary is permanently lost, the secondary can be renamed so that replication can continue. The remainder of this topic describes the requirements and procedures for handling this case. The example given is the publication database, which is the most common database to log ship, but a similar process can also be applied to subscription and distribution databases.  
  
 For information about recovering databases involved in replication without any need to reconfigure replication, see [Back Up and Restore Replicated Databases](../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md).  
  
> [!NOTE]  
>  We recommend using database mirroring, rather than log shipping, to provide availability for the publication database. For more information, see [Database Mirroring and Replication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md).  
  
## Requirements and Procedures for Replicating from the Secondary If the Primary Is Lost  
 Be aware of the following requirements and considerations:  
  
-   If a primary contains more than one publication database, log ship all of the publication databases to the same secondary.  
  
-   The installation path for the secondary server instance must be the same as the primary. User database locations on the secondary server must be the same as on the primary.  
  
-   Back up the service master key at the primary. This key will be restored at the secondary. For more information, see [BACKUP SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/backup-service-master-key-transact-sql.md).  
  
-   Log shipping does not guarantee against data loss. A failure on the primary database can result in the loss of data that has not yet been backed up or for backups that are lost during the failure.  
  
### Log Shipping with Transactional Replication  
 For transactional replication, the behavior of log shipping depends on the **sync with backup** option. This option can be set on the publication database and distribution database; in log shipping for the Publisher, only the setting on the publication database is relevant.  
  
 Setting this option on the publication database ensures that transactions are not delivered to the distribution database until they are backed up at the publication database. The last publication database backup can then be restored at the secondary server without any possibility of the distribution database having transactions that the restored publication database does not have. This option guarantees that if the Publisher fails over to a secondary server, consistency is maintained between the Publisher, Distributor, and Subscribers. Latency and throughput are affected because transactions cannot be delivered to the distribution database until they have been backed up at the Publisher; if your application can tolerate this latency, we recommend that you set this option on the publication database. If the **sync with backup** option is not set, Subscribers might receive changes that are no longer included in the recovered database at the secondary server. For more information, see [Strategies for Backing Up and Restoring Snapshot and Transactional Replication](../../relational-databases/replication/administration/strategies-for-backing-up-and-restoring-snapshot-and-transactional-replication.md).  
  
 **To configure transactional replication and log shipping with the sync with backup option**  
  
1.  If the sync with backup option is not set on the publication database, execute `sp_replicationdboption '<publicationdatabasename>', 'sync with backup', 'true'`. For more information, see [sp_replicationdboption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md).  
  
2.  Configure log shipping for the publication database. For more information, see [Configure Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/configure-log-shipping-sql-server.md).  
  
3.  If the Publisher fails, restore the last log of the database to the secondary server, using the KEEP_REPLICATION option of RESTORE LOG. This retains all replication settings for the database. For more information, see [Fail Over to a Log Shipping Secondary &#40;SQL Server&#41;](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md) and [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
4.  Restore the **msdb** database and **master** databases from the primary to the secondary. For more information, see [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md). If the primary was also a Distributor, restore the distribution database from the primary to the secondary.  
  
     These databases must be consistent with the publication database at the primary in terms of replication configuration and settings.  
  
5.  At the secondary server, rename the computer and then rename the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to match the primary server name. For information about renaming the computer, see the Windows documentation. For information about renaming the server, see [Rename a Computer that Hosts a Stand-Alone Instance of SQL Server](../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md) and [Rename a SQL Server Failover Cluster Instance](../../sql-server/failover-clusters/install/rename-a-sql-server-failover-cluster-instance.md).  
  
6.  At the secondary server, restore the service master key that was backed up from the primary. For more information, see [RESTORE SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-service-master-key-transact-sql.md).  
  
 **To configure transactional replication and log shipping without the sync with backup option**  
  
1.  Configure log shipping for the publication database. For more information, see [Configure Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/configure-log-shipping-sql-server.md).  
  
2.  If the Publisher fails, restore the last log of the database to the secondary server, using the KEEP_REPLICATION option of RESTORE LOG. This retains all replication settings for the database. For more information, see [Fail Over to a Log Shipping Secondary &#40;SQL Server&#41;](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md) and [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
3.  Restore the **msdb** database and **master** databases from the primary to the secondary. For more information, see [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md). If the primary was also a Distributor, restore the distribution database from the primary to the secondary.  
  
     These databases must be consistent with the publication database at the primary in terms of replication configuration and settings.  
  
4.  At the secondary server, rename the computer and then rename the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to match the primary server name. For information about renaming the computer, see the Windows documentation. For information about renaming the server, see [Rename a Computer that Hosts a Stand-Alone Instance of SQL Server](../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md) and [Rename a SQL Server Failover Cluster Instance](../../sql-server/failover-clusters/install/rename-a-sql-server-failover-cluster-instance.md).  
  
     You might receive an error message from the Log Reader Agent that the publication database and the distribution database are not synchronized.  
  
5.  At the secondary server, restore the service master key that was backed up from the primary. For more information, see [RESTORE SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-service-master-key-transact-sql.md).  
  
6.  Execute **sp_replrestart**. This stored procedure can be used to force the Log Reader Agent to ignore all the previous replicated transactions in the publication database log. Transactions applied after the completion of the stored procedure are processed by the Log Reader Agent. For more information, see [sp_replrestart &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replrestart-transact-sql.md).  
  
7.  Restart the Log Reader Agent after the stored procedure executes successfully. For more information, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/agents/start-and-stop-a-replication-agent-sql-server-management-studio.md).  
  
8.  Transactions that have already been distributed to Subscriber might be applied at the Publisher. To ensure that the Distribution Agent does not fail with an error when attempting to reapply these transactions at a Subscriber, specify the agent profile titled **Continue On Data Consistency Errors**.  
  
### Log Shipping with Merge Replication  
 Follow the steps in the procedure below to configure merge replication and log shipping.  
  
 **To configure merge replication and log shipping**  
  
1.  Configure log shipping for the publication database. For more information, see [Configure Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/configure-log-shipping-sql-server.md).  
  
2.  If the Publisher fails, at the secondary server, rename the computer and then rename the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to match the primary server name. For information about renaming the computer, see the Windows documentation. For information about renaming the server, see [Rename a Computer that Hosts a Stand-Alone Instance of SQL Server](../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md) and [Rename a SQL Server Failover Cluster Instance](../../sql-server/failover-clusters/install/rename-a-sql-server-failover-cluster-instance.md).  
  
3.  Restore the last log of the database to the secondary server, using the KEEP_REPLICATION option of RESTORE LOG. This retains all replication settings for the database. For more information, see [Fail Over to a Log Shipping Secondary &#40;SQL Server&#41;](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md) and [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
4.  Restore the **msdb** database and **master** databases from the primary to the secondary. For more information, see [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md). If the primary was also a Distributor, restore the distribution database from the primary to the secondary.  
  
     These databases must be consistent with the publication database at the primary in terms of replication configuration and settings.  
  
5.  At the secondary server, restore the service master key that was backed up from the primary. For more information, see [RESTORE SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-service-master-key-transact-sql.md).  
  
6.  Synchronize the publication database with one or more subscription databases. This allows you to upload those changes made previously in the publication database, but not represented in the restored backup. The data that can be uploaded depends on the way in which a publication is filtered:  
  
    -   If the publication is not filtered, you should be able to bring the publication database up-to-date by synchronizing with the most up-to-date Subscriber.  
  
    -   If the publication is filtered, you might not be able to bring the publication database up-to-date. Consider a table that is partitioned such that each subscription receives customer data only for a single region: North, East, South, and West. If there is at least one Subscriber for each partition of data, synchronizing with a Subscriber for each partition should bring the publication database up-to-date. However, if data in the West partition, for example, was not replicated to any Subscribers, this data at the Publisher cannot be brought up-to-date. In this case, we recommend reinitializing all subscriptions so that the data at the Publisher and Subscribers converges. For more information, see [Reinitialize Subscriptions](../../relational-databases/replication/reinitialize-subscriptions.md).  
  
     If you synchronize with a Subscriber that is running a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the subscription cannot be anonymous; it must be a client subscription or server subscription (referred to as local subscriptions and global subscriptions in previous releases). For more information, see [Synchronize Data](../../relational-databases/replication/synchronize-data.md).  
  
## See Also  
 [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md)   
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [Database Mirroring and Replication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md)  
  
  
