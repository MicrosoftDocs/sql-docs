---
title: "Replication Tutorials | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "replication"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
helpviewer_keywords: 
  - "tutorials [SQL Server replication]"
  - "walkthroughs [SQL Server replication]"
  - "replication [SQL Server], tutorials"
ms.assetid: 19fbd10e-5b59-4cd0-a988-52d5d9206242
caps.latest.revision: 13
author: "MashaMSFT"
ms.author: "mathoma"
manager: "craigg"
ms.workload: "On Demand"
---
# Replication Tutorials
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
Replication is a powerf solution for moving data, or subsets of data, between servers. You can replicate data between servers that are fully connected using Transactional Replication. You can also replicate data between servers and clients that are intermittently connected using Merge Replication. Below, you will find tutorials that help prepare your server for replication, and then teach you to configure both Transactional and Merge replication. 
  
In the Replication Tutorials, "Publisher" refers to the server that contains that source data being replicated and "Subscriber" refers to the destination server. The Publisher and Subscriber may share the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but it is not a requirement. For more information, see [Replication Publishing Model Overview](../../relational-databases/replication/publish/replication-publishing-model-overview.md).  

These tutorials uses NODE1\SQL2016 as the Publisher and Distributor, and NODE2\SQL2016 as the Subscriber. 
  
> [!NOTE]  
> Most of the tasks shown in these tutorials can be performed programmatically. For more information, see [Replication Developer Documentation](../../relational-databases/replication/concepts/replication-developer-documentation.md).  
  
## Replication Tutorials  
[Tutorial: Prepare SQL Server For Replication - Publisher, Distributor, Subscriber](../../relational-databases/replication/tutorial-preparing-the-server-for-replication.md)  
Learn how to prepare servers so that replication can be run with least privileges. You must complete this tutorial before the other replication tutorials.  
  
[Tutorial: Configure Replication between Two Fully Connected Servers (Transactional)](../../relational-databases/replication/tutorial-replicating-data-between-continuously-connected-servers.md)  
Learn how to configure Transactional Replication to replicate data between fully connected servers. This tutorial also covers some basic Transactional error troubleshooting methodology. 

  
[Tutorial: Configure Replication between a Server and Mobile Clients (Merge)](../../relational-databases/replication/tutorial-replicating-data-with-mobile-clients.md)  
Learn how to use Merge Replication to exchange data between a server and one or more clients that are only occasionally connected.  
  
## See Also  
[Security and Protection for Replication](../../relational-databases/replication/security/security-and-protection-replication.md) 

[Transactional Replication Overview](https://docs.microsoft.com/en-us/sql/relational-databases/replication/transactional/transactional-replication) 

[Merge Replication Overview](https://docs.microsoft.com/en-us/sql/relational-databases/replication/merge/merge-replication)

  