---
title: "rollback Method ()"
description: "rollback Method ()"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.rollback ()"
apitype: "Assembly"
---
# rollback Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Undoes all changes made in the current transaction and releases any database locks currently held by this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
## Syntax  
  
```  
  
public void rollback()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This rollBack method is specified by the rollBack method in the java.sql.Connection interface.  
  
 This method should be used only when auto-commit mode has been disabled.  
  
## Related content

- [rollback Method (SQLServerConnection)](rollback-method-sqlserverconnection.md)
- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
