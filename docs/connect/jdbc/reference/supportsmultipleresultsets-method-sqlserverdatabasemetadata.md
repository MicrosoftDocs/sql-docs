---
title: "supportsMultipleResultSets Method (SQLServerDatabaseMetaData)"
description: "supportsMultipleResultSets Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsMultipleResultSets"
apitype: "Assembly"
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
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
