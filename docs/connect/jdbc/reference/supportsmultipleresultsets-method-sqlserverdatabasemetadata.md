---
title: "supportsMultipleResultSets Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsMultipleResultSets"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: cb4d0b91-db1d-4a6f-a87c-8ea125215afc
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsMultipleResultSets Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports getting multiple [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects from a single call to the [execute](../../../connect/jdbc/reference/execute-method.md) method of the [SQLServerCallableStatement](../../../connect/jdbc/reference/sqlservercallablestatement-class.md) class.  
  
## Syntax  
  
```  
  
public boolean supportsMultipleResultSets()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsMultipleResultSets method is specified by the supportsMultipleResultSets method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
