---
title: "getBinaryStream (java.lang.String)"
description: "getBinaryStream (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "SQLServerCallableStatement.getBinaryStream(String paramName)"
apiname: "SQLServerCallableStatement.getBinaryStream(String paramName)"
apitype: "Assembly"
---
# getBinaryStream (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a binary stream of uninterrupted bytes given the parameter name.  
  
## Syntax  
  
```  
  
public final java.io.InputStream getBinaryStream(java.lang.String paramName)  
```  
  
#### Parameters  
 *paramName*  
  
 A **String** that indicates the parameter name.  
  
## Return Value  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Related content

- [getBinaryStream Method (SQLServerCallableStatement)](getbinarystream-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
