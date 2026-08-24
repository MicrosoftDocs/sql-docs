---
title: "allProceduresAreCallable Method (SQLServerDatabaseMetaData)"
description: "allProceduresAreCallable Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.allProceduresAreCallable"
apitype: "Assembly"
---
# allProceduresAreCallable Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the current user has permissions to call all the procedures that are returned by the [getProcedures](../../../connect/jdbc/reference/getprocedures-method-sqlserverdatabasemetadata.md) method.  
  
## Syntax  
  
```  
  
public boolean allProceduresAreCallable()  
```  
  
## Return Value  
 **true** if the user has permissions to call all the procedures. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This allProceduresAreCallable method is specified by the allProceduresAreCallable method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
