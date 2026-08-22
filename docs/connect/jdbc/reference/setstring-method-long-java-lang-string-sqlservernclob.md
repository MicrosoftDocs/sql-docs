---
title: "setString Method (long, java.lang.String) - NClob"
description: "setString Method (long, java.lang.String) (SQLServerNClob)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setString Method (long, java.lang.String) (SQLServerNClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Writes the specified **String** to the **NCLOB** starting at the specified position.  
  
## Syntax  
  
```  
  
public int setString(long pos,  
              java.lang.String str)  
```  
  
#### Parameters  
 *pos*  
  
 The position at which to start writing to the **NCLOB**; the first position is 1.  
  
 *str*  
  
 The String to be written to the **NCLOB**.  
  
## Return Value  
 The number of characters written.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setString method is specified by the setString method in the java.sql.NClob interface.  
  
## Related content

- [SQLServerNClob Methods](sqlservernclob-methods.md)
- [SQLServerNClob Members](sqlservernclob-members.md)
- [SQLServerNClob Class](sqlservernclob-class.md)
