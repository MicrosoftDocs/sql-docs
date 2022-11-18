---
title: "addConnectionEventListener Method (SQLServerPooledConnection)"
description: "addConnectionEventListener Method (SQLServerPooledConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPooledConnection.addConnectionEventListener"
apitype: "Assembly"
---
# addConnectionEventListener Method (SQLServerPooledConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Registers the given event listener so that it will be notified when an event occurs on this [SQLServerPooledConnection](../../../connect/jdbc/reference/sqlserverpooledconnection-class.md) object.  
  
## Syntax  
  
```  
  
public void addConnectionEventListener(javax.sql.ConnectionEventListener listener)  
```  
  
#### Parameters  
 *listener*  
  
 A ConnectionEventListener object.  
  
## Remarks  
 This addConnectionEventListener method is specified by the addConnectionEventListener method in the javax.sql.PooledConnection interface.  
  
## See Also  
 [SQLServerPooledConnection Methods](../../../connect/jdbc/reference/sqlserverpooledconnection-methods.md)   
 [SQLServerPooledConnection Members](../../../connect/jdbc/reference/sqlserverpooledconnection-members.md)   
 [SQLServerPooledConnection Class](../../../connect/jdbc/reference/sqlserverpooledconnection-class.md)  
  
  
