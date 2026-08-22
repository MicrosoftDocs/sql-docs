---
title: "rollback Method (SQLServerXAResource)"
description: "rollback Method (SQLServerXAResource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerXAResource.rollback"
apitype: "Assembly"
---
# rollback Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Requests that the resource manager roll back work done on behalf of a transaction branch.  
  
## Syntax  
  
```  
  
public void rollback(javax.transaction.xa.Xid xid)  
```  
  
#### Parameters  
 *xid*  
  
 An Xid object.  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This rollback method is specified by the rollback method in the javax.transaction.xa.XAResource interface.  
  
## Related content

- [SQLServerXAResource Methods](sqlserverxaresource-methods.md)
- [SQLServerXAResource Members](sqlserverxaresource-members.md)
- [SQLServerXAResource Class](sqlserverxaresource-class.md)
