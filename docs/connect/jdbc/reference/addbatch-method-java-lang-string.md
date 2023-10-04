---
title: "addBatch Method (java.lang.String)"
description: "addBatch Method (java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.addBatch (java.lang.String)"
apitype: "Assembly"
---
# addBatch Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Adds the given SQL command to the current list of commands for this [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) object.  
  
## Syntax  
  
```  
  
public void addBatch(java.lang.String sql)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** that contains an SQL statement.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This addBatch method is specified by the addBatch method in the java.sql.Statement interface.  
  
 Calling this method will result in an exception since the SQL statement for the SQLServerPreparedStatement object is specified when the object is created.  
  
## See Also  
 [addBatch Method &#40;SQLServerPreparedStatement&#41;](../../../connect/jdbc/reference/addbatch-method-sqlserverpreparedstatement.md)   
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
