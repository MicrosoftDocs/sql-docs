---
title: "setFailoverPartner Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.setFailoverPartner"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 5310b7c2-9d10-474f-ad3a-218fe5da694b
author: MightyPen
ms.author: genemi
manager: craigg
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
  
 The driver does not support specifying the port number of the failover server when the failover server name is set. However, calling the [setServerName](../../../connect/jdbc/reference/setservername-method-sqlserverdatasource.md) method and the [setInstanceName](../../../connect/jdbc/reference/setinstancename-method-sqlserverdatasource.md) method with the [setFailoverPartner](../../../connect/jdbc/reference/setfailoverpartner-method-sqlserverdatasource.md) method is supported.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
