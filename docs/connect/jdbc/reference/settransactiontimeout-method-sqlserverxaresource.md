---
title: "setTransactionTimeout Method (SQLServerXAResource)"
description: "setTransactionTimeout Method (SQLServerXAResource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerXAResource.setTransactionTimeout"
apitype: "Assembly"
---
# setTransactionTimeout Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the current transaction time out value for this [SQLServerXAResource](../../../connect/jdbc/reference/sqlserverxaresource-class.md) object.  
  
## Syntax  
  
```  
  
public boolean setTransactionTimeout(int seconds)  
```  
  
#### Parameters  
 *seconds*  
  
 An **int** value.  
  
## Return Value  
 **true** if the timeout was successfully set. Otherwise, **false**.  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This setTransactionTimeout method is specified by the setTransactionTimeout method in the javax.transaction.xa.XAResource interface.  
  
## See Also  
 [SQLServerXAResource Methods](../../../connect/jdbc/reference/sqlserverxaresource-methods.md)   
 [SQLServerXAResource Members](../../../connect/jdbc/reference/sqlserverxaresource-members.md)   
 [SQLServerXAResource Class](../../../connect/jdbc/reference/sqlserverxaresource-class.md)  
  
  
