---
title: "Delete a Push Subscription"
description: Learn how to delete a push subscription in SQL Server by using SQL Server Management Studio, Transact-SQL, or Replication Management Objects.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 02/03/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "removing subscriptions"
  - "push subscriptions [SQL Server replication], deleting"
  - "deleting subscriptions"
  - "subscriptions [SQL Server replication], push"
monikerRange: "=azuresqldb-mi-current || =azuresqldb-current || >=sql-server-2017"
---
# Delete a Push Subscription
[!INCLUDE[sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  This topic describes how to delete a push subscription in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  

##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Delete a push subscription at the Publisher (from the **Local Publications** folder in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]) or the Subscriber (from the **Local Subscriptions** folder). Deleting a subscription does not remove objects or data from the subscription; they must be removed manually.  
  
#### To delete a push subscription at the Publisher  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Expand the publication associated with the subscription you want to delete.  
  
4.  Right-click the subscription, and then click **Delete**.  
  
5.  In the confirmation dialog box, select whether to connect to the Subscriber to delete subscription information. If you clear the **Connect to Subscriber** checkbox, you should connect to the Subscriber later to delete the information.  

#### To delete a push subscription at the Subscriber  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Subscriptions** folder.  
  
3.  Right-click the subscription you want to delete, and then click **Delete**.  
  
4.  In the confirmation dialog box, select whether to connect to the Publisher to delete subscription information. If you clear the **Connect to Publisher** check box, you should connect to the Publisher later to delete the information.  

## <a name="known-issue-port"></a> Known issue: Linked server port not updated when recreating subscription

When you delete and recreate a subscription with a nondefault port on the Subscriber, the system reuses the existing linked server but fails to update the port configuration. This can cause replication to fail when attempting to connect to the Subscriber.

**Applies to:** SQL Server (all versions including Linux), Azure SQL Managed Instance, Azure SQL Database (as Subscriber)

### Symptoms

After following these steps, data changes on the Publisher fail to replicate to the Subscriber:

1. Create a subscription with a non-default port specified for the Subscriber (for example, `MySubscriber,1450`)
2. Delete the subscription using `sp_dropsubscription`
3. Recreate the subscription with a different port (for example, `MySubscriber,1455`)
4. Publisher attempts to connect using the original port (1450) instead of the new port (1455)

### Cause

When you delete a subscription using `sp_dropsubscription`, the linked server to the Subscriber isn't deleted. When you recreate the subscription with a different port, the system reuses the existing linked server but doesn't update the port configuration.

### Workaround

Manually delete the linked server to the Subscriber after deleting the subscription and before recreating it:

```sql
-- After running sp_dropsubscription
-- Delete the linked server manually
EXEC sp_dropserver @server = 'MySubscriber', @droplogins = 'droplogins';

-- Now you can recreate the subscription with a different port
-- The system will create a new linked server with the correct port
```

> [!IMPORTANT]
> Ensure no other subscriptions or processes are using the linked server before deleting it.
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Push subscriptions can be deleted programmatically using replication stored procedures. The stored procedures used depend on the type of publication to which the subscription belongs.  
  
#### To delete a push subscription to a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_dropsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscription-transact-sql.md). Specify **\@publication** and **\@subscriber**. Specify a value of **all** for **\@article**. (Optional) If the Distributor cannot be accessed, specify a value of **1** for **\@ignore_distributor** to delete the subscription without removing related objects at the Distributor.  
  
2.  At the Subscriber on the subscription database, execute [sp_subscription_cleanup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-subscription-cleanup-transact-sql.md) to remove replication metadata in the subscription database.  
  
#### To delete a push subscription to a merge publication  
  
1.  At the Publisher, execute [sp_dropmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergesubscription-transact-sql.md), specifying **\@publication**, **\@subscriber** and **\@subscriber_db**. (Optional) If the Distributor cannot be accessed, specify a value of **1** for **\@ignore_distributor** to delete the subscription without removing related objects at the Distributor.  
  
2.  At the Subscriber on the subscription database, execute [sp_mergesubscription_cleanup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-mergesubscription-cleanup-transact-sql.md). Specify **\@publisher**, **\@publisher_db**, and **\@publication**. This removes merge metadata from the subscription database.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 This example deletes a push subscription to a transactional publication.  
  
 :::code language="sql" source="codesnippet/tsql/delete-a-push-subscription_1.sql":::
  
 This example deletes a push subscription to a merge publication.  
  
 :::code language="sql" source="codesnippet/tsql/delete-a-push-subscription_2.sql":::
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 The RMO classes that you use to delete a push subscription depend on the type of publication to which the push subscription is subscribed.  
  
#### To delete a push subscription to a snapshot or transactional publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>, and <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A> properties.  
  
4.  Set the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
5.  Check the <xref:Microsoft.SqlServer.Replication.ReplicationObject.IsExistingObject%2A> property to verify that the subscription exists. If the value of this property is **false**, either the subscription properties in step 2 were defined incorrectly or the subscription does not exist.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.Subscription.Remove%2A> method.  
  
#### To delete a push subscription to a merge publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeSubscription> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A>, <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>, and <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A> properties.  
  
4.  Set the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
5.  Check the <xref:Microsoft.SqlServer.Replication.ReplicationObject.IsExistingObject%2A> property to verify that the subscription exists. If the value of this property is **false**, either the subscription properties in step 2 were defined incorrectly or the subscription does not exist.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.Subscription.Remove%2A> method.  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 You can delete push subscriptions programmatically by using Replication Management Objects (RMO).  
  
 :::code language="csharp" source="../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs" range="rmo_droptranpushsub":::

 :::code language="vb" source="../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb" range="rmo_vb_droptranpushsub":::

## Related content

- [Subscribe to Publications](subscribe-to-publications.md)
- [Replication Security Best Practices](security/replication-security-best-practices.md)
