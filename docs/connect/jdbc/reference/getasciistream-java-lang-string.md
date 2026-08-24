---
title: "getAsciiStream (java.lang.String)"
description: "getAsciiStream (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "SQLServerCallableStatement.getAsciiStream(String paramName)"
apiname: "SQLServerCallableStatement.getAsciiStream(String paramName)"
apitype: "Assembly"
---
# getAsciiStream (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a stream of **ASCII** characters given the parameter name.  
  
## Syntax  
  
```  
  
public final java.io.InputStream getAsciiStream(java.lang.String paramName)  
```  
  
#### Parameters  
 *paramName*  
  
 A **String** that indicates the parameter name.  
  
## Return Value  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Related content

- [getAsciiStream Method (SQLServerCallableStatement)](getasciistream-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
