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
ms.assetid: 
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

SQL Server 2016 also supports distributed transactions for databases in availability groups, however this support does not include transactions involving two or more databases on the server. 

>[!NOTE]
>SQL Server does not prevent distributed transactions for databases in an availability group - even when the availability group is not configured for distributed transactions. However, databases in an availability group that are not configured for distributed transactions are vulnerable to in-doubt transactions under specific scenarios. 

All instances of SQL Server that participate in the distributed transaction must be SQL Server 2016 or later.

Availability groups must be running on Windows Server 2016 or Windows Server 2012 R2. For Windows Server 2012 R2, you must install the update in KB3090973 available at [https://support.microsoft.com/en-us/kb/3090973](https://support.microsoft.com/en-us/kb/3090973).  

## Create an availability group for distributed transactions

You can create an availability group for distributed transactions on SQL Server 2016 or later. To create an availability group for distributed transactions, include `DTC_SUPPORT = PER_DB` in the availability group definition. The following script creates an availability group for distributed transactions. 

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
>The preceding script is a simple example of an availability group and is not designed for any specific production environment. 

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

In order to participate in distributed transactions, an instance of SQL Server enlists with a distributed transaction coordinator (DTC) service. Normally the instance of SQL Server enlists with the DTC service on the local server. The DTC service assigns the instance of SQL Server a resource manager identification (RMID). In the default configuration, all databases on an instance of SQL Server use the same RMID. The instance of SQL Server is a *resource manager* for DTC transactions. 

When a database is in an availability group, the read-write copy of the database - or primary replica - may move to a different instance of SQL Server. To support distributed transactions during this movement, each database must have a unique RMID. When an availability group is configured with `DTC_SUPPORT = PER_DB`, SQL Server registers an RMID for each database in the availability group with the DTC service. In this configuration, the database is a resource manager for DTC transactions.

## How distributed transactions work

The following list explains how distributed transactions work.

1. When an application requires a distributed transaction, it connects to a DTC service to begin the transaction. The client owns the DTC transaction. The DTC service is one resource manager. 
2. The client then connects to a SQL Server instance and enlists in the DTC transaction. Normally, the SQL Server instance is also the resource manager. If the database is in an availability group and registered for DTC support, the database is a resource manager. The SQL Server instance or database resource manager exchange transaction information with the DTC service. 
3. The client does some work in the SQL Server instance under the DTC transaction. The SQL Server instance holds locks, and preserves references to the DTC transaction. 
4. The client either disconnects or enlists in NULL. The client can disconnect from the SQL Server instance. The SQL Server instance unhooks the connection from the DTC transaction it is tracking. The transaction object remains in the list of SQL Server transactions because it is active. It stays active until the DTC resource manager indicates either abort or commit.
5. After the client has completed the work on all resources, the DTC service sends either abort or commit to the SQL Server instance - and any other resources in the transaction.
6. The SQL Server instance either commits or aborts the transaction and releases the locks.

### Manage in-doubt transactions
When DTC support is not configured per database, a database in an availability group can have in-doubt transactions in the following cases:

* Set distributed transaction support per database while transactions are in flight. 
* Add or remove a database while transactions are in flight. 
* Drop an availability group.

If the preceding cases happen while `DTC_SUPPORT = NONE`, and the primary replica fails over to a new instance of SQL Server, the instance tries to contact the DTC service to identify the transaction result. The DTC service cannot identify the transaction result because the resource manager was registered under the instance of SQL Server that hosted the primary replica before fail over. Therefore the database goes into SUSPECT state.

The new SQL Server error log has an entry like the following example:

```
Microsoft Distributed Transaction Coordinator (MS DTC) 
failed to reenlist citing that the database RMID does 
not match the RMID [xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx] 
associated with the transaction.  Please manually resolve
the transaction.
	
SQL Server detected a DTC/KTM in-doubt transaction with UOW 
{yyyyyyyy-yyyy-yyyy-yyyy-yyyyyyyyyyyy}.Please resolve it 
following the guideline for Troubleshooting DTC Transactions.
```

The preceding example shows that the DTC service could not enlist the database from the new primary replica in the transaction that was created before failover. The SQL Server instance cannot determine the result of the result of the distributed transaction so it marks the database as suspect. In order to recover the database, either commit or rollback the transaction manually. 

>[!WARNING]
>When you manually commit or rollback a transaction it can affect an application. Verify that the action of commit or rollback is consistent with your application requirements. 

Run only one of the following scripts:

   * To commit the transaction, update and run the following script - replace the `yyyyyyyy-yyyy-yyyy-yyyy-yyyyyyyyyyyy` with the in-doubt transaction UOW from the previous error message, and run:

      ```transact-sql
      KILL 'yyyyyyyy-yyyy-yyyy-yyyy-yyyyyyyyyyyy' WITH COMMIT
      ```

   * To roll back the transaction, update and run the following script - replace the `yyyyyyyy-yyyy-yyyy-yyyy-yyyyyyyyyyyy` with the in-doubt transaction UOW from the previous error message, and run:

      ```transact-sql
      KILL 'yyyyyyyy-yyyy-yyyy-yyyy-yyyyyyyyyyyy' WITH ROLLBACK
     ```

After you commit or roll back the transaction, you can use `ALTER DATABASE` to set the database online. Update and run the following script - set the database name for the name of the suspect database:

   ```transact-sql
   ALTER DATABASE [DB1] SET ONLINE
   ```


## See Also  


[Distributed Transactions](http://docs.microsoft.com/dotnet/framework/data/adonet/distributed-transactions)

[Always On availability groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)  
  
[Transactions - Always On availability groups and Database Mirroring](transactions-always-on-availability-and-database-mirroring.md)  
