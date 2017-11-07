---
title: "Specify the Processing Order of Merge Table Articles | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "articles [SQL Server replication], processing order"
  - "merge replication [SQL Server replication], article processing order"
ms.assetid: 9fe576a2-f5fb-4fdf-bd7d-cb322021b669
caps.latest.revision: 33
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Specify the Processing Order of Merge Table Articles
  Merge replication enables you to specify the order in which articles are processed by the Merge Agent during the synchronization process. You can assign an order to each article programmatically when creating an article using replication stored procedures. Articles are processed in order from lowest to highest value. If two articles have the same value, they are processed concurrently. For more information, see [Specify the Processing Order of Merge Articles](../../../relational-databases/replication/merge/specify-the-processing-order-of-merge-articles.md).  
  
### To specify the processing order for a new merge article  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md). Specify an integer value that represents the processing order for the article for **@processing_order**. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
    > [!NOTE]  
    >  When creating ordered articles, you should leave gaps between the article order values. This makes it easier to set new values in the future. For example, if you have three articles for which you need to specify a fixed processing order, set the value of **@processing_order** to 10, 20, and 30 rather than 1, 2, and 3, respectively.  
  
### To change the processing order of a merge article  
  
1.  To determine processing order of an article, execute [sp_helpmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md) and note the value of **processing_order** in the result set.  
  
2.  At the Publisher on the publication database, execute [sp_changemergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of **processing_order** for **@property** and an integer value that represents the processing order for **@value**.  
  
## See Also  
 [Specify the Processing Order of Merge Articles](../../../relational-databases/replication/merge/specify-the-processing-order-of-merge-articles.md)  
  
  