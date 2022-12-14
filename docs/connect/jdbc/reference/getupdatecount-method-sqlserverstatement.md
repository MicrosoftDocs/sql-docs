---
title: "getUpdateCount Method (SQLServerStatement)"
description: "getUpdateCount Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.getUpdateCount"
apitype: "Assembly"
---
# getUpdateCount Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the current result as an update count.  
  
## Syntax  
  
```  
  
public final int getUpdateCount()  
```  
  
## Return Value  
 An **int** that contains the update count. If the returned result is a result set object or there are no more results, -1 is returned.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getUpdateCount method is specified by the getUpdateCount method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
