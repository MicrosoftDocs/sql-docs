---
title: "isPoolable Method (SQLServerStatement)"
description: "isPoolable Method (SQLServerStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# isPoolable Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a value indicating if a statement can be added to the user-provided statement pool.  
  
## Syntax  
  
```  
  
public boolean isPoolable() throws SQLException  
```  
  
## Return Value  
 **true** if the statement can be added to the user-provided statement pool; **false** otherwise.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 [setPoolable](../../../connect/jdbc/reference/setpoolable-method-sqlserverstatement.md) changes the poolable behavior of an object.  
  
## Related content

- [SQLServerStatement Members](sqlserverstatement-members.md)
- [SQLServerStatement Class](sqlserverstatement-class.md)
