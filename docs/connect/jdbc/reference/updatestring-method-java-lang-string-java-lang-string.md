---
title: "updateString Method (java.lang.String, java.lang.String)"
description: "updateString Method (java.lang.String, java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateString (java.lang.String, java.lang.String)"
apitype: "Assembly"
---
# updateString Method (java.lang.String, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **String** value given the column name.  
  
## Syntax  
  
```  
  
public void updateString(java.lang.String columnName,  
                         java.lang.String x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 A **String** object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateString method is specified by the updateString method in the java.sql.ResultSet interface.  
  
## Related content

- [updateString Method (SQLServerResultSet)](updatestring-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
