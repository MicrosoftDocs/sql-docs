---
title: "truncate Method (SQLServerBlob) | Microsoft Docs"
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
  - "SQLServerBlob.truncate"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ef181e04-003a-442a-9b7e-0c508a7cc873
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
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
  
## See Also  
 [SQLServerBlob Methods](../../../connect/jdbc/reference/sqlserverblob-methods.md)   
 [SQLServerBlob Members](../../../connect/jdbc/reference/sqlserverblob-members.md)   
 [SQLServerBlob Class](../../../connect/jdbc/reference/sqlserverblob-class.md)  
  
  