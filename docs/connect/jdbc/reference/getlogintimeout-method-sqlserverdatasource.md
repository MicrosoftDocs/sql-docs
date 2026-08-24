---
title: "getLoginTimeout Method (SQLServerDataSource)"
description: "getLoginTimeout Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getLoginTimeout"
apitype: "Assembly"
---
# getLoginTimeout Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the number of seconds that this [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) object will wait while trying to make a connection.  
  
## Syntax  
  
```  
  
public int getLoginTimeout()  
```  
  
## Return Value  
 An **int** value that represents the number of seconds to wait.  
  
## Remarks  
 If the application does not specify a timeout value explicitly, this method returns a default value of 15 seconds.  
  
 This getLoginTimeout method is specified by the getLoginTimeout method in the javax.sql.DataSource interface.  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
