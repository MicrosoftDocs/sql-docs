---
title: "updateArray Method (int, java.sql.Array)"
description: "updateArray Method (int, java.sql.Array)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateArray (int, java.sql.Array)"
apitype: "Assembly"
---
# updateArray Method (int, java.sql.Array)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an Array object given the column index.  
  
## Syntax  
  
```  
  
public void updateArray(int columnIndex,  
                        java.sql.Array x)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 An Array object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateArray method is specified by the updateArray method in the java.sql.ResultSet interface.  
  
## Related content

- [updateArray Method (SQLServerResultSet)](updatearray-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
