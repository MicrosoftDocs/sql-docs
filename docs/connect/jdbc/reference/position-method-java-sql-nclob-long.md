---
title: "position Method (java.sql.NClob, long)"
description: "position Method (java.sql.NClob, long)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# position Method (java.sql.NClob, long)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the character position at which the specified **NClob** object *searchstr* appears in this **NClob** object.  
  
## Syntax  
  
```  
  
long position(java.sql.NClob searchstr,  
              long start)  
```  
  
#### Parameters  
 *searchstr*  
  
 A NClob object for which to search.  
  
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
