---
title: "updateFloat Method (java.lang.String, float)"
description: "updateFloat Method (java.lang.String, float)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateFloat (java.lang.String, float)"
apitype: "Assembly"
---
# updateFloat Method (java.lang.String, float)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **float** value given the column name.  
  
## Syntax  
  
```  
  
public void updateFloat(java.lang.String columnName,  
                        float x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 A **float** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateFloat method is specified by the updateFloat method in the java.sql.ResultSet interface.  
  
## Related content

- [updateFloat Method (SQLServerResultSet)](updatefloat-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
