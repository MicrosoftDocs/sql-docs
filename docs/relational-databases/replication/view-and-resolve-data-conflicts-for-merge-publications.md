---
title: "View and Resolve Data Conflicts for Merge Publications | Microsoft Docs"
ms.custom: ""
ms.date: "11/20/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "merge replication conflict resolution [SQL Server replication], viewing conflicts"
  - "viewing conflict information"
  - "conflict resolution [SQL Server replication], merge replication"
ms.assetid: aeee9546-4480-49f9-8b1e-c71da1f056c7
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Conflict resolution for Merge Replication
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Conflicts in merge replication are resolved based on the resolver specified for each article. By default, conflicts are resolved without the need for user intervention. But conflicts can be viewed, and the outcome of the resolution can be changed, in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Replication Conflict Viewer.  
  
 Conflict data is available in the Replication Conflict Viewer for the amount of time specified for the conflict retention period (with a default of 14 days). To set the conflict retention period, either:  
  
-   Specify a retention value for the **@conflict_retention** parameter of [sp_addmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md).  
  
-   Specify a value of **conflict_retention** for the **@property** parameter and a retention value for the **@value** parameter of [sp_changemergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md).  
  
 By default, conflict information is stored:    
-   At the Publisher and Subscriber if the publication compatibility level is 90RTM or higher.   
-   At the Publisher if the publication compatibility level is lower than 80RTM.   
-   At the Publisher if Subscribers are running [!INCLUDE[ssEW](../../includes/ssew-md.md)]. Conflict data cannot be stored on [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers.  
  
 Storage of conflict information is controlled by the **conflict_logging** publication property. For more information, see [sp_addmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md) and [sp_changemergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md).  
  
 Conflicts can also be resolved interactively during synchronization using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Interactive Resolver. The Interactive Resolver is available through the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Synchronization Manager. For more information, see [Synchronize a Subscription Using Windows Synchronization Manager &#40;Windows Synchronization Manager&#41;](../../relational-databases/replication/synchronize-a-subscription-using-windows-synchronization-manager.md).  
  
## Resolve conflicts  
  
1.  Connect to the Publisher (or Subscriber if appropriate) in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Right-click the publication for which you want to view conflicts, and then click **View Conflicts**.  
  
    > [!NOTE]  
    >  If you specified a value of **'subscriber'** for the **conflict_logging** property, the **View Conflicts** menu option is not available. To view conflicts, start ConflictViewer.exe from the command prompt. By default, ConflictViewer.exe is located in the following directory: Microsoft SQL Server\100\Tools\Binn\VSShell\Common7\IDE. For a list of valid startup parameters, run ConflictViewer.exe -?.  
  
4.  In the **Select Conflict Table** dialog box, select a database, publication, and table for which to view conflicts.  
  
5.  In the Replication Conflict Viewer, you can:  
  
    -   Filter rows with the buttons to the right of the upper grid.  
  
    -   Select a row in the upper grid to display information on that row in the lower grid.  
  
    -   Select one or more rows in the upper grid, and then click **Remove**, which is equivalent to clicking the **Submit Winner** button (without making any changes to the data).  
  
    -   Click the properties button (**…**) to view more information on a column involved in a conflict.  
  
    -   Edit data in the **Conflict winner** or **Conflict loser** column before submitting the data (data is read-only if the column is gray).  
  
    -   Click **Submit Winner** to accept the row designated as the winner of the conflict.  
  
    -   Click **Submit Loser** to override the resolution and to propagate the value designated as the loser of the conflict to all nodes in the topology.  
  
    -   Select **Log the details of this conflict** to log conflict data to a file. To specify a location for the file, point to the **View** menu, and then click **Options**. Enter a value, or click the browse button (**...**), and then navigate to the appropriate file. Click **OK** to exit the **Options** dialog box.  
  
6.  Close the Replication Conflict Viewer.  


  
## View conflict information
When a conflict is resolved in merge replication, the data from the losing row is written to a conflict table. This conflict data can be viewed programmatically by using replication stored procedures. For more information, see [Advanced Merge Replication Conflict Detection and Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md).  
  
1.  At the Publisher on the publication database, execute [sp_helpmergepublication](../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md). Note the values of the following columns in the result set:  
  
    -   **centralized_conflicts** - 1 indicates that conflict rows are stored at the Publisher, and 0 indicates that conflict rows are not stored at the Publisher.  
  
    -   **decentralized_conflicts** - 1 indicates that conflict rows are stored at the Subscriber, and 0 indicates that conflict rows are not stored at the Subscriber.  
  
        > [!NOTE]  
        >  The conflict logging behavior of a merge publication is set by using the **@conflict_logging** parameter of [sp_addmergepublication](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md). Use of the **@centralized_conflicts** parameter has been deprecated.  
  
     The following table describes the values of these columns based on the value specified for **@conflict_logging**.  
  
    |@conflict_logging value|centralized_conflicts|decentralized_conflicts|  
    |------------------------------|----------------------------|------------------------------|  
    |**publisher**|1|0|  
    |**subscriber**|0|1|  
    |**both**|1|1|  
  
2.  At either the Publisher on the publication database or at the Subscriber on the subscription database, execute [sp_helpmergearticleconflicts](../../relational-databases/system-stored-procedures/sp-helpmergearticleconflicts-transact-sql.md). Specify a value for **@publication** to only return conflict information for articles that belong to a specific publication. This returns conflict table information for articles with conflicts. Note the value of **conflict_table** for any articles of interest. If the value of **conflict_table** for an article is NULL, only delete conflicts have occurred in this article.  
  
3.  (Optional) Review conflict rows for articles of interest. Depending on the values of **centralized_conflicts** and **decentralized_conflicts** from step 1, do one of the following:  
  
    -   At the Publisher on the publication database, execute [sp_helpmergeconflictrows](../../relational-databases/system-stored-procedures/sp-helpmergeconflictrows-transact-sql.md). Specify a conflict table for the article (from step 1) for **@conflict_table**. (Optional) Specify a value of **@publication** to restrict returned conflict information to a specific publication. This returns row data and other information for the losing row.  
  
    -   At the Subscriber on the subscription database, execute [sp_helpmergeconflictrows](../../relational-databases/system-stored-procedures/sp-helpmergeconflictrows-transact-sql.md). Specify a conflict table for the article (from step 1) for **@conflict_table**. This returns row data and other information for the losing row.  
  
## Conflict where delete failed   
  
1.  At the Publisher on the publication database, execute [sp_helpmergepublication](../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md). Note the values of the following columns in the result set:  
  
    -   **centralized_conflicts** - 1 indicates that conflict rows are stored at the Publisher, and 0 indicates that conflict rows are not stored at the Publisher.  
  
    -   **decentralized_conflicts** - 1 indicates that conflict rows are stored at the Subscriber, and 0 indicates that conflict rows are not stored at the Subscriber.  
  
        > [!NOTE]  
        >  The conflict logging behavior of a merge publication is set using the **@conflict_logging** parameter of [sp_addmergepublication](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md). Use of the **@centralized_conflicts** parameter has been deprecated.  
  
2.  At either the Publisher on the publication database or at the Subscriber on the subscription database, execute [sp_helpmergearticleconflicts](../../relational-databases/system-stored-procedures/sp-helpmergearticleconflicts-transact-sql.md). Specify a value for **@publication** to only return conflict table information for articles that belong to a specific publication. This returns conflict table information for articles with conflicts. Note the value of **source_object** for any articles of interest. If the value of **conflict_table** for an article is NULL, only delete conflicts have occurred in this article.  
  
3.  (Optional) Review conflict information for delete conflicts. Depending on the values of **centralized_conflicts** and **decentralized_conflicts** from step 1, do one of the following:  
  
    -   At the Publisher on the publication database, execute [sp_helpmergedeleteconflictrows](../../relational-databases/system-stored-procedures/sp-helpmergedeleteconflictrows-transact-sql.md). Specify the name of the source table (from step 1) on which the conflict occurred for **@source_object**. (Optional) Specify a value of **@publication** to restrict returned conflict information to a specific publication. This returns delete conflict information stored at the Publisher.  
  
    -   At the Subscriber on the subscription database, execute [sp_helpmergedeleteconflictrows](../../relational-databases/system-stored-procedures/sp-helpmergedeleteconflictrows-transact-sql.md). Specify the name of the source table (from step 1) on which the conflict occurred for **@source_object**. (Optional) Specify a value of **@publication** to restrict returned conflict information to a specific publication. This returns delete conflict information stored at the Subscriber.  
  
## See Also  
 [Advanced Merge Replication Conflict Detection and Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)   
 [Specify a Merge Article Resolver](../../relational-databases/replication/publish/specify-a-merge-article-resolver.md)  
  
  
