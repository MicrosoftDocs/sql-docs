---
description: "Specify Merge Replication properties"
title: "Specify Merge Replication properties| Microsoft Docs"
ms.custom: ""
ms.date: "11/20/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "merge replication [SQL Server replication], download-only articles"
  - "articles [SQL Server replication], download-only"
  - "download-only articles"
ms.assetid: 14839cec-6dbf-49c2-aa27-56847b09b4db
author: "MashaMSFT"
ms.author: "mathoma"
---
# Specify Merge Replication properties
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
This topic explains how to specify various properties for your merge replication. 

## Merge Article is Download-Only
Download-only articles are designed for applications with data that is not updated at Subscribers. For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md).  
   
###  Considerations   
-   If you specify that an article is download-only after subscriptions have been initialized, all client subscriptions that received the article must be reinitialized. Server subscriptions do not have to be reinitialized. For more information on the effects of property changes, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
### Use SQL Server Management Studio  
  
#### On the Articles page
On the **Articles** page of the New Publication Wizard select a table, and then select the checkbox **Highlighted table is download-only**.  
  
#### On the Properties tab of the Article Properties  
  
1.  On the **Articles** page of the New Publication Wizard or the **Publication Properties - \<Publication>** dialog box, select a table, and then click **Article Properties**.    
2.  Click **Set Properties of Highlighted Table Article** or **Set Properties of All Table Articles**.  
  
3.  In the **Destination Object** section of the **Properties** tab of the **Article Properties - \<Article>** dialog box, specify one of the following values for **Synchronization direction**:    
    -   **Download to Subscriber, prohibit Subscriber changes**    
    -   **Download to Subscriber, allow Subscriber changes**    
4.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  

###  Use Transact-SQL  
  
#### New article  
  
1.  Execute [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md), specifying a value of **1** or **2** for the parameter `@subscriber_upload_options`. The numbers correspond to the following behavior:  
  
    -   **0** - No restrictions (default). Changes made at the Subscriber are uploaded to the Publisher.    
    -   **1** - Changes are allowed at the Subscriber, but they are not uploaded to the Publisher.    
    -   **2** - Changes are not allowed at the Subscriber.  
  
       > [!NOTE]  
       > If the source table for an article is already published in another publication, the value of `@subscriber_upload_options` must be the same for both articles.  
  
#### Existing article
  
1.  To determine if an article is download-only, execute [sp_helpmergearticle](../../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md) and verify value of  **upload_options** for the article in the result set. 
  
2.  If the value returned in step 1 is **0**, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying a value of **subscriber_upload_options** for `@property`, a value of **1** for `@force_invalidate_snapshot` and `@force_reinit_subscription`, and a value of **1** or **2** for `@value`, which corresponds to the following behavior:  
  
    -   **1** - Changes are allowed at the Subscriber, but they are not uploaded to the Publisher.    
    -   **2** - Changes are not allowed at the Subscriber.  
  
        > [!NOTE]  
        >  If the source table for an article is already published in another publication, the download-only behavior must be the same for both articles.  
  
## Interactive Conflict Resolution 
  
 Microsoft SQL Server replication provides an Interactive Resolver, which allows you to resolve conflicts manually during on-demand synchronization in Microsoft Windows Synchronization Manager. After interactive resolution is enabled, resolve conflicts interactively during synchronization, using the Interactive Resolver. The Interactive Resolver is available through the Microsoft Windows Synchronization Manager. For more information, see [Synchronize a Subscription Using Windows Synchronization Manager &#40;Windows Synchronization Manager&#41;](../../../relational-databases/replication/synchronize-a-subscription-using-windows-synchronization-manager.md).  
  
  
### <a name="Recommendations"></a> Recommendations  
  
-   If a synchronization is performed outside of Windows Synchronization Manager (as a scheduled synchronization or an on demand synchronization in SQL Server Management Studio or Replication Monitor), conflicts are resolved automatically without user intervention, using the default conflict resolution specified for the article. For more information, see [Interactive Conflict Resolution](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-interactive-resolution.md).  
  
### Use SQL Server Management Studio  
  
#### Enable interactive conflict resolution for an article  
  
1.  On the **Articles** page of the New Publication Wizard or the **Publication Properties - \<Publication>** dialog box, select a table. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).   
2.  Click **Article Properties**, and then click **Set Properties of Highlighted Table Article** or **Set Properties of All Table Articles**.    
3.  On the **Article Properties - \<Article>** or **Article Properties - \<ArticleType>** page, click the **Resolver** tab.    
4.  Select **Allow Subscriber to resolve conflicts interactively during on-demand synchronization**.    
5.  Select **OK**.
6.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
#### Specify that a subscription should use interactive conflict resolution  
  
