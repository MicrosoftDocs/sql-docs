---
title: "Article Options for Transactional Replication"
description: "Article Options for Transactional Replication"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "articles [SQL Server replication], transactional replication options"
  - "transactional replication, article options"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Article Options for Transactional Replication
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  There are a number of options for articles in transactional publications. With transactional replication, you can do the following:  
  
-   Specify how changes are propagated from the Publisher to Subscribers. For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).  
  
-   Specify that the execution of a stored procedure be replicated. This is useful in replicating the results of maintenance-oriented stored procedures that affect large amounts of data. For more information, see [Publishing Stored Procedure Execution in Transactional Replication](../../../relational-databases/replication/transactional/publishing-stored-procedure-execution-in-transactional-replication.md).  
  
-   Specify schema options, such as whether constraints and triggers are copied to the Subscriber. For more information, see [Specify Schema Options](../../../relational-databases/replication/publish/specify-schema-options.md).  
  
-   Use row filters and column filters. Filtering table articles enables you to create partitions of data to be published. For more information, see [Filter Published Data](../../../relational-databases/replication/publish/filter-published-data.md).  
  
## Related content

- [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)
