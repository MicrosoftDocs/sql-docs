---
title: "Snapshot Agent (New Publication Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newpubwizard.configuresnapshotagent.f1"
ms.assetid: 0257d4ee-1f7b-49fd-b4ef-65bfc1ef6951
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Snapshot Agent (New Publication Wizard)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The Snapshot Agent creates files containing the publication schema and data that are used to initialize new subscriptions. By default, the Snapshot Agent runs immediately after the publication is created in the New Publication Wizard. Subsequently, the agent runs according to a schedule you specify. Whether the agent creates new snapshot files each time it runs depends on the type of replication and options chosen. For more information, see [Create and Apply the Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
 For merge publications that use parameterized filters, you must create a snapshot for each partition of data after the publication snapshot has completed. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
## Options  
 **Create a snapshot immediately** (merge replication) or **Create a snapshot immediately and keep the snapshot available to initialize subscriptions** (transactional replication)  
 Select this check box to create a snapshot immediately after the New Publication Wizard is completed. Clear this check box if you plan to change snapshot properties in the **Publication Properties** dialog box before generating a snapshot, or if you will initialize the Subscriber without a snapshot. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
> [!NOTE]  
>  The wizard might prompt for a connection to the Distributor in order to start the appropriate job for the Distribution Agent or Merge Agent.  
  
 **Schedule the Snapshot Agent to run at the following times**  
 Accept the default schedule for running the Snapshot Agent, or click **Change** to specify a schedule.  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)   
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Initialize a Subscription with a Snapshot](../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)  
  
  
