---
title: "getPooledConnection Method ()"
description: "getPooledConnection Method ()"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnectionPoolDataSource.getPooledConnection ()"
apitype: "Assembly"
---
# getPooledConnection Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Tries to establish a physical database connection that can be used as a pooled connection.  
  
## Syntax  
  
```  
  
public javax.sql.PooledConnection getPooledConnection()  
```  
  
## Return Value  
 A [SQLServerPooledConnection](../../../connect/jdbc/reference/sqlserverpooledconnection-class.md) object.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This getPooledConnection method is specified by the getPooledConnection method in the javax.sql.ConnectionPoolDataSource interface.  
  
## Related content

- [getPooledConnection Method (SQLServerConnectionPoolDataSource)](getpooledconnection-method-sqlserverconnectionpooldatasource.md)
- [SQLServerConnectionPoolDataSource Methods](sqlserverconnectionpooldatasource-methods.md)
- [SQLServerConnectionPoolDataSource Members](sqlserverconnectionpooldatasource-members.md)
- [SQLServerConnectionPoolDataSource Class](sqlserverconnectionpooldatasource-class.md)
