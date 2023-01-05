---
title: "getNString Method (int)"
description: "getNString Method (int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getNString Method (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated **NCHAR**, **NVARCHAR**, or **LONGNVARCHAR** parameter as a String in the Java programming language.  
  
## Syntax  
  
```  
  
public final java.lang.String getNString(int parameterIndex)  
```  
  
#### Parameters  
 *parameterIndex*  
  
 An **int** that indicates the parameter index.  
  
## Return Value  
 AStringobject.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getNString method is specified by the getNString method in the java.sql.CallableStatement interface.  
  
## See Also  
 [getNString Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getnstring-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)  
  
  
