---
title: "High Availability, Recovery"
description: Learn about SQL Server Native Client support for Always On availability groups, which was added in SQL Server 2012.
ms.custom: ""
ms.date: "04/04/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
ms.assetid: 2b06186b-4090-4728-b96b-90d6ebd9f66f
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Native Client Support for High Availability, Disaster Recovery
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  This topic discusses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client support (added in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]) for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. For more information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md), [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md), [Failover Clustering and Always On Availability Groups &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md), and [Active Secondaries: Readable Secondary Replicas &#40;Always On Availability Groups&#41;](~/database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).  
  
 You can specify the availability group listener of a given availability group in the connection string. If a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client application is connected to a database in an availability group that fails over, the original connection is broken, and the application must open a new connection to continue work after the failover.  
  
 If you are not connecting to an availability group listener, and if multiple IP addresses are associated with a hostname, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client will iterate sequentially through all IP addresses associated with DNS entry. This can be time consuming if the first IP address returned by DNS server is not bound to any network interface card (NIC). When connecting to an availability group listener, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client attempts to establish connections to all IP addresses in parallel and if a connection attempt succeeds, the driver will discard any pending connection attempts.  
  
> [!NOTE]  
>  Increasing connection timeout and implementing connection retry logic will increase the probability that an application will connect to an availability group. Also, because a connection can fail because of an availability group failover, you should implement connection retry logic, retrying a failed connection until it reconnects.  
  
## Connecting With MultiSubnetFailover  
 Always specify **MultiSubnetFailover=Yes** when connecting to a SQL Server 2012 availability group listener or SQL Server 2012 Failover Cluster Instance. **MultiSubnetFailover** enables faster failover for all Availability Groups and failover cluster instance in SQL Server 2012 and will significantly reduce failover time for single and multi-subnet Always On topologies. During a multi-subnet failover, the client will attempt connections in parallel. During a subnet failover, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client will aggressively retry the TCP connection.  
  
 The **MultiSubnetFailover** connection property indicates that the application is being deployed in an availability group or Failover Cluster Instance, and that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client will try to connect to the database on the primary [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance by trying to connect to all the IP addresses. When **MultiSubnetFailover=Yes** is specified for a connection, the client retries TCP connection attempts faster than the operating system's default TCP retransmit intervals. This enables faster reconnection after failover of either an Always On Availability Group or an Always On Failover Cluster Instance, and is applicable to both single- and multi-subnet Availability Groups and Failover Cluster Instances.  
  
 For more information about connection string keywords, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 Specifying **MultiSubnetFailover=Yes** when connecting to something other than an availability group listener or Failover Cluster Instance may result in a negative performance impact, and is not supported.  
  
 Use the following guidelines to connect to a server in an availability group or Failover Cluster Instance:  
  
-   Use the **MultiSubnetFailover** connection property when connecting to a single subnet or multi-subnet; it will improve performance for both.  
  
-   To connect to an availability group, specify the availability group listener of the availability group as the server in your connection string.  
  
-   Connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance configured with more than 64 IP addresses will cause a connection failure.  
  
-   Behavior of an application that uses the **MultiSubnetFailover** connection property is not affected based on the type of authentication: [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication, Kerberos Authentication, or Windows Authentication.  
  
-   You can increase the value of **loginTimeout** to accommodate for failover time and reduce application connection retry attempts.  
  
-   Distributed transactions are not supported.  
  
 If read-only routing is not in effect, connecting to a secondary replica location in an availability group will fail in the following situations:  
  
1.  If the secondary replica location is not configured to accept connections.  
  
2.  If an application uses **ApplicationIntent=ReadWrite** (discussed below) and the secondary replica location is configured for read-only access.  
  
 A connection will fail if a primary replica is configured to reject read-only workloads and the connection string contains **ApplicationIntent=ReadOnly**.  
  
## Upgrading to Use Multi-Subnet Clusters from Database Mirroring  
 A connection error will occur if the **MultiSubnetFailover** and **Failover_Partner** connection keywords are present in the connection string. An error will also occur if **MultiSubnetFailover** is used and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] returns a failover partner response indicating it is part of a database mirroring pair.  
  
 If you upgrade a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client application that currently uses database mirroring to a multi-subnet scenario, you should remove the **Failover_Partner** connection property and replace it with **MultiSubnetFailover** set to **Yes** and replace the server name in the connection string with an availability group listener. If a connection string uses **Failover_Partner** and **MultiSubnetFailover=Yes**, the driver will generate an error. However, if a connection string uses **Failover_Partner** and **MultiSubnetFailover=No** (or **ApplicationIntent=ReadWrite**), the application will use database mirroring.  
  
 The driver will return an error if database mirroring is used on the primary database in the availability group, and if **MultiSubnetFailover=Yes** is used in the connection string that connects to a primary database instead of to an availability group listener.  


[!INCLUDE[specify-application-intent_read-only-routing](~/includes/paragraph-content/specify-application-intent-read-only-routing.md)]


## ODBC  
 Two ODBC connection string keywords were added to support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client:  
  
-   **ApplicationIntent**  
  
-   **MultiSubnetFailover**  
  
 For more information about ODBC connection string keywords in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 The equivalent connection properties are:  
  
-   **SQL_COPT_SS_APPLICATION_INTENT**  
  
-   **SQL_COPT_SS_MULTISUBNET_FAILOVER**  
  
 For more information about ODBC connection properties in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, see [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md).  
  
 The functionality of the **ApplicationIntent** and **MultiSubnetFailover** keywords will be exposed in the ODBC Data Source Administrator for DSNs that use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client driver, beginning in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)].  
  
 A [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC application can use one of three functions to make the connection:  
  
|Function|Description|  
|--------------|-----------------|  
|[SQLBrowseConnect](../../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md)|The list of servers returned by **SQLBrowseConnect** will not include VNNs. You will only see a list of servers without any indication if the server is a standalone server, or a primary or secondary server in a Windows Server Failover Clustering (WSFC) cluster that contains two or more [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instances that have been enabled for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. If you connect to a server and get a failure, it may be because you have connected to a server, and the **ApplicationIntent** setting is not compatible with the server configuration.<br /><br /> Because **SQLBrowseConnect** does not recognize servers in a Windows Server Failover Clustering (WSFC) cluster that contains two or more [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instances that have been enabled for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], **SQLBrowseConnect** ignores the **MultiSubnetFailover** connection string keyword.|  
|[SQLConnect](../../../relational-databases/native-client-odbc-api/sqlconnect.md)|**SQLConnect** supports both **ApplicationIntent** and **MultiSubnetFailover** via a data source name (DSN) or connection properties.|  
|[SQLDriverConnect](../../../relational-databases/native-client-odbc-api/sqldriverconnect.md)|**SQLDriverConnect** supports **ApplicationIntent** and **MultiSubnetFailover** via connection string keywords, connection properties, or DSN.|  
  
## OLE DB  
 OLE DB in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client does not support the **MultiSubnetFailover** keyword.  
  
 OLE DB in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client will support application intent. Application intent will behave the same for OLE DB applications as ODBC applications (see above).  
  
 One OLE DB connection string keyword added to support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client:  
  
-   **Application Intent**  
  
 For more information about connection string keywords in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 The equivalent connection properties are:  
  
-   **SSPROP_INIT_APPLICATIONINTENT**  
  
-   **DBPROP_INIT_PROVIDERSTRING**  
  
 A [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB application can use one of the methods to specify application intent:  
  
 **IDBInitialize::Initialize**  
 **IDBInitialize::Initialize** uses the previously configured set of properties to initialize the data source and create the data source object. Specify application intent as a provider property or as part of the extended properties string.  
  
 **IDataInitialize::GetDataSource**  
 **IDataInitialize::GetDataSource** takes an input connection string that can contain the **Application Intent** keyword.  
  
 **IDBProperties::GetProperties**  
 **IDBProperties::GetProperties** retrieves the value of the property that is currently set on the data source.  You can retrieve the **Application Intent** value through the DBPROP_INIT_PROVIDERSTRING property and SSPROP_INIT_APPLICATIONINTENT property.  
  
 **IDBProperties::SetProperties**  
 To set the **ApplicationIntent** property value, call **IDBProperties::SetProperties** passing in the **SSPROP_INIT_APPLICATIONINTENT** property with value "**ReadWrite**" or "**ReadOnly**" or **DBPROP_INIT_PROVIDERSTRING** property with value containing "**ApplicationIntent=ReadOnly**" or "**ApplicationIntent=ReadWrite**".  
  
 You can specify application intent in the Application Intent Properties field of the All tab in the **Data Link Properties** dialog box.  
  
 When implicit connections are established, the implicit connection will use the application intent setting of the parent connection. Similarly, multiple sessions created from the same data source will inherit the data source's application intent setting.  
  
## See Also  
 [SQL Server Native Client Features](../../../relational-databases/native-client/features/sql-server-native-client-features.md)   
 [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md)  
  
  
