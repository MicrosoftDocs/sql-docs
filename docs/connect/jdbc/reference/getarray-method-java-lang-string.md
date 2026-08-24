---
title: "getArray Method (java.lang.String)"
description: "getArray Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.getArray (java.lang.String)"
apitype: "Assembly"
---
# getArray Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as an Array object given the parameter name.  
  
## Syntax  
  
```  
  
public java.sql.Array getArray(java.lang.String sCol)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
## Return Value  
 An Array object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getArray method is specified by the getArray method in the java.sql.CallableStatement interface.  
  
## Related content

- [getArray Method (SQLServerCallableStatement)](getarray-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
