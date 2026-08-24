---
title: "setLong Method (SQLServerPreparedStatement)"
description: "setLong Method (SQLServerPreparedStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.setLong"
apitype: "Assembly"
---
# setLong Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given **long** value.  
  
## Syntax  
  
```  
  
public final void setLong(int n,  
                          long x)  
```  
  
#### Parameters  
 *n*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 A **long** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setLong method is specified by the setLong method in the java.sql.PreparedStatement interface.  
  
## Related content

- [SQLServerPreparedStatement Members](sqlserverpreparedstatement-members.md)
- [SQLServerPreparedStatement Class](sqlserverpreparedstatement-class.md)
