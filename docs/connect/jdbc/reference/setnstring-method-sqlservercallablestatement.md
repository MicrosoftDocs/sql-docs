---
title: "setNString Method (SQLServerCallableStatement)"
description: "setNString Method (SQLServerCallableStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setNString Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the specified String object.  
  
## Syntax  
  
```  
  
public final void setNString(java.lang.String parameterName, java.lang.String value)  
```  
  
#### Parameters  
 *parameterName*  
  
 A **String** that indicates the parameter name.  
  
 *value*  
  
 A String object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This method should be used for **NCHAR**, **NVARCHAR**, **NTEXT**, and **XML** data types.  
  
 This setNString method is specified by the setNString method in the java.sql.CallableStatement interface.  
  
## See Also  
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
