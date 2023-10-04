---
title: "close Method (SQLServerResultSet)"
description: "close Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.close"
apitype: "Assembly"
---
# close Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Releases this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object's database and JDBC resources immediately instead of waiting for this to happen when it is automatically closed.  
  
## Syntax  
  
```  
  
public void close()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This close method is specified by the close method in the java.sql.ResultSet interface.  
  
 A SQLServerResultSet object is automatically closed by the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object that generated it when that SQLServerStatement object is closed, re-run, or used to retrieve the next result from a sequence of multiple results. A SQLServerResultSet object is also automatically closed when it is garbage collected.  
  
 When executing a statement that produces a single large forward-only, read-only result set, you might only be interested in some initial set of rows in the returned result set. In this case, the application might call the [cancel](../../../connect/jdbc/reference/cancel-method-sqlserverstatement.md) method of the associated statement object before closing the result set in order to minimize the processing time needed to discard the remaining unnecessary rows. We recommend considering the tradeoff between the processing time that would be saved and the time and the additional round trip to the server needed to cancel the execution when deciding whether to use this technique or not.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
