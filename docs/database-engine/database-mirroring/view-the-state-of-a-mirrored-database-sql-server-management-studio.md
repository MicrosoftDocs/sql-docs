---
title: "View the State of a Mirrored Database (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "states [SQL Server], database mirroring"
  - "database mirroring [SQL Server], states"
ms.assetid: 544f4194-253e-4c57-96ca-31c16301434f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# View the State of a Mirrored Database (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  During a database mirroring session, you can view the status on the **Mirroring** page of the **Database Properties** dialog box.  
  
### To view the status of a database mirroring session  
  
1.  After connecting to the principal server instance, in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and select the database to be mirrored.  
  
3.  Right-click the database, select **Tasks**, and then click **Mirror**. This opens the **Mirroring** page of the **Database Properties** dialog box.  
  
4.  After mirroring begins, the **Status** panel displays the status of the database mirroring session as of when you selected the **Mirroring** page or clicked the **Refresh** button. The possible states are as follows:  
  
    |States|Explanation|  
    |------------|-----------------|  
    |\<blank>|No database mirroring session exists and there is no activity to report on the **Mirroring** page.|  
    |Paused|The principal database is running but is not sending any logs to the mirror server. The mirror copy of the database is not available.|  
    |No connection|The principal server instance cannot connect to its partner or to the witness server instance (if any)|  
    |Synchronizing|The contents of the mirror database are lagging behind the contents of the principal database. The principal server instance is sending log records to the mirror server instance, which is applying the changes to the mirror database to roll it forward.<br /><br /> At the start of a database mirroring session, the mirror and principal databases are in the synchronizing state.|  
    |Failover|On the principal server instance, a manual failover (role swap) has begun but has not yet accepted by the mirror.|  
    |Synchronized|The mirror database contains the same data as the principal database. Manual and automatic failover are possible *only* in the synchronized state.|  
  
## See Also  
 [Mirroring States &#40;SQL Server&#41;](../../database-engine/database-mirroring/mirroring-states-sql-server.md)  
  
  
