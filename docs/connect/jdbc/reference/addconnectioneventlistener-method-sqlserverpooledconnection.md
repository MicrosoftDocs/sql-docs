---
title: "addConnectionEventListener Method (SQLServerPooledConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerPooledConnection.addConnectionEventListener"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 142830a8-8d4e-48ca-911d-85bf195ca4fe
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
