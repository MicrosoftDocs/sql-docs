---
title: "SupportsDataDefinitionAndDataManipulationTransactions Method | Microsoft Docs"
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
  - "SQLServerDatabaseMetaData.supportsDataDefinitionAndDataManipulationTransactions"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: fe91c601-9bb3-4364-9131-575a94d3a1b3
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
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
  
  