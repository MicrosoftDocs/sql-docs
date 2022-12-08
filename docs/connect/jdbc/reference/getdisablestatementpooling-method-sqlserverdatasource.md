---
title: "getDisableStatementPooling Method (SQLServerDataSource)"
description: "getDisableStatementPooling Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getDisableStatementPooling Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the value of **disableStatementPooling** connection property. This setting controls whether statement pooling is enabled or not for this connection.

  
## Syntax  
  
```
public boolean getDisableStatementPooling();  
```  
  
## Return Value  
 A **boolean** that contains the value of **disableStatementPooling** connection property.
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
