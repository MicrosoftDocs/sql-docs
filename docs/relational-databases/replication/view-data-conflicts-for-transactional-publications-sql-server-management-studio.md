---
title: "View data conflicts (Transactional) - SSMS"
description: View data conflicts for Transactional Replication using SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "conflict resolution [SQL Server replication], queued updating subscriptions"
  - "queued updating subscriptions [SQL Server replication]"
  - "viewing conflict information"
ms.assetid: 9977dd75-b0de-4376-9c13-86d80567d8aa
author: "MashaMSFT"
ms.author: "mathoma"
---
# View Data Conflicts for Transactional Publications (SQL Server Management Studio)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  You can view conflicts for peer-to-peer transactional replication and transactional replication with queued updating subscriptions in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Replication Conflict Viewer. For information about how conflicts are detected and resolved, see [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md) and [Set Queued Updating Conflict Resolution Options &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/publish/create-an-updatable-subscription-to-a-transactional-publication.md).  
  
 The availability of conflict data depends on the type of replication and the conflict retention period:  
  
-   For peer-to-peer replication, by default, the Distribution Agent fails when it detects a conflict. A conflict error is logged into the error log, but no conflict data is logged into the conflict table; therefore it is not available for viewing. If the Distribution Agent is allowed to continue, a conflict is logged locally on each node where it was detected. For more information, see "Handling Conflicts" in [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).  
  
-   For queued updating subscriptions, data is available for every conflict. Conflict data is available in the Replication Conflict Viewer for the amount of time specified for the conflict retention period, with a default of 14 days. To set the conflict retention period, perform either of the following:  
  
    -   Specify a retention value for the @conflict_retention parameter of [sp_addpublication](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md).  
  
    -   Specify a value of **'conflict_retention'** for the @property parameter and a retention value for the @value parameter of [sp_changepublication](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md).  
  
### To view conflicts  
  
1.  Connect to the appropriate server in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node:  
  
    -   For peer-to-peer replication, this is the node at which the conflict occurred.  
  
    -   For queued updating subscriptions, this is the Publisher.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder.  
  
3.  Right-click the publication for which you want to view conflicts, and then click **View Conflicts**.  
  
4.  In the **Select Conflict Table** dialog box, select a database, publication, and table for which to view conflicts.  
  
5.  In the Replication Conflict Viewer, you can:  
  
    -   Filter rows with the buttons to the right of the upper grid.  
  
    -   Select a row in the upper grid to display information on that row in the lower grid.  
  
    -   Select one or more rows in the upper grid, and then click **Remove**, which removes the row from the conflicts metadata table.  
  
    -   Click the properties button (**…**) to view more information on a column involved in a conflict.  
  
    -   Select **Log the details of this conflict** to log conflict data to a file. To specify a location for the file, point to the **View** menu, and then click **Options**. Enter a value, or click the browse button (**...**), and then navigate to the appropriate file. Click **OK** to close the **Options** dialog box.  
  
6.  Close the Replication Conflict Viewer.  
  
## See Also  
 [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)   
 [Queued Updating Conflict Detection and Resolution](../../relational-databases/replication/transactional/updatable-subscriptions-queued-updating-conflict-resolution.md)  
  
  
