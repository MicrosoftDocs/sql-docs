---
title: "updateTime Method (int, java.sql.Time)"
description: "updateTime Method (int, java.sql.Time)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateTime (int, java.sql.Time)"
apitype: "Assembly"
---
# updateTime Method (int, java.sql.Time)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a time value given the column index.  
  
## Syntax  
  
```  
  
public void updateTime(int index,  
                       java.sql.Time x)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 A time value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateTime method is specified by the updateTime method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateTime Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatetime-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
