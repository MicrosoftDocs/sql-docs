---
title: "View Conflict Information for Merge Publications (Replication Transact-SQL Programming) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "merge replication conflict resolution [SQL Server replication], viewing conflicts"
  - "sp_helpmergeconflictrows"
  - "viewing conflict information"
  - "conflict resolution [SQL Server replication], merge replication"
  - "sp_helpmergearticleconflicts"
ms.assetid: 4907fe35-10ee-4f81-b924-fc419b1864d2
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# View Conflict Information for Merge Publications (Replication Transact-SQL Programming)
  When a conflict is resolved in merge replication, the data from the losing row is written to a conflict table. This conflict data can be viewed programmatically by using replication stored procedures. For more information, see [Advanced Merge Replication Conflict Detection and Resolution](merge/advanced-merge-replication-conflict-detection-and-resolution.md).  
  
### To view conflict information and losing row data for all types of conflicts  
  
1.  At the Publisher on the publication database, execute [sp_helpmergepublication](/sql/relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql). Note the values of the following columns in the result set:  
  
    -   **centralized_conflicts** - 1 indicates that conflict rows are stored at the Publisher, and 0 indicates that conflict rows are not stored at the Publisher.  
  
    -   **decentralized_conflicts** - 1 indicates that conflict rows are stored at the Subscriber, and 0 indicates that conflict rows are not stored at the Subscriber.  
  
        > [!NOTE]  
        >  The conflict logging behavior of a merge publication is set by using the **@conflict_logging** parameter of [sp_addmergepublication](/sql/relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql). Use of the **@centralized_conflicts** parameter has been deprecated.  
  
     The following table describes the values of these columns based on the value specified for **@conflict_logging**.  
  
    |@conflict_logging value|centralized_conflicts|decentralized_conflicts|  
    |------------------------------|----------------------------|------------------------------|  
    |`publisher`|1|0|  
    |`subscriber`|0|1|  
    |`both`|1|1|  
  
2.  At either the Publisher on the publication database or at the Subscriber on the subscription database, execute [sp_helpmergearticleconflicts](/sql/relational-databases/system-stored-procedures/sp-helpmergearticleconflicts-transact-sql). Specify a value for **@publication** to only return conflict information for articles that belong to a specific publication. This returns conflict table information for articles with conflicts. Note the value of **conflict_table** for any articles of interest. If the value of **conflict_table** for an article is NULL, only delete conflicts have occurred in this article.  
  
3.  (Optional) Review conflict rows for articles of interest. Depending on the values of **centralized_conflicts** and **decentralized_conflicts** from step 1, do one of the following:  
  
    -   At the Publisher on the publication database, execute [sp_helpmergeconflictrows](/sql/relational-databases/system-stored-procedures/sp-helpmergeconflictrows-transact-sql). Specify a conflict table for the article (from step 1) for **@conflict_table**. (Optional) Specify a value of **@publication** to restrict returned conflict information to a specific publication. This returns row data and other information for the losing row.  
  
    -   At the Subscriber on the subscription database, execute [sp_helpmergeconflictrows](/sql/relational-databases/system-stored-procedures/sp-helpmergeconflictrows-transact-sql). Specify a conflict table for the article (from step 1) for **@conflict_table**. This returns row data and other information for the losing row.  
  
### To view information only on conflicts where the delete failed  
  
1.  At the Publisher on the publication database, execute [sp_helpmergepublication](/sql/relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql). Note the values of the following columns in the result set:  
  
    -   **centralized_conflicts** - 1 indicates that conflict rows are stored at the Publisher, and 0 indicates that conflict rows are not stored at the Publisher.  
  
    -   **decentralized_conflicts** - 1 indicates that conflict rows are stored at the Subscriber, and 0 indicates that conflict rows are not stored at the Subscriber.  
  
        > [!NOTE]  
        >  The conflict logging behavior of a merge publication is set using the **@conflict_logging** parameter of [sp_addmergepublication](/sql/relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql). Use of the **@centralized_conflicts** parameter has been deprecated.  
  
2.  At either the Publisher on the publication database or at the Subscriber on the subscription database, execute [sp_helpmergearticleconflicts](/sql/relational-databases/system-stored-procedures/sp-helpmergearticleconflicts-transact-sql). Specify a value for **@publication** to only return conflict table information for articles that belong to a specific publication. This returns conflict table information for articles with conflicts. Note the value of **source_object** for any articles of interest. If the value of **conflict_table** for an article is NULL, only delete conflicts have occurred in this article.  
  
3.  (Optional) Review conflict information for delete conflicts. Depending on the values of **centralized_conflicts** and **decentralized_conflicts** from step 1, do one of the following:  
  
    -   At the Publisher on the publication database, execute [sp_helpmergedeleteconflictrows](/sql/relational-databases/system-stored-procedures/sp-helpmergedeleteconflictrows-transact-sql). Specify the name of the source table (from step 1) on which the conflict occurred for **@source_object**. (Optional) Specify a value of **@publication** to restrict returned conflict information to a specific publication. This returns delete conflict information stored at the Publisher.  
  
    -   At the Subscriber on the subscription database, execute [sp_helpmergedeleteconflictrows](/sql/relational-databases/system-stored-procedures/sp-helpmergedeleteconflictrows-transact-sql). Specify the name of the source table (from step 1) on which the conflict occurred for **@source_object**. (Optional) Specify a value of **@publication** to restrict returned conflict information to a specific publication. This returns delete conflict information stored at the Subscriber.  
  
## See Also  
 [Advanced Merge Replication Conflict Detection and Resolution](merge/advanced-merge-replication-conflict-detection-and-resolution.md)  
  
  
