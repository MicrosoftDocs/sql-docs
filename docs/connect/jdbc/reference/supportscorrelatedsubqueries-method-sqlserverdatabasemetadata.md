---
title: "supportsCorrelatedSubqueries Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsCorrelatedSubqueries"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 85bb1bcc-31ae-4f6b-a103-699724bbb0aa
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsCorrelatedSubqueries Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports correlated subqueries.  
  
## Syntax  
  
```  
  
public boolean supportsCorrelatedSubqueries()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsCorelatedSubqueries method is specified by the supportsCorelatedSubqueries method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
