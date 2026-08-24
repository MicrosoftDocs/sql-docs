---
title: "updateTime Method (java.lang.String, java.sql.Time)"
description: "updateTime Method (java.lang.String, java.sql.Time)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateTime (java.lang.String, java.sql.Time)"
apitype: "Assembly"
---
# updateTime Method (java.lang.String, java.sql.Time)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a time value given the column name.  
  
## Syntax  
  
```  
  
public void updateTime(java.lang.String columnName,  
                       java.sql.Time x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 A time value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateTime method is specified by the updateTime method in the java.sql.ResultSet interface.  
  
## Related content

- [updateTime Method (SQLServerResultSet)](updatetime-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
