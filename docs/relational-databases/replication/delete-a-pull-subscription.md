---
description: "Delete a Pull Subscription"
title: "Delete a Pull Subscription | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "removing subscriptions"
  - "deleting subscriptions"
  - "pull subscriptions [SQL Server replication], deleting"
  - "subscriptions [SQL Server replication], pull"
ms.assetid: 997c0b8e-d8d9-4eed-85b1-6baa1f8594ce
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Delete a Pull Subscription
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  This topic describes how to delete a pull subscription in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 **In This Topic**  
  
-   **To delete a pull subscription, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Delete a pull subscription at the Publisher (from the **Local Publications** folder in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]) or the Subscriber (from the **Local Subscriptions** folder). Deleting a subscription does not remove objects or data from the subscription; they must be removed manually.  
  
#### To delete a pull subscription at the Publisher  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Expand the publication associated with the subscription you want to delete.  
  
4.  Right-click the subscription, and then click **Delete**.  
  
5.  In the confirmation dialog box, select whether to connect to the Subscriber to delete subscription information. If you clear the **Connect to Subscriber** check box, you should connect to the Subscriber later to delete the information.  
  
#### To delete a pull subscription at the Subscriber  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Subscriptions** folder.  
  
3.  Right-click the subscription you want to delete, and then click **Delete**.  
  
4.  In the confirmation dialog box, select whether to connect to the Publisher to delete subscription information. If you clear the **Connect to Publisher** check box, you should connect to the Publisher later to delete the information.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Pull subscriptions can be deleted programmatically using replication stored procedures. The stored procedures used will depend on the type of publication to which the subscription belongs.  
  
#### To delete a pull subscription to a snapshot or transactional publication  
  
1.  At the Subscriber on the subscription database, execute [sp_droppullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droppullsubscription-transact-sql.md). Specify **\@publication**, **\@publisher**, and **\@publisher_db**.  
  
2.  At the Publisher on the publication database, execute [sp_dropsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscription-transact-sql.md). Specify **\@publication** and **\@subscriber**. Specify a value of **all** for **\@article**. (Optional) If the Distributor cannot be accessed, specify a value of **1** for **\@ignore_distributor** to delete the subscription without removing related objects at the Distributor.  
  
#### To delete a pull subscription to a merge publication  
  
1.  At the Subscriber on the subscription database, execute [sp_dropmergepullsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergepullsubscription-transact-sql.md). Specify **\@publication**, **\@publisher**, and **\@publisher_db**.  
  
2.  At the Publisher on the publication database, execute [sp_dropmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergesubscription-transact-sql.md). Specify **\@publication**, **\@subscriber**, and **\@subscriber_db**. Specify a value of **pull** for **\@subscription_type**. (Optional) If the Distributor cannot be accessed, specify a value of **1** for **\@ignore_distributor** to delete the subscription without removing related objects at the Distributor.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 The following example deletes a pull subscription to a transactional publication. The first batch is executed at the Subscriber and the second is executed at the Publisher.  
  
 [!code-sql[HowTo#sp_droptranpullsubscription](../../relational-databases/replication/codesnippet/tsql/delete-a-pull-subscription_1.sql)]  
  
 [!code-sql[HowTo#sp_droptransubscription](../../relational-databases/replication/codesnippet/tsql/delete-a-pull-subscription_2.sql)]  
  
 The following example deletes a pull subscription to a merge publication. The first batch is executed at the Subscriber and the second is executed at the Publisher.  
  
 [!code-sql[HowTo#sp_dropmergepullsubscription](../../relational-databases/replication/codesnippet/tsql/delete-a-pull-subscription_3.sql)]  
  
 [!code-sql[HowTo#sp_dropmergesubscription](../../relational-databases/replication/codesnippet/tsql/delete-a-pull-subscription_4.sql)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 You can delete pull subscriptions programmatically by using Replication Management Objects (RMO). The RMO classes that you use to delete a pull subscription depend on the type of publication to which the pull subscription is subscribed.  
  
#### To delete a pull subscription to a snapshot or transactional publication  
  
1.  Create connections to both the Subscriber and Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> Class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPullSubscription> class, and set the <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>, and <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A> properties. Use the Subscriber connection from step 1 to set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
3.  Check the <xref:Microsoft.SqlServer.Replication.ReplicationObject.IsExistingObject%2A> property to verify that the subscription exists. If the value of this property is **false**, either the subscription properties in step 2 were defined incorrectly or the subscription does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.PullSubscription.Remove%2A> method.  
  
5.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPublication> class by using the Publisher connection from step 1. Specify <xref:Microsoft.SqlServer.Replication.Publication.Name%2A>, <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> and <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method. If this method returns **false**, either the properties specified in step 5 are incorrect or the publication does not exist on the server.  
  
7.  Call the <xref:Microsoft.SqlServer.Replication.TransPublication.RemovePullSubscription%2A> method. Specify the name of the Subscriber and the subscription database for the *subscriber* and *subscriberDB* parameters.  
  
#### To delete a pull subscription to a merge publication  
  
1.  Create connections to both the Subscriber and Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> Class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePullSubscription> class, and set the <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>, <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>, and <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A> properties. Use the connection from step 1 to set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
3.  Check the <xref:Microsoft.SqlServer.Replication.ReplicationObject.IsExistingObject%2A> property to verify that the subscription exists. If the value of this property is **false**, either the subscription properties in step 2 were defined incorrectly or the subscription does not exist.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.PullSubscription.Remove%2A> method.  
  
5.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class by using the Publisher connection from step 1. Specify <xref:Microsoft.SqlServer.Replication.Publication.Name%2A>, <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> and <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method. If this method returns **false**, either the properties specified in step 5 are incorrect or the publication does not exist on the server.  
  
7.  Call the <xref:Microsoft.SqlServer.Replication.MergePublication.RemovePullSubscription%2A> method. Specify the name of the Subscriber and the subscription database for the *subscriber* and *subscriberDB* parameters.  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 This example deletes a pull subscription to a transactional publication and removes the subscription registration at the Publisher.  
  
 [!code-cs[HowTo#rmo_DropTranPullSub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_droptranpullsub)]  
  
 [!code-vb[HowTo#rmo_vb_DropTranPullSub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_droptranpullsub)]  
  
 This example deletes a pull subscription to a merge publication and removes the subscription registration at the Publisher.  
  
 [!code-cs[HowTo#rmo_DropMergePullSub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_dropmergepullsub)]  
  
 [!code-vb[HowTo#rmo_vb_DropMergePullSub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_dropmergepullsub)]  
  
## See Also  
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)  
  
  
