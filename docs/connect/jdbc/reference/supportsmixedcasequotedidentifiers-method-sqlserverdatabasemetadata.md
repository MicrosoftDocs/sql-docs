---
title: "supportsMixedCaseQuotedIdentifiers Method"
description: "supportsMixedCaseQuotedIdentifiers Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsMixedCaseQuotedIdentifiers"
apitype: "Assembly"
---
# supportsMixedCaseQuotedIdentifiers Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database treats mixed-case SQL identifiers that are enclosed in quotation marks as case-sensitive and stores them in mixed case.  
  
## Syntax  
  
```  
  
public boolean supportsMixedCaseQuotedIdentifiers()  
```  
  
## Return Value  
 **true** if the identifiers are stored in mixed case. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsMixedCaseQuotedIdentifiers method is specified by the supportsMixedCaseQuotedIdentifiers method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
