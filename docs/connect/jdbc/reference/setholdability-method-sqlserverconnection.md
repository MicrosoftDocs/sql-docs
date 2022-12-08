---
title: "setHoldability Method (SQLServerConnection)"
description: "setHoldability Method (SQLServerConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.setHoldability"
apitype: "Assembly"
---
# setHoldability Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Changes the holdability of [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects that are created by using this [SQLServerSavepoint](../../../connect/jdbc/reference/sqlserversavepoint-class.md) object to the given holdability.  
  
## Syntax  
  
```  
  
public void setHoldability(int nNewHold)  
```  
  
#### Parameters  
 *nNewHold*  
  
 An **int** value that contains one of the following holdability levels:  
  
 HOLD_CURSORS_OVER_COMMIT  
  
 CLOSE_CURSORS_AT_COMMIT  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setHoldability method is specified by the setHoldability method in the java.sql.Connection interface.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
