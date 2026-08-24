---
title: "getBlob Method (java.lang.String)"
description: "getBlob Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.getBlob (java.lang.String)"
apitype: "Assembly"
---
# getBlob Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated JDBC BLOB parameter as a Blob object in the Java programming language given the parameter name.  
  
## Syntax  
  
```  
  
public java.sql.Blob getBlob(java.lang.String sCol)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
## Return Value  
 A Blob object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getBlob method is specified by the getBlob method in the java.sql.CallableStatement interface.  
  
## Related content

- [getBlob Method (SQLServerCallableStatement)](getblob-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
