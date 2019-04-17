---
title: "Database Mirroring: Interoperability and Coexistence (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "high availability [SQL Server], interoperability and coexistence"
  - "Database Engine [SQL Server], high availability"
ms.assetid: 89fef397-e0cf-4e08-b598-25b8d4170523
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Mirroring: Interoperability and Coexistence (SQL Server)
  Database mirroring can be used with the following features or components of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   [AlwaysOn Failover Cluster Instances (SQL Server Failover Clustering)](database-mirroring-and-sql-server-failover-cluster-instances.md)  
  
-   [Change data capture (and change tracking)](../../relational-databases/track-changes/change-data-capture-and-other-sql-server-features.md)  
  
-   [Database snapshots](../../relational-databases/databases/database-snapshots-sql-server.md)  
  
-   [Full-text catalogs](database-mirroring-and-full-text-catalogs-sql-server.md)  
  
-   [Log shipping](database-mirroring-and-log-shipping-sql-server.md)  
  
-   [Replication](database-mirroring-and-replication-sql-server.md)  
  
 Database mirroring does not interoperate with the following:  
  
-   Cross-database transactions/distributed transactions  
  
     For information about why such transactions are not supported, see [Cross-Database Transactions Not Supported For Database Mirroring or AlwaysOn Availability Groups &#40;SQL Server&#41;](../availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md).  
  
-   [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]  
  
## See Also  
 [Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)  
  
  
