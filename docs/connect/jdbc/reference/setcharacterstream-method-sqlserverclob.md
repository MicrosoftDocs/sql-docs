---
title: "setCharacterStream Method (SQLServerClob)"
description: "setCharacterStream Method (SQLServerClob)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerClob.setCharacterStream"
apitype: "Assembly"
---
# setCharacterStream Method (SQLServerClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a stream to be used to write a stream of Unicode characters to the CLOB, starting at the given position.  
  
## Syntax  
  
```  
  
public java.io.Writer setCharacterStream(long pos)  
```  
  
#### Parameters  
 *pos*  
  
 The position at which to start writing to the CLOB object.  
  
## Return Value  
 A stream to which Unicode encoded characters can be written.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This setCharacterStream method is specified by the setCharacterStream method in the java.sql.Clob interface.  
  
 Character data in the CLOB is overwritten by the writer starting at the specified position and can over-run the initial length of the CLOB. Specifying a position+1 value will append characters. Specifying a position+2 or greater (or zero or less) value will cause a position error to be thrown.  
  
## See Also  
 [SQLServerClob Methods](../../../connect/jdbc/reference/sqlserverclob-methods.md)   
 [SQLServerClob Members](../../../connect/jdbc/reference/sqlserverclob-members.md)   
 [SQLServerClob Class](../../../connect/jdbc/reference/sqlserverclob-class.md)  
  
  
