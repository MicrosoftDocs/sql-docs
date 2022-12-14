---
title: "setPortNumber Method (SQLServerDataSource)"
description: "setPortNumber Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setPortNumber"
apitype: "Assembly"
---
# setPortNumber Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the port number to be used to communicate with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Syntax  
  
```  
  
public void setPortNumber(int portNumber)  
```  
  
#### Parameters  
 *portNumber*  
  
 An **int** value that contains the port number.  
  
## Remarks  
 The port number is the TCP/IP port number that is used when opening a socket connection to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If the portNumber property is not set, the [getPortNumber](../../../connect/jdbc/reference/getportnumber-method-sqlserverdatasource.md) method returns the default value of 1433.  
  
> [!NOTE]  
>  The setPortNumber method does not do any range checking on the port value passed in. You can pass a port number that is not valid, like 99999, without triggering an error.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
