---
title: "Add Non-SQL Server Subscriber | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.rep.newsubwizard.addnonsqlsubscriber.f1"
ms.assetid: 21beeaa0-4b9e-48da-be63-1b9ff14e03d2
caps.latest.revision: 11
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Add Non-SQL Server Subscriber
  Replication supports creating push subscriptions to snapshot and transactional publications for Oracle and IBM DB2 Subscribers.  
  
## Options  
 **Type of Subscriber to add**  
 Select an Oracle Subscriber or IBM DB2 Subscriber. For more information about support for these Subscribers, see [Non-SQL Server Subscribers](../../relational-databases/replication/non-sql/non-sql-server-subscribers.md).  
  
 **Data source name**  
 The name used to locate the database on a network. Replication produces a connection string for the database using the data source name, combined with the login, password, and any connection options you specify in the **Distribution Agent Security** page in this wizard.  
  
> [!NOTE]  
>  The data source name and connection string are not validated by [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] until the Distribution Agent attempts to initialize the subscription.  
  
## See Also  
 [Create a Subscription for a Non-SQL Server Subscriber](../../relational-databases/replication/create-a-subscription-for-a-non-sql-server-subscriber.md)   
 [Non-SQL Server Subscribers](../../relational-databases/replication/non-sql/non-sql-server-subscribers.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  