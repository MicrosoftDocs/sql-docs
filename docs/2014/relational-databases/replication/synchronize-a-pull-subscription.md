---
title: "Synchronize a Pull Subscription | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "pull subscriptions [SQL Server replication], synchronizing"
  - "synchronization [SQL Server replication], pull subscriptions"
  - "subscriptions [SQL Server replication], pull"
ms.assetid: 3ca24b23-fdc3-408e-8208-a2ace48fc8e3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Synchronize a Pull Subscription
  This topic describes how to synchronize a pull subscription in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [replication agents](agents/replication-agents-overview.md), or Replication Management Objects (RMO).  
  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Subscriptions are synchronized by the Distribution Agent (for snapshot and transactional replication) or the Merge Agent (for merge replication). Agents can run continuously, run on demand, or run on a schedule. For more information about specifying synchronization schedules, see [Specify Synchronization Schedules](specify-synchronization-schedules.md).  
  
 Synchronize a subscription on demand from the **Local Subscriptions** folder in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
#### To synchronize a pull subscription on demand in Management Studio  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Subscriptions** folder.  
  
3.  Right-click the subscription you want to synchronize, and then click **View Synchronization Status**.  
  
4.  In the **View Synchronization Status - \<Subscriber>:\<SubscriptionDatabase>** dialog box, click **Start**. When synchronization is complete, the message **Synchronization completed** is displayed.  
  
5.  Click **Close**.  
  
##  <a name="ReplProg"></a> Replication Agents  
 Pull subscriptions can be synchronized programmatically and on-demand by invoking the appropriate replication agent executable file from the command prompt. The replication agent executable file that is invoked will depend on the type of publication to which the pull subscription belongs. For more information, see [Replication Agents](agents/replication-agents-overview.md).  
  
> [!NOTE]  
>  Replication agents connect to the local server using the Windows Authentication credentials of the user who started the agent from the command prompt. These Windows credentials are also used when connecting to remote servers using Windows Integrated Authentication.  
  
#### To start the distribution agent from the command prompt or from a batch file  
  
1.  From the command prompt or in a batch file, start the [Replication Distribution Agent](agents/replication-distribution-agent.md) by running **distrib.exe**, specifying the following command-line arguments:  
  
    -   **-Publisher**  
  
    -   **-PublisherDB**  
  
    -   **-Distributor**  
  
    -   **-DistributorSecurityMode** = **1**  
  
    -   **-Subscriber**  
  
    -   **-SubscriberDB**  
  
    -   **-SubscriberSecurityMode** = **1**  
  
    -   **-SubscriptionType** = **1**  
  
     If you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must also specify the following arguments:  
  
    -   **-DistributorLogin**  
  
    -   **-DistributorPassword**  
  
    -   **-DistributorSecurityMode** = **0**  
  
    -   **-PublisherLogin**  
  
    -   **-PublisherPassword**  
  
    -   **-PublisherSecurityMode** = **0**  
  
    -   **-SubscriberLogin**  
  
    -   **-SubscriberPassword**  
  
    -   **-SubscriberSecurityMode** = **0**  
  
#### To start the merge agent from the command prompt or from a batch file  
  
1.  From the command prompt or in a batch file, start the [Replication Merge Agent](agents/replication-merge-agent.md) by running **replmerg.exe**, specifying the following command-line arguments:  
  
    -   **-Publisher**  
  
    -   **-PublisherDB**  
  
    -   **-PublisherSecurityMode** = **1**  
  
    -   **-Publication**  
  
    -   **-Distributor**  
  
    -   **-DistributorSecurityMode** = **1**  
  
    -   **-Subscriber**  
  
    -   **-SubscriberSecurityMode** = **1**  
  
    -   **-SubscriberDB**  
  
    -   **-SubscriptionType** = **1**  
  
     If you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must also specify the following arguments:  
  
    -   **-DistributorLogin**  
  
    -   **-DistributorPassword**  
  
    -   **-DistributorSecurityMode** = **0**  
  
    -   **-PublisherLogin**  
  
    -   **-PublisherPassword**  
  
    -   **-PublisherSecurityMode** = **0**  
  
    -   **-SubscriberLogin**  
  
    -   **-SubscriberPassword**  
  
    -   **-SubscriberSecurityMode** = **0**  
  
