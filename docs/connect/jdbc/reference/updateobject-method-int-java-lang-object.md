---
title: "updateObject Method (int, java.lang.Object)"
description: "updateObject Method (int, java.lang.Object)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateObject (int, java.lang.Object)"
apitype: "Assembly"
---
# updateObject Method (int, java.lang.Object)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an **Object** value given the column index.  
  
## Syntax  
  
```  
  
public void updateObject(int index,  
                         java.lang.Object obj)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
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
