---
title: "setLockTimeout Method (SQLServerDataSource)"
description: "setLockTimeout Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setLockTimeout"
apitype: "Assembly"
---
# setLockTimeout Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets an **int** value that indicates the number of milliseconds to wait before the database reports a lock time out.  
  
## Syntax  
  
```  
  
public void setLockTimeout(int lockTimeout)  
```  
  
#### Parameters  
 *lockTimeout*  
  
 An **int** value that contains the number of milliseconds to wait.  
  
## Remarks  
 The lock time out is the number of milliseconds to wait before the database reports a lock time out. The default value of -1 means that it will wait indefinitely. If specified, this value will be the default for all statements on the connection.  
  
> [!NOTE]  
>  A value of 0 means no wait. If the lockTimeout property is not set, the [getLockTimeout](../../../connect/jdbc/reference/getlocktimeout-method-sqlserverdatasource.md) method returns the default value of -1.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
