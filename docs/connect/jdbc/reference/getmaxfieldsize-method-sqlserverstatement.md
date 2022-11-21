---
title: "getMaxFieldSize Method (SQLServerStatement)"
description: "getMaxFieldSize Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.getMaxFieldSize"
apitype: "Assembly"
---
# getMaxFieldSize Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of bytes that can be returned for character and binary column values in a [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object that is produced by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.  
  
## Syntax  
  
```  
  
public final int getMaxFieldSize()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of bytes that a column can contain, or 0 if there is no limit.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxFieldSize method is specified by the getMaxFieldSize method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Methods](../../../connect/jdbc/reference/sqlserverstatement-methods.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
