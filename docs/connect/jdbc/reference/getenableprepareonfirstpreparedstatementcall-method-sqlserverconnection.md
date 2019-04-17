---
title: "getEnablePrepareOnFirstPreparedStatementCall Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.getEnablePrepareOnFirstPreparedStatementCall"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid:
author: MightyPen
ms.author: genemi
manager: craigg
---
# getEnablePrepareOnFirstPreparedStatementCall Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

 Returns the value of **enablePrepareOnFirstPreparedStatementCall** connection property. If false the first execution will call sp_executesql and not prepare a statement, once the second execution happens it will call sp_prepexec and actually set up a prepared statement handle. Following executions will call sp_execute. This relieves the need for sp_unprepare on prepared statement close if the statement is only executed once. The default for this option can be changed by calling setDefaultEnablePrepareOnFirstPreparedStatementCall().

## Syntax  
  
```  
  
public boolean getEnablePrepareOnFirstPreparedStatementCall()  
```  

## Return Value
 A **boolean** that contains the value of **enablePrepareOnFirstPreparedStatementCall** connection property.

## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
