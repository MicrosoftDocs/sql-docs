---
title: "Synchronize a Push Subscription | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "synchronization [SQL Server replication], push subscriptions"
  - "subscriptions [SQL Server replication], push"
  - "push subscriptions [SQL Server replication], synchronizing"
ms.assetid: 0cfa7ae5-91d3-4a4f-9edf-a852d45783b5
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Synchronize a Push Subscription
  This topic describes how to synchronize a push subscription in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [replication agents](agents/replication-agents-overview.md), or Replication Management Objects (RMO).  
  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Subscriptions are synchronized by the Distribution Agent (for snapshot and transactional replication) or the Merge Agent (for merge replication). Agents can run continuously, run on demand, or run on a schedule. For more information about specifying synchronization schedules, see [Specify Synchronization Schedules](specify-synchronization-schedules.md).  
  
 Synchronize a subscription on demand from the **Local Publications** and **Local Subscriptions** folders in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and the **All Subscriptions** tab in Replication Monitor. Subscriptions to Oracle publications cannot be synchronized on demand from the Subscriber. For information about starting Replication Monitor, see [Start the Replication Monitor](monitor/start-the-replication-monitor.md).  
  
#### To synchronize a push subscription on demand in Management Studio (at the Publisher)  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Expand the publication for which you want to synchronize subscriptions.  
  
4.  Right-click the subscription you want to synchronize, and then click **View Synchronization Status**.  
  
5.  In the **View Synchronization Status - \<Subscriber>:\<SubscriptionDatabase>** dialog box, click **Start**. When synchronization is complete, the message **Synchronization completed** is displayed.  
  
6.  Click **Close**.  
  
#### To synchronize a push subscription on demand in Management Studio (at the Subscriber)  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Subscriptions** folder.  
  
3.  Right-click the subscription you want to synchronize, and then click **View Synchronization Status**.  
  
4.  A message is displayed about establishing a connection to the Distributor. Click **OK**.  
  
5.  In the **View Synchronization Status - \<Subscriber>:\<SubscriptionDatabase>** dialog box, click **Start**. When synchronization is complete, the message **Synchronization completed** is displayed.  
  
6.  Click **Close**.  
  
#### To synchronize a push subscription on demand in Replication Monitor  
  
1.  In Replication Monitor, expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  Click the **All Subscriptions** tab.  
  
3.  Right-click the subscription you want to synchronize, and then click **Start Synchronizing**.  
  
4.  To view synchronization progress, right-click the subscription, and then click **View Details**.  
  
##  <a name="ReplProg"></a> Using Replication Agents  
 Push subscriptions can be synchronized programmatically and on-demand by invoking the appropriate replication agent executable file from the command prompt. The replication agent executable file that is invoked will depend on the type of publication to which the push subscription belongs.  
  
#### To start the Distribution Agent to synchronize a push subscription to a transactional publication  
  
1.  From the command prompt or in a batch file at the Distributor, execute **distrib.exe**. Specify the following command-line arguments:  
  
    -   **-Publisher**  
  
    -   **-PublisherDB**  
  
    -   **-Distributor**  
  
    -   **-Subscriber**  
  
    -   **-SubscriberDB**  
  
    -   **-SubscriptionType = 0**  
  
     If you are using SQL Server Authentication, you must also specify the following arguments:  
  
    -   **-DistributorLogin**  
  
    -   **-DistributorPassword**  
  
    -   **-DistributorSecurityMode = 0**  
  
    -   **-PublisherLogin**  
  
    -   **-PublisherPassword**  
  
    -   **-PublisherSecurityMode = 0**  
  
    -   **-SubscriberLogin**  
  
    -   **-SubscriberPassword**  
  
    -   **-SubscriberSecurityMode = 0**  
  
        > [!IMPORTANT]  
        >  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
#### To start the Merge Agent to synchronize a push subscription to a merge publication  
  