1.  In the **Subscription Properties - \<Subscriber>: \<SubscriptionDatabase>** dialog box, specify a value of **True** for the **Resolve conflicts interactively** option. For more information about accessing this dialog box, see [View and Modify Push Subscription Properties](../../../relational-databases/replication/view-and-modify-push-subscription-properties.md) and [View and Modify Pull Subscription Properties](../../../relational-databases/replication/view-and-modify-pull-subscription-properties.md).   
2.  Select **OK**.
  
###  Use Transact-SQL  
 You can programmatically specify that a Subscriber will use this graphical interface to resolve article conflicts when a pull subscription to a merge publication is created. Only conflicts in articles that support this option will be displayed in the Interactive Resolver.  
  
#### Create a merge pull subscription that uses the Interactive Resolver  
  
1.  At the Publisher on the publication database, execute [sp_helpmergearticle](../../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md), specifying `@publication`. Note the value of **allow_interactive_resolver** for each article in the result set for which the Interactive Resolver will be used.   
    -   If this value is **1**, the Interactive Resolver will be used.    
    -   If this value is **0**, you must first enable the Interactive Resolver for each article. To do this, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying `@publication`, `@article`, a value of **allow_interactive_resolver** for `@property`, and a value of **true** for `@value`.    
2.  At the Subscriber on the subscription database, execute [sp_addmergepullsubscription](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql.md). For more information, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md).    
3.  At the Subscriber on the subscription database, execute [sp_addmergepullsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md), specifying the following parameters:    
    -   `@publisher`, `@publisher_db` (the published database), and `@publication`.    
    -   A value of **true** for `@enabled_for_syncmgr`.    
    -   A value of **true** for `@use_interactive_resolver`.    
    -   The security account information required by the Merge Agent. For more information, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md).    
4.  At the Publisher on the publication database, execute [sp_addmergesubscription](../../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md).  
  
#### Define an article that supports the Interactive Resolver  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md). Specify the name of the publication to which the article belongs for `@publication`, a name for the article for `@article`, the database object being published for `@source_object`, and a value of **true** for `@allow_interactive_resolver`. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
 
## Conflict Tracking and Resolution Level for Merge Articles
This topic describes how to specify the conflict tracking and resolution level for merge articles in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 When a subscription to a merge publication is synchronized, replication checks for conflicts caused by changes to the same data made at both the Publisher and the Subscriber. You can specify whether conflicts are detected at the row-level, where any change to the row is considered a conflict, or column-level, where only changes to the same row and column are considered a conflict. Conflict resolution for articles is performed at the row-level. For more information about conflict detection and resolution when logical records are used, see [Detecting and Resolving Conflicts in Logical Records](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-resolving-in-logical-record.md).  
 
### Limitations and Restrictions  
  
-   If you modify the tracking level after subscriptions have been initialized, those subscriptions must be reinitialized. For more information about the effects of property changes, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).   
-   With row- and column-level tracking, conflict resolution is always performed at the row-level: the winning row overwrites the losing row. Merge replication also allows you to specify that conflicts be tracked and resolved at the logical record level, but these options are not available from [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]. For information about setting these options from replication stored procedures, see [Define a Logical Record Relationship Between Merge Table Articles](../../../relational-databases/replication/publish/define-a-logical-record-relationship-between-merge-table-articles.md).  
  
### Use SQL Server Management Studio  
 Specify row- or column-level tracking for merge articles on the **Properties** tab of the **Article Properties** dialog box, which is available in the New Publication Wizard and the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
#### Specify row- or column-level tracking  
  
1.  On the **Articles** page of the New Publication Wizard or the **Publication Properties - \<Publication>** dialog box, select a table.  
2.  Click **Article Properties**, and then click **Set Properties of Highlighted Table Article** or **Set Properties of All Table Articles**.   
3.  On the **Properties** tab of the **Article Properties \<Article>** dialog box, select one of the following values for the **Tracking level** property: **Row-level tracking** or **Column-level tracking**.   
4.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
### Use Transact-SQL  
  
#### To specify conflict tracking options for a new merge article  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) and specify one of the following values for `@column_tracking`:  
  
    -   **true** - Use column-level tracking for the article.    
    -   **false** - Use row-level tracking, which is the default.  
  
#### Change conflict tracking options for a merge article  
  
1.  To determine the conflict tracking options for a merge article, execute [sp_helpmergearticle](../../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md). Note the value of the **column_tracking** option in the result set for the article. A value of **1** means that column-level tracking is being used, and a value of **0** means that row-level tracking is being used.    
2.  At the Publisher on the publication database, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of **column_tracking** for `@property` and one of the following values for `@value`:  
  
    -   **true** - Use column-level tracking for the article.    
    -   **false** - Use row-level tracking, which is the default.  
  
     Specify a value of **1** for both `@force_invalidate_snapshot` and `@force_reinit_subscription`. 

