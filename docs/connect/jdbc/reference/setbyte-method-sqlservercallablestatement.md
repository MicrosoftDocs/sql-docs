---
title: "setByte Method (SQLServerCallableStatement)"
description: "setByte Method (SQLServerCallableStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.setByte"
apitype: "Assembly"
---
# setByte Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given **byte** value.  
  
## Syntax  
  
```  
  
public void setByte(java.lang.String sCol,  
                    byte b)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
 *b*  
  
 A **byte** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setByte method is specified by the setByte method in the java.sql.CallableStatement interface.  
  
## Related content

- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
