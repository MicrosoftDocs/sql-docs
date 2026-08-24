---
title: "nullsAreSortedAtEnd Method (SQLServerDatabaseMetaData)"
description: "nullsAreSortedAtEnd Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.nullsAreSortedAtEnd"
apitype: "Assembly"
---
# nullsAreSortedAtEnd Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether NULL values are sorted at the end, regardless of sort order.  
  
## Syntax  
  
```  
  
public boolean nullsAreSortedAtEnd()  
```  
  
## Return Value  
 **true** if sorted at the end. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This nullsAreSortedAtEnd method is specified by the nullsAreSortedAtEnd method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
