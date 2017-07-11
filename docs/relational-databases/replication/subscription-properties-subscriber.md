---
title: "Subscription Properties - Subscriber | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.rep.newsubwizard.subproperties.subscriber.f1"
helpviewer_keywords: 
  - "Subscription Properties dialog box"
ms.assetid: bef66929-3234-4a45-8ec4-3b271519d07a
caps.latest.revision: 25
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Subscription Properties - Subscriber
  The **Subscription Properties** dialog box at the Subscriber allows you to view and set properties for pull subscriptions.  
  
 Each property in the **Subscription Properties** dialog box includes a description. Click a property to see its description displayed at the bottom of the dialog box. This topic provides additional information on a number of properties. The properties are grouped into the following categories:  
  
-   Properties that apply to all subscriptions.  
  
-   Properties that apply to transactional subscriptions.  
  
-   Properties that apply to merge subscriptions.  
  
 If an option is displayed as read-only, it can only be set when the subscription is created. If you want to set options that are not available in the New Subscription Wizard, create the subscription with stored procedures. For more information, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md) and [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md).  
  
> [!NOTE]  
>  If a Distribution Agent or Merge Agent job has not yet been created for the subscription, many subscription properties are not displayed. To create an agent job for a pull subscription, Execute [sp_addpullsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md) (for a subscription to a snapshot or transactional publication) or [sp_addmergepullsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md) (for a subscription to a merge publication).  
  
## Options for all subscriptions  
 **Initialize published data from a snapshot**  
 Determines whether subscriptions are initialized with a snapshot (the default) or through another method. For more information on initializing subscriptions, see [Initialize a Subscription](../../relational-databases/replication/initialize-a-subscription.md).  
  
 **Snapshot location**  
 Determines the location from which snapshot files are accessed during initialization or reinitialization. The location can be one of the following values:  
  
-   **Default location**: the default location, which is defined when configuring a Distributor. For more information, see [Specify the Default Snapshot Location &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/specify-the-default-snapshot-location-sql-server-management-studio.md).  
  
-   **Alternate folder**: an alternate location, which can be specified in the **Publication Properties** dialog box. For more information, see [Alternate Snapshot Folder Locations](../../relational-databases/replication/alternate-snapshot-folder-locations.md).  
  
-   **Dynamic snapshot folder**: a snapshot location for merge publications that use parameterized row filters. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../../relational-databases/replication/snapshots-for-merge-publications-with-parameterized-filters.md).  
  
-   **FTP folder**: a folder accessible to a File Transfer Protocol (FTP) server. For more information, see [Transfer Snapshots Through FTP](../../relational-databases/replication/transfer-snapshots-through-ftp.md).  
  
 **Snapshot folder**  
 If you select any value other than **Default location** for the **Snapshot Location** option, you must specify a path to the snapshot folder.  
  
 **Use Windows Synchronization Manager**  
 Determines whether this subscription can be synchronized using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Synchronization Manager.  
  
 **Security**  
 Click the **Agent process account** row, and then click the properties button (**...**) to change the account under which the Distribution Agent or Merge Agent runs at the Subscriber. The security options related to connections depend on the type of subscription:  
  
-   For subscriptions to a transactional publication: to change the account under which the Distribution Agent makes connections to the Distributor, click **Distributor Connection**, and then click the properties button (**...**).  
  
-   For immediate updating subscriptions to a transactional publication: in addition to the Distributor connection described above, you can change the method used to propagate changes from the Subscriber to the Publisher: click **Publisher Connection**, and then click the properties button (**...**).  
  
-   For subscriptions to merge publications click **Publisher Connection**, and then click the properties button (**...**).  
  
 For more information about the permissions required for each agent, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
## Options for transactional subscriptions  
 **Updatable subscription**  
 Determines whether Subscriber changes are replicated back to the Publisher. Changes can be replicated using queued updating or immediate updating. The option **Subscriber update method** determines which method to use. For more information, see [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md).  
  
## Options for merge subscriptions  
 **Partition definition (HOST_NAME)**  
 For a publication that uses parameterized filters, merge replication evaluates one of two system functions (or both if the filter references both functions) during synchronization to determine the data that a Subscriber should receive: **SUSER_SNAME()** or **HOST_NAME()**. By default, **HOST_NAME()** returns the name of the computer on which the Merge Agent is running, but you can override this value in the New Subscription Wizard. For more information on parameterized filters and overriding **HOST_NAME()**, see [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
 **Subscription type** and **Priority**  
 Displays whether the subscription is a client or server subscription (this cannot be changed after the subscription has been created). Server subscriptions can republish data to other Subscribers and can be assigned a priority for conflict resolution.  
  
 If you selected a subscription type of server in the New Subscription Wizard, the Subscriber is given a priority that is used during conflict resolution  
  
 **Resolve conflicts interactively**  
 Determines whether to use the Interactive Resolver user interface to resolve conflicts during merge synchronization. This requires a value of **Enable** for **Use Windows Synchronization Manager**. For more information, see [Interactive Conflict Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-interactive-resolution.md).  
  
 **Web Synchronization**  
 **Use Web Synchronization** determines whether to connect to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Information Services (IIS) server to synchronize the subscription. This option is available only if the publication is enabled for Web synchronization. For more information, see [Web Synchronization for Merge Replication](../../relational-databases/replication/web-synchronization-for-merge-replication.md).  
  
 If you select **True** for **Use Web Synchronization**:  
  
-   Enter the full address to the IIS server in **Web server address**.  
  
-   Click the **Web Server Connection** row, and then click the properties button (**...**) to set or change the account under which the Subscriber connects to the IIS server.  
  
-   Change **Web server timeout** if necessary. The timeout is the length of time, in seconds, before a Web synchronization request expires.  
  
 For more information about configuration, see [Configure Web Synchronization](../../relational-databases/replication/configure-web-synchronization.md).  
  
## See Also  
 [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md)   
 [View and Modify Push Subscription Properties](../../relational-databases/replication/view-and-modify-push-subscription-properties.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  