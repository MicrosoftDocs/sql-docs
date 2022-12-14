---
title: "position Method (java.sql.NClob, long)"
description: "position Method (java.sql.NClob, long)"
author: David-Engel
ms.author: v-davidengel
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
  
## See Also  
 [position Method &#40;SQLServerNClob&#41;](../../../connect/jdbc/reference/position-method-sqlservernclob.md)   
 [SQLServerNClob Methods](../../../connect/jdbc/reference/sqlservernclob-methods.md)   
 [SQLServerNClob Members](../../../connect/jdbc/reference/sqlservernclob-members.md)   
 [SQLServerNClob Class](../../../connect/jdbc/reference/sqlservernclob-class.md)  
  
  
