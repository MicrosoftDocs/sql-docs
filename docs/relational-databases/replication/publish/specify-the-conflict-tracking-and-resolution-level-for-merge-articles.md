---
title: "Specify the Conflict Tracking and Resolution Level for Merge Articles | Microsoft Docs"
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
  - "merge replication conflict resolution [SQL Server replication], levels"
  - "articles [SQL Server replication], conflict resolution"
  - "conflict resolution [SQL Server replication], merge replication"
ms.assetid: 81e9ecb6-1d31-4a78-b32a-96f7f4d67077
caps.latest.revision: 35
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Specify the Conflict Tracking and Resolution Level for Merge Articles
  This topic describes how to specify the conflict tracking and resolution level for merge articles in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 When a subscription to a merge publication is synchronized, replication checks for conflicts caused by changes to the same data made at both the Publisher and the Subscriber. You can specify whether conflicts are detected at the row-level, where any change to the row is considered a conflict, or column-level, where only changes to the same row and column are considered a conflict. Conflict resolution for articles is performed at the row-level. For more information about conflict detection and resolution when logical records are used, see [Detecting and Resolving Conflicts in Logical Records](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-resolving-in-logical-record.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
-   **To specify the conflict tracking and resolution level for merge articles, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If you modify the tracking level after subscriptions have been initialized, those subscriptions must be reinitialized. For more information about the effects of property changes, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
-   With row- and column-level tracking, conflict resolution is always performed at the row-level: the winning row overwrites the losing row. Merge replication also allows you to specify that conflicts be tracked and resolved at the logical record level, but these options are not available from [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]. For information about setting these options from replication stored procedures, see [Define a Logical Record Relationship Between Merge Table Articles](../../../relational-databases/replication/publish/define-a-logical-record-relationship-between-merge-table-articles.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Specify row- or column-level tracking for merge articles on the **Properties** tab of the **Article Properties** dialog box, which is available in the New Publication Wizard and the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
#### To specify row- or column-level tracking  
  
1.  On the **Articles** page of the New Publication Wizard or the **Publication Properties - \<Publication>** dialog box, select a table.  
  
2.  Click **Article Properties**, and then click **Set Properties of Highlighted Table Article** or **Set Properties of All Table Articles**.  
  
3.  On the **Properties** tab of the **Article Properties \<Article>** dialog box, select one of the following values for the **Tracking level** property: **Row-level tracking** or **Column-level tracking**.  
  
4.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To specify conflict tracking options for a new merge article  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) and specify one of the following values for **@column_tracking**:  
  
    -   **true** - Use column-level tracking for the article.  
  
    -   **false** - Use row-level tracking, which is the default.  
  
#### To change conflict tracking options for a merge article  
  
1.  To determine the conflict tracking options for a merge article, execute [sp_helpmergearticle](../../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md). Note the value of the **column_tracking** option in the result set for the article. A value of **1** means that column-level tracking is being used, and a value of **0** means that row-level tracking is being used.  
  
2.  At the Publisher on the publication database, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of **column_tracking** for **@property** and one of the following values for **@value**:  
  
    -   **true** - Use column-level tracking for the article.  
  
    -   **false** - Use row-level tracking, which is the default.  
  
     Specify a value of **1** for both **@force_invalidate_snapshot** and **@force_reinit_subscription**.  
  
## See Also  
 [Advanced Merge Replication Conflict Detection and Resolution](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)   
 [Detecting and Resolving Conflicts in Logical Records](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-resolving-in-logical-record.md)   
 [Define a Logical Record Relationship Between Merge Table Articles](../../../relational-databases/replication/publish/define-a-logical-record-relationship-between-merge-table-articles.md)   
 [Detect and Resolve Merge Replication Conflicts](../../../relational-databases/replication/merge/advanced-merge-replication-resolve-merge-replication-conflicts.md)  
  
  