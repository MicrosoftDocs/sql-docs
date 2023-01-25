---
description: "Initialize Subscriptions"
title: "Initialize Subscriptions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newsubwizard.initializesubscriptions.f1"
ms.assetid: 7b170e4e-470d-4828-a9ed-7435b0b03514
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Initialize Subscriptions
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Subscribers must be initialized before they can begin receiving replicated data. An initial dataset is not required, but the Subscriber must at least have the schema for each replicated object and any metadata tables and procedures required by replication.  
  
## Options  
 **Subscription properties**  
 Select the check box in the **Initialize** column for each Subscriber that requires an initial data set. If the check box is cleared, only the replication metadata and procedures will be initialized. For more information about initializing a subscription without a snapshot, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
 Select **Immediately** from the drop-down list box in the **Initialize When** column to have the Merge Agent or Distribution Agent transfer snapshot files to the Subscriber after this wizard is completed. Select **At first synchronization** to have the agent transfer the files the next time it is scheduled to run. The **Immediately** option is not available for pull subscriptions to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. The Merge Agent and Distribution Agent do not run on instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]; therefore the subscription must be initialized through another method.  
  
> [!NOTE]  
>  The wizard might prompt for a connection to the Distributor in order to start the appropriate job for the Distribution Agent or Merge Agent.  
  
## See Also  
 [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md)   
 [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md)   
 [Initialize a Subscription](../../relational-databases/replication/initialize-a-subscription.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  
