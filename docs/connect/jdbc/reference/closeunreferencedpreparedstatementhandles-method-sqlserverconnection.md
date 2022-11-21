---
title: "closeUnreferencedPreparedStatementHandles Method (SQLServerConnection)"
description: "closeUnreferencedPreparedStatementHandles Method (SQLServerConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.closeUnreferencedPreparedStatementHandles"
apitype: "Assembly"
---
# closeUnreferencedPreparedStatementHandles Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

 Forces the un-prepare requests for any outstanding discarded prepared statements to be executed.

## Syntax  
  
```  
  
public void closeUnreferencedPreparedStatementHandles()  
```  


## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  

## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
