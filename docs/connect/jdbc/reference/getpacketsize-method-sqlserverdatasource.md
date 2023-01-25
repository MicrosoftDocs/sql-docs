---
title: "getPacketSize Method (SQLServerDataSource)"
description: "getPacketSize Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getPacketSize"
apitype: "Assembly"
---
# getPacketSize Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the current network packet size used to communicate with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], specified in bytes.  
  
## Syntax  
  
```  
  
public int getPacketSize()  
```  
  
## Return Value  
 An **int** value containing the current network packet size.  
  
## Remarks  
 If the packetSize property is not set, the getPacketSize method returns the default value of 8000.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
