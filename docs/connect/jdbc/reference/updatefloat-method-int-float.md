---
title: "updateFloat Method (int, float)"
description: "updateFloat Method (int, float)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateFloat (int, float)"
apitype: "Assembly"
---
# updateFloat Method (int, float)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **float** value given the column index.  
  
## Syntax  
  
```  
  
public void updateFloat(int index,  
                        float x)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
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
