---
description: "Initialize a Subscription Manually"
title: "Initialize a Subscription Manually | Microsoft Docs"
ms.custom: ""
ms.date: "08/25/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "manual subscription initialization [SQL Server replication]"
  - "subscriptions [SQL Server replication], initializing"
  - "initializing subscriptions [SQL Server replication], without snapshots"
ms.assetid: 27a1bc38-e498-4fff-8082-04b52aa4b22c
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Initialize a Subscription Manually
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  This topic describes how to initialize a subscription manually in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. While the initial snapshot is normally used to initialize a subscription, subscriptions to publications can be initialized without using a snapshot, provided that the schema and initial data are already present at the subscriber.  
  

##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If there is activity on a database published using transactional replication between the time data and schema are copied to the Subscriber and the time at which the subscription is manually initialized, changes resulting from this activity might not be replicated to the Subscriber.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Initialize a subscription to a publication manually by copying the schema (and typically data) to the subscription database. The schema and data should match the publication database. Then specify that the subscription does not require schema and data on the **Initialize Subscriptions** page of the New Subscription Wizard. For more information about accessing this wizard, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md) and [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md).  
  
 When you synchronize the subscription for the first time, the objects and metadata required by replication are copied to the subscription database.  
  
#### To initialize a subscription to a publication manually  
  
1.  Ensure that the schema and data are copied to the subscription database.  
  
2.  Clear the **Initialize** check box on the **Initialize Subscriptions** page of the New Subscription Wizard. Do this for each subscription that requires only replication objects and metadata to be copied.  

##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Subscriptions can be initialized manually using replication stored procedures.  
  
#### To manually initialize a pull subscription to a transactional publication  
  
1.  Ensure that the schema and data exist on the subscription database. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
2.  At the Publisher on the publication database, execute [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). Specify **\@publication**, **\@subscriber**, the name of the database at the Subscriber containing the published data for **\@destination_db**, a value of **pull** for **\@subscription_type**, and a value of **replication support only** for **\@sync_type**. For more information, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md).  
  
3.  At the Subscriber, execute [sp_addpullsubscription](../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md). For updating subscriptions, see [Create an Updatable Subscription to a Transactional Publication](./publish/create-an-updatable-subscription-to-a-transactional-publication.md).  
  
4.  At the Subscriber, execute [sp_addpullsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md). For more information, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md).  
  
5.  Start the Distribution Agent to transfer replication objects and download the latest changes from the Publisher. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md).  
  
#### To manually initialize a push subscription to a transactional publication  
  
1.  Ensure that the schema and data exist on the subscription database. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
2.  At the Publisher on the publication database, execute [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). Specify the name of the database at the Subscriber containing the published data for **\@destination_db**, a value of **push** for **\@subscription_type**, and a value of **replication support only** for **\@sync_type**. For updating subscriptions, see [Create an Updatable Subscription to a Transactional Publication](./publish/create-an-updatable-subscription-to-a-transactional-publication.md).  
  
3.  At the Publisher on the publication database, execute [sp_addpushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md). For more information, see [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md).  
  
4.  Start the Distribution Agent to transfer replication objects and download the latest changes from the Publisher. For more information, see [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
#### To manually initialize a pull subscription to a merge publication  
  
1.  Ensure that the schema and data exist on the subscription database. This can be done by restoring a backup of the publication database at the Subscriber.  
  
2.  At the Publisher, execute [sp_addmergesubscription](../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md). Specify **\@publication**, **\@subscriber**, **\@subscriber_db**, and a value of **pull** for **\@subscription_type**. This registers the pull subscription.  
  
3.  At the Subscriber on the database containing the published data, execute [sp_addmergepullsubscription](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql.md). Specify a value of **none** for **\@sync_type**.  
  
4.  At the Subscriber, execute [sp_addmergepullsubscription_agent](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md). For more information, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md).  
  
5.  Start the Merge Agent to transfer replication objects and download the latest changes from the Publisher. For more information, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md).  
  
#### To manually initialize a push subscription to a merge publication  
  
1.  Ensure that the schema and data exist on the subscription database. This can be done by restoring a backup of the publication database at the Subscriber.  
  
2.  At the Publisher on the publication database, execute [sp_addmergesubscription](../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md). Specify the name of the database at the Subscriber containing the published data for **\@subscriber_db**, a value of **push** for **\@subscription_type**, and a value of **none** for **\@sync_type**.  
  
3.  At the Publisher on the publication database, execute [sp_addmergepushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addmergepushsubscription-agent-transact-sql.md). For more information, see [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md).  
  
4.  Start the Merge Agent to transfer replication objects and download the latest changes from the Publisher. For more information, see [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
## See Also  
 [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md)   
 [Back Up and Restore Replicated Databases](../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)  
  
