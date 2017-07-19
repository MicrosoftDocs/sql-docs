---
title: "Configure availability group for distributed transactions | Microsoft Docs"
ms.custom: ""
ms.date: "07/19/2017"
ms.prod: 
   - "sql-server-2016"
   - "sql-server-2017"
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
# Configure availability group for distributed transactions
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

This article explains how to configure an availability group for distributed transactions  

## Support for distributed transactions

[!INCLUDE[SQL2016](../../../includes/sssql15-md.md)] supports distributed transactions for databases in availability groups across multiple data sources, including SQL Server instances on different servers. [!INCLUDE[SQL2017](../../../includes/sssqlv14-md.md)] adds support for all distributed transactions - including transactions between:

- Databases on the same server
- Databases on the same instance of SQL Server
- Databases on different instances of SQL Server
- Databases on different servers

In a distributed transaction, client applications work with Microsoft Distributed Transaction Coordinator (MS DTC or DTC) to guarantee transactional consistency across multiple data sources. DTC is a service available on supported Windows Server-based operating systems. For a distributed transaction, DTC is the *transaction coordinator*. Normally, a SQL Server instance is the *resource manager*. When a database is in an availability group, each database needs to be its own resource manager. 

[!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] does not prevent distributed transactions for databases in an availability group - even when the availability group is not configured for distributed transactions. However when an availability group is not configured for distributed transactions, failover may not succeed in some situations. Specifically the new primary replica [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance may not be able to get the transaction outcome from DTC. To enable the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance to get the outcome of in-doubt transactions from the DTC after failover, configure the availability group for distributed transactions. 

## Prerequisites

Before you configure an availability group to support distributed transactions, you must meet the following prerequisites:

* All instances of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] that participate in the distributed transaction must be  [!INCLUDE[SQL2016](../../../includes/sssql15-md.md)] or later.

* Availability groups must be running on Windows Server 2016 or Windows Server 2012 R2. For Windows Server 2012 R2, you must install the update in KB3090973 available at [https://support.microsoft.com/en-us/kb/3090973](https://support.microsoft.com/en-us/kb/3090973).  

## Create an availability group for distributed transactions

Configure an availability group to support distributed transactions. Set the availability group to allow each database to register as a resource manager. This article explains how to configure an availability group so that each database can be a resource manager in DTC.

You can create an availability group for distributed transactions on  [!INCLUDE[SQL2016](../../../includes/sssql15-md.md)] or later. To create an availability group for distributed transactions, include `DTC_SUPPORT = PER_DB` in the availability group definition. The following script creates an availability group for distributed transactions. 

```transact-sql
CREATE AVAILABILITY GROUP MyAG
   WITH (
      DTC_SUPPORT = PER_DB  
      )
   FOR DATABASE DB1, DB2
   REPLICA ON
      Server1 WITH (
         ENDPOINT_URL = 'TCP://SERVER1.corp.com:5022',  
         AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,  
         FAILOVER_MODE = AUTOMATIC  
         )
      Server2 WITH (
         ENDPOINT_URL = 'TCP://SERVER2.corp.com:5022',  
         AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,  
         FAILOVER_MODE = AUTOMATIC  
         )
```

>[!NOTE]
>The preceding script is a simple example of an availability group and is not designed for any specific production environment. 

## Alter an availability group for distributed transactions

You can alter an availability group for distributed transactions on [!INCLUDE[SQL2017](../../../includes/sssqlv14-md.md)] or later. To alter an availability group for distributed transactions, include `DTC_SUPPORT = PER_DB` in the `ALTER AVAILABILITY GROUP` script. The example script changes the availability group to support distributed transactions. 

```transact-sql
ALTER AVAILABILITY GROUP MyaAG
   WITH (
      DTC_SUPPORT = PER_DB  
      );
```

>[!NOTE]
>On [!INCLUDE[SQL2016](../../../includes/sssql15-md.md)] you cannot alter an availability group for distributed transactions. To change the setting drop, and recreate the availability group with the `DTC_SUPPORT = PER_DB` setting. 

## <a name="distTran"/>Distributed transactions - technical concepts

A distributed transaction spans two or more databases. As the transaction manager, DTC coordinates the transaction between SQL Server instances, and other data sources. Each instance of the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] database engine can operate as a resource manager. When an availability group is configured with `DTC_SUPPORT = PER_DB`, the databases can operate as resource managers. For more information, see the MS DTC documentation.

A transaction with two or more databases in a single instance of the database engine is actually a distributed transaction. The instance manages the distributed transaction internally; to the user, it operates as a local transaction. [!INCLUDE[SQL2017](../../../includes/sssqlv14-md.md)] promotes all cross-database transactions to distributed transactions when databases are in an availability group configured with `DTC_SUPPORT = PER_DB` - even within a single instance of SQL Server. 

