---
title: "Define an Article | Microsoft Docs"
ms.custom: ""
ms.date: "06/30/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "articles [SQL Server replication], defining"
  - "sp_addmergearticle"
  - "adding articles"
  - "sp_addarticle"
  - "articles [SQL Server replication], adding"
ms.assetid: 220584d8-b291-43ae-b036-fbba3cc07a2e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Define an Article
  This topic describes how to define an article in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To define an article, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Article names cannot include any of the following characters: % , * , [ , ] , | , : , " , ? , ' , \ , / , \< , >. If objects in the database include any of these characters and you want to replicate them, you must specify an article name that is different from the object name.  
  
##  <a name="Security"></a> Security  
 When possible, prompt users to enter security credentials at runtime. If you must store credentials, use the [cryptographic services](https://go.microsoft.com/fwlink/?LinkId=34733) provided by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows .NET Framework.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Create publications and define articles with the New Publication Wizard. After a publication is created, view and modify publication properties in the **Publication Properties - \<Publication>** dialog box. For information about creating a publication from an Oracle database, see [Create a Publication from an Oracle Database](create-a-publication-from-an-oracle-database.md).  
  
#### To create a publication and define articles  
  
1.  Connect to the Publisher in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then right-click the **Local Publications** folder.  
  
3.  Click **New Publication**.  
  
4.  Follow the pages in the New Publication Wizard to:  
  
    -   Specify a Distributor if distribution has not been configured on the server. For more information about configuring distribution, see [Configure Publishing and Distribution](../configure-publishing-and-distribution.md).  
  
         If you specify on the **Distributor** page that the Publisher server will act as its own Distributor (a local Distributor), and the server is not configured as a Distributor, the New Publication Wizard will configure the server. You will specify a default snapshot folder for the Distributor on the **Snapshot Folder** page. The snapshot folder is simply a directory that you have designated as a share; agents that read from and write to this folder must have sufficient permissions to access it. For more information about securing the folder appropriately, see [Secure the Snapshot Folder](../security/secure-the-snapshot-folder.md).  
  
         If you specify that another server should act as the Distributor, you must enter a password on the **Administrative Password** page for connections made from the Publisher to the Distributor. This password must match the password specified when the Publisher was enabled at the remote Distributor.  
  
         For more information, see [Configure Distribution](../configure-distribution.md).  
  
    -   Choose a publication database.  
  
    -   Select a publication type. For more information, see [Types of Replication](../types-of-replication.md).  
  
    -   Specify data and database objects to publish; optionally filter columns from table articles, and set article properties.  
  
    -   Optionally filter rows from table articles. For more information, see [Filter Published Data](filter-published-data.md).  
  
    -   Set the Snapshot Agent schedule.  
  
    -   Specify the credentials under which the following replication agents run and make connections:  
  
         \- Snapshot Agent for all publications.  
  
         \- Log Reader Agent for all transactional publications.  
  
         \- Queue Reader Agent for transactional publications that allow updating subscriptions.  
  
         For more information, see [Replication Agent Security Model](../security/replication-agent-security-model.md) and [Replication Security Best Practices](../security/replication-security-best-practices.md).  
  
    -   Optionally script the publication. For more information, see [Scripting Replication](../scripting-replication.md).  
  
    -   Specify a name for the publication.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 After a publication has been created, articles can be created programmatically using replication stored procedures. The stored procedures used to create an article will depend on the type of publication for which the article is being defined. For more information, see [Create a Publication](create-a-publication.md).  
  
#### To define an article for a Snapshot or Transactional Publication  
  
1.  At the Publisher on the publication database, execute [sp_addarticle](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql). Specify the name of the publication to which the article belongs for **@publication**, a name for the article for **@article**, the database object being published for **@source_object**, and any other optional parameters. Use **@source_owner** to specify the schema ownership of the object, if not **dbo**. If the article is not a log-based table article, specify the article type for **@type**; for more information, see [Specify Article Types &#40;Replication Transact-SQL Programming&#41;](specify-article-types-replication-transact-sql-programming.md).  
  
2.  To horizontally filter rows in a table or view an article, use [sp_articlefilter](/sql/relational-databases/system-stored-procedures/sp-articlefilter-transact-sql) to define the filter clause. For more information, see [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md).  
  
3.  To vertically filter columns in a table or view an article, use [sp_articlecolumn](/sql/relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql). For more information, see [Define and Modify a Column Filter](define-and-modify-a-column-filter.md).  
  
4.  Execute [sp_articleview](/sql/relational-databases/system-stored-procedures/sp-articleview-transact-sql) if the article is filtered.  
  
5.  If the publication has existing subscriptions and [sp_helppublication](/sql/relational-databases/system-stored-procedures/sp-helppublication-transact-sql) returns a value of **0** in the **immediate_sync** column, you must call [sp_addsubscription](/sql/relational-databases/system-stored-procedures/sp-addsubscription-transact-sql) to add the article to each existing subscription.  
  
6.  If the publication has existing pull subscriptions, execute [sp_refreshsubscriptions](/sql/relational-databases/system-stored-procedures/sp-refreshsubscriptions-transact-sql) at the Publisher to create a new snapshot for existing pull subscriptions that contains just the new article.  
  
    > [!NOTE]  
    >  For subscriptions that are not initialized using a snapshot, you do not need to execute [sp_refreshsubscriptions](/sql/relational-databases/system-stored-procedures/sp-refreshsubscriptions-transact-sql) as this procedure is executed by [sp_addarticle](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql).  
  
#### To define an article for a Merge Publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](/sql/relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql). Specify the name of the publication for **@publication**, a name for the article name for **@article**, and the object being published for **@source_object**. To horizontally filter table rows, specify a value for **@subset_filterclause**. For more information, see [Define and Modify a Parameterized Row Filter for a Merge Article](define-and-modify-a-parameterized-row-filter-for-a-merge-article.md) and [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md). If the article is not a table article, specify the article type for **@type**. For more information, see [Specify Article Types &#40;Replication Transact-SQL Programming&#41;](specify-article-types-replication-transact-sql-programming.md).  
  
2.  (Optional) At the Publisher on the publication database, execute [sp_addmergefilter](/sql/relational-databases/system-stored-procedures/sp-addmergefilter-transact-sql) to define a join filter between two articles. For more information, see [Define and Modify a Join Filter Between Merge Articles](define-and-modify-a-join-filter-between-merge-articles.md).  
  
3.  (Optional) At the Publisher on the publication database, execute [sp_mergearticlecolumn](/sql/relational-databases/system-stored-procedures/sp-mergearticlecolumn-transact-sql) to filter table columns. For more information, see [Define and Modify a Column Filter](define-and-modify-a-column-filter.md).  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 This example defines an article based on the `Product` table for a transactional publication, where the article is filtered both horizontally and vertically.  
  
 [!code-sql[HowTo#sp_AddTranArticle](../../../snippets/tsql/SQL15/replication/howto/tsql/createtranpub.sql#sp_addtranarticle)]  
  
 This example defines articles for a merge publication, where the `SalesOrderHeader` article is statically filtered based on **SalesPersonID**, and the `SalesOrderDetail` article is join filtered based on `SalesOrderHeader`.  
  
 [!code-sql[HowTo#sp_AddMergeArticle](../../../snippets/tsql/SQL15/replication/howto/tsql/createmergepub.sql#sp_addmergearticle)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 You can define articles programmatically by using Replication Management Objects (RMO). The RMO classes that you use to define an article depend on the type of publication for which the article is defined.  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 The following example adds an article with row and column filters to a transactional publication.  
  
 [!code-csharp[HowTo#rmo_CreateTranArticles](../../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_createtranarticles)]  
  
 [!code-vb[HowTo#rmo_vb_CreateTranArticles](../../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_createtranarticles)]  
  
 The following example adds three articles to a merge publication. The articles have column filters, and two join filters are used to propagate a parameterized row filter to the other articles.  
  
 [!code-csharp[HowTo#rmo_CreateMergeArticles](../../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_createmergearticles)]  
  
 [!code-vb[HowTo#rmo_vb_CreateMergeArticles](../../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_createmergearticles)]  
  
## See Also  
 [Create a Publication](create-a-publication.md)   
 [Replication System Stored Procedures Concepts](../concepts/replication-system-stored-procedures-concepts.md)   
 [Add Articles to and Drop Articles from Existing Publications](add-articles-to-and-drop-articles-from-existing-publications.md)   
 [Filter Published Data](filter-published-data.md)   
 [Publish Data and Database Objects](publish-data-and-database-objects.md)   
 [Replication System Stored Procedures Concepts](../concepts/replication-system-stored-procedures-concepts.md)  
  
  
