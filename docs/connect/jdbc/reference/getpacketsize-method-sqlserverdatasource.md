---
title: "getPacketSize Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.getPacketSize"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: b2e9f01a-2e51-47e5-90bf-43c62d1be74d
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
