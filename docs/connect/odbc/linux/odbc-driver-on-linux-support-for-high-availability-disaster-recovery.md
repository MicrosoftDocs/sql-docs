---
title: "ODBC Driver on Linux - High Availability, Disaster Recovery | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: fa656c5b-a935-40bf-bc20-e517ca5cd0ba
caps.latest.revision: 16
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# ODBC Driver on Linux Support for High Availability, Disaster Recovery
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The ODBC driver for Linux supports [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)]. For more information about [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)], see:  
  
-   [Availability Group Listeners, Client Connectivity, and Application Failover (SQL Server)](http://msdn.microsoft.com/library/hh213417.aspx)  
  
-   [Creation and Configuration of Availability Groups (SQL Server)](http://msdn.microsoft.com/library/ff878265.aspx)  
  
-   [Failover Clustering and AlwaysOn Availability Groups (SQL Server)](http://msdn.microsoft.com/library/ff929171.aspx)  
  
-   [Active Secondaries: Readable Secondary Replicas (AlwaysOn Availability Groups)](http://msdn.microsoft.com/library/ff878253.aspx)  
  
You can specify the availability group listener of a given availability group in the connection string. If an ODBC driver on Linux application is connected to a database in an availability group that fails over, the original connection is broken. Then the application must open a new connection to continue work after the failover.  
  
The ODBC driver on Linux iterates sequentially through all IP addresses associated with a DNS entry if:  
  
-   You are not connecting to an availability group listener. And,  
  
-   Multiple IP addresses are associated with a hostname.  
  
If the DNS server's first returned IP address by is not bound to any network interface card (NIC), these iterations can be time consuming. When connecting to an availability group listener, the ODBC driver on Linux attempts to establish connections to all IP addresses in parallel. If a connection attempt succeeds, the driver discards any pending connection attempts.  
  
> [!NOTE]  
> Increasing connection timeout and implementing connection retry logic increases the chance of connecting to an availability group. Also, because a connection can fail because of an availability group failover, implement connection retry logic; retry a failed connection until it reconnects.  
  
## Connecting With MultiSubnetFailover  
Always specify **MultiSubnetFailover=Yes** (or **=True**) when connecting to a [!INCLUDE[ssSQL11](../../../includes/sssql11_md.md)] availability group listener or [!INCLUDE[ssSQL11](../../../includes/sssql11_md.md)] Failover Cluster Instance. **MultiSubnetFailover** enables faster failover for all Availability Groups and failover cluster instance in [!INCLUDE[ssSQL11](../../../includes/sssql11_md.md)]. **MultiSubnetFailover** also significantly reduces failover time for single and multisubnet AlwaysOn topologies. During a multisubnet failover, the client attempts connections in parallel. During a subnet failover, the ODBC driver on Linux aggressively retries the TCP connection.  
  
The **MultiSubnetFailover** connection property indicates that the application is being deployed in an availability group or Failover Cluster Instance. The ODBC driver on Linux tries to connect to the database on the primary [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] instance by trying to connect to all the IP addresses. When connecting with **MultiSubnetFailover=Yes**, the client retries TCP connection attempts faster than the operating system’s default TCP retransmit intervals. **MultiSubnetFailover=Yes** enables faster reconnection after failover of either an AlwaysOn Availability Group or an AlwaysOn Failover Cluster Instance. **MultiSubnetFailover=Yes** applies to both single- and multisubnet Availability Groups and Failover Cluster Instances.  
  
Use **MultiSubnetFailover=Yes** when connecting to an availability group listener or Failover Cluster Instance. Otherwise, your application' performance can be negatively affected.  
  
To connect to a server in an availability group or Failover Cluster Instance:  
  
-   **MultiSubnetFailover** improves performance when connecting to a single subnet or multisubnet.  
  
-   To connect to an availability group, specify the availability group listener of the availability group as the server in your connection string.  
  
-   You cannot connect to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] instance configured with more than 64 IP addresses.  
  
-   Behavior of an application that uses the **MultiSubnetFailover** connection property is not affected based on the type of authentication: [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] Authentication or Kerberos Authentication.  
  
-   You can increase the value of **loginTimeout** to accommodate for failover time and reduce the application's connection retry attempts.  
  
-   Distributed transactions are not supported.  
  
If read-only routing is not in effect, connecting to a secondary replica location in an availability group fails in the following situations:  
  
1.  If the secondary replica location is not configured to accept connections.  
  
2.  If an application uses **ApplicationIntent=ReadWrite** and the secondary replica location is configured for read-only access.  
  
A connection fails if a primary replica is configured to reject read-only workloads and the connection string contains **ApplicationIntent=ReadOnly**.  
  
## Specifying Application Intent  
When **ApplicationIntent=ReadOnly**, the client requests a read workload when connecting to an AlwaysOn enabled database. The server enforces the intent at connection time and during a USE database statement but only to an Always On enabled database.  
  
The **ApplicationIntent** keyword does not work with legacy, read-only databases.  
  
A database can allow or disallow read workloads on the targeted AlwaysOn database. (Use the **ALLOW_CONNECTIONS** clause of the **PRIMARY_ROLE** and **SECONDARY_ROLE**[!INCLUDE[tsql](../../../includes/tsql_md.md)] statements.)  
  
The **ApplicationIntent** keyword is used to enable read-only routing.  
  
## Read-Only Routing  
Read-only routing is a feature that can ensure the availability of a read-only replica of a database. To enable read-only routing:  
  
1.  Connect to an Always On Availability Group availability group listener.  
  
2.  The **ApplicationIntent** connection string keyword must be set to **ReadOnly**.  
  
3.  The database administrator must configure the Availability Group to enable read-only routing.  
  
It is possible for multiple connections that use read-only routing to connect to different read-only replicas. Changes in database synchronization or changes in the server's routing configuration can result in client connections to different read-only replicas. To ensure that all read-only requests connect to the same read-only replica, do not pass an availability group listener to the **Server** connection keyword. Instead, specify the name of the read-only instance.  
  
Expect longer connection times with read-only routing than connecting to the primary. Therefore, increase your login timeout. Read-only routing first connects to the primary and then looks for the best available readable secondary.  
  
## ODBC Syntax  
Two ODBC connection string keywords support [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)]:  
  
-   **ApplicationIntent**  
  
-   **MultiSubnetFailover**  
  
For more information about ODBC connection string keywords, see [Using Connection String Keywords with SQL Server Native Client](http://msdn.microsoft.com/library/ms130822.aspx).  
  
The equivalent connection properties are:  
  
-   **SQL_COPT_SS_APPLICATION_INTENT**  
  
-   **SQL_COPT_SS_MULTISUBNET_FAILOVER**  
  
For more information about ODBC connection properties, see [SQLSetConnectAttr](http://msdn.microsoft.com/library/ms131709.aspx).  
  
An ODBC driver on Linux application that uses [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)] can use one of two functions to make the connection:  
  
|Function|Description|  
|------------|---------------|  
|[SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md)|**SQLConnect** supports both **ApplicationIntent** and **MultiSubnetFailover** via a data source name (DSN) or connection properties.|  
|[SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md)|**SQLDriverConnect** supports **ApplicationIntent** and **MultiSubnetFailover** via connection string keywords, connection properties, or DSN.|  
  
## See Also  
[Microsoft ODBC Driver for SQL Server on Linux](../../../connect/odbc/linux/microsoft-odbc-driver-for-sql-server-on-linux.md)  
  
