---
title: "Configure Availability Group for Distributed Transactions | Microsoft Docs"
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
# Configure Availability Group for Distributed Transactions
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

This article explains how to configure an availability group for distributed transactions  

## Support for distributed transactions

SQL Server 2017 supports distributed transactions for databases in availability groups. This support includes cross-database transactions, for example, databases on the same instance of SQL Server. In order to prevent in-doubt transactions after an availability group fails over, configure the availability group for distributed transactions. 

SQL Server 2016 also supports distributed transactions for databases in availability groups, however this support does not include transactions involving two or more databases on the same instance of SQL Server. 

>[!NOTE]
>SQL Server does not prevent distributed transactions for databases in an availability group - even when the availability group is not configured for distributed transactions. However, databases in an availability group that are not configured for distributed transactions are vulnerable to in-doubt transactions under specific scenarios. 

All instances of SQL Server that will participate in the distributed transaction must be SQL Server 2016 or later.

Availability groups must be running on Windows Server 2016 or Windows Server 2012 R2. For Windows Server 2012 R2, you must install the update in KB3090973 available at [https://support.microsoft.com/en-us/kb/3090973](https://support.microsoft.com/en-us/kb/3090973).  

## Create an availability group for distributed transactions

You can create an availability group for distributed transactions on SQL Server 2016 or later. To create an availability group for distributed transactions, include `DTC_SUPPORT = PER_DB` in the availability group definition. The example script below creates an availability group for distributed transactions. 

```transact-sql
CREATE AVAILABILITY GROUP MyAG
   WITH (
      DTC_SUPPORT = PER_DB  
      )
   FOR DATABASE DB1, DB2
   REPLICA ON
      Server1 WITH (
         ENDPOINT_URL = 'TCP://Server1:5022',  
         AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,  
         FAILOVER_MODE = AUTOMATIC  
         )
      Server2 WITH (
         ENDPOINT_URL = 'TCP://Server2:5022',  
         AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,  
         FAILOVER_MODE = AUTOMATIC  
         )
```

>[!NOTE]
>The script above is a simple example of an availability group and is not designed for any specific production environment. 

## Alter an availability group for distributed transactions

You can alter an availability group for distributed transactions on SQL Server 2017 or later. To alter an availability group for distributed transactions, include `DTC_SUPPORT = PER_DB` in the `ALTER AVAILABILITY GROUP` script. The example script changes the availability group to support distributed transactions. 

```transact-sql
ALTER AVAILABILITY GROUP MyaAG
   WITH (
      DTC_SUPPORT = PER_DB  
      );
```

>[!NOTE]
>On SQL Server 2016 you cannot alter an availability group for distributed transactions. To change the setting drop, and recreate the availability group with the `DTC_SUPPORT = PER_DB` setting. 

## Effects of configuring an availability group for distributed transactions

In order to participate in distributed transactions, an instance of SQL Server enlists with a distributed transaction coordinator (DTC) service. Normally the instance of SQL Server enlists with the DTC service on the local server. The DTC service assigns the instance of SQL Server a resource manager identification (RMID). In the default configuration, all databases on an instance of SQL Server use the same RMID. The instance of SQL Server is the *resource manager* for DTC transactions. 

When a database is in an availability group, the read-write copy of the database - or primary replica - may move to a different instance of SQL Server. To support distributed transactions during this movement, each database must have a unique RMID. When an availability group is configured with `DTC_SUPPORT = PER_DB` SQL Server registers an RMID for each database in the availability group with the DTC service. In this configuration, the database is the resource manager for DTC transactions.

## See Also  


[Distributed Transactions](http://docs.microsoft.com/dotnet/framework/data/adonet/distributed-transactions)

[Always On availability groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)  
  
[Transactions - Always On availability groups and Database Mirroring](transactions-always-on-availability-and-database-mirroring.md)  
