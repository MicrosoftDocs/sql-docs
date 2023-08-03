---
title: "getStatementPoolingCacheSize Method (SQLServerConnection)"
description: "getStatementPoolingCacheSize Method (SQLServerConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.getStatementPoolingCacheSize"
apitype: "Assembly"
---
# getStatementPoolingCacheSize Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

 Returns the size of the prepared statement cache for this connection. '0' means caching not enabled.

## Syntax  
  
```  
  
public int getStatementPoolingCacheSize()  
```  

## Return Value
 An **int** that contains the value of **statementPoolingCacheSize** connection property.

## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
