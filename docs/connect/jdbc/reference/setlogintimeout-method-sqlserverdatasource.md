---
title: "setLoginTimeout Method (SQLServerDataSource)"
description: "setLoginTimeout Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
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
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
