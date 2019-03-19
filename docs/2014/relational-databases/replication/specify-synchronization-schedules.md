---
title: "Specify Synchronization Schedules | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [SQL Server replication], synchronizing"
  - "scheduling synchronization [SQL Server replication]"
  - "synchronization [SQL Server replication], schedules"
  - "replication [SQL Server], synchronization"
ms.assetid: 97f2535b-ec19-4973-823d-bcf3d5aa0216
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Specify Synchronization Schedules
  This topic describes how to specify synchronization schedules in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO). When you create a subscription, you can define a synchronization schedule that controls when the replication agent for the subscription will run. If you do not specify scheduling parameters, the subscription will use the default schedule.  
  
 Subscriptions are synchronized by the Distribution Agent (for snapshot and transactional replication) or the Merge Agent (for merge replication). Agents can run continuously, run on demand, or run on a schedule.  
  
 **In This Topic**  
  
-   **To specify synchronization schedules, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Specify synchronization schedules on the **Synchronization Schedule** page of the New Subscription Wizard. For more information about accessing this wizard, see [Create a Push Subscription](create-a-push-subscription.md) and [Create a Pull Subscription](create-a-pull-subscription.md).  
  
 Modify synchronization schedules in the **Job Schedule Properties** dialog box, which is available from the **Jobs** folder in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and from the agent details windows in Replication Monitor. For information about starting Replication Monitor, see [Start the Replication Monitor](monitor/start-the-replication-monitor.md).  
  
 If you specify schedules from the **Jobs** folder, use the following table to determine the agent job name.  
  
|Agent|Job name|  
|-----------|--------------|  
|Merge Agent for pull subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<SubscriptionDatabase>-\<integer>**|  
|Merge Agent for push subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<integer>**|  
|Distribution Agent for push subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<integer>** <sup>1</sup>|  
|Distribution Agent for pull subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<SubscriptionDatabase>-\<GUID>** <sup>2</sup>|  
|Distribution Agent for push subscriptions to non-SQL Server Subscribers|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<integer>**|  
  
 <sup>1</sup> For push subscriptions to Oracle publications, it is **\<Publisher>-\<Publisher**> rather than **\<Publisher>-\<PublicationDatabase>**  
  
 <sup>2</sup> For pull subscriptions to Oracle publications, it is **\<Publisher>-\<DistributionDatabase**> rather than **\<Publisher>-\<PublicationDatabase>**  
  
#### To specify synchronization schedules  
  
1.  On the **SynchronizationSchedule** page of the New Subscription Wizard, select one of the following values from the **Agent Schedule** drop-down list for each subscription you are creating:  
  
    -   **Run continuously**  
  
    -   **Run on demand only**  
  
    -   **\<Define Schedule...>**  
  
2.  If you select **\<Define Schedule...>**, specify a schedule in the **Job Schedule Properties** dialog box, and then click **OK**.  
  
3.  Complete the wizard.  
  
#### To modify a synchronization schedule for a push subscription in Replication Monitor  
  
1.  Expand a Publisher group in the left pane of Replication Monitor, expand a Publisher, and then click a publication.  
  
2.  Click the **All Subscriptions** tab.  
  
3.  Right-click a subscription, and then click **View Details**.  
  
4.  In the **Subscription \< SubscriptionName>** window, click **Action**, and then click **\<AgentName> Job Properties**.  
  
5.  On the **Schedules** page of the **Job Properties - \<JobName>** dialog box, click **Edit.**  
  
6.  In the **Job Schedule Properties** dialog box, select a value from the **Schedule Type** drop-down list:  
  
    -   To specify that the agent should run continuously, select **Start automatically when SQL Server Agent starts**.  
  
    -   To specify that the agent should run on a schedule, select **Recurring**.  
  
    -   To specify that the agent should run on demand, select **One time**.  
  
7.  If you select **Recurring**, specify a schedule for the agent.  
  
8.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
#### To modify a synchronization schedule for a push subscription in Management Studio  
  
