---
title: "supportsGroupByBeyondSelect Method (SQLServerDatabaseMetaData)"
description: "supportsGroupByBeyondSelect Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsGroupByBeyondSelect"
apitype: "Assembly"
---
# supportsGroupByBeyondSelect Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports using columns that are not included in the SELECT statement in a GROUP BY clause, provided that all the columns in the SELECT statement are included in the GROUP BY clause.  
  
## Syntax  
  
```  
  
public boolean supportsGroupByBeyondSelect()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsGroupByBeyondSelect method is specified by the supportsGroupByBeyondSelect method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
