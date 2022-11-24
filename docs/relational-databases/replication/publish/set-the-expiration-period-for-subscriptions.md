---
description: "Set the Expiration Period for Subscriptions"
title: "Set the Expiration Period for Subscriptions | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [SQL Server replication], expiration"
  - "expiration [SQL Server replication]"
  - "retention periods [SQL Server replication]"
  - "deactivating subscriptions"
ms.assetid: 542f0613-5817-42d0-b841-fb2c94010665
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Set the Expiration Period for Subscriptions
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  This topic describes how to set the expiration period for subscriptions in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. The expiration period for subscriptions determines the period of time before a subscription expires and is removed. For more information, see [Subscription Expiration and Deactivation](../../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
-   **To set the expiration period for subscriptions, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   The subscription expiration period is also referred to as the *publication retention period*. Cleanup of merge replication metadata is dependent on this setting:  
  
    -   Replication cannot clean up metadata in the publication and subscription databases until the retention period is reached. Use caution in specifying a high value for the retention period, because it can negatively impact replication performance. It is recommended that you use a lower setting if you can reliably predict that all Subscribers will synchronize regularly within that time period.  
  
         The retention period for merge publications has a 24-hour grace period to accommodate Subscribers in different time zones. If, for example, you set a retention period of one day, the actual retention period is 48 hours.  
  
    -   It is possible to specify that subscriptions never expire, but it is strongly recommended that you do not use this value, because metadata cannot be cleaned up.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Set the expiration period for subscriptions on the **General** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
#### To set the expiration period for subscriptions  
  
1.  In the **Subscription expiration** section on the **General** page of the **Publication Properties - \<Publication>** dialog box, specify whether subscriptions should expire.  
  
2.  If they should expire, specify an expiration time period.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 You can use replication stored procedures to either set this value when a publication is created or modify this value at a later time.  
  
#### To set the expiration period for a subscription to a snapshot or transactional publication  
  
1.  At the Publisher, execute [sp_addpublication](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md). Specify the desired subscription expiration period, in hours, for **\@retention**. The default expiration period is 336 hours. For more information, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
#### To set the expiration period for a subscription to a merge publication  
  
1.  At the Publisher, execute [sp_addmergepublication](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md). Specify the desired value for the subscription expiration period for **\@retention**. Specify the units in which the expiration period is expressed for **\@retention_period_unit**, which can be one of the following:  
  
    -   **1** = week  
  
    -   **2** = month  
  
    -   **3** = year  
  
     The default expiration period is 14 days. For more information, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
#### To change the expiration period for a subscription to a snapshot or transactional publication  
  
1.  At the Publisher, execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md). Specify **retention** for **\@property** and the new subscription expiration period, in hours, for **\@value**.  
  
#### To change the expiration period for a subscription to a merge publication  
  
1.  At the Publisher, execute [sp_helpmergepublication](../../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md), specifying **\@publication** and **\@publisher**. Note the value of **retention_period_unit** in the result set, which can be one of the following:  
  
    -   **0** = day  
  
    -   **1** = week  
  
    -   **2** = month  
  
    -   **3** = year  
  
2.  At the Publisher, execute [sp_changemergepublication](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md). Specify **retention** for **\@property** and the new subscription expiration period, as text based on the retention period unit from step 1, for **\@value**.  
  
3.  (Optional) At the Publisher, execute [sp_changemergepublication](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md). Specify **retention_period_unit** for **\@property** and a new unit for the subscription expiration period for **\@value**.  
  
## See Also  
 [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)   
 [Subscription Expiration and Deactivation](../../../relational-databases/replication/subscription-expiration-and-deactivation.md)  
  
  