1.  Connect to the Distributor in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **SQL Server Agent** folder, and then expand the **Jobs** folder.  
  
3.  Right-click the job for the Distribution Agent or Merge Agent associated with the subscription, and then click **Properties**.  
  
4.  On the **Schedules** page of the **Job Properties - \<JobName>** dialog box, click **Edit.**  
  
5.  In the **Job Schedule Properties** dialog box, select a value from the **Schedule Type** drop-down list:  
  
    -   To specify that the agent should run continuously, select **Start automatically when SQL Server Agent starts**.  
  
    -   To specify that the agent should run on a schedule, select **Recurring**.  
  
    -   To specify that the agent should run on demand, select **One time**.  
  
6.  If you select **Recurring**, specify a schedule for the agent.  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
#### To modify a synchronization schedule for a pull subscription in Management Studio  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **SQL Server Agent** folder, and then expand the **Jobs** folder.  
  
3.  Right-click the job for the Distribution Agent or Merge Agent associated with the subscription, and then click **Properties**.  
  
4.  On the **Schedules** page of the **Job Properties - \<JobName>** dialog box, click **Edit.**  
  
5.  In the **Job Schedule Properties** dialog box, select a value from the **Schedule Type** drop-down list:  
  
    -   To specify that the agent should run continuously, select **Start automatically when SQL Server Agent starts**.  
  
    -   To specify that the agent should run on a schedule, select **Recurring**.  
  
    -   To specify that the agent should run on demand, select **One time**.  
  
6.  If you select **Recurring**, specify a schedule for the agent.  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 You can define synchronization schedules programmatically using replication stored procedures. The stored procedures that you use depend on the type of replication and the type of subscription (pull or push).  
  
 A schedule is defined by the following scheduling parameters, the behaviors of which are inherited from [sp_add_schedule &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-add-schedule-transact-sql):  
  
-   **@frequency_type** - the type of frequency used when scheduling the agent.  
  
-   **@frequency_interval** - the day of the week when an agent runs.  
  
-   **@frequency_relative_interval** - the week of a given month when the agent is scheduled to run monthly.  
  
-   **@frequency_recurrence_factor** - the number of frequency-type units that occur between synchronizations.  
  
-   **@frequency_subday** - the frequency unit when the agent runs more often than once a day.  
  
-   **@frequency_subday_interval** - the number of frequency units between runs when the agent runs more often than once a day.  
  
-   **@active_start_time_of_day** - the earliest time in a given day when an agent run will start.  
  
-   **@active_end_time_of_day** - the latest time in a given day when an agent run will start.  
  
-   **@active_start_date** - the first day that the agent schedule will be in effect.  
  
-   **@active_end_date** - the last day that the agent schedule will be in effect.  
  
#### To define the synchronization schedule for a pull subscription to a transactional publication  
  
1.  Create a new pull subscription to a transactional publication. For more information, see [Create a Pull Subscription](create-a-pull-subscription.md).  
  
2.  At the Subscriber, execute [sp_addpullsubscription_agent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql). Specify **@publisher**, **@publisher_db**, **@publication**, and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows credentials under which the Distribution Agent at the Subscriber runs for **@job_name** and **@password**. Specify the synchronization parameters, detailed above, that define the schedule for the Distribution Agent job that synchronizes the subscription.  
  
#### To define the synchronization schedule for a push subscription to a transactional publication  
  
1.  Create a new push subscription to a transactional publication. For more information, see [Create a Push Subscription](create-a-push-subscription.md).  
  
2.  At the Subscriber, execute [sp_addpushsubscription_agent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql). Specify **@subscriber**, **@subscriber_db**, **@publication**, and the Windows credentials under which the Distribution Agent at the Subscriber runs for **@job_name** and **@password**. Specify the synchronization parameters, detailed above, that define the schedule for the Distribution Agent job that synchronizes the subscription.  
  
#### To define the synchronization schedule for a pull subscription to a merge publication  
  
1.  Create a new pull subscription to a merge publication. For more information, see [Create a Pull Subscription](create-a-pull-subscription.md).  
  
