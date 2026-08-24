---
title: "setUser Method (SQLServerDataSource)"
description: "setUser Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setUser"
apitype: "Assembly"
---
# setUser Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the user name that is used to connect the data source.  
  
## Syntax  
  
```  
  
public void setUser(java.lang.String user)  
```  
  
#### Parameters  
 *user*  
  
 A **String** that contains the user name.  
  
## Remarks  
 The setUser method sets the user name that will be used to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If user name value is not set, the [getUser](../../../connect/jdbc/reference/getuser-method-sqlserverdatasource.md) method returns the default value of null.  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
