---
title: "getBytes Method (SQLServerBlob)"
description: "getBytes Method (SQLServerBlob)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerBlob.getBytes"
apitype: "Assembly"
---
# getBytes Method (SQLServerBlob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Gets the BLOB data as an array of bytes.  
  
## Syntax  
  
```  
  
public byte[] getBytes(long pos,  
                       int length)  
```  
  
#### Parameters  
 *pos*  
  
 The starting position, starting at 1 (not 0).  
  
 *length*  
  
 The length of the data to get.  
  
## Return Value  
 A **byte** array containing the requested data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getBytes method is specified by the getBytes method in the java.sql.Blob interface.  
  
 If you have a null or zero length BLOB, and try to get exactly zero bytes at position 1, an empty **byte** array is returned (byte array of length 0).  
  
 If you have a null or zero length BLOB, and try to get any length of bytes at a position other than 1, a position exception will be thrown.  
  
## See Also  
 [SQLServerBlob Methods](../../../connect/jdbc/reference/sqlserverblob-methods.md)   
 [SQLServerBlob Members](../../../connect/jdbc/reference/sqlserverblob-members.md)   
 [SQLServerBlob Class](../../../connect/jdbc/reference/sqlserverblob-class.md)  
  
  
