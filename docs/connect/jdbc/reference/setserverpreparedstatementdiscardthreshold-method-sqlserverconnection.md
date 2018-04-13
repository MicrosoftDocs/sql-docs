---
title: "setServerPreparedStatementDiscardThreshold Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: "sql"
ms.prod_service: "drivers"
ms.service: ""
ms.component: "jdbc"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerConnection.setServerPreparedStatementDiscardThreshold"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid:
caps.latest.revision: 1
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
ms.workload: "Inactive"
---
# setServerPreparedStatementDiscardThreshold Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

 Specifies the behavior for a specific connection instance. This setting controls how many outstanding prepared statement discard actions (sp_unprepare) can be outstanding per connection before a call to clean up the outstanding handles on the server is executed. When the setting is <= 1 un-prepare actions are executed immediately on prepared statement close. If the value is set to > 1 these calls are batched together to avoid overhead of calling sp_unprepare too often.


## Syntax  
  
```  
  
public void setServerPreparedStatementDiscardThreshold(boolean thresholdValue)  
```  

#### Parameters  
 *thresholdValue*  
 
 The new value of the **serverPreparedStatementDiscardThreshold** connection property.  
 
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
 
## Remarks  
 This method is available from JDBC driver version 6.4 and onward.
 
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
