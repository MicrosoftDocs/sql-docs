---
title: "getWorkstationID Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.getWorkstationID"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: f6a701de-a8fa-4668-9310-99a8c6e32c88
author: MightyPen
ms.author: genemi
manager: craigg
---
# getWorkstationID Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the name of the client computer name that is used to connect to the data source.  
  
## Syntax  
  
```  
  
public java.lang.String getWorkstationID()  
```  
  
## Return Value  
 A **String** that contains the client computer name.  
  
## Remarks  
 The workstationID is the name of the client computer or workstation. If the workstationID property is not set, the default value is constructed by calling InetAddress.getLocalHost().getHostName() method. If getHostName returns a blank value, the getHostAddress().toString() method is called.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
