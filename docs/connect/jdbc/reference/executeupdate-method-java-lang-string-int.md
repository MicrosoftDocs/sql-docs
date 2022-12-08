---
title: "executeUpdate Method (java.lang.String, int)"
description: "executeUpdate Method (java.lang.String, int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.executeUpdate (java.lang.String, int)"
apitype: "Assembly"
---
# executeUpdate Method (java.lang.String, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Runs the given SQL statement and signals the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with the given flag about whether the auto-generated keys that are produced by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object should be made available for retrieval.  
  
## Syntax  
  
```  
  
public final int executeUpdate(java.lang.String sql,  
                               int flag)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** that contains an SQL statement.  
  
 *flag*  
  
 An **int** value that indicates if auto-generated keys should be made available. It must be one of the following constants:  
  
 RETURN_GENERATED_KEYS  
  
 NO_GENERATED_KEYS  
  
## Return Value  
 An **int** that indicates the number of rows affected, or 0 if using a DDL statement.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This executeUpdate method is specified by the executeUpdate method in the java.sql.Statement interface.  
  
 If executing a stored procedure results in an update count that is greater than one, or that generates more than one result set, use the [execute](../../../connect/jdbc/reference/execute-method-sqlserverstatement.md) method to execute the stored procedure.  
  
## See Also  
 [executeUpdate Method &#40;SQLServerStatement&#41;](../../../connect/jdbc/reference/executeupdate-method-sqlserverstatement.md)   
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
