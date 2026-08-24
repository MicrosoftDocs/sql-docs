---
title: "getMaxColumnsInTable Method (SQLServerDatabaseMetaData)"
description: "getMaxColumnsInTable Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getMaxColumnsInTable"
apitype: "Assembly"
---
# getMaxColumnsInTable Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of columns this database allows in a table.  
  
## Syntax  
  
```  
  
public int getMaxColumnsInTable()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of columns allowed.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxColumnsInTable method is specified by the getMaxColumnsInTable method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
