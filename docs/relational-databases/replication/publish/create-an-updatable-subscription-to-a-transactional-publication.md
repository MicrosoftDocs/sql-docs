---
title: "Create an Updatable Subscription to a Transactional Publication | Microsoft Docs"
ms.custom: ""
ms.date: "07/21/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "updatable transactional subscriptions"
  - "updateable transactional subscriptions, SSMS"
ms.assetid: f9ef89ed-36f6-431b-8843-25d445ec137f
caps.latest.revision: 51
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Create an Updatable Subscription to a Transactional Publication

> [!NOTE]  
>  This feature remains supported in versions of [!INCLUDE[ssNoVersion_md](../../../includes/ssnoversion-md.md)] from 2012 through 2016.  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
 
Configure updatable subscriptions on the **Updatable Subscriptions** page of the **New Subscription Wizard**. This page is only available if you have enabled a transactional publication for updatable subscriptions. For more information about enabling updatable subscriptions, see [Enable Updating Subscriptions for Transactional Publications](../../../relational-databases/replication/publish/enable-updating-subscriptions-for-transactional-publications.md).   
  
## To configure an updatable subscription from the Publisher  

1. Connect to the Publisher in Microsoft SQL Server Management Studio, and then expand the server node.

2. Expand the **Replication** folder, and then expand the **Local Publications** folder.

3. Right-click a transactional publication enabled for updating subscriptions, and then click **New Subscriptions**.

4. Follow pages in the wizard to specify options for the subscription, such as where the Distribution Agent should run.

5. On the **Updatable Subscriptions** page of the **New Subscription Wizard**, ensure **Replicate** is selected.

6. Select an option from the **Commit at Publisher** drop-down list:

    * To use immediate updating subscriptions, select **Simultaneously commit changes**. If you select this option, and the publication allows queued updating subscriptions (the default for publications created with the New Publication Wizard), the subscription property **update_mode** is set to **failover**. This mode allows you to switch to queued updating later if necessary.

    * To use queued updating subscriptions, select **Queue changes and commit when possible**. If you select this option, and the publication allows immediate updating subscriptions (the default for publications created with the New Publication Wizard), and the Subscriber is running SQL Server 2005 or a later version, the subscription property **update_mode** is set to queued failover. This mode allows you to switch to immediate updating later if necessary.

    For information about switching update modes, see [Switch Between Update Modes for an Updatable Transactional Subscription](../../../relational-databases/replication/administration/switch-between-update-modes-for-an-updatable-transactional-subscription.md).

7. The **Login for Updatable Subscriptions** page is displayed for subscriptions that use immediate updating or have **update_mode** set to **queued failover**. On the **Login for Updatable Subscriptions** page, specify a linked server over which connections to the Publisher are made for immediate updating subscriptions. Connections are used by the triggers that fire at the Subscriber and propagate changes to the Publisher. Select one of the following options:

    * **Create a linked server that connects using SQL Server Authentication.** Select this option if you have not defined a remote server or linked server between the Subscriber and the Publisher. Replication creates a linked server for you. The account you specify must already exist at the Publisher.

    * **Use a linked server or remote server that you have already defined.** Select this option if you have defined a remote server or linked server between the Subscriber and the Publisher using [sp_addserver (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md), [sp_addlinkedserver (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md), SQL Server Management Studio, or another method.

    For information about the permissions required by the linked server account, see the **Queued Updating Subscriptions** of [enter link description here](../../../relational-databases/replication/security/secure-the-subscriber.md).

8. Complete the wizard.

## To configure an updatable subscription from the Subscriber


1. Connect to the Subscriber in SQL Server Management Studio, and then expand the server node.

2. Expand the **Replication** folder.

3. Right-click the **Local Subscriptions** folder, and then click **New Subscriptions**.

4. On the **Publication** page of the **New Subscription Wizard**, select **Find SQL Server Publisher** from the **Publisher** drop-down list.

5. Connect to the Publisher in the **Connect to Server** dialog box.

6. Select a transactional publication enabled for updating subscriptions on the **Publication** page.

7. Follow pages in the wizard to specify options for the subscription, such as where the Distribution Agent should run.

8. On the **Updatable Subscriptions** page of the New Subscription Wizard, ensure **Replicate** is selected.

9. Select an option from the **Commit at Publisher** drop-down list:

    * To use immediate updating subscriptions, select **Simultaneously commit changes**. If you select this option, and the publication allows queued updating subscriptions (the default for publications created with the New Publication Wizard), the subscription property **update_mode** is set to **failover**. This mode allows you to switch to queued updating later if necessary.

    * To use queued updating subscriptions, select **Queue changes and commit when possible**. If you select this option, and the publication allows immediate updating subscriptions (the default for publications created with the New Publication Wizard), and the Subscriber is running SQL Server 2005 or a later version, the subscription property **update_mode** is set to queued **failover**. This mode allows you to switch to immediate updating later if necessary.

    For information about switching update modes, see [Switch Between Update Modes for an Updatable Transactional Subscription](../../../relational-databases/replication/administration/switch-between-update-modes-for-an-updatable-transactional-subscription.md).

10. The **Login for Updatable Subscriptions** page is displayed for subscriptions that use immediate updating or have **update_mode** set to queued **failover**. On the **Login for Updatable Subscriptions** page, specify a linked server over which connections to the Publisher are made for immediate updating subscriptions. Connections are used by the triggers that fire at the Subscriber and propagate changes to the Publisher. Select one of the following options:

    * **Create a linked server that connects using SQL Server Authentication.** Select this option if you have not defined a remote server or linked server between the Subscriber and the Publisher. Replication creates a linked server for you. The account you specify must already exist at the Publisher.

    * **Use a linked server or remote server that you have already defined.** Select this option if you have defined a remote server or linked server between the Subscriber and the Publisher using [sp_addserver (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md), [sp_addlinkedserver (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md), SQL Server Management Studio, or another method.

    For information about the permissions required by the linked server account, see the **Queued Updating Subscriptions** of [enter link description here](../../../relational-databases/replication/security/secure-the-subscriber.md).

11. Complete the wizard.

## See Also

[Updatable Subscriptions for Transactional Replication](../../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)

[Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md)

[Create an Updatable Subscription to a Transactional Publication Using Transact-SQL](../../../relational-databases/replication/publish/create-updatable-subscription-to-transactional-publication.md) 

