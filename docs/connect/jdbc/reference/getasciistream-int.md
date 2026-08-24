---
title: "getAsciiStream (int)"
description: "getAsciiStream (int)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "SQLServerCallableStatement.getAsciiStream(int paramIndex)"
apiname: "SQLServerCallableStatement.getAsciiStream(int paramIndex)"
apitype: "Assembly"
---
# getAsciiStream (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a stream of **ASCII** characters given the parameter index.  
  
## Syntax  
  
```  
  
public final java.io.InputStream getAsciiStream(int paramIndex)  
```  
  
#### Parameters  
 *paramIndex*  
  
 An **int** that indicates the parameter index.  
  
## Return Value  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Related content

- [getAsciiStream Method (SQLServerCallableStatement)](getasciistream-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
