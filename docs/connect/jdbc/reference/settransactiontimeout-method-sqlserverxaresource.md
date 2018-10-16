---
title: "setTransactionTimeout Method (SQLServerXAResource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerXAResource.setTransactionTimeout"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 38bf4a1a-6ad3-437c-b9ed-8792ab6dde7e
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
