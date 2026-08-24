---
title: "execute Method (java.lang.String) (SQLServerStatement)"
description: "execute Method (java.lang.String) (SQLServerStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.execute (java.lang.String)"
apitype: "Assembly"
---
# execute Method (java.lang.String) (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Runs the given SQL statement, which can return multiple results.  
  
## Syntax  
  
```  
  
public boolean execute(java.lang.String sql)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** that contains a SQL statement.  
  
## Return Value  
 **true** if the first result is a result set. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This execute method is specified by the execute method in the java.sql.Statement interface.  
  
## Related content

- [execute Method (SQLServerStatement)](execute-method-sqlserverstatement.md)
- [SQLServerStatement Members](sqlserverstatement-members.md)
- [SQLServerStatement Class](sqlserverstatement-class.md)
