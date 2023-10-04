---
title: "getMaxRows Method (SQLServerStatement)"
description: "getMaxRows Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.getMaxRows"
apitype: "Assembly"
---
# getMaxRows Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of rows that a [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object that is produced by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object can contain.  
  
## Syntax  
  
```  
  
public final int getMaxRows()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of rows, or 0 if there is no limit.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxRows method is specified by the getMaxRows method in the java.sql.Statement interface.  
  
 This getMaxRows method always returns 0 for dynamic scrollable cursors.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
