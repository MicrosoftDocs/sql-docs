---
title: "getNString Method (java.lang.String)"
description: "getNString Method (java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getNString Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated **NCHAR**, **NVARCHAR**, or **LONGNVARCHAR** parameter as a String in the Java programming language.  
  
## Syntax  
  
```  
  
public final java.lang.String getNString(java.lang.String parameterName)  
```  
  
#### Parameters  
 *parameterName*  
  
 A **String** that contains the parameter name.  
  
## Return Value  
 AStringobject.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getNString method is specified by the getNString method in the java.sql.CallableStatement interface.  
  
## See Also  
 [getNString Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getnstring-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Methods](../../../connect/jdbc/reference/sqlservercallablestatement-methods.md)  
  
  
