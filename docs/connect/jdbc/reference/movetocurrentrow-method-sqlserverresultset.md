---
title: "moveToCurrentRow Method (SQLServerResultSet)"
description: "moveToCurrentRow Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.moveToCurrentRow"
apitype: "Assembly"
---
# moveToCurrentRow Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves the cursor to the remembered cursor position, which is usually the current row.  
  
## Syntax  
  
```  
  
public void moveToCurrentRow()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This moveToCurrentRow method is specified by the moveToCurrentRow method in the java.sql.ResultSet interface.  
  
 This method has no effect if the cursor is not on the insert row.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
