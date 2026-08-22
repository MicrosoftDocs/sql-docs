---
title: "setDisableStatementPooling Method (SQLServerConnection)"
description: "setDisableStatementPooling Method (SQLServerConnection)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.setDisableStatementPooling"
apitype: "Assembly"
---
# setDisableStatementPooling Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

 Sets statement pooling to true or false. If false, enables statement pooling to be used in coupling with statementPoolingCacheSize value > 0.

## Syntax  
  
```  
  
public void setDisableStatementPooling(boolean disableStatementPooling)  
```  

#### Parameters  
 *disableStatementPooling*  
  
 The new value of the **disableStatementPooling** connection property.  
 
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## Related content

- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
