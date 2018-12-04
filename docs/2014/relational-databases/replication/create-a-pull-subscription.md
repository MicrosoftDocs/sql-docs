---
title: "Create a Pull Subscription | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "pull subscriptions [SQL Server replication], creating"
  - "merge replication subscribing [SQL Server replication], pull subscriptions"
  - "subscriptions [SQL Server replication], pull"
  - "snapshot replication [SQL Server], subscribing"
  - "transactional replication, subscribing"
ms.assetid: 41d1886d-59c9-41fc-9bd6-a59b40e0af6e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Create a Pull Subscription
  This topic describes how create a pull subscription in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 Setting up a pull subscription for P2P replication is possible by script, but is not available through the wizard.  
  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Create a pull subscription at the Publisher or the Subscriber using the New Subscription Wizard. Follow the pages in the wizard to:  
  
-   Specify the Publisher and publication.  
  
-   Select where replication agents will run. For a pull subscription, select **Run each agent at its Subscriber (pull subscriptions)** on the **Distribution Agent Location** page or **Merge Agent Location** page, depending on the type of publication.  
  
-   Specify Subscribers and subscription databases.  
  
-   Specify the logins and passwords used for connections made by replication agents:  
  
    -   For subscriptions to snapshot and transactional publications, specify credentials on the **Distribution Agent Security** page.  
  
    -   For subscriptions to merge publications, specify credentials on the **Merge Agent Security** page.  
  
     For information about the permissions required by each agent, see [Replication Agent Security Model](security/replication-agent-security-model.md).  
  
-   Specify a synchronization schedule and when the Subscriber should be initialized.  
  
-   Specify additional options for merge publications: subscription type; values for parameterized filtering; and information for synchronization through HTTPS if the publication is enabled for Web synchronization.  
  
-   Specify additional options for transactional publications that allow updating subscriptions: whether Subscribers should commit changes at the Publisher immediately or write them to a queue; credentials used to connect from the Subscriber to the Publisher.  
  
-   Optionally script the subscription.  
  
#### To create a pull subscription from the Publisher  
  
1.  Connect to the Publisher in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Right-click the publication for which you want to create one or more subscriptions, and then click **New Subscriptions**.  
  
4.  Complete the pages in the New Subscription Wizard.  
  
#### To create a pull subscription from the Subscriber  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder.  
  
3.  Right-click the **Local Subscriptions** folder, and then click **New Subscriptions**.  
  
4.  On the **Publication** page of the New Subscription Wizard, select **\<Find SQL Server Publisher>** or **\<Find Oracle Publisher>** from the **Publisher** drop-down list.  
  
5.  Connect to the Publisher in the **Connect to Server** dialog box.  
  
6.  Select a publication on the **Publication** page.  
  
7.  Complete the pages in the New Subscription Wizard.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Pull subscriptions can be created programmatically using replication stored procedures. The stored procedures used will depend on the type of publication to which the subscription belongs.  
  
#### To create a pull subscription to a snapshot or transactional publication  
  
1.  At the Publisher, verify that the publication supports pull subscriptions by executing [sp_helppublication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-helppublication-transact-sql).  
  
    -   If the value of **allow_pull** in the result set is **1**, then the publication supports pull subscriptions.  
  
    -   If the value of **allow_pull** is **0**, execute [sp_changepublication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-changepublication-transact-sql), specifying **allow_pull** for **@property** and `true` for **@value**.  
  
2.  At the Subscriber, execute [sp_addpullsubscription &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql). Specify **@publisher** and **@publication**. For information about updating subscriptions, see [Create an Updatable Subscription to a Transactional Publication](publish/create-an-updatable-subscription-to-a-transactional-publication.md).  
  
3.  At the Subscriber, execute [sp_addpullsubscription_agent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql). Specify the following:  
  
    -   The **@publisher**, **@publisher_db**, and **@publication** parameters.  
  
    -   The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows credentials under which the Distribution Agent at the Subscriber runs for **@job_login** and **@job_password**.  
  
        > [!NOTE]  
        >  Connections made using Windows Integrated Authentication always use the Windows credentials specified by **@job_login** and **@job_password**. The Distribution Agent always makes the local connection to the Subscriber using Windows Integrated Authentication. By default, the agent will connect to the Distributor using Windows Integrated Authentication.  
  
    -   (Optional) A value of **0** for **@distributor_security_mode** and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login information for **@distributor_login** and **@distributor_password**, if you need to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Distributor.  
  
    -   A schedule for the Distribution Agent job for this subscription. For more information, see [Specify Synchronization Schedules](specify-synchronization-schedules.md).  
  
