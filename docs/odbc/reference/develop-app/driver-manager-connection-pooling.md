---
title: "Driver Manager Connection Pooling | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connection pooling [ODBC]"
  - "pooled connections [ODBC]"
  - "connecting to driver [ODBC], connection pooling"
  - "connecting to data source [ODBC], connection pooling"
ms.assetid: ee95ffdb-5aa1-49a3-beb2-7695b27c3df9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Driver Manager Connection Pooling
Connection pooling enables an application to use a connection from a pool of connections that do not need to be re-established for each use. Once a connection has been created and placed in a pool, an application can reuse that connection without performing the complete connection process.  
  
 Using a pooled connection can result in significant performance gains, because applications can save the overhead involved in making a connection. This can be particularly significant for middle-tier applications that connect over a network or for applications that repeatedly connect and disconnect, such as Internet applications.  
  
 In addition to performance gains, the connection pooling architecture enables an environment and its associated connections to be used by multiple components in a single process. This means that stand-alone components in the same process can interact with each other without being aware of each other. A connection in a connection pool can be used repeatedly by multiple components.  
  
> [!NOTE]
>  Connection pooling can be used by an ODBC application exhibiting ODBC 2.*x* behavior, as long as the application can call *SQLSetEnvAttr*. When using connection pooling, the application must not execute SQL statements that change the database or the context of the database, such as changing the \<*database**name*>, which changes the catalog used by a data source.  


 An ODBC driver must be fully thread-safe, and connections must not have thread affinity to support connection pooling. This means the driver is able to handle a call on any thread at any time and is able to connect on one thread, to use the connection on another thread, and to disconnect on a third thread.  
  
 The connection pool is maintained by the Driver Manager. Connections are drawn from the pool when the application calls **SQLConnect** or **SQLDriverConnect** and are returned to the pool when the application calls **SQLDisconnect**. The size of the pool grows dynamically, based on the requested resource allocations. It shrinks based on the inactivity timeout: If a connection is inactive for a period of time (it has not been used in a connection), it is removed from the pool. The size of the pool is limited only by memory constraints and limits on the server.  
  
 The Driver Manager determines whether a specific connection in a pool should be used according to the arguments passed in **SQLConnect** or **SQLDriverConnect**, and according to the connection attributes set after the connection was allocated.  
  
 When the Driver Manager is pooling connections, it needs to be able to determine if a connection is still working before handing out the connection. Otherwise, the Driver Manager keeps on handing out the dead connection to the application whenever a transient network failure occurs. A new connection attribute has been defined in ODBC 3*.x*: SQL_ATTR_CONNECTION_DEAD. This is a read-only connection attribute that returns either SQL_CD_TRUE or SQL_CD_FALSE. The value SQL_CD_TRUE means that the connection has been lost, while the value SQL_CD_FALSE means that the connection is still active. (Drivers conforming to earlier versions of ODBC can also support this attribute.)  
  
 A driver must implement this option efficiently or it will impair the connection pooling performance. Specifically, a call to get this connection attribute should not cause a round trip to the server. Instead, a driver should just return the last known state of the connection. The connection is dead if the last trip to the server failed, and not dead if the last trip succeeded.  
  
## Remarks  
 If a connection has been lost (reported via SQL_ATTR_CONNECTION_DEAD), the ODBC Driver Manager will destroy that connection by calling SQLDisconnect in the driver. New connection requests might not find a usable connection in the pool. Eventually the Driver Manager might make a new connection, assuming the pool is empty.  
  
 To use a connection pool, an application performs the following steps:  
  
1.  Enables connection pooling by calling **SQLSetEnvAttr** to set the SQL_ATTR_CONNECTION_POOLING environment attribute to SQL_CP_ONE_PER_DRIVER or SQL_CP_ONE_PER_HENV. This call must be made before the application allocates the shared environment for which connection pooling is to be enabled. The environment handle in the call to **SQLSetEnvAttr** should be set to null, which makes SQL_ATTR_CONNECTION_POOLING a process-level attribute. If the attribute is set to SQL_CP_ONE_PER_DRIVER, a single connection pool is supported for each driver. If an application works with many drivers and few environments, this might be more efficient because fewer comparisons may be required. If set to SQL_CP_ONE_PER_HENV, a single connection pool is supported for each environment. If an application works with many environments and few drivers, this might be more efficient because fewer comparisons may be required. Connection pooling is disabled by setting SQL_ATTR_CONNECTION_POOLING to SQL_CP_OFF.  
  
2.  Allocates an environment by calling **SQLAllocHandle** with the *HandleType* argument set to SQL_HANDLE_ENV. The environment allocated by this call will be an implicit shared environment because connection pooling has been enabled. The environment to be used is not determined, however, until **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_DBC is called on this environment.  
  
3.  Allocates a connection by calling **SQLAllocHandle** with *InputHandle* set to SQL_HANDLE_DBC, and the *InputHandle* set to the environment handle allocated for connection pooling. The Driver Manager attempts to find an existing environment that matches the environment attributes set by the application. If no such environment exists, one is created, with a reference count (maintained by the Driver Manager) of 1. If a matching shared environment is found, the environment is returned to the application and its reference count is incremented. (The actual connection to be used is not determined by the Driver Manager until **SQLConnect** or **SQLDriverConnect** is called.)  
  
4.  Calls **SQLConnect** or **SQLDriverConnect** to make the connection. The Driver Manager uses the connection options in the call to **SQLConnect** (or the connection keywords in the call to **SQLDriverConnect**) and the connection attributes set after connection allocation to determine which connection in the pool should be used.  
  
    > [!NOTE]  
    >  How a requested connection is matched to a pooled connection is determined by the SQL_ATTR_CP_MATCH environment attribute. For more information, see [SQLSetEnvAttr](../../../odbc/reference/syntax/sqlsetenvattr-function.md).  
  
     ODBC applications using connection pooling should call [CoInitializeEx](https://go.microsoft.com/fwlink/?LinkID=116307) during application initialization and [CoUninitialize](https://go.microsoft.com/fwlink/?LinkId=116310) when the application closes.  
  
5.  Calls **SQLDisconnect** when done with the connection. The connection is returned to the connection pool and becomes available for reuse.  
  
 For an in-depth discussion, see [Pooling in the Microsoft Data Access Components](https://go.microsoft.com/fwlink/?LinkId=120776).  
  
## Connection Pooling Considerations  
 Performing any of the following actions using a SQL command (rather than through the ODBC API) can affect the connection's state and cause unexpected problems when connection pooling is active:  
  
-   Opening a connection and changing the default database.  
  
-   Using the SET statement to change any configurable options (including SET ROWCOUNT, ANSI_NULL, IMPLICIT_TRANSACTIONS, SHOWPLAN, STATISTICS, TEXTSIZE, and DATEFORMAT).  
  
-   Creating temporary tables and stored procedures.  
  
 If any of these actions are performed outside of the ODBC API, the next person who uses the connection will automatically inherit the previous settings, tables, or procedures.  
  
> [!NOTE]  
>  Do not expect certain settings to be present in the connection state. You should always set the connection state in your application and ensure that the application removes any unused connection pooling settings.  
  
## Driver-Aware Connection Pooling  
 Beginning in Windows 8, an ODBC driver can use connections in the pool more efficiently. For more information, see [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md).  
  
## See Also  
 [Connecting to a Data Source or Driver](../../../odbc/reference/develop-app/connecting-to-a-data-source-or-driver.md)   
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)   
 [Pooling in the Microsoft Data Access Components](https://go.microsoft.com/fwlink/?LinkId=120776)
