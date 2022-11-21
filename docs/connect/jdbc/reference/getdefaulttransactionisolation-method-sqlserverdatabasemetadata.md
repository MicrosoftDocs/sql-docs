---
title: "getDefaultTransactionIsolation Method (SQLServerDatabaseMetaData)"
description: "getDefaultTransactionIsolation Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getDefaultTransactionIsolation"
apitype: "Assembly"
---
# getDefaultTransactionIsolation Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the default transaction isolation level for this database.  
  
## Syntax  
  
```  
  
public int getDefaultTransactionIsolation()  
```  
  
## Return Value  
 An **int** that indicates the default transaction isolation level.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getDefaultTransactionIsolation method is specified by the getDefaultTransactionIsolation method in the java.sql.DatabaseMetaData interface.  
  
 When using the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this method returns either a value of TRANSACTION_READ_COMMITTED, or the **int** value 2.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
