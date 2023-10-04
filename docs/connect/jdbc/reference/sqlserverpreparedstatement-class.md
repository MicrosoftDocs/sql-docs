---
title: "SQLServerPreparedStatement Class"
description: "SQLServerPreparedStatement Class"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# SQLServerPreparedStatement Class
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents the basic implementation of JDBC prepared statement functionality.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Extends:** SQLServerStatement  
  
 **Implements:** [ISQLServerPreparedStatement](../../../connect/jdbc/reference/isqlserverpreparedstatement-interface.md)  
  
## Syntax  
  
```  
  
public class SQLServerPreparedStatement  
```  
  
## Remarks  
 SQLServerPreparedStatement provides methods that let you supply parameters as any native Java type and many Java object types. SQLServerPreparedStatement prepares a statement by using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **sp_prepare** stored procedure, and then reuses the returned statement handle for each subsequent running of the statement, typically using different parameters provided by the user.  
  
 SQLServerPreparedStatement supports batching, where a set of prepared statements are run in a single database round trip, to improve runtime performance.  
  
 This class supports unwrapping to SQLServerPreparedStatement class, ISQLServerPreparedStatement interface, java.sql.PreparedStatement interface, and the classes and interfaces supported by SQLServerStatement for unwrapping. For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [JDBC Driver API Reference](../../../connect/jdbc/reference/jdbc-driver-api-reference.md)  
  
  
