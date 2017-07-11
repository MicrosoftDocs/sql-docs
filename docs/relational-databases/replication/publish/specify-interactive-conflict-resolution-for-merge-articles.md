---
title: "Specify Interactive Conflict Resolution for Merge Articles | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "merge replication conflict resolution [SQL Server replication], interactive resolvers"
  - "interactive conflict resolution [SQL Server replication]"
  - "articles [SQL Server replication], conflict resolution"
  - "conflict resolution [SQL Server replication], merge replication"
ms.assetid: e298dea0-b5ef-4907-a745-cfad9793653f
caps.latest.revision: 40
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Specify Interactive Conflict Resolution for Merge Articles
  This topic describes how to specify interactive conflict resolution for merge articles in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication provides an Interactive Resolver, which allows you to resolve conflicts manually during on-demand synchronization in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows Synchronization Manager. After interactive resolution is enabled, resolve conflicts interactively during synchronization, using the Interactive Resolver. The Interactive Resolver is available through the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows Synchronization Manager. For more information, see [Synchronize a Subscription Using Windows Synchronization Manager &#40;Windows Synchronization Manager&#41;](../../../relational-databases/replication/synchronize-a-subscription-using-windows-synchronization-manager.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
-   **To specify interactive conflict resolution for merge articles, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   If a synchronization is performed outside of Windows Synchronization Manager (as a scheduled synchronization or an on demand synchronization in SQL Server Management Studio or Replication Monitor), conflicts are resolved automatically without user intervention, using the default conflict resolution specified for the article. For more information, see [Interactive Conflict Resolution](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-interactive-resolution.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To enable interactive conflict resolution for an article  
  
1.  On the **Articles** page of the New Publication Wizard or the **Publication Properties - \<Publication>** dialog box, select a table. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
2.  Click **Article Properties**, and then click **Set Properties of Highlighted Table Article** or **Set Properties of All Table Articles**.  
  
3.  On the **Article Properties - \<Article>** or **Article Properties - \<ArticleType>** page, click the **Resolver** tab.  
  
4.  Select **Allow Subscriber to resolve conflicts interactively during on-demand synchronization**.  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
6.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
#### To specify that a subscription should use interactive conflict resolution  
  
1.  In the **Subscription Properties - \<Subscriber>: \<SubscriptionDatabase>** dialog box, specify a value of **True** for the **Resolve conflicts interactively** option. For more information about accessing this dialog box, see [View and Modify Push Subscription Properties](../../../relational-databases/replication/view-and-modify-push-subscription-properties.md) and [View and Modify Pull Subscription Properties](../../../relational-databases/replication/view-and-modify-pull-subscription-properties.md).  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 You can programmatically specify that a Subscriber will use this graphical interface to resolve article conflicts when a pull subscription to a merge publication is created. Only conflicts in articles that support this option will be displayed in the Interactive Resolver.  
  
#### To create a merge pull subscription that uses the Interactive Resolver  
  
1.  At the Publisher on the publication database, execute [sp_helpmergearticle](../../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md), specifying **@publication**. Note the value of **allow_interactive_resolver** for each article in the result set for which the Interactive Resolver will be used.  
  
    -   If this value is **1**, the Interactive Resolver will be used.  
  
    -   If this value is **0**, you must first enable the Interactive Resolver for each article. To do this, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying **@publication**, **@article**, a value of **allow_interactive_resolver** for **@property**, and a value of **true** for **@value**.  
  
2.  At the Subscriber on the subscription database, execute [sp_addmergepullsubscription](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql.md). For more information, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md).  
  
3.  At the Subscriber on the subscription database, execute [sp_addmergepullsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md), specifying the following parameters:  
  
    -   **@publisher**, **@publisher_db** (the published database), and **@publication**.  
  
    -   A value of **true** for **@enabled_for_syncmgr**.  
  
    -   A value of **true** for **@use_interactive_resolver**.  
  
    -   The security account information required by the Merge Agent. For more information, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md).  
  
4.  At the Publisher on the publication database, execute [sp_addmergesubscription](../../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md).  
  
#### To define an article that supports the Interactive Resolver  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md). Specify the name of the publication to which the article belongs for **@publication**, a name for the article for **@article**, the database object being published for **@source_object**, and a value of **true** for **@allow_interactive_resolver**. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
## See Also  
 [View and Resolve Data Conflicts for Merge Publications &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/view-and-resolve-data-conflicts-for-merge-publications.md)   
 [Interactive Conflict Resolution](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-interactive-resolution.md)  
  
  