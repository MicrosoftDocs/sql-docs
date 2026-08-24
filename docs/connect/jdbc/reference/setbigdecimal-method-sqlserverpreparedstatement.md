---
title: "setBigDecimal Method (SQLServerPreparedStatement)"
description: "setBigDecimal Method (SQLServerPreparedStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.setBigDecimal"
apitype: "Assembly"
---
# setBigDecimal Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter number to the given BigDecimal object.  
  
## Syntax  
  
```  
  
public final void setBigDecimal(int n,  
                                java.math.BigDecimal x)  
```  
  
#### Parameters  
 *n*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 A BigDecimal object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setBigDecimal method is specified by the setBigDecimal method in the java.sql.PreparedStatement interface.  
  
## Related content

- [SQLServerPreparedStatement Members](sqlserverpreparedstatement-members.md)
- [SQLServerPreparedStatement Class](sqlserverpreparedstatement-class.md)
