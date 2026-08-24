---
title: "getCharacterStream (java.lang.String)"
description: "getCharacterStream (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "SQLServerCallableStatement.getCharacterStream(String paramName)"
apiname: "SQLServerCallableStatement.getCharacterStream(String paramName)"
apitype: "Assembly"
---
# getCharacterStream (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a java.io.Reader object given the parameter name.  
  
## Syntax  
  
```  
  
public final java.io.Reader getCharacterStream(java.lang.String paramName)  
```  
  
#### Parameters  
 *paramName*  
  
 A **String** that indicates the parameter name.  
  
## Return Value  
 A Reader object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Related content

- [getCharacterStream Method (SQLServerCallableStatement)](getcharacterstream-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
