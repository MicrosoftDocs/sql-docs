---
title: "setApplicationName Method (SQLServerDataSource) | Microsoft Docs"
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
  - "SQLServerDataSource.setApplicationName"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 24d6e48d-53c4-4da2-a6de-1cdff463c9cd
caps.latest.revision: 12
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# setApplicationName Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the application name.  
  
## Syntax  
  
```  
  
public void setApplicationName(java.lang.String applicationName)  
```  
  
#### Parameters  
 *applicationName*  
  
 A **String** that contains the name of the application.  
  
## Remarks  
 The application name is used to identify the specific application in various [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] profiling and logging tools. If the application name is not set, the getApplicationName method returns the non-localized string "[!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]".  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  