4.  At the Publisher, execute [sp_addsubscription &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addsubscription-transact-sql) to register the pull subscription. Specify **@publication**, **@subscriber**, and **@destination_db**. Specify a value of **pull** for **@subscription_type**.  
  
#### To create a pull subscription to a merge publication  
  
1.  At the Publisher, verify that the publication supports pull subscriptions by executing [sp_helpmergepublication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql).  
  
    -   If the value of **allow_pull** in the result set is **1**, then the publication supports pull subscriptions.  
  
    -   If the value of **allow_pull** is **0**, execute [sp_changemergepublication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql), specifying **allow_pull** for **@property** and `true` for **@value**.  
  
2.  At the Subscriber, execute [sp_addmergepullsubscription &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql). Specify **@publisher**, **@publisher_db**, **@publication**, and the following parameters:  
  
    -   **@subscriber_type** - specify **local** for a client subscription and **global** for a server subscription.  
  
    -   **@subscription_priority** - Specify a priority for the subscription (**0.00** to **99.99**). This is only required for a server subscription.  
  
         For more information, see [Advanced Merge Replication Conflict Detection and Resolution](merge/advanced-merge-replication-conflict-detection-and-resolution.md).  
  
3.  At the Subscriber, execute [sp_addmergepullsubscription_agent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql). Specify the following parameters:  
  
    -   **@publisher**, **@publisher_db**, and **@publication**.  
  
    -   The Windows credentials under which the Merge Agent at the Subscriber runs for **@job_login** and **@job_password**.  
  
        > [!NOTE]  
        >  Connections made using Windows Integrated Authentication always use the Windows credentials specified by **@job_login** and **@job_password**. The Merge Agent always makes the local connection to the Subscriber using Windows Integrated Authentication. By default, the agent will connect to the Distributor and Publisher using Windows Integrated Authentication.  
  
    -   (Optional) A value of **0** for **@distributor_security_mode** and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login information for **@distributor_login** and **@distributor_password**, if you need to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Distributor.  
  
    -   (Optional) A value of **0** for **@publisher_security_mode** and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login information for **@publisher_login** and **@publisher_password**, if you need to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher.  
  
    -   A schedule for the Merge Agent job for this subscription. For more information, see [Create an Updatable Subscription to a Transactional Publication](publish/create-an-updatable-subscription-to-a-transactional-publication.md).  
  
