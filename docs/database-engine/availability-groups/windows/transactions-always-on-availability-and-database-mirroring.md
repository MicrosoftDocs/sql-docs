---
title: "Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/29/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "database mirroring [SQL Server], interoperability"
  - "cross-database transactions [SQL Server]"
  - "transactions [database mirroring]"
  - "Availability Groups [SQL Server], interoperability"
  - "troubleshooting [SQL Server], cross-database transactions"
ms.assetid: 9f7ed895-ad65-43e3-ba08-00d7bff1456d
caps.latest.revision: 33
ms.author: "mikeray"
manager: "jhubbard"
---
# Transactions - Always On Availability and Database Mirroring
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

This topic describes cross-database and distributed transactions support for Always On Availability Groups and database mirroring.  
  
## Support for cross-database transactions within the same SQL Server instance  
Cross-database transactions within the same SQL Server instance are not supported for Always On Availability Groups. This means that no two databases in a cross-database transaction may be hosted by the same SQL Server instance. This is true even if those databases are part of the same Availability Group.  
  
Cross-database transactions are also not supported for database mirroring.  
  
##  <a name="dtcsupport"></a> Support for distributed transactions  
Distributed transactions are supported with Always On Availability Groups. This applies to distributed transactions between databases hosted by two different SQL Server instances. It also applies to distributed transactions between SQL Server and another DTC-compliant server.  
 
Microsoft Distributed Transaction Coordinator (MSDTC or DTC) is a Windows service that provides transaction infrastructure for distributed systems. MSDTC permits client applications to include multiple data sources in one transaction which then is committed across all servers included in the transaction. For example, you can use MSDTC to coordinate transactions that span multiple databases on different servers.

SQL Server 2016 introduces the capability to use distributed transactions where one or more of the databases in the transaction are in an Availability Group. Prior to SQL Server 2016 distributed transactions were not supported for databases in Availability Groups. SQL Server 2016 can register a resource manager per database. This new capability is why distributed transactions can include databases in Availability Groups.

  
 The following requirements must be met:  
  
-   Availability Groups must be running on Windows Server 2016 or Windows Server 2012 R2. For Windows Server 2012 R2, you must install the update in KB3090973 available at [https://support.microsoft.com/en-us/kb/3090973](https://support.microsoft.com/en-us/kb/3090973).  
  
-   Availability Groups must be created with the **CREATE AVAILABILITY GROUP** command and the **WITH DTC_SUPPORT = PER_DB** clause. You cannot currently alter an existing Availability Group.  

- All instances of SQL Server  that will participate in the Availability Group must be SQL Server 2016 or later.
  
 
 ## Non-support for distributed transactions
 Specific cases where distributed transactions are not supported include:
 
 -  Where more than one database involved in the transaction is in the same availability group.
 
 -  Where at least one database is in an availability group and another database is on the same instance of SQL Server. 
 
 -  Where the availability group was not created with enable distributed transaction.
 
 -  Database mirroring.
 
 ## Recommendation
 In production environments you should cluster the DTC service. If you do not cluster the DTC service, SQL Server will use the local DTC service. With the local DTC service, the overall availability of the solution is reduced. When DTC is clustered, in-process transactions can be recovered if the cluster node fails.
 
 > [!IMPORTANT]
 > Determine the appropriate default outcome of transactions that DTC is unable to resolve for your environment.  For information on how to configure the default outcome see [in-doubt xact resolution Server Configuration Option](../../../database-engine/configure-windows/in-doubt-xact-resolution-server-configuration-option.md).
  
## Example scenario with database mirroring  
 The following database mirroring example illustrates how a logical inconsistency could occur. In this example, an application uses a cross-database transaction to insert two rows of data: one row is inserted into a table in a mirrored database, A, and the other row is inserted into a table in another database, B. Database A is being mirrored in high-safety mode with automatic failover. While the transaction is being committed, database A becomes unavailable, and the mirroring session automatically fails over to the mirror of database A.  
  
 After the failover, the cross-database transaction might be successfully committed on database B but not on the failed-over database. This would occur if the original principal server for database A had not sent the log for the cross-database transaction to the mirror server before the failure. After the failover, that transaction would not exist on the new principal server. Databases A and B would become inconsistent, because the data inserted in database B remains intact, but the data inserted in database A has been lost.  
  
 A similar scenario can occur while using a MS DTC transaction. For example, after failover, the new principal contacts MS DTC. But MS DTC has no knowledge of the new principal server, and it terminates any transactions that are "preparing to commit," which are considered committed in other databases.  
  
> [!NOTE]  
>  Using Database Mirroring with DTC or using Always On Availability Groups with DTC in ways not approved in this topic is not supported.  This does not imply that aspects of the product unrelated to DTC are unsupported; however, any issues arising from the improper use of distributed transactions will not be supported.  
  
## See Also  
 [Always On Availability Groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)  
  
  
