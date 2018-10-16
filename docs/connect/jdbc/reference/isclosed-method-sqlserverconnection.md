---
title: "isClosed Method (SQLServerConnection) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.isClosed"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 3560ab18-4350-4d02-9716-439f0c2f7142
author: MightyPen
ms.author: genemi
manager: craigg
---
# isClosed Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object has been closed.  
  
## Syntax  
  
```  
  
public boolean isClosed()  
```  
  
## Return Value  
 **true** if the connection is close, **false** if it is not.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isClosed method is specified by the isClosed method in the java.sql.Connection interface.  
  
 Verifies the state of the called SQLServerConnection object. A connection is closed if the [close](../../../connect/jdbc/reference/close-method-sqlserverconnection.md) method has been called on it, or if certain fatal errors have occurred. This method will return **true** only when it is called after the close method has been called.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