4.  At the Publisher, execute [sp_addmergesubscription &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql). Specify **@publication**, **@subscriber**, **@subscriber_db**, and a value of **pull** for **@subscription_type**. This registers the pull subscription.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 The following example creates a pull subscription to a transactional publication. The first batch is executed at the Subscriber, and the second batch is executed at the Publisher. Login and password values are supplied at runtime using sqlcmd scripting variables.  
  
 [!code-sql[HowTo#sp_addtranpullsubscriptionagent](../../snippets/tsql/SQL15/replication/howto/tsql/createtranpullsub.sql#sp_addtranpullsubscriptionagent)]  
  
 [!code-sql[HowTo#sp_addtranpullsubscription](../../snippets/tsql/SQL15/replication/howto/tsql/createtranpullsub.sql#sp_addtranpullsubscription)]  
  
 The following example creates a pull subscription to a merge publication. The first batch is executed at the Subscriber, and the second batch is executed at the Publisher. Login and password values are supplied at runtime using **sqlcmd** scripting variables.  
  
 [!code-sql[HowTo#sp_addmergepullsubscriptionagent](../../snippets/tsql/SQL15/replication/howto/tsql/createmergepullsub.sql#sp_addmergepullsubscriptionagent)]  
  
 [!code-sql[HowTo#sp_addmergepullsubscription](../../snippets/tsql/SQL15/replication/howto/tsql/createmergepullsub.sql#sp_addmergepullsubscription)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 The RMO classes used to create a pull subscription depend on the type of publication to which the subscription belongs.  
  
#### To create a pull subscription to a snapshot or transactional publication  
  
1.  Create connections to both the Subscriber and Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> Class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPublication> class by using the Publisher connection from step 1. Specify <xref:Microsoft.SqlServer.Replication.Publication.Name%2A>, <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> and <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method. If this method returns `false`, either the properties specified in step 2 are incorrect or the publication does not exist on the server.  
  
4.  Perform a bitwise logical AND (`&` in Visual C# and `And` in Visual Basic) between the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property and <xref:Microsoft.SqlServer.Replication.PublicationAttributes.AllowPull>. If the result is <xref:Microsoft.SqlServer.Replication.PublicationAttributes.None>, set <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> to the result of a bitwise logical OR (`|` in Visual C# and `Or` in Visual Basic) between <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> and <xref:Microsoft.SqlServer.Replication.PublicationAttributes.AllowPull>. Then, call <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> to enable pull subscriptions.  
  
5.  If the subscription database does not exist, create it by using the <xref:Microsoft.SqlServer.Management.Smo.Database> class. For more information, see [Creating, Altering, and Removing Databases](../server-management-objects-smo/tasks/creating-altering-and-removing-databases.md).  
  
6.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPullSubscription> class.  
  
7.  Set the following subscription properties:  
  
    -   The <xref:Microsoft.SqlServer.Management.Common.ServerConnection> to the Subscriber created in step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
    -   Name of the subscription database for <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>.  
  
    -   Name of the Publisher for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>.  
  
    -   Name of the publication database for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A>.  
  
    -   Name of the publication for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>.  
  
    -   The <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> or <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.SecurePassword%2A> fields of <xref:Microsoft.SqlServer.Replication.PullSubscription.SynchronizationAgentProcessSecurity%2A> to provide the credentials for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the Distribution Agent runs at the Subscriber. This account is used to make local connections to the Subscriber and to make remote connections using Windows Authentication.  
  
        > [!NOTE]  
        >  Setting <xref:Microsoft.SqlServer.Replication.PullSubscription.SynchronizationAgentProcessSecurity%2A> is not required when the subscription is created by a member of the `sysadmin` fixed server role, however it is recommended. In this case, the agent will impersonate the SQL Server Agent account. For more information, see [Replication Agent Security Model](security/replication-agent-security-model.md).  
  
    -   (Optional) A value of `true` for <xref:Microsoft.SqlServer.Replication.PullSubscription.CreateSyncAgentByDefault%2A> to create an agent job that is used to synchronize the subscription. If you specify `false` (the default), the subscription can only be synchronized programmatically and you must specify additional properties of <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent> when you access this object from the <xref:Microsoft.SqlServer.Replication.TransPullSubscription.SynchronizationAgent%2A> property. For more information, see [Synchronize a Pull Subscription](synchronize-a-pull-subscription.md).  
  
        > [!NOTE]  
        >  SQL Server Agent is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md). When you specify a value of `true` for Express Subscribers, the agent job is not created. However, important subscription-related metadata is stored at the Subscriber.  
  
    -   (Optional) Set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> or <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SecureSqlStandardPassword%2A> fields of <xref:Microsoft.SqlServer.Replication.PullSubscription.DistributorSecurity%2A> when using SQL Server Authentication to connect to the Distributor.  
  
8.  Call the <xref:Microsoft.SqlServer.Replication.PullSubscription.Create%2A> method.  
  
9. Using the instance of the <xref:Microsoft.SqlServer.Replication.TransPublication> class from step 2, call the <xref:Microsoft.SqlServer.Replication.TransPublication.MakePullSubscriptionWellKnown%2A> method to register the pull subscription with the Publisher. If this registration already exists, an exception occurs.  
  
#### To create a pull subscription to a merge publication  
  
1.  Create connections to both the Subscriber and Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class by using the Publisher connection from step 1. Specify <xref:Microsoft.SqlServer.Replication.Publication.Name%2A>, <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A>, and <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method. If this method returns `false`, either the properties specified in step 2 are incorrect or the publication does not exist on the server.  
  
4.  Perform a bitwise logical AND (`&` in Visual C# and `And` in Visual Basic) between the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property and <xref:Microsoft.SqlServer.Replication.PublicationAttributes.AllowPull>. If the result is <xref:Microsoft.SqlServer.Replication.PublicationAttributes.None>, set <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> to the result of a bitwise logical OR (`|` in Visual C# and `Or` in Visual Basic) between <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> and <xref:Microsoft.SqlServer.Replication.PublicationAttributes.AllowPull>. Then, call <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> to enable pull subscriptions.  
  
5.  If the subscription database does not exist, create it by using the <xref:Microsoft.SqlServer.Management.Smo.Database> class. For more information, see [Creating, Altering, and Removing Databases](../server-management-objects-smo/tasks/creating-altering-and-removing-databases.md).  
  
6.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePullSubscription> class.  
  
7.  Set the following subscription properties:  
  
    -   The <xref:Microsoft.SqlServer.Management.Common.ServerConnection> to the Subscriber created in step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
    -   Name of the subscription database for <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>.  
  
    -   Name of the Publisher for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>.  
  
    -   Name of the publication database for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A>.  
  
    -   Name of the publication for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>.  
  
    -   The <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> or <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.SecurePassword%2A> fields of <xref:Microsoft.SqlServer.Replication.PullSubscription.SynchronizationAgentProcessSecurity%2A> to provide the credentials for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the Merge Agent runs at the Subscriber. This account is used to make local connections to the Subscriber and to make remote connections using Windows Authentication.  
  
        > [!NOTE]  
        >  Setting <xref:Microsoft.SqlServer.Replication.PullSubscription.SynchronizationAgentProcessSecurity%2A> is not required when the subscription is created by a member of the `sysadmin` fixed server role, however it is recommended. In this case, the agent will impersonate the SQL Server Agent account. For more information, see [Replication Agent Security Model](security/replication-agent-security-model.md).  
  
    -   (Optional) A value of `true` for <xref:Microsoft.SqlServer.Replication.PullSubscription.CreateSyncAgentByDefault%2A> to create an agent job that is used to synchronize the subscription. If you specify `false` (the default), the subscription can only be synchronized programmatically and you must specify additional properties of <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent> when you access this object from the <xref:Microsoft.SqlServer.Replication.MergePullSubscription.SynchronizationAgent%2A> property. For more information, see [Synchronize a Pull Subscription](synchronize-a-pull-subscription.md).  
  
    -   (Optional) Set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> or <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SecureSqlStandardPassword%2A> fields of <xref:Microsoft.SqlServer.Replication.PullSubscription.DistributorSecurity%2A> when using SQL Server Authentication to connect to the Distributor.  
  
    -   (Optional) Set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> or <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SecureSqlStandardPassword%2A> fields of <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherSecurity%2A> when using SQL Server Authentication to connect to the Publisher.  
  
8.  Call the <xref:Microsoft.SqlServer.Replication.PullSubscription.Create%2A> method.  
  
9. Using the instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class from step 2, call the <xref:Microsoft.SqlServer.Replication.MergePublication.MakePullSubscriptionWellKnown%2A> method to register the pull subscription with the Publisher. If this registration already exists, an exception occurs.  
  
###  <a name="PShellExample"></a> Example (RMO)  
 This example creates a pull subscription to a transactional publication. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account credentials used to create the Distribution Agent job are passed at runtime.  
  
 [!code-csharp[HowTo#rmo_CreateTranPullSub](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_createtranpullsub)]  
  
 [!code-vb[HowTo#rmo_vb_CreateTranPullSub](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_createtranpullsub)]  
  
 This example creates a pull subscription to a merge publication. The Windows account credentials used to create the Merge Agent job are passed at runtime.  
  
 [!code-csharp[HowTo#rmo_CreateMergePullSub](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_createmergepullsub)]  
  
 [!code-vb[HowTo#rmo_vb_CreateMergePullSub](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_createmergepullsub)]  
  
 This example creates a pull subscription to a merge publication without creating an associated agent job and subscription metadata in [MSsubscription_properties](/sql/relational-databases/system-tables/mssubscription-properties-transact-sql). The Windows account credentials used to create the Merge Agent job are passed at runtime.  
  
 [!code-csharp[HowTo#rmo_CreateMergePullSub_NoJob](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_createmergepullsub_nojob)]  
  
 [!code-vb[HowTo#rmo_vb_CreateMergePullSub_NoJob](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_createmergepullsub_nojob)]  
  
 This example creates a pull subscription to a merge publication that can be synchronized over the Internet using Web synchronization. The Windows account credentials used to create the Merge Agent job are passed at runtime. For more information, see [Configure Web Synchronization](configure-web-synchronization.md).  
  
 [!code-csharp[HowTo#rmo_CreateMergePullSub_WebSync](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_createmergepullsub_websync)]  
  
 [!code-vb[HowTo#rmo_vb_CreateMergePullSub_WebSync](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_createmergepullsub_websync)]  
  
## See Also  
 [Replication Management Objects Concepts](concepts/replication-management-objects-concepts.md)   
 [View and Modify Pull Subscription Properties](view-and-modify-pull-subscription-properties.md)   
 [Configure Web Synchronization](configure-web-synchronization.md)   
 [Subscribe to Publications](subscribe-to-publications.md)   
 [Replication Security Best Practices](security/replication-security-best-practices.md)  
  
  
