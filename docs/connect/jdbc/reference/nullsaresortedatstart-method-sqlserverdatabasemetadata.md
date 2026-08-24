---
title: "nullsAreSortedAtStart Method (SQLServerDatabaseMetaData)"
description: "nullsAreSortedAtStart Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.nullsAreSortedAtStart Method (SQLServerDatabaseMetaData)"
apitype: "Assembly"
---
# nullsAreSortedAtStart Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether NULL values are sorted at the start, regardless of sort order.  
  
## Syntax  
  
```  
  
public boolean nullsAreSortedAtStart()  
```  
  
## Return Value  
 **true** if sorted at the start. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This nullsAreSortedAtStart method is specified by the nullsAreSortedAtStart method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
