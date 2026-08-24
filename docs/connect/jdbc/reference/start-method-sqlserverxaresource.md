---
title: "start Method (SQLServerXAResource)"
description: "start Method (SQLServerXAResource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
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
  
## Related content

- [SQLServerXAResource Methods](sqlserverxaresource-methods.md)
- [SQLServerXAResource Members](sqlserverxaresource-members.md)
- [SQLServerXAResource Class](sqlserverxaresource-class.md)
