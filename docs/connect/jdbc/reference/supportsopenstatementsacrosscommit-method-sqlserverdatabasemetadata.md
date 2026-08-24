---
title: "supportsOpenStatementsAcrossCommit Method"
description: "supportsOpenStatementsAcrossCommit Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsOpenStatementsAcrossCommit"
apitype: "Assembly"
---
# supportsOpenStatementsAcrossCommit Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports keeping statements open across commits.  
  
## Syntax  
  
```  
  
public boolean supportsOpenStatementsAcrossCommit()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsOpenStatementsAcrossCommit method is specified by the supportsOpenStatementsAcrossCommit method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
