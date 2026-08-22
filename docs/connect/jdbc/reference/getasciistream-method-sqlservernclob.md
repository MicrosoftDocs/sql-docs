---
title: "getAsciiStream Method (SQLServerNClob)"
description: "getAsciiStream Method (SQLServerNClob)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
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
  
## Related content

- [SQLServerNClob Methods](sqlservernclob-methods.md)
- [SQLServerNClob Members](sqlservernclob-members.md)
- [SQLServerNClob Class](sqlservernclob-class.md)
