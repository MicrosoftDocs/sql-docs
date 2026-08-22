---
title: "setLoginTimeout Method (SQLServerDataSource)"
description: "setLoginTimeout Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setLoginTimeout"
apitype: "Assembly"
---
# setLoginTimeout Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the number of seconds that this [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) object will wait while trying to make a connection.  
  
## Syntax  
  
```  
  
public void setLoginTimeout(int loginTimeout)  
```  
  
#### Parameters  
 *loginTimeout*  
  
 An **int** value that represents the number of seconds to wait. Zero means that the timeout is the default system timeout, which is specified as 15 seconds by default.  
  
## Remarks  
 This setLoginTimeout method is specified by the setLoginTimeout method in the javax.sql.DataSource interface.  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
