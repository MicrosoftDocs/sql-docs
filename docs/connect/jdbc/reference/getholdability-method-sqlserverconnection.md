---
title: "getHoldability Method (SQLServerConnection)"
description: "getHoldability Method (SQLServerConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.getHoldability"
apitype: "Assembly"
---
# getHoldability Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the current holdability of [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects created by using this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
## Syntax  
  
```  
  
public int getHoldability()  
```  
  
## Return Value  
 An **int** value that contains one of the following holdability levels:  
  
 HOLD_CURSORS_OVER_COMMIT  
  
 CLOSE_CURSORS_AT_COMMIT  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getHoldability method is specified by the getHoldability method in the java.sql.Connection interface.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
