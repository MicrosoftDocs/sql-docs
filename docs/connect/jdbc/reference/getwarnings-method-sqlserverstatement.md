---
title: "getWarnings Method (SQLServerStatement)"
description: "getWarnings Method (SQLServerStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.getWarnings"
apitype: "Assembly"
---
# getWarnings Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the first warning that is reported by calls on this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.  
  
## Syntax  
  
```  
  
public final java.sql.SQLWarning getWarnings()  
```  
  
## Return Value  
 An SQLWarning object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getWarnings method is specified by the getWarnings method in the java.sql.Statement interface.  
  
## Related content

- [SQLServerStatement Members](sqlserverstatement-members.md)
- [SQLServerStatement Class](sqlserverstatement-class.md)
