---
title: "relative Method (SQLServerResultSet)"
description: "relative Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.relative"
apitype: "Assembly"
---
# relative Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves the cursor the given amount of rows, relative to the current row, in either a positive or negative direction.  
  
## Syntax  
  
```  
  
public boolean relative(int nRows)  
```  
  
#### Parameters  
 *nRows*  
  
 An **int** that indicates the number of rows to move.  
  
## Return Value  
 **true** if the cursor is on a row. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This relative method is specified by the relative method in the java.sql.ResultSet interface.  
  
 Trying to move beyond the first or last row in the result set positions the cursor before or after the first or last row. Calling `relative(0)` is valid, but does not change the cursor position.  
  
 Calling the method `relative(1)` is identical to calling the [next](../../../connect/jdbc/reference/next-method-sqlserverresultset.md) method. Calling the method `relative(-1)` is identical to calling the [previous](../../../connect/jdbc/reference/previous-method-sqlserverresultset.md) method.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
