---
title: "getAsciiStream Method (SQLServerClob)"
description: "getAsciiStream Method (SQLServerClob)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerClob.getAsciiStream"
apitype: "Assembly"
---
# getAsciiStream Method (SQLServerClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Materializes the CLOB as an ASCII stream.  
  
## Syntax  
  
```  
  
public java.io.InputStream getAsciiStream()  
```  
  
## Return Value  
 An input stream that contains the CLOB data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getAsciiStream method is specified by the getAsciiStream method in the java.sql.Clob interface.  
  
 Always returns a stream of bytes and assumes that the data in the CLOB is in an ASCII format because it has no way of knowing if it is in Unicode or any other multi-byte code page.  
  
## See Also  
 [SQLServerClob Methods](../../../connect/jdbc/reference/sqlserverclob-methods.md)   
 [SQLServerClob Members](../../../connect/jdbc/reference/sqlserverclob-members.md)   
 [SQLServerClob Class](../../../connect/jdbc/reference/sqlserverclob-class.md)  
  
  
