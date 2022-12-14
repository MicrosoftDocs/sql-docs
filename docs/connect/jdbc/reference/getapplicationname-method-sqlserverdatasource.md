---
title: "getApplicationName Method (SQLServerDataSource)"
description: "getApplicationName Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getApplicationName"
apitype: "Assembly"
---
# getApplicationName Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the application name.  
  
## Syntax  
  
```  
  
public java.lang.String getApplicationName()  
```  
  
## Return Value  
 A **String** that contains the application name, or " [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]" if no value is set.  
  
## Remarks  
 The application name is used to identify the specific application in various [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] profiling and logging tools. If the application name is not set, the getApplicationName method returns the non-localized string " [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]".  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
