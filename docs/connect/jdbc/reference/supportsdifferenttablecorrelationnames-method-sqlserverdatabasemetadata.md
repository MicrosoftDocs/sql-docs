---
title: "supportsDifferentTableCorrelationNames Method"
description: "supportsDifferentTableCorrelationNames Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsDifferentTableCorrelationNames"
apitype: "Assembly"
---
# supportsDifferentTableCorrelationNames Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether, when table correlation names are supported, they are restricted to being different from the names of the tables.  
  
## Syntax  
  
```  
  
public boolean supportsDifferentTableCorrelationNames()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsDifferentTableCorrelationNames method is specified by the supportsDifferentTableCorrelationNames method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
