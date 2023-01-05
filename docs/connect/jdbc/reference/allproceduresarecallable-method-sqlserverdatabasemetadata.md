---
title: "allProceduresAreCallable Method (SQLServerDatabaseMetaData)"
description: "allProceduresAreCallable Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
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
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
