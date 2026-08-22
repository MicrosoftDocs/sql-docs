---
title: "getTransactionTimeout Method (SQLServerXAResource)"
description: "getTransactionTimeout Method (SQLServerXAResource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerXAResource.getTransactionTimeout"
apitype: "Assembly"
---
# getTransactionTimeout Method (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Obtains the current transaction timeout value set for this [SQLServerXAResource](../../../connect/jdbc/reference/sqlserverxaresource-class.md) object.  
  
## Syntax  
  
```  
  
public int getTransactionTimeout()  
```  
  
## Exceptions  
 javax.transaction.xa.XAException  
  
## Remarks  
 This getTransactionTimeout method is specified by the getTransactionTimeout method in the javax.transaction.xa.XAResource interface.  
  
## Related content

- [SQLServerXAResource Methods](sqlserverxaresource-methods.md)
- [SQLServerXAResource Members](sqlserverxaresource-members.md)
- [SQLServerXAResource Class](sqlserverxaresource-class.md)
