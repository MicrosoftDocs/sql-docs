---
title: "Connect to an availability group listener"
description: "Contains information about connecting to an Always On availability group listener, before and after failover."
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], listeners"
  - "read-only routing"
  - "read-intent connections [AlwaysOn Availability Groups]"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], read-only routing"
  - "Availability Groups [SQL Server], client connectivity"
ms.assetid: 76fb3eca-6b08-4610-8d79-64019dd56c44
author: MashaMSFT
ms.author: mathoma
---
# Connect to an Always On availability group listener 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
This article contains information about the listener used to connect to an Always On availability group. 
  
For the majority of common listener configurations, you can create the first availability group listener using [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements or PowerShell cmdlets. For more information, see [Related Tasks](#RelatedTasks), later in this topic.  
  
  
##  <a name="AGlisteners"></a> Overview

An availability group listener is a virtual network name (VNN) that clients can connect  to in order to access a database in a primary or secondary replica of an Always On availability group. A listener allows a client to connect to a replica without having to know the physical instance name of the SQL Server. Since the listener routes traffic, the client connection string does not need to be modified after a failover occurs. 

An availability group listener consists of a Domain Name System (DNS) listener name, listener port designation, and one or more IP addresses. Only the TCP protocol is supported by availability group listener.  The DNS name of the listener must be unique in the domain and in NetBIOS.  When you create a  listener, it becomes a resource in a cluster with an associated virtual network name (VNN), virtual IP (VIP), and availability group dependency. A client uses DNS to resolve the VNN into multiple IP addresses and then tries to connect to each address, until a connection request succeeds or until the connection requests time out.  
  
If read-only routing is configured for one or more [readable secondary replicas](../../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md), read-intent client connections to the primary replica are redirected to a readable secondary replica. 
  
For essential information about availability group listeners, see [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).  
  
  
##  <a name="AGlConfig"></a> Listener parameters  

 An availability group listener uses the following:
  
 **A unique DNS name**  
 This is also known as a Virtual Network Name (VNN). Active Directory naming rules for DNS host names apply. For more information, see the [Naming conventions in Active Directory for computers, domains, sites, and OUs](https://support.microsoft.com/kb/909264) KB article.  
  
**One or more Virtual IP addresses (VIPs)**  
 VIPs are configured for one or more subnets to which the availability group can fail over.  
  
**IP address configuration**  
 For a given availability group listener, the IP address can use either Dynamic Host Configuration Protocol (DHCP), or one or more static IP address. Using DHCP can cause connectivity delays during failover, and so it is not recommended for use in production environments. Availability groups that extend across multiple subnets, or use hybrid network configurations, must use a static IP address. 
 
  
##  <a name="SelectListenerPort"></a> Listener port 
 When configuring an availability group listener, you must designate a port.  You can configure the default port to 1433 in order to allow for simplicity of the client connection strings. If using 1433, you do not need to designate a port number in a connection string.   Also, since each availability group listener will have a separate virtual network name, each availability group listener configured on a single WSFC can be configured to reference the same default port of 1433.  
  
 You can also designate a non-standard listener port; however this means that you will also need to explicitly specify a target port in your connection string whenever connecting to the availability group listener.  You will also need to open permission on the firewall for the non-standard port.  
  
 If you use the default port of 1433 for availability group listener VNNs, you will still need to ensure that no other services on the cluster node are using this port; otherwise this would cause a port conflict.  
  
 If one of the instances of SQL Server is already listening on TCP port 1433 via the instance listener and there are no other services (including additional instances of SQL Server) on the computer listening on port 1433, this will not cause a port conflict with the availability group listener.  This is because the availability group listener can share the same TCP port inside the same service process.  However multiple instances of SQL Server (side-by-side) should not be configured to listen on the same port.  
  
##  <a name="ConnectToPrimary"></a> Connect to the primary replica  

Specify the availability group listener DNS name in the connection string to connect to the primary replica for read-write access. During a failover, when the primary replica changes, existing connections to the listener are disconnected and new connections are routed to the new primary replica.  


An example of a basic connection string for the ADO.NET provider (System.Data.SqlClient): 
  
```  
Server=tcp: AGListener,1433;Database=MyDB;Integrated Security=SSPI  
```  

You can still connect directly to the instance of SQL Server using the instance name of the primary or secondary replica instead of using the availability group listener. However, you will then lose the benefit of new connections being routed automatically to the new current primary replica. Additionally, you will lose the benefit of read-only routing, where connections specified with `read-intent` are automatically routed to the readable secondary replica. 

##  <a name="ReadOnlyAppIntent"></a> Read-only routing  
 The application intent connection string property expresses the client application's request to be directed either to a read-write or read-only version of an availability group database. To use read-only routing, a client must use an application intent of read-only in the connection string when connecting to the availability group listener. Without the read-only application intent, connections to the availability group listener are directed to the database on the primary replica.  
  
 The application intent attribute is stored in the client's session during login and the instance of SQL Server will then process this intent and determine what to do according to the configuration of the availability group and the current read-write state of the target database in the secondary replica.  
  
 An example of a connection string for the ADO.NET provider (System.Data.SqlClient) that designates read-only application intent: 

```  
Server=tcp:AGListener,1443;Database=AdventureWorks;Integrated Security=SSPI;ApplicationIntent=ReadOnly  
```  
  
In this connection string example, the client is attempting to connect to the AdventureWorks database via an availability group listener named `AGListener` on port 1443 (you may omit the port if the listener is listening on 1433).  The connection string has the **ApplicationIntent** property set to **ReadOnly**, making this a *read-intent connection string*.  Without this setting, the server would not have attempted a read-only routing of the connection.  
  
The primary database of the availability group processes the incoming read-only routing request and attempts to locate an online, read-only replica that is joined to the primary replica and is configured for read-only routing.  The client receives back connection information from the primary replica server and connects to the identified read-only replica.  
  
The application intent can be sent from a client driver to a down-level instance of SQL Server.  In this case, application intent of read-only is ignored and the connection proceeds as normal.  
  
You can bypass read-only routing by not setting the application intent connection property to **ReadOnly** (when not designated, the default is **ReadWrite** during login) or by connecting directly to the primary replica instance of SQL Server instead of using the availability group listener name.  Read-only routing will also not occur if you connect directly to a read-only replica.  

    
##  <a name="ConfigureARsForROR"></a> Configure read-only routing  
 A database administrator must configure the availability replicas as follows:  
  
1. For each replica you want to configure as a readable secondary replica, set the settings for the secondary role: 

    -   Connection access must be set to "all" or "read only".  
  
    -   The read-only routing URL must be specified.  
  
2.  For each of these replicas, a read-only routing list must be specified for the primary role. Specify one or more server names as routing targets.  

##  <a name="ConnectToSecondary"></a> Connect to a read-only replica 

_Read-only routing_ refers to automatically routing incoming listener connections to a readable secondary replica that is configured to allow read-only workloads. 

Connections are automatically routed to the read-only replica if the following are true: 

 
-   At least one secondary replica is set to read-only access, and each read-only secondary replica and the primary replica are configured to support read-only routing. For more information, see [To Configure Availability Replicas for Read-Only Routing](#ConfigureARsForROR), later in this section.  

-   The connection string references a database involved in the Availability Group. An alternative to this would be the login used in the connection has the database configured as its default database. For more information, see [this article on how the algorithm works with read-only routing](https://blogs.msdn.microsoft.com/mattn/2012/04/25/calculating-read_only_routing_url-for-alwayson/).

-   The connection string references an availability group listener, and the application intent of the incoming connection is set to read-only (for example, by using the **Application Intent=ReadOnly** keyword in the ODBC or OLEDB connection strings or connection attributes or properties). 
  
####  <a name="RelatedTasksROR"></a> Related Tasks  
  
-   [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [SQL Server Native Client Support for High Availability, Disaster Recovery](../../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md)  
  
-   [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md)  
  
##  <a name="BypassAGl"></a> Bypass listeners  
 While availability group listeners enable support for failover redirection and read-only routing, client connections are not required to use them. A client connection can also directly reference the instance of SQL Server instead of connecting to the availability group listener.  
  
 To the instance of SQL Server, it is irrelevant whether a connection logs in using the availability group listener or using another instance endpoint.  The instance of SQL Server will verify the state of the targeted database and either allow or disallow connectivity based on the configuration of the availability group and the current state of the database on the instance.  For example, if a client application connects directly to an instance of SQL Server port and connects to a target database hosted in an availability group, and the target database is in primary state and online, then connectivity will succeed.  If the target database is offline or in a transitional state, connectivity to the database will fail.  
  
 Alternatively, while migrating from database mirroring to [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], applications can specify the database mirroring connection string as long as only one secondary replica exists and it disallows user connections. For more information, see [Using Database-Mirroring Connection Strings with Availability Groups](#DbmConnectionString), later in this section.  
  
##  <a name="DbmConnectionString"></a> Using database mirroring connection strings 
 If an availability group possesses only one secondary replica and is configured with either ALLOW_CONNECTIONS = READ_ONLY or ALLOW_CONNECTIONS = NONE for the secondary replica, clients can connect to the primary replica by using a database mirroring connection string. This approach can be useful while migrating an existing application from database mirroring to an availability group, as long as you limit the availability group to two availability replicas (a primary replica and one secondary replica). If you add additional secondary replicas, you will need to create an availability group listener for the availability group and update your applications to use the availability group listener DNS name.  
  
 When using database mirroring connection strings, the client can use either [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client or .NET Framework Data Provider for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The connection string provided by a client must minimally supply the name of one server instance, the *initial partner name*, to identify the server instance that initially hosts the availability replica to which you intend to connect. Optionally, the connection string can also supply the name of another server instance, the *failover partner name*, to identify the server instance that initially hosts the secondary replica as the failover partner name.  
  
 For more information about database mirroring connection strings, see [Connect Clients to a Database Mirroring Session &#40;SQL Server&#41;](../../../database-engine/database-mirroring/connect-clients-to-a-database-mirroring-session-sql-server.md).  
  
##  <a name="CCBehaviorOnFailover"></a> Behavior of client connections on failover  

 When an availability group failover occurs, existing persistent connections to the availability group are terminated and the client must establish a new connection in order to continue working with the same primary database or read-only secondary database.  While a failover is occurring on the server side, connectivity to the availability group may fail, forcing the client application to retry connecting until the primary is brought fully back online.  
  
 If the availability group comes back online during a client application's connection attempt but before the connect timeout period, the client driver may successfully connect during one of its internal retry attempts and no error will be surfaced to the application in this case.  
  
##  <a name="SupportAgMultiSubnetFailover"></a> Supporting multi-subnet failovers  
 
 If you are using client libraries that support the MultiSubnetFailover connection option in the connection string, you can optimize availability group failover to a different subnet by setting MultiSubnetFailover to "True" or "Yes", depending on the syntax of the provider you are using.  
  
> [!NOTE]  
>  We recommend this setting for both single and multi-subnet connections to availability groups listeners and to SQL Server Failover Cluster Instance names.  Enabling this option adds additional optimizations, even for single-subnet scenarios.  
  
 The **MultiSubnetFailover** connection option only works with the TCP network protocol and is only supported when connecting to an availability group listener and for any virtual network name connecting to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
 An example of the ADO.NET provider (System.Data.SqlClient) connection string that enables multi-subnet failover is as follows:  
  
```  
Server=tcp:AGListener,1433;Database=AdventureWorks;Integrated Security=SSPI; MultiSubnetFailover=True  
```  
  
 The **MultiSubnetFailover** connection option should be set to **True** even if the availability group only spans a single subnet.  This allows you to preconfigure new clients to support future spanning of subnets without any need for future client connection string changes and also optimizes failover performance for single subnet failovers.  While the **MultiSubnetFailover** connection option is not required, it does provide the benefit of a faster subnet failover.  This is because the client driver will attempt to open up a TCP socket for each IP address in parallel associated with the availability group.  The client driver will wait for the first IP to respond with success and once it does, will then use it for the connection.  
  
##  <a name="SSLcertificates"></a>  Listeners & SSL Certificates  

 When connecting to an availability group listener, if the participating instances of SQL Server use SSL certificates in conjunction with session encryption, the connecting client driver will need to support the Subject Alternate Name in the SSL certificate in order to force encryption.  SQL Server driver support for certificate Subject Alternative Name is planned for ADO.NET (SqlClient), Microsoft JDBC, and SQL Native Client (SNAC).  
  
 An X.509 certificate must be configured for each participating server node in the failover cluster with a list of all availability group listeners set in the Subject Alternate Name of the certificate.  
  
 For example, if the WSFC has three availability group listeners with the names `AG1_listener.Adventure-Works.com`, `AG2_listener.Adventure-Works.com`, and `AG3_listener.Adventure-Works.com`, the Subject Alternative Name for the certificate should be set as follows:  
  
```  
CN = ServerFQDN  
SAN = ServerFQDN,AG1_listener.Adventure-Works.com, AG2_listener.Adventure-Works.com, AG3_listener.Adventure-Works.com  
```  
  
##  <a name="SPNs"></a> Listeners & Kerberos (SPNs) 

A domain administrator must configure a Service Principal Name (SPN) in Active Directory for each availability group listener to enable Kerberos for client connections to the listener. When registering the SPN, you must use the service account of the server instance that hosts the availability replica .  For the SPN to work across all replicas, the same service account must be used for all instances in the WSFC cluster that hosts the availability group.  
  
 Use the **setspn** Windows command line tool to configure the SPN.  For example to configure an SPN for an availability group named `AG1listener.Adventure-Works.com` hosted on a set of instances of SQL Server all configured to run under the domain account `corp/svclogin2`:  
  
```  
setspn -A MSSQLSvc/AG1listener.Adventure-Works.com:1433 corp/svclogin2  
```  
  
 For more information about manual registration of a SPN for SQL Server, see [Register a Service Principal Name for Kerberos Connections](../../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Always On Client Connectivity &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-client-connectivity-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
-   [View Availability Group Listener Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-group-listener-properties-sql-server.md)  
  
-   [Remove an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-an-availability-group-listener-sql-server.md)  
  
-   [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)  
  
-   [Introduction to the Availability Group Listener](https://blogs.msdn.microsoft.com/sqlalwayson/2012/01/16/introduction-to-the-availability-group-listener/) (a SQL Server Always On team blog)  
  
-   [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](https://blogs.msdn.microsoft.com/sqlalwayson/)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Always On Client Connectivity &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-client-connectivity-sql-server.md)   
 [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/about-client-connection-access-to-availability-replicas-sql-server.md)   
 [Active Secondaries: Readable Secondary Replicas &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)   
 [Connect Clients to a Database Mirroring Session &#40;SQL Server&#41;](../../../database-engine/database-mirroring/connect-clients-to-a-database-mirroring-session-sql-server.md)  
  
  
