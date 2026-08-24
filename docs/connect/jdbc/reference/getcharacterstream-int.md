---
title: "getCharacterStream (int)"
description: "getCharacterStream (int)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "SQLServerCallableStatement.getCharacterStream(int paramIndex)"
apiname: "SQLServerCallableStatement.getCharacterStream(int paramIndex)"
apitype: "Assembly"
---
# getCharacterStream (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a java.io.Reader object given the parameter index.  
  
## Syntax  
  
```  
  
public final java.io.Reader getCharacterStream(int paramIndex)  
```  
  
#### Parameters  
 *paramIndex*  
  
 An **int** that indicates the parameter index.  
  
## Return Value  
 A Reader object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Related content

- [getCharacterStream Method (SQLServerCallableStatement)](getcharacterstream-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
