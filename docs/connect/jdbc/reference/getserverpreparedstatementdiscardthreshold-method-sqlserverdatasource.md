---
title: "getServerPreparedStatementDiscardThreshold Method (SQLServerDataSource)"
description: "getServerPreparedStatementDiscardThreshold Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getServerPreparedStatementDiscardThreshold Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the value of **serverPreparedStatementDiscardThreshold** connection property. This setting controls how many outstanding prepared statement discard actions (sp_unprepare) can be outstanding per connection before a call to clean up the outstanding handles on the server is executed. When the setting is <= 1 unprepare actions are executed immediately on prepared statement close. If this value set to > 1 these calls are batched together to avoid overhead of calling sp_unprepare too often.

  
## Syntax  
  
```
public int getServerPreparedStatementDiscardThreshold();  
```  
  
## Return Value  
 Returns the **int** value of the **serverPreparedStatementDiscardThreshold** connection property.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
