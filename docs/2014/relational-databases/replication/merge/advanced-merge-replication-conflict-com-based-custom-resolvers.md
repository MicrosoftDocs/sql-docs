---
title: "COM-Based Custom Resolvers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "COM-based resolvers [SQL Server replication]"
  - "custom resolvers [SQL Server replication]"
ms.assetid: 94195797-ad7a-4962-a8e3-b259cd13aa38
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# COM-Based Custom Resolvers
  Custom resolvers provide more flexibility than the default resolution mechanism, and they can implement business logic required by applications using the replicated data. A COM-based custom resolver is a dynamic-link library (DLL) that implements the **ICustomResolver** COM interface, its methods and properties, and other supporting interfaces and type definitions designed specifically for conflict resolution.  
  
> [!NOTE]  
>  It is recommended to use a business logic handler rather than a COM-based custom resolver if possible. For more information on business logic handlers, see [Execute Business Logic During Merge Synchronization](execute-business-logic-during-merge-synchronization.md).  
  
 To build a custom COM resolver, you can use the type library that is provided in the replrec.dll; by default, this library is installed at [!INCLUDE[ssInstallPath](../../../includes/ssinstallpath-md.md)]COM.  
  
 Before writing a custom COM resolver, you need to decide:  
  
-   The types of row changes you want to resolve, such as updates, inserts, and deletes, and whether the resolver should be invoked during the upload of merge changes, the download of merge changes, or both. You can specify one type of change, all changes, or any combination. The default merge conflict resolver handles any conflicts not covered by a custom resolver.  
  
-   Whether to use column tracking when resolving the conflict. When column-level tracking is on, only data in those columns where a conflict exists are flagged as a conflict, otherwise the data is merged. However, conflicts are resolved in the same way as row-level tracking: the priority winner overwrites the entire row of data (but the data can be a mix of values from the Publisher, Subscribers, or some altered values that were from neither Publisher nor Subscribers). For more information, see [Detect and Resolve Merge Replication Conflicts](advanced-merge-replication-conflict-detection-and-resolution.md).  
  
 To implement a COM-based custom conflict resolver, see [Implement a Custom Conflict Resolver for a Merge Article](../implement-a-custom-conflict-resolver-for-a-merge-article.md).  
  
 A custom resolver is specified for an article, not an entire publication. The same resolver can be used with more than one article, but the logic in custom resolvers is often specific to a particular table. If the table used in the article is modified after the resolver is created (for example, renaming the column name that is used in conflict resolution), the custom resolver might need to be modified and recompiled.  
  
 To specify a custom resolver, see [Specify a Merge Article Resolver](../publish/specify-a-merge-article-resolver.md).  
  
## See Also  
 [Advanced Merge Replication Conflict Detection and Resolution](advanced-merge-replication-conflict-detection-and-resolution.md)   
 [Microsoft COM-Based Resolvers](advanced-merge-replication-conflict-com-based-resolvers.md)  
  
  
