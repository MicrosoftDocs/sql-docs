---
title: "getLastUpdateCount Method (SQLServerDataSource)"
description: "getLastUpdateCount Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getLastUpdateCount"
apitype: "Assembly"
---
# getLastUpdateCount Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a **Boolean** value that indicates if the lastUpdateCount property is enabled.  
  
## Syntax  
  
```  
  
public boolean getLastUpdateCount()  
```  
  
## Return Value  
 **true** if lastUpdateCount is enabled. Otherwise, **false**.  
  
## Remarks  
 If the lastUpdateCount property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will return only the last update count from an SQL statement passed to the server. If the lastUpdateCount property is set to **false**, the driver will return all update counts including those returned by any triggers that may have fired. If the lastUpdateCount property is not set, the getLastUpdateCount method returns the default value of **true**.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
