---
title: "truncate Method (SQLServerNClob)"
description: "truncate Method (SQLServerNClob)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# truncate Method (SQLServerNClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Truncates the **NCLOB** to the specified length.  
  
## Syntax  
  
```  
  
public void truncate(long len)  
```  
  
#### Parameters  
 *len*  
  
 The length, in characters, to which the **NCLOB** value should be truncated.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This truncate method is specified by the truncate method in the java.sql.NClob interface.  
  
## Related content

- [SQLServerNClob Methods](sqlservernclob-methods.md)
- [SQLServerNClob Members](sqlservernclob-members.md)
- [SQLServerNClob Class](sqlservernclob-class.md)
