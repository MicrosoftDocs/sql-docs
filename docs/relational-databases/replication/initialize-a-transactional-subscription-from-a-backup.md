---
title: "Initialize a Transactional Subscription from a Backup | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "manual subscription initialization [SQL Server replication]"
  - "subscriptions [SQL Server replication], initializing"
  - "initializing subscriptions [SQL Server replication], without snapshots"
  - "transactional replication, backup and restore"
  - "backups [SQL Server replication], transactional replication"
ms.assetid: d0637fc4-27cc-4046-98ea-dc86b7a3bd75
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Initialize a Transactional Subscription from a Backup
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Although a subscription to a transactional publication is typically initialized with a snapshot, a subscription can be initialized from a backup using replication stored procedures. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
### To initialize a transactional subscriber from a backup  
  
1.  For an existing publication, ensure that the publication supports the ability to initialize from backup by executing [sp_helppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md) at the Publisher on the publication database. Note the value of **allow_initialize_from_backup** in the result set.  
  
    -   If the value is **1**, the publication supports this functionality.  
  
    -   If the value is **0**, execute [sp_changepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md) at the Publisher on the publication database. Specify a value of **allow_initialize_from_backup** for **@property** and a value of **true** for **@value**.  
  
2.  For a new publication, execute [sp_addpublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md) at the Publisher on the publication database. Specify a value of **true** for **allow_initialize_from_backup**. For more information, see [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md).  
  
    > [!WARNING]  
    >  To avoid missing subscriber data, when using **sp_addpublication** with `@allow_initialize_from_backup = N'true'`, always use `@immediate_sync = N'true'`.  
  
3.  Create a backup of the publication database using the [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md) statement.  
  
4.  Restore the backup on the Subscriber using the [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md) statement.  
  
5.  At the Publisher on the publication database, execute the stored procedure [sp_addsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). Specify the following parameters:  
  
    -   **@sync_type** - a value of **initialize with backup**.  
  
    -   **@backupdevicetype** - the type of backup device: **logical** (default), **disk**, or **tape**.  
  
    -   **@backupdevicename** - the logical or physical backup device to use for the restore.  
  
         For a logical device, specify the name of the backup device specified when **sp_addumpdevice** was used to create the device.  
  
         For a physical device, specify a complete path and file name, such as `DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\BACKUP\Mybackup.dat'` or `TAPE = '\\.\TAPE0'`.  
  
    -   (Optional) **@password** - a password that was provided when the backup set was created.  
  
    -   (Optional) **@mediapassword** - a password that was provided when the media set was formatted.  
  
    -   (Optional) **@fileidhint** - identifier for the backup set to be restored. For example, specifying **1** indicates the first backup set on the backup medium and **2** indicates the second backup set.  
  
    -   (Optional for tape devices) **@unload** - specify a value of **1** (default) if the tape should be unloaded from the drive after the restore is complete and **0** if it should not be unloaded.  
  
6.  (Optional) For a pull subscription, execute [sp_addpullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md) and [sp_addpullsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md) at the Subscriber on the subscription database. For more information, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md).  
  
7.  (Optional) Start the Distribution Agent. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md) or [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
## See Also  
 [Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md)   
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)  
  
  
