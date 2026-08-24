---
title: "updateRef Method (int, java.sql.Ref)"
description: "updateRef Method (int, java.sql.Ref)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateRef (int, java.sql.Ref)"
apitype: "Assembly"
---
# updateRef Method (int, java.sql.Ref)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a java.sql.Ref value given the column index.  
  
## Syntax  
  
```  
  
public void updateRef(int columnIndex,  
                      java.sql.Ref x)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 A Ref object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateRef method is specified by the updateRef method in the java.sql.ResultSet interface.  
  
## Related content

- [updateRef Method (SQLServerResultSet)](updateref-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
