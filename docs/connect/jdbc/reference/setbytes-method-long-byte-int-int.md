---
title: "setBytes Method (long, byte, int, int) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerBlob.setBytes (long.byte[], int, int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 7def226c-b211-459e-8c1a-08592d75d4a4
author: MightyPen
ms.author: genemi
manager: craigg
---
# setBytes Method (long, byte, int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Writes all or part of the given array of bytes into the BLOB starting at the given position, offset, and length; and then returns the number of bytes written.  
  
## Syntax  
  
```  
  
public int setBytes(long pos,  
                    byte[] bytes,  
                    int offset,  
                    int len)  
```  
  
#### Parameters  
 *pos*  
  
 The position (1 based) in the BLOB at which to start writing the data.  
  
 *bytes*  
  
 The array of bytes to be written into the BLOB.  
  
 *offset*  
  
 The offset in the bytes array where to start reading data from the **byte** array.  
  
 *len*  
  
 The number of bytes to attempt to read from the bytes array into the BLOB.  
  
## Return Value  
 An **int** containing the number of bytes written.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This setBytes method is specified by the setBytes method in the java.sql.Blob interface.  
  
 Data is overwritten starting at the specified position and can over-run the initial length of the BLOB. Specifying a position+1 values will append bytes. Passing a position+2 or greater (or zero or less) value will cause a position error to be thrown. Passing a zero-length **byte** array will return zero because no bytes were written.  
  
## See Also  
 [setBytes Method &#40;SQLServerBlob&#41;](../../../connect/jdbc/reference/setbytes-method-sqlserverblob.md)   
 [SQLServerBlob Methods](../../../connect/jdbc/reference/sqlserverblob-methods.md)   
 [SQLServerBlob Members](../../../connect/jdbc/reference/sqlserverblob-members.md)   
 [SQLServerBlob Class](../../../connect/jdbc/reference/sqlserverblob-class.md)  
  
  