2.  At the Subscriber, execute [sp_addmergepullsubscription_agent](/sql/relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql). Specify **@publisher**, **@publisher_db**, **@publication**, and the Windows credentials under which the Merge Agent at the Subscriber runs for **@job_name** and **@password**. Specify the synchronization parameters, detailed above, that define the schedule for the Merge Agent job that synchronizes the subscription.  
  
#### To define the synchronization schedule for a push subscription to a merge publication  
  
1.  Create a new push subscription to a merge publication. For more information, see [Create a Push Subscription](create-a-push-subscription.md).  
  
2.  At the Subscriber, execute [sp_addmergepushsubscription_agent](/sql/relational-databases/system-stored-procedures/sp-addmergepushsubscription-agent-transact-sql). Specify **@subscriber**, **@subscriber_db**, **@publication**, and the Windows credentials under which the Merge Agent at the Subscriber runs for **@job_name** and **@password**. Specify the synchronization parameters, detailed above, that define the schedule for the Merge Agent job that synchronizes the subscription.  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 Replication uses the SQL Server Agent to schedule jobs for activities that occur periodically, such as snapshot generation and subscription synchronization. You can use Replication Management Objects (RMO) programmatically to specify schedules for replication agent jobs.  
  
> [!NOTE]  
>  When you create a subscription and specify a value `false` for `CreateSyncAgentByDefault` (the default behavior for pull subscriptions) the agent job is not created and scheduling properties are ignored. In this case, the synchronization schedule must be determined by the application. For more information, see [Create a Pull Subscription](create-a-pull-subscription.md) and [Create a Push Subscription](create-a-push-subscription.md).  
  
#### To define a replication agent schedule when you create a push subscription to a transactional publication  
  
1.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransSubscription> class for the subscription you are creating. For more information, see [Create a Push Subscription](create-a-push-subscription.md).  
  
2.  Before you call <xref:Microsoft.SqlServer.Replication.Subscription.Create%2A>, set one or more of the following fields of the <xref:Microsoft.SqlServer.Replication.Subscription.AgentSchedule%2A> property:  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyType%2A> - the type of frequency (such as daily or weekly) you use when you schedule the agent.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyInterval%2A> - the day of the week that an agent runs.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyRelativeInterval%2A> - the week of a given month when the agent is scheduled to run monthly.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyRecurrenceFactor%2A> - the number of frequency-type units that occur between synchronizations.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencySubDay%2A> - the frequency unit when the agent runs more often than once a day.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencySubDayInterval%2A> - the number of frequency units between runs when the agent runs more often than once a day.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveStartTime%2A> - earliest time on a given day that an agent run starts.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveEndTime%2A> - latest time on a given day that an agent run starts.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveStartDate%2A> - first day that the agent schedule is in effect.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveEndDate%2A> - last day that the agent schedule is in effect.  
  
    > [!NOTE]  
    >  If you do not specify one of these properties, a default value is set.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.Subscription.Create%2A> method to create the subscription.  
  
#### To define a replication agent schedule when you create a pull subscription to a transactional publication  
  
1.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPullSubscription> class for the subscription you are creating. For more information, see [Create a Pull Subscription](create-a-pull-subscription.md).  
  
2.  Before you call <xref:Microsoft.SqlServer.Replication.PullSubscription.Create%2A>, set one or more of the following fields of the <xref:Microsoft.SqlServer.Replication.PullSubscription.AgentSchedule%2A> property:  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyType%2A> - the type of frequency (such as daily or weekly) that you use when you schedule the agent.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyInterval%2A> - the day of the week that an agent runs.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyRelativeInterval%2A> - the week of a given month that the agent is scheduled to run monthly.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyRecurrenceFactor%2A> - the number of frequency-type units that occur between synchronizations.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencySubDay%2A> - the frequency unit when the agent runs more often than once a day.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencySubDayInterval%2A> - the number of frequency units between runs when the agent runs more often than once a day.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveStartTime%2A> - earliest time on a given day that an agent run starts.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveEndTime%2A> - latest time on a given day that an agent run starts.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveStartDate%2A> - first day that the agent schedule is in effect.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveEndDate%2A> - last day that the agent schedule is in effect.  
  
    > [!NOTE]  
    >  If you do not specify one of these properties, a default value is set.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.PullSubscription.Create%2A> method to create the subscription.  
  
