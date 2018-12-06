---
title: "Subscribers | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.newsubwizard.subscribers.f1"
helpviewer_keywords: 
  - "Subscribers [SQL Server replication]"
ms.assetid: 43fb2454-c220-4d25-a826-83c332eb00d2
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Subscribers
  Specify the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers that will receive a subscription to the selected publication.  
  
## Options  
 **Subscribers**  
 Select the check box in the grid to enable the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source as a Subscriber to the publication chosen on the **Publication** page. If the Subscriber is not listed, click **Add Subscriber** or **Add SQL Server Subscriber**.  
  
 **Subscription database**  
 The information displayed in and actions available from this column depend on the type of Subscriber listed in the **Subscribers** column:  
  
-   For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, select a subscription database from the **Subscription Database** list or create a new database by selecting the **New database** command from the same list.  
  
    > [!NOTE]  
    >  If you are enabling the Publisher as a Subscriber, the subscription database must be different from the publication database.  
  
-   For non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, the subscription database is not displayed. Specify the database, along with other connection information, in the **Data source name** field of the **Add Non-SQL Server** dialog box. This dialog box is available by clicking **Add Subscriber** and then clicking **Add Non-SQL Server Subscriber**.  
  
 **Add Subscriber**  
 Add a server to the list of servers that can be enabled as Subscribers. This button is displayed when all of the following conditions are true:  
  
-   The publication you selected is a snapshot or transactional publication that does not support updating subscriptions.  
  
    > [!NOTE]  
    >  If the publication you are subscribing to has [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] subscriptions and the publication is not already enabled for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, you cannot add a non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] subscription.  
  
-   The subscription is a push subscription.  
  
-   The Publisher of the selected publication is [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later.  
  
 Clicking **Add Subscriber** shows a menu with two choices: **Add SQL Server Subscriber** and **Add Non-SQL Server Subscriber**. Click **Add Non-SQL Server Subscriber** to add an Oracle or IBM DB2 Subscriber.  
  
 **Add SQL Server Subscriber**  
 Add a server to the list of servers that can be enabled as Subscribers. This button is displayed when one or more of the following conditions is true:  
  
-   The publication you selected is a merge publication, or a snapshot or transactional publication that supports updating subscriptions.  
  
-   The subscription is a pull subscription.  
  
-   The Publisher of the selected publication is earlier than [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. For earlier versions, the button is displayed only if one or more of the following conditions is true:  
  
    -   You are a member of the **sysadmin** fixed server role at the Publisher.  
  
    -   The Subscriber has been added on the **Subscribers** page of the **Publisher Properties** dialog box.  
  
    -   The publication allows anonymous subscriptions.  
  
## See Also  
 [Create a Pull Subscription](create-a-pull-subscription.md)   
 [Create a Push Subscription](create-a-push-subscription.md)   
 [Non-SQL Server Subscribers](non-sql/non-sql-server-subscribers.md)   
 [Subscribe to Publications](subscribe-to-publications.md)  
  
  
