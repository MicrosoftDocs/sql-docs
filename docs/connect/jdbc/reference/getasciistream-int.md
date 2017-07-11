---
title: "getAsciiStream (int) | Microsoft Docs"
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
  - "SQLServerCallableStatement.getAsciiStream(int paramIndex)"
apilocation: 
  - "SQLServerCallableStatement.getAsciiStream(int paramIndex)"
apitype: "Assembly"
ms.assetid: 9d8b235e-4208-40ee-b5a5-bc76f73b82f8
caps.latest.revision: 14
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# getAsciiStream (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a stream of **ASCII** characters given the parameter index.  
  
## Syntax  
  
```  
  
public final java.io.InputStream getAsciiStream(int paramIndex)  
```  
  
#### Parameters  
 *paramIndex*  
  
 An **int** that indicates the parameter index.  
  
## Return Value  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## See Also  
 [getAsciiStream Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getasciistream-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  