---
title: "View and Modify Article Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_changearticle"
  - "sp_helparticle"
  - "viewing replication properties"
  - "sp_changemergearticle"
  - "sp_helpmergearticle"
  - "modifying replication properties, articles"
  - "articles [SQL Server replication], modifying"
  - "articles [SQL Server replication], properties"
ms.assetid: e71831fa-3d39-4e4a-9706-4d3a497082cc
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# View and Modify Article Properties
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to view and modify article properties in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
-   **To view and modify article properties, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Some properties cannot be modified after a publication has been created, and others cannot be modified if there are subscriptions to the publication. Properties that cannot be modified are displayed as read-only.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   After a publication is created, some property changes require a new snapshot. If a publication has subscriptions, some changes also require all subscriptions to be reinitialized. For more information, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md) and [Add Articles to and Drop Articles from Existing Publications](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 View and modify article properties in the **Publication Properties - \<Publication>** dialog box, which is available in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] and Replication Monitor. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
-   The **General** page includes the publication name and description, the database name, the type of publication, and the subscription expiration settings.  
  
-   The **Articles** page corresponds to the **Articles** page in the New Publication Wizard. Use this page to add and delete articles, and to change properties and column filtering for articles.  
  
-   The **Filter Rows** page corresponds to the **Filter Table Rows** page in the New Publication Wizard. Use this page to add, edit, and delete static row filters for all types of publications, and to add, edit, and delete parameterized row filters and join filters for merge publications.  
  
-   The **Snapshot** page allows you to specify the format and location of the snapshot, whether the snapshot should be compressed, and scripts to run before and after the snapshot is applied.  
  
-   The **FTP Snapshot** page (for snapshot and transactional publications, and merge publications for Publishers running versions prior to SQL Server 2005) allows you to specify whether Subscribers can download snapshot files through File Transfer Protocol (FTP).  
  
-   The **FTP Snapshot and Internet** page (for merge publications from Publishers running SQL Server 2005 or later) allows you to specify whether Subscribers can download snapshot files through FTP, and whether Subscribers can synchronize subscriptions through HTTPS.  
  
-   The **Subscription Options** page allows you to set a number of options that apply to all subscriptions. The options differ depending on the type of publication.  
  
-   The **Publication Access List** page allows you to specify which logins and groups can access a publication.  
  
-   The **Agent Security** page allows you to access settings for the accounts under which the following agents run and make connections to the computers in a replication topology: the Snapshot Agent for all publications; the Log Reader Agent for all transactional publications; and the Queue Reader Agent for transactional publications that allow queued updating subscriptions.  
  
-   The **Data Partitions** page (for merge publications from Publishers running SQL Server 2005 or later) allows you to specify whether Subscribers to publications with parameterized filters can request a snapshot if one is not available. It also allows you to generate snapshots for one or more partitions, either once or on a recurring schedule.  
  
#### To view and modify article properties  
  
1.  On the **Articles** Page of the **Publication Properties - \<Publication>** dialog box, select an article, and then click **Article Properties**.  
  
2.  Select which articles property changes should apply to:  
  
    -   Click **Set Properties of Highlighted \<ObjectType> Article** to launch the **Article Properties - \<ObjectName>** dialog box; property changes made in this dialog box are applied only to the object that is highlighted in the object pane on the **Articles** page.  
  
    -   Click **Set Properties of All \<ObjectType> Articles**, to launch the **Properties for All \<ObjectType> Articles** dialog box; property changes made in this dialog box are applied to all objects of that type in the object pane on the **Articles** page, including ones not yet selected for publication.  
  
        > [!NOTE]  
        >  Property changes made in the **Properties for All \<ObjectType> Articles** dialog box override any made previously in the **Article Properties - \<ObjectName>** dialog box. If, for example, you want to set a number of defaults for all articles of an object type, but also want to set some properties for individual objects, set the defaults for all articles first. Then set the properties for the individual objects.  
  
3.  Modify any properties if necessary, and then click **OK**.  
  
4.  Click **OK** on the **Publication Properties - \<Publication>** dialog box.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Articles can be modified and their properties returned programmatically using replication stored procedures. The stored procedures that you use depend on the type of publication to which the article belongs.  
  
#### To view the properties of an article belonging to a snapshot or transactional publication  
  
1.  Execute [sp_helparticle](../../../relational-databases/system-stored-procedures/sp-helparticle-transact-sql.md), specifying the name of the publication for the **@publication** parameter and the name of the article for the **@article** parameter. If you do not specify **@article**, information will be returned for all articles in the publication.  
  
2.  Execute [sp_helparticlecolumns](../../../relational-databases/system-stored-procedures/sp-helparticlecolumns-transact-sql.md) for table articles to list all columns available in the base table.  
  
