---
title: "setDisableStatementPooling Method (SQLServerDataSource)"
description: "setDisableStatementPooling Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setDisableStatementPooling Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the value of the **disableStatementPooling** connection property. If false, enables statement pooling to be used in coupling with statementPoolingCacheSize value > 0.  

## Syntax  
  
```
public void setDisableStatementPooling(boolean disableStatementPooling);  
```  
  
#### Parameters  
 *disableStatementPooling*  
  
 The new value of the **disableStatementPooling** connection property.  

## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
