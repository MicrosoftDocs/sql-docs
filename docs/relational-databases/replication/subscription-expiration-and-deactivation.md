---
title: "Subscription Expiration and Deactivation | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Distributors [SQL Server replication], distribution retention period"
  - "subscriptions [SQL Server replication], expiration"
  - "publications [SQL Server replication], publication retention periods"
  - "expiration [SQL Server replication]"
  - "retention periods [SQL Server replication]"
  - "publication retention periods"
  - "distribution retention period"
  - "subscriptions [SQL Server replication], deactivation"
  - "deactivating subscriptions"
ms.assetid: 4d03f5ab-e721-4f56-aebc-60f6a56c1e07
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Subscription Expiration and Deactivation
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Subscriptions can be deactivated or can expire if they are not synchronized within a specified *retention period*. The action that occurs depends on the type of replication and the retention period that is exceeded.  
  
 To set retention periods, see [Set the Expiration Period for Subscriptions](../../relational-databases/replication/publish/set-the-expiration-period-for-subscriptions.md), [Set the Distribution Retention Period for Transactional Publications &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/set-distribution-retention-period-for-transactional-publications.md), and [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md).  
  
## Transactional Replication  
 Transactional replication uses the maximum distribution retention period (the **@max_distretention** parameter of [sp_adddistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md)) and the publication retention period (the **@retention** parameter of [sp_addpublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md)):  
  
-   If a subscription is not synchronized within the maximum distribution retention period (default of 72 hours) and there are changes in the distribution database that have not been delivered to the Subscriber, the subscription will be marked deactivated by the **Distribution clean up** job that runs on the Distributor. The subscription must be reinitialized.  
  
-   If a subscription is not synchronized within the publication retention period (default of 336 hours), the subscription will expire and be dropped by the **Expired subscription clean up** job that runs on the Publisher. The subscription must be recreated and synchronized.  
  
     If a push subscription expires, it is completely removed, but pull subscriptions are not. You must clean up pull subscriptions at the Subscriber. For more information, see [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md).  
  
## Merge Replication  
 Merge replication uses the publication retention period (the **@retention** and **@retention_period_unit** parameters of [sp_addmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md)). When a subscription expires, it must be reinitialized, because metadata for the subscription is removed. Subscriptions that are not reinitialized are dropped by the **Expired subscription clean up** job that runs on the Publisher. By default, this job runs daily; it removes all push subscriptions that have not synchronized for double the length of the publication retention period. For example:  
  
-   If a publication has a retention period of 14 days, a subscription can expire if it has not synchronized within 14 days.  
  
     If the Publisher is running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version and the agent for the subscription is from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version, a subscription only expires if there have been changes to the data in that subscription's partition. For example, suppose a Subscriber receives customer data only for customers in Germany. If the retention period is set to 14 days, the subscription expires on day 14 only if there have been changes to the German customer data in the last 14 days.  
  
-   From 14 days to 27 days after the last synchronization, the subscription can be reinitialized.  
  
-   At 28 days after the last synchronization, the subscription is dropped by the **Expired subscription clean up** job. If a push subscription expires, it is completely removed, but pull subscriptions are not. You must clean up pull subscriptions at the Subscriber. For more information, see [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md).  
  
### Considerations for Setting the Publication Retention Period for Merge Publications  
 Keep the following considerations in mind when setting the retention period for merge publications:  
  
-   The retention period for merge publications has a 24-hour grace period to accommodate Subscribers in different time zones. If, for example, you set a retention period of one day, the actual retention period is 48 hours.  
  
-   Cleanup of merge replication metadata is dependent on the publication retention period:  
  
    -   Replication cannot clean up metadata in the publication and subscription databases until the retention period is reached. Use caution in specifying a high value for the retention period, because it can negatively impact replication performance. It is recommended that you use a lower setting if you can reliably predict that all Subscribers will synchronize regularly within that time period.  
  
    -   It is possible to specify that subscriptions never expire (a value of 0 for **@retention**), but it is strongly recommended that you do not use this value, because metadata cannot be cleaned up.  
  
-   The retention period for any republisher must be set to a value equal to or less than the retention period set at the original Publisher. You should also use the same publication retention values for all Publishers and their alternate synchronization partners. Using different values may lead to non-convergence. If you need to change the publication retention value, reinitialize the Subscriber to avoid the non-convergence of data.  
  
-   If, after a clean up, the publication retention period is increased and a subscription tries to merge with the Publisher (which has already deleted the metadata), the subscription will not expire because of the increased retention value. However, the Publisher does not have enough metadata to download changes to the Subscriber, which leads to non-convergence.  
  
## See Also  
 [Reinitialize Subscriptions](../../relational-databases/replication/reinitialize-subscriptions.md)   
 [Replication Agent Administration](../../relational-databases/replication/agents/replication-agent-administration.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  
