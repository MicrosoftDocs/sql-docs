---
title: "next Method (SQLServerResultSet)"
description: "next Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.next"
apitype: "Assembly"
---
# next Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves the cursor down one row from its current position.  
  
## Syntax  
  
```  
  
public boolean next()  
```  
  
## Return Value  
 **true** if the new current row is valid. **false** if there are no more rows to process.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This next method is specified by the next method in the java.sql.ResultSet interface.  
  
 A result set cursor is initially positioned before the first row. The first call to the next method makes the first row the current row, the second call makes the second row the current row, and so on.  
  
 If an input stream is open for the current row, a call to the next method will implicitly close it. A warning chain for the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object is cleared when a new row is read.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
