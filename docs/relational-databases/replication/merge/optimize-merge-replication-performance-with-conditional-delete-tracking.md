---
title: Optimize Performance with Conditional Delete Tracking (Merge)
description: Conditional delete tracking gives SQL Server administrators control over which merge replication deletes propagate. Explore scenarios and configure the option.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "conditional delete tracking [SQL Server replication]"
  - "merge replication [SQL Server replication], conditional delete tracking"
  - "articles [SQL Server replication], conditional delete tracking"
---
# Optimize merge replication performance with conditional delete tracking
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
 By using merge replication, you can specify that replication triggers and system tables don't track deletes for one or more articles. If you select this option for an article, deletes aren't tracked or replicated from the Publisher or any Subscribers. This option supports a number of application scenarios and provides a performance optimization for cases where replicating deletes isn't necessary or desirable. Performance is enhanced in three ways: the system doesn't store metadata for deletes; it doesn't enumerate deletes during synchronization; and it doesn't replicate deletes to, or applies them at, the Subscriber.
  
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
  
## Related content

- [Article Options for Merge Replication](article-options-for-merge-replication.md)
- [Optimize Merge Replication Performance with Download-Only Articles](optimize-merge-replication-performance-with-download-only-articles.md)
