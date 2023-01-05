---
title: "Does Database Ignore Data Definition Statement Within Transaction"
description: "dataDefinitionIgnoredInTransactions Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.dataDefinitionIgnoredInTransactions"
apitype: "Assembly"
---
# dataDefinitionIgnoredInTransactions Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database ignores a data definition statement within a transaction.  
  
## Syntax  
  
```  
  
public boolean dataDefinitionIgnoredInTransactions()  
```  
  
## Return Value  
 **true** if DDL statements are ignored within transactions. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This dataDefinitionIgnoredInTransactions method is specified by the dataDefinitionIgnoredInTransactions method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
