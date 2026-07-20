---
title: "Establish a Performance Baseline"
description: Take performance measurements at regular intervals over time, even when no problems occur, to establish a server performance baseline in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/05/2024
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "database performance [SQL Server], baselines"
  - "monitoring performance [SQL Server], baselines"
  - "tuning databases [SQL Server], baselines"
  - "server performance [SQL Server], baselines"
  - "performance [SQL Server], baselines"
  - "baseline performance [SQL Server]"
  - "measurements for baseline statistics [SQL Server]"
  - "monitoring server performance [SQL Server], establishing baseline"
  - "database monitoring [SQL Server], baselines"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Establish a Performance Baseline
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  To determine whether your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system is performing optimally, take performance measurements at regular intervals over time, even when no problems occur, to establish a server performance baseline. Compare each new set of measurements with those taken earlier.  
  
 The following areas affect the performance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   System resources (hardware)  
  
-   Network architecture  
  
-   The operating system  
  
-   Database applications  
  
-   Client applications  
  
 At a minimum, use baseline measurements to determine:  
  
-   Peak and off-peak hours of operation.  
  
-   Production-query or batch-command response times.  
  
-   Database backup and restore completion times.  
  
 After you establish a server performance baseline, compare the baseline statistics to current server performance. Numbers far above or far below your baseline are candidates for further investigation. They might indicate areas in need of tuning or reconfiguration. For example, if the amount of time to execute a set of queries increases, examine the queries to determine if they can be rewritten, or if column statistics or new indexes must be added.  
  
## Related content

- [Best practices for monitoring workloads with Query Store](best-practice-with-the-query-store.md)
- [Tune applications and databases for performance in Azure SQL Database](/azure/azure-sql/database/performance-guidance)
- [Monitor performance in Fabric SQL database](/fabric/database/sql/monitor)
