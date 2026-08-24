---
title: "getBinaryStream Method (long, long)"
description: "getBinaryStream Method (long, long)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getBinaryStream Method (long, long)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns an input stream object that contains a partial BLOB value by using the specified starting position and the length.  
  
## Syntax  
  
```  
  
public java.io.InputStream getBinaryStream(long pos, long length)  
```  
  
#### Parameters  
 *pos*  
  
 The offset to the first byte of the partial value to be retrieved.  
  
 *length*  
  
 The length in bytes of the partial value to be retrieved.  
  
## Return Value  
 An input stream that contains the BLOB data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getBinaryStream method is specified by the getBinaryStream method in the java.sql.Blob interface.  
  
## Related content

- [SQLServerBlob Methods](sqlserverblob-methods.md)
- [SQLServerBlob Members](sqlserverblob-members.md)
- [SQLServerBlob Class](sqlserverblob-class.md)
