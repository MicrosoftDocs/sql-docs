---
title: "getServerName Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.getServerName"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 3004ed22-5d69-4dd0-8761-d39f0b7dde13
author: MightyPen
ms.author: genemi
manager: craigg
---
# getServerName Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the name of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance.  
  
## Syntax  
  
```  
  
public java.lang.String getServerName()  
```  
  
## Return Value  
 A **String** that contains the server name or null if no value is set.  
  
## Remarks  
 The server name is the host name of the target computer that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If the getServerName property is not set, getServerName returns the default value of null.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
