---
title: "getFailoverPartner Method (SQLServerDataSource)"
description: "getFailoverPartner Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
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
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
