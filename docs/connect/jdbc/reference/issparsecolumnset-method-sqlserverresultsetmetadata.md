---
title: "isSparseColumnSet Method (SQLServerResultSetMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: ac363670-78ae-49f1-aeda-4fba3329a258
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
