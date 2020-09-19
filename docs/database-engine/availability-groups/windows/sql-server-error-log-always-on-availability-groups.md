---
title: "SQL Server error log (availability groups)"
description: Learn about the SQL Server error log events that affect an Always On availability group and which symptoms should lead to review of the error log.
ms.custom: seo-lt-2019
ms.date: "06/13/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
ms.assetid: 39d0c98d-75af-4dd1-b908-30d31af56f2a
author: rothja
ms.author: jroth
---
# SQL Server error log (Always On Availability Groups)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  The SQL Server error log reports events affecting Always On Availability Groups, such as:  
  
-   Communication with the Windows Server Failover Clustering (WSFC) cluster    
-   State transitions of availability replicas    
-   State transitions of availability databases    
-   Connectivity state of availability databases between primary and secondary replicas    
-   Statuses of the availability group endpoints    
-   Statuses of the availability group listeners    
-   Lease status between the SQL Server resource DLL (running in the WSFC cluster) and the SQL Server instance (for more information, see [How It Works: SQL Server Always On lease timeout](https://docs.microsoft.com/archive/blogs/psssql/how-it-works-sql-server-alwayson-lease-timeout))    
-   Error events in the availability group  

The following symptoms should lead to review of the SQL Server error log:  

-   Cannot access availability databases    
-   Unexpected availability group failover    
-   Availability group in the Resolving state unexpectedly    
-   Availability group in an indeterminate state  
  
For more information, see [View the SQL Server error log &#40;SQL Server Management Studio&#41;](~/relational-databases/performance/view-the-sql-server-error-log-sql-server-management-studio.md).  
  
  
