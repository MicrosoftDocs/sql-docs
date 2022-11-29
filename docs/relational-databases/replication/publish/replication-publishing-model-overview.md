---
title: "Replication Publishing Model Overview | Microsoft Docs"
description: Learn about the replication publishing model in SQL Server, including Publisher, Distributor, Subscribers, publications, articles, and subscriptions.
ms.custom: ""
ms.date: "09/01/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], publishing model"
  - "subscriptions [SQL Server replication], about subscriptions"
  - "articles [SQL Server replication]"
  - "publications [SQL Server replication]"
  - "Publishers [SQL Server replication], about Publishers"
  - "Subscribers [SQL Server replication]"
  - "Distributors [SQL Server replication], about Distributors"
  - "Subscribers [SQL Server replication], about Subscribers"
  - "articles [SQL Server replication], about articles"
  - "publications [SQL Server replication], about publications"
  - "Distributors [SQL Server replication]"
ms.assetid: b9567832-e6a8-45b2-a3ed-ea12aa002f4b
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Replication Publishing Model Overview
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Replication uses a publishing industry metaphor to represent the components in a replication topology, which include Publisher, Distributor, Subscribers, publications, articles, and subscriptions. It is helpful to think of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication in terms of a magazine:  
  
-   A magazine publisher produces one or more publications  
  
-   A publication contains articles  
  
-   The publisher either distributes the magazine directly or uses a distributor  
  
-   Subscribers receive publications to which they have subscribed  
  
 Although the magazine metaphor is useful for understanding replication, it is important to note that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication includes functionality that is not represented in this metaphor, particularly the ability for a Subscriber to make updates and for a Publisher to send out incremental changes to the articles in a publication.  
  
 A *replication topology* defines the relationship between servers and copies of data and clarifies the logic that determines how data flows between servers. There are several replication processes (referred to as *agents*) that are responsible for copying and moving data between the Publisher and Subscribers. The following illustration is an overview of the components and processes involved in replication.  
  
 ![Replication components and data flow](../../../relational-databases/replication/publish/media/replintro1.gif "Replication components and data flow")  
  
## Publisher  
 The Publisher is a database instance that makes data available to other locations through replication. The Publisher can have one or more publications, each defining a logically related set of objects and data to replicate.  
  
## Distributor  
 The Distributor is a database instance that acts as a store for replication specific data associated with one or more Publishers. Each Publisher is associated with a single database (known as a distribution database) at the Distributor. The distribution database stores replication status data, metadata about the publication, and, in some cases, acts as a queue for data moving from the Publisher to the Subscribers. In many cases, a single database server instance acts as both the Publisher and the Distributor. This is known as a *local Distributor*. When the Publisher and the Distributor are configured on separate database server instances, the Distributor is known as a *remote Distributor*.  
  
## Subscribers  
 A Subscriber is a database instance that receives replicated data. A Subscriber can receive data from multiple Publishers and publications. Depending on the type of replication chosen, the Subscriber can also pass data changes back to the Publisher or republish the data to other Subscribers.  
  
## Article  
 An article identifies a database object that is included in a publication. A publication can contain different types of articles, including tables, views, stored procedures, and other objects. When tables are published as articles, filters can be used to restrict the columns and rows of the data sent to Subscribers.  
  
## Publication  
 A publication is a collection of one or more articles from one database. The grouping of multiple articles into a publication makes it easier to specify a logically related set of database objects and data that are replicated as a unit.  
  
## Subscription  
 A subscription is a request for a copy of a publication to be delivered to a Subscriber. The subscription defines what publication will be received, where, and when. There are two types of subscriptions: push and pull. For more information about push and pull subscriptions, see [Subscribe to Publications](../../../relational-databases/replication/subscribe-to-publications.md).  
  
## See Also  
 [Replication Agents Overview](../../../relational-databases/replication/agents/replication-agents-overview.md)   
 [Types of Replication](../../../relational-databases/replication/types-of-replication.md)   
 [Configure Replication for Always On Availability Groups (SQL Server)](../../../database-engine/availability-groups/windows/configure-replication-for-always-on-availability-groups-sql-server.md)   
 [Maintaining an Always On Publication Database (SQL Server)](../../../database-engine/availability-groups/windows/maintaining-an-always-on-publication-database-sql-server.md)  
  
  
