---
title: "position Method (java.lang.String, long) (SQLServerNClob)"
description: "position Method (java.lang.String, long) (SQLServerNClob)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# position Method (java.lang.String, long) (SQLServerNClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the character position at which the specified substring *searchstr* appears in the **NCLOB** value represented by this **NClob** object.  
  
## Syntax  
  
```  
  
public long position(java.lang.String searchstr,  
              long start)  
```  
  
#### Parameters  
 *searchstr*  
  
 The substring for which to search.  
  
 *start*  
  
 The position at which to begin searching; the first position is 1.  
  
## Return Value  
 The position at which the substring appears, or -1 if it is not present. The first position is 1.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This position method is specified by the position method in the java.sql.NClob interface.  
  
## Related content

- [position Method (SQLServerNClob)](position-method-sqlservernclob.md)
- [SQLServerNClob Methods](sqlservernclob-methods.md)
- [SQLServerNClob Members](sqlservernclob-members.md)
- [SQLServerNClob Class](sqlservernclob-class.md)
