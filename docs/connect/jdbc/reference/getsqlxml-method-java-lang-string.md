---
title: "getSQLXML Method (java.lang.String)"
description: "getSQLXML Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getSQLXML Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a SQLXML object given the parameter name.  
  
## Syntax  
  
```  
  
public final java.sql.SQLXML getSQLXML(java.lang.String parameterName)  
```  
  
#### Parameters  
 *parameterName*  
  
 A **String** that indicates the parameter name.  
  
## Return Value  
 ASQLXMLobject.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getSQLXML method is specified by the getSQLXML method in the java.sql.CallableStatement interface.  
  
## Related content

- [getSQLXML Method (SQLServerCallableStatement)](getsqlxml-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
