---
title: "truncate Method (SQLServerBlob)"
description: "truncate Method (SQLServerBlob)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerBlob.truncate"
apitype: "Assembly"
---
# truncate Method (SQLServerBlob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Truncates a BLOB, given the length.  
  
## Syntax  
  
```  
  
public void truncate(long len)  
```  
  
#### Parameters  
 *len*  
  
 The new length for the BLOB.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This truncate method is specified by the truncate method in the java.sql.Blob interface.  
  
## Related content

- [SQLServerBlob Methods](sqlserverblob-methods.md)
- [SQLServerBlob Members](sqlserverblob-members.md)
- [SQLServerBlob Class](sqlserverblob-class.md)
