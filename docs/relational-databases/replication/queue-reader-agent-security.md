---
description: "Queue Reader Agent Security"
title: "Queue Reader Agent Security | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.security.QRA.f1"
helpviewer_keywords: 
  - "Queue Reader Agent Security dialog box"
ms.assetid: 77938da0-2afd-4455-8826-f4a6a9440cb3
author: "MashaMSFT"
ms.author: "mathoma"
---
# Queue Reader Agent Security
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **Queue Reader Agent Security** dialog box allows you to specify the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the Queue Reader Agent runs and makes local connections to the Distributor. The agent connects to the Publisher using the account specified in the **Publisher Properties** dialog box (available from the **Distributor Properties** dialog box); the agent connects to the Subscriber using the same context as the Distribution Agent for the subscription. For more information, see [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
 The account must be valid with the correct password specified. Accounts and passwords are not validated until an agent runs.  
  
## Options  
 **Process account**  
 Enter a Windows account under which the Queue Reader Agent runs at the Distributor. The Windows account you specify must at minimum be a member of the **db_owner** fixed database role in the distribution database.  
  
 **Password** and **Confirm password**  
 Enter the password for the Windows account.  
  
## See Also  
 [Identity and access control for replication](../../relational-databases/replication/security/identity-and-access-control-replication.md)   
 [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)  
  
  
