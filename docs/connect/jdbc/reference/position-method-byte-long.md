---
title: "position Method (byte, long) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerBlob.position (byte[], long)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 787412c2-4342-49c8-9ca2-7a9ddcd3277c
caps.latest.revision: 10
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# position Method (byte, long)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the position of a specified pattern in the BLOB based on the given **byte** array pattern and starting index.  
  
## Syntax  
  
```  
  
public long position(byte[] bPattern,  
                     long start)  
```  
  
#### Parameters  
 *bPattern*  
  
 The pattern to search for.  
  
 *start*  
  
 The start index to search at.  
  
## Return Value  
 A **long** value of the position where the pattern was found, or -1 if it was not found.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This position method is specified by the position method in the java.sql.Blob interface.  
  
## See Also  
 [position Method &#40;SQLServerBlob&#41;](../../../connect/jdbc/reference/position-method-sqlserverblob.md)   
 [SQLServerBlob Methods](../../../connect/jdbc/reference/sqlserverblob-methods.md)   
 [SQLServerBlob Members](../../../connect/jdbc/reference/sqlserverblob-members.md)   
 [SQLServerBlob Class](../../../connect/jdbc/reference/sqlserverblob-class.md)  
  
  