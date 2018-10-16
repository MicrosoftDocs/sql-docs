---
title: "setBinaryStream Method (SQLServerBlob) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerBlob.setBinaryStream"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: abcec31f-1a60-4765-9725-8cf7e9f1f8ab
author: MightyPen
ms.author: genemi
manager: craigg
---
# setBinaryStream Method (SQLServerBlob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a stream that can be used to write to the BLOB value.  
  
## Syntax  
  
```  
  
public java.io.OutputStream setBinaryStream(long pos)  
```  
  
#### Parameters  
 *Pos*  
  
 The position in the BLOB value at which to start writing)  
  
## Return Value  
 An output stream.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This setBinaryStream method is specified by the setBinaryStream method in the java.sql.Blob interface.  
  
 Data in the BLOB is overwritten by the output stream starting at the specified position and can over-run the initial length of the BLOB. Specifying a position+1 value will append bytes. Passing a position+2 or greater (or zero or less) value will cause a position error to be thrown.  
  
## See Also  
 [SQLServerBlob Methods](../../../connect/jdbc/reference/sqlserverblob-methods.md)   
 [SQLServerBlob Members](../../../connect/jdbc/reference/sqlserverblob-members.md)   
 [SQLServerBlob Class](../../../connect/jdbc/reference/sqlserverblob-class.md)  
  
  
