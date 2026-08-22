---
title: "getServerName Method (SQLServerDataSource)"
description: "getServerName Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getServerName"
apitype: "Assembly"
---
# getServerName Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the name of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance.  
  
## Syntax  
  
```  
  
public java.lang.String getServerName()  
```  
  
## Return Value  
 A **String** that contains the server name or null if no value is set.  
  
## Remarks  
 The server name is the host name of the target computer that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If the getServerName property is not set, getServerName returns the default value of null.  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
