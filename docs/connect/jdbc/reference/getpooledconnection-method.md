---
title: "getPooledConnection Method () | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnectionPoolDataSource.getPooledConnection ()"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: aad6c325-3398-462c-aa6e-201dc89fa5ef
author: MightyPen
ms.author: genemi
manager: craigg
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
  
## See Also  
 [getPooledConnection](../../../connect/jdbc/reference/getpooledconnection-method-sqlserverconnectionpooldatasource.md)   
 [SQLServerConnectionPoolDataSource Methods](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-methods.md)   
 [SQLServerConnectionPoolDataSource Members](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-members.md)   
 [SQLServerConnectionPoolDataSource Class](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-class.md)  
  
  
