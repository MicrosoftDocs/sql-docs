---
title: "getDate Method (java.lang.String) parameter"
description: "getDate Method (java.lang.String) parameter"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.getDate (java.lang.String)"
apitype: "Assembly"
---
# getDate Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a java.sql.Date object in the Java programming language given the parameter name.  
  
## Syntax  
  
```  
  
public java.sql.Date getDate(java.lang.String sCol)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
## Return Value  
 A Date object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getDate method is specified by the getDate method in the java.sql.CallableStatement interface.  
  
 This method returns a valid date part of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **datetime** or **smalldatetime** data type, with the time part set to the Java time baseline of 00:00 (midnight).  
  
## Related content

- [getDate Method (SQLServerCallableStatement)](getdate-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
