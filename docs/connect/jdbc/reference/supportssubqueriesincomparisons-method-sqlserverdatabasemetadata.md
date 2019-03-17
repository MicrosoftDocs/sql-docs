---
title: "supportsSubqueriesInComparisons Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsSubqueriesInComparisons"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 467d32e6-b47e-4095-9f8b-73e07fb814e8
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsSubqueriesInComparisons Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports subqueries in comparison expressions.  
  
## Syntax  
  
```  
  
public boolean supportsSubqueriesInComparisons()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsSubqueriesInComparisons method is specified by the supportsSubqueriesInComparisons method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