#### To define a replication agent schedule when you create a pull subscription to a merge publication  
  
1.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePullSubscription> class for the subscription you are creating. For more information, see [Create a Pull Subscription](create-a-pull-subscription.md).  
  
2.  Before you call <xref:Microsoft.SqlServer.Replication.PullSubscription.Create%2A>, set one or more of the following fields of the <xref:Microsoft.SqlServer.Replication.PullSubscription.AgentSchedule%2A> property:  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyType%2A> - the type of frequency (such as daily or weekly) that you use when you schedule the agent.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyInterval%2A> - the day of the week that an agent runs.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyRelativeInterval%2A> - the week of a given month that the agent is scheduled to run monthly.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyRecurrenceFactor%2A> - the number of frequency-type units that occur between synchronizations.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencySubDay%2A> - the frequency unit when the agent runs more often than once a day.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencySubDayInterval%2A> - the number of frequency units between runs when the agent runs more often than once a day.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveStartTime%2A> - earliest time on a given day that an agent run starts.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveEndTime%2A> - latest time on a given day that an agent run starts.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveStartDate%2A> - first day that the agent schedule is in effect.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveEndDate%2A> - last day that the agent schedule is in effect.  
  
    > [!NOTE]  
    >  If you do not specify one of these properties, a default value is set.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.PullSubscription.Create%2A> method to create the subscription.  
  
#### To define a replication agent schedule when you create a push subscription to a merge publication  
  
1.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeSubscription> class for the subscription you are creating. For more information, see [Create a Push Subscription](create-a-push-subscription.md).  
  
2.  Before you call <xref:Microsoft.SqlServer.Replication.Subscription.Create%2A>, set one or more of the following fields of the <xref:Microsoft.SqlServer.Replication.Subscription.AgentSchedule%2A> property:  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyType%2A> - the type of frequency (such as daily or weekly) that you use when you schedule the agent.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyInterval%2A> - the day of the week that an agent runs.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyRelativeInterval%2A> - the week of a given month that the agent is scheduled to run monthly.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencyRecurrenceFactor%2A> - the number of frequency-type units that occur between synchronizations.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencySubDay%2A> - the frequency unit when the agent runs more often than once a day.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.FrequencySubDayInterval%2A> - the number of frequency units between runs when the agent runs more often than once a day.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveStartTime%2A> - earliest time on a given day that an agent run starts.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveEndTime%2A> - latest time on a given day that an agent run starts.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveStartDate%2A> - first day that the agent schedule is in effect.  
  
    -   <xref:Microsoft.SqlServer.Replication.ReplicationAgentSchedule.ActiveEndDate%2A> - last day that the agent schedule is in effect.  
  
    > [!NOTE]  
    >  If you do not specify one of these properties, a default value is set.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.Subscription.Create%2A> method to create the subscription.  
  
###  <a name="PShellExample"></a> Example (RMO)  
 This example creates a push subscription to a merge publication and specifies the schedule on which the subscription is synchronized.  
  
 [!code-csharp[HowTo#rmo_CreateMergePushSub](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_createmergepushsub)]  
  
 [!code-vb[HowTo#rmo_vb_CreateMergePushSub](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_createmergepushsub)]  
  
## See Also  
 [Replication Security Best Practices](security/replication-security-best-practices.md)   
 [Subscribe to Publications](subscribe-to-publications.md)   
 [Synchronize a Push Subscription](synchronize-a-push-subscription.md)   
 [Synchronize a Pull Subscription](synchronize-a-pull-subscription.md)   
 [Synchronize Data](synchronize-data.md)  
  
  
