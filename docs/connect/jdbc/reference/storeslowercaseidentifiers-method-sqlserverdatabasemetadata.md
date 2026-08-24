---
title: "storesLowerCaseIdentifiers Method (SQLServerDatabaseMetaData)"
description: "storesLowerCaseIdentifiers Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.storesLowerCaseIdentifiers"
apitype: "Assembly"
---
# storesLowerCaseIdentifiers Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database treats mixed-case SQL identifiers that are not enclosed in quotation marks as case-insensitive and stores them in lowercase.  
  
## Syntax  
  
```  
  
public boolean storesLowerCaseIdentifiers()  
```  
  
## Return Value  
 **true** if the identifiers are stored in lowercase. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This storesLowerCaseIdentifiers method is specified by the storesLowerCaseIdentifiers method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
