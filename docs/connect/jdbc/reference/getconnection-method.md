---
title: "getConnection Method ()"
description: "getConnection Method ()"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getConnection ()"
apitype: "Assembly"
---
# getConnection Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Tries to establish a connection with the data source that this [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) object represents.  
  
## Syntax  
  
```  
  
public java.sql.Connection getConnection()  
```  
  
## Return Value  
 A [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This getConnection method is specified by the getConnection method in the javax.sql.DataSource interface.  
  
## See Also  
 [getConnection Method &#40;SQLServerDataSource&#41;](../../../connect/jdbc/reference/getconnection-method-sqlserverdatasource.md)   
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
