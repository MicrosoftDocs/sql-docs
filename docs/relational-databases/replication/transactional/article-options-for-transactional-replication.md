---
title: "Article Options for Transactional Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "articles [SQL Server replication], transactional replication options"
  - "transactional replication, article options"
ms.assetid: 3469b185-0ea5-4690-a71c-717230d886b6
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Article Options for Transactional Replication
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  There are a number of options for articles in transactional publications. With transactional replication, you can do the following:  
  
-   Specify how changes are propagated from the Publisher to Subscribers. For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).  
  
-   Specify that the execution of a stored procedure be replicated. This is useful in replicating the results of maintenance-oriented stored procedures that affect large amounts of data. For more information, see [Publishing Stored Procedure Execution in Transactional Replication](../../../relational-databases/replication/transactional/publishing-stored-procedure-execution-in-transactional-replication.md).  
  
-   Specify schema options, such as whether constraints and triggers are copied to the Subscriber. For more information, see [Specify Schema Options](../../../relational-databases/replication/publish/specify-schema-options.md).  
  
-   Use row filters and column filters. Filtering table articles enables you to create partitions of data to be published. For more information, see [Filter Published Data](../../../relational-databases/replication/publish/filter-published-data.md).  
  
## See Also  
 [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  
