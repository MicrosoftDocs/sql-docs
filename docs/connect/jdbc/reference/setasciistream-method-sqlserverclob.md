---
title: "setAsciiStream Method (SQLServerClob)"
description: "setAsciiStream Method (SQLServerClob)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerClob.setAsciiStream"
apitype: "Assembly"
---
# setAsciiStream Method (SQLServerClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a stream to be used to write ASCII characters to the CLOB starting at the given position.  
  
## Syntax  
  
```  
  
public java.io.OutputStream setAsciiStream(long pos)  
```  
  
#### Parameters  
 *pos*  
  
 The position at which to start writing to the CLOB object.  
  
## Return Value  
 The stream to which ASCII encoded characters can be written.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This setAsciiStream method is specified by the setAsciiStream method in the java.sql.Clob interface.  
  
 Character data in the CLOB is overwritten by the output stream starting at the given position and can overrun the initial length of the CLOB. Specifying a position+1 value will append ASCII characters. Specifying a position+2 or greater (or zero or less) value will cause a position error to be thrown.  
  
## See Also  
 [SQLServerClob Methods](../../../connect/jdbc/reference/sqlserverclob-methods.md)   
 [SQLServerClob Members](../../../connect/jdbc/reference/sqlserverclob-members.md)   
 [SQLServerClob Class](../../../connect/jdbc/reference/sqlserverclob-class.md)  
  
  
