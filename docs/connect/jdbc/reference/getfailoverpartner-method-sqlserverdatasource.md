---
title: "getFailoverPartner Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.getFailoverPartner"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 885f927f-9c48-42e0-a7fb-fd936d2b8130
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
