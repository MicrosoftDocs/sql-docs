---
title: "supportsDataManipulationTransactionsOnly Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsDataManipulationTransactionsOnly"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: bdc182db-4518-4bf2-b63a-05f58fdb4eb8
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsDataManipulationTransactionsOnly Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports only data manipulation statements within a transaction.  
  
## Syntax  
  
```  
  
public boolean supportsDataManipulationTransactionsOnly()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsDataManipulationTransactionsOnly method is specified by the supportsDataManipulationTransactionsOnly method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
