---
title: "setEnablePrepareOnFirstPreparedStatementCall Method (SQLServerDataSource)"
description: "setEnablePrepareOnFirstPreparedStatementCall Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setEnablePrepareOnFirstPreparedStatementCall Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Specifies the behavior for a specific connection instance. If this configuration is false the first execution of a prepared statement will call sp_executesql and not prepare a statement, once the second execution happens it will call sp_prepexec and actually setup a prepared statement handle. Following executions will call sp_execute. This relieves the need for sp_unprepare on prepared statement close if the statement is only executed once.  
## Syntax  
  
```
public void setEnablePrepareOnFirstPreparedStatementCall(boolean enablePrepareOnFirstPreparedStatementCall);  
```  
  
#### Parameters  
 *enablePrepareOnFirstPreparedStatementCall*  
  
 The new value of the **enablePrepareOnFirstPreparedStatementCall** connection property.  

## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
