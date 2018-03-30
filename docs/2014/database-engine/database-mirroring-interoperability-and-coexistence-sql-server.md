---
title: "Database Mirroring: Interoperability and Coexistence (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "high availability [SQL Server], interoperability and coexistence"
  - "Database Engine [SQL Server], high availability"
ms.assetid: 89fef397-e0cf-4e08-b598-25b8d4170523
caps.latest.revision: 14
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Database Mirroring: Interoperability and Coexistence (SQL Server)
  Database mirroring can be used with the following features or components of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
-   [AlwaysOn Failover Cluster Instances (SQL Server Failover Clustering)](../../2014/database-engine/database-mirroring-and-sql-server-failover-cluster-instances.md)  
  
-   [Change data capture (and change tracking)](../../2014/database-engine/change-data-capture-and-other-sql-server-features.md)  
  
-   [Database snapshots](../../2014/database-engine/database-mirroring-and-database-snapshots-sql-server.md)  
  
-   [Full-text catalogs](../../2014/database-engine/database-mirroring-and-full-text-catalogs-sql-server.md)  
  
-   [Log shipping](../../2014/database-engine/database-mirroring-and-log-shipping-sql-server.md)  
  
-   [Replication](../../2014/database-engine/database-mirroring-and-replication-sql-server.md)  
  
 Database mirroring does not interoperate with the following:  
  
-   Cross-database transactions/distributed transactions  
  
     For information about why such transactions are not supported, see [Cross-Database Transactions Not Supported For Database Mirroring or AlwaysOn Availability Groups &#40;SQL Server&#41;](../../2014/database-engine/transactions-always-on-availability-and-database-mirroring.md).  
  
-   [!INCLUDE[ssHADR](../includes/sshadr-md.md)]  
  
## See Also  
 [Database Mirroring &#40;SQL Server&#41;](../../2014/database-engine/database-mirroring-sql-server.md)  
  
  