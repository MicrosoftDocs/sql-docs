---
title: "getStatementPoolingCacheSize Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid:
author: MightyPen
ms.author: genemi
manager: craigg
---
# getStatementPoolingCacheSize Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the value of **statementPoolingCacheSize** connection property. Returns the size of the prepared statement cache for this connection. '0' means caching not enabled.
  
## Syntax  
  
```
public boolean getStatementPoolingCacheSize();  
```  
  
## Return Value  
 The **int** value of the **statementPoolingCacheSize** connection property.  

## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
