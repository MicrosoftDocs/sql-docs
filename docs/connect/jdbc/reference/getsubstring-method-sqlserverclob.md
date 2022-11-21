---
title: "getSubString Method (SQLServerClob)"
description: "getSubString Method (SQLServerClob)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerClob.getSubString"
apitype: "Assembly"
---
# getSubString Method (SQLServerClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a copy of the specified substring in the CLOB based on the given starting position and the number of characters to copy.  
  
## Syntax  
  
```  
  
public java.lang.String getSubString(long pos,  
                                     int length)  
```  
  
#### Parameters  
 *pos*  
  
 The first character of the substring to be extracted. The first character is at position 1.  
  
 *length*  
  
 The number of consecutive characters to be copied.  
  
## Return Value  
 A **String** that is the specified substring in the CLOB.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getSubString method is specified by the getSubString method in the java.sql.Clob interface.  
  
 Trying to get zero characters from a null or zero-length CLOB returns an empty string. Trying to get any length of characters at any position other than position 1 in a zero-length CLOB will cause a position exception to be thrown.  
  
## See Also  
 [SQLServerClob Methods](../../../connect/jdbc/reference/sqlserverclob-methods.md)   
 [SQLServerClob Members](../../../connect/jdbc/reference/sqlserverclob-members.md)   
 [SQLServerClob Class](../../../connect/jdbc/reference/sqlserverclob-class.md)  
  
  
