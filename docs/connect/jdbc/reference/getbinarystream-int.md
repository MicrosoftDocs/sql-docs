---
title: "getBinaryStream (int)"
description: "getBinaryStream (int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "SQLServerCallableStatement.getBinaryStream(int paramIndex)"
apiname: "SQLServerCallableStatement.getBinaryStream(int paramIndex)"
apitype: "Assembly"
---
# getBinaryStream (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a binary stream of uninterrupted bytes given the parameter index.  
  
## Syntax  
  
```  
  
public final java.io.InputStream getBinaryStream(int paramIndex)  
```  
  
#### Parameters  
 *paramIndex*  
  
 An **int** that indicates the parameter index.  
  
## Return Value  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## See Also  
 [getBinaryStream Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getbinarystream-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
