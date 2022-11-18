---
title: "Back Up and Restore Replicated Databases | Microsoft Docs"
description: Review overview information and links to additional information about backup and restore strategies for each type of replication in SQL Server.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "backups [SQL Server replication]"
  - "administering replication, restoring"
  - "backing up replicated databases"
  - "backups [SQL Server replication], about backups"
  - "restoring replicated databases [SQL Server replication]"
  - "recovery [SQL Server replication], about recovery"
  - "restoring databases [SQL Server], replicated databases"
  - "backing up databases [SQL Server], replicated databases"
  - "restoring [SQL Server replication], about restoring"
  - "recovery [SQL Server replication]"
  - "replication [SQL Server], administering"
  - "distribution databases [SQL Server replication], backing up"
  - "restoring [SQL Server replication]"
  - "administering replication, backing up"
ms.assetid: 04588807-21e7-4bbe-9727-b72f692cffa7
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Back Up and Restore Replicated Databases
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]

  Replicated databases require special attention with regards to backing up and restoring data. This topic provides introductory information and links to further information on backup and restore strategies for each type of replication.  
  
 Replication supports restoring replicated databases to the same server and database from which the backup was created. If you restore a backup of a replicated database to another server or database, replication settings cannot be preserved. In this case, you must recreate all publications and subscriptions after backups are restored.  
  
> [!NOTE]  
>  It is possible to restore a replicated database to a standby server if log shipping is being used. For more information, see [Log Shipping and Replication &#40;SQL Server&#41;](../../../database-engine/log-shipping/log-shipping-and-replication-sql-server.md).  
  
 Replicated databases and their associated system databases should be backed up regularly. Back up the following databases:  
  
-   The publication database at the Publisher  
  
-   The distribution database at the Distributor  
  
-   The subscription database at each Subscriber  
  
-   The **master** and **msdb** system databases at the Publisher, Distributor and all Subscribers. These databases should be backed up at the same time as each other and the relevant replication database. For example, back up the **master** and **msdb** databases at the Publisher at the same time you back up the publication database. If the publication database is restored, ensure that the **master** and **msdb** database are consistent with the publication database in terms of replication configuration and settings.  
  
 If you perform regular log backups, any replication-related changes should be captured in the log backups. If you do not perform log backups, a backup should be performed whenever a setting relevant to replication is changed. For more information, see [Common Actions Requiring an Updated Backup](../../../relational-databases/replication/administration/common-actions-requiring-an-updated-backup.md).  
  
## Backup and Restore Strategies  
 The strategies for backing up and restoring each node in a replication topology differ according to the type of replication used. For information on backup and restore strategies for each type of replication, see the following topics:  
  
-   [Strategies for Backing Up and Restoring Snapshot and Transactional Replication](../../../relational-databases/replication/administration/strategies-for-backing-up-and-restoring-snapshot-and-transactional-replication.md)  
  
-   [Strategies for Backing Up and Restoring Merge Replication](../../../relational-databases/replication/administration/strategies-for-backing-up-and-restoring-merge-replication.md)  
  
 As part of any recovery strategy, always keep a current script of your replication settings in a safe location. In the event of server failure or the need to set up a test environment, you can modify the script by changing server name references, and it can be used to help recreate your replication settings. In addition to scripting your current replication settings, you should script the enabling and disabling of replication. For information about scripting replication objects, see [Scripting Replication](../../../relational-databases/replication/scripting-replication.md).  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](../../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Best Practices for Replication Administration](../../../relational-databases/replication/administration/best-practices-for-replication-administration.md)  
  
  
