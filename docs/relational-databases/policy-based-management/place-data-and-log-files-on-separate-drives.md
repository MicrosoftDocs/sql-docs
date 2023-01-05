---
title: "Place Data and Log Files on Separate Drives | Microsoft Docs"
description: Place data and log files on separate logical drives. Separate locations allow activity for each to occur at the same time, improving SQL performance.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 6cbedc27-4d77-44ad-bed2-c23b628475a7
author: VanMSFT
ms.author: vanto
---
# Place Data and Log Files on Separate Drives
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks whether data and log files are placed on separate logical drives. Placing both data AND log files on the same device can cause contention for that device, resulting in poor performance. Placing the files on separate drives allows the I/O activity to occur at the same time for both the data and log files.  
  
## Recommendations  
 When you create a new database, specify separate drives for the data and logs. To move files after the database is created, the database must be taken offline. Move files by using one of the following methods:  
  
> [!NOTE]  
>  This policy cannot detect separate physical devices through mount points  
  
-   Restore the database from backup by using the RESTORE DATABASE statement with the WITH MOVE option.  
  
-   Detach and then attach the database specifying separate locations for the data and log devices.  
  
-   Specify a new location by running the ALTER DATABASE statement with the MODIFY FILE option, and then restarting the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## For More Information  
 [Move Database Files](../../relational-databases/databases/move-database-files.md)  
  
 [Move User Databases](../../relational-databases/databases/move-user-databases.md)  
  
 [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