1.  From the command prompt or in a batch file at the Distributor, execute **replmerg.exe**. Specify the following command-line arguments:  
  
    -   **-Publisher**  
  
    -   **-PublisherDB**  
  
    -   **-Publication**  
  
    -   **-Distributor**  
  
    -   **-Subscriber**  
  
    -   **-SubscriberDB**  
  
    -   **-SubscriptionType = 0**  
  
     If you are using SQL Server Authentication, you must also specify the following arguments:  
  
    -   **-DistributorLogin**  
  
    -   **-DistributorPassword**  
  
    -   **-DistributorSecurityMode = 0**  
  
    -   **-PublisherLogin**  
  
    -   **-PublisherPassword**  
  
    -   **-PublisherSecurityMode = 0**  
  
    -   **-SubscriberLogin**  
  
    -   **-SubscriberPassword**  
  
    -   **-SubscriberSecurityMode = 0**  
  
        > [!IMPORTANT]  
        >  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
###  <a name="TsqlExample"></a> Examples (Replication Agents)  
 The following example starts the Distribution Agent to synchronize a push subscription.  
  
 
```
REM -- Declare the variables.  
SET Publisher=%instancename%  
SET Subscriber=%instancename%  
SET PublicationDB=AdventureWorks2012  
SET SubscriptionDB=AdventureWorks2012Replica   
SET Publication=AdvWorksProductsTran  
  
REM -- Start the Distribution Agent with four subscription streams.  
REM -- The following command must be supplied without line breaks.  
"C:\Program Files\Microsoft SQL Server\120\COM\DISTRIB.EXE" -Subscriber %Subscriber%   
-SubscriberDB %SubscriptionDB% -SubscriberSecurityMode 1 -Publication %Publication%   
-Publisher %Publisher% -PublisherDB %PublicationDB% -Distributor %Publisher%   
-DistributorSecurityMode 1 -Continuous -SubscriptionType 0 -SubscriptionStreams 4
```
  
 The following example starts the Merge Agent to synchronize a push subscription.  

```
REM -- Declare the variables.  
SET Publisher=%instancename%  
SET Subscriber=%instancename%  
SET PublicationDB=AdventureWorks2012  
SET SubscriptionDB=AdventureWorks2012Replica   
SET Publication=AdvWorksSalesOrdersMerge  
  
REM -- Start the Merge Agent.  
REM -- The following command must be supplied without line breaks.  
"C:\Program Files\Microsoft SQL Server\120\COM\REPLMERG.EXE"  -Publisher %Publisher%   
-Subscriber  %Subscriber%  -Distributor %Publisher% -PublisherDB  %PublicationDB%   
-SubscriberDB %SubscriptionDB% -Publication %Publication% -PublisherSecurityMode 1   
-OutputVerboseLevel 3  -Output -SubscriberSecurityMode 1  -SubscriptionType 0   
-DistributorSecurityMode 1
```
  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 You can synchronize push subscriptions programmatically by using Replication Management Objects (RMO) and managed code access to replication agent functionalities. The classes that you use to synchronize a push subscription depend on the type of publication to which the subscription belongs.  
  
> [!NOTE]  
>  If you want to start a synchronization that runs autonomously without affecting your application, start the agent asynchronously. However, if you want to monitor the outcome of the synchronization and receive callbacks from the agent during the synchronization process (for example, if you want to display a progress bar), you should start the agent synchronously. For [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssExpressEd2005](../../includes/ssexpressed2005-md.md)] Subscribers, you must start the agent synchronously.  
  
#### To synchronize a push subscription to a snapshot or transactional publication  
  
1.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransSubscription> class and set the following properties:  
  
    -   The publication database name for <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A>.  
  
    -   The name of the publication to which the subscription belongs for <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>.  
  
    -   The name of the subscription database for <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A>.  
  
    -   The name of the Subscriber for <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>.  
  
    -   The connection created in step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the remaining subscription properties. If this method returns `false`, verify that the subscription exists.  
  
