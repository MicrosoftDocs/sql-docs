---
title: "end Method (SQLServerXAResource)"
description: "end Method (SQLServerXAResource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerXAResource.end"
apitype: "Assembly"
---
# end Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Ends the work performed on behalf of a transaction branch.  
  
## Syntax  
  
```  
  
public void end(javax.transaction.xa.Xid xid,  
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
 This end method is specified by the end method in the javax.transaction.xa.XAResource interface.  
  
## Related content

- [SQLServerXAResource Methods](sqlserverxaresource-methods.md)
- [SQLServerXAResource Members](sqlserverxaresource-members.md)
- [SQLServerXAResource Class](sqlserverxaresource-class.md)
