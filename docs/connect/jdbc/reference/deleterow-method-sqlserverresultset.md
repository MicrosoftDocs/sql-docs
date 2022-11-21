---
title: "deleteRow Method (SQLServerResultSet)"
description: "deleteRow Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/20/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.deleteRow"
apitype: "Assembly"
---
# deleteRow Method (SQLServerResultSet)

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Deletes the current row from this[SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object and from the underlying database.  
  
## Syntax  
  
```cpp
public void deleteRow()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This deleteRow method is specified by the deleteRow method in the java.sql.ResultSet interface.  
  
 This method cannot be called when the cursor is on the insert row.  
  
 When using keyset cursors, this method leaves a gap in the result set. You can test for this gap by using the [rowDeleted](../../../connect/jdbc/reference/rowdeleted-method-sqlserverresultset.md) method. The row numbers of the rows in the result set do not change.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
