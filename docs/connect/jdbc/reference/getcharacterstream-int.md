---
title: "getCharacterStream (int) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerCallableStatement.getCharacterStream(int paramIndex)"
apilocation: 
  - "SQLServerCallableStatement.getCharacterStream(int paramIndex)"
apitype: "Assembly"
ms.assetid: eb20714b-52bc-4b6c-b23f-c9c3c9d73783
author: MightyPen
ms.author: genemi
manager: craigg
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
  
## See Also  
 [getCharacterStream Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getcharacterstream-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
