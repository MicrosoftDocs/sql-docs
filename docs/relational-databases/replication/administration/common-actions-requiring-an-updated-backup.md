---
title: "Common Actions Requiring an Updated Backup | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "recovery [SQL Server replication], actions requiring a backup"
  - "restoring [SQL Server replication], actions requiring a backup"
  - "backups [SQL Server replication], actions requiring a backup"
ms.assetid: a5975bf4-183e-42e3-b7d1-ad02f89d2e1d
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Common Actions Requiring an Updated Backup
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  If you perform regular log backups, any replication-related changes should be captured in the log backups. If you don't perform log backups, perform a backup of the publication, distribution, subscription, **msdb**, and **master** databases after making modifications to your replication schema or topology.  
  
## Publication Database  
 Backup the publication database after:  
  
-   Creating new publications.  
  
-   Changing any publication property, including filtering.  
  
-   Adding articles to an existing publication.  
  
-   Performing a publication-wide reinitialization of subscriptions.  
  
-   Making a schema change on a published table.  
  
-   Performing on-demand script execution with [sp_addscriptexec &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addscriptexec-transact-sql.md).  
  
-   Changing any article property.  
  
-   Dropping any publications.  
  
-   Dropping any articles.  
  
-   Disabling replication.  
  
## Distribution Database  
 Backup the distribution database after:  
  
-   Creating or modifying replication agent profiles.  
  
-   Modifying replication agent profile parameters.  
  
-   Changing the replication agent properties (including schedules) for any push subscriptions.  
  
-   A new range of identities is assigned by the automatic identity range management feature.  
  
## Subscription Database  
 Backup the subscription database after:  
  
-   Changing any subscription property.  
  
-   Changing the priority for a merge subscription at the Publisher.  
  
-   Dropping any subscriptions.  
  
-   Disabling replication.  
  
## msdb Database  
 Backup the **msdb** system database at the appropriate node after:  
  
-   Enabling or disabling replication.  
  
-   Adding or dropping a distribution database (at the Distributor).  
  
-   Enabling or disabling a database for publishing (at the Publisher).  
  
-   Creating or modifying replication agent profiles (at the Distributor).  
  
-   Modifying any replication agent profile parameters (at the Distributor).  
  
-   Changing the replication agent properties (including schedules) for any push subscriptions (at the Distributor).  
  
-   Changing the replication agent properties (including schedules) for any pull subscriptions (at the Subscriber).  
  
-   Creating a DTS package associated with a transactional publication that uses transformable subscriptions (at the Distributor and Subscriber).  
  
-   Adding or dropping a transformable subscription (at the Distributor and Subscriber).  
  
## master Database  
 Backup the **master** system database at the appropriate node after:  
  
-   Enabling or disabling replication.  
  
-   Adding or dropping a distribution database (at the Distributor).  
  
-   Enabling or disabling a database for publishing (at the Publisher).  
  
-   Adding the first or dropping the last publication in any database (at the Publisher).  
  
-   Adding the first or dropping the last subscription in any database (at the Subscriber).  
  
-   Enabling or disabling a Publisher at a Distribution Publisher (at the Publisher and Distributor).  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](../../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Back Up and Restore Replicated Databases](../../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md)  
  
  
