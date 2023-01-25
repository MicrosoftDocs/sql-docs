---
title: "setSelectMethod Method (SQLServerDataSource)"
description: "setSelectMethod Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setSelectMethod"
apitype: "Assembly"
---
# setSelectMethod Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the default cursor type that is used for all result sets that are created by using this [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) object.  
  
## Syntax  
  
```  
  
public void setSelectMethod(java.lang.String selectMethod)  
```  
  
#### Parameters  
 *selectMethod*  
  
 A **String** value that contains the default cursor type.  
  
## Remarks  
 The selectMethod is the default cursor type that is used for a result set. This property is useful when you are dealing with large result sets and do not want to store the whole result set in memory on the client side. By setting the property to "cursor," you can create a server-side cursor that can fetch smaller chunks of data at a time. If the selectMethod property is not set, [getSelectMethod](../../../connect/jdbc/reference/getselectmethod-method-sqlserverdatasource.md) returns the default value of "direct".  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
