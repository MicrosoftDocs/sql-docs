---
title: "SupportsDataDefinitionAndDataManipulationTransactions Method"
description: "supportsDataDefinitionAndDataManipulationTransactions Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsDataDefinitionAndDataManipulationTransactions"
apitype: "Assembly"
---
# supportsDataDefinitionAndDataManipulationTransactions Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports both data definition and data manipulation statements within a transaction.  
  
## Syntax  
  
```  
  
public boolean supportsDataDefinitionAndDataManipulationTransactions()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This suportsDataDefinitionAndDataManipulationTransactions method is specified by the suportsDataDefinitionAndDataManipulationTransactions method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
