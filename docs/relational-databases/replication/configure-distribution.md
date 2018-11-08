---
title: "Configure Distribution | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], distribution"
  - "distribution configuration [SQL Server replication]"
  - "remote Distributors [SQL Server replication]"
  - "transactional replication, configuring distribution"
  - "distribution databases [SQL Server replication], sizing"
  - "Distributors [SQL Server replication], configuring"
  - "distribution databases [SQL Server replication], about distribution databases"
  - "distribution databases [SQL Server replication]"
  - "merge replication [SQL Server replication], configuring distribution"
ms.assetid: 94d52169-384e-4885-84eb-2304e967d9f7
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Configure Distribution
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The Distributor is a server that contains the distribution database, which stores metadata and history data for all types of replication and transactions for transactional replication. To set up replication, you must configure a Distributor. Each Publisher can be assigned to only a single Distributor instance, but multiple publishers can share a Distributor. The Distributor uses these additional resources on the server where it is located:  
  
-   Additional disk space if the snapshot files for the publication are stored on the Distributor (which they typically are).  
  
-   Additional disk space to store the distribution database.  
  
-   Additional processor usage by replication agents for push subscriptions running on the Distributor.  
  
 The server you select as the Distributor should have adequate disk space and processor power to support replication and any other activities on that server. When you configure the Distributor, you specify the following:  
  
-   A snapshot folder, which is used, by default, for all Publishers that use this Distributor. Ensure that this folder is already shared and has the appropriate permissions set. For more information, see [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
-   A name and file locations for the distribution database. The distribution database cannot be renamed after it is created. To use a different name for the database, you must disable distribution and reconfigure it.  
  
-   Any Publishers authorized to use the Distributor. If you specify Publishers other than the instance on which the Distributor runs, you must also specify a password for the connections the Publishers make to the remote Distributor.  
  
 For transactional replication, after you configure distribution, we recommend that you:  
  
-   Size the distribution database appropriately. Test replication with a typical load for your system to determine how much space is required to store commands. Ensure the database is large enough to store commands without having to auto-grow frequently. For more information about changing the size of a database, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
-   Set the **sync with backup** option on the distribution database. For more information, see [Strategies for Backing Up and Restoring Snapshot and Transactional Replication](../../relational-databases/replication/administration/strategies-for-backing-up-and-restoring-snapshot-and-transactional-replication.md) and [Enable Coordinated Backups for Transactional Replication &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/enable-coordinated-backups-for-transactional-replication.md).  
  
## Local and Remote Distributors  
 By default, the Distributor is the same server as the Publisher (a local Distributor), but it can also be a separate server from the Publisher (a remote Distributor). Typically, you would choose to use a remote Distributor if you want to:  
  
-   Offload processing to another computer if you want minimal impact from replication on the Publisher (for example, if the Publisher is an OLTP server).  
  
-   Configure a centralized Distributor for multiple Publishers.  
  
 Remote Distributors are more common in transactional replication than they are in merge replication for two reasons:  
  
-   The Distributor plays a larger role in transactional replication because all replicated transactions are written to and read from the distribution database.  
  
-   Merge replication topologies typically use pull subscriptions, so agents run at each Subscriber, rather than all running at the Distributor. For more information, see [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md). In most cases, you should use a local Distributor for merge replication.  
  
 To configure publishing and distribution, see [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md).  
  
 To modify Publisher and Distributor properties, see [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
## See Also  
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Secure the Distributor](../../relational-databases/replication/security/secure-the-distributor.md)  
  
  
