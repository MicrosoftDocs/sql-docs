---
title: "getNCharacterStream Method (java.lang.String)"
description: "getNCharacterStream Method (java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getNCharacterStream Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a Reader object given the parameter name.  
  
## Syntax  
  
```  
  
public final java.io.Reader getNCharacterStream(java.lang.String columnLabel)  
```  
  
#### Parameters  
 *columnLabel*  
  
 A **String** that contains the column label.  
  
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
  
  
