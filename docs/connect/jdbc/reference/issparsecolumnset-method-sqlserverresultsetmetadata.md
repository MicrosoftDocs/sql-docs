---
title: "isSparseColumnSet Method (SQLServerResultSetMetaData)"
description: "isSparseColumnSet Method (SQLServerResultSetMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# isSparseColumnSet Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates if a column in a result set is a sparse column set.  
  
## Syntax  
  
```scr  
public boolean isSparseColumnSet(int column)  
```  
  
#### Parameters  
 *column*  
  
 The (one-based) index of the column.  
  
## Return Value  
 **true** if a column in a result set is a sparse column set, otherwise **false**.  
  
## Remarks  
 This method does not retrieve information from the database.  
  
## See Also  
 [SQLServerResultSetMetaData Methods](../../../connect/jdbc/reference/sqlserverresultsetmetadata-methods.md)   
 [SQLServerResultSetMetaData Members](../../../connect/jdbc/reference/sqlserverresultsetmetadata-members.md)   
 [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)  
  
  
