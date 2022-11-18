---
title: "getNCharacterStream Method (int)"
description: "getNCharacterStream Method (int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getNCharacterStream Method (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a Reader object given the parameter index.  
  
## Syntax  
  
```  
  
public final java.io.Reader getNCharacterStream(int parameterIndex)  
```  
  
#### Parameters  
 *parameterIndex*  
  
 An **int** that indicates the parameter index.  
  
## Return Value  
 AReaderobject.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This method should be used when accessing **NCHAR**, **NVARCHAR** and **LONGNVARCHAR** parameters.  
  
 This getNCharacterStream method is specified by the getNCharacterStream method in the java.sql.CallableStatement interface.  
  
## See Also  
 [getNCharacterStream Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getncharacterstream-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)  
  
  
