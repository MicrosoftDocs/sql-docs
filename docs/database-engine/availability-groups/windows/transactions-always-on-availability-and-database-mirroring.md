---
title: "Transactions - Always On availability groups and database mirroring | Microsoft Docs"
ms.custom: ""
ms.date: "12/11/2018"
ms.prod: sql
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
# Transactions - availability groups and database mirroring
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes cross-database and distributed transactions support for Always On availability groups and database mirroring.  

## Support for distributed transactions

SQL Server 2017 supports distributed transactions for databases in availability groups. This support includes databases on the same instance of SQL Server or databases on different instances of SQL Server. Distributed transactions are not supported for databases configured for database mirroring.

> [!NOTE]
> [!INCLUDE[SQL Server 2016](../../../includes/sssql15-md.md)] Service Pack 2 and later provides full support for distributed transactions in availability groups. 
> 
> In [!INCLUDE[SQL Server 2016](../../../includes/sssql15-md.md)] versions prior to Service Pack 2, cross-database distributed transactions (i.e. transaction using databases on the same SQL Server instance) involving a database in an availability group are not supported.

To configure an availability group for distributed transactions, see [Configure Availability Group for Distributed Transactions](configure-availability-group-for-distributed-transactions.md).

See more information at:

- [DTC Administration Guide](https://msdn.microsoft.com/library/ms681291.aspx)
- [DTC Developers Guide](https://msdn.microsoft.com/library/ms679938.aspx)
- [DTC Programmers Reference](https://msdn.microsoft.com/library/ms686108.aspx)

## SQL Server 2016 SP1 and before: Support for cross-database transactions within the same SQL Server instance  

In SQL Server 2016 SP1 and before, cross-database transactions within the same SQL Server instance are not supported for availability groups. No two databases in a cross-database transaction may be hosted by the same SQL Server instance if either or both databases are in an availability group. This limitation also applies when those databases are part of the same availability group.  
  
Cross-database transactions are also not supported for database mirroring.  
  
##  <a name="dtcsupport"></a> SQL Server 2016 SP1 and before: Support for distributed transactions  
Distributed transactions are supported with availability groups when databases are hosted by different SQL Server instances. It also applies to distributed transactions between SQL Server instances and other DTC-compliant server.  
 
Microsoft Distributed Transaction Coordinator (MSDTC or DTC) is a Windows service that provides transaction infrastructure for distributed systems. MSDTC permits client applications to include multiple data sources in one transaction, which then is committed across all servers included in the transaction. For example, you can use MSDTC to coordinate transactions that span multiple databases on different servers.

SQL Server 2016 introduces the capability to use distributed transactions where one or more of the databases in the transaction are in an availability group. Prior to SQL Server 2016 distributed transactions were not supported for databases in availability groups. SQL Server 2016 can register a resource manager per database. This new capability is why distributed transactions can include databases in availability groups.
  
 The following requirements must be met:  
  
-   Availability groups must be running on Windows Server 2012 R2 or later. For Windows Server 2012 R2, you must install the update in KB3090973 available at [https://support.microsoft.com/kb/3090973](https://support.microsoft.com/kb/3090973).  
  
-   Availability groups must be created with the **CREATE AVAILABILITY GROUP** command and the **WITH DTC\_SUPPORT = PER_DB** clause. You cannot currently alter an existing availability group.  

- All instances of SQL Server that participate in the availability group must be SQL Server 2016 or later.
 
 ## Non-support for distributed transactions
 Specific cases where distributed transactions are not supported include:
 
 - In SQL Server 2016 SP1 and prior, where more than one database involved in the transaction is in the same availability group.
 
 - In SQL Server 2016 SP1 and prior, where at least one database is in an availability group and another database is on the same instance of SQL Server. 
 
 - Where the availability group was not created with enable distributed transaction.
 
 - Database mirroring.
 
 > [!IMPORTANT]
 > Determine the appropriate default outcome of transactions that DTC is unable to resolve for your environment.  For information on how to configure the default outcome see [in-doubt xact resolution Server Configuration Option](../../../database-engine/configure-windows/in-doubt-xact-resolution-server-configuration-option.md).
  
## Example scenario with database mirroring  
 The following database mirroring example illustrates how a logical inconsistency could occur. In this example, an application uses a cross-database transaction to insert two rows of data: one row is inserted into a table in a mirrored database, A, and the other row is inserted into a table in another database, B. Database A is being mirrored in high-safety mode with automatic failover. While the transaction is being committed, database A becomes unavailable, and the mirroring session automatically fails over to the mirror of database A.  
  
 After the failover, the cross-database transaction might be successfully committed on database B but not on the failed-over database. For example, if the original principal server for database A had not sent the log for the cross-database transaction to the mirror server before the failure. After the failover, that transaction would not exist on the new principal server. Databases A and B would become inconsistent, because the data inserted in database B remains intact, but the data inserted in database A has been lost.  
  
 A similar scenario can occur while using an MS DTC transaction. For example, after failover, the new principal contacts MS DTC. But MS DTC has no knowledge of the new principal server, and it terminates any transactions that are "preparing to commit," which are considered committed in other databases.  
  
> [!NOTE]  
>  Using Database Mirroring with DTC or using availability groups with DTC in ways not approved in this article is not supported.  This does not imply that aspects of the product unrelated to DTC are unsupported; however, any issues arising from the improper use of distributed transactions are not supported.  
  
## Next steps  
 [Always On availability groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)  
  
  