## Manage tracking  deletes
    
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
 By default, merge replication synchronizes DELETE commands between the Publisher and Subscriber. Merge replication enables you to retain rows in the subscription database even when they have been deleted from the publication, and vice versa. You can programmatically specify that DELETE commands be ignored when creating a new article or you can enable this functionality at a later time using replication stored procedures.  
  
> [!IMPORTANT]  
>  Enabling this functionality will result in non-convergence, which means that data present at the Subscriber will not accurately reflect data at the Publisher. You must implement your own mechanism for manually removing deleted rows.  
  
### Specify that deletes be ignored for a new merge article  
  
At the Publisher on the publication database, execute [sp_addmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md). Specify a value of **false** for `@delete_tracking`. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).
  
> [!NOTE]  
>  If the source table for an article is already published in another publication, the value of **delete_tracking** must be the same for both articles.  
  
### Specify that deletes be ignored for an existing merge article  
  
1.  To determine if error compensation is enabled for an article, execute [sp_helpmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md) and note the value of **delete_tracking** in the result set. If this value is **0**, deletes are already being ignored.  
  
2.  If the value from step 1 is **1**, execute [sp_changemergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md) at the Publisher on the publication database. Specify a value of `delete_tracking` for `@property`, and a value of **false** for `@value`.  
  
    > [!NOTE]  
    >  If the source table for an article is already published in another publication, the value of **delete_tracking** must be the same for both articles.  
  

## Processing Order
  Merge replication enables you to specify the order in which articles are processed by the Merge Agent during the synchronization process. You can assign an order to each article programmatically when creating an article using replication stored procedures. Articles are processed in order from lowest to highest value. If two articles have the same value, they are processed concurrently. 

### How Processing Order is Determined  
 During merge synchronization, articles are, by default, processed in the order required by the dependencies between objects, including the declarative referential integrity (DRI) constraints defined on the base tables. Processing involves enumerating the changes to a table and then applying those changes. If no DRI is present but join filters or logical records exist between table articles, the articles are processed in the order required by the filters and logical records. Articles not related to any other article through DRI, join filters, logical records, or other dependencies are processed according to the article nickname in the [sysmergearticles &#40;Transact-SQL&#41;](../../../relational-databases/system-tables/sysmergearticles-transact-sql.md) system table.  
  
 Consider a publication that includes the tables **SalesOrderHeader** and **SalesOrderDetail** with a primary key column **SalesOrderID** in the **SalesOrderHeader** table and a corresponding foreign key column **SalesOrderID** in the **SalesOrderDetail** table. During synchronization, merge replication prevents foreign key violations by inserting any new rows in **SalesOrderHeader** before inserting associated rows in **SalesOrderDetail**. Similarly, rows are deleted from **SalesOrderDetail** before the associated row is deleted from **SalesOrderHeader**.  
  
 However, in some applications referential integrity is enforced through database triggers, or at the application level, rather than through DRI. Given the publication described above, instead of DRI, the **SalesOrderDetail** table could have an insert trigger that ensures the associated row in the **SalesOrderHeader** table exists before allowing an insert. **SalesOrderHeader** could have a delete trigger that ensures there are no associated rows in **SalesOrderDetail** before allowing a delete. Merge replication does not take into account triggers when determining processing order of articles because it cannot determine what the result of the trigger will be until it has fired. Similarly, replication cannot take into account constraints defined at the application level.  
  
 When referential integrity is maintained through triggers or at the application level, you should specify the order in which the articles should be processed. In the example with triggers, you would specify that the **SalesOrderHeader** table should be processed before **SalesOrderDetail**, because article ordering is based on insert order. Merge replication will automatically reverse the order for deletes. Merge replication will not fail without article ordering, because the Merge Agent continues to process articles if a constraint violation occurs; it then retries any operations that failed after other articles have been processed. Specifying article order simply avoids retries and the additional processing associated with them. If you specify an incorrect order (for example, one that results in detail records being processed before header records), merge replication will retry processing until it succeeds.   
  
### New article
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md). Specify an integer value that represents the processing order for the article for `@processing_order`. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
    > [!NOTE]  
    >  When creating ordered articles, you should leave gaps between the article order values. This makes it easier to set new values in the future. For example, if you have three articles for which you need to specify a fixed processing order, set the value of `@processing_order` to 10, 20, and 30 rather than 1, 2, and 3, respectively.  
  
### Existing article
  
1.  To determine processing order of an article, execute [sp_helpmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md) and note the value of **processing_order** in the result set.   
2.  At the Publisher on the publication database, execute [sp_changemergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of **processing_order** for `@property` and an integer value that represents the processing order for `@value`.  


## See Also  
 [Optimize Merge Replication Performance with Download-Only Articles](../../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md)   
 [Define an Article](../../../relational-databases/replication/publish/define-an-article.md)   
 [View and Modify Article Properties](../../../relational-databases/replication/publish/view-and-modify-article-properties.md)  
  
  
