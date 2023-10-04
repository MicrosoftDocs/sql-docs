---
title: "setArray Method (SQLServerPreparedStatement)"
description: "setArray Method (SQLServerPreparedStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.setArray"
apitype: "Assembly"
---
# setArray Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter number to the given Array object.  
  
## Syntax  
  
```  
  
public final void setArray(int i,  
                           java.sql.Array x)  
```  
  
#### Parameters  
 *i*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 An Array object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setArray method is specified by the setArray method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
