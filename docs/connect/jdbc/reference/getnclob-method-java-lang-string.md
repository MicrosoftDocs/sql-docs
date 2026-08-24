---
title: "getNClob Method (java.lang.String)"
description: "getNClob Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getNClob Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of a JDBC **NCLOB** parameter as a NClob object in the Java programming language.  
  
## Syntax  
  
```  
  
public java.sql.NClob getNClob(java.lang.String parameterName)  
```  
  
#### Parameters  
 *parameterName*  
  
 A **String** that contains the parameter name.  
  
## Return Value  
 ANClobobject.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getNClob method is specified by the getNClob method in the java.sql.CallableStatement interface.  
  
 This method only supports retrieving **NCHAR**, **NVARCHAR**, **NTEXT**, and **XML** parameters. Calling these methods on other data type parameters will cause an exception.  
  
## Related content

- [getNClob Method (SQLServerCallableStatement)](getnclob-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
