---
title: "Switch between modes (Updatable Transactional)"
description: Describes how to switch between update modes for an Updatable Transactional Publication by using SQL Server Management Studio (SSMS) or Transact-SQL (T-SQL).
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "transactional replication, updatable subscriptions"
  - "updatable subscriptions, update modes"
  - "subscriptions [SQL Server replication], updatable"
ms.assetid: ab5ebab1-7ee4-41f4-999b-b4f0c420c921
author: "MashaMSFT"
ms.author: "mathoma"
---
# Switch Between Update Modes for an Updatable Transactional Subscription
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to switch between update modes for an updatable transaction subscription in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. Specify the mode for updatable subscriptions using the New Subscription Wizard. For information about setting the mode when using this wizard, see [View and Modify Pull Subscription Properties](../../../relational-databases/replication/view-and-modify-pull-subscription-properties.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
-   **To switch between update modes for an updatable transactional subscription, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   You can fail over from immediate to queued updating at any time. After you do, however, you cannot return to immediate updating until the Subscriber and Publisher are connected and the Queue Reader Agent has applied all pending messages in the queue to the Publisher.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   When an updating subscription to a transactional publication supports failover from one updating mode to another, you can programmatically switch update modes to handle situations when connectivity changes for a short period of time. The update mode can be set programmatically and on demand using replication stored procedures. For more information, see [Updatable Subscriptions for Transactional Replication](../../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
> [!NOTE]  
>  To change the update mode after the subscription is created, the **update_mode** property must be set to **failover** (which allows a switch from immediate updating to queued updating) or **queued failover** (which allows a switch from queued updating to immediate updating) when the subscription is created. These properties are set automatically in the New Subscription Wizard.  
  
#### To set the updating mode for a push subscription  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Subscriptions** folder.  
  
3.  Right-click the subscription for which you want to set the update mode, and then click **Set Update Method**.  
  
4.  In the **Set Update Method - \<Subscriber>: \<SubscriptionDatabase>** dialog box, select **Immediate updating** or **Queued updating**.  
  
5.  Select **OK**.

#### To set the updating mode for a pull subscription  
  
1.  In the **Subscription Properties - \<Publisher>: \<PublicationDatabase>** dialog box, select a value of **Immediately replicate changes** or **Queue changes** for the **Subscriber update method** option.  
  
2.  Select **OK**.
  
 For more information about accessing the **Subscription Properties - \<Publisher>: \<PublicationDatabase>** dialog box, see [View and Modify Pull Subscription Properties](../../../relational-databases/replication/view-and-modify-pull-subscription-properties.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To switch between update modes  
  
1.  Verify that the subscription supports failover by executing [sp_helppullsubscription](../../../relational-databases/system-stored-procedures/sp-helppullsubscription-transact-sql.md) for a pull subscription or [sp_helpsubscription](../../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md) for a push subscription. If the value of **update mode** in the result set is **3** or **4**, failover is supported.  
  
2.  At the Subscriber on the subscription database, execute [sp_setreplfailovermode](../../../relational-databases/system-stored-procedures/sp-setreplfailovermode-transact-sql.md). Specify `@publisher`, `@publisher_db`, `@publication`, and one of the following values for `@failover_mode`:  
  
    -   **queued** - fail over to queued updating when connectivity has been temporarily lost.  
  
    -   **immediate** - fail over to immediate updating when connectivity has been restored.  
  
## See Also  
 [Updatable Subscriptions for Transactional Replication](../../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)  
  
  
