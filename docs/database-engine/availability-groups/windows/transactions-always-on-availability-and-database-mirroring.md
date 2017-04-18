---
title: "Transactions - Always On Availability Groups and Database Mirroring | Microsoft Docs"
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
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Transactions - Always On availability groups and Database Mirroring
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

This topic describes cross-database and distributed transactions support for Always On availability groups and database mirroring.  

## Support for distributed transactions

SQL Server 2017 supports distributed transactions for databases in availability groups. This support includes cross-database transactions, for example, databases on the same instance of SQL Server. Distributed transactions are not supported for databases configured for database mirroring.

### Handling unresolved transaction

Distributed transaction coordinator (DTC) distributed transactions follow a 2-phase commit protocol (2PC). 

- **Prepare phase**
   
   When DTC receives a commit request, it sends a prepare command to all the resource managers involved in the transaction. Each resource manager then ensures that the transaction is durable (e.g. hardening log to disk). As each resource manager completes the prepare phase, it returns success or failure to the transaction manager.

- **Commit phase**
   
   If DTC receives successful prepares from all the resource managers, it sends commit commands to all resource managers. The resource managers can then complete the commit. If all resource managers report a successful commit, then DTC sends a success notification to the application. If any resource manager reported a failure to prepare, DTC sends a rollback command to each resource manager and indicates the failure of the commit to the application.

See more information at:

- [DTC Administration Guide](http://msdn.microsoft.com/library/ms681291.aspx)
- [DTC Developers Guide](http://msdn.microsoft.com/library/ms679938.aspx)
- [DTC Programmers Reference](http://msdn.microsoft.com/library/ms686108.aspx)

SQL Server uses a resource manager identifier to uniquely identify the database as resource manager in conjunction with DTC. SQL Server will not be able to resolve the outcome of the transactions that are in prepared state while actions that result in change of resource manager identifier occur. The actions include:
- Adding new database to the availability group
- Removing a database from the availability group
- Updating the dtc_support option for the availability group

During these actions, SQL Server enlists first with DTC with the old resource manager identifier. The identifier is changed following the action and SQL Server will try to determine the transaction outcome using the new resource manager identifier. This will fail as the new identifier is not recognized by the DTC. As of SQL Server 2017 CTP2.0, SQL Server will presume transactions in 'prepared' state are aborted in this case.
An option to let users choose the outcome of the transactions in this corner case is being worked on and will be available in the upcoming preview release.


## SQL Server 2016 and before: Support for cross-database transactions within the same SQL Server instance  

In SQL Server 2016 and before, cross-database transactions within the same SQL Server instance are not supported for availability groups. This means that no two databases in a cross-database transaction may be hosted by the same SQL Server instance. This is true even if those databases are part of the same availability group.  
  
Cross-database transactions are also not supported for database mirroring.  
  
##  <a name="dtcsupport"></a> SQL Server 2016: Support for distributed transactions  
Distributed transactions are supported with availability groups. This applies to distributed transactions between databases hosted by two different SQL Server instances. It also applies to distributed transactions between SQL Server and another DTC-compliant server.  
 
Microsoft Distributed Transaction Coordinator (MSDTC or DTC) is a Windows service that provides transaction infrastructure for distributed systems. MSDTC permits client applications to include multiple data sources in one transaction which then is committed across all servers included in the transaction. For example, you can use MSDTC to coordinate transactions that span multiple databases on different servers.

SQL Server 2016 introduces the capability to use distributed transactions where one or more of the databases in the transaction are in an availability group. Prior to SQL Server 2016 distributed transactions were not supported for databases in availability groups. SQL Server 2016 can register a resource manager per database. This new capability is why distributed transactions can include databases in availability groups.

  
 The following requirements must be met:  
  
-   Availability groups must be running on Windows Server 2016 or Windows Server 2012 R2. For Windows Server 2012 R2, you must install the update in KB3090973 available at [https://support.microsoft.com/en-us/kb/3090973](https://support.microsoft.com/en-us/kb/3090973).  
  
-   Availability groups must be created with the **CREATE AVAILABILITY GROUP** command and the **WITH DTC_SUPPORT = PER_DB** clause. You cannot currently alter an existing availability group.  

- All instances of SQL Server  that will participate in the availability group must be SQL Server 2016 or later.
  
 
 ## Non-support for distributed transactions
 Specific cases where distributed transactions are not supported include:
 
 - In SQL Server 2016 and prior, where more than one database involved in the transaction is in the same availability group.
 
 - In SQL Server 2016 and prior, where at least one database is in an availability group and another database is on the same instance of SQL Server. 
 
 - Where the availability group was not created with enable distributed transaction.
 
 - Database mirroring.
 
 ## Recommendation
 In production environments you should cluster the DTC service. If you do not cluster the DTC service, SQL Server will use the local DTC service. With the local DTC service, the overall availability of the solution is reduced. When DTC is clustered, in-process transactions can be recovered if the cluster node fails.
 
 > [!IMPORTANT]
 > Determine the appropriate default outcome of transactions that DTC is unable to resolve for your environment.  For information on how to configure the default outcome see [in-doubt xact resolution Server Configuration Option](../../../database-engine/configure-windows/in-doubt-xact-resolution-server-configuration-option.md).
  
## Example scenario with database mirroring  
 The following database mirroring example illustrates how a logical inconsistency could occur. In this example, an application uses a cross-database transaction to insert two rows of data: one row is inserted into a table in a mirrored database, A, and the other row is inserted into a table in another database, B. Database A is being mirrored in high-safety mode with automatic failover. While the transaction is being committed, database A becomes unavailable, and the mirroring session automatically fails over to the mirror of database A.  
  
 After the failover, the cross-database transaction might be successfully committed on database B but not on the failed-over database. This would occur if the original principal server for database A had not sent the log for the cross-database transaction to the mirror server before the failure. After the failover, that transaction would not exist on the new principal server. Databases A and B would become inconsistent, because the data inserted in database B remains intact, but the data inserted in database A has been lost.  
  
 A similar scenario can occur while using a MS DTC transaction. For example, after failover, the new principal contacts MS DTC. But MS DTC has no knowledge of the new principal server, and it terminates any transactions that are "preparing to commit," which are considered committed in other databases.  
  
> [!NOTE]  
>  Using Database Mirroring with DTC or using availability groups with DTC in ways not approved in this topic is not supported.  This does not imply that aspects of the product unrelated to DTC are unsupported; however, any issues arising from the improper use of distributed transactions will not be supported.  
  
## See Also  
 [Always On availability groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)  
  
  
