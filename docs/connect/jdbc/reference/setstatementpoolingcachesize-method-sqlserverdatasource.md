---
title: "setStatementPoolingCacheSize Method (SQLServerDataSource)"
description: "setStatementPoolingCacheSize Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 01/20/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setStatementPoolingCacheSize Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the size of the prepared statement cache for this connection. Works if disableStatementPooling is set to false and value > 0.
  
## Syntax  
  
```

public void setStatementPoolingCacheSize(int statementPoolingCacheSize);  
```  
  
#### Parameters  
 *statementPoolingCacheSize*  
  
 The new value of the **statementPoolingCacheSize** connection property.  

## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
