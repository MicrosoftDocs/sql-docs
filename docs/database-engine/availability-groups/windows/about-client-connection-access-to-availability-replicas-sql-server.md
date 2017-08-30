---
title: "About client connection access to availability replicas (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], readable secondary replicas"
  - "active secondary replicas [SQL Server], read-only access to"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], client connectivity"
  - "Availability Groups [SQL Server], active secondary replicas"
ms.assetid: 29027e46-43e4-4b45-b650-c4cdeacdf552
caps.latest.revision: 16
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# About client connection access to availability replicas (SQL Server)
In an Always On availability group, you can configure one or more availability replicas to allow read-only connections when the replicas are running under the secondary role (that is, as a secondary replica). You can also configure each availability replica to allow or exclude read-only connections when they are running under the primary role (that is, as the primary replica). 
  
To facilitate client access to primary or secondary databases of a specified availability group, you should define an availability group listener. By default, the availability-group listener directs incoming connections to the primary replica. However, you can configure an availability group to support read-only routing, which enables its availability-group listener to redirect the connection requests of read-intent applications to a readable secondary replica. For more information, see [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md).  
  
During a failover, a secondary replica transitions to the primary role, and the former primary replica transitions to the secondary role. During the failover process, all client connections to both the primary replica and secondary replicas are terminated. After the failover, when a client reconnects to the availability group listener, the listener reconnects the client to the new primary replica, except for a read-intent connect request. 

If read-only routing is configured on (a) the client, (b) the server instances that host the new primary replica, and (c) at least one readable secondary replica, read-intent connection requests are re-routed to a secondary replica that supports the type of connection access that the client requires. To ensure a graceful client experience after a failover, it is important to configure connection access for both the secondary and primary roles of every availability replica.  
  
> [!NOTE]  
> For information about the availability-group listener, which handles client connection requests, see [Availability-group listeners, client connectivity, and application failover (SQL Server)](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md).  
  
##  Types of connection access supported by the secondary role  
The secondary role supports three alternatives for client connections, as follows:  
  
* **No connections**: No user connections are allowed. Secondary databases are not available for read access. This is the default behavior in the secondary role.  
  
* **Only read-intent connections**: The secondary databases are available only for connection for which the **Application Intent** connection property is set to **ReadOnly** (*read-intent connections*).  
  
    For information about this connection property, see [SQL Server native client support for high availability, disaster recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).  
  
* **Allow any read-only connection**: The secondary databases are all available for read access connections. This option allows lower-versioned clients to connect.  
  
For more information, see [Configure read-only access on an availability replica (SQL Server)](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md).  
  
##  Types of connection access supported by the primary role  
The primary role supports two alternatives for client connections, as follows:  
  
* **All connections are allowed**: Both read-write and read-only connections are allowed to primary databases. This is the default behavior for the primary role.  
  
* **Allow only read-write connections**: When the **Application Intent** connection property is set to **ReadWrite** or is not set, the connection is allowed. Connections for which the **Application Intent** connection-string keyword is set to **ReadOnly** are not allowed. Allowing only read-write connections can help prevent your customers from connecting a read-intent work load to the primary replica by mistake.  
  
    For information about this connection property, see [Using connection-string keywords with SQL Server native client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 For more information, see [Configure read-only access on an availability replica (SQL Server)](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md).  
  
##  How the connection-access configuration affects client connectivity  
 The connection-access settings of a replica determine whether a connection attempt fails or succeeds. The following table summarizes whether a given connection attempt succeeds or fails for each connection-access setting.  
  
|Replica role|Connection access supported on replica|Connection intent|Connection-attempt result|  
|------------------|--------------------------------------------|-----------------------|--------------------------------|  
|Secondary|All|Read-intent, read-write, or no connection intent specified|Success|  
|Secondary|None (This is the default secondary behavior.)|Read-intent, read-write, or no connection intent specified|Failure|  
|Secondary|Read-intent only|Read-intent|Success|  
|Secondary|Read-intent only|Read-write or no connection intent specified|Failure|  
|Primary|All (This is the default primary behavior.)|Read-only, read-write, or no connection intent specified|Success|  
|Primary|Read-write|Read-intent only|Failure|  
|Primary|Read-write|Read-write or no connection intent specified|Success|  
  
For information about configuring an availability group to accept client connections to its replicas, see [Availability-group listeners, client connectivity, and application failover (SQL Server)](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md).  
  
### Example connection-access configuration  
Depending on how various availability replicas are configured for connection access, support for client connections might change after an availability group fails over. For example, consider an availability group for which reporting is performed on remote asynchronous-commit secondary replicas. All the read-only applications for the databases in this availability group set their **Application Intent** connection property to **ReadOnly**, so that all read-only connections are read-intent connections.  
  
This example availability group possesses two synchronous-commit replicas at the main computing center and two asynchronous-commit replicas at a satellite site. For the primary role, all the replicas are configured for read-write access, which prevents read-intent connections to the primary replica in all situations. The synchronous commit secondary role uses the default connection-access configuration ("none"), which prevents all client connections under the secondary role. In contrast, the asynchronous commit replicas are configured to permit read-intent connections under the secondary role. The following table summarizes this example configuration:  
  
|Replica|Commit mode|Initial role|Connection access for secondary role|Connection access for primary role|  
|-------------|-----------------|------------------|------------------------------------------|----------------------------------------|  
|Replica1|Synchronous|Primary|None|Read-write|  
|Replica2|Synchronous|Secondary|None|Read-write|  
|Replica3|Asynchronous|Secondary|Read-intentonly|Read-write|  
|Replica4|Asynchronous|Secondary|Read-intent only|Read-write|  
  
Usually, in this example scenario, failovers occur only between the synchronous-commit replicas, and immediately after the failover, read-intent applications can reconnect to one of the asynchronous-commit secondary replicas. However, when a disaster occurs at the main computing center, both synchronous-commit replicas are lost. 

The database administrator at the satellite site responds by performing a forced manual failover to an asynchronous-commit secondary replica. The secondary databases on the remaining secondary replica are suspended by the forced failover, making them unavailable for read-only workloads. The new primary replica, which is configured for read-write connections, prevents the read-intent workload from competing with the read-write workload. This means that until the database administrator resumes the secondary databases on the remaining asynchronous-commit secondary replica, read-intent clients cannot connect to any availability replica.  
  
##  Related tasks  
  
-   [Configure read-only access on an availability replica (SQL Server)](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [Configure read-only routing for an availability group (SQL Server)](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
-   [Monitor availability groups (Transact-SQL)](../../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)  
  
-   [View availability replica properties (SQL Server)](../../../database-engine/availability-groups/windows/view-availability-replica-properties-sql-server.md)  
  
-   [Use the new availability-group dialog box (SQL Server Management Studio)](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
##  Related content  
  
-   [Microsoft SQL Server Always On solutions guide for high availability and disaster recovery](http://go.microsoft.com/fwlink/?LinkId=227600)  
  
-   [SQL Server Always On team blog: The official SQL Server Always On team blog](https://blogs.msdn.microsoft.com/sqlalwayson/)  
  
## See also  
 [Overview of Always On availability groups (SQL Server)](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Availability-group listeners, client connectivity, and application failover (SQL Server)](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)   
 [Statistics](../../../relational-databases/statistics/statistics.md)  
  
  
