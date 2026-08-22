---
title: "nullPlusNonNullIsNull Method (SQLServerDatabaseMetaData)"
description: "nullPlusNonNullIsNull Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.nullPlusNonNullIsNull"
apitype: "Assembly"
---
# nullPlusNonNullIsNull Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether this database supports concatenations between NULL and non-NULL values being NULL.  
  
## Syntax  
  
```  
  
public boolean nullPlusNonNullIsNull()  
```  
  
## Return Value  
 **true** if the concatenations are supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This nullPlusNonNullIsNull method is specified by the nullPlusNonNullIsNull method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
