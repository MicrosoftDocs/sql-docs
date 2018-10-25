---
title: "Specify That Deletes Should Not Be Tracked For Merge Articles (Replication Transact-SQL Programming) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "replication"
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "conditional delete tracking [SQL Server replication]"
  - "merge replication [SQL Server replication], conditional delete tracking"
ms.assetid: 0fe330ca-5fb5-422e-ad6f-92fb5d6a3b6c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Specify That Deletes Should Not Be Tracked For Merge Articles (Replication Transact-SQL Programming)
    
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
 By default, merge replication synchronizes DELETE commands between the Publisher and Subscriber. Merge replication enables you to retain rows in the subscription database even when they have been deleted from the publication, and vice versa. You can programmatically specify that DELETE commands be ignored when creating a new article or you can enable this functionality at a later time using replication stored procedures.  
  
> [!IMPORTANT]  
>  Enabling this functionality will result in non-convergence, which means that data present at the Subscriber will not accurately reflect data at the Publisher. You must implement your own mechanism for manually removing deleted rows.  
  
### To specify that deletes be ignored for a new merge article  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql). Specify a value of `false` for **@delete_tracking**. For more information, see [Define an Article](define-an-article.md).  
  
    > [!NOTE]  
    >  If the source table for an article is already published in another publication, the value of **delete_tracking** must be the same for both articles.  
  
### To specify that deletes be ignored for an existing merge article  
  
1.  To determine if error compensation is enabled for an article, execute [sp_helpmergearticle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql) and note the value of **delete_tracking** in the result set. If this value is **0**, deletes are already being ignored.  
  
2.  If the value from step 1 is **1**, execute [sp_changemergearticle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql) at the Publisher on the publication database. Specify a value of **delete_tracking** for **@property**, and a value of `false` for **@value**.  
  
    > [!NOTE]  
    >  If the source table for an article is already published in another publication, the value of **delete_tracking** must be the same for both articles.  
  
## See Also  
 [Optimize Merge Replication Performance with Conditional Delete Tracking](../merge/optimize-merge-replication-performance-with-conditional-delete-tracking.md)  
  
  
