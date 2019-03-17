---
title: "About Client Connection Access to Availability Replicas (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], readable secondary replicas"
  - "active secondary replicas [SQL Server], read-only access to"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], client connectivity"
  - "Availability Groups [SQL Server], active secondary replicas"
ms.assetid: 29027e46-43e4-4b45-b650-c4cdeacdf552
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# About Client Connection Access to Availability Replicas (SQL Server)
  In an AlwaysOn availability group, you can configure one or more availability replicas to allow read-only connections when running under the secondary role (that is, when running as a secondary replica). You can also configure each availability replica to allow or exclude read-only connections when running under the primary role (that is, when running as the primary replica).  
  
 To facilitate client access to primary or secondary databases of a given availability group, you should define an availability group listener. By default, the availability group listener directs incoming connections to the primary replica. However, you can configure an availability group to support read-only routing, which enables its availability group listener to redirect the connection requests of read-intent applications to a readable secondary replica. For more information, see [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](configure-read-only-routing-for-an-availability-group-sql-server.md).  
  
 During a failover, a secondary replica transitions to the primary role and the former primary replica transitions to the secondary role. During the failover process, all client connections to both the primary replica and secondary replicas are terminated. After the failover, when a client reconnects to the availability group listener, the listener reconnects the client to the new primary replica, except for a read-intent connect request. If read-only routing is configured on the client and on the server instances that hosts the new primary replica and on at least one readable secondary replica, read-intent connection requests are re-routed to a secondary replica that supports the type of connection access that the client requires. To ensure a graceful client experience after a failover, it is important to configure connection access for both the secondary and primary roles of every availability replica.  
  
> [!NOTE]  
>  For information about the availability group listener, which handles client connection requests, see [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../listeners-client-connectivity-application-failover.md).  
  
 **In This Topic:**  
  
-   [Types of Connection Access Supported by the Secondary Role](#ConnectAccessForSecondary)  
  
-   [Types of Connection Access Supported by the Primary Role](#ConnectAccessForPrimary)  
  
-   [How the Connection Access Configuration Affects Client Connectivity](#HowConnectionAccessAffectsConnectivity)  
  
-   [Related Tasks](#RelatedTasks)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="ConnectAccessForSecondary"></a> Types of Connection Access Supported by the Secondary Role  
 The secondary role supports three alternatives for client connections, as follows:  
  
 No connections  
 No user connections are allowed. Secondary databases are not available for read access. This is the default behavior in the secondary role.  
  
 Only read-intent connections  
 The secondary database(s) are available only for connection for which the `Application Intent` connection property is set to `ReadOnly` (*read-intent connections*).  
  
 For information about this connection property, see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).  
  
 Allow any read-only connection  
 The secondary database(s) are all available for read access connections. This option allows lower versioned clients to connect.  
  
 For more information, see [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](configure-read-only-access-on-an-availability-replica-sql-server.md).  
  
##  <a name="ConnectAccessForPrimary"></a> Types of Connection Access Supported by the Primary Role  
 The primary role supports two alternatives for client connections, as follows:  
  
 All connections are allowed  
 Both read-write and read-only connections are allowed to primary databases. This is the default behavior for the primary role.  
  
 Allow only read-write connections  
 When the `Application Intent` connection property is set to **ReadWrite** or is not set, the connection is allowed. Connections for which the `Application Intent` connection string keyword is set to `ReadOnly` are not allowed. Allowing only read-write connections can help prevent your customers from connecting a read-intent work load to the primary replica by mistake.  
  
 For information about this connection property, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 For more information, see [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](configure-read-only-access-on-an-availability-replica-sql-server.md).  
  
##  <a name="HowConnectionAccessAffectsConnectivity"></a> How the Connection Access Configuration Affects Client Connectivity  
 The connection access settings of a replica determine whether a connection attempt fails or succeeds. The following table summarizes whether a given connection attempt succeeds or fails for each the connection-access setting.  
  
|Replica Role|Connection Access Supported on Replica|Connection Intent|Connection-Attempt Result|  
|------------------|--------------------------------------------|-----------------------|--------------------------------|  
|Secondary|All|Read-intent, read-write, or no connection intent specified|Success|  
|Secondary|None (This is the default secondary behavior.)|Read-intent, read-write, or no connection intent specified|Failure|  
|Secondary|Read-intent only|Read-intent|Success|  
|Secondary|Read-intent only|Read-write or no connection intent specified|Failure|  
|Primary|All (This is the default primary behavior.)|Read-only, read-write, or no connection intent specified|Success|  
|Primary|Read-write|Read-intent only|Failure|  
|Primary|Read-write|Read-write or no connection intent specified|Success|  
  
 For information about configuring an availability group to accept client connections to its replicas, see [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../listeners-client-connectivity-application-failover.md).  
  
### Example Connection-Access Configuration  
 Depending on how different availability replicas are configured for connection access, support for client connections might change after an availability group fails over. For example, consider an availability group for which reporting is performed on remote asynchronous-commit secondary replicas. All of the read-only applications for the databases in this availability group set their `Application Intent` connection property to `ReadOnly`, so that all read-only connections are read-intent connections.  
  
 This example availability group possesses two synchronous-commit replicas at the main computing center and two asynchronous-commit replicas at a satellite site. For the primary role, all the replicas are configured for read-write access, which prevents read-intent connections to the primary replica in all situations. The synchronous commit secondary role uses the default connection-access configuration ("none"), which prevents all client connections under the secondary role.  In contrast, the asynchronous commit replicas are configured to permit read-intent connections under the secondary role. The following table summarize this example configuration:  
  
|Replica|Commit Mode|Initial Role|Connection Access for Secondary Role|Connection Access for Primary Role|  
|-------------|-----------------|------------------|------------------------------------------|----------------------------------------|  
|Replica1|Synchronous|Primary|None|Read-write|  
|Replica2|Synchronous|Secondary|None|Read-write|  
|Replica3|Asynchronous|Secondary|Read-intentonly|Read-write|  
|Replica4|Asynchronous|Secondary|Read-intent only|Read-write|  
  
 Typically, in this example scenario, failovers occur only between the synchronous-commit replicas, and  immediately after the failover, read-intent applications are able to reconnect to one of the asynchronous-commit secondary replicas. However, when a disaster occurs at the main computing center both synchronous-commit replicas are lost. The database administrator at the satellite site responds by performing a forced manual failover to an asynchronous-commit secondary replica. The secondary databases on the remaining secondary replica are suspended by the forced failover, making them unavailable for read-only workloads. The new primary replica, which is configured for read-write connections, prevents the read-intent workload from competing with the read-write workload. This means that until the database administrator resumes the secondary databases on the remaining asynchronous-commit secondary replica, read-intent clients cannot connect to any availability replica.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
-   [Monitor Availability Groups &#40;Transact-SQL&#41;](monitor-availability-groups-transact-sql.md)  
  
-   [View Availability Replica Properties &#40;SQL Server&#41;](view-availability-replica-properties-sql-server.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [Microsoft SQL Server AlwaysOn Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)  
  
-   [SQL Server AlwaysOn Team Blog: The official SQL Server AlwaysOn Team Blog](https://blogs.msdn.com/b/sqlalwayson/)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../listeners-client-connectivity-application-failover.md)   
 [Statistics](../../../relational-databases/statistics/statistics.md)  
  
  
