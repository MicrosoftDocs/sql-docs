---
title: "getClientConnectionID Method (SQLServerConnection)"
description: "getClientConnectionID Method (SQLServerConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getClientConnectionID Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Gets the connection ID of the most recent connection attempt, regardless of whether the attempt succeeded or failed.  
  
## Syntax  
  
``` 
public Java.util.UUID SQLServerConnection.getClientConnectionID();  
```  
  
## Return Value  
 A 16-byte GUID representing the connection ID of the most recent connection attempt. Or, NULL if there is a failure after the connection request is initiated and the pre-login handshake.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 For more information about accessing diagnostic information in the extended events log, see [Accessing Diagnostic Information in the Extended Events Log](../../../connect/jdbc/accessing-diagnostic-information-in-the-extended-events-log.md).  
  
 The following sample shows how to get the connection ID:  
  
```  
Connection con = DriverManager.getConnection(connectionUrl);  
UUID id = ((ISQLServerConnection)con).getClientConnectionId();  
```  
  
 The following sample shows another way to get the connection ID:  
  
```  
SQLServerConnectionPoolDataSource ds = new SQLServerConnectionPoolDataSource();  
ds.setUser("...");  
ds.setPassword("...");  
ds.setServerName("...");  
PooledConnection pcon= ds.getPooledConnection();  
Connection cn = pcon.getConnection();  
UUID conid = ((ISQLServerConnection)cn).getClientConnectionId();  
```  
  
 **getClientConnectionID** works regardless of which version of the server you connect to, but extended events logs and entry on connectivity ring buffer errors will not be present in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] 2008 R2 and earlier.  
  
 You can locate the connection ID in the extended events log to see if the failure was on the server if the extended event for logging connection ID is enabled. You can also locate the connection ID in the connection ring buffer ([Connectivity troubleshooting in SQL Server 2008 with the Connectivity Ring Buffer](/archive/blogs/sql_protocols/connectivity-troubleshooting-in-sql-server-2008-with-the-connectivity-ring-buffer)) for certain connection errors. If the connection ID is not in the connection ring buffer, you can assume a network error.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
