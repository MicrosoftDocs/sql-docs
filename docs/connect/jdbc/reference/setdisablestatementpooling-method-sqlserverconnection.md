---
title: "setDisableStatementPooling Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "drivers"
ms.service: ""
ms.component: "jdbc"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerConnection.setDisableStatementPooling"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid:
caps.latest.revision: 1
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
ms.workload: "Inactive"
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
 
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
