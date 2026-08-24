---
title: "getXAResource Method (SQLServerXAConnection)"
description: "getXAResource Method (SQLServerXAConnection)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerXAConnection.getXAResource"
apitype: "Assembly"
---
# getXAResource Method (SQLServerXAConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a [SQLServerXAResource](../../../connect/jdbc/reference/sqlserverxaresource-class.md) object that the transaction manager will use to manage this [SQLServerXAConnection](../../../connect/jdbc/reference/sqlserverxaconnection-class.md) object's participation in a distributed transaction.  
  
## Syntax  
  
```  
  
public javax.transaction.xa.XAResource getXAResource()  
```  
  
## Return Value  
 An XAResource object.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This getXAResource method is specified by the getXAResource method in the javax.sql.XAConnection interface.  
  
## Related content

- [SQLServerXAConnection Methods](sqlserverxaconnection-methods.md)
- [SQLServerXAConnection Members](sqlserverxaconnection-members.md)
- [SQLServerXAConnection Class](sqlserverxaconnection-class.md)