At the application, a distributed transaction is managed much the same as a local transaction. At the end of the transaction, the application requests the transaction to be either committed or rolled back. A distributed commit must be managed differently by the transaction manager to minimize the risk that a network failure may result in some resource managers successfully committing while others roll back the transaction. This is achieved by managing the commit process in two phases (the prepare phase and the commit phase), which is known as a two-phase commit (2PC).

- **Prepare phase**
   
   When the transaction manager receives a commit request, it sends a prepare command to all of the resource managers involved in the transaction. Each resource manager then does everything required to make the transaction durable, and all buffers holding log images for the transaction are flushed to disk. As each resource manager completes the prepare phase, it returns success or failure of the prepare to the transaction manager.

- **Commit phase**
   
   If the transaction manager receives successful prepares from all of the resource managers, it sends commit commands to each resource manager. The resource managers can then complete the commit. If all of the resource managers report a successful commit, the transaction manager then sends a success notification to the application. If any resource manager reported a failure to prepare, the transaction manager sends a rollback command to each resource manager and indicates the failure of the commit to the application.

### Detailed steps

The following list explains how the application works with DTC to complete distributed transactions.

1. When an application requires a distributed transaction, it connects to a DTC to begin the transaction. The client owns the DTC transaction. DTC is the transaction manager. 
2. The client then connects to a [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance and enlists in the DTC transaction and creates another resource manager. Normally, the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance this resource manager. If the database is in an availability group and registered for DTC support, the database is this resource manager. This resource manager exchanges transaction information with DTC. 

   >[!NOTE]
   >It is not necessary for the client to initiate the distributed transaction. If a client connects to an instance of SQL Server 2017, and the connection initiates a transaction that involves more than one database on the instance, SQL Server promotes the transaction to a distributed transaction. 

3. The client does some work in the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance under the DTC transaction. The [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance holds locks, and preserves references to the DTC transaction.
4. The client either disconnects or enlists in NULL. The client can disconnect from the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance. The [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance unhooks the connection from the DTC transaction it is tracking. The transaction object remains in the list of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] transactions because it is active. It stays active until the DTC resource manager indicates either abort or commit.
5. After the client has completed the work on all resources, DTC performs the 2-phase commit protocol to either abort or commit to the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance - and any other resources in the transaction.
6. The [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance either commits or aborts the transaction and releases the locks.

### Effects of configuring an availability group for distributed transactions

Each entity participating in a distributed transaction is called a resource manager. Examples of resource managers include:

* A [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance. 
* A database in an availability group that has been configured for distributed transactions.
* DTC service - can also be a transaction manager.
* Other data sources. 

In order to participate in distributed transactions, an instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] enlists with a DTC. Normally the instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] enlists with DTC on the local server. Each instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] creates a resource manager with a unique resource manager identifier (RMID) and registers it with DTC. In the default configuration, all databases on an instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] use the same RMID. 

When a database is in an availability group, the read-write copy of the database - or primary replica - may move to a different instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)]. To support distributed transactions during this movement, each database should act as a separate resource manager and must have a unique RMID. When an availability group has `DTC_SUPPORT = PER_DB`, [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] creates a resource manager for each database and registers with DTC using a unique RMID. In this configuration, the database is a resource manager for DTC transactions.

For more detail on distributed transactions in [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)], see [Distributed transactions](#distTran)

## Manage unresolved transactions

The outcome for the active transactions that exists during RMID change cannot be recovered after a failover. This is because the RMID SQL Server used to enlist and RMID SQL Server used to recover are different. The RMID change can happen in following cases:

* Change `DTC_SUPPORT` for an availability group. 
* Add or remove a database from an availability group. 
* Drop an availability group.

If the preceding cases happen while `DTC_SUPPORT = NONE`, and the primary replica fails over to a new instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)], the instance tries to contact DTC to identify the transaction result. DTC cannot return the outcome because the RMID that the database is using to get the outcome for in-doubt transactions during recovery was not enlisted before. Therefore the database goes into SUSPECT state.

The new [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] error log has an entry like the following example:

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

The preceding example shows that DTC could not re-enlist the database from the new primary replica in the transaction that was created after failover. The [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance cannot determine the result of the distributed transaction so it marks the database as suspect. The transaction is marked as a unit of work (UOW) and referred to by a GUID. In order to recover the database, either commit or rollback the transaction manually. 

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

For more information about resolving in-doubt transactions, see [Resolve Transactions Manually](http://technet.microsoft.com/library/cc754134.aspx).

## Next Steps  

[Distributed Transactions](http://docs.microsoft.com/dotnet/framework/data/adonet/distributed-transactions)

[Always On availability groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)  
  
[Transactions - Always On availability groups and Database Mirroring](transactions-always-on-availability-and-database-mirroring.md)  

[Supporting XA Transactions](http://technet.microsoft.com/library/cc753563(v=ws.10).aspx)

[How It Works: Session/SPID (–2) for DTC Transactions](http://blogs.msdn.microsoft.com/bobsql/2016/08/04/how-it-works-sessionspid-2-for-dtc-transactions/)