4.  Start the Distribution Agent at the Distributor in one of the following ways:  
  
    -   Call the <xref:Microsoft.SqlServer.Replication.TransSubscription.SynchronizeWithJob%2A> method on the instance of <xref:Microsoft.SqlServer.Replication.TransSubscription> from step 2. This method starts the Distribution Agent asynchronously, and control immediately returns to your application while the agent job is running. You cannot call this method if the subscription was created with a value of `false` for <xref:Microsoft.SqlServer.Replication.Subscription.CreateSyncAgentByDefault%2A>.  
  
    -   Obtain an instance of the <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent> class from the <xref:Microsoft.SqlServer.Replication.TransSubscription.SynchronizationAgent%2A> property, and call the <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent.Synchronize%2A> method. This method starts the agent synchronously, and control remains with the running agent job. During synchronous execution you can handle the <xref:Microsoft.SqlServer.Replication.TransSynchronizationAgent.Status> event while the agent is running.  
  
#### To synchronize a push subscription to a merge publication  
  
1.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeSubscription> class, and set the following properties:  
  
    -   The publication database name for <xref:Microsoft.SqlServer.Replication.Subscription.DatabaseName%2A>.  
  
    -   The name of the publication to which the subscription belongs for <xref:Microsoft.SqlServer.Replication.Subscription.PublicationName%2A>.  
  
    -   The name of the subscription database for <xref:Microsoft.SqlServer.Replication.Subscription.SubscriptionDBName%2A>.  
  
    -   The name of the Subscriber for <xref:Microsoft.SqlServer.Replication.Subscription.SubscriberName%2A>.  
  
    -   The connection created in step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the remaining subscription properties. If this method returns `false`, verify that the subscription exists.  
  
4.  Start the Merge Agent at the Distributor in one of the following ways:  
  
    -   Call the <xref:Microsoft.SqlServer.Replication.MergeSubscription.SynchronizeWithJob%2A> method on the instance of <xref:Microsoft.SqlServer.Replication.MergeSubscription> from step 2. This method starts the Merge Agent asynchronously, and control immediately returns to your application while the agent job is running. You cannot call this method if the subscription was created with a value of `false` for <xref:Microsoft.SqlServer.Replication.Subscription.CreateSyncAgentByDefault%2A>.  
  
    -   Obtain an instance of the <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent> class from the <xref:Microsoft.SqlServer.Replication.MergeSubscription.SynchronizationAgent%2A> property, and call the <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.Synchronize%2A> method. This method starts the Merge Agent synchronously, and control remains with the running agent job. During synchronous execution, you can handle the <xref:Microsoft.SqlServer.Replication.MergeSynchronizationAgent.Status> event while the agent is running.  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 This example synchronizes a push subscription to a transactional publication, where the agent is started asynchronously using the agent job.  
  
 [!code-csharp[HowTo#rmo_SyncTranPushSub_WithJob](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_synctranpushsub_withjob)]  
  
 [!code-vb[HowTo#rmo_vb_SyncTranPushSub_WithJob](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_synctranpushsub_withjob)]  
  
 This example synchronizes a push subscription to a transactional publication, where the agent is started synchronously.  
  
 [!code-csharp[HowTo#rmo_SyncTranPushSub](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_synctranpushsub)]  
  
 [!code-vb[HowTo#rmo_vb_SyncTranPushSub](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_synctranpushsub)]  
  
 This example synchronizes a push subscription to a merge publication, where the agent is started asynchronously using the agent job.  
  
 [!code-csharp[HowTo#rmo_SyncMergePushSub_WithJob](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_syncmergepushsub_withjob)]  
  
 [!code-vb[HowTo#rmo_vb_SyncMergePushSub_WithJob](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_syncmergepushsub_withjob)]  
  
 This example synchronizes a push subscription to a merge publication, where the agent is started synchronously.  
  
 [!code-csharp[HowTo#rmo_SyncMergePushSub](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_syncmergepushsub)]  
  
 [!code-vb[HowTo#rmo_vb_SyncMergePushSub](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_syncmergepushsub)]  
  
## See Also  
 [Replication Management Objects Concepts](concepts/replication-management-objects-concepts.md)   
 [Synchronize Data](synchronize-data.md)   
 [Replication Security Best Practices](security/replication-security-best-practices.md)  
  
  
