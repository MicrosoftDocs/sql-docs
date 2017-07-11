---
title: "Execute Scripts Before and After a Snapshot Is Applied | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "snapshots [SQL Server replication], scripts"
  - "scripts [SQL Server replication], snapshots"
  - "snapshot replication [SQL Server], scripts"
ms.assetid: b7bb1e4c-5b48-4bb1-9dc8-47c911f2cc82
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Execute Scripts Before and After a Snapshot Is Applied
  Specify an optional script to execute before or after the snapshot is applied on the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
> [!NOTE]  
>  These options are not available if the **Snapshot format** option is set to **Character**.  
  
### To execute a script before or after a snapshot is applied  
  
1.  On the **Snapshot** page of the **Publication Properties - \<Publication>** dialog box:  
  
    -   To specify a script to execute before the snapshot is applied, click **Browse** to navigate to the script, or enter a path to the script in the **Before applying the snapshot, execute this script** text box.  
  
        > [!NOTE]  
        >  The Distribution Agent or Merge Agent must have read permissions for the directory you specify. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\computername\scripts\myscript.sql.  
  
    -   To specify a script to execute after the snapshot is applied, click **Browse** to navigate to the script, or enter a UNC path to the script in the **After applying the snapshot, execute this script** text box.  
  
2.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Configure Snapshot Properties &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/publish/configure-snapshot-properties-replication-transact-sql-programming.md)   
 [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [Execute Scripts Before and After the Snapshot Is Applied](../../relational-databases/replication/execute-scripts-before-and-after-the-snapshot-is-applied.md)   
 [Initialize a Subscription with a Snapshot](../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md)  
  
  