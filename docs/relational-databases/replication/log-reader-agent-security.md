---
title: "Log Reader Agent Security | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.security.LRA.f1"
helpviewer_keywords: 
  - "Log Reader Agent Security dialog box"
ms.assetid: d6981e74-ddb8-41b8-9ea1-56c2ece63b8a
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Log Reader Agent Security
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **Log Reader Agent Security** dialog box allows you to specify:  
  
-   The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the Log Reader Agent runs at the Distributor. The Windows account is also referred to as the *process account*, because the agent process runs under this account.  
  
-   The context under which the Log Reader Agent makes connections to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. The connection can be made by impersonating the Windows account or under the context of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account you specify.  
  
    > [!NOTE]  
    >  The Log Reader Agent makes connections to the Publisher even if the Publisher and Distributor are on the same computer. The Log Reader Agent also makes connections to the Distributor; these connections are always made by impersonating the Windows account under which the agent runs.  
  
     For Oracle Publishers, specify the context under which the Log Reader Agent connects to the Publisher in the **Publisher Properties** dialog box (available from the **Distributor Properties** dialog box). For more information, see [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
 All accounts must be valid, with the correct password specified for each account. Accounts and passwords are not validated until an agent runs.  
  
## Options  
 **Process account**  
 Enter a Windows account under which the Log Reader Agent runs at the Distributor. The Windows account you specify must at minimum be a member of the **db_owner** fixed database role in the distribution database.  
  
 **Password** and **Confirm password**  
 Enter the password for the Windows account.  
  
 **Connect to the Publisher**  
 Select whether the Log Reader Agent should make connections to the Publisher by impersonating the account specified in the **Process account** text box or by using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account. If you select to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account, enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and password.  
  
> [!NOTE]  
>  [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you select to impersonate the Windows account rather than using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account.  
  
 The Windows account or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account used for the connection must at minimum be a member of the **db_owner** fixed database role in the publication database.  
  
## See Also  
 [Identity and Access control in Replication](../../relational-databases/replication/security/identity-and-access-control-replication.md)   
 [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)  
  
  
