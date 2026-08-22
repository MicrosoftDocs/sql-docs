---
title: "executeQuery Method (java.lang.String)"
description: "executeQuery Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.executeQuery (java.lang.String)"
apitype: "Assembly"
---
# executeQuery Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Runs the given SQL statement and returns a single [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public final java.sql.ResultSet executeQuery(java.lang.String sql)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** that contains a SQL statement.  
  
## Return Value  
 A SQLServerResultSet object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This executeQuery method is specified by the executeQuery method in the java.sql.Statement interface.  
  
 This method overrides the [executeQuery](../../../connect/jdbc/reference/executequery-method-sqlserverstatement.md) method that is found in the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) class.  
  
 Calling this method will result in an exception since the SQL statement for the SQLServerPreparedStatement object is specified when the object is created.  
  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md) is thrown if the given SQL statement produces anything other than a single [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Related content

- [executeQuery Method (SQLServerPreparedStatement)](executequery-method-sqlserverpreparedstatement.md)
- [SQLServerPreparedStatement Members](sqlserverpreparedstatement-members.md)
- [SQLServerPreparedStatement Class](sqlserverpreparedstatement-class.md)
