---
title: "setNCharacterStream Method to Reader object - int"
description: "setNCharacterStream Method to Reader object - int"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setNCharacterStream Method (int, java.io.Reader)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the specified Reader object.  
  
## Syntax  
  
```  
  
public final void setNCharacterStream(int parameterIndex,  
                                                 java.io.Reader value)  
```  
  
#### Parameters  
 *parameterIndex*  
  
 An **int** that indicates the parameter index.  
  
 *value*  
  
 A Reader object that contains the parameter value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setNCharacterStream method is specified by the setNCharacterStream method in the java.sql.PreparedStatement interface.  
  
 This method should be used for **NCHAR**, **NVARCHAR**, **NTEXT**, and **XML** data types.  
  
## See Also  
 [setNCharacterStream Method &#40;SQLServerPreparedStatement&#41;](../../../connect/jdbc/reference/setncharacterstream-method-sqlserverpreparedstatement.md)   
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)  
  
  
