---
title: "rollback Method (java.sql.Savepoint)"
description: "rollback Method (java.sql.Savepoint)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.rollback (java.sql.Savepoint)"
apitype: "Assembly"
---
# rollback Method (java.sql.Savepoint)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Undoes all changes made after the given [SQLServerSavepoint](../../../connect/jdbc/reference/sqlserversavepoint-class.md) object was set.  
  
## Syntax  
  
```  
  
public void rollback(java.sql.Savepoint s)  
```  
  
#### Parameters  
 *s*  
  
 The SavePoint object to rollback to.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This rollBack method is specified by the rollBack method in the java.sql.Connection interface.  
  
 This method should be used only when auto-commit mode has been disabled.  
  
## Related content

- [rollback Method (SQLServerConnection)](rollback-method-sqlserverconnection.md)
- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
