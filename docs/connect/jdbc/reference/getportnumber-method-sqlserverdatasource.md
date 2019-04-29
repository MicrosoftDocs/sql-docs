---
title: "getPortNumber Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.getPortNumber"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: e5dc38d0-4340-4ad7-a56e-1d2a0f0fd846
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
