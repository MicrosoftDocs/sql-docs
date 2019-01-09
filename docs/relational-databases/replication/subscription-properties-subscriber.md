---
title: "SQL Server Replication Subscription Properties dialog box | Microsoft Docs"
ms.custom: ""
ms.date: "11/20/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newsubwizard.subproperties.publisher.f1"
  - "sql13.rep.newsubwizard.subproperties.subscriber.f1"
ms.assetid: db2be511-c76e-4f21-8be4-6a8c60a50d30
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Replication Subscription Properties dialog box 
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

### Publisher properties
The **Subscription Properties** dialog box at the Publisher allows you to view and set properties for push subscriptions. You can also view some properties for pull subscriptions, but the **Subscriptions Properties** dialog box at the Subscriber displays additional properties and allows properties to be modified.  
  
 Each property in the **Subscription Properties** dialog box includes a description. Click a property to see its description displayed at the bottom of the dialog box. This topic provides additional information on a number of properties, most of which are displayed at the Publisher only for push subscriptions. The properties are grouped into the following categories:  
  
-   Properties that apply to all subscriptions.    
-   Properties that apply to transactional subscriptions.  
-   Properties that apply to merge subscriptions.  
  
 If an option is displayed as read-only, it can only be set when the subscription is created. If you want to set options that are not available in the New Subscription Wizard, create the subscription with stored procedures. For more information, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md) and [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md).  

### Subscriber properties
The **Subscription Properties** dialog box at the Subscriber allows you to view and set properties for pull subscriptions.  
  
 Each property in the **Subscription Properties** dialog box includes a description. Click a property to see its description displayed at the bottom of the dialog box. This topic provides additional information on a number of properties. The properties are grouped into the following categories:    
-   Properties that apply to all subscriptions.    
-   Properties that apply to transactional subscriptions.   
-   Properties that apply to merge subscriptions.  
  
 If an option is displayed as read-only, it can only be set when the subscription is created. If you want to set options that are not available in the New Subscription Wizard, create the subscription with stored procedures. For more information, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md) and [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md).  
  
> [!NOTE]  
>  If a Distribution Agent or Merge Agent job has not yet been created for the subscription, many subscription properties are not displayed. To create an agent job for a pull subscription, Execute [sp_addpullsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md) (for a subscription to a snapshot or transactional publication) or [sp_addmergepullsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md) (for a subscription to a merge publication).  
  
## Publisher Options for all subscriptions  
 **Security**  
 Click the **Agent process account** row, and then click the properties button (**...**) to change the account under which the Distribution Agent or Merge Agent runs at the Distributor. To change the account under which the Distribution Agent or Merge Agent makes connections to the Subscriber, click **Subscriber connection**, and then click the properties button (**...**).  
  
 For more information about the permissions required for each agent, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
## Publisher Options for transactional subscriptions  
 **Prevent transaction looping**  
 Determines whether the Distribution Agent sends transactions that originated at the Subscriber back to the Subscriber. This option is used for bidirectional transactional replication. For more information, see [Bidirectional Transactional Replication](../../relational-databases/replication/transactional/bidirectional-transactional-replication.md).  
  
 **Updatable subscription**  
 Determines whether Subscriber changes are replicated back to the Publisher. Changes can be replicated using queued updating or immediate updating. The option **Subscriber update method** determines which method to use. For more information, see [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md).  
  
## Publisher Options for merge subscriptions  
 **Partition definition (HOST_NAME)**  
 For a publication that uses parameterized filters, merge replication evaluates one of two system functions (or both if the filter references both functions) during synchronization to determine the data that a Subscriber should receive: **SUSER_SNAME()** or **HOST_NAME()**. By default, **HOST_NAME()** returns the name of the computer on which the Merge Agent is running, but you can override this value in the New Subscription Wizard. For more information on parameterized filters and overriding **HOST_NAME()**, see [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
 **Subscription type** and **Priority**  
 Displays whether the subscription is a client or server subscription (this cannot be changed after the subscription has been created). Server subscriptions can republish data to other Subscribers and can be assigned a priority for conflict resolution.  
  
 If you selected a subscription type of server in the New Subscription Wizard, the Subscriber is given a priority that is used during conflict resolution.  
  
 **Resolve conflicts interactively**  
 Determines whether to use the Interactive Resolver user interface to resolve conflicts during merge synchronization. This requires a value of **Enable** for **Use Windows Synchronization Manager**. For more information, see [Interactive Conflict Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-interactive-resolution.md).  

  
## Subscriber Options for all subscriptions  
 **Initialize published data from a snapshot**  
 Determines whether subscriptions are initialized with a snapshot (the default) or through another method. For more information on initializing subscriptions, see [Initialize a Subscription](../../relational-databases/replication/initialize-a-subscription.md).  
  
 **Snapshot location**  
 Determines the location from which snapshot files are accessed during initialization or reinitialization. The location can be one of the following values:  
  
-   **Default location**: the default location, which is defined when configuring a Distributor. For more information, see [Specify snapshot options](../../relational-databases/replication/snapshot-options.md).  
  
-   **Alternate folder**: an alternate location, which can be specified in the **Publication Properties** dialog box. For more information, see [Specify snapshot options](../../relational-databases/replication/snapshot-options.md).  
  
-   **Dynamic snapshot folder**: a snapshot location for merge publications that use parameterized row filters. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
-   **FTP folder**: a folder accessible to a File Transfer Protocol (FTP) server. For more information, see [Deliver Snapshots Through FTP](../../relational-databases/replication/publish/deliver-a-snapshot-through-ftp.md).  
  
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
  
## Subscriber options for transactional subscriptions  
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

  
  
