---
title: "getMaxTablesInSelect Method (SQLServerDatabaseMetaData)"
description: "getMaxTablesInSelect Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getMaxTablesInSelect"
apitype: "Assembly"
---
# getMaxTablesInSelect Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of tables this database allows in a SELECT statement.  
  
## Syntax  
  
```  
  
public int getMaxTablesInSelect()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of tables allowed.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxTablesInSelect method is specified by the getMaxTablesInSelect method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
