---
title: "wasNull Method (SQLServerCallableStatement)"
description: "wasNull Method (SQLServerCallableStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.wasNull"
apitype: "Assembly"
---
# wasNull Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the last OUT parameter read had the value of SQL NULL.  
  
## Syntax  
  
```  
  
public boolean wasNull()  
```  
  
## Return Value  
 **true** if the last parameter read was null. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This wasNull method is specified by the wasNull method in the java.sql.CallableStatement interface.  
  
## Related content

- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
