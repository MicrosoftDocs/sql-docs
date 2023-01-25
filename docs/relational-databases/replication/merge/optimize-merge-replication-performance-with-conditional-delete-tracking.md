---
title: "Optimize performance with conditional delete tracking (Merge)"
description: Learn how to optimize Merge Replication performance by using conditional delete tracking for SQL Server. 
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "conditional delete tracking [SQL Server replication]"
  - "merge replication [SQL Server replication], conditional delete tracking"
  - "articles [SQL Server replication], conditional delete tracking"
ms.assetid: 58f120a3-ea3a-4e97-93f0-0eb4e580ecf2
author: "MashaMSFT"
ms.author: "mathoma"
---
# Optimize Merge Replication Performance with Conditional Delete Tracking
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
 With merge replication you can specify that deletes for one or more articles should not be tracked by replication triggers and system tables. If you specify this option for an article, deletes are not tracked or replicated from the Publisher or any Subscribers. This option is available to support a number of application scenarios and to provide a performance optimization for cases in which the replication of deletes is not necessary or desirable. Performance is enhanced in three ways: metadata for deletes is not stored; deletes are not enumerated during synchronization; deletes are not replicated to and applied at the Subscriber.  
  
> [!NOTE]  
>  To use download-only articles, the compatibility level of the publication must be at least 90RTM.  
  
 The option can be specified when a publication is created or it can be toggled on and off if an application requires that some deletes be replicated and that others not be replicated, such as batch deletes. The following examples illustrate ways in which this option might be used in an application:  
  
-   An application for a mobile sales force typically has tables such as **SalesOrderHeader**, **SalesOrderDetail** and **Product**. Orders are entered at the Subscriber and then replicated to the Publisher, which often supplies data to an order fulfillment system. Many mobile workers use handheld devices which have limited storage: after the order is received at the Publisher, it can be deleted at the Subscriber. The delete is not propagated to the Publisher, because the order is still active in the system.  
  
     In this scenario, deletes would not be tracked for the **SalesOrderHeader** and **SalesOrderDetail** tables. Deletes would be tracked for the **Product** table, because if a product is deleted at the Publisher, the delete should be sent to the Subscriber to keep the product list up to date.  
  
-   An application could store historical data in a table such as **TransactionHistory**, which is periodically purged of records older than a year. The table could be filtered such that Subscribers only receive data on transactions within the current month. Monthly batch deletes at the Publisher that purge older data are not relevant to Subscribers, but they would still be tracked and enumerated by default.  
  
     In this scenario, before the batch processing occurred, activity could be stopped on the system, and the application could disable the tracking of deletes. After the processing has finished, tracking could again be enabled.  
  
> [!IMPORTANT]  
>  If other activity continues at the Publisher, you must ensure that deletes that should be propagated to Subscribers do not occur while delete tracking is disabled.  
  
 **To specify that deletes should not be tracked**  
  
-   Replication [!INCLUDE[tsql](../../../includes/tsql-md.md)] programming: [Specify Merge Replication properties](../../../relational-databases/replication/merge/specify-merge-replication-properties.md)  
  
## See Also  
 [Article Options for Merge Replication](../../../relational-databases/replication/merge/article-options-for-merge-replication.md)   
 [Optimize Merge Replication Performance with Download-Only Articles](../../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md)  
  
  
