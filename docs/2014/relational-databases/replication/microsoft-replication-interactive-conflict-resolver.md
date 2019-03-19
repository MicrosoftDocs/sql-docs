---
title: "Microsoft Replication Interactive Conflict Resolver | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.replconflictviewer.interactiveresolver.f1"
ms.assetid: d3d4a480-782b-4b1d-b839-565c8cf6cb24
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Microsoft Replication Interactive Conflict Resolver
  The Interactive Conflict Resolver can be used for merge subscriptions that are synchronized using Windows Synchronization Manager. It allows you to view, compare, edit, and select the outcome for data conflicts. Replication also includes the Conflict Viewer, which allows you to view and modify conflict outcomes after they have been committed. The Interactive Conflict Resolver allows you to select the outcome during synchronization.  
  
> [!NOTE]  
>  Conflicts that involve logical records are not displayed in the Interactive Resolver. To view information about these conflicts, use replication stored procedures. For more information, see [View Conflict Information for Merge Publications &#40;Replication Transact-SQL Programming&#41;](view-conflict-information-for-merge-publications.md).  
  
## Options  
 **Column name**  
 The name of all columns in the table. One or more columns might have conflicting data. Regardless of which columns conflict, the entire winning row will overwrite the entire losing row.  
  
 **Suggested Resolution**  
 The suggested resolution provided by the conflict resolver for the article.  
  
 **Publisher**  
 The data value at the Publisher.  
  
 **Subscriber**  
 The data value at the Subscriber.  
  
 **Accept Suggested**, **Accept Publisher**, and **Accept Subscriber**  
 Click to accept the row that will be applied at either the Publisher or the Subscriber, depending on which one lost the conflict. If the Publisher lost the conflict, all other Subscribers will receive the winning row the next time they synchronize with the Publisher.  
  
 **Resolve remaining conflicts automatically**  
 Resolve all remaining conflicts using the suggested resolution provided by the conflict resolver for the article.  
  
 **Log the details of the conflict for later reference**  
 Logs the details of the conflict in system tables.  
  
## See Also  
 [Interactive Conflict Resolution](merge/advanced-merge-replication-conflict-interactive-resolution.md)   
 [View and Resolve Data Conflicts for Merge Publications &#40;SQL Server Management Studio&#41;](view-and-resolve-data-conflicts-for-merge-publications.md)   
 [Synchronize a Subscription Using Windows Synchronization Manager &#40;Windows Synchronization Manager&#41;](synchronize-a-subscription-using-windows-synchronization-manager.md)   
 [Advanced Merge Replication Conflict Detection and Resolution](merge/advanced-merge-replication-conflict-detection-and-resolution.md)  
  
  
