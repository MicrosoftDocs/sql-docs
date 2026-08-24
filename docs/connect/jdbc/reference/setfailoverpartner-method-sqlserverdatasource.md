---
title: "setFailoverPartner Method (SQLServerDataSource)"
description: "setFailoverPartner Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setFailoverPartner"
apitype: "Assembly"
---
# setFailoverPartner Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the name of the failover server that is used in a database mirroring configuration.  
  
## Syntax  
  
```  
  
public void setFailoverPartner(java.lang.String serverName)  
```  
  
#### Parameters  
 *serverName*  
  
 A **String** that contains the failover server name.  
  
## Remarks  
 The value set by this method is used in the case of an initial connection failure to the principal server; after the initial connection is made, this value is ignored. The [setDatabaseName](../../../connect/jdbc/reference/setdatabasename-method-sqlserverdatasource.md) method should also be used in conjunction with this method or an exception will be thrown.  
  
 The driver does not support specifying the port number of the failover server when the failover server name is set. However, calling the [setServerName](../../../connect/jdbc/reference/setservername-method-sqlserverdatasource.md) method and the [setInstanceName](../../../connect/jdbc/reference/setinstancename-method-sqlserverdatasource.md) method with the [setFailoverPartner](#setfailoverpartner-method-sqlserverdatasource) method is supported.  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
