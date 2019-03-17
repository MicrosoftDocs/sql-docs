---
title: "SQLServerXAConnection Members | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 4b61dabd-369b-460c-8450-9fe424f76541
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLServerXAConnection Members
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  The following tables list the members that are exposed by the [SQLServerXAConnection](../../../connect/jdbc/reference/sqlserverxaconnection-class.md) class.  
  
## Constructors  
 None.  
  
## Fields  
 None.  
  
## Inherited Fields  
 None.  
  
## Methods  
  
|Name|Description|  
|----------|-----------------|  
|[addConnectionEventListener](../../../connect/jdbc/reference/addconnectioneventlistener-method-sqlserverpooledconnection.md)|(Inherited from [SQLServerPooledConnection](../../../connect/jdbc/reference/sqlserverpooledconnection-class.md)) Registers the given event listener so that it will be notified when an event occurs on this Connection object.|  
|[close](../../../connect/jdbc/reference/close-method-sqlserverpooledconnection.md)|(Inherited from [SQLServerPooledConnection](../../../connect/jdbc/reference/sqlserverpooledconnection-class.md)) Closes the physical connection that this Connection object represents.|  
|[getConnection](../../../connect/jdbc/reference/getconnection-method-sqlserverpooledconnection.md)|(Inherited from [SQLServerPooledConnection](../../../connect/jdbc/reference/sqlserverpooledconnection-class.md)) Creates an object handle for the physical connection that this Connection object represents.|  
|[getXAResource](../../../connect/jdbc/reference/getxaresource-method-sqlserverxaconnection.md)|Retrieves a [SQLServerXAResource](../../../connect/jdbc/reference/sqlserverxaresource-class.md) object that the transaction manager will use to manage the participation of this [SQLServerXAConnection](../../../connect/jdbc/reference/sqlserverxaconnection-class.md) object in a distributed transaction.|  
|[removeConnectionEventListener](../../../connect/jdbc/reference/removeconnectioneventlistener-method-sqlserverpooledconnection.md)|(Inherited from [SQLServerPooledConnection](../../../connect/jdbc/reference/sqlserverpooledconnection-class.md)) Removes the given event listener.|  
  
## Inherited Methods  
  
|Class inherited from:|Methods|  
|---------------------------|-------------|  
|com.microsoft.sqlserver.jdbc.SQLServerPooledConnection|addConnectionEventListener, close, getConnection, removeConnectionEventListener|  
|java.lang.Object|clone, equals, finalize, getClass, hashCode, notify, notifyAll, toString, wait|  
|javax.sql.PooledConnection|addConnectionEventListener, close, getConnection, removeConnectionEventListener|  
  
## See Also  
 [SQLServerXAConnection Class](../../../connect/jdbc/reference/sqlserverxaconnection-class.md)  
  
  
