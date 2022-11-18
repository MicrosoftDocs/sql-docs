---
title: "isBeforeFirst Method (SQLServerResultSet)"
description: "isBeforeFirst Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.isBeforeFirst"
apitype: "Assembly"
---
# isBeforeFirst Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the cursor is before the first row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public boolean isBeforeFirst()  
```  
  
## Return Value  
 **true** if the cursor is before the first row. **false** if the cursor is at any other position or if the result set contains no rows.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isBeforeFirst method is specified by the isBeforeFirst method in the java.sql.ResultSet interface.  
  
 If this method is used with dynamic cursors, including forward-only read-only cursors, and the selectMethod connection property is set to "cursor", an exception will occur.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
