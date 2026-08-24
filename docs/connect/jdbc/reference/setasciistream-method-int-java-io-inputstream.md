---
title: "setAsciiStream Method (int, java.io.InputStream)"
description: "setAsciiStream Method (int, java.io.InputStream)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setAsciiStream Method (int, java.io.InputStream)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter number to the given java.io.InputStream object.  
  
## Syntax  
  
```  
  
public final void setAsciiStream(int parameterIndex,  
                                 java.io.InputStream x)  
```  
  
#### Parameters  
 *parameterIndex*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 A java.io.InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setAsciiStream method is specified by the setAsciiStream method in the java.sql.PreparedStatement interface.  
  
## Related content

- [setAsciiStream Method (SQLServerPreparedStatement)](setasciistream-method-sqlserverpreparedstatement.md)
- [SQLServerPreparedStatement Members](sqlserverpreparedstatement-members.md)
