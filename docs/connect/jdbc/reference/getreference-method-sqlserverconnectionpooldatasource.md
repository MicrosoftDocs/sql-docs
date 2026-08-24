---
title: "getReference Method (SQLServerConnectionPoolDataSource)"
description: "getReference Method (SQLServerConnectionPoolDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnectionPoolDataSource.getReference"
apitype: "Assembly"
---
# getReference Method (SQLServerConnectionPoolDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a reference to this [SQLServerConnectionPoolDataSource](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-class.md) object.  
  
## Syntax  
  
```  
  
public javax.naming.Reference getReference()  
```  
  
## Return Value  
 A Reference object.  
  
## Remarks  
 This getReference method is specified by the getReference method in the javax.naming.Referenceable interface. It overrides the [getReference](../../../connect/jdbc/reference/getreference-method-sqlserverdatasource.md) method of the [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) class.  
  
## Related content

- [SQLServerConnectionPoolDataSource Methods](sqlserverconnectionpooldatasource-methods.md)
- [SQLServerConnectionPoolDataSource Members](sqlserverconnectionpooldatasource-members.md)
- [SQLServerConnectionPoolDataSource Class](sqlserverconnectionpooldatasource-class.md)
