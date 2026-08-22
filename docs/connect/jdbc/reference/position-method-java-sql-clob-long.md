---
title: "position Method (java.sql.Clob, long)"
description: "position Method (java.sql.Clob, long)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerClob.position (java.sql.Clob, long)"
apitype: "Assembly"
---
# position Method (java.sql.Clob, long)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the character position of the specified CLOB object in the CLOB based on the given starting position.  
  
## Syntax  
  
```  
  
public long position(java.sql.Clob searchstr,  
                     long start)  
```  
  
#### Parameters  
 *searchstr*  
  
 The substring to search for.  
  
 *start*  
  
 The position at which to begin searching. The first position is 1.  
  
## Return Value  
 The position at which the substring appears, or -1 if it is not present. The first position is 1.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This position method is specified by the position method in the java.sql.Clob interface.  
  
## Related content

- [position Method (SQLServerClob)](position-method-sqlserverclob.md)
- [SQLServerClob Methods](sqlserverclob-methods.md)
- [SQLServerClob Members](sqlserverclob-members.md)
- [SQLServerClob Class](sqlserverclob-class.md)
