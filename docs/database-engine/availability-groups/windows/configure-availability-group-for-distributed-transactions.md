---
title: "Configure Availability Group for Distributed Transactions | Microsoft Docs"
ms.custom: ""
ms.date: "07/12/2017"
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

[!INCLUDE[SQL2017](../../../includes/sssqlv14-md.md)] supports distributed transactions for databases in availability groups. This support includes transactions containing multiple databases on the same instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] as well as databases across multiple instances. 

[!INCLUDE[SQL2016](../../../includes/sssql15-md.md)] also supports distributed transactions for databases in availability groups, however this support does not include transactions involving two or more databases on the same server. The transactions can be between multiple databases from different servers. [!INCLUDE[SQL2016](../../../includes/sssql15-md.md)] does not support distributed transactions for databases in availability groups when more than one database is on the same server.

In a distributed transaction, client applications work with Microsoft Distributed Transaction Coordinator (MS DTC or DTC) to guarantee transactional consistency across multiple data sources. DTC is a service available on all supported operating systems. For a distributed transaction, DTC is the *transaction coordinator*. Normally, a SQL Server instance is the *resource manager*. When a database is in an availability group, each database needs to be its own resource manager. 

Configure an availability group to support distributed transactions. Set the availability group to allow each database to register as a resource manager. This article explains how to configure an availability group so that each database can be a resource manager in DTC.

[!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] does not prevent distributed transactions for databases in an availability group - even when the availability group is not configured for distributed transactions. However when an availability group is not configured for distributed transactions, failover may not succeed in some situations. Specifically the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance may not be able to get the transaction outcome from DTC. To enable the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance to get the outcome of in-doubt transactions from the DTC after failover, configure the availability group for distributed transactions. 

## Prerequisites

All instances of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] that participate in the distributed transaction must be  [!INCLUDE[SQL2016](../../../includes/sssql15-md.md)] or later.

Availability groups must be running on Windows Server 2016 or Windows Server 2012 R2. For Windows Server 2012 R2, you must install the update in KB3090973 available at [https://support.microsoft.com/en-us/kb/3090973](https://support.microsoft.com/en-us/kb/3090973).  

## Create an availability group for distributed transactions

You can create an availability group for distributed transactions on  [!INCLUDE[SQL2016](../../../includes/sssql15-md.md)] or later. To create an availability group for distributed transactions, include `DTC_SUPPORT = PER_DB` in the availability group definition. The following script creates an availability group for distributed transactions. 

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

Distributed transactions span two or more databases. The management of the transaction must be coordinated between the resource managers by a server component called a transaction manager. Each instance of the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] Database Engine can operate as a resource manager in distributed transactions coordinated by transaction managers, such as Microsoft Distributed Transaction Coordinator (MS DTC), or other transaction managers that support the Open Group XA specification for distributed transaction processing. When an availability group is configured with `DTC_SUPPORT = PER_DB`, the databases can operate as resource managers. For more information, see the MS DTC documentation.

A transaction within a single instance of the Database Engine that spans two or more databases is actually a distributed transaction. The instance manages the distributed transaction internally; to the user, it operates as a local transaction. [!INCLUDE[SQL2017](../../../includes/sssqlv14-md.md)] handles all cross-database transactions as distributed transactions when databases are in an availability group configured with `DTC_SUPPORT = PER_DB` - even within a single instance of SQL Server. 

At the application, a distributed transaction is managed much the same as a local transaction. At the end of the transaction, the application requests the transaction to be either committed or rolled back. A distributed commit must be managed differently by the transaction manager to minimize the risk that a network failure may result in some resource managers successfully committing while others roll back the transaction. This is achieved by managing the commit process in two phases (the prepare phase and the commit phase), which is known as a two-phase commit (2PC).

- **Prepare phase**
   
   When the transaction manager receives a commit request, it sends a prepare command to all of the resource managers involved in the transaction. Each resource manager then does everything required to make the transaction durable, and all buffers holding log images for the transaction are flushed to disk. As each resource manager completes the prepare phase, it returns success or failure of the prepare to the transaction manager.

- **Commit phase**
   
   If the transaction manager receives successful prepares from all of the resource managers, it sends commit commands to each resource manager. The resource managers can then complete the commit. If all of the resource managers report a successful commit, the transaction manager then sends a success notification to the application. If any resource manager reported a failure to prepare, the transaction manager sends a rollback command to each resource manager and indicates the failure of the commit to the application.

### Detailed steps

The following list explains how the application works with DTC to complete distributed transactions.

1. When an application requires a distributed transaction, it connects to a DTC to begin the transaction. The client owns the DTC transaction. DTC is the transaction manager. 
2. The client then connects to a [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance and enlists in the DTC transaction and creates another resource manager. Normally, the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance this resource manager. If the database is in an availability group and registered for DTC support, the database is this resource manager. This resource manager exchanges transaction information with DTC. 
3. The client does some work in the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance under the DTC transaction. The [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance holds locks, and preserves references to the DTC transaction. 
4. The client either disconnects or enlists in NULL. The client can disconnect from the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance. The [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance unhooks the connection from the DTC transaction it is tracking. The transaction object remains in the list of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] transactions because it is active. It stays active until the DTC resource manager indicates either abort or commit.
5. After the client has completed the work on all resources, DTC sends either abort or commit to the [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance - and any other resources in the transaction.
6. The [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance either commits or aborts the transaction and releases the locks.

### Effects of configuring an availability group for distributed transactions

Each entity participating in a distributed transaction is called a *resource manager*. Examples of resource managers include:

* A [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance. 
* A database in an availability group that has been configured for distributed transactions.
* Other data sources. 

In order to participate in distributed transactions, an instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] enlists with a DTC. Normally the instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] enlists with DTC on the local server. Each instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] creates a resource manager identifier (RMID) and registers it with DTC. In the default configuration, all databases on an instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] use the same RMID. 

When a database is in an availability group, the read-write copy of the database - or primary replica - may move to a different instance of [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)]. To support distributed transactions during this movement, each database should act as a separate resource manager and must have a unique RMID. When an availability group has `DTC_SUPPORT = PER_DB`, [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] creates a resource manager for each database and registers with DTC using a unique RMID. In this configuration, the database is a resource manager for DTC transactions.

For more detail on distributed transactions in [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)], see [Distributed transactions](transactions-always-on-availability-and-database-mirroring.md#distTran)

## Manage in-doubt transactions

When an availability group fails over, while distributed transactions are pending, the instance that hosts the primary replica contacts DTC to find out the results of the transactions. If databases in the availability group are not configured for distributed transactions, the RMID is the new [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance. The RMID when the transaction began was from the instance that held the primary replica before failover. The failover results in a changed RMID. The new [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance cannot get the transaction outcome from DTC for active transactions because the RMID has changed. The following cases can result in a changed RMID:

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

The preceding example shows that DTC could not enlist the database from the new primary replica in the transaction that was created after failover. The [!INCLUDE[SQLServer](../../../includes/ssnoversion_md.md)] instance cannot determine the result of the distributed transaction so it marks the database as suspect. The transaction is marked as a unit of work (UOW) and referred to by a GUID. In order to recover the database, either commit or rollback the transaction manually. 

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

## See Also  


[Distributed Transactions](http://docs.microsoft.com/dotnet/framework/data/adonet/distributed-transactions)

[Always On availability groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)  
  
[Transactions - Always On availability groups and Database Mirroring](transactions-always-on-availability-and-database-mirroring.md)  
