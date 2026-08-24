---
title: "setApplicationName Method (SQLServerDataSource)"
description: "setApplicationName Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setApplicationName"
apitype: "Assembly"
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
 The application name is used to identify the specific application in various [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] profiling and logging tools. If the application name is not set, the getApplicationName method returns the non-localized string " [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]".  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
