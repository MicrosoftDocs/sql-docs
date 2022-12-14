---
title: "Create a push subscription | Microsoft Docs"
description: Learn how to create a push subscription in SQL Server by using SQL Server Management Studio, Transact-SQL, or Replication Management Objects.
ms.custom: ""
ms.date: "08/25/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "push subscriptions [SQL Server replication], creating"
  - "merge replication subscribing [SQL Server replication], push subscriptions"
  - "subscriptions [SQL Server replication], push"
  - "snapshot replication [SQL Server], subscribing"
  - "transactional replication, subscribing"
ms.assetid: adfbbc61-58d1-4330-9ad6-b14ab1142e2b
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-current||>=sql-server-2016"
---
# Create a push subscription
[!INCLUDE[sql-asdb](../../includes/applies-to-version/sql-asdb.md)]
  This topic describes how to create a push subscription in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO). For information about creating a push subscription for a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber, see [Create a subscription for a non-SQL Server Subscriber](../../relational-databases/replication/create-a-subscription-for-a-non-sql-server-subscriber.md).  

[!INCLUDE[azure-sql-db-replication-supportability-note](../../includes/azure-sql-db-replication-supportability-note.md)]
  
 
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
Create a push subscription at the Publisher or the Subscriber by using the New Subscription Wizard. Follow the pages in the wizard to:  
  
- Specify the Publisher and publication.  
  
- Select where replication agents will run. For a push subscription, select **Run all agents at the Distributor (push subscriptions)** on the **Distribution Agent Location** page or **Merge Agent Location** page, depending on the type of publication.  
  
- Specify Subscribers and subscription databases.  
  
