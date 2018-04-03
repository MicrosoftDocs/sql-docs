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
Replication includes tutorials that you can use to learn how to set up and run replication topologies using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
In the replication tutorials, "Publisher" refers to the server that contains that source data being replicated and "Subscriber" refers to the destination server. The Publisher and Subscriber may share the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but it is not a requirement. For more information, see [Replication Publishing Model Overview](../../relational-databases/replication/publish/replication-publishing-model-overview.md).  
  
> [!NOTE]  
> Most of the tasks shown in these tutorials can be performed programmatically. For more information, see [Replication Developer Documentation](../../relational-databases/replication/concepts/replication-developer-documentation.md).  
  
## Replication Tutorials  
[Tutorial: Prepare SQL Server Publisher and Distributor for Replication](../../relational-databases/replication/tutorial-preparing-the-server-for-replication.md)  
Learn how to prepare servers so that replication can be run with least privileges. You must complete this tutorial before the other replication tutorials.  
  
[Tutorial: Configure Publisher and Subscriber for Transactional Replication](../../relational-databases/replication/tutorial-replicating-data-between-continuously-connected-servers.md)  
Learn how to configure Transactional Replication to replicate data between fully connected servers. This section also covers some basic Transactional error troubleshooting methodology. 

  
[Tutorial: Configure Publisher and Subscriber for Merge Replication](../../relational-databases/replication/tutorial-replicating-data-with-mobile-clients.md)  
Learn how to use Merge Replication to exchange data between a server and one or more clients that are only occasionally connected.  
  
## See Also  
[Security and Protection &#40;Replication&#41;](../../relational-databases/replication/security/security-and-protection-replication.md)  
  
  
  
# [Prepare the SQL Server Publisher and Distributor for Replication](tutorial-preparing-the-server-for-replication.md)  
# [Configure Publisher and Subscriber for Transactional Replication](tutorial-replicating-data-between-continuously-connected-servers.md)  
# [Configure Publisher and Subscriber for Merge Replication](tutorial-replicating-data-with-mobile-clients.md)  