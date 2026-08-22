---
title: "finalize Method (SQLServerResultSet)"
description: "finalize Method (SQLServerResultSet)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.finalize"
apitype: "Assembly"
---
# finalize Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Explicitly closes this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public void finalize()  
```  
  
## Remarks  
 Closes the result set if the application does not. This method exists only to conform to the JDBC specification. Because the Java Virtual Machine (JVM) does not guarantee when a finalizer will have a chance to run, applications that neglect to explicitly close their result sets could still deadlock on another statement that is using the same connection and is blocked on a common server resource, such as row locks.  
  
## Related content

- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
