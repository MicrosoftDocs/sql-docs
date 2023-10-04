---
title: "prepareStatement Method (java.lang.String, int[])"
description: "prepareStatement Method (java.lang.String, int[])"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.prepareStatement (java.lang.String, int[])"
apitype: "Assembly"
---
# prepareStatement Method (java.lang.String, int[])
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) object for sending parameterized SQL statements to the database, and that is capable of returning the auto-generated keys designated by the given array.  
  
## Syntax  
  
```  
  
public java.sql.PreparedStatement prepareStatement(java.lang.String sql,  
                                                   int[] columnIndexes)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** that contains an SQL statement.  
  
 *columnIndexes*  
  
 An array of ints.  
  
## Return Value  
 A PreparedStatement object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This prepareStatement method is specified by the prepareStatement method in the java.sql.Connection interface.  
  
## See Also  
 [prepareStatement Method &#40;SQLServerConnection&#41;](../../../connect/jdbc/reference/preparestatement-method-sqlserverconnection.md)   
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
