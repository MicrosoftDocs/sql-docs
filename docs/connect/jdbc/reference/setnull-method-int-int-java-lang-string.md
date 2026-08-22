---
title: "setNull Method (int, int, java.lang.String)"
description: "setNull Method (int, int, java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.setNull (int, int, java.lang.String)"
apitype: "Assembly"
---
# setNull Method (int, int, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to a null value, given the type and name of the parameter to set.  
  
## Syntax  
  
```  
  
public final void setNull(int paramIndex,  
                          int sqlType,  
                          java.lang.String typeName)  
```  
  
#### Parameters  
 *paramIndex*  
  
 An **int** that indicates the parameter number.  
  
 *sqlType*  
  
 A JDBC type code defined by java.sql.Types.  
  
 *typeName*  
  
 A **String** that indicates the fully qualified name of the parameter that is being set.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setNull method is specified by the setNull method in the java.sql.PreparedStatement interface.  
  
## Related content

- [setNull Method (SQLServerPreparedStatement)](setnull-method-sqlserverpreparedstatement.md)
- [SQLServerPreparedStatement Members](sqlserverpreparedstatement-members.md)
- [SQLServerPreparedStatement Class](sqlserverpreparedstatement-class.md)
