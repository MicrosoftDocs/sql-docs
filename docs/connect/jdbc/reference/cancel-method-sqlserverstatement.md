---
title: "cancel Method (SQLServerStatement)"
description: "cancel Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.cancel"
apitype: "Assembly"
---
# cancel Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Cancels the SQL statement that is currently being run by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.  
  
## Syntax  
  
```  
  
public final void cancel()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This cancel method is specified by the cancel method in the java.sql.Statement interface.  
  
 When executing a statement that produces a single large forward-only, read-only result set, you might only be interested in some initial set of rows in the returned result set. In this case, the application might call the [cancel](../../../connect/jdbc/reference/cancel-method-sqlserverstatement.md) method of the associated statement object before closing the result set in order to minimize the processing time needed to discard the remaining unnecessary rows. We recommend considering the tradeoff between the processing time that would be saved and the time and the additional round trip to the server needed to cancel the execution when deciding whether to use this technique or not.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
