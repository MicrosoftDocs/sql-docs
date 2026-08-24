---
title: "getLockTimeout Method (SQLServerDataSource)"
description: "getLockTimeout Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getLockTimeout"
apitype: "Assembly"
---
# getLockTimeout Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns an **int** value that indicates the number of milliseconds that the database will wait before reporting a lock time out.  
  
## Syntax  
  
```  
  
public int getLockTimeout()  
```  
  
## Return Value  
 An **int** value that contains the number of milliseconds that the database will wait.  
  
## Remarks  
 The lock time out is the number of milliseconds to wait before the database reports a lock time out. The default value of -1 means that it will wait indefinitely. If specified, this value will be the default for all statements on the connection.  
  
> [!NOTE]  
>  A value of 0 means no wait. If the lockTimeout property is not set, the getLockTimeout method returns the default value of -1.  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
