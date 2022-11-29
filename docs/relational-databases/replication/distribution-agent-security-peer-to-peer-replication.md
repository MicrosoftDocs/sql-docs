---
title: "Distribution Agent Security (Peer-to-Peer)"
description: Describes the 'Distribution Agent Security' page for a Peer-to-Peer Replication topology in SQL Server Management Studio.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.p2pwizard.DA.f1"
ms.assetid: def6bf26-c640-4caf-ad30-05d1e649541d
author: "MashaMSFT"
ms.author: "mathoma"
---
# Distribution Agent Security (Peer-to-Peer Replication)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **Distribution Agent Security** page allows you to specify the accounts under which the Distribution Agent runs and makes connections to the computers in a peer-to-peer topology. For information on permissions required by agents and best practices for replication security, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md) and [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md).  
  
> [!NOTE]  
>  If the Distribution Agent for a subscription has already been configured in a previous run of this wizard, you cannot change the credentials it uses in this wizard. If you specify new credentials, they are ignored. To change credentials, use the **Subscription Properties** dialog box. For more information, see [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
## Options  
 Click the properties button (**...**) in the row for each Subscriber to access the **Distribution Agent Security** dialog box. Click **Help** on the **Distribution Agent Security** dialog box that is launched for more information on the permissions required for accounts used by the agents.  
  
 After settings have been entered in one of the dialog boxes, connection information for the Subscriber is displayed in the grid.  
  
 **Agent for Subscriber**  
 The name of each peer.  
  
 **Peer Database**  
 The database at the peer that will serve as both a publication database and subscription database.  
  
 **Connection to Distributor**  
 The context under which the connection to the Distributor is made. Local connections are always made using the context of the Windows account under which the agent runs. This wizard creates push subscriptions (the local connection is the connection to the Distributor), so this field will always display: **Impersonate '\<Domain>\\<Login\>'** or **Impersonate '\<Computer>\\<Login\>'**.  
  
 **Connection to Subscriber**  
 The context under which the connection to the Subscriber is made. The connection can be made using the context of the Windows account under which the agent runs or under the context of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The field displays one of the following: **Use login '\<Login>'**, **Impersonate '\<Domain>\\<Login\>'** or **Impersonate '\<Computer>\\<Login\>'**. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that all connections be made using the context of the Windows account.  
  
## See Also  
 [Administer a Peer-to-Peer Topology &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/administer-a-peer-to-peer-topology-replication-transact-sql-programming.md)   
 [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)  
  
  
