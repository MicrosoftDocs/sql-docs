---
title: "getPortNumber Method (SQLServerDataSource)"
description: "getPortNumber Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getPortNumber"
apitype: "Assembly"
---
# getPortNumber Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the current port number that is used to communicate with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Syntax  
  
```  
  
public int getPortNumber()  
```  
  
## Return Value  
 An **int** value that contains the current port number.  
  
## Remarks  
 The port number is the TCP/IP port number that is used when opening a socket connection to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If the portNumber property is not set, the getPortNumber method returns the default value of 1433.  
  
> [!NOTE]  
>  The [setPortNumber](../../../connect/jdbc/reference/setportnumber-method-sqlserverdatasource.md) method does not do any range checking on the port value passed in. You can pass tort numbers that are not valid, like 99999, without triggering an error.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
