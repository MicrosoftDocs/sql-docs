---
title: "position Method (java.sql.Blob, long)"
description: "position Method (java.sql.Blob, long)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerBlob.position (java.sql.Blob.long)"
apitype: "Assembly"
---
# position Method (java.sql.Blob, long)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the position of a specified pattern in the BLOB based on the given pattern and starting index.  
  
## Syntax  
  
```  
  
public long position(java.sql.Blob pattern,  
                     long start)  
```  
  
#### Parameters  
 *pattern*  
  
 The pattern to search for.  
  
 *start*  
  
 The start index to search at.  
  
## Return Value  
 A **long** value of the position where the pattern was found, or -1 if it was not found.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This position method is specified by the position method in the java.sql.Blob interface.  
  
## Related content

- [position Method (SQLServerBlob)](position-method-sqlserverblob.md)
- [SQLServerBlob Methods](sqlserverblob-methods.md)
- [SQLServerBlob Members](sqlserverblob-members.md)
- [SQLServerBlob Class](sqlserverblob-class.md)
