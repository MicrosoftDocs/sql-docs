---
title: "truncate Method (SQLServerClob)"
description: "truncate Method (SQLServerClob)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerClob.truncate"
apitype: "Assembly"
---
# truncate Method (SQLServerClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Truncates the CLOB to the given length.  
  
## Syntax  
  
```  
  
public void truncate(long len)  
```  
  
#### Parameters  
 *len*  
  
 The length, in characters, to which the CLOB should be truncated.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This truncate method is specified by the truncate method in the java.sql.Clob interface.  
  
## Related content

- [SQLServerClob Methods](sqlserverclob-methods.md)
- [SQLServerClob Members](sqlserverclob-members.md)
- [SQLServerClob Class](sqlserverclob-class.md)
