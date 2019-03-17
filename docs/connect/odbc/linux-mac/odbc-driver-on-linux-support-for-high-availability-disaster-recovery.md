---
title: "ODBC Driver on Linux and macOS - High Availability and Disaster Recovery | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: fa656c5b-a935-40bf-bc20-e517ca5cd0ba
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC Driver on Linux and macOS Support for High Availability and Disaster Recovery
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The ODBC drivers for Linux and macOS support [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)]. For more information about [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)], see:  
  
-   [Availability Group Listeners, Client Connectivity, and Application Failover (SQL Server)](https://msdn.microsoft.com/library/hh213417.aspx)  
  
-   [Creation and Configuration of Availability Groups (SQL Server)](https://msdn.microsoft.com/library/ff878265.aspx)  
  
-   [Failover Clustering and AlwaysOn Availability Groups (SQL Server)](https://msdn.microsoft.com/library/ff929171.aspx)  
  
-   [Active Secondaries: Readable Secondary Replicas (AlwaysOn Availability Groups)](https://msdn.microsoft.com/library/ff878253.aspx)  
  
You can specify the availability group listener of a given availability group in the connection string. If an ODBC application on Linux or macOS is connected to a database in an availability group that fails over, the original connection is broken and the application must open a new connection to continue work after the failover.

The ODBC drivers on Linux and macOS iterate sequentially through all IP addresses associated with a DNS hostname if you are not connecting to an availability group listener, and multiple IP addresses are associated with the hostname.

If the DNS server's first returned IP address is not connectable, these iterations can be time consuming. When connecting to an availability group listener, the driver attempts to establish connections to all IP addresses in parallel. If a connection attempt succeeds, the driver discards any pending connection attempts.

> [!NOTE]  
> Because a connection can fail due to an availability group failover, implement connection retry logic; retry a failed connection until it reconnects. Increasing connection timeout and implementing connection retry logic increases the chance of connecting to an availability group.

## Connecting With MultiSubnetFailover

Always specify **MultiSubnetFailover=Yes** when connecting to a [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] availability group listener or [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] Failover Cluster Instance. **MultiSubnetFailover** enables faster failover for all Availability Groups and failover cluster instance in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]. **MultiSubnetFailover** also significantly reduces failover time for single and multi-subnet AlwaysOn topologies. During a multisubnet failover, the client attempts connections in parallel. During a subnet failover, the driver aggressively retries the TCP connection.

The **MultiSubnetFailover** connection property indicates that the application is being deployed in an availability group or Failover Cluster Instance. The driver tries to connect to the database on the primary [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance by trying to connect to all the IP addresses. When connecting with **MultiSubnetFailover=Yes**, the client retries TCP connection attempts faster than the operating system's default TCP retransmit intervals. **MultiSubnetFailover=Yes** enables faster reconnection after failover of either an AlwaysOn Availability Group or an AlwaysOn Failover Cluster Instance. **MultiSubnetFailover=Yes** applies to both single- and multi-subnet Availability Groups and Failover Cluster Instances.  

Use **MultiSubnetFailover=Yes** when connecting to an availability group listener or Failover Cluster Instance. Otherwise, your application's performance can be negatively affected.

Note the following when connecting to a server in an availability group or Failover Cluster Instance:
  
-   Specify **MultiSubnetFailover=Yes** to improve performance when connecting to a single subnet or multi-subnet Availability Group.

-   Specify the availability group listener of the availability group as the server in your connection string.
  
-   You cannot connect to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance configured with more than 64 IP addresses.

-   Both [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication or Kerberos Authentication can be used with **MultiSubnetFailover=Yes** without affecting the behavior of the application.

-   You can increase the value of **loginTimeout** to accommodate for failover time and reduce the application's connection retry attempts.

-   Distributed transactions are not supported.  
  
If read-only routing is not in effect, connecting to a secondary replica location in an availability group fails in the following situations:  
  
1.  If the secondary replica location is not configured to accept connections.  
  
2.  If an application uses **ApplicationIntent=ReadWrite** and the secondary replica location is configured for read-only access.  
  
A connection fails if a primary replica is configured to reject read-only workloads and the connection string contains **ApplicationIntent=ReadOnly**.  


[!INCLUDE[specify-application-intent_read-only-routing](~/includes/paragraph-content/specify-application-intent-read-only-routing.md)]


## ODBC Syntax

Two ODBC connection string keywords support [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)]:  
  
-   **ApplicationIntent**  
  
-   **MultiSubnetFailover**  
  
For more information about ODBC connection string keywords, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
The equivalent connection attributes are:
  
-   **SQL_COPT_SS_APPLICATION_INTENT**  
  
-   **SQL_COPT_SS_MULTISUBNET_FAILOVER**  
  
For more information about ODBC connection attributes, see [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md).  
  
An ODBC application that uses [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)] can use one of two functions to make the connection:  
  
|Function|Description|  
|------------|---------------|  
|[SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md)|**SQLConnect** supports both **ApplicationIntent** and **MultiSubnetFailover** via a data source name (DSN) or connection attribute.|  
|[SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md)|**SQLDriverConnect** supports **ApplicationIntent** and **MultiSubnetFailover** via DSN, connection string keyword, or connection attribute.|
  
## See Also  

[Connection String Keywords and Data Source Names (DSNs)](../../../connect/odbc/linux-mac/connection-string-keywords-and-data-source-names-dsns.md)

[Programming Guidelines](../../../connect/odbc/linux-mac/programming-guidelines.md)

[Release Notes](../../../connect/odbc/linux-mac/release-notes.md)  
