---
title: "executeUpdate Method (java.lang.String, java.lang.String)"
description: "executeUpdate Method (java.lang.String, java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.executeUpdate (java.lang.String[])"
apitype: "Assembly"
---
# executeUpdate Method (java.lang.String, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Runs the given SQL statement and signals [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] that the auto-generated keys indicated in the given array should be made available for retrieval.  
  
## Syntax  
  
```  
  
public final int executeUpdate(java.lang.String sql,  
                               java.lang.String[] columnNames)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** that contains a SQL statement.  
  
 *columnNames*  
  
 An array of type **String** that indicates which column names of the auto-generated keys should be made available.  
  
## Return Value  
 An **int** that indicates the number of rows affected, 0 if using a DDL statement.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This executeUpdate method is specified by the executeUpdate method in the java.sql.Statement interface.  
  
 If executing a stored procedure results in an update count that is greater than one, or that generates more than one result set, use the [execute](../../../connect/jdbc/reference/execute-method-sqlserverstatement.md) method to execute the stored procedure.  
  
## Related content

- [executeUpdate Method (SQLServerStatement)](executeupdate-method-sqlserverstatement.md)
- [SQLServerStatement Members](sqlserverstatement-members.md)
- [SQLServerStatement Class](sqlserverstatement-class.md)
