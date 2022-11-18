---
description: "View and Modify Pull Subscription Properties"
title: "View and Modify Pull Subscription Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying subscriptions"
  - "viewing replication properties"
  - "modifying replication properties, pull subscriptions"
  - "pull subscriptions [SQL Server replication], modifying"
  - "subscriptions [SQL Server replication], pull"
  - "pull subscriptions [SQL Server replication], properties"
  - "modifying subscriptions, SQL Server Management Studio"
ms.assetid: 1601e54f-86f0-49e8-b023-87a5d1def033
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# View and Modify Pull Subscription Properties
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  This topic describes how to view and modify pull subscription properties in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 **In This Topic**  
  
-   **To view and modify pull subscription properties, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 View pull subscription properties from the Publisher or the Subscriber in the **Subscription Properties - \<Publisher>: \<PublicationDatabase>** dialog box, which is available from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. More properties are visible from the Subscriber, and properties can be modified at the Subscriber. You can also view properties from the Publisher on the **All Subscriptions** tab, which is available in Replication Monitor. For information about starting Replication Monitor, see [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
#### To view pull subscription properties from the Publisher in Management Studio  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Expand the appropriate publication, right-click a subscription, and then click **Properties**.  
  
4.  View properties, and then click **OK**.  

#### To view and modify pull subscription properties from the Subscriber in Management Studio  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Subscriptions** folder.  
  
3.  Right-click a subscription, and then click **Properties**.  
  
4.  Modify any properties if necessary, and then click **OK**.  
  
#### To view pull subscription properties from the Publisher in Replication Monitor  
  
1.  Expand a Publisher group in the left pane of Replication Monitor, expand a Publisher, and then click a publication.  
  
2.  Click the **All Subscriptions** tab.  
  
3.  Right-click a subscription, and then click **Properties**.  
  
4.  View properties, and then click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Pull subscriptions can be modified and their properties accessed programmatically using replication stored procedures. The stored procedures used depend on the type of publication to which the subscription belongs.  
  
#### To view the properties of a pull subscription to a snapshot or transactional publication  
  
1.  At the Subscriber, execute [sp_helppullsubscription](../../relational-databases/system-stored-procedures/sp-helppullsubscription-transact-sql.md). Specify `@publisher`, `@publisher_db`, and `@publication`. This returns information about the subscription that is stored in system tables at the Subscriber.  
  
2.  At the Subscriber, execute [sp_helpsubscription_properties](../../relational-databases/system-stored-procedures/sp-helpsubscription-properties-transact-sql.md). Specify `@publisher`, `@publisher_db`, `@publication`, and one of the following values for `@publication_type`:  
  
    -   **0** - Subscription belongs to a transactional publication.  
  
    -   **1** - Subscription belongs to a snapshot publication.  
  
3.  At the Publisher, execute [sp_helpsubscription](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md). Specify `@publication` and `@subscriber`.  
  
4.  At the Publisher, execute [sp_helpsubscriberinfo](../../relational-databases/system-stored-procedures/sp-helpsubscriberinfo-transact-sql.md), specifying `@subscriber`. This displays information about the Subscriber.  
  
#### To change the properties of a pull subscription to a snapshot or transactional publication  
  
1.  At the Subscriber, execute [sp_change_subscription_properties](../../relational-databases/system-stored-procedures/sp-change-subscription-properties-transact-sql.md), specifying `@publisher`, `@publisher_db`, `@publication`, a value of either **0** (transactional) or **1** (snapshot) for `@publication_type`, the subscription property being changed as `@property`, and the new value as `@value`.  
  
2.  (Optional) At the Subscriber on the subscription database, execute [sp_changesubscriptiondtsinfo](../../relational-databases/system-stored-procedures/sp-changesubscriptiondtsinfo-transact-sql.md). Specify the ID of the Distribution Agent job for `@jobid`, and the following Data Transformation Services (DTS) package properties:  
  
    -   `@dts_package_name`  
  
    -   `dts_package_password`  
  
    -   `@dts_package_location`  
  
     This changes the DTS package properties of a subscription.  
  
    > [!NOTE]  
    >  The job ID can be obtained by executing [sp_helpsubscription](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md).  
  
#### To view the properties of a pull subscription to a merge publication  
  
1.  At the Subscriber, execute [sp_helpmergepullsubscription](../../relational-databases/system-stored-procedures/sp-helpmergepullsubscription-transact-sql.md). Specify `@publisher`, `@publisher_db`, and `@publication`.  
  
2.  At the Subscriber, execute [sp_helpsubscription_properties](../../relational-databases/system-stored-procedures/sp-helpsubscription-properties-transact-sql.md). Specify `@publisher`, `@publisher_db`, `@publication`, and a value of 2 for `@publication_type`.  
  
3.  At the Publisher, execute [sp_helpmergesubscription](../../relational-databases/system-stored-procedures/sp-helpmergesubscription-transact-sql.md) to display subscription information. To return information on a specific subscription, you must specify `@publication`, `@subscriber`, and a value of **pull** for @subscription_type.  
  
4.  At the Publisher, execute [sp_helpsubscriberinfo](../../relational-databases/system-stored-procedures/sp-helpsubscriberinfo-transact-sql.md), specifying `@subscriber`. This displays information about the Subscriber.  
  
#### To change the properties of a pull subscription to a merge publication  
  
1.  At the Subscriber, execute [sp_changemergepullsubscription](../../relational-databases/system-stored-procedures/sp-changemergepullsubscription-transact-sql.md). Specify `@publication`, `@publisher`, `@publisher_db`, the subscription property being changed as `@property`, and the new value as `@value`.  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 The RMO classes you use to view or modify pull subscription properties depend on the type of publication to which the pull subscription is subscribed.  
  
#### To view or modify properties of a pull subscription to a snapshot or transactional publication  
  
1.  Create a connection to the Subscriber by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPullSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>, and <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A> properties.  
  
4.  Set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the subscription properties in step 3 were defined incorrectly or the subscription does not exist on the server.  
  
6.  (Optional) To change properties, set a new value for one of the <xref:Microsoft.SqlServer.Replication.TransPullSubscription> properties that can be set, and then call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method.  
  
7.  (Optional) To view the new settings, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.Refresh%2A> method to reload the properties for the article.  
  
8.  Close all connections.  
  
#### To view or modify properties of a pull subscription to a merge publication  
  
1.  Create a connection to the Subscriber by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePullSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>, and <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A> properties.  
  
4.  Set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the subscription properties in step 3 were defined incorrectly or the subscription does not exist on the server.  
  
6.  (Optional) To change properties, set a new value for one of the <xref:Microsoft.SqlServer.Replication.MergePullSubscription> properties that can be set, and then call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method.  
  
7.  (Optional) To view the new settings, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.Refresh%2A> method to reload the properties for the article.  
  
8.  Close all connections.  
  
## See Also  
 [View information and perform tasks using Replication Monitor](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  
