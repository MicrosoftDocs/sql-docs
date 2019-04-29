---
title: "Does JDBC Driver Close Open Result Sets | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 1739ecb5-e5cb-4807-b5a8-97c0299929d0
author: MightyPen
ms.author: genemi
manager: craigg
---
# autoCommitFailureClosesAllResultSets Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether the JDBC driver closes all the open result sets, including the holdable ones, when an auto-commit is enabled and an exception is raised.  
  
## Syntax  
  
```  
  
public boolean autoCommitFailureClosesAllResultSets()  
```  
  
## Return Value  
 **true** if all the open result sets, including the holdable ones, are closed when an auto-commit is enabled and an exception is raised. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This autoCommitFailureClosesAllResultSets method is specified by the autoCommitFailureClosesAllResultSets method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
