---
title: "setLoginTimeout Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.setLoginTimeout"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: b63d1cf4-dc1b-4e35-9a56-50436642eaaf
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
