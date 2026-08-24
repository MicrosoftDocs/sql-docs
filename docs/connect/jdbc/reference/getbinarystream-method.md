---
title: "getBinaryStream Method ()"
description: "getBinaryStream Method ()"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerBlob.getBinaryStream"
apitype: "Assembly"
---
# getBinaryStream Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns an input stream to read data from the BLOB.  
  
## Syntax  
  
```  
  
public java.io.InputStream getBinaryStream()  
```  
  
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
