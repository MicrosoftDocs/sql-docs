---
title: "setMaxFieldSize Method (SQLServerStatement)"
description: "setMaxFieldSize Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.setMaxFieldSize"
apitype: "Assembly"
---
# setMaxFieldSize Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the limit for the maximum number of bytes in a [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) column storing character or binary values to the given number of bytes.  
  
## Syntax  
  
```  
  
public final void setMaxFieldSize(int max)  
```  
  
#### Parameters  
 *max*  
  
 An **int** that indicates the maximum number of bytes.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setMaxFieldSize method is specified by the setMaxFieldSize method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
