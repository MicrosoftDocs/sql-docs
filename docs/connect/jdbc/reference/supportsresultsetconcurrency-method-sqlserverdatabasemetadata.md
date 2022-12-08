---
title: "supportsResultSetConcurrency Method (SQLServerDatabaseMetaData)"
description: "supportsResultSetConcurrency Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.supportsResultSetConcurrency"
apitype: "Assembly"
---
# supportsResultSetConcurrency Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports the given concurrency type in combination with the given result set type.  
  
## Syntax  
  
```  
  
public boolean supportsResultSetConcurrency(int type,  
                                            int concurrency)  
```  
  
#### Parameters  
 *type*  
  
 An **int** that indicates the result set type, which can be one of the following values as defined in java.sql.ResultSet or SQLServerResultSet:  
  
## java.sql.ResultSet Types  
 TYPE_FORWARD_ONLY  
  
 TYPE_SCROLL_SENSITIVE  
  
 TYPE_SCROLL_INSENSITIVE  
  
## SQLServerResultSet Types  
 TYPE_SS_SCROLL_STATIC  
  
 TYPE_SS_SCROLL_KEYSET  
  
 TYPE_SS_DIRECT_FORWARD_ONLY  
  
 TYPE_SS_SERVER_CURSOR_FORWARD_ONLY  
  
 TYPE_SS_SCROLL_DYNAMIC  
  
 *concurrency*  
  
 An **int** that indicates the result set concurrency level, which can be one of the following values as defined in java.sql.ResultSet or SQLServerResultSet:  
  
## Concurrency java.sql.ResultSet Types  
 CONCUR_READ_ONLY  
  
 CONCUR_UPDATABLE  
  
## Concurrency SQLServerResultSet Types  
 CONCUR_SS_OPTIMISTIC_CC  
  
 CONCUR_SS_SCROLL_LOCKS  
  
 CONCUR_SS_OPTIMISTIC_VAL  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsResultSetConcurrency method is specified by the supportsResultSetConcurrency method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
