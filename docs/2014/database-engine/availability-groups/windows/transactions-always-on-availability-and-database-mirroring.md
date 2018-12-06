---
title: "Cross-Database Transactions Not Supported For Database Mirroring or AlwaysOn Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "database mirroring [SQL Server], interoperability"
  - "cross-database transactions [SQL Server]"
  - "transactions [database mirroring]"
  - "Availability Groups [SQL Server], interoperability"
  - "troubleshooting [SQL Server], cross-database transactions"
ms.assetid: 9f7ed895-ad65-43e3-ba08-00d7bff1456d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Cross-Database Transactions Not Supported For Database Mirroring or AlwaysOn Availability Groups (SQL Server)
  Cross-database transactions and distributed transactions are not supported by [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] or by database mirroring. This is because transaction atomicity/integrity cannot be guaranteed for the following reasons:  
  
-   For cross-database transactions: Each database commits independently. Therefore, even for databases in a single availability group, a failover could occur after one database commits a transaction but before the other database does. For database mirroring this issue is compounded because after a failover, the mirrored database is typically on a different server instance from the other database, and  even if both databases are mirrored between the same two partners, there is no guarantee that both databases will fail over at the same time.  
  
-   For distributed transactions: After a failover, the new principal server/primary replica is unable to connect to the distributed transaction coordinator on the previous principal server/primary replica. Therefore, the new principal server/primary replica cannot obtain the transaction status.  
  
 The following database mirroring example illustrates how a logical inconsistency could occur. In this example, an application uses a cross-database transaction to insert two rows of data: one row is inserted into a table in a mirrored database, A, and the other row is inserted into a table in another database, B. Database A is being mirrored in high-safety mode with automatic failover. While the transaction is being committed, database A becomes unavailable, and the mirroring session automatically fails over to the mirror of database A.  
  
 After the failover, the cross-database transaction might be successfully committed on database B but not on the failed-over database. This would occur if the original principal server for database A had not sent the log for the cross-database transaction to the mirror server before the failure. After the failover, that transaction would not exist on the new principal server. Databases A and B would become inconsistent, because the data inserted in database B remains intact, but the data inserted in database A has been lost.  
  
 A similar scenario can occur while using a MS DTC transaction. For example, after failover, the new principal contacts MS DTC. But MS DTC has no knowledge of the new principal server, and it terminates any transactions that are "preparing to commit," which are considered committed in other databases.  
  
> [!IMPORTANT]  
>  Using Database Mirroring or Availability Groups together with DTC does not result in an unsupported SQL Server installation. If, however, a database is part of a Database Mirroring session or an Availability Group and DTC is also used in the database, support issues will be investigated by Microsoft only if unrelated to the combined use of Database Mirroring or Availability Groups with DTC.  
  
  
