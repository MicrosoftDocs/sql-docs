---
title: "getDouble Method (java.lang.String)"
description: "getDouble Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.getDouble (java.lang.String)"
apitype: "Assembly"
---
# getDouble Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a **double** in the Java programming language given the parameter name.  
  
## Syntax  
  
```  
  
public double getDouble(java.lang.String sCol)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
## Return Value  
 A **double** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getDouble method is specified by the getDouble method in the java.sql.CallableStatement interface.  
  
 This method returns all number-based data types with Java **double** fidelity.  
  
## Related content

- [getDouble Method (SQLServerCallableStatement)](getdouble-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
