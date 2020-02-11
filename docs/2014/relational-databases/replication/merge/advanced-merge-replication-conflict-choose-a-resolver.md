---
title: "Choose a Resolver | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "default conflict resolver"
  - "articles [SQL Server replication], conflict resolution"
  - "conflict resolution [SQL Server replication], merge replication"
ms.assetid: b7dec3fa-d9d9-409d-b946-f9b9a3202829
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Choose a Resolver
  When choosing a resolver, consider the importance of conflict resolution in your application and whether you can use the default priority-based conflict resolver or need to use an article resolver.  
  
 If your data is partitioned without multiple users writing to the same partitions, and your replication topology is relatively basic (one Publisher and a few Subscribers), conflicts should be rare or nonexistent. In these environments, you probably do not need a complex conflict resolution strategy. A strategy using the default settings for conflict resolution, using client subscriptions and a first change in wins policy, is recommended. If the topology is more complex (using republishing Subscribers, for example), server subscriptions with specific priorities might be more appropriate.  
  
 An article resolver is recommended if your business needs require a more finely tuned solution than is available with the default resolver. If you choose to use an article resolver, it is recommended that you use a business logic handler. For more information, see [Execute Business Logic During Merge Synchronization](execute-business-logic-during-merge-synchronization.md).  
  
 Ultimately, choosing whether to use the default resolver or an article resolver should be based on the data and the business logic needs of the application. For example, consider employees who enter customer ranking data into a set of non-partitioned tables at different Subscribers; the employees span various job categories (branch managers, line managers, sales staff), and job category determines whose data should be given priority. In this case, an article resolver can be built that uses job category data from the article to determine the winner if a conflict occurs.  
  
 If conflicts are likely to occur with some frequency, here are the most important decisions you should consider when implementing a conflict resolution strategy.  
  
|Conflict resolution issue|Recommendation|  
|-------------------------------|--------------------|  
|Different categories of users require different priority values.|Use the default resolver and create server subscriptions with different priority values.<br /><br /> -Or-<br /><br /> Use an article resolver that recognizes an authority value column in the article to help resolve a conflict.|  
|First change in wins conflict solution wanted.|Use the default resolver and create client subscriptions.|  
|Multiple users changing the same data row is acceptable, as long as no conflicting changes are made to the same column.|Use either the default resolver or an article resolver with column-level tracking enabled.|  
|Flag multiple changes to any value in a row as a conflict.|Use either the default resolver or an article resolver with row-level tracking.|  
|Flag multiple changes to any value in a logical record as a conflict.|Use the default resolver with logical record-level tracking (the logical records feature does not support custom resolvers or business logic handlers).|  
|Conflict outcome data needs to be different from original conflict data.|Use an article resolver that calculates new values.|  
  
## See Also  
 [Detecting and Resolving Conflicts in Logical Records](advanced-merge-replication-conflict-resolving-in-logical-record.md)   
 [Advanced Merge Replication Conflict Detection and Resolution](advanced-merge-replication-conflict-detection-and-resolution.md)   
 [Republish Data](../republish-data.md)  
  
  
