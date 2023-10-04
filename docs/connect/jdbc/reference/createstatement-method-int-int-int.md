---
title: "createStatement Method (int, int, int)"
description: "createStatement Method (int, int, int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.createStatement (int, int, int)"
apitype: "Assembly"
---
# createStatement Method (int, int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object that generates [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects with the given type, concurrency, and holdability.  
  
## Syntax  
  
```  
  
public java.sql.Statement createStatement(int nType,  
                                          int nConcur,  
                                          int nHold)  
```  
  
#### Parameters  
 *resultSetType*  
  
 The **int** value that represents the result set type.  
  
 *nConcur*  
  
 The **int** value that represents the result set concurrency type.  
  
 *nHold*  
  
 The **int** value that represents the holdability.  
  
## Return Value  
 The Statement object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This createStatement method is specified by the createStatement method in the java.sql.Connection interface.  
  
## See Also  
 [createStatement Method &#40;SQLServerConnection&#41;](../../../connect/jdbc/reference/createstatement-method-sqlserverconnection.md)   
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
