---
title: "View and Modify Replication Agent Command Prompt Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "agents [SQL Server replication], command prompt parameters"
ms.assetid: 45f2e781-c21d-4b44-8992-89f60fb3d022
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# View and Modify Replication Agent Command Prompt Parameters
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Replication agents are executables that accept command line parameters. By default, agents run under [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent job steps, so these parameters can be viewed and modified using the **Job Properties - \<Job>** dialog box. This dialog box is available from the **Jobs** folder in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] and from the **Agents** tab in Replication Monitor. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
> [!NOTE]  
>  Agent parameter changes take effect the next time the agent is started. If the agent runs continuously, you must stop and restart the agent.  
  
 Although parameters can be modified directly, it is more common to modify them through an agent profile. For more information, see [Replication Agent Profiles](../../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
 If you access agent jobs from the **Jobs** folder, use the following table to determine the agent job name and the parameters available for each agent.  
  
|Agent|Job name|For a list of parameters, see…|  
|-----------|--------------|------------------------------------|  
|Snapshot Agent|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<integer>**|[Replication Snapshot Agent](../../../relational-databases/replication/agents/replication-snapshot-agent.md)|  
|Snapshot Agent for a merge publication partition|**Dyn_\<Publisher>-\<PublicationDatabase>-\<Publication>-\<GUID>**|[Replication Snapshot Agent](../../../relational-databases/replication/agents/replication-snapshot-agent.md)|  
|Log Reader Agent|**\<Publisher>-\<PublicationDatabase>-\<integer>**|[Replication Log Reader Agent](../../../relational-databases/replication/agents/replication-log-reader-agent.md)|  
|Merge Agent for pull subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<SubscriptionDatabase>-\<integer>**|[Replication Merge Agent](../../../relational-databases/replication/agents/replication-merge-agent.md)|  
|Merge Agent for push subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<integer>**|[Replication Merge Agent](../../../relational-databases/replication/agents/replication-merge-agent.md)|  
|Distribution Agent for push subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<integer>**|[Replication Distribution Agent](../../../relational-databases/replication/agents/replication-distribution-agent.md)|  
|Distribution Agent for pull subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<SubscriptionDatabase>-\<GUID>**|[Replication Distribution Agent](../../../relational-databases/replication/agents/replication-distribution-agent.md)|  
|Distribution Agent for push subscriptions to non-SQL Server Subscribers|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<integer>**|[Replication Distribution Agent](../../../relational-databases/replication/agents/replication-distribution-agent.md)|  
|Queue Reader Agent|**[\<Distributor>].\<integer>**|[Replication Queue Reader Agent](../../../relational-databases/replication/agents/replication-queue-reader-agent.md)|  
  
 \*For push subscriptions to Oracle publications, it is **\<Publisher>-\<Publisher**> rather than **\<Publisher>-\<PublicationDatabase>**  
  
 \*\*For pull subscriptions to Oracle publications, it is **\<Publisher>-\<DistributionDatabase**> rather than **\<Publisher>-\<PublicationDatabase>**  
  
### To view and modify replication agent command line parameters from Management Studio  
  
1.  Connect to the appropriate computer in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)], and then expand the server node:  
  
    -   For the Distribution Agent and Merge Agent for pull subscriptions, connect to the Subscriber.  
  
    -   For all other agents, connect to the Distributor.  
  
2.  Expand the **SQL Server Agent** folder, and then expand the **Jobs** folder.  
  
3.  Right-click a job, and then click **Properties**.  
  
4.  On the **Steps** page of the **Job Properties - \<Job>** dialog box, select the step **Run agent**, and then click **Edit**.  
  
5.  In the **Job Step Properties - Run agent** dialog box, edit the **Command** field.  
  
6.  Click **OK** on both dialog boxes.  
  
### To view and modify Distribution Agent and Merge Agent command line parameters from Replication Monitor  
  
1.  Expand a Publisher group in the left pane of Replication Monitor, expand a Publisher, and then click a publication.  
  
2.  Click the **All Subscriptions** tab.  
  
3.  Right-click a subscription, and then click **View Details**.  
  
4.  In the **Subscription < SubscriptionName>** window, click **Action**, and then click **\<AgentName> Job Properties**.  
  
5.  On the **Steps** page of the **Job Properties - \<Job>** dialog box, select the step **Run agent**, and then click **Edit**.  
  
6.  In the **Job Step Properties - Run agent** dialog box, edit the **Command** field.  
  
7.  Click **OK** on both dialog boxes.  
  
### To view and modify Snapshot Agent, Log Reader Agent, and Queue Reader Agent command line parameters from Replication Monitor  
  
1.  Expand a Publisher group in the left pane of Replication Monitor, expand a Publisher, and then click a publication.  
  
2.  Click the **Agents** tab.  
  
3.  Right-click an agent in the grid, and then click **Properties**.  
  
4.  On the **Steps** page of the **Job Properties - \<Job>** dialog box, select the step **Run agent**, and then click **Edit**.  
  
5.  In the **Job Step Properties - Run agent** dialog box, edit the **Command** field.  
  
6.  Click **OK** on both dialog boxes.  
  
## See Also  
 [Replication Agent Administration](../../../relational-databases/replication/agents/replication-agent-administration.md)   
 [Replication Agent Executables Concepts](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)   
 [Replication Agents Overview](../../../relational-databases/replication/agents/replication-agents-overview.md)  
  
  
