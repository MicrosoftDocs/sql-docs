---
title: "findColumn Method (SQLServerResultSet)"
description: "findColumn Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.findColumn"
apitype: "Assembly"
---
# findColumn Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the index of the first matching column for the given column name in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public int findColumn(java.lang.String columnName)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the name of the column.  
  
## Return Value  
 An **int** that indicates the column index.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This findColumn method is specified by the findColumn method in the java.sql.ResultSet interface.  
  
 If there are multiple columns with the same name, the findColumn method returns the first case-sensitive match. If there is no case-sensitive match, this method returns the first case-insensitive match.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
