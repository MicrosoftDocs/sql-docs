---
description: "Article Options for Merge Replication"
title: "Article Options for Merge Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "merge replication [SQL Server replication], article options"
  - "articles [SQL Server replication], merge replication options"
ms.assetid: 670abd41-d204-4cd7-a371-7664e603a0ce
author: "MashaMSFT"
ms.author: "mathoma"
---
# Article Options for Merge Replication
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  There are many options for merge table articles that enable you to customize replication behavior to the needs of your applications. By using merge replication, you can do the following:  
  
-   Use row filters, join filters, and column filters. Filtering table articles enables you to create partitions of data to be published. For more information, see [Filter Published Data](../../../relational-databases/replication/publish/filter-published-data.md).  
  
-   Specify whether changes at the Subscriber are uploaded to the Publisher. For applications in which some or all data should be read-only at the Subscriber, download-only articles provide a performance benefit. For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md).  
  
-   Specify that deletes for one or more articles should not be tracked by replication triggers and system tables. This option can be useful in many application scenarios. These include scenarios that use batch deletes that do not need to be replicated. For more information, see [Optimize Merge Replication Performance with Conditional Delete Tracking](../../../relational-databases/replication/merge/optimize-merge-replication-performance-with-conditional-delete-tracking.md).  
  
-   Specify the processing order of articles to make sure that articles are processed in the order required by your application. For more information, see [Specify merge replication options](../../../relational-databases/replication/merge/specify-merge-replication-properties.md).  
  
-   Specify that a set of related records should be processed as a unit (by default, merge replication processes changes to tables on a row-by-row basis). For more information, see [Group Changes to Related Rows with Logical Records](../../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
-   Use conflict detection and resolution for cases in which the same data could be changed at more than one node in a topology. For more information, see [Detect and Resolve Merge Replication Conflicts](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md).  
  
-   Specify schema options, such as whether constraints and triggers are copied to the Subscriber. For more information, see [Specify Schema Options](../../../relational-databases/replication/publish/specify-schema-options.md).  
  
-   Use a business logic handler to respond to many conditions during synchronization. These include data changes, conflicts, and errors. For more information, see [Execute Business Logic During Merge Synchronization](../../../relational-databases/replication/merge/execute-business-logic-during-merge-synchronization.md).  
  
## See Also  
 [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  
