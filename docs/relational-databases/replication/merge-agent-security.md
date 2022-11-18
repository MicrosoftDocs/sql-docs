---
description: "Merge Agent Security"
title: "Merge Agent Security | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.security.MA.f1"
helpviewer_keywords: 
  - "Merge Agent Security dialog box"
ms.assetid: 9b86171a-4381-4b39-869a-cdc161e7cd15
author: "MashaMSFT"
ms.author: "mathoma"
---
# Merge Agent Security
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **Merge Agent Security** dialog box allows you to specify the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which the Merge Agent runs. The Merge Agent runs at the Distributor for push subscriptions and at the Subscriber for pull subscriptions. The Windows account is also referred to as the *process account*, because the agent process runs under this account. Additional options available in the dialog box depend on how you access it:  
  
-   If the dialog box is accessed from the New Subscription Wizard, it also allows you to specify the context under which the Merge Agent makes connections to the Subscriber (for push subscriptions) or the Publisher and Distributor (for pull subscriptions). The connection can be made using the Windows account or under the context of a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account you specify.  
  
-   If the dialog box is accessed from the **Subscription Properties** dialog box, specify the context under which the Merge Agent makes connections by clicking the properties button (**...**) in the **Subscriber Connection** or **Publisher Connection** row of that dialog box. For more information about accessing the **Subscription Properties** dialog box, see [View and Modify Push Subscription Properties](../../relational-databases/replication/view-and-modify-push-subscription-properties.md) and how to: [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md).  
  
 All accounts must be valid, with the correct password specified for each account. Accounts and passwords are not validated until an agent runs.  
  
## Options  
 **Process Account**  
 Enter a Windows account under which the Merge Agent runs.  
  
-   For push subscriptions, the account must:  
  
    -   At minimum be a member of the **db_owner** fixed database role in the distribution database.  
  
    -   Be a member of the PAL.  
  
    -   Be a login associated with a user in the publication database.  
  
    -   Have read permissions on the snapshot share.  
  
-   For pull subscriptions, the account must at minimum be a member of the **db_owner** fixed database role in the subscription database.  
  
 Additional permissions are required if the process account is impersonated when connections are made. See the **Connect to the Publisher and Distributor** and **Connect to the Subscriber** sections below.  
  
 **Process Account** cannot be specified for pull subscriptions to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], because the Merge Agent does not run on instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)].  
  
 **Password** and **Confirm Password**  
 Enter the password for the Windows account.  
  
 **Connect to the Publisher and Distributor**  
 For push subscriptions, connections to the Publisher and Distributor are always made by impersonating the account specified in the **Process account** text box.  
  
 For pull subscriptions, select whether the Merge Agent should make connections to the Publisher and Distributor by impersonating the account specified in the **Process account** text box or by using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account. If you select to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account, enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and password.  
  
> [!NOTE]  
>  [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you select to impersonate the Windows account rather than using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account.  
  
 The Windows account or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account used for the connection must:  
  
-   Be a member of the PAL.  
  
-   Be a login associated with a user in the publication database.  
  
-   Be a login associated with a user in the distribution database (the user can be the Guest user).  
  
-   Have read permissions on the snapshot share.  
  
 **Connect to the Subscriber**  
 For pull subscriptions, connections to the Subscriber are always made by impersonating the account specified in the **Process account** text box.  
  
 For push subscriptions, select whether the Merge Agent should make connections to the Publisher and Distributor by impersonating the account specified in the **Process account** text box or by using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account. If you select to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account, enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and password.  
  
> [!NOTE]  
>  It is recommended that you select to impersonate the Windows account rather than using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account.  
  
 The Windows account or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account used for the connection to the Subscriber must at minimum be a member of the **db_owner** fixed database role in the subscription database.  
  
## See Also  
 [Manage Logins and Passwords in Replication](../../relational-databases/replication/security/identity-and-access-control-replication.md)   
 [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  