- Specify the logins and passwords used for connections made by replication agents:  
  
  - For subscriptions to snapshot and transactional publications, specify credentials on the **Distribution Agent Security** page.  
  
  - For subscriptions to merge publications, specify credentials on the **Merge Agent Security** page.  
  
    For information about the permissions that each agent requires, see [Replication agent security model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
- Specify a synchronization schedule and when the Subscriber should be initialized.  
  
- Specify additional options for merge publications: subscription type and values for parameterized filtering.  
  
- Specify additional options for transactional publications that allow updating subscriptions. One option is to decide whether Subscribers should commit changes at the Publisher immediately or write them to a queue. Another option is setting up credentials used to connect from the Subscriber to the Publisher.  
  
- Optionally, script the subscription.  
  
#### To create a push subscription from the Publisher  
  
1. Connect to the Publisher in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2. Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3. Right-click the publication for which you want to create one or more subscriptions, and then select **New Subscriptions**.  
  
4. Complete the pages in the New Subscription Wizard.  
  
#### To create a push subscription from the Subscriber  
  
1. Connect to the Subscriber in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2. Expand the **Replication** folder.  
  
3. Right-click the **Local Subscriptions** folder, and then select **New Subscriptions**.  
  
4. On the **Publication** page of the New Subscription Wizard, select **\<Find SQL Server Publisher>** or **\<Find Oracle Publisher>** from the **Publisher** drop-down list.  
  
5. Connect to the Publisher in the **Connect to Server** dialog box.  
  
6. Select a publication on the **Publication** page.  
  
7. Complete the pages in the New Subscription Wizard.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
You can create push subscriptions programmatically by using replication stored procedures. The stored procedures used will depend on the type of publication to which the subscription belongs.  
  
> [!IMPORTANT]
> When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
#### To create a push subscription to a snapshot or transactional publication  
  
1. At the Publisher on the publication database, verify that the publication supports push subscriptions by running [sp_helppublication](../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md).  
  
   - If the value of **allow_push** is **1**, push subscriptions are supported.  
  
   - If the value of **allow_push** is **0**, run [sp_changepublication](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md). Specify **allow_push** for **\@property** and **true** for **\@value**.  
  
2. At the Publisher on the publication database, run [sp_addsubscription](../system-stored-procedures/sp-addsubscription-transact-sql.md). Specify **\@publication**, **\@subscriber**, and **\@destination_db**. Specify a value of **push** for **\@subscription_type**. For information about how to update subscriptions, see [Create an updatable subscription to a transactional publication](publish/create-an-updatable-subscription-to-a-transactional-publication.md).  
  
3. At the Publisher on the publication database, run [sp_addpushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md). Specify the following:  
  
   - The **\@subscriber**, **\@subscriber_db**, and **\@publication** parameters.  
  
   - The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows credentials under which the Distribution Agent at the Distributor runs for **\@job_login** and **\@job_password**.  
  
     > [!NOTE]
     > Connections made through Windows Integrated Authentication always use the Windows credentials specified by **\@job_login** and **\@job_password**. The Distribution Agent always makes the local connection to the Distributor by using Windows Integrated Authentication. By default, the agent will connect to the Subscriber by using Windows Integrated Authentication.  
  
   - (Optional) A value of **0** for **\@subscriber_security_mode** and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login information for **\@subscriber_login** and **\@subscriber_password**. Specify these parameters if you need to use SQL Server Authentication when connecting to the Subscriber.  
  
   - A schedule for the Distribution Agent job for this subscription. For more information, see [Specify synchronization schedules](../../relational-databases/replication/specify-synchronization-schedules.md).  
  
> [!IMPORTANT]
> When you're creating a push subscription at a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before running this stored procedure. For more information, see [Enable encrypted connections to the database engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
#### To create a push subscription to a merge publication  
  
1. At the Publisher on the publication database, verify that the publication supports push subscriptions by running [sp_helpmergepublication](../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md).  
  
   - If the value of **allow_push** is **1**, the publication supports push subscriptions.  
  
   - If the value of **allow_push** is not **1**, run [sp_changemergepublication](../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md). Specify **allow_push** for **\@property** and **true** for **\@value**.  
  
2. At the Publisher on the publication database, run [sp_addmergesubscription](../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md). Specify the following parameters:  
  
   - **\@publication**. This is the name of the publication.  
  
   - **\@subscriber_type**. For a client subscription, specify **local**. For a server subscription, specify **global**.  
  
   - **\@subscription_priority**. For a server subscription, specify a priority for the subscription (**0.00** to **99.99**).  
  
   For more information, see [Advanced merge replication conflict detection and resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md).  
  
3. At the Publisher on the publication database, run [sp_addmergepushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addmergepushsubscription-agent-transact-sql.md). Specify the following:  
  
   - The **\@subscriber**, **\@subscriber_db**, and **\@publication** parameters.  
  
   - The Windows credentials under which the Merge Agent at the Distributor runs for **\@job_login** and **\@job_password**.  
  
     > [!NOTE]
     > Connections made through Windows Integrated Authentication always use the Windows credentials specified by **\@job_login** and **\@job_password**. The Merge Agent always makes the local connection to the Distributor by using Windows Integrated Authentication. By default, the agent will connect to the Subscriber by using Windows Integrated Authentication.  
  
   - (Optional) A value of **0** for **\@subscriber_security_mode** and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login information for **\@subscriber_login** and **\@subscriber_password**. Specify these parameters if you need to use SQL Server Authentication when connecting to the Subscriber.  
  
   - (Optional) A value of **0** for **\@publisher_security_mode** and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login information for **\@publisher_login** and **\@publisher_password**. Specify these values if you need to use SQL Server Authentication when connecting to the Publisher.  
  
   - A schedule for the Merge Agent job for this subscription. For more information, see [Specify synchronization schedules](../../relational-databases/replication/specify-synchronization-schedules.md).  
  
> [!IMPORTANT]
> When you're creating a push subscription at a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before running this stored procedure. For more information, see [Enable encrypted connections to the database engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 The following example creates a push subscription to a transactional publication. Login and password values are supplied at runtime through **sqlcmd** scripting variables.  
  
 [!code-sql[HowTo#sp_addtranpushsubscription_agent](../../relational-databases/replication/codesnippet/tsql/create-a-push-subscription_1.sql)]  
  
 The following example creates a push subscription to a merge publication. Login and password values are supplied at runtime through **sqlcmd** scripting variables.  
  
 [!code-sql[HowTo#sp_addmergepushsubscriptionagent](../../relational-databases/replication/codesnippet/tsql/create-a-push-subscription_2.sql)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects  
 You can create push subscriptions programmatically by using Replication Management Objects (RMO). The RMO classes that you use to create a push subscription depend on the type of publication to which the subscription is created.  
  
> [!IMPORTANT]
> When possible, prompt users to enter security credentials at runtime. If you must store credentials, use the [cryptographic services](/previous-versions/aa719848(v=vs.71)) that the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows .NET Framework provides.  
  
#### To create a push subscription to a snapshot or transactional publication  
  
1. Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2. Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPublication> class by using the Publisher connection from step 1. Specify <xref:Microsoft.SqlServer.Replication.Publication.Name%2A>, <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A>, and <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
3. Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method. If this method returns **false**, either the properties specified in step 2 are incorrect or the publication does not exist on the server.  
  
4. Perform a bitwise logical AND (**&** in Visual C# and **And** in Visual Basic) between the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property and <xref:Microsoft.SqlServer.Replication.PublicationAttributes.AllowPush>. If the result is <xref:Microsoft.SqlServer.Replication.PublicationAttributes.None>, set <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> to the result of a bitwise logical OR (**|** in Visual C# and **Or** in Visual Basic) between <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> and <xref:Microsoft.SqlServer.Replication.PublicationAttributes.AllowPush>. Then, call <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> to enable push subscriptions.  
  
5. If the subscription database does not exist, create it by using the <xref:Microsoft.SqlServer.Management.Smo.Database> class. For more information, see [Creating, altering, and removing databases](../../relational-databases/server-management-objects-smo/tasks/creating-altering-and-removing-databases.md).  
  
6. Create an instance of the <xref:Microsoft.SqlServer.Replication.TransSubscription> class.  
  
7. Set the following subscription properties:  
  
   - The <xref:Microsoft.SqlServer.Management.Common.ServerConnection> to the Publisher created in step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
   - Name of the subscription database for <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A>.  
  
   - Name of the Subscriber for <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>. 
  
   - Name of the publication database for <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A>.  
  
   - Name of the publication for <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>.
  
   - The <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> fields of <xref:Microsoft.SqlServer.Replication.Subscription.SynchronizationAgentProcessSecurity%2A> to provide the credentials for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the Distribution Agent runs at the Distributor. This account is used to make local connections to the Distributor and to make remote connections by using Windows Authentication.  
  
     > [!NOTE]
     > Setting <xref:Microsoft.SqlServer.Replication.Subscription.SynchronizationAgentProcessSecurity%2A> is not required when the subscription is created by a member of the **sysadmin** fixed server role, but we recommend it. In this case, the agent will impersonate the SQL Server Agent account. For more information, see [Replication Agent security model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
   - (Optional) A value of **true** (the default) for <xref:Microsoft.SqlServer.Replication.Subscription.CreateSyncAgentByDefault%2A> to create an agent job that is used to synchronize the subscription. If you specify **false**, the subscription can only be synchronized programmatically.  
  
   - (Optional) Set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> or <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SecureSqlStandardPassword%2A> fields of <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberSecurity%2A> when using SQL Server Authentication to connect to the Subscriber.  
  
8. Call the <xref:Microsoft.SqlServer.Replication.Subscription.Create%2A> method.  
  
> [!IMPORTANT]
> When you're creating a push subscription at a Publisher with a remote Distributor, the values supplied for all properties, including <xref:Microsoft.SqlServer.Replication.Subscription.SynchronizationAgentProcessSecurity%2A>, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before calling the <xref:Microsoft.SqlServer.Replication.Subscription.Create%2A> method. For more information, see [Enable encrypted connections to the database engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
#### To create a push subscription to a merge publication  
  
1. Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2. Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class by using the Publisher connection from step 1. Specify <xref:Microsoft.SqlServer.Replication.Publication.Name%2A>, <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A>, and <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
3. Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method. If this method returns **false**, either the properties specified in step 2 are incorrect or the publication does not exist on the server.  
  
4. Perform a bitwise logical AND (**&** in Visual C# and **And** in Visual Basic) between the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property and <xref:Microsoft.SqlServer.Replication.PublicationAttributes.AllowPush>. If the result is <xref:Microsoft.SqlServer.Replication.PublicationAttributes.None>, set <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> to the result of a bitwise logical OR (**|** in Visual C# and **Or** in Visual Basic) between <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> and <xref:Microsoft.SqlServer.Replication.PublicationAttributes.AllowPush>. Then, call <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> to enable push subscriptions.  
  
5. If the subscription database does not exist, create it by using the <xref:Microsoft.SqlServer.Management.Smo.Database> class. For more information, see [Creating, altering, and removing databases](../../relational-databases/server-management-objects-smo/tasks/creating-altering-and-removing-databases.md).  
  
6. Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeSubscription> class.  
  
7. Set the following subscription properties:  
  
   - The <xref:Microsoft.SqlServer.Management.Common.ServerConnection> to the Publisher created in step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
   - Name of the subscription database for <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A>.  
  
   - Name of the Subscriber for <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>.    
   - Name of the publication database for <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A>.  
  
   - Name of the publication for <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>.    
   - The <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> fields of <xref:Microsoft.SqlServer.Replication.Subscription.SynchronizationAgentProcessSecurity%2A> to provide the credentials for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the Merge Agent runs at the Distributor. This account is used to make local connections to the Distributor and to make remote connections through Windows Authentication.  
  
     > [!NOTE]
     > Setting <xref:Microsoft.SqlServer.Replication.Subscription.SynchronizationAgentProcessSecurity%2A> is not required when the subscription is created by a member of the **sysadmin** fixed server role, but we recommend it. In this case, the agent will impersonate the SQL Server Agent account. For more information, see [Replication Agent security model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
   - (Optional) A value of **true** (the default) for <xref:Microsoft.SqlServer.Replication.Subscription.CreateSyncAgentByDefault%2A> to create an agent job that is used to synchronize the subscription. If you specify **false**, the subscription can only be synchronized programmatically.  
  
   - (Optional) Set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> or <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SecureSqlStandardPassword%2A> fields of <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberSecurity%2A> when using SQL Server Authentication to connect to the Subscriber.  
  
   - (Optional) Set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> or <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SecureSqlStandardPassword%2A> fields of <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherSecurity%2A> when using SQL Server Authentication to connect to the Publisher.  
  
8. Call the <xref:Microsoft.SqlServer.Replication.Subscription.Create%2A> method.  
  
> [!IMPORTANT]  
> When you're creating a push subscription at a Publisher with a remote Distributor, the values supplied for all properties, including <xref:Microsoft.SqlServer.Replication.Subscription.SynchronizationAgentProcessSecurity%2A>, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before calling the <xref:Microsoft.SqlServer.Replication.Subscription.Create%2A> method. For more information, see [Enable encrypted connections to the database engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 This example creates a new push subscription to a transactional publication. The Windows account credentials that you use to run the Distribution Agent job are passed at runtime.  
  
 [!code-cs[HowTo#rmo_CreateTranPushSub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_createtranpushsub)]  
  
 [!code-vb[HowTo#rmo_vb_CreateTranPushSub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_createtranpushsub)]  
  
 This example creates a new push subscription to a merge publication. The Windows account credentials that you use to run the Merge Agent job are passed at runtime.  
  
 [!code-cs[HowTo#rmo_CreateMergePushSub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_createmergepushsub)]  
  
 [!code-vb[HowTo#rmo_vb_CreateMergePushSub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_createmergepushsub)]  
  
## See also  
 [View and modify push subscription properties](../../relational-databases/replication/view-and-modify-push-subscription-properties.md)   
 [Replication security best practices](../../relational-databases/replication/security/replication-security-best-practices.md)   
 [Create a publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [Replication management objects concepts](../../relational-databases/replication/concepts/replication-management-objects-concepts.md)   
 [Synchronize a push subscription](../../relational-databases/replication/synchronize-a-push-subscription.md)   
 [Subscribe to publications](../../relational-databases/replication/subscribe-to-publications.md)   
 [Use sqlcmd with scripting variables](../../ssms/scripting/sqlcmd-use-with-scripting-variables.md)  
  
