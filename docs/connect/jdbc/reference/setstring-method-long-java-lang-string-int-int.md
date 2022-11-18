---
title: "setString Method (long, java.lang.String, int, int)"
description: "setString Method (long, java.lang.String, int, int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerClob.setString (long, java.lang.String, int, int)"
apitype: "Assembly"
---
# setString Method (long, java.lang.String, int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Writes the given string to the CLOB starting at the given position, based on the given offset and length.  
  
## Syntax  
  
```  
  
public int setString(long pos,  
                     java.lang.String str,  
                     int offset,  
                     int len)  
```  
  
#### Parameters  
 *pos*  
  
 The position at which to start writing to the CLOB.  
  
 *str*  
  
 The string to be written to the CLOB.  
  
 *offset*  
  
 The offset within the string to start reading the characters from.  
  
 *len*  
  
 The number of characters to be written.  
  
## Return Value  
 The number of characters written.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This setString method is specified by the setString method in the java.sql.Clob interface.  
  
 Character data is overwritten starting at the specified position and can overwrite the initial length of the CLOB. Specifying a position+1 value will append the string. Specifying a position+2 or greater (or zero or less) value will cause a position error to be thrown.  
  
## See Also  
 [setString Method &#40;SQLServerClob&#41;](../../../connect/jdbc/reference/setstring-method-sqlserverclob.md)   
 [SQLServerClob Methods](../../../connect/jdbc/reference/sqlserverclob-methods.md)   
 [SQLServerClob Members](../../../connect/jdbc/reference/sqlserverclob-members.md)   
 [SQLServerClob Class](../../../connect/jdbc/reference/sqlserverclob-class.md)  
  
  
