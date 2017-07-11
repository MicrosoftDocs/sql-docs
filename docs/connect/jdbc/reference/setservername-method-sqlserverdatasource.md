---
title: "setServerName Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerDataSource.setServerName"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 70920828-eda0-4064-be9f-c1e460db8f00
caps.latest.revision: 9
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# setServerName Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the name of the computer that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)].  
  
## Syntax  
  
```  
  
public void setServerName(java.lang.String serverName)  
```  
  
#### Parameters  
 *serverName*  
  
 A **String** that contains the server name.  
  
## Remarks  
 The server name is the host name of the target computer that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)]. If the serverName property is not set, [getServerName](../../../connect/jdbc/reference/getservername-method-sqlserverdatasource.md) returns the default value of null.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  