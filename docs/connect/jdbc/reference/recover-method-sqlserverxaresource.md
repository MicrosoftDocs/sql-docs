---
title: "recover Method (SQLServerXAResource)"
description: "recover Method (SQLServerXAResource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerXAResource.recover"
apitype: "Assembly"
---
# recover Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Obtains a list of prepared transaction branches from a resource manager.  
  
## Syntax  
  
```  
  
public javax.transaction.xa.Xid[] recover(int flags)  
```  
  
#### Parameters  
 *flags*  
  
 An **int** value that can take one of the following values: XAResource.TMSTARTRSCAN or XAResource.TMENDRSCAN or XAResource.TMNOFLAGS or XAResource.TMSTARTTRSCAN | XAResource.TMENDRSCAN.  
  
## Return Value  
 An Xid object.  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This recover method is specified by the recover method in the javax.transaction.xa.XAResource interface.  
  
 If the parameter **flag** is not XAResource.TMSTARTRSCAN or XAResource.TMSTARTRSCAN | XAResource.TMENDRSCAN, a recovery scan must be in progress.  
  
## See Also  
 [SQLServerXAResource Methods](../../../connect/jdbc/reference/sqlserverxaresource-methods.md)   
 [SQLServerXAResource Members](../../../connect/jdbc/reference/sqlserverxaresource-members.md)   
 [SQLServerXAResource Class](../../../connect/jdbc/reference/sqlserverxaresource-class.md)  
  
  
