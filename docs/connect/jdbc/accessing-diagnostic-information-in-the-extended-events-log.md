---
title: "Accessing Diagnostic Information in the Extended Events Log | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: a79e9468-2257-4536-91f1-73b008c376c3
caps.latest.revision: 16
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Accessing Diagnostic Information in the Extended Events Log
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  In the [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)], tracing ([Tracing Driver Operation](../../connect/jdbc/tracing-driver-operation.md)) has been updated to make it easier to easier to correlate client events with diagnostic information, such as connection failures, from the server's connectivity ring buffer and application performance information in the extended events log. For information about reading the extended events log, see [View Event Session Data](http://msdn.microsoft.com/library/hh710068(SQL.110).aspx).  
  
## Details  
 For connection operations, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] will send a client connection ID. If the connection fails, you can access the connectivity ring buffer ([Connectivity troubleshooting in SQL Server 2008 with the Connectivity Ring Buffer](http://go.microsoft.com/fwlink/?LinkId=207752)) and find the **ClientConnectionID** field and get diagnostic information about the connection failure. Client connection IDs are logged in the ring buffer only if an error occurs. (If a connection fails before sending the prelogin packet, a client connection ID will not be generated.) The client connection ID is a 16-byte GUID. You can also find the client connection ID in the extended events target output, if the **client_connection_id** action is added to events in an extended events session. You can enable tracing and rerun the connection command and observe the **ClientConnectionID** field in the trace, if you need further client driver diagnostic assistance.  
  
 You can get the client connection ID programmatically by using [ISQLServerConnection Interface](../../connect/jdbc/reference/isqlserverconnection-interface.md). The connection ID will also be present in any connection-related exceptions.  
  
 When there is a connection error, the client connection ID in the server's BID trace information and in the connectivity ring buffer can help correlate the client connections to connections on the server. For more information about BID traces on the server, see [Data Access Tracing](http://go.microsoft.com/fwlink/?LinkId=125805). Note, the data access tracing article also contains information about performing a data access trace, which does not apply to the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)]; see [Tracing Driver Operation](../../connect/jdbc/tracing-driver-operation.md) for information on doing a data access trace using the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)].  
  
 The JDBC Driver also sends a thread-specific activity ID. The activity ID is captured in the extended events sessions if the sessions are started with the TRACK_CAUSAILITY option enabled. For performance issues with an active connection, you can get the activity ID from the client's trace (ActivityID field) and then locate the activity ID in the extended events output. The activity ID in extended events is a 16-byte GUID (not the same as the GUID for the client connection ID) appended with a four-byte sequence number. The sequence number represents the order of a request within a thread. The ActivityId is sent for SQL batch statements and RPC requests. To enable sending ActivityId to the server, you first need to specify the following key-value pair in the Logging.Properties file:  
  
```  
com.microsoft.sqlserver.jdbc.traceactivity = on  
```  
  
 Any value other than `on` (case sensitive) will disable sending the ActivityId.  
  
 For more information, see [Tracing Driver Operation](../../connect/jdbc/tracing-driver-operation.md). This trace flag is used with corresponding JDBC object loggers to decide whether to trace and send the ActivityId in the JDBC driver. In addition to updating the Logging.Properties file, the logger com.microsoft.sqlserver.jdbc needs to be enabled at FINER or higher. If you want to send ActivityId to the server for requests made by a particular class, the corresponding class logger needs to be enabled at FINER or FINEST. For example, if the class is, SQLServerStatement, enable the logger com.microsoft.sqlserver.jdbc.SQLServerStatement.  
  
 The following is a sample that uses [!INCLUDE[tsql](../../includes/tsql_md.md)] to start an extended events session that will be stored in a ring buffer and will record the activity ID sent from a client on RPC and batch operations:  
  
```  
create event session MySession on server  
add event connectivity_ring_buffer_recorded,  
add event sql_statement_starting (action (client_connection_id)),  
add event sql_statement_completed (action (client_connection_id)),  
add event rpc_starting (action (client_connection_id)),  
add event rpc_completed (action (client_connection_id))  
add target ring_buffer with (track_causality=on)  
```  
  
## See Also  
 [Diagnosing Problems with the JDBC Driver](../../connect/jdbc/diagnosing-problems-with-the-jdbc-driver.md)  
  
  