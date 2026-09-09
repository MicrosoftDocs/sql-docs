---
title: Subscription Expiration and Deactivation
description: Subscription expiration in SQL Server replication happens when Subscribers don't sync in time. Explore retention periods, cleanup jobs, and reinitialization steps.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: concept-article
ms.custom:
  - updatefrequency5
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
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Subscription expiration and deactivation
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Subscriptions can be deactivated or can expire if they are not synchronized within a specified *retention period*. The action that occurs depends on the type of replication and the retention period that is exceeded.  
  
 To set retention periods, see [Set the Expiration Period for Subscriptions](../../relational-databases/replication/publish/set-the-expiration-period-for-subscriptions.md), [Set the Distribution Retention Period for Transactional Publications &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/set-distribution-retention-period-for-transactional-publications.md), and [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md).  
  
## Transactional replication  
 Transactional replication uses the maximum distribution retention period (the `@max_distretention` parameter of [sp_adddistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md)) and the publication retention period (the `@retention` parameter of [sp_addpublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md)):  
  
-   If a subscription isn't synchronized within the maximum distribution retention period (default of 72 hours) and there are changes in the distribution database that aren't delivered to the Subscriber, the subscription is marked deactivated by the **Expired Subscription clean up** job that runs on the Distributor. You must reinitialize the subscription.  
  
-   If a subscription isn't synchronized within the publication retention period (default of 336 hours), the subscription expires and is dropped by the **Expired subscription clean up** job that runs on the Publisher. (Before the fix in [KB4014798](https://support.microsoft.com/topic/kb4014798-update-reduces-the-execution-frequency-of-the-sp-mssubscription-cleanup-stored-procedure-in-sql-server-82f6de06-6ce0-3620-bc79-e954df38a536), the job was named **Distribution cleanup**.) You must recreate and synchronize the subscription.  
  
     If a push subscription expires, it's completely removed, but pull subscriptions aren't. You must clean up pull subscriptions at the Subscriber. For more information, see [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md).  
  
## Merge Replication  
 Merge replication uses the publication retention period (the `@retention` and `@retention_period_unit` parameters of [sp_addmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md)). When a subscription expires, you must reinitialize it because the process removes metadata for the subscription. The **Expired subscription clean up** job that runs on the Publisher drops subscriptions that aren't reinitialized. By default, this job runs daily. It removes all push subscriptions that don't synchronize for double the length of the publication retention period. For example:  
  
-   If a publication has a retention period of 14 days, a subscription can expire if it doesn't synchronize within 14 days.  
  
     If the Publisher is running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version and the agent for the subscription is from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version, a subscription only expires if there are changes to the data in that subscription's partition. For example, suppose a Subscriber receives customer data only for customers in Germany. If the retention period is set to 14 days, the subscription expires on day 14 only if there are changes to the German customer data in the last 14 days.  
  
-   From 14 days to 27 days after the last synchronization, you can reinitialize the subscription.  
  
-   At 28 days after the last synchronization, the **Expired subscription clean up** job drops the subscription. If a push subscription expires, it's completely removed, but pull subscriptions aren't. You must clean up pull subscriptions at the Subscriber. For more information, see [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md).  
  
### Considerations for setting the publication retention period for merge publications  
 Keep the following considerations in mind when setting the retention period for merge publications:  
  
-   The retention period for merge publications includes a 24-hour grace period to accommodate Subscribers in different time zones. For example, if you set a retention period of one day, the actual retention period is 48 hours.  
  
-   Cleanup of merge replication metadata depends on the publication retention period:  
  
    -   Replication can't clean up metadata in the publication and subscription databases until the retention period is reached. Use caution when specifying a high value for the retention period, because it can negatively affect replication performance. Use a lower setting if you can reliably predict that all Subscribers will synchronize regularly within that time period.  
  
    -   You can specify that subscriptions never expire (a value of 0 for `@retention`), but don't use this value because metadata can't be cleaned up.  
  
-   Set the retention period for any republisher to a value equal to or less than the retention period set at the original Publisher. Also, use the same publication retention values for all Publishers and their alternate synchronization partners. Using different values might lead to non-convergence. If you need to change the publication retention value, reinitialize the Subscriber to avoid the non-convergence of data.  
  
-   If you increase the publication retention period after a cleanup and a subscription tries to merge with the Publisher (which already deleted the metadata), the subscription won't expire because of the increased retention value. However, the Publisher doesn't have enough metadata to download changes to the Subscriber, which leads to non-convergence.  
  
## Related content

- [Reinitialize Subscriptions](reinitialize-subscriptions.md)
- [Replication Agent Administration](agents/replication-agent-administration.md)
- [Subscribe to Publications](subscribe-to-publications.md)
