---
title: "&lt;AgentName&gt; Agent Security | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newsubwizard.agentnameagentsecurity.f1"
ms.assetid: d34c7ef8-cf77-4ffd-887f-3c4214dfd71e
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# &lt;AgentName&gt; Agent Security
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **\<AgentName> Agent Security** page allows you to specify the accounts under which the Distribution Agent (for transactional and snapshot replication) or Merge Agent (for merge replication) run and make connections to the computers in a replication topology. For information on permissions required by agents and best practices for replication security, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md) and [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md).  
  
## Options  
 Click the properties button (**...**) in the row for each Subscriber to access the **Distribution Agent Security** or **Merge Agent Security** dialog box. Click **Help** on the dialog box that is launched for more information on the permissions required for accounts used by the agents.  
  
 After settings have been entered in one of the dialog boxes, connection information for the Subscriber is displayed in the grid.  
  
 **Agent for Subscriber**  
 The name of each Subscriber.  
  
 **Connection to Distributor**  
 Displayed for transactional and snapshot replication. The context under which the connection to the Distributor is made. Local connections are always made using the context of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the agent runs:  
  
-   For push subscriptions, the local connection is the connection to the Distributor, so this field will always display: **Impersonate '\<Domain>\\<Login\>'** or **Impersonate '\<Computer>\\<Login\>'** for push subscriptions.  
  
-   For pull subscriptions, the connection can also be made under the context of a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The field displays one of the following: **Use login '\<Login>'**, **Impersonate '\<Domain>\\<Login\>'** or **Impersonate '\<Computer>\\<Login\>'**. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that all connections be made using the context of the Windows account.  
  
 **Connection to Publisher & Distributor**  
 Displayed for merge replication. The context under which the connections to the Publisher and Distributor are made. Local connections are always made using the context of the Windows account under which the agent runs:  
  
-   For push subscriptions, the local connection is the connection to the Publisher and Distributor, so this field will always display: **Impersonate '\<Domain>\\<Login\>'** or **Impersonate '\<Computer>\\<Login\>'** for push subscriptions.  
  
-   For pull subscriptions, the connection can also be made under the context of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The field displays one of the following: **Use login '\<Login>'**, **Impersonate '\<Domain>\\<Login\>'** or **Impersonate '\<Computer>\\<Login\>'**. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that all connections be made using the context of the Windows account.  
  
 **Connection to Subscriber**  
 The context under which the connection to the Subscriber is made. Local connections are always made using the context of the Windows account under which the agent runs:  
  
-   For pull subscriptions, the local connection is the connection to the Subscriber, so this field will always display: **Impersonate '\<Domain>\\<Login\>'** or **Impersonate '\<Computer>\\<Login\>'** for push subscriptions.  
  
-   For push subscriptions, the connection can also be made under the context of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The field displays one of the following: **Use login '\<Login>'**, **Impersonate '\<Domain>\\<Login\>'** or **Impersonate '\<Computer>\\<Login\>'**. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that all connections be made using the context of the Windows account.  
  
## See Also  
 [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md)   
 [View and Modify Push Subscription Properties](../../relational-databases/replication/view-and-modify-push-subscription-properties.md)   
 [Identity and access control for replication](../../relational-databases/replication/security/identity-and-access-control-replication.md)   
 [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md)   
 [View and modify replication security settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)  
  
  
