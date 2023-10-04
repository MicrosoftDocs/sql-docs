---
title: "executeUpdate Method ()"
description: "executeUpdate Method ()"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.executeUpdate ()"
apitype: "Assembly"
---
# executeUpdate Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Runs the SQL statement in this [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) object, which must be an SQL INSERT, UPDATE, MERGE, or DELETE statement; or an SQL statement that returns nothing, such as a DDL statement.  
  
## Syntax  
  
```  
  
public int executeUpdate()  
```  
  
## Return Value  
 An **int** that indicates the number of rows affected, or 0 if using a DDL statement.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This executeUpdate method is specified by the executeUpdate method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [executeUpdate Method &#40;SQLServerPreparedStatement&#41;](../../../connect/jdbc/reference/executeupdate-method-sqlserverpreparedstatement.md)   
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
