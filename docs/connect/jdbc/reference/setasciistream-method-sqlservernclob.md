---
title: "setAsciiStream Method (SQLServerNClob)"
description: "setAsciiStream Method (SQLServerNClob)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setAsciiStream Method (SQLServerNClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a stream to be used to write ASCII characters to the **NCLOB** value that this **java.sql.NClob** object represents, starting at the specified position.  
  
## Syntax  
  
```  
  
public java.io.OutputStream setAsciiStream(long pos)  
```  
  
#### Parameters  
 *pos*  
  
 The position at which to start writing to the **NCLOB** object; the first position is 1.  
  
## Return Value  
 An OutputStream object that represents the stream to which ASCII encoded characters can be written.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setAsciiStream method is specified by the setAsciiStream method in the java.sql.NClob interface.  
  
## Related content

- [SQLServerNClob Methods](sqlservernclob-methods.md)
- [SQLServerNClob Members](sqlservernclob-members.md)
- [SQLServerNClob Class](sqlservernclob-class.md)
