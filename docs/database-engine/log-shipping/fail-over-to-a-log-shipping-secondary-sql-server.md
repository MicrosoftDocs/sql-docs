---
title: "Fail over to a log shipping secondary"
description: Learn how to fail over to a SQL Server log shipping secondary by using SQL Server Management Studio or Transact-SQL.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: log-shipping
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "primary databases [SQL Server]"
  - "secondary data files [SQL Server], manual fail over"
  - "log shipping [SQL Server], failover"
  - "failover [SQL Server], log shipping"
---
# Fail Over to a Log Shipping Secondary (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Failing over to a log shipping secondary is useful if the primary server instance fails or requires maintenance.  
  
## Preparing for a Controlled Failover  
 Typically, the primary and secondary databases are unsynchronized, because the primary database continues to be updated after its latest backup job. Also, in some cases, recent transaction log backups have not been copied to the secondary server instances, or some copied log backups might still not have been applied to the secondary database. We recommend that you begin by synchronizing all of the secondary databases with the primary database, if possible.  
  
 For information about log shipping jobs, see [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md).  
  
## Failing Over  
 To fail over to a secondary database:  
  
1.  Copy any uncopied backup files from the backup share to the copy destination folder of each secondary server.  
  
2.  Apply any unapplied transaction log backups in sequence to each secondary database. For more information, see [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md).  
  
3.  If the primary database is accessible back up the active transaction log and apply the log backup to the secondary databases. You may need to set the database to [single-user mode](../../relational-databases/databases/set-a-database-to-single-user-mode.md) to obtain exclusive access before issuing the restore command, and then switch it back to multi-user after the restore completes.  
  
     If the original primary server instance is not damaged, back up the tail of the transaction log of the primary database using WITH NORECOVERY. This leaves the database in the restoring state and therefore unavailable to users. Eventually you will be able to roll this database forward by applying transaction log backups from the replacement primary database.  
  
     For more information, see [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md).   
  
4.  After the secondary servers are synchronized, you can fail over to whichever one you prefer by recovering its secondary database and redirecting clients to that server instance. Recovering puts the database into a consistent state and brings it online.  
  
    > [!NOTE]  
    >  When you make a secondary database available, you should ensure that its metadata is consistent with the metadata of the original primary database. For more information, see [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md).  
  
5.  After you have recovered a secondary database, you can reconfigure it to act as a primary database for other secondary databases.  
  
     If no other secondary database is available, see [Configure Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/configure-log-shipping-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Change Roles Between Primary and Secondary Log Shipping Servers &#40;SQL Server&#41;](../../database-engine/log-shipping/change-roles-between-primary-and-secondary-log-shipping-servers-sql-server.md)  
  
-   [Management of Logins and Jobs After Role Switching &#40;SQL Server&#41;](../../sql-server/failover-clusters/management-of-logins-and-jobs-after-role-switching-sql-server.md)  
  
## See Also  
 [Log Shipping Tables and Stored Procedures](../../database-engine/log-shipping/log-shipping-tables-and-stored-procedures.md)   
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md)  
  
  
