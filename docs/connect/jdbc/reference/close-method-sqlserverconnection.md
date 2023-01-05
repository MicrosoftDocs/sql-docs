---
title: "close Method (SQLServerConnection)"
description: "close Method (SQLServerConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.close"
apitype: "Assembly"
---
# close Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Releases this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object's database and JDBC resources immediately instead of waiting for them to be automatically released.  
  
## Syntax  
  
```  
  
public void close()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This close method is specified by the close method in the java.sql.Connection interface.  
  
 Calling the close method in the middle of a transaction causes the transaction to be rolled back.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
