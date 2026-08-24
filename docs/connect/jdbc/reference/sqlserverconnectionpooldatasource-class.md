---
title: "SQLServerConnectionPoolDataSource Class"
description: "SQLServerConnectionPoolDataSource Class"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# SQLServerConnectionPoolDataSource Class
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents physical database connections for connection pool managers.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Extends:** [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
 **Implements:** javax.sql.ConnectionPoolDataSource  
  
## Syntax  
  
```  
  
public class SQLServerConnectionPoolDataSource  
```  
  
## Remarks  
 SQLServerConnectionPoolDataSource is typically used in Java Application Server environments that support built-in connection pooling and require a ConnectionPoolDataSource to provide physical connections, such as Java Platform, Enterprise Edition (Java EE) application servers that provide JDBC 3.0 API spec connection pooling.  
  
## Related content

- [SQLServerConnectionPoolDataSource Members](sqlserverconnectionpooldatasource-members.md)
- [JDBC driver API reference](jdbc-driver-api-reference.md)