#### To modify the properties of an article belonging to a snapshot or transactional publication  
  
1.  Execute [sp_changearticle](../../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md), specifying the article property being changed in the **@property** parameter and the new value of this property in the **@value** parameter.  
  
    > [!NOTE]  
    >  If the change requires the generation of a new snapshot, you must also specify a value of **1** for **@force_invalidate_snapshot**, and if the change requires that Subscribers be reinitialized, you must also specify a value of **1** for **@force_reinit_subscription**. For more information on the properties that, when changed, require a new snapshot or reinitialization, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
#### To view the properties of an article belonging to a merge publication  
  
1.  Execute [sp_helpmergearticle](../../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md), specifying the name of the publication for the **@publication** parameter and the name of the article for the **@article** parameter. If you do not specify these parameters, information will be returned for all articles in a publication or at the publisher.  
  
2.  Execute [sp_helpmergearticlecolumn](../../../relational-databases/system-stored-procedures/sp-helpmergearticlecolumn-transact-sql.md) for table articles to list all columns available in the base table.  
  
#### To modify the properties of an article belonging to a merge publication  
  
1.  Execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying the article property being changed in the **@property** parameter and the new value of this property in the **@value** parameter.  
  
    > [!NOTE]  
    >  If the change requires the generation of a new snapshot, you must also specify a value of **1** for **@force_invalidate_snapshot**, and if the change requires that Subscribers be reinitialized, you must also specify a value of **1** for **@force_reinit_subscription**. For more information on the properties that, when changed, require a new snapshot or reinitialization, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 This transactional replication example returns the properties of the published article.  
  
 [!code-sql[HowTo#sp_helptranarticle](../../../relational-databases/replication/codesnippet/tsql/view-and-modify-article-_1.sql)]  
  
 This transactional replication example changes the schema options for the published article.  
  
 [!code-sql[HowTo#sp_changetranarticle](../../../relational-databases/replication/codesnippet/tsql/view-and-modify-article-_2.sql)]  
  
 This merge replication example returns the properties of the published article.  
  
 [!code-sql[HowTo#sp_helpmergearticle](../../../relational-databases/replication/codesnippet/tsql/view-and-modify-article-_3.sql)]  
  
 This merge replication example changes the conflict detection settings for a published article.  
  
 [!code-sql[HowTo#sp_changemergearticle](../../../relational-databases/replication/codesnippet/tsql/view-and-modify-article-_4.sql)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 You can modify articles and access their properties programmatically by using Replication Management Objects (RMO). The RMO classes you use to view or modify article properties depend on the type of publication to which the article belongs.  
  
#### To view or modify properties of an article that belongs to a snapshot or transactional publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransArticle> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Article.Name%2A>, <xref:Microsoft.SqlServer.Replication.Article.PublicationName%2A>, and <xref:Microsoft.SqlServer.Replication.Article.DatabaseName%2A> properties.  
  
4.  Set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the article properties in step 3 were defined incorrectly or the article does not exist.  
  
6.  (Optional) To change properties, set a new value for one of the <xref:Microsoft.SqlServer.Replication.TransArticle> properties that can be set.  
  
7.  (Optional) If you specified a value of **true** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A>, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit changes on the server. If you specified a value of **false** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> (the default), changes are sent to the server immediately.  
  
#### To view or modify properties of an article that belongs to a merge publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeArticle> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Article.Name%2A>, <xref:Microsoft.SqlServer.Replication.Article.PublicationName%2A>, and <xref:Microsoft.SqlServer.Replication.Article.DatabaseName%2A> properties.  
  
4.  Set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the article properties in step 3 were defined incorrectly or the article does not exist.  
  
6.  (Optional) To change properties, set a new value for one of the <xref:Microsoft.SqlServer.Replication.MergeArticle> properties that can be set.  
  
7.  (Optional) If you specified a value of **true** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A>, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit changes on the server. If you specified a value of **false** for <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> (the default), changes are sent to the server immediately.  
  
###  <a name="PShellExample"></a> Example (RMO)  
 This example changes a merge article to specify the business logic handler used by the article.  
  
 [!code-cs[HowTo#rmo_ChangeMergeArticle_BLH](../../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_changemergearticle_blh)]  
  
 [!code-vb[HowTo#rmo_vb_ChangeMergeArticle_BLH](../../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_changemergearticle_blh)]  
  
## See Also  
 [Implement a Business Logic Handler for a Merge Article](../../../relational-databases/replication/implement-a-business-logic-handler-for-a-merge-article.md)   
 [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)   
 [Advanced Merge Replication Conflict Detection and Resolution](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)  
  
  
