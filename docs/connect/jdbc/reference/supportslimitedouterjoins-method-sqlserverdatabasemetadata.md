---
title: "supportsLimitedOuterJoins Method (SQLServerDatabaseMetaData)"
description: "supportsLimitedOuterJoins Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsLimitedOuterJoins"
apitype: "Assembly"
---
# supportsLimitedOuterJoins Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database provides limited support for outer joins.  
  
## Syntax  
  
```  
  
public boolean supportsLimitedOuterJoins()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsLimitedOuterJoins method is specified by the supportsLimitedOuterJoins method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
