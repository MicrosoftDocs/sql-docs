---
title: "Database Mirroring: Interoperability & Coexistence"
description: Learn about interoperability and coexistence of SQL Server database mirroring and other SQL Server features, such as full-text catalogs and database snapshots.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "high availability [SQL Server], interoperability and coexistence"
  - "Database Engine [SQL Server], high availability"
---
# Database Mirroring: Interoperability and Coexistence (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Database mirroring can be used with the following features or components of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   [Always On Failover Cluster Instances (SQL Server Failover Clustering)](../../database-engine/database-mirroring/database-mirroring-and-sql-server-failover-cluster-instances.md)  
  
-   [Change data capture (and change tracking)](../../relational-databases/track-changes/change-data-capture-and-other-sql-server-features.md)  
  
-   [Database snapshots](../../database-engine/database-mirroring/database-mirroring-and-database-snapshots-sql-server.md)  
  
-   [Full-text catalogs](../../database-engine/database-mirroring/database-mirroring-and-full-text-catalogs-sql-server.md)  
  
-   [Log shipping](../../database-engine/database-mirroring/database-mirroring-and-log-shipping-sql-server.md)  
  
-   [Replication](../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md)  
  
 Database mirroring does not interoperate with the following:  
  
-   Cross-database transactions/distributed transactions  
  
     For information about why such transactions are not supported, see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md).  
  
-   [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]  
  
## See Also  
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)  
  
  
