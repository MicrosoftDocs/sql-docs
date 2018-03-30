---
title: "Set Queued Updating Conflict Resolution Options (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "conflict resolution [SQL Server replication], queued updating subscriptions"
  - "queued updating subscriptions [SQL Server replication]"
ms.assetid: bb6b6c71-42c7-421a-a0fa-d5594d27e35d
caps.latest.revision: 32
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Set Queued Updating Conflict Resolution Options (SQL Server Management Studio)
  Set conflict resolution options for publications that support queued updating subscriptions on the **Subscription Options** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../../2014/relational-databases/replication/view-and-modify-publication-properties.md).  
  
### To set queued updating conflict resolution options  
  
1.  On the **Subscription Options** page of the **Publication Properties - \<Publication>** dialog box, select one of the following values for the **Conflict resolution policy** option:  
  
    -   **Keep the Publisher change**  
  
    -   **Keep the Subscriber change**  
  
    -   **Reinitialize the subscription**  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
## See Also  
 [Enable Updating Subscriptions for Transactional Publications](../../../2014/relational-databases/replication/enable-updating-subscriptions-for-transactional-publications.md)   
 [Queued Updating Conflict Detection and Resolution](../../../2014/relational-databases/replication/queued-updating-conflict-detection-and-resolution.md)  
  
  