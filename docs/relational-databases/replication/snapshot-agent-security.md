---
description: "Snapshot Agent Security"
title: "Snapshot Agent Security | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.security.SSA.f1"
helpviewer_keywords: 
  - "Snapshot Agent Security dialog box"
ms.assetid: 64e84c67-acc6-4906-98d4-3451767363fe
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Snapshot Agent Security
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The **Snapshot Agent Security** dialog box allows you to specify:  
  
-   The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the Snapshot Agent runs at the Distributor. The Windows account is also referred to as the *process account*, because the agent process runs under this account.  
  
-   The context under which the Snapshot Agent makes connections to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. The connection can be made by impersonating the Windows account or under the context of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account you specify.  
  
    > [!NOTE]  
    >  The Snapshot Agent makes connections to the Publisher even if the Publisher and Distributor are on the same computer. The Snapshot Agent also makes connections to the Distributor; these connections are always made by impersonating the Windows account under which the agent runs.  
  
     For Oracle Publishers, specify the context under which the Snapshot Agent connects to the Publisher in the **Publisher Properties** dialog box (available from the **Distributor Properties** dialog box). For more information, see [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
 All accounts must be valid, with the correct password specified for each account. Accounts and passwords are not validated until an agent runs.  
  
## Options  
 **Process account**  
 Enter a Windows account under which the Snapshot Agent runs at the Distributor. The Windows account you specify must:  
  
-   At minimum be a member of the **db_owner** fixed database role in the distribution database.  
  
-   Have write permissions on the snapshot share.  
  
 **Password** and **Confirm password**  
 Enter the password for the Windows account.  
  
 **Connect to the Publisher**  
 Select whether the Snapshot Agent should make connections to the Publisher by impersonating the account specified in the **Process account** text box or by using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account. If you select to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account, enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and password.  
  
> [!NOTE]  
>  It is recommended that you select to impersonate the Windows account rather than using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account.  
  
 The Windows account or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account used for the connection must at minimum be a member of the **db_owner** fixed database role in the publication database.  
  
## See Also  
 [Identity and access control for replication](../../relational-databases/replication/security/identity-and-access-control-replication.md)   
 [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)  
  
  
