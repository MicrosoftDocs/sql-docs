---
title: "getWarnings Method (SQLServerConnection)"
description: "getWarnings Method (SQLServerConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.getWarnings"
apitype: "Assembly"
---
# getWarnings Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the first warning reported by calls on this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
## Syntax  
  
```  
  
public java.sql.SQLWarning getWarnings()  
```  
  
## Return Value  
 A SQLWarning object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getWarnings method is specified by the getWarnings method in the java.sql.Connection interface.  
  
 Subsequent warnings are chained to the first SQLWarning and called with the getNextWarning method. If called on a closed connection, an exception will be thrown.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
