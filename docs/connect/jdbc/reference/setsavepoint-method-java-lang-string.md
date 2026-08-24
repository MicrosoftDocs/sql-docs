---
title: "setSavepoint Method (java.lang.String)"
description: "setSavepoint Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.setSavepoint (java.lang.String)"
apitype: "Assembly"
---
# setSavepoint Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a savepoint with the given name in the current transaction, and returns the new [SQLServerSavepoint](../../../connect/jdbc/reference/sqlserversavepoint-class.md) object that represents it.  
  
## Syntax  
  
```  
  
public java.sql.Savepoint setSavepoint(java.lang.String sName)  
```  
  
#### Parameters  
 *sName*  
  
 A **String** value that contains the name of the savepoint.  
  
## Return Value  
 A SavePoint object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setSavePoint method is specified by the setSavePoint method in the java.sql.Connection interface.  
  
 The *sName* argument is automatically escaped by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)].  
  
## Related content

- [setSavepoint Method (SQLServerConnection)](setsavepoint-method-sqlserverconnection.md)
- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
