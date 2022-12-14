---
title: "prepareStatement Method (java.lang.String, int, int)"
description: "prepareStatement Method (java.lang.String, int, int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.prepareStatement (java.lang.String, int, int)"
apitype: "Assembly"
---
# prepareStatement Method (java.lang.String, int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) object that generates [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects with the given type and concurrency.  
  
## Syntax  
  
```  
  
public java.sql.PreparedStatement prepareStatement(java.lang.String sSql,  
                                                   int resultSetType,  
                                                   int resultSetConcurrency)  
```  
  
#### Parameters  
 *sSql*  
  
 A **String** containing an SQL statement.  
  
 *resultSetType*  
  
 An **int** that indicates the result set type.  
  
 *resultSetConcurrency*  
  
 An **int** that indicates the result set concurrency type.  
  
## Return Value  
 A PreparedStatement object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This prepareStatement method is specified by the prepareStatement method in the java.sql.Connection interface.  
  
## See Also  
 [SQLServerConnection Methods](../../../connect/jdbc/reference/sqlserverconnection-methods.md)   
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
