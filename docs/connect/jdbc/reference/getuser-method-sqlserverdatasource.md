---
title: "getUser Method (SQLServerDataSource)"
description: "getUser Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getUser"
apitype: "Assembly"
---
# getUser Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the user name that is used to connect the data source.  
  
## Syntax  
  
```  
  
public java.lang.String getUser()  
```  
  
## Return Value  
 A **String** that contains the user name.  
  
## Remarks  
 The [setUser](../../../connect/jdbc/reference/setuser-method-sqlserverdatasource.md) method sets the user name that will be used when connecting to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If user name value is not set, the getUser method returns the default value of null.  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
