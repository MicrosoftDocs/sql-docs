---
title: "updateObject Method (int, java.lang.Object, int)"
description: "updateObject Method (int, java.lang.Object, int)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateObject (int, java.lang.Object, int)"
apitype: "Assembly"
---
# updateObject Method (int, java.lang.Object, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an **Object** value given the column index and scale.  
  
## Syntax  
  
```  
  
public void updateObject(int index,  
                         java.lang.Object x,  
                         int scale)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
 *obj*  
  
 An **Object** value.  
  
 *scale*  
  
 For java.sql.Types.DECIMAL or java.sql.Types.NUMERIC types, this is the number of digits after the decimal point. For all other types, this value is ignored.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Related content

- [updateObject Method (SQLServerResultSet)](updateobject-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
