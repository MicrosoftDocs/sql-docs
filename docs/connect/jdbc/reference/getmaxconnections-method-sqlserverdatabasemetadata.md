---
title: "getMaxConnections Method (SQLServerDatabaseMetaData)"
description: "getMaxConnections Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getMaxConnections"
apitype: "Assembly"
---
# getMaxConnections Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of concurrent connections to this database that are possible.  
  
## Syntax  
  
```  
  
public int getMaxConnections()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of concurrent connections allowed.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxConnections method is specified by the getMaxConnections method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