###  <a name="TsqlExample"></a> Examples (Replication Agents)  
 The following example starts the Distribution Agent to synchronize a pull subscription. All connections are made using Windows Authentication.  
  
 [!code-sql[HowTo#bat_synctranpullsub_10](../../snippets/tsql/SQL15/replication/howto/tsql/synctranpullsub_10.bat)]  
  
 The following example starts the Merge Agent to synchronize a pull subscription. All connections are made using Windows Authentication.  
  
 [!code-sql[HowTo#bat_syncmergepullsub_10](../../snippets/tsql/SQL15/replication/howto/tsql/syncmergepullsub_10.bat)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 You can synchronize pull subscriptions programmatically by using Replication Management Objects (RMO) and managed code access to replication agent functionalities. The classes you use to synchronize a pull subscription depend on the type of publication to which the subscription belongs.  
  
> [!NOTE]  
>  If you want to start a synchronization that runs autonomously without affecting your application, start the agent asynchronously. However, if you want to monitor the outcome of the synchronization and receive callbacks from the agent during the synchronization process (for example, to display a progress bar), you should start the agent synchronously. For [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssExpressEd2005](../../includes/ssexpressed2005-md.md)] Subscribers, you must start the agent synchronously.  
  
#### To synchronize a pull subscription to a snapshot or transactional publication  
  
1.  Create a connection to the Subscriber by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPullSubscription> class, and set the following properties:  
  
    -   The subscription database name for <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>.  
  
    -   The name of the publication to which the subscription belongs for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>.  
  
    -   The name of the publication database for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A>.  
  
    -   The name of the Publisher for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>.  
  
    -   The connection created in step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the remaining subscription properties. If this method returns `false`, verify that the subscription exists.  
  
4.  Start the Distribution Agent at the Subscriber in one of the following ways:  
  
    -   Call the <xref:Microsoft.SqlServer.Replication.TransPullSubscription.SynchronizeWithJob%2A> method on the instance of <xref:Microsoft.SqlServer.Replication.TransPullSubscription> from step 2. This method starts the Distribution Agent asynchronously, and control immediately returns to your application while the agent job is running. You cannot call this method for [!INCLUDE[ssExpressEd2005](../../includes/ssexpressed2005-md.md)] Subscribers or if the subscription was created with a value of `false` for <xref:Microsoft.SqlServer.Replication.PullSubscription.CreateSyncAgentByDefault%2A> (the default).  
  
    -   Get an instance of the <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent> class from the <xref:Microsoft.SqlServer.Replication.TransPullSubscription.SynchronizationAgent%2A> property, and call the <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent.Synchronize%2A> method. This method starts the agent synchronously, and control remains with the running agent job. During synchronous execution, you can handle the <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent.Status> event while the agent is running.  
  
        > [!NOTE]  
        >  If you specified a value of `false` for <xref:Microsoft.SqlServer.Replication.PullSubscription.CreateSyncAgentByDefault%2A> (the default) when you created the pull subscription, you also need to specify <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent.Distributor%2A>, <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent.DistributorSecurityMode%2A>, and optionally <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent.DistributorLogin%2A> and <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent.DistributorPassword%2A> because the agent job related metadata for the subscription is not available in [MSsubscription_properties](/sql/relational-databases/system-tables/mssubscription-properties-transact-sql).  
  
#### To synchronize a pull subscription to a merge publication  
  
1.  Create a connection to the Subscriber by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePullSubscription> class, and set the following properties:  
  
    -   The subscription database name for <xref:Microsoft.SqlServer.Replication.PullSubscription.DatabaseName%2A>.  
  
    -   The name of the publication to which the subscription belongs for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationName%2A>.  
  
    -   The name of the published database for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublicationDBName%2A>.  
  
    -   The name of the Publisher for <xref:Microsoft.SqlServer.Replication.PullSubscription.PublisherName%2A>.  
  
    -   The connection created in step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the remaining subscription properties. If this method returns `false`, verify that the subscription exists.  
  
4.  Start the Merge Agent at the Subscriber in one of the following ways:  
  
    -   Call the <xref:Microsoft.SqlServer.Replication.MergePullSubscription.SynchronizeWithJob%2A> method on the instance of <xref:Microsoft.SqlServer.Replication.MergePullSubscription> from step 2. This method starts the Merge Agent asynchronously, and control immediately returns to your application while the agent job is running. You cannot call this method for [!INCLUDE[ssExpressEd2005](../../includes/ssexpressed2005-md.md)] Subscribers or if the subscription was created with a value of `false` for <xref:Microsoft.SqlServer.Replication.PullSubscription.CreateSyncAgentByDefault%2A> (the default).  
  
    -   Obtain an instance of the <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent> class from the <xref:Microsoft.SqlServer.Replication.MergePullSubscription.SynchronizationAgent%2A> property, and call the <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.Synchronize%2A> method. This method starts the Merge Agent synchronously, and control remains with the running agent job. During synchronous execution, you can handle the <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.Status> event while the agent is running.  
  
        > [!NOTE]  
        >  If you specified a value of `false` for <xref:Microsoft.SqlServer.Replication.PullSubscription.CreateSyncAgentByDefault%2A> (the default) when you created the pull subscription, you also need to specify <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.Distributor%2A>, <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.DistributorSecurityMode%2A>, <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.PublisherSecurityMode%2A>, <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.HostName%2A>, <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.SubscriptionType%2A>, <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.ExchangeType%2A>, and optionally <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.DistributorLogin%2A>, <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.DistributorPassword%2A>, <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.PublisherLogin%2A>, and <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.PublisherPassword%2A> because the agent job related metadata for the subscription is not available in [MSsubscription_properties](/sql/relational-databases/system-tables/mssubscription-properties-transact-sql).  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 This example synchronizes a pull subscription to a transactional publication, where the agent is started asynchronously using the agent job.  
  
 [!code-csharp[HowTo#rmo_SyncTranPullSub_WithJob](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_synctranpullsub_withjob)]  
  
 [!code-vb[HowTo#rmo_vb_SyncTranPullSub_WithJob](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_synctranpullsub_withjob)]  
  
 This example synchronizes a pull subscription to a transactional publication, where the agent is started synchronously.  
  
 [!code-csharp[HowTo#rmo_SyncTranPullSub](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_synctranpullsub)]  
  
 [!code-vb[HowTo#rmo_vb_SyncTranPullSub](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_synctranpullsub)]  
  
 This example synchronizes a pull subscription to a merge publication, where the agent is started asynchronously using the agent job.  
  
 [!code-csharp[HowTo#rmo_SyncMergePullSub_WithJob](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_syncmergepullsub_withjob)]  
  
 [!code-vb[HowTo#rmo_vb_SyncMergePullSub_WithJob](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_syncmergepullsub_withjob)]  
  
 This example synchronizes a pull subscription to a merge publication, where the agent is started synchronously.  
  
 [!code-csharp[HowTo#rmo_SyncMergePullSub](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_syncmergepullsub)]  
  
 [!code-vb[HowTo#rmo_vb_SyncMergePullSub](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_syncmergepullsub)]  
  
 This example synchronizes a pull subscription to a merge publication using Web synchronization. The subscription was created without the agent job and related subscription metadata, so the agent must be started synchronously and additional subscription information is supplied.  
  
 [!code-csharp[HowTo#rmo_SyncMergePullSub_NoJobWebSync](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_syncmergepullsub_nojobwebsync)]  
  
 [!code-vb[HowTo#rmo_vb_SyncMergePullSub_NoJobWebSync](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_syncmergepullsub_nojobwebsync)]  
  
## See Also  
 [Synchronize Data](synchronize-data.md)   
 [Create a Pull Subscription](create-a-pull-subscription.md)   
 [Replication Security Best Practices](security/replication-security-best-practices.md)  
  
  
