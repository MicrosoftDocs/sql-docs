---
title: "Queue Reader Agent Security | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.security.QRA.f1"
helpviewer_keywords: 
  - "Queue Reader Agent Security dialog box"
ms.assetid: 77938da0-2afd-4455-8826-f4a6a9440cb3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Queue Reader Agent Security
  The **Queue Reader Agent Security** dialog box allows you to specify the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the Queue Reader Agent runs and makes local connections to the Distributor. The agent connects to the Publisher using the account specified in the **Publisher Properties** dialog box (available from the **Distributor Properties** dialog box); the agent connects to the Subscriber using the same context as the Distribution Agent for the subscription. For more information, see [View and Modify Replication Security Settings](security/view-and-modify-replication-security-settings.md).  
  
 The account must be valid with the correct password specified. Accounts and passwords are not validated until an agent runs.  
  
## Options  
 **Process account**  
 Enter a Windows account under which the Queue Reader Agent runs at the Distributor. The Windows account you specify must at minimum be a member of the **db_owner** fixed database role in the distribution database.  
  
 **Password** and **Confirm password**  
 Enter the password for the Windows account.  
  
## See Also  
 [Manage Logins and Passwords in Replication](security/identity-and-access-control-replication.md#manage-logins-and-passwords-in-replication)   
 [Replication Agent Security Model](security/replication-agent-security-model.md)   
 [Replication Agents Overview](agents/replication-agents-overview.md)   
 [Replication Security Best Practices](security/replication-security-best-practices.md)  
  
  
