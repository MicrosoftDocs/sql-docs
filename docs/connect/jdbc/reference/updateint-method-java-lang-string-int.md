---
title: "updateInt Method (java.lang.String, int)"
description: "updateInt Method (java.lang.String, int)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateInt (java.lang.String, int)"
apitype: "Assembly"
---
# updateInt Method (java.lang.String, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an **int** value given the column name.  
  
## Syntax  
  
```  
  
public void updateInt(java.lang.String columnName,  
                      int x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 An **int** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateInt method is specified by the updateInt method in the java.sql.ResultSet interface.  
  
## Related content

- [updateInt Method (SQLServerResultSet)](updateint-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
