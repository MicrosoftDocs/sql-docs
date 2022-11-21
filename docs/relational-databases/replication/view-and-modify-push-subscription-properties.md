---
description: "View and Modify Push Subscription Properties"
title: "View and Modify Push Subscription Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing replication properties"
  - "push subscriptions [SQL Server replication], properties"
  - "subscriptions [SQL Server replication], push"
  - "push subscriptions [SQL Server replication], modifying"
  - "modifying replication properties, push subscriptions"
  - "modifying subscriptions, SQL Server Management Studio"
ms.assetid: 801d2995-7aa5-4626-906e-c8190758ec71
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-current||>=sql-server-2016"
---
# View and Modify Push Subscription Properties
[!INCLUDE[sql-asdb](../../includes/applies-to-version/sql-asdb.md)]
  This topic describes how to view and modify push subscription properties in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  

[!INCLUDE[azure-sql-db-replication-supportability-note](../../includes/azure-sql-db-replication-supportability-note.md)]

  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 View and modify push subscription properties from the Publisher in:  
  
-   The **Subscription Properties - \<Publisher>: \<PublicationDatabase>** dialog box, which is available from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
-   The **All Subscriptions** tab, which is available in Replication Monitor. For information about starting Replication Monitor, see [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
#### To view and modify push subscription properties in Management Studio  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Expand the appropriate publication, right-click a subscription, and then click **Properties**.  
  
4.  Modify any properties if necessary, and then click **OK**.  
  
#### To view and modify push subscription properties in Replication Monitor  
  
1.  Expand a Publisher group in the left pane of Replication Monitor, expand a Publisher, and then click a publication.  
  
2.  Click the **All Subscriptions** tab.  
  
3.  Right-click a subscription, and then click **Properties**.  
  
4.  Modify any properties if necessary, and then click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Push subscriptions can be modified and their properties accessed programmatically using replication stored procedures. The stored procedures used depend on the type of publication to which the subscription belongs.  
  
#### To view the properties of a push subscription to a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_helpsubscription](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md). Specify **\@publication**, **\@subscriber**, and a value of **all** for **\@article**.  
  
2.  At the Publisher on the publication database, execute [sp_helpsubscriberinfo](../../relational-databases/system-stored-procedures/sp-helpsubscriberinfo-transact-sql.md), specifying **\@subscriber**.  
  
#### To change the properties of a push subscription to a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_changesubscriber](../../relational-databases/system-stored-procedures/sp-changesubscriber-transact-sql.md), specifying **\@subscriber** and any parameters for the Subscriber properties being changed.  
  
2.  At the Publisher on the publication database, execute [sp_changesubscription](../../relational-databases/system-stored-procedures/sp-changesubscription-transact-sql.md). Specify **\@publication**, **\@subscriber**, **\@destination_db**, a value of **all** for **\@article**, the subscription property being changed as **\@property**, and the new value as **\@value**. This changes security settings for the push subscription.  
  
3.  (Optional) To change the Data Transformation Services (DTS) package properties of a subscription, execute [sp_changesubscriptiondtsinfo](../../relational-databases/system-stored-procedures/sp-changesubscriptiondtsinfo-transact-sql.md) at the Subscriber on the subscription database. Specify the ID of the Distribution Agent job for **\@jobid** and the following DTS package properties:  
  
    -   **\@dts_package_name**  
  
    -   **\@dts_package_password**  
  
    -   **\@dts_package_location**  
  
     This changes the DTS package properties of a subscription.  
  
    > [!NOTE]  
    >  The job ID can be obtained by executing [sp_helpsubscription](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md).  
  
#### To view the properties of a push subscription to a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_helpmergesubscription](../../relational-databases/system-stored-procedures/sp-helpmergesubscription-transact-sql.md). Specify **\@publication** and **\@subscriber**.  
  
2.  At the Publisher, execute [sp_helpsubscriberinfo](../../relational-databases/system-stored-procedures/sp-helpsubscriberinfo-transact-sql.md), specifying **\@subscriber**.  
  
#### To change the properties of a push subscription to a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_changemergesubscription](../../relational-databases/system-stored-procedures/sp-changemergesubscription-transact-sql.md). Specify **\@publication**, **\@subscriber**, **\@subscriber_db**, the subscription property being changed as **\@property**, and the new value as **\@value**.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 The RMO classes you use to view or modify push subscription properties depend on the type of publication to which the push subscription is subscribed.  
  
#### To view or modify properties of a push subscription to a snapshot or transactional publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>, and <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A> properties.  
  
4.  Set the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property setting.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the subscription properties in step 3 were defined incorrectly or the subscription does not exist.  
  
6.  (Optional) To change properties, set a new value for one of the <xref:Microsoft.SqlServer.Replication.TransSubscription> properties that can be set, and then call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method.  
  
7.  (Optional) To view the new settings, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.Refresh%2A> method to reload the properties for the subscription.  
  
#### To view or modify properties of a push subscription to a merge publication  
  
1.  Create a connection to the Subscriber by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>, and <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A> properties.  
  
4.  Set the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property setting.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the subscription properties in step 3 were defined incorrectly or the subscription does not exist.  
  
6.  (Optional) To change properties, set a new value for one of the <xref:Microsoft.SqlServer.Replication.MergeSubscription> properties that can be set, and then call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method.  
  
7.  (Optional) To view the new settings, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.Refresh%2A> method to reload the properties for the subscription.  
  
## See Also  
 [View information and perform tasks using Replication Monitor](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  
