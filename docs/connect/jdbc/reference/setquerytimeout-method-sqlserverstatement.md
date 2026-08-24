---
title: "setQueryTimeout Method (SQLServerStatement)"
description: "setQueryTimeout Method (SQLServerStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.setQueryTimeout"
apitype: "Assembly"
---
# setQueryTimeout Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the number of seconds the driver will wait for a [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object to run to the given number of seconds.  
  
## Syntax  
  
```  
  
public final void setQueryTimeout(int seconds)  
```  
  
#### Parameters  
 *seconds*  
  
 An **int** that indicates the number of seconds to wait, or 0 if there is no limit.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setQueryTimeout method is specified by the setQueryTimeout method in the java.sql.Statement interface.  
  
## Related content

- [SQLServerStatement Members](sqlserverstatement-members.md)
- [SQLServerStatement Class](sqlserverstatement-class.md)
