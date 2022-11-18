---
title: "cancelRowUpdates Method (SQLServerResultSet)"
description: "cancelRowUpdates Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.cancelRowUpdates"
apitype: "Assembly"
---
# cancelRowUpdates Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Cancels the updates made to the current row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public void cancelRowUpdates()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This cancelRowUpdates method is specified by the cancelRowUpdates method in the java.sql.ResultSet interface.  
  
 This method can be called after calling an updater method and before calling the [updateRow](../../../connect/jdbc/reference/updaterow-method-sqlserverresultset.md) method to roll back the updates that were made to a row. If no updates have been made or updateRow has already been called, this method has no effect.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
