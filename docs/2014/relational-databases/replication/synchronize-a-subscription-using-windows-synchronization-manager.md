---
title: "Synchronize a Subscription Using Windows Synchronization Manager (Windows Synchronization Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "synchronization [SQL Server replication], Windows Synchronization Manager"
  - "Windows Synchronization Manager"
ms.assetid: 80f15dd6-e84d-4f96-9866-5b34ea531f1e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Synchronize a Subscription Using Windows Synchronization Manager (Windows Synchronization Manager)
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Synchronization Manager can only be used to synchronize subscriptions to Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running on the same computer as Synchronization Manager (it can also be used to synchronize offline files and Web pages). To use Synchronization Manager:  
  
1.  Enable the synchronization of pull subscriptions with Windows Synchronization Manager in the **Subscription Properties - \<Subscriber>: \<SubscriptionDatabase>** dialog box. For more information about accessing this dialog box, see [View and Modify Pull Subscription Properties](view-and-modify-pull-subscription-properties.md).  
  
2.  Access Synchronization Manager through the **Start** menu in Windows.  
  
 Synchronization Manager allows you to use the Interactive Resolver for merge subscriptions. Typically, conflicts detected during synchronization are resolved automatically, but if interactive resolution is enabled, conflicts can be resolved by a user during synchronization. If a synchronization is performed outside of Windows Synchronization Manager (as a scheduled synchronization or an on demand synchronization in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or Replication Monitor), conflicts are resolved automatically without user intervention, according to the resolver specified for the article.  
  
> [!NOTE]  
>  Beginning with [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] and [!INCLUDE[wiprlhlong](../../includes/wiprlhlong-md.md)], 64-bit versions of the Windows Synchronization Manager cannot detect 32-bit subscriptions.  
  
### To enable the synchronization of pull subscriptions with Windows Synchronization Manager  
  
1.  On the **General** page of the **Subscription Properties - \<Subscriber>: \<SubscriptionDatabase>** dialog box, select a value of **Enable** for the **Use Windows Synchronization Manager** option.  
  
2.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To synchronize a pull subscription with Synchronization Manager  
  
1.  Launch Synchronization Manager using one of the following methods:  
  
    -   In Internet Explorer, click **Tools**, and then click **Synchronize**.  
  
    -   Click **Start**, point to **Programs** or **All Programs**, point to **Accessories**, and then click **Synchronize**.  
  
    -   Click **Start**, and then click **Run.** In the **Run** dialog box, type `mobsync.exe` in the **Open** field, and then click **OK**.  
  
2.  In the **Items to Synchronize** dialog box, select the subscriptions to synchronize. Subscriptions are listed under the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the computer.  
  
3.  Click **Synchronize**.  
  
### To reinitialize a pull subscription with Synchronization Manager  
  
1.  In the **Items to Synchronize** dialog box, select a subscription, and then click **Properties**.  
  
2.  In the **SQL Server Subscription Properties** dialog box, click **Reinitialize Subscription**.  
  
3.  Click **Yes**.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     The next time the subscription is synchronized, by default a new snapshot is applied to the subscription database. For more information, see [Reinitialize Subscriptions](reinitialize-subscriptions.md).  
  
> [!NOTE]  
>  Merge replication allows any outstanding changes to be uploaded to the Publisher before the snapshot is applied, but this option is not available from Synchronization Manager. To upload changes, synchronize the subscription before reinitializing it.  
  
### To set properties for a pull subscription in Synchronization Manager  
  
1.  In the **Items to Synchronize** dialog box, select a subscription, and then click **Properties**.  
  
2.  View and modify properties on the following tabs:  
  
    -   **Identification**  
  
    -   **Subscriber Login**, **Distributor Login**, and **Publisher Login** (for merge replication only)  
  
    -   **Web Server Information** (for merge subscriptions on Subscribers running SQL Server 2005 or later)  
  
    -   **Other**  
  
     It is recommended to use Windows Authentication for all connections. For information about the permissions required by the Distribution Agent and the Merge Agent, see [Replication Agent Security Model](security/replication-agent-security-model.md).  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To remove a pull subscription from Synchronization Manager  
  
1.  In the **Items to Synchronize** dialog box, select a subscription, and then click **Properties**.  
  
2.  In the **SQL Server Subscription Properties** dialog box, click **Remove Subscription**.  
  
3.  Select an option in the **Remove Subscription** dialog box.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To use the Interactive Resolver  
  
1.  Enable the article and subscription to use interactive resolution. For more information, see [Specify Interactive Conflict Resolution for Merge Articles](publish/specify-merge-replication-properties.md#interactive-conflict-resolution).  
  
2.  After the subscription begins synchronizing in Synchronization Manager, the Interactive Resolver launches automatically if interactive conflict resolution is enabled and there are conflicts for one or more articles. The Interactive Resolver displays conflicts one at a time, with a suggested resolution for each conflict (based on the resolver specified when the publication and subscription were created).  
  
3.  Optionally edit any of the columns displayed in the Interactive Resolver, and then click one of the following buttons to resolve the conflict:  
  
    -   **Accept Suggested**  
  
    -   **Accept Publisher**  
  
    -   **Accept Subscriber**  
  
    -   **Resolve All Automatically** (all current conflicts are resolved without further input)  
  
     The selected row is then applied to the Publisher and/or Subscriber; it is propagated to other nodes in the topology during subsequent synchronizations.  
  
> [!NOTE]  
>  Edits are only applied if they are part of the row that is chosen for resolution. For example, if you make edits under **Publisher**, and then click **Accept Subscriber**, the edits are discarded.  
  
## See Also  
 [Interactive Conflict Resolution](merge/advanced-merge-replication-conflict-interactive-resolution.md)  
  
  
