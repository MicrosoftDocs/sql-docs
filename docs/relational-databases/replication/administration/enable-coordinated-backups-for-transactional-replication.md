---
title: "Enable Coordinated Backups for Transactional Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "transactional replication, backup and restore"
  - "sp_replicationdboption"
  - "sync with backup [SQL Server replication]"
  - "coordinated backups [SQL Server replication]"
  - "backups [SQL Server replication], transactional replication"
ms.assetid: 73a914ba-8b2d-4f4d-ac1b-db9bac676a30
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Enable Coordinated Backups for Transactional Replication
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  When enabling a database for transactional replication, you can specify that all transactions must be backed up before being delivered to the distribution database. You can also enable coordinated backup on the distribution database so that the transaction log for the publication database is not truncated until transactions that have been propagated to the Distributor have been backed up. For more information, see [Strategies for Backing Up and Restoring Snapshot and Transactional Replication](../../../relational-databases/replication/administration/strategies-for-backing-up-and-restoring-snapshot-and-transactional-replication.md).  
  
### To enable coordinated backups for a database published with transactional replication  
  
1.  At the Publisher, use the [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../../t-sql/functions/databasepropertyex-transact-sql.md) function to return the **IsSyncWithBackup** property of the publication database. If the function returns **1**, coordinated backups are already enabled for the published database.  
  
2.  If the function in step 1 returns **0**, execute [sp_replicationdboption &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md) at the Publisher on the publication database. Specify a value of **sync with backup** for **@optname**, and **true** for **@value**.  
  
    > [!NOTE]  
    >  If you change the **sync with backup** option to **false**, the truncation point of the publication database will be updated after the Log Reader Agent runs, or after an interval if the Log Reader Agent is running continuously. The maximum interval is controlled by the **–MessageInterval** agent parameter (which has a default of 30 seconds).  
  
### To enable coordinated backups for a distribution database  
  
1.  At the Distributor, use the [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../../t-sql/functions/databasepropertyex-transact-sql.md) function to return the **IsSyncWithBackup** property of the distribution database. If the function returns **1**, coordinated backups are already enabled for the distribution database.  
  
2.  If the function in step 1 returns **0**, execute [sp_replicationdboption &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md) at the Distributor on the distribution database. Specify a value of **sync with backup** for **@optname** and **true** for **@value**.  
  
### To disable coordinated backups  
  
1.  At either the Publisher on the publication database or at the Distributor on the distribution database, execute [sp_replicationdboption &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md). Specify a value of **sync with backup** for **@optname** and **false** for **@value**.  
  
  
