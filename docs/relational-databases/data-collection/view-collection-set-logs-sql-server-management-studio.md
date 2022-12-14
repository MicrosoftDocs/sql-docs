---
description: "View collection set logs (SQL Server Management Studio)"
title: "View collection set logs (SSMS)"
ms.date: 06/03/2020
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [SQL Server], viewing"
  - "collection sets [SQL Server], viewing logs"
ms.assetid: 428908b8-fb6a-4d0c-8339-ee133e23aad2
author: MashaMSFT
ms.author: mathoma
ms.custom: "seo-lt-2019"
---
# View collection set logs (SQL Server Management Studio)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  You can view all the collection set logs or individual collection set logs from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
### To view collection set logs  
  
1.  In Object Explorer, expand the **Management** folder.  
  
2.  Right-click **Data Collection**, and then click **View Logs**.  
  
     This opens the **Log File Viewer**.All the log files for each collection set are listed and pre-selected under the **Data Collection** node in the viewer.  
  
3.  To view specific collection set logs, clear the check box next to each collection set whose log you do not want to view. The log information for that collection set is removed from the **Log File Viewer** details pane.  

### To view a specific collection set log file  
  
1.  In Object Explorer, expand the **Management** folder, and then expand **Data Collection**.  
  
2.  Right-click the collection set whose log you want to view, and then click **View Logs**.  
  
     The **Log File Viewer** opens, displaying only the log file for the collection set that you selected.  
  
## See Also  
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [Manage Data Collection](../../relational-databases/data-collection/manage-data-collection.md)  
  
  
