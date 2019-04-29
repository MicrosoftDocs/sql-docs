---
title: "getAsciiStream Method (SQLServerClob) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerClob.getAsciiStream"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 134abe5e-5add-4d27-b333-b4b0f4d94c31
author: MightyPen
ms.author: genemi
manager: craigg
---
# getAsciiStream Method (SQLServerClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Materializes the CLOB as an ASCII stream.  
  
## Syntax  
  
```  
  
public java.io.InputStream getAsciiStream()  
```  
  
## Return Value  
 An input stream that contains the CLOB data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getAsciiStream method is specified by the getAsciiStream method in the java.sql.Clob interface.  
  
 Always returns a stream of bytes and assumes that the data in the CLOB is in an ASCII format because it has no way of knowing if it is in Unicode or any other multi-byte code page.  
  
## See Also  
 [SQLServerClob Methods](../../../connect/jdbc/reference/sqlserverclob-methods.md)   
 [SQLServerClob Members](../../../connect/jdbc/reference/sqlserverclob-members.md)   
 [SQLServerClob Class](../../../connect/jdbc/reference/sqlserverclob-class.md)  
  
  
