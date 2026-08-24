---
title: "updateObject Method (java.lang.String, java.lang.Object)"
description: "updateObject Method (java.lang.String, java.lang.Object)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateObject (java.lang.String, java.lang.Object)"
apitype: "Assembly"
---
# updateObject Method (java.lang.String, java.lang.Object)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an **Object** value given the column name.  
  
## Syntax  
  
```  
  
public void updateObject(java.lang.String columnName,  
                         java.lang.Object x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *obj*  
  
 An **Object** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateObject method is specified by the updateObject method in the java.sql.ResultSet interface.  
  
## Related content

- [updateObject Method (SQLServerResultSet)](updateobject-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
