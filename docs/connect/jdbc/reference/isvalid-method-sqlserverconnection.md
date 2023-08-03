---
title: "isValid Method (SQLServerConnection)"
description: "isValid Method (SQLServerConnection)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# isValid Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object has not been closed and is still valid.  
  
## Syntax  
  
```  
  
public boolean isValid(int timeout)  
```  
  
#### Parameters  
 *timeout*  
  
 An **int** that specifies the number of seconds to wait for validating the connection.  
  
## Return Value  
 **true** if the connection is valid; **false** if the connection is not valid or the validity of the connection cannot be determined before the timeout expires.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isValid method is specified by the isValid method in the java.sql.Connection interface.  
  
## See Also  
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
