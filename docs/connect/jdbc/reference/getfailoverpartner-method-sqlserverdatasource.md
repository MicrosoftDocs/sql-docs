---
title: "getFailoverPartner Method (SQLServerDataSource)"
description: "getFailoverPartner Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getFailoverPartner"
apitype: "Assembly"
---
# getFailoverPartner Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the name of the failover server that is used in a database mirroring configuration.  
  
## Syntax  
  
```  
  
public string getFailoverPartner()  
```  
  
## Return Value  
 A **String** that contains the name of the failover partner, or null if none is set.  
  
## Remarks  
 The value returned by this method reflects the failover partner name set using the [setFailoverPartner](../../../connect/jdbc/reference/setfailoverpartner-method-sqlserverdatasource.md) method.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
