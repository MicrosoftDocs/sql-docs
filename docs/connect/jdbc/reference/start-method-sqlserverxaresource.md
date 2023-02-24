---
title: "start Method (SQLServerXAResource)"
description: "start Method (SQLServerXAResource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerXAResource.start"
apitype: "Assembly"
---
# start Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Starts work on behalf of a transaction branch that is specified in the Xid object.  
  
## Syntax  
  
```  
  
public void start(javax.transaction.xa.Xid xid,  
                  int flags)  
```  
  
#### Parameters  
 *xid*  
  
 An Xid object.  
  
 *flags*  
  
 An **int** value.  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This start method is specified by the start method in the javax.transaction.xa.XAResource interface.  
  
## See Also  
 [SQLServerXAResource Methods](../../../connect/jdbc/reference/sqlserverxaresource-methods.md)   
 [SQLServerXAResource Members](../../../connect/jdbc/reference/sqlserverxaresource-members.md)   
 [SQLServerXAResource Class](../../../connect/jdbc/reference/sqlserverxaresource-class.md)  
  
  
