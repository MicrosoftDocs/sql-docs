---
title: "getAsciiStream Method (SQLServerNClob)"
description: "getAsciiStream Method (SQLServerNClob)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getAsciiStream Method (SQLServerNClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the **NCLOB** value designated by this **NClob** object as an ASCII stream.  
  
## Syntax  
  
```  
  
public java.sql.InputStream getAsciiStream()  
```  
  
## Return Value  
 An InputStream object that contains the NCLOB data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getAsciiStream method is specified by the getAsciiStream method in the java.sql.SQLServerNClob interface.  
  
## See Also  
 [SQLServerNClob Methods](../../../connect/jdbc/reference/sqlservernclob-methods.md)   
 [SQLServerNClob Members](../../../connect/jdbc/reference/sqlservernclob-members.md)   
 [SQLServerNClob Class](../../../connect/jdbc/reference/sqlservernclob-class.md)  
  
  
