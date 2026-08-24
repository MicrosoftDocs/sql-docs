---
title: "allTablesAreSelectable Method (SQLServerDatabaseMetaData)"
description: "allTablesAreSelectable Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.allTablesAreSelectable"
apitype: "Assembly"
---
# allTablesAreSelectable Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the current user has permissions to use all the tables that are returned by the [getTables](../../../connect/jdbc/reference/gettables-method-sqlserverdatabasemetadata.md) method in a SELECT statement.  
  
## Syntax  
  
```  
  
public boolean allTablesAreSelectable()  
```  
  
## Return Value  
 **true** if the user has permissions to call use all the tables. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This allTablesAreSelectable method is specified by the allTablesAreSelectable method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
