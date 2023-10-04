---
title: "setNString Method (int, java.lang.String)"
description: "setNString Method (int, java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setNString Method (int, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the specified **String** object.  
  
## Syntax  
  
```  
  
public final void setNString(int parameterIndex,  
                                                  java.lang.String value)  
```  
  
#### Parameters  
 *parameterIndex*  
  
 An **int** that indicates the parameter index.  
  
 *value*  
  
 A **String** object that contains the parameter value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This method should be used for **NCHAR**, **NVARCHAR**, **NTEXT**, and **XML** data types.  
  
 This setNString method is specified by the setNString method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)  
  
